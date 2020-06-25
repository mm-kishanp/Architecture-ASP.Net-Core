using Microsoft.EntityFrameworkCore;

namespace Architecture.DataBase.CodeFirst
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<User> Employees { get; set; }
        DbSet<Role> Role { get; set; }
        DbSet<UserRole> UserRole { get; set; }
        DbSet<Module> Module { get; set; }
        DbSet<Action> Action { get; set; }
        DbSet<ModuleAction> ModuleAction { get; set; }
        DbSet<RoleModuleAction> RoleModuleAction { get; set; }
        DbSet<UserRoleModuleAction> UserRoleModuleAction { get; set; }

    }
}
