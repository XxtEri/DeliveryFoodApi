using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using webNET_Hits_backend_aspnet_project_2.JWT;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Servises;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeliveryFood API", Version = "v1"});
    
    // c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    // {
    //     Description = "Please enter token",
    //     In = ParameterLocation.Header,
    //     Type = SecuritySchemeType.Http,
    //     BearerFormat = "JWT",
    //     Scheme = "Bearer"
    // });
    //
    // c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    // {
    //     {
    //         new OpenApiSecurityScheme
    //         {
    //             Reference = new OpenApiReference
    //             {
    //                 Type = ReferenceType.SecurityScheme,
    //                 Id = "Bearer"
    //             },
    //         },
    //         new List<string>()
    //     }
    // });
    
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "webNET-Hits-backend-aspnet-project-2.xml");
    c.IncludeXmlComments(filePath);
});

var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //валидация издателя при валидации токена
            ValidateIssuer = true,
            //строка, представляющая издателя
            ValidIssuer = JwtConfigurations.Issuer,
            //валидация потребителя токена
            ValidateAudience = true,
            //установка потребителя токена
            ValidAudience = JwtConfigurations.Audience,
            //валидация времени существования
            ValidateLifetime = true,
            //установка ключа безопасности
            IssuerSigningKey = JwtConfigurations.GetSymmetricSecurityKey(),
            //валидация ключа безопасности
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IUserService, UserService>();

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