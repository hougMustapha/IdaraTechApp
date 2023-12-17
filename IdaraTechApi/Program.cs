using Google.Apis.Logging;
using IdaraTech_Admin.Data;
using IdaraTech_Admin.Models;
using IdaraTech_Admin.Services;
using IdaraTechApi;
using IdaraTechApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ContextSeedService>();

builder.Services.AddIdentityCore<User>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;

    options.SignIn.RequireConfirmedEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<Context>()
    .AddSignInManager<SignInManager<User>>()
    .AddUserManager<UserManager<User>>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
          ValidIssuer = builder.Configuration["JWT:Issuer"],
          ValidateIssuer = true,
          ValidateAudience = false
      };
  });

builder.Services.AddCors();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
        .Where(x => x.Value.Errors.Count > 0)
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage).ToArray();

        var toReturn = new
        {
            Errors = errors
        };

        return new BadRequestObjectResult(toReturn);
    };
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("SuperAdminPolicy", policy => policy.RequireRole("SuperAdmin"));
    opt.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    opt.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
    opt.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
    opt.AddPolicy("CitizenPolicy", policy => policy.RequireRole("Citoyen"));

    opt.AddPolicy("AdminOrManagerPolicy", policy => policy.RequireRole("Admin", "Manager"));
    opt.AddPolicy("AdminAndManagerPolicy", policy => policy.RequireRole("Admin").RequireRole("Manager"));
    opt.AddPolicy("AllRolePolicy", policy => policy.RequireRole("SuperAdmin", "Admin", "Manager", "Customer", "Citoyen"));

    opt.AddPolicy("AdminEmailPolicy", policy => policy.RequireClaim(ClaimTypes.Email, "admin@example.com"));
    opt.AddPolicy("HananeSurnamePolicy", policy => policy.RequireClaim(ClaimTypes.Surname, "Hanane"));
    opt.AddPolicy("ManagerEmailAndNacerSurnamePolicy", policy => policy.RequireClaim(ClaimTypes.Surname, "Nacer")
        .RequireClaim(ClaimTypes.Email, "manager@example.com"));
    opt.AddPolicy("SHIPPolicy", policy => policy.RequireAssertion(context => SD.SHIPPolicy(context)));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["JWT:ClientUrl"]);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region ContextSeed
using var scope = app.Services.CreateScope();
try
{
    var contextSeedService = scope.ServiceProvider.GetService<ContextSeedService>();
    await contextSeedService.InitializeContextAsync();
}
catch(Exception ex)
{
    var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
    logger.LogError(ex.Message, "Failed to initialize and seed the database");
}
#endregion

app.Run();
