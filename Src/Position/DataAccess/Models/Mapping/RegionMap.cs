using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class RegionMap : EntityTypeConfiguration<Region>
    {
        public RegionMap()
        {
            // Primary Key
            this.HasKey(t => t.Region_id);

            // Properties
            this.Property(t => t.Region_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Region_name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Region_info)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Region");
            this.Property(t => t.Region_id).HasColumnName("Region_id");
            this.Property(t => t.Region_name).HasColumnName("Region_name");
            this.Property(t => t.Region_type).HasColumnName("Region_type");
            this.Property(t => t.People_max).HasColumnName("People_max");
            this.Property(t => t.Linger_max).HasColumnName("Linger_max");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Display_x).HasColumnName("Display_x");
            this.Property(t => t.Display_y).HasColumnName("Display_y");
            this.Property(t => t.Display_type).HasColumnName("Display_type");
            this.Property(t => t.Region_info).HasColumnName("Region_info");
        }
    }
}
