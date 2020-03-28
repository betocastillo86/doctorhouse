using Newtonsoft.Json;

namespace DoctorHouse.Api.Models
{
    public class AuthResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}