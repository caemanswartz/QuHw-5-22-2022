using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using QuintrixHomeworkPlayerMVP.Data;
using QuintrixHomeworkPlayerMVP.Models;
using QuintrixHomeworkPlayerMVP.Authorization;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString(
    "DefaultConnection")
    ?? throw new InvalidOperationException(
        "Connection string 'DefaultConnection' not found.");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<Player,IdentityRole>(
    options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.AddRazorPages();

builder.Services.AddAuthorization(options=>
{
    options.FallbackPolicy=new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddControllers(config=>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddScoped<IAuthorizationHandler,BotIsOwnerAuthorizationHandler>();
builder.Services.AddScoped<IAuthorizationHandler,BotAdministratorsAuthorizationHandler>();


var app = builder.Build();

using (var scope=app.Services.CreateScope())
{
    var services=scope.ServiceProvider;
    var context=services.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
    var testUserPw=builder.Configuration.GetValue<string>("SeedUserPW");
    await SeedData.Initialize(services,testUserPw);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
