using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class MonitorTypeMap : EntityTypeConfiguration<MonitorType>
    {
        public MonitorTypeMap() {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MonitorType");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.OffsetX).HasColumnName("OffsetX");
            this.Property(t => t.OffsetY).HasColumnName("OffsetY");
        }
    }
}
