using DL;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var conString = builder.Configuration.GetConnectionString("ConnectionDB");
builder.Services.AddDbContext<FbatallaProgramacionNcapasContext>(options =>
    options.UseSqlServer(conString));


builder.Services.AddScoped<BL.Empleado>();
builder.Services.AddScoped<SL_API.Controllers.EmpleadosAPIController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
