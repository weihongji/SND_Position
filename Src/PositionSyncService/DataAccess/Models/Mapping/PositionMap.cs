using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SharedEntity;

namespace DataAccess.Models.Mapping
{
    public class PositionMap : EntityTypeConfiguration<Position>
    {
        public PositionMap()
        {
            // Primary Key
            this.HasKey(t => t.Position_id);

            // Properties
            this.Property(t => t.Position_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Position_desc)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Position");
            this.Property(t => t.Position_id).HasColumnName("Position_id");
            this.Property(t => t.Position_x).HasColumnName("Position_x");
            this.Property(t => t.Position_y).HasColumnName("Position_y");
            this.Property(t => t.Position_z).HasColumnName("Position_z");
            this.Property(t => t.Position_sin).HasColumnName("Position_sin");
            this.Property(t => t.Position_cos).HasColumnName("Position_cos");
            this.Property(t => t.Position_vcos).HasColumnName("Position_vcos");
            this.Property(t => t.Position_desc).HasColumnName("Position_desc");

        }
    }
}
