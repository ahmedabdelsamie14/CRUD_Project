using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configurations
{
    internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void  Configure(EntityTypeBuilder<Department> builder)
        {
            //Fluent APIs

            builder.Property(D => D.Id).UseIdentityColumn(10, 10);
            builder.HasMany(d => d.Employees)
                .WithOne(e=>e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
