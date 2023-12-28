using Demo.DAL.Data.Configurations;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationModel>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("Server = . ; Database = MVCApllication; Trusted_Connection = True");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call Configuration Classes
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigurations());
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Add all Configuration Class


        }

        public object AsNoTracking()
        {
            throw new NotImplementedException();
        }

        public DbSet<Department> Departments { get; set; }   
        public DbSet<Employee> Employees { get; set; }

        //public DbSet<IdentityUser>  Users { get; set; }
        //public DbSet<IdentityRole>  Roles { get; set; }

    }
}
