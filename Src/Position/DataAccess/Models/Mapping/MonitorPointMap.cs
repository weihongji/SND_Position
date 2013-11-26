using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class MonitorPointMap : EntityTypeConfiguration<MonitorPoint>
    {
        public MonitorPointMap() {
            // Ignores
            this.Ignore(t => t.X);
            this.Ignore(t => t.Y);

            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MonitorPoint");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Information).HasColumnName("Information").HasMaxLength(100);
            this.Property(t => t.OffsetX).HasColumnName("OffsetX");
            this.Property(t => t.OffsetY).HasColumnName("OffsetY");
            this.Property(t => t.MonitorContentId).HasColumnName("MonitorContentId");
            this.Property(t => t.AlarmUp).HasColumnName("AlarmUp");
            this.Property(t => t.AlarmDown).HasColumnName("AlarmDown");
            this.Property(t => t.RangeUp).HasColumnName("RangeUp");
            this.Property(t => t.RangeDown).HasColumnName("RangeDown");
            this.Property(t => t.Remark).HasColumnName("Remark").HasMaxLength(100);

            this.HasRequired(t => t.MonitorContent).WithMany();
        }
    }
}
