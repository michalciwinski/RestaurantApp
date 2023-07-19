using RestaurantApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using RestaurantApp.DBSeeder;
using RestaurantApp.Controllers;
using RestaurantApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<RestaurantDbContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("EntitiesDB")));
builder.Services.AddControllers();
builder.Services.AddScoped<SeederDishType>();


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

app.MapControllers();
app.Run();
