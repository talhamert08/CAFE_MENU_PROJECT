

using AutoMapper;
using Business.AutoMapperProfile;
using Business.DependencyResolvers;
using Core.DependencyResolvers;
using Core.Utilities.IoC;
using DataAccess.Contexts;
using DataAccess.DependencyResolvers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Core.Extensions;
using WebUI.Api_Manager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<Api_Service_Manager>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new BusinessModule(),
    new DataAccessModule(),
    new CoreModule()
});

builder.Services.AddDbContext<EfContext>();


#region AutoMapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new BusinessProfile());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion 

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new PathString("/User/Login/"); //401 - Unauthorized
        options.AccessDeniedPath = new PathString("/User/Login/"); //403 - Forbidden
        options.ExpireTimeSpan = TimeSpan.FromHours(15);
        options.SlidingExpiration = true;
        options.Cookie.MaxAge = TimeSpan.FromDays(1);
    });
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(option =>
{
    //Süre 1 dk olarak belirlendi
    option.IdleTimeout = TimeSpan.FromMinutes(300);
});

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

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always
});


app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var context = new EfContext())
{
    context.Database.Migrate();
}

app.Run();
