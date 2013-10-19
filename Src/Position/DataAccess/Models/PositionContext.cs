using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataAccess.Models.Mapping;

namespace DataAccess.Models
{
    public partial class PositionContext : DbContext
    {
        static PositionContext()
        {
            Database.SetInitializer<PositionContext>(null);
        }

        public PositionContext()
            : base("Name=PositionContext")
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lamp> Lamps { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PeopleSender> PeopleSenders { get; set; }
        public DbSet<PeopleShift> PeopleShifts { get; set; }
        public DbSet<PeopleWorkPath> PeopleWorkPaths { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<ShiftGroup> ShiftGroups { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BranchMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new LampMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new PeopleSenderMap());
            modelBuilder.Configurations.Add(new PeopleShiftMap());
            modelBuilder.Configurations.Add(new PeopleWorkPathMap());
            modelBuilder.Configurations.Add(new PositionMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new RankMap());
            modelBuilder.Configurations.Add(new ReceiverMap());
            modelBuilder.Configurations.Add(new RegionMap());
            modelBuilder.Configurations.Add(new SenderMap());
            modelBuilder.Configurations.Add(new ShiftMap());
            modelBuilder.Configurations.Add(new ShiftGroupMap());
            modelBuilder.Configurations.Add(new WorkTypeMap());
        }
    }
}
