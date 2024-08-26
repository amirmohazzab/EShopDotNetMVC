using Eshop.Data.Context;
using Eshop.Ioc.Container;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Config Services

    builder.Services.RegisterServices();
#endregion

#region Config BbContext

string connectionString = builder.Configuration.GetConnectionString("EshopConnectionString");
builder.Services.AddDbContext<EshopDbContext>(options => 
{ 
    options.UseSqlServer(connectionString); 
});

#endregion

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
