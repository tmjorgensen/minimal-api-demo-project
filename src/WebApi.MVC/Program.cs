using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
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

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
