using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WishList.Infrastructure.Data;
using WishList.Razor.App.Middleware;
using WishList.Services.Interfaces;
using WishList.Services.Services;

var builder = WebApplication.CreateBuilder(args);

//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var connectionString = builder.Configuration["ConnectionStrings:WishListDB"];
builder.Services.AddDbContext<WishListContext>(options => options.UseSqlite(connectionString, b => b.MigrationsAssembly("WishList.Infrastructure")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWishService, WishService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<AuthorizationMiddleware>();
app.UseMiddleware<GetUserMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
