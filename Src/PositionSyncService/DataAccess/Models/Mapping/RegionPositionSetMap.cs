using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SharedEntity;

namespace DataAccess.Models.Mapping
{
    public class RegionPositionSetMap : EntityTypeConfiguration<RegionPositionSet>
    {
        public RegionPositionSetMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Region_id, t.Position_id });

            // Table & Column Mappings
            this.ToTable("RegionPositionSet");
            this.Property(t => t.Region_id).HasColumnName("Region_id");
            this.Property(t => t.Position_id).HasColumnName("Position_id");
        }
    }
}
