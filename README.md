# DoctorHouse API

Find a place to stay while coronavirus ends


# Getting started

The project is developed with .Net Core 3.0 and uses:

- EF Core
- Migrations
- Swagger

**Update submodules**

Open a terminal window on the project's folder and run `git submodule update --init --recursive`.

**Update database**

Before starting please change the connection string on the file `ext/DoctorHouse.Api/appsettings.json`. After that on the Package Manager Console run `update-database -project doctorhouse.data`.

**Run the application**

Run the application and go to Swagger UI where you will find the different endpoints to be developed. https://localhost:44370/swagger/index.html

In order to authenticate you can generate a token with this cURL:

`curl --location --request POST 'https://localhost:44370/api/v1/auth' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'username=test@test.com' \
--data-urlencode 'password=123456' \
--data-urlencode 'grant_type=password'`

Copy the 'access_token' property and add it to the Swagger authentication.


