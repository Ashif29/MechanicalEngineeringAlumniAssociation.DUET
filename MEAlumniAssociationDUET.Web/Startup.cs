using Autofac;
using Autofac.Extensions.DependencyInjection;
using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Repository.Contracts;
using MEAlumniAssociationDUET.Repository.Core;
using MEAlumniAssociationDUET.Repository.DataAccess;
using MEAlumniAssociationDUET.Repository.Implementations;
using MEAlumniAssociationDUET.Service.Contracts;
using MEAlumniAssociationDUET.Service.Implementations;
using MEAlumniAssociationDUET.Web.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MEAlumniAssociationDUET.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static ILifetimeScope AutofacContainer { get; private set; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext using SQL Server (or any other provider you're using)
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultSqlConnection")));            

            // Add Identity services with custom ApplicationUser and ApplicationRole
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();  // Provides tokens for password reset, email confirmation, etc.

            services.AddAuthentication("AuthCookie")
                       .AddCookie("AuthCookie", options =>
                       {                      
                           options.LoginPath = "/Account/Login";
                           options.LogoutPath = "/Account/Logout";
                           options.AccessDeniedPath = "/Account/AccessDenied";  // Redirect if access is denied             
                       });
            // Add authorization policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("Admin")); // Policy requiring Admin role

                options.AddPolicy("SuperAdmin", policy =>
                    policy.RequireRole("SuperAdmin")); // Policy requiring SuperAdmin role
            });
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews();
            services.AddScoped<IPhotoStorage, FileSystemPhotoStorage>();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IApplicationRoleService, ApplicationRoleService>();
            services.AddTransient<ICurrentUserService,CurrentUserService>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IApplicationRoleRepository, ApplicationRoleRepository>();
            services.AddTransient<IAuthUserService, AuthUserService>();
         
            // Add services for controllers with views
            services.AddControllersWithViews();

            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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

            // Enable authentication and authorization
            app.UseAuthentication();  // This enables cookie-based authentication
            app.UseAuthorization();   // This enables role-based authorization
            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute(
                 name: "default",
                 pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

            });

        }
    }
}
