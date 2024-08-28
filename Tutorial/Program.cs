using IntergrateMongodb.Helps;
using IntergrateMongodb.Mappings;
using IntergrateMongodb.Models;
using IntergrateMongodb.Repositories;
using IntergrateMongodb.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDbGenericRepository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomCors("AllowAllOrigins");


// add config
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddAutoMapper(typeof(ModelMapping));

builder.Services.AddSingleton<MongoDbAppContext>(); 

// identity config
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddMongoDbStores<MongoDbAppContext>(builder.Services.BuildServiceProvider().GetService<MongoDbAppContext>())
    .AddDefaultTokenProviders(); 
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.Name = ".WebApiTestCookie";
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.Headers["Location"] = context.RedirectUri;
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
    options.Events.OnRedirectToAccessDenied = context =>
    {
        context.Response.Headers["Location"] = context.RedirectUri;
        context.Response.StatusCode = 403;
        return Task.CompletedTask;
    };  
});


builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddVersioning();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
