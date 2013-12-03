using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class MonitorContentMap : EntityTypeConfiguration<MonitorContent>
    {
        public MonitorContentMap() {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("MonitorContent");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.MonitorSystemId).HasColumnName("MonitorSystemId");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.ImageOverview).HasColumnName("ImageOverview");
            this.Property(t => t.PointerX).HasColumnName("PointerX");
            this.Property(t => t.PointerY).HasColumnName("PointerY");
        }
    }
}
