using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class BranchMap : EntityTypeConfiguration<Branch>
    {
        public BranchMap()
        {
            // Primary Key
            this.HasKey(t => t.Branch_id);

            // Properties
            this.Property(t => t.Parameters)
                .IsFixedLength()
                .HasMaxLength(512);

            this.Property(t => t.Branch_info)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Branch");
            this.Property(t => t.Branch_id).HasColumnName("Branch_id");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Product_id).HasColumnName("Product_id");
            this.Property(t => t.Position_id).HasColumnName("Position_id");
            this.Property(t => t.Comm_mode).HasColumnName("Comm_mode");
            this.Property(t => t.Can_no).HasColumnName("Can_no");
            this.Property(t => t.Uart_port).HasColumnName("Uart_port");
            this.Property(t => t.Ip).HasColumnName("Ip");
            this.Property(t => t.Ip_port).HasColumnName("Ip_port");
            this.Property(t => t.Parameters).HasColumnName("Parameters");
            this.Property(t => t.Branch_info).HasColumnName("Branch_info");

            // Relationships
            this.HasRequired(t => t.Position)
                .WithMany(t => t.Branches)
                .HasForeignKey(d => d.Position_id);
            this.HasRequired(t => t.Product)
                .WithMany(t => t.Branches)
                .HasForeignKey(d => d.Product_id);

        }
    }
}
