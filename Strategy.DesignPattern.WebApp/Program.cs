using BaseProject.Identity.Contexts;
using BaseProject.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Strategy.DesignPattern.WebApp.Entities;
using Strategy.DesignPattern.WebApp.Enums;
using Strategy.DesignPattern.WebApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProductRepository>(sp =>
{
    var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();

    var claim = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault();

    var context = sp.GetRequiredService<AppIdentityDbContext>();

    if (claim == null) return new ProductRepositoryFromSqlServer(context);

    var databaseType = (EDatabaseType)int.Parse(claim.Value);

    return databaseType switch
    {
        EDatabaseType.MSSqlServer => new ProductRepositoryFromSqlServer(context),
        EDatabaseType.MongoDb => new ProductRepositoryFromMongoDb(builder.Configuration),
        _ => throw new NotImplementedException(),
    };
});

builder.Services.AddDbContext<AppIdentityDbContext>(ops =>
{
    ops.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(ops =>
{
    ops.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//using var scope = app.Services.CreateScope();
//var idenetityDbContext = scope.ServiceProvider.GetRequiredService<AppIdentityDbContext>();
//var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
//idenetityDbContext.Database.Migrate();

//if (userManager.Users == null)
//{
//    userManager.CreateAsync(new AppUser() { UserName = "appuser", Email = "user@gmail.com" }, "Password1.").Wait();
//}

app.Run();
