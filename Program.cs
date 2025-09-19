using EmployeeLeaveManagementSystem.Data;
using EmployeeLeaveManagementSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using EmployeeLeaveManagementSystem.Models;
using Microsoft.OpenApi.Models;

/*var hasher = new PasswordHasher<Employee>();
var hash = hasher.HashPassword(null, "Admin@123");
Console.WriteLine(hash);*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Leave Management API", Version = "v1" });

    //Add JWT Auth definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    //Require JWT globally
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


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
