using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ProjForProj.Api.Common;
using ProjForProj.Api.Domain.Entity;

namespace ProjForProj.Api.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid,
                                                IdentityUserClaim<Guid>,
                                                IdentityUserRole<Guid>,
                                                IdentityUserLogin<Guid>,
                                                IdentityRoleClaim<Guid>,
                                                IdentityUserToken<Guid>>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
             //Database.EnsureCreated();
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<DesignObject> DesignObjects { get; set; }
        public DbSet<Mark> Marks { get; set; }
        public DbSet<DocumentationSet> DocumentationSets { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.DesignObjects)
                .WithOne(o => o.Project)
                .HasForeignKey(o => o.ProjectId);

            modelBuilder.Entity<Project>().HasQueryFilter(p => !p.IsDeleted);

            modelBuilder.Entity<DesignObject>()
                .HasMany(o => o.ChildObjects)
                .WithOne(c => c.ParentObject)
                .HasForeignKey(c => c.ParentObjectId);

            modelBuilder.Entity<DesignObject>()
                .HasMany(o => o.DocumentationSets)
                .WithOne(d => d.DesignObject)
                .HasForeignKey(d => d.DesignObjectId);

            modelBuilder.Entity<Mark>()
                .HasMany(m => m.DocumentationSets)
                .WithOne(d => d.Mark)
                .HasForeignKey(d => d.MarkId);
        }
    }
}
