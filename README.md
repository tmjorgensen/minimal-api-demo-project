# minimal-api-demo-project

This repository accompanies a story on Medium.com that aims at debunking some myths about ASP.NET Minimal API. It contains a very basic project mangement api, implemented as both FastEndpoints, ASP.NET Core MVC (Controllers), and ASP:NET Core Minimal API.

## How to run

To run the api you must first clone the repository to your local machine. Then you can navigate to one of the folders naed `./src/WebApi.<something>`. Inside one of the WebApi folders you can then run the following command:
```
dotnet run --launch-profile "https"
```
Alterntively, you can open the `./src/MinimalAPI.sln` solution in Visual Studio, select one of the WebApi projets as startup project, select the `https` launcg profile, and run the application.

Once the application is running you can open the file `./src/webapi.http` in Visual Studio or Visual Studio Code (with the RestClient add-on installed), and sign in by using the first HTTP request to sign in and get an access token. Then explore the other HTTP requests in the file.

## What is it?

The complete solution is a very simple project management api. It allows you to perform CRUD operations (create, read, update, delete) on projects, and on activities within projects.

The solution is structured using the principles of Clean Architecture. This means that it consists of the following parts:

- **Web api:** The entry point, where HTTP requests are mapped to business logic, in the form of individual UseCases.
- **Application:** The business logic layer; the "smart" project. All actions that the system can take are described here in the form of UseCases.
- **Domain:** The domain model.
- **Infrastructure:** Implementation of services that interact with external resources such as a database or a third-party api.

In this solution there are **three** web apis. This is because this solution is part of a demo where the three api frameworks FastEndpoints, ASP.NET Core MVC (Controllers) and ASP.NET Core Minimal API are shown doing the same job.

There is only one Infrastructure project, and it does not really interact with anything. It fakes how you would do it if you were using a database to store projects and activities. In a real-world scenario there would probably be both a Infrastructure.DataStore.SqlServer (using Microsoft SqlServer database infrastructure), a Infrastructure.Notifications.SendGrid (using SendGrid email infrastructure) and possibly other Infrastructure projects as well.
