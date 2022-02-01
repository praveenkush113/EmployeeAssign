using Employee.Core.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Employee.Core.Mapping
{
    public class EmployeeMap : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.ToTable("Employees");
            builder.Property(p => p.Id).HasColumnName("Id");
            builder.HasKey(p => p.Id);
        }

        //public void MapEntity(EntityTypeBuilder<EmployeeEntity> builder)
        //{
        //    builder.ToTable("Employees");
        //    builder.Property(p => p.Id).HasColumnName("Id");
        //    builder.HasKey(p => p.Id);
        //}
    }
}