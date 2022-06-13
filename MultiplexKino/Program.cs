using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiplexKino.Areas.Identity.Data;
using MultiplexKino.Core;
using MultiplexKino.Core.Repositories;
using MultiplexKino.Repositories;
using MultiplexKino.Services;
using MultiplexKino.Settings;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MultiplexKinoDbContextConnection");

builder.Services.AddDbContext<MultiplexKinoDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<MultiplexKinoUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MultiplexKinoDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

AddAuthorizationPolicies();

AddScoped();

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Projekcija}/{action=UcitajSveProjekcije}/{id?}");

app.MapRazorPages();

app.Run();

void AddAuthorizationPolicies()
{
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("EmployeeNumber"));
    });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Constants.Policies.RequireAdmin, policy => policy.RequireRole(Constants.Roles.Administrator));
        options.AddPolicy(Constants.Policies.RequireManager, policy => policy.RequireRole(Constants.Roles.Manager));
    });
}




void AddScoped()
{
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IRoleRepository, RoleRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<IEmailSender, EmailSender>();
    builder.Services.AddScoped<IEmailLogSender, EmailLogSender>();
}