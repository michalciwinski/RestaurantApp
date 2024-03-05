using RestaurantApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using RestaurantApp.DBSeeder;
using RestaurantApp.Controllers;
using RestaurantApp.Services.Interface;
using RestaurantApp.Services.Implementation;
using RestaurantApp.Controllers.Implementation;
using RestaurantApp.Controllers.Interface;
using RestaurantApp;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();




// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseNpgsql(config["ConnectionStrings:EntitiesDB"]));





builder.Services.AddControllers();
builder.Services.AddScoped<SeederDishType>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuIngredientsService, MenuIngredientsService>();
builder.Services.AddScoped<IController_Menu, Controller_Menu>();
builder.Services.AddScoped<IController_MenuWithIngredients, Controller_MenuWithIngredients>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IController_UserAccount, Controller_UserAccount>();
builder.Services.AddScoped<IController_Order, Controller_Order>();
builder.Services.AddScoped<IOrderService, OrderService>();
//builder.Services.AddHostedService<DatabaseListenerService>(); //registered iwebhost service to listen chagnes in correct tables in database

//seed db tables
var _DB = new RestaurantDbContext(config["ConnectionStrings:EntitiesDB"]);
var _SeederDishType = new SeederDishType(_DB);
var _SeederUserType = new SeederUserType(_DB);
var _SeederState = new SeederState(_DB);
var _SeederMenu = new SeederMenu(_DB);
_SeederDishType.Seeder();
_SeederUserType.Seeder();
_SeederState.Seeder();
_SeederMenu.Seeder();

builder.Services.AddHostedService(provider =>
{
    return new DatabaseListenerService(config["ConnectionStrings:EntitiesDB"], config["OpenAIClient"], config["AssistantsEndpoint"], _DB);
});


builder.Services.AddCors(options =>
{
     options.AddPolicy("AllowOrigin",
       builder => { builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
           });
});// added becaouse of CORS problem on forntend




var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllers();
app.Run();
