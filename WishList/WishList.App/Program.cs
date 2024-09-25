using WishList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WishList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WishListContext>(options => options.UseSqlite("Data Source=WishList.db", b => b.MigrationsAssembly("WishList.Infrastructure")));

builder.Services.AddScoped<UserRepository>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
