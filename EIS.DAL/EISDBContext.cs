using System.Data.Entity;
using EIS.BOL;

namespace EIS.DAL
{
    public class EISDBContext : DbContext
    {
        public EISDBContext() : base("EISDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EISDBContext, EIS.DAL.Migrations.Configuration>());
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
