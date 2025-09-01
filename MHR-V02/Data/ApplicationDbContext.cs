using MHR_V02.Models.Base;
using MHR_V02.Models.BasicTables;
using Microsoft.EntityFrameworkCore;

namespace MHR_V02.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
        public DbSet<RoleAction> RoleActions { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // اعمال قانون ON DELETE RESTRICT به همه کلیدهای خارجی
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // ایجاد ایندکس روی ControllerName و ActionName و HttpMethod 
            modelBuilder.Entity<ActionLog>()
                .HasIndex(al => new { al.ControllerName, al.ActionName, al.HttpMethod })
                .IsUnique();     

            modelBuilder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoleAction>()
                .HasKey(ra => new { ra.RoleId, ra.ActionLogId });
           

            modelBuilder.Entity<RoleAction>()
                .HasOne(ra => ra.Role)
                .WithMany(r => r.RoleActions)
                .HasForeignKey(ra => ra.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoleAction>()
                .HasOne(ra => ra.ActionLog)
                .WithMany(al => al.RoleActions)
                .HasForeignKey(ra => ra.ActionLogId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAction>()
                .HasKey(ua => new { ua.UserId, ua.ActionLogId });
           
            modelBuilder.Entity<UserAction>()
                .HasOne(ua => ua.User)
                .WithMany(u => u.UserActions)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserAction>()
                .HasOne(ua => ua.ActionLog)
                .WithMany(al => al.UserActions)
                .HasForeignKey(ua => ua.ActionLogId)
                .OnDelete(DeleteBehavior.Cascade);

            // سایر تنظیمات مدل


        }
    }
}
