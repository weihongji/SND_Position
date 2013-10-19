using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            this.HasKey(t => t.Product_id);

            // Properties
            this.Property(t => t.Product_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Parameters)
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.Product_desc)
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("Product");
            this.Property(t => t.Product_id).HasColumnName("Product_id");
            this.Property(t => t.Parameters).HasColumnName("Parameters");
            this.Property(t => t.Product_desc).HasColumnName("Product_desc");
        }
    }
}
