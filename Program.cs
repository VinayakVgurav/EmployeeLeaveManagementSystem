using EmployeeLeaveManagementSystem.Data;
using EmployeeLeaveManagementSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure DbContext with connection string
builder.Services.AddDbContext<Mydbcontext>(e=>e.UseSqlServer(builder.Configuration.GetConnectionString("EMPDB")));

//Register services with Dependency Injection
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();

//Configure JWT Authentication
var key = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,                                 // validate token issuer
            ValidateAudience = true,                               // validate token audience
            ValidateLifetime = true,                               // check token expiry
            ValidateIssuerSigningKey = true,                       // validate token signature
            ValidIssuer = builder.Configuration["Jwt:Issuer"],     // from appsettings.json
            ValidAudience = builder.Configuration["Jwt:Audience"], // from appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Use authentication and authorization
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
