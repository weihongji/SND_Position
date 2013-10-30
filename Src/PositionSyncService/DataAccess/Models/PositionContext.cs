using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DataAccess.Models.Mapping;
using SharedEntity;

namespace DataAccess.Models
{
    public partial class PositionContext : DbContext
    {
        private const string cnnExternal = "RYDW";
        private const string cnnInternal = "SND";

        static PositionContext()
        {
            Database.SetInitializer<PositionContext>(null);
        }

        public PositionContext(DBSource source)
            : base("Name=" + (source == DBSource.External ? cnnExternal : cnnInternal))
        {
        }

        public DbSet<AlarmReport> AlarmReports { get; set; }
        public DbSet<AlarmType> AlarmTypes { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<CurrentAlarmReport> CurrentAlarmReports { get; set; }
        public DbSet<CurrentPositionReport> CurrentPositionReports { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Lamp> Lamps { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<PeopleSender> PeopleSenders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionReport> PositionReports { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RegionPositionSet> RegionPositionSets { get; set; }
        public DbSet<Sender> Senders { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlarmReportMap());
            modelBuilder.Configurations.Add(new AlarmTypeMap());
            modelBuilder.Configurations.Add(new BranchMap());
            modelBuilder.Configurations.Add(new CurrentAlarmReportMap());
            modelBuilder.Configurations.Add(new CurrentPositionReportMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new LampMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new PeopleSenderMap());
            modelBuilder.Configurations.Add(new PositionMap());
            modelBuilder.Configurations.Add(new PositionReportMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new RankMap());
            modelBuilder.Configurations.Add(new ReceiverMap());
            modelBuilder.Configurations.Add(new RegionMap());
            modelBuilder.Configurations.Add(new RegionPositionSetMap());
            modelBuilder.Configurations.Add(new SenderMap());
            modelBuilder.Configurations.Add(new WorkTypeMap());
        }
    }
}
