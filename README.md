# DoctorHouse API

Find a place to stay while coronavirus ends for medical staff

![image](https://user-images.githubusercontent.com/8453022/77238526-b857fb80-6b9e-11ea-8411-2c1bd7f0631f.png)

You can see the mockups here:

https://xd.adobe.com/view/90541b33-88f5-400d-6dd8-b676b5f9495e-e665/

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

```
curl --location --request POST 'https://localhost:44370/api/v1/auth' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'username=test@test.com' \
--data-urlencode 'password=123456' \
--data-urlencode 'grant_type=password'
```

Copy the `access_token` property and add it to the Swagger authentication

![image](https://user-images.githubusercontent.com/8453022/77238271-be4cdd00-6b9c-11ea-935b-de907c59c3d7.png)

# Endpoints

This is the list of endpoints:

- Users
   - ✔️ Authentication
   - ✔️ GET by ID
   - ✔️ POST
   - ✔️ PUT
- Places
   - ✔️ GET
   - ✔️ GET by ID
   - ❌ POST
   - ❌ PUT
   - ❌ DELETE
- Requests
   - ❌ GET
   - ❌ GET by ID
   - ❌ POST
   - ❌ PUT
   - ❌ DELETE
- Guests
   - ❌ GET
   - ❌ PUT
- Locations
   - ❌ GET

![image](https://user-images.githubusercontent.com/8453022/77238367-4b903180-6b9d-11ea-8a9a-fc85c4b37476.png)


# Other features

- ❌ Send email notifications
- ❌ Unit testing
- ❌ Front end (If you have any ideas you can start here https://github.com/betocastillo86/doctorhouse.web)
- ❌ Seeding full list of countries and cities
- ❌ CD / CI