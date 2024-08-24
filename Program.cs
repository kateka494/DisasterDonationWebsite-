using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using POE1.Data;
using Microsoft.AspNetCore.Identity;
using POE1.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<POE1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("POE1Context") ?? throw new InvalidOperationException("Connection string 'POE1Context' not found.")));

//builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders()
//    .AddRoles<IdentityRole>()
//    .AddEntityFrameworkStores<POE1Context>();



builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
   .AddEntityFrameworkStores<POE1Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Service>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
