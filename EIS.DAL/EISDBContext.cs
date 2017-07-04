using System.Data.Entity;
using EIS.BOL;

namespace EIS.DAL
{
    public class EISDBContext : DbContext
    {
        public EISDBContext() : base("EISDB")
        {
            
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Emloyee> Employees { get; set; }
    }
}
