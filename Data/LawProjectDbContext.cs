
using LawProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LawProject.Data
{
    public class LawProjectDbContext:IdentityDbContext<AppUser>
    {
        public LawProjectDbContext(DbContextOptions<LawProjectDbContext> options):base(options)
        {
            
        }
        
        //Models
        public DbSet<Home> Home { get; set; }
        public DbSet<About> About { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<CaseStudies> CaseStudies { get; set; }
        public DbSet<Practices> Practices { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<EmployeeOfClient> EmployeeOfClients { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Profession> Profession { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }





    }
}
