using Microsoft.EntityFrameworkCore;
using UmbAPI.Models;

namespace UmbAPI.Data
{
    public class HumanResourcesContext : DbContext
    {
        public HumanResourcesContext(DbContextOptions<HumanResourcesContext> options) : base(options)
        { 
        
        }
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        //public DbSet<EmployeeType> EmployeeTypes { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-RR8EHIV;Initial Catalog=UMBHR_DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
    }
}
