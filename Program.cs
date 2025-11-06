using Microsoft.EntityFrameworkCore;
using TRIAL.Persistence.Repository;
using TRIAL.Services;
using TRIAL.Persistence;
using TRIAL.Services.Implementations;
// using VerificationRegisterN;
// using AssigningRoleU;
using Microsoft.Extensions.Configuration;
using EmailSending;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
//using TRIAL.Middleware;

//using exceptionHandlingMiddleware;

var builder = WebApplication.CreateBuilder(args);


// Add logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();



// Add services to the container.
builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("MyConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<ISubjectsService, SubjectsService>(); //this method registers the interface and its implementation with the DI container in ASP.NET Core.
builder.Services.AddScoped<IHomeworkTeacherService, HomeworkTeacherService>();
builder.Services.AddScoped<IStudentHomeworkService, StudentHomeworkService>();
builder.Services.AddScoped<HomeworkCleanupService>();
builder.Services.AddScoped<Emailsending>();
builder.Services.AddScoped<IMarksService, MarksService>();

// start Configure JWT authentication (authorization)
var key = Encoding.ASCII.GetBytes("ThisIsASuperLongSecretKeyForJWTToken12345"); // same key you used for token generation

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        RoleClaimType = ClaimTypes.Role

    };
});


builder.Services.AddAuthorization();
//end

// Register the necessary services for RoleMiddleware
//builder.Services.AddScoped<RoleMiddleware>();

//
//builder.Services.AddScoped<ExceptionHandlingMiddleware>();


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API V1", Version = "v1" });

    // start Add JWT Authentication support in Swagger(authorization)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

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
            Array.Empty<string>()
        }
    });//end authorization
});


var app = builder.Build();

// Use middleware in the app
//app.UseMiddleware<RoleMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1"); });
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Configure the HTTP request pipeline. (authentication the authorization)
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

