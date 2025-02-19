using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using WebApi.Endpoints;
using WebApi.Extensions;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddValidators()
    .AddFluentValidationAutoValidation();

builder.Services
    .AddWebApiAuthentication(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

var validationGroup = app.MapGroup("/")
    .AddFluentValidationAutoValidation();

validationGroup.MapGroup("/")
    .AllowAnonymous()
    .UseIdentityEndpoints();

validationGroup.MapGroup("/")
    .RequireAuthorization()
    .UseProjectEndpoints()
    .UseActivityEndpoints();

app.Run();
