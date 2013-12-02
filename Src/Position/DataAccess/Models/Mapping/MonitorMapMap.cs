using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class MonitorMapMap : EntityTypeConfiguration<MonitorMap>
    {
        public MonitorMapMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("MonitorMap");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.MonitorSystemId).HasColumnName("MonitorSystemId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.StartX).HasColumnName("StartX");
            this.Property(t => t.StartY).HasColumnName("StartY");
            this.Property(t => t.Scale).HasColumnName("Scale");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
        }
    }
}
