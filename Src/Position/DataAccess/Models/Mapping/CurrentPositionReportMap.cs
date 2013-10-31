using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class CurrentPositionReportMap : EntityTypeConfiguration<CurrentPositionReport>
    {
        public CurrentPositionReportMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Sender_id, t.Branch_id, t.Receiver_id, t.Position_id, t.Distance, t.Report_time });

            // Properties
            this.Property(t => t.Sender_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Position_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Distance)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("CurrentPositionReport");
            this.Property(t => t.Sender_id).HasColumnName("Sender_id");
            this.Property(t => t.Branch_id).HasColumnName("Branch_id");
            this.Property(t => t.Receiver_id).HasColumnName("Receiver_id");
            this.Property(t => t.Position_id).HasColumnName("Position_id");
            this.Property(t => t.Distance).HasColumnName("Distance");
            this.Property(t => t.Report_time).HasColumnName("Report_time");
        }
    }
}
