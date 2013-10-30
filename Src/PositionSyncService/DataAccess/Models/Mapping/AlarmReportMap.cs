using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SharedEntity;

namespace DataAccess.Models.Mapping
{
    public class AlarmReportMap : EntityTypeConfiguration<AlarmReport>
    {
        public AlarmReportMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Alarm_id, t.Alarm_type, t.Alarm_param1, t.Alarm_param2, t.First_report_time, t.Last_report_time, t.Process_time, t.Login_name, t.Process_status });

            // Properties
            this.Property(t => t.Alarm_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Alarm_param1)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Alarm_param2)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Login_name)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("AlarmReport");
            this.Property(t => t.Alarm_id).HasColumnName("Alarm_id");
            this.Property(t => t.Alarm_type).HasColumnName("Alarm_type");
            this.Property(t => t.Alarm_param1).HasColumnName("Alarm_param1");
            this.Property(t => t.Alarm_param2).HasColumnName("Alarm_param2");
            this.Property(t => t.First_report_time).HasColumnName("First_report_time");
            this.Property(t => t.Last_report_time).HasColumnName("Last_report_time");
            this.Property(t => t.Process_time).HasColumnName("Process_time");
            this.Property(t => t.Login_name).HasColumnName("Login_name");
            this.Property(t => t.Process_status).HasColumnName("Process_status");
        }
    }
}
