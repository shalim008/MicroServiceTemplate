using MasterDataManagement.API.Extensions;
using MasterDataManagement.API.Helpers;
using MasterDataManagement.API.Middleware;
using MasterDataManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));

// Database 
builder.Services.AddDbContext<StoreContext>(x =>
x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services for DI.
builder.Services.AddApplicationServices();

// Enable Cors Request
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4401");
    });
});


var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseCors("CorsPolicy");
app.MapControllers();

// Migrate database runtime
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<StoreContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

app.Run();

