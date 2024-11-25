﻿using RestaurantApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.IdentityModel.Tokens;
using RestaurantApp.DBSeeder;
using RestaurantApp.Controllers;
using RestaurantApp.Services.Interface;
using RestaurantApp.Services.Implementation;
using RestaurantApp.Controllers.Implementation;
using RestaurantApp.Controllers.Interface;
using RestaurantApp;
using System.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using RestaurantApp.Model;
using RestaurantApp.Model.Validators;
using FluentValidation.AspNetCore;
using RestaurantApp.Authentication;
using System.Text;
using RestaurantApp.Middleware;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

//Authentication
var authenticationSettings = new AuthenticationSettings();
config.GetSection("Authentication").Bind(authenticationSettings);



// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<RestaurantDbContext>(options =>
    options.UseNpgsql(config["ConnectionStrings:EntitiesDB"]));
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddControllers().AddFluentValidation();//fluentvalidation because of Validator..
//builder.Services.AddScoped<SeederDishType>();
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuIngredientsService, MenuIngredientsService>();
builder.Services.AddScoped<IController_Menu, Controller_Menu>();
builder.Services.AddScoped<IController_MenuWithIngredients, Controller_MenuWithIngredients>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IController_UserAccount, Controller_UserAccount>();
builder.Services.AddScoped<IController_Order, Controller_Order>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPasswordHasher<Tuser>, PasswordHasher<Tuser>>();//hash passwords
builder.Services.AddScoped<IValidator<ModelUserRegister>, ModelUserRegisterValidator>(); //external validator for registration
builder.Services.AddScoped<IValidator<ModelUserLogin>, ModelUserLoginValidator>(); //external validator for registration


//************seed db tables
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
});// ******added becaouse of CORS problem on frontend




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//place for middleware
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseCors("AllowOrigin");
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.Run();
