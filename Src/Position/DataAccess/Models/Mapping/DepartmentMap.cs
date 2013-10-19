using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class DepartmentMap : EntityTypeConfiguration<Department>
    {
        public DepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.Dept_id);

            // Properties
            this.Property(t => t.Dept_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Dept_name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Dept_fullname)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Dept_info)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Department");
            this.Property(t => t.Dept_id).HasColumnName("Dept_id");
            this.Property(t => t.Dept_name).HasColumnName("Dept_name");
            this.Property(t => t.Dept_fullname).HasColumnName("Dept_fullname");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.Dept_info).HasColumnName("Dept_info");
        }
    }
}
