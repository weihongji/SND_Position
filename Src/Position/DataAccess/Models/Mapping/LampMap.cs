using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class LampMap : EntityTypeConfiguration<Lamp>
    {
        public LampMap()
        {
            // Primary Key
            this.HasKey(t => t.Lamp_id);

            // Properties
            this.Property(t => t.Lamp_id)
                .IsRequired()
                .HasMaxLength(8);

            this.Property(t => t.Lamp_info)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Lamp");
            this.Property(t => t.Lamp_id).HasColumnName("Lamp_id");
            this.Property(t => t.Sender_id).HasColumnName("Sender_id");
            this.Property(t => t.Lamp_info).HasColumnName("Lamp_info");
        }
    }
}
