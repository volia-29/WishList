using FluentValidation;
using FluentValidation.AspNetCore;
using WishList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WishList.Services.Services;
using WishList.Services.Interfaces;
using static WishList.App.Validators.CreateUserDtoValidator;
using WishList.App.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateToDoItemDtoValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration["ConnectionStrings:WishListDB"];
builder.Services.AddDbContext<WishListContext>(options => options.UseSqlite(connectionString, b => b.MigrationsAssembly("WishList.Infrastructure")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWishService, WishService>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();