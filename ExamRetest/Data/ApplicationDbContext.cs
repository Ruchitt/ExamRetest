using ExamRetest.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamRetest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
        }
        public DbSet<EmployeeDetail> employees { get; set; }
        public DbSet<RoleType> roles { get; set; }
		public DbSet<EmployeeeRoles> emproles { get; set; }

	}
}
