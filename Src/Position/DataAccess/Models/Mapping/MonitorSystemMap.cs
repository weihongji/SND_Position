using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class MonitorSystemMap : EntityTypeConfiguration<MonitorSystem>
    {
        public MonitorSystemMap() {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MonitorSystem");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.CompanyMineId).HasColumnName("CompanyMineId");
            this.Property(t => t.Information).HasColumnName("Information");
            this.Property(t => t.Remark).HasColumnName("Remark");
        }
    }
}
