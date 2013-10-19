using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class PeopleWorkPathMap : EntityTypeConfiguration<PeopleWorkPath>
    {
        public PeopleWorkPathMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Path_id, t.Step_id });

            // Properties
            this.Property(t => t.Path_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Step_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("PeopleWorkPath");
            this.Property(t => t.Path_id).HasColumnName("Path_id");
            this.Property(t => t.Step_id).HasColumnName("Step_id");
            this.Property(t => t.People_id).HasColumnName("People_id");
            this.Property(t => t.Position_id).HasColumnName("Position_id");
            this.Property(t => t.Begin_time).HasColumnName("Begin_time");
            this.Property(t => t.End_time).HasColumnName("End_time");
            this.Property(t => t.Check_status).HasColumnName("Check_status");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.PeopleWorkPaths)
                .HasForeignKey(d => d.People_id);
            this.HasRequired(t => t.Position)
                .WithMany(t => t.PeopleWorkPaths)
                .HasForeignKey(d => d.Position_id);

        }
    }
}
