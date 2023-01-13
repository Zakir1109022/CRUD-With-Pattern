using CRUD.Application.Services;
using CRUD.Common.Producer;
using CRUD.Common.RabbitMQConnection;
using CRUD.Common.Services;
using CRUD.Common.TenantConfig;
using CRUD.Extentions;
using CRUD.Infrastructure;
using CRUD.RabbitMQ;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RabbitMQ.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var provider = builder.Services.BuildServiceProvider();
var Configuration = provider.GetRequiredService<IConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region FluentValidation
builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = false;
});
#endregion


#region JWT Authentication

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,// on production make it true
                    ValidateAudience = false,// on production make it true
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
#endregion




#region Tenant Config Dependencies

builder.Services.Configure<TenantSettings>(Configuration.GetSection("TenantSettings"));

builder.Services.AddSingleton<ITenantSettings>(serviceProvider =>
    serviceProvider.GetRequiredService<IOptions<TenantSettings>>().Value);

builder.Services.AddSingleton<IHttpContextAccessor>(serviceProvider =>
   serviceProvider.GetRequiredService<IOptions<HttpContextAccessor>>().Value);

#endregion

#region RabbitMQ Dependencies

builder.Services.AddSingleton<IRabbitMQConnection>(sp =>
{
    var factory = new ConnectionFactory()
    {
        HostName = Configuration["EventBus:HostName"]
    };

    if (!string.IsNullOrEmpty(Configuration["EventBus:UserName"]))
    {
        factory.UserName = Configuration["EventBus:UserName"];
    }

    if (!string.IsNullOrEmpty(Configuration["EventBus:Password"]))
    {
        factory.Password = Configuration["EventBus:Password"];
    }

    return new RabbitMQConnection(factory);
});

builder.Services.AddSingleton<EventBusRabbitMQProducer>();
builder.Services.AddSingleton<EventBusRabbitMQConsumer>();

#endregion

#region Project Dependencies

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasherService, PasswordHasherService>();

#endregion

//Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add MediatR
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//Initilize Rabbit Listener in ApplicationBuilderExtentions
app.UseRabbitListener();

app.Run();
