using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class ShiftGroupMap : EntityTypeConfiguration<ShiftGroup>
    {
        public ShiftGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ShiftGroupId);

            // Properties
            this.Property(t => t.ShiftGroupName)
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("ShiftGroup");
            this.Property(t => t.ShiftGroupId).HasColumnName("ShiftGroupId");
            this.Property(t => t.ShiftGroupName).HasColumnName("ShiftGroupName");
        }
    }
}
