using RestaurantApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using RestaurantApp.DBSeeder;
using RestaurantApp.Controllers;
using RestaurantApp.Services;
using RestaurantApp.Services.Interface;
using RestaurantApp.Services.Implementation;
using RestaurantApp.Controllers.Implementation;
using RestaurantApp.Controllers.Interface;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<RestaurantDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("EntitiesDB")));
builder.Services.AddControllers();
builder.Services.AddScoped<SeederDishType>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IController_Menu, Controller_Menu>();
builder.Services.AddScoped<IController_UserAccount, Controller_UserAccount>();

builder.Services.AddCors(options =>
{
     options.AddPolicy("AllowOrigin",
       builder => { builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
           });
                            

    //builder => builder.AllowAnyOrigin() 
    //                  .AllowAnyMethod()  
    //                  .AllowAnyHeader()); 

    //options.AddPolicy(name: MyAllowSpecificOrigins,
   //                  policy =>
    //                  {
    //                      policy.WithOrigins("http://localhost:3000")
    //                        .AllowAnyHeader()
    //                        .AllowAnyMethod();
     //                 });

});// added becaouse of CORS problem on forntend



var app = builder.Build();


//seed db tables
var _DB = new RestaurantDbContext();
var _SeederDishType = new SeederDishType(_DB);
var _SeederUserType = new SeederUserType(_DB);
var _SeederState = new SeederState(_DB);
var _SeederMenu = new SeederMenu(_DB);
_SeederDishType.Seeder();
_SeederUserType.Seeder();
_SeederState.Seeder();
_SeederMenu.Seeder();

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
