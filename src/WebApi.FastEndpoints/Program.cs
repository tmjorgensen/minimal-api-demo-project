using FastEndpoints;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebApiAuthentication(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices();

builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app
    .UseCustomExceptionhandler()
    .UseFastEndpoints();

app.Run();
