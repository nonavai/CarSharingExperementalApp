
using System.Text;
using BusinessLogic.Mapping;
using BusinessLogic.Models.Activity;
using BusinessLogic.Models.Borrower;
using BusinessLogic.Models.Car;
using BusinessLogic.Models.Lender;
using BusinessLogic.Models.Roles;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using BusinessLogic.Services.Implemetation;
using BusinessLogic.Validators;
using CarSharingAPI.Mapping;
using CarSharingAPI.Middleware;
using DataAccess.DbContext;
using FluentValidation.AspNetCore;
using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddAuthentication(x =>
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x=> x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy();
});
/*builder.Services.AddAuthentication(x =>
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddAuthentication(x =>
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme);*/


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CarSharingContext>(options => options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=CarSharingDB;Trusted_Connection=True;"));
ConfigureServices(builder.Services);

var app = builder.Build();


Configure(app);
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

void Configure(WebApplication app)
{
    app.UseMiddleware<LoggingMiddleware>();
    app.UseMiddleware<ExceptionMiddleware>();
}
void ConfigureServices(IServiceCollection serviceCollection)
{
    serviceCollection.AddTransient<ICarRepository, CarRepository>();
    serviceCollection.AddTransient<ICarService, CarService>();
    
    serviceCollection.AddTransient<IUserRepository, UserRepository>();
    serviceCollection.AddTransient<IUserService, UserService>();
    
    serviceCollection.AddTransient<ILenderRepository, LenderRepository>();
    serviceCollection.AddTransient<ILenderService, LenderService>();
    
    serviceCollection.AddTransient<IFeedBackRepository, FeedBackRepository>();
    serviceCollection.AddTransient<IFeedBackService, FeedBackService>();
    
    serviceCollection.AddTransient<IBorrowerRepository, BorrowerRepository>();
    serviceCollection.AddTransient<IBorrowerService, BorrowerService>();
    
    serviceCollection.AddTransient<IActivityRepository, ActivityRepository>();
    serviceCollection.AddTransient<IActivityService, ActivityService>();
    
    serviceCollection.AddTransient<IDealRepository, DealRepository>();
    serviceCollection.AddTransient<IDealService, DealService>();
    
    serviceCollection.AddTransient<IRolesRepository, RolesRepository>();
    serviceCollection.AddTransient<IRolesService, RolesService>();
    
    serviceCollection.AddAutoMapper(typeof(MappingProfile));
    serviceCollection.AddAutoMapper(typeof(MappingProfileApi));
    
    serviceCollection.AddTransient<IValidator<ActivityDto>, ActivityValidator>();
    serviceCollection.AddTransient<IValidator<BorrowerDto>, BorrowerValidator>();
    serviceCollection.AddTransient<IValidator<CarDto>, CarValidator>();
    //serviceCollection.AddTransient<IValidator<LenderDto>, LenderValidator>();
    serviceCollection.AddTransient<IValidator<UserDto>, UserValidator>();
}