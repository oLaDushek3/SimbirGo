using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TestApi;
using TestApi.Jwt;
using TestApi.Models;
using TestApi.Repositories;
using TestApi.Services;
using TestApi.Services.Account;
using TestApi.Services.Rent;
using TestApi.Services.Transport;

var builder = WebApplication.CreateBuilder(args);

var jwtOptions = builder.Configuration
    .GetSection("JwtOptions")
    .Get<JwtOptions>();

var connectionString = builder.Configuration
    .GetSection("ConnectionStrings")
    .Get<DbConnectionString>();
    
builder.Services.AddSingleton(jwtOptions!);
builder.Services.AddSingleton(connectionString!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        //convert the string signing key to byte array
        byte[] signingKeyBytes = Encoding.UTF8
            .GetBytes(jwtOptions!.Secret);

        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.ValidIssuer,
            ValidAudience = jwtOptions.ValidAudience,
            IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
        };
    });

void ConfigureRepositories(IServiceCollection services)
{
    services.AddScoped<IAccountRepository, AccountRepository>();
    services.AddScoped<IUsersSessionsRepository, UsersSessionsRepository>();
    services.AddScoped<ITransportRepository, TransportRepository>();
    services.AddScoped<ITransportTypeRepository, TransportTypeRepository>();
    services.AddScoped<ITransportModelRepository, TransportModelRepository>();
    services.AddScoped<IColorRepository, ColorRepository>();
    services.AddScoped<IPriceTypeRepository, PriceTypeRepository>();
    services.AddScoped<IRentRepository, RentRepository>();
}
void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IAccountService, AccountService>();
    services.AddScoped<ITransportServices, TransportServices>();
    services.AddScoped<IRentServices, RentServices>();
    services.AddScoped<SimbirGoContext, SimbirGoContext>();
}

ConfigureServices(builder.Services);
ConfigureRepositories(builder.Services);

builder.Services.AddAuthorization();

builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // To Enable authorization using Swagger (JWT)
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();
app.Run();