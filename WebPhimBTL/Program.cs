using Microsoft.EntityFrameworkCore;
using WebPhimBTL.Models;
using WebPhimBTL.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var ConnectString = builder.Configuration.GetConnectionString("DbphimContext");
builder.Services.AddDbContext<DbphimContext>(x => x.UseSqlServer(ConnectString));
builder.Services.AddScoped<ILoaiPhimRepository, LoaiPhimRepository>();
builder.Services.AddScoped<ITheLoaiRespository, TheLoaiRespository>();

builder.Services.AddSession();

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
app.UseSession();

app.Run();
