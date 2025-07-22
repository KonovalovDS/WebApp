using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Services;
using WebApplication1.Properties;
using Microsoft.AspNetCore.Identity;
using WebApplication1.Models;
using WebApplication1;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<OrderStorage>();
builder.Services.AddSingleton<ProductStorage>();
builder.Services.AddSingleton<UserStorage>();

builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
