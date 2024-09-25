using WishList.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using WishList.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration["ConnectionStrings:WishListDB"];
builder.Services.AddDbContext<WishListContext>(options => options.UseSqlite(connectionString, b => b.MigrationsAssembly("WishList.Infrastructure")));

//builder.Services.AddTransient<UserRepository>();
builder.Services.AddScoped<UserRepository>();
//builder.Services.AddSingleton<UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<WishRepository>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.Map("/map1", MyMethod);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();

static void MyMethod(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Map Test 1");
    });
}