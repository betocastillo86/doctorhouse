using System;
using System.Linq;
using Beto.Core.Data;
using DoctorHouse.Business.Security;
using DoctorHouse.Data;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;

namespace DoctorHouse.Business.Tasks
{
    public class SendMailTask : ITask
    {
        private readonly ILogger<SendMailTask> logger;

        private readonly IRepository<EmailNotification> notificationRepository;

        private readonly IConfiguration configuration;

        public SendMailTask(
            IRepository<EmailNotification> notificationRepository,
            ILogger<SendMailTask> logger,
            IConfiguration configuration)
        {
            this.notificationRepository = notificationRepository;
            this.logger = logger;
            this.configuration = configuration;
        }

        [AutomaticRetry(Attempts = 0, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public void SendPendingMails()
        {
            var mails = this.notificationRepository.Table
                .AsTracking()
                .Where(c => !c.IsSent && c.SentTries < 3)
                .Take(20)
                .ToList();

            try
            {
                foreach (EmailNotification mail in mails)
                {
                    try
                    {
                        this.SendMessage(mail);
                        mail.SentDate = DateTime.UtcNow;
                        mail.IsSent = true;
                    }
                    catch (Exception e)
                    {
                        mail.SentTries++;
                        this.logger.LogError(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                this.logger.LogError(e.ToString());
            }
            finally
            {
                if (mails.Count > 0)
                {
                    this.notificationRepository.Update(mails);
                }
            }
        }

        private void SendMessage(EmailNotification notification)
        {
            var hostSmtp = this.configuration["HostSmtp"];
            var userSmtp = this.configuration["UserSmtp"];
            var passwordSmtp = this.configuration["PasswordSmtp"];
            var portSmtp = this.configuration["PortSmtp"];
            var emailSenderName = this.configuration["EmailSenderName"];
            var emailSenderEmail = this.configuration["EmailSenderEmail"];

            var mailsEnabled = Convert.ToBoolean(this.configuration["MailsEnabled"]);

            if (mailsEnabled)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(emailSenderName, emailSenderEmail));
                message.To.Add(new MailboxAddress(notification.ToName, notification.To));
                message.Subject = notification.Subject;
                message.Body = new TextPart(TextFormat.Html)
                {
                    Text = notification.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(
                        hostSmtp,
                        Convert.ToInt32(portSmtp),
                        false);

                    client.Authenticate(userSmtp, passwordSmtp);

                    client.Send(message);

                    client.Disconnect(true);
                }
            }
        }
    }
}