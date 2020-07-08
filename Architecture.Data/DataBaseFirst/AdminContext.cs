using Architecture.DataBase.DataBaseFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace Architecture.DataBase.DataBaseFirst
{
    public partial class AdminContext : DbContext
    {
        public AdminContext()
        {
        }

        public AdminContext(DbContextOptions<AdminContext> options)
            : base(options)
        {
        }

        public virtual DbSet<LogEntry> LogEntry { get; set; }

        public virtual DbSet<Models.Action> Action { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleAction> ModuleAction { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleModuleAction> RoleModuleAction { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<UserRoleModuleAction> UserRoleModuleAction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Models.Action>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");
            });

            modelBuilder.Entity<Module>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");
            });

            modelBuilder.Entity<ModuleAction>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.ModuleAction)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleAction_ModuleAction_Action");

                entity.HasOne(d => d.Module)
                    .WithMany(p => p.ModuleAction)
                    .HasForeignKey(d => d.ModuleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ModuleAction_ModuleAction_Module");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");
            });

            modelBuilder.Entity<RoleModuleAction>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");

                entity.HasOne(d => d.ModuleAction)
                    .WithMany(p => p.RoleModuleAction)
                    .HasForeignKey(d => d.ModuleActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleModuleAction_RoleModuleAction_ModuleAction");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_UserRole_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRole_UserRole_User");
            });

            modelBuilder.Entity<UserRoleModuleAction>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");

                entity.HasOne(d => d.RoleModuleActionNavigation)
                    .WithMany(p => p.UserRoleModuleAction)
                    .HasForeignKey(d => d.RoleModuleAction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleModuleAction_UserRoleModuleAction_RoleModuleAction");

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.UserRoleModuleAction)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleModuleAction_UserRoleModuleAction_UserRole");
            });
            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.Property(e => e.CreatedUtcdate).HasColumnName("CreatedUTCDate");

                entity.Property(e => e.UpdatedUtcdate).HasColumnName("UpdatedUTCDate");
            });
        }
    }
}