using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Servises;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));

builder.Services.AddScoped<IDishService, DishService>();

var app = builder.Build();

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