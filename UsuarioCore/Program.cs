using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5075")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
// Add services to the container.
builder.Services.AddControllersWithViews();



var conString = builder.Configuration.GetConnectionString("ConnectionDB");

builder.Services.AddDbContext<FbatallaProgramacionNcapasContext>(options =>
    options.UseSqlServer(conString));

builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Rol>();
builder.Services.AddScoped<BL.Estado>();
builder.Services.AddScoped<BL.Colonia>();
builder.Services.AddScoped<BL.Municipio>();
builder.Services.AddScoped<BL.Empleado>();
builder.Services.AddScoped<BL.Departamento>();
builder.Services.AddScoped<BL.Empleado>();






var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
