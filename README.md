# minimal-api-demo-project

This repository accompanies a story on Medium.com that aims at debunking some myths about ASP.NET Minimal API. It contains a very basic project mangement api, implemented as both FastEndpoints, ASP.NET MVC (Controllers), and ASP:NET Minimal API.

## How to run

To run the api you must first clone the repository to your local machine. Then you can navigate to one of the folders naed `./src/WebApi.<something>`. Inside one of the WebApi folders you can then run the following command:
```
dotnet run --launch-profile "https"
```
Alterntively, you can open the `./src/MinimalAPI.sln` solution in Visual Studio, select one of the WebApi projets as startup project, select the `https` launcg profile, and run the application.

Once the application is running you can open the file `./src/webapi.http` in Visual Studio or Visual Studio Code (with the RestClient add-on installed), and sign in by using the first HTTP request to sign in and get an access token. Then explore the other HTTP requests in the file.
