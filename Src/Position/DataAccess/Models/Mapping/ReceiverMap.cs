using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class ReceiverMap : EntityTypeConfiguration<Receiver>
    {
        public ReceiverMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Branch_id, t.Receiver_id });

            // Properties
            this.Property(t => t.Parameters)
                .IsFixedLength()
                .HasMaxLength(512);

            this.Property(t => t.Receiver_info)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Receiver");
            this.Property(t => t.Branch_id).HasColumnName("Branch_id");
            this.Property(t => t.Receiver_id).HasColumnName("Receiver_id");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Product_id).HasColumnName("Product_id");
            this.Property(t => t.Position_id).HasColumnName("Position_id");
            this.Property(t => t.Parameters).HasColumnName("Parameters");
            this.Property(t => t.Receiver_info).HasColumnName("Receiver_info");
        }
    }
}
