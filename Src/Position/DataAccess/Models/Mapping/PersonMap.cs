using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace DataAccess.Models.Mapping
{
    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            // Primary Key
            this.HasKey(t => t.People_id);

            // Properties
            this.Property(t => t.People_id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.People_name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Phone)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.ID_Number)
                .IsRequired()
                .HasMaxLength(18);

            this.Property(t => t.Allergy)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Linkman_name)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.Linkman_dept)
                .IsRequired()
                .HasMaxLength(64);

            this.Property(t => t.Linkman_phone)
                .IsRequired()
                .HasMaxLength(32);

            this.Property(t => t.People_info)
                .IsRequired()
                .HasMaxLength(64);

            // Table & Column Mappings
            this.ToTable("People");
            this.Property(t => t.People_id).HasColumnName("People_id");
            this.Property(t => t.People_name).HasColumnName("People_name");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Picture).HasColumnName("Picture");
            this.Property(t => t.Dept_id).HasColumnName("Dept_id");
            this.Property(t => t.Worktype_id).HasColumnName("Worktype_id");
            this.Property(t => t.Rank_id).HasColumnName("Rank_id");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Enroll_time).HasColumnName("Enroll_time");
            this.Property(t => t.Address).HasColumnName("Address");
            this.Property(t => t.Phone).HasColumnName("Phone");
            this.Property(t => t.ID_Number).HasColumnName("ID_Number");
            this.Property(t => t.Blood_type).HasColumnName("Blood_type");
            this.Property(t => t.Allergy).HasColumnName("Allergy");
            this.Property(t => t.Linkman_name).HasColumnName("Linkman_name");
            this.Property(t => t.Linkman_dept).HasColumnName("Linkman_dept");
            this.Property(t => t.Linkman_phone).HasColumnName("Linkman_phone");
            this.Property(t => t.People_info).HasColumnName("People_info");
        }
    }
}
