using System.Data.Entity;

namespace AAdAuthDemo
{
    public class SampleContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}