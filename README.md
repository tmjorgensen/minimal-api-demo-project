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

The complete solution is a very simple project management application. It allows you to perform CRUD operations (create, read, update, delete) on projects, and on activities within projects.

The solution is structured using the principles of Clean Architecture. This means that it consists of the following parts:

- **Web api:** The entry point, where HTTP requests are mapped to business logic, in the form of individual UseCases.
- **Application:** The business logic layer; the "smart" project. All actions that the system can take are described here in the form of UseCases.
- **Domain:** The domain model.
- **Infrastructure:** Implementation of services that interact with external resources such as a database or a third-party api.

In this solution there are **three** web apis. This is because this solution is part of a demo where the three api frameworks FastEndpoints, ASP.NET Core MVC (Controllers) and ASP.NET Core Minimal API are shown doing the same job.

There is only one Infrastructure project, and it does not really interact with anything. It fakes how you would do it if you were using a database to store projects and activities. In a real-world scenario there would probably be both a Infrastructure.DataStore.SqlServer (using Microsoft SqlServer database infrastructure), a Infrastructure.Notifications.SendGrid (using SendGrid email infrastructure) and possibly other Infrastructure projects as well.

## The three web apis

The solution contains three web apis that each do the exact same job, but in different ways:

- **WebApi.MVC:** The traditional ASP.NET Core MVC (Controllers) web api, where a controller class contains a number of related endpoints.
- **WebApi.MinimalAPI:** The ASP.NET Core Minimal API approach, where endpoints are not implemented but configured, using functions instead of classes.
- **WebApi.FastEndpoints:** ASP.NET Core web api using the FastEndpoints framework, where endpoints are implemented as individual classes.

All three web apis do the exact same job: Providing the endpoints needed by the project management application, and routing HTTP requests to the correct business logic. To achieve this they each configure authentication, set up dependency injection, and the HTTP pipeline.

### WebApi.MVC

This web api is the more traditional one: Endpoints are implemented in controlle classes, found in the `Controllers`folder.

Note that the `AtiitiesController`and `ProjectsController` have been implemented in two different ways: 

- ProjectsController uses a standard Dependency Injection approach, where services are injected in the constructor and stored in readonly fields, later to be used on various methods.
- ActivitiesController uses an approach where the ASP.NET Core framework injects the services in the individual methods. In this way, only the services actually used by an individual endpoint needs to be resolved by the Dependency Injection contianer.

### WebApi.MinimalAPI

The built-in alternative to MVC controllers - this is what you get when you create a new web api projcet in Visual Studio, and uncheck the option `Use controllers`.

The template shows you how to configure an endpoint inside the `Program.cs` file. However, this is only meant to sho you how the configuration is done; you should not place all your endpoints there. Instead, as this project demonstrates you create extension methods where you, in a structured manner, define the related endpoints.

This project defines three extension methods, handling identity, project and activity endpoints respectively. In `Program.cs` the endpoints are first set up to use validation (implemented with `FluentValidation`), then grouped by whether they require authorization or not, and then the extension methods to add the endpoints are called.

This approach gives you a clear structure for your endpoints: You can at a glance determine which areas of your application require authorization, validation, or other requirements you may have, and at the same time provides a clear path to find the code for each endpoint.

### WebApi.FastEndpoint

FastEndpoints is implemented by first creating a ASP.NET Core web api, then adding the `FastEndpoints` NuGet package. From there, each endpoint is implemented as individual classes . The Fastendpoints framework suggests using a REPR (Request-EndPoint-Response) approach, where the classes defining the related request, edpoint and response, are placed together.

For the `Projcets` endpoints the individual parts (request, response, endpoint and validator) are placed in the same file. For the `Activities`endpoints they are placed in folders as individual files.

