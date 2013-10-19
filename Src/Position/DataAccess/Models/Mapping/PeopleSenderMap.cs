using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class PeopleSenderMap : EntityTypeConfiguration<PeopleSender>
    {
        public PeopleSenderMap()
        {
            // Primary Key
            this.HasKey(t => new { t.People_id, t.Sender_id, t.First_use_time, t.Last_use_time });

            // Properties
            this.Property(t => t.People_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Sender_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PeopleSender");
            this.Property(t => t.People_id).HasColumnName("People_id");
            this.Property(t => t.Sender_id).HasColumnName("Sender_id");
            this.Property(t => t.First_use_time).HasColumnName("First_use_time");
            this.Property(t => t.Last_use_time).HasColumnName("Last_use_time");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.PeopleSenders)
                .HasForeignKey(d => d.People_id);
            this.HasRequired(t => t.Sender)
                .WithMany(t => t.PeopleSenders)
                .HasForeignKey(d => d.Sender_id);

        }
    }
}
