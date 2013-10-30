using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SharedEntity;

namespace DataAccess.Models.Mapping
{
    public class RankMap : EntityTypeConfiguration<Rank>
    {
        public RankMap()
        {
            // Primary Key
            this.HasKey(t => t.Rank_id);

            // Properties
            this.Property(t => t.Rank_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Rank_name)
                .IsRequired()
                .HasMaxLength(32);

            // Table & Column Mappings
            this.ToTable("Rank");
            this.Property(t => t.Rank_id).HasColumnName("Rank_id");
            this.Property(t => t.Rank_type).HasColumnName("Rank_type");
            this.Property(t => t.Rank_name).HasColumnName("Rank_name");
        }
    }
}
