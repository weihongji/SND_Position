using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class WorkTypeMap : EntityTypeConfiguration<WorkType>
    {
        public WorkTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.Worktype_id);

            // Properties
            this.Property(t => t.Worktype_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Worktype_name)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("WorkType");
            this.Property(t => t.Worktype_id).HasColumnName("Worktype_id");
            this.Property(t => t.Worktype_type).HasColumnName("Worktype_type");
            this.Property(t => t.Worktype_name).HasColumnName("Worktype_name");
        }
    }
}
