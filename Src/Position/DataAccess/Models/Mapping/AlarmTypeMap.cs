using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class AlarmTypeMap : EntityTypeConfiguration<AlarmType>
    {
        public AlarmTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Alarm_type);

            // Properties
            this.Property(t => t.Alarm_name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Param1_name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Param2_name)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("AlarmType");
            this.Property(t => t.Alarm_type).HasColumnName("Alarm_type");
            this.Property(t => t.Alarm_name).HasColumnName("Alarm_name");
            this.Property(t => t.Param1_name).HasColumnName("Param1_name");
            this.Property(t => t.Param2_name).HasColumnName("Param2_name");
            this.Property(t => t.Alarm_level).HasColumnName("Alarm_level");
            this.Property(t => t.Alarm_attrib).HasColumnName("Alarm_attrib");
            this.Property(t => t.Valid_seconds).HasColumnName("Valid_seconds");
            this.Property(t => t.Auto_recovery_seconds).HasColumnName("Auto_recovery_seconds");
        }
    }
}
