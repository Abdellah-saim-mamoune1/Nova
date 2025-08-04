using bankApI.Data;
using bankApI.Interfaces.Repositories.Employee;
using bankApI.Interfaces.Repositories.Shared;
using bankApI.Interfaces.RepositoriesInterfaces.AuthenticationRepositoryInterfaces;
using bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces;
using bankApI.Interfaces.RepositoriesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.AuthenticationServicesInterfaces;
using bankApI.Interfaces.ServicesInterfaces.ClientServicesInterfaces;
using bankApI.Interfaces.ServicesInterfaces.Employee;
using bankApI.Interfaces.ServicesInterfaces.EmployeeServicesInterfaces;
using bankApI.Interfaces.ServicesInterfaces.Shared;
using bankApI.Repositories.AuthenticationRepositories;
using bankApI.Repositories.ClientRepositories;
using bankApI.Repositories.Employee;
using bankApI.Repositories.EmployeeRepositories;
using bankApI.Repositories.Shared;
using bankApI.Services.AuthenticationServices;
using bankApI.Services.Client;
using bankApI.Services.Employee;
using bankApI.Services.EmployeeServices;
using bankApI.Services.Shared;
using bankApI.Validators.Client;
using bankApI.Validators.Employee;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthenticationValidationService, AuthenticationValidationService>();
builder.Services.AddScoped<IClientManagementService, ClientManagementService>();
builder.Services.AddScoped<INotificationsManagementService, NotificationsManagementService>();
builder.Services.AddScoped<ITransactionsManagementService, TransactionsManagementService>();
builder.Services.AddScoped<IEmployeeManagementService, EmployeeManagementService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IClientAdminService, ClientAdminService>();
builder.Services.AddScoped<IClientInfoGetService, ClientInfoGetService>();
builder.Services.AddScoped<INotificationsService, NotificationsService>();
builder.Services.AddScoped<ITransactionsService, TransactionsService>();
builder.Services.AddScoped<IBankRevenueService, BankRevenueService>();

//Repos  
builder.Services.AddScoped<IClientManagementRepository, ClientManagementRepository>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddScoped<bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces.INotificationsRepository, bankApI.Repositories.ClientRepositories.NotificationsRepository>();
builder.Services.AddScoped<bankApI.Interfaces.RepositoriesInterfaces.Employee.INotificationsRepository, bankApI.Repositories.Employee.NotificationsRepository>();
builder.Services.AddScoped<bankApI.Interfaces.RepositoriesInterfaces.ClientRepositoriesInterfaces.ITransactionsManagementRepository, TransactionsRepository>();
builder.Services.AddScoped<IEmployeeManagementRepository, EmployeeManagementRepository>();
builder.Services.AddScoped<IStatisticsRepository, StatisticsRepository>();
builder.Services.AddScoped<IClientAdminRepository, ClientAdminRepository>();
builder.Services.AddScoped<IClientInfoGetRepository, ClientInfoGetRepository>();
builder.Services.AddScoped<bankApI.Interfaces.Repositories.Employee.ITransactionsManagementRepository,bankApI.Repositories.Employee.TransactionsManagementRepository>();
builder.Services.AddScoped<IBankRevenueManagementRepository, BankRevenueManagementRepository>();
builder.Services.AddScoped<bankApI.Interfaces.Repositories.Client.IStatisticsRepository,bankApI.Repositories.Client.StatisticsRepository>();


//Validators
builder.Services.AddValidatorsFromAssemblyContaining<TransferFundSetDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ClientUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TransferFundSetDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<NotificationsDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BankEmailDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TransferFundSetDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<DepositWithdrawDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeAccountDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TransferFundSetDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<PersonDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SetEmailStateDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();


// ? Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
           
        };
     
    });

builder.Services.AddAuthorization();
builder.Services.AddMemoryCache();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("https://nova-zhe3.vercel.app")
                  .AllowCredentials()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});




Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseHttpsRedirection();


//Extracting  JWT from the cookie and append it to the api header instead of storing JWT in local storage
app.Use(async (context, next) =>
{
    var accessToken = context.Request.Cookies["accessToken"];
    if (!string.IsNullOrEmpty(accessToken) &&
        !context.Request.Headers.ContainsKey("Authorization"))
    {
        context.Request.Headers.Append("Authorization", $"Bearer {accessToken}");
    }

    await next();
});



app.UseCors("AllowReactApp");

app.UseAuthentication(); 
app.UseAuthorization();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
