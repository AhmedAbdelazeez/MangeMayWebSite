using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using usermange2.Models;

namespace usermange2.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("userRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("userclaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("userLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("Roleclaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("usertokens", "security");
        }
    }
}
