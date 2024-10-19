using MEAlumniAssociationDUET.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using MEAlumniAssociationDUET.Core.Entities;

namespace MEAlumniAssociationDUET.Repository.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
    IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        // Define DbSets for permissions and alumni
        public DbSet<ApplicationPermission> ApplicationPermissions { get; set; }
        public DbSet<ApplicationRolePermission> ApplicationRolePermissions { get; set; }
        public DbSet<AlumniUser> AlumniUsers { get; set; }  // Add AlumniUser DbSet

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            // Define relationships for ApplicationUserRole
            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            // Configure the ApplicationPermission
            builder.Entity<ApplicationPermission>()
                .HasKey(x => x.Id);

            // Define the relationship between AlumniUser and ApplicationUser
            builder.Entity<AlumniUser>(alumni =>
            {
                alumni.HasKey(a => a.Id);

                alumni.HasOne(a => a.ApplicationUser)
                    .WithOne()
                    .HasForeignKey<AlumniUser>(a => a.ApplicationUserId)
                    .IsRequired();

                // Optional: Additional configuration for AlumniUser table, like indexes, constraints, etc.
            });
        }
    }

}
