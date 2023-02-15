using MasterDataManagement.API.Extensions;
using MasterDataManagement.API.Helpers;
using MasterDataManagement.API.Middleware;
using MasterDataManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddAuthentication("Bearer")
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Authority = "https://localhost:5000";
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateAudience = false
                        };
                    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "masterDataManagementClient", "masterData_mvc_client"));
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

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
////// Migrate database runtime
////using (var scope = app.Services.CreateScope())
////{
////    var services = scope.ServiceProvider;

////    var context = services.GetRequiredService<StoreContext>();
////    if (context.Database.GetPendingMigrations().Any())
////    {
////        context.Database.Migrate();
////    }
////}

app.Run();

