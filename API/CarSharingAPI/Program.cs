
using BusinessLogic.Mapping;
using BusinessLogic.Models.Activity;
using BusinessLogic.Models.Borrower;
using BusinessLogic.Models.Car;
using BusinessLogic.Models.Lender;
using BusinessLogic.Models.User;
using BusinessLogic.Services;
using BusinessLogic.Services.Implemetation;
using BusinessLogic.Validators;
using CarSharingAPI.Mapping;
using DataAccess.DbContext;
using FluentValidation.AspNetCore;
using DataAccess.Repositories;
using DataAccess.Repositories.Implementation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddFluentValidation();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CarSharingContext>(options => options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database=CarSharingDB;Trusted_Connection=True;"));

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

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