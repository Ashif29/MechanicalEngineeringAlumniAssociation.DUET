using Autofac.Extensions.DependencyInjection;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Repository.Contracts;
using MEAlumniAssociationDUET.Repository.DataAccess;
using MEAlumniAssociationDUET.Repository.Implementations;
using MEAlumniAssociationDUET.Service.Contracts;
using MEAlumniAssociationDUET.Service.Implementations;
using MEAlumniAssociationDUET.Web;
using MEAlumniAssociationDUET.Web.Core;
using MEAlumniAssociationDUET.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MEAlumniAssociationDUET.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
//    // Add services to the container

//    // Add DbContext using SQL Server (or any other provider you're using)
//    builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection")));

////builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

//// Add Identity services with custom ApplicationUser and ApplicationRole
//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();  // Provides tokens for password reset, email confirmation, etc.

//builder.Services.AddAuthentication("AuthCookie")
//           .AddCookie("AuthCookie",options =>
//           {
//               options.LoginPath = "/Account/Login";
//               options.LogoutPath = "/Account/Logout";                     
//               options.AccessDeniedPath = "/Account/AccessDenied";  // Redirect if access is denied             
//           });

//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
//builder.Services.Configure<PhotoSettings>(builder.Configuration.GetSection("PhotoSettings"));

//builder.Services.AddTransient<IApplicationUserService, ApplicationUserService>();
//builder.Services.AddTransient<IApplicationRoleService, ApplicationRoleService>();

//builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
//builder.Services.AddScoped<IAuthUserService,AuthUserService>();

//// Add services for controllers with views
//builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

//var app = builder.Build();

//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//// Enable authentication and authorization
//app.UseAuthentication();  // This enables cookie-based authentication
//app.UseAuthorization();   // This enables role-based authorization

//// Configure default routing
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();
