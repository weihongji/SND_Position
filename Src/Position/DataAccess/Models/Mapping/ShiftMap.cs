using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class ShiftMap : EntityTypeConfiguration<Shift>
    {
        public ShiftMap()
        {
            // Primary Key
            this.HasKey(t => t.ShiftId);

            // Properties
            this.Property(t => t.ShiftName)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("Shift");
            this.Property(t => t.ShiftId).HasColumnName("ShiftId");
            this.Property(t => t.ShiftName).HasColumnName("ShiftName");
            this.Property(t => t.ShiftBeginTime).HasColumnName("ShiftBeginTime");
            this.Property(t => t.ShiftEndTime).HasColumnName("ShiftEndTime");
            this.Property(t => t.ShiftGroupId).HasColumnName("ShiftGroupId");
        }
    }
}
