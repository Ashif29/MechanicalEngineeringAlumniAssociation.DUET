using MEAlumniAssociationDUET.Core;
using MEAlumniAssociationDUET.Repository.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Add DbContext using SQL Server (or any other provider you're using)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSqlConnection")));

// Add Identity services with custom ApplicationUser and ApplicationRole
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();  // Provides tokens for password reset, email confirmation, etc.

// Configure cookie-based authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;  // Make the cookie accessible only via HTTP
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);  // Cookie expiration time
    options.LoginPath = "/Account/Login";  // Redirect to login page if not authenticated
    options.AccessDeniedPath = "/Account/AccessDenied";  // Redirect if access is denied
    options.SlidingExpiration = true;  // Refresh cookie expiration with each request
});

// Add services for controllers with views
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

var app = builder.Build();

// Seed roles and SuperAdmin during startup
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Initialize roles and create default SuperAdmin
    await RoleInitializer.InitializeAsync(roleManager, userManager);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable authentication and authorization
app.UseAuthentication();  // This enables cookie-based authentication
app.UseAuthorization();   // This enables role-based authorization

// Configure default routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
