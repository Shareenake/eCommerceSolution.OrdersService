using eCommerce.OrderService.DataAccessLayer;

using eCommerce.OrderService.BusinessLogicLayer;
using FluentValidation.AspNetCore;
using eCommerce.OrdersService.Middleware;
using eCommerce.OrderService.BusinessLogicLayer.Client;


var builder = WebApplication.CreateBuilder(args);

//Add DAL and BAL Services
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessLogicLayer();

//Controllers
builder.Services.AddControllers();

//Fluent Validation
builder.Services.AddFluentValidationAutoValidation();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cores
builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});
builder.Services.AddHttpClient<UserMicroServiceClient>(
    client =>
    {
        client.BaseAddress =new Uri($"http://{builder.Configuration["UserMicroServiceName"]}:" +
            $"{builder.Configuration["UserMicroServicePort"]}");
    });
var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();

//Cors
app.UseCors();

//Swagger
app.UseSwagger();
app.UseSwaggerUI();

//Auth
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthentication();

//Endpoints
app.MapControllers();

app.Run();
