using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class PeopleShiftMap : EntityTypeConfiguration<PeopleShift>
    {
        public PeopleShiftMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PeopleId, t.FirstShiftTime });

            // Properties
            this.Property(t => t.PeopleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PeopleShift");
            this.Property(t => t.PeopleId).HasColumnName("PeopleId");
            this.Property(t => t.ShiftId).HasColumnName("ShiftId");
            this.Property(t => t.FirstShiftTime).HasColumnName("FirstShiftTime");
            this.Property(t => t.LastShiftTime).HasColumnName("LastShiftTime");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.PeopleShifts)
                .HasForeignKey(d => d.PeopleId);
            this.HasRequired(t => t.Shift)
                .WithMany(t => t.PeopleShifts)
                .HasForeignKey(d => d.ShiftId);

        }
    }
}
