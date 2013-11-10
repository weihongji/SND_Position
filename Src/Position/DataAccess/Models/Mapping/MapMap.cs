using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class MapMap : EntityTypeConfiguration<Map>
    {
        public MapMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("Map");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.StartX).HasColumnName("StartX");
            this.Property(t => t.StartY).HasColumnName("StartY");
            this.Property(t => t.Scale).HasColumnName("Scale");
            this.Property(t => t.DTStamp).HasColumnName("DTStamp");
        }
    }
}
