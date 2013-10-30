using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SharedEntity;

namespace DataAccess.Models.Mapping
{
    public class SenderMap : EntityTypeConfiguration<Sender>
    {
        public SenderMap()
        {
            // Primary Key
            this.HasKey(t => t.Sender_id);

            // Properties
            this.Property(t => t.Sender_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Parameters)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.Sender_info)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Sender");
            this.Property(t => t.Sender_id).HasColumnName("Sender_id");
            this.Property(t => t.Sender_type).HasColumnName("Sender_type");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Product_id).HasColumnName("Product_id");
            this.Property(t => t.Parameters).HasColumnName("Parameters");
            this.Property(t => t.First_use_time).HasColumnName("First_use_time");
            this.Property(t => t.Last_use_time).HasColumnName("Last_use_time");
            this.Property(t => t.Sender_info).HasColumnName("Sender_info");
        }
    }
}
