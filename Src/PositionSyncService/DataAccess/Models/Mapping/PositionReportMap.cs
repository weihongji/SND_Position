using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SharedEntity;

namespace DataAccess.Models.Mapping
{
    public class PositionReportMap : EntityTypeConfiguration<PositionReport>
    {
        public PositionReportMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Sender_id, t.Position_id, t.Report_time });

            // Properties
            this.Property(t => t.Sender_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Position_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Distance)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PositionReport");
            this.Property(t => t.Sender_id).HasColumnName("Sender_id");
            this.Property(t => t.Branch_id).HasColumnName("Branch_id");
            this.Property(t => t.Receiver_id).HasColumnName("Receiver_id");
            this.Property(t => t.Position_id).HasColumnName("Position_id");
            this.Property(t => t.Distance).HasColumnName("Distance");
            this.Property(t => t.Report_time).HasColumnName("Report_time");
        }
    }
}
