using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;
using SharedEntity;

namespace DataAccess
{
    public class Dao
    {
        public int SyncAlarmReports(List<AlarmReport> newEntries) {
            var context = new PositionContext(DBSource.Internal);
            foreach (var item in newEntries) {
                context.AlarmReports.Add(item);
            }
            if (newEntries.Any()) {
                return context.SaveChanges();
            }
            return 0;
        }

        public int SyncAlarmTypes(List<AlarmType> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<AlarmType>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Alarm_type).ToList();
            var removedEntries = context.AlarmTypes.Where(x => !IDs.Contains(x.Alarm_type)).ToList();
            foreach (var item in removedEntries) {
                context.AlarmTypes.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncBranches(List<Branch> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Branch>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Branch_id).ToList();
            var removedEntries = context.Branches.Where(x => !IDs.Contains(x.Branch_id)).ToList();
            foreach (var item in removedEntries) {
                context.Branches.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncCurrentPositionReports(List<CurrentPositionReport> potentialEntries) {
            if (!potentialEntries.Any()) {
                return 0;
            }
            var context = new PositionContext(DBSource.Internal);
            var minTime = potentialEntries.Min(x => x.Report_time);
            var existingEntries = context.CurrentPositionReports.Where(x => x.Report_time >= minTime).ToList();
            foreach (var item in potentialEntries) {
                if (!existingEntries.Any(x => x.Sender_id == item.Sender_id && x.Position_id == item.Position_id && x.Report_time == item.Report_time)) {
                    context.CurrentPositionReports.Add(item);
                }
            }
            return context.SaveChanges();
        }

        public int SyncDepartments(List<Department> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Department>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Dept_id).ToList();
            var removedEntries = context.Departments.Where(x => !IDs.Contains(x.Dept_id)).ToList();
            foreach (var item in removedEntries) {
                context.Departments.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncLamps(List<Lamp> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Lamp>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Lamp_id).ToList();
            var removedEntries = context.Lamps.Where(x => !IDs.Contains(x.Lamp_id)).ToList();
            foreach (var item in removedEntries) {
                context.Lamps.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncPeople(List<Person> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Person>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.People_id).ToList();
            var removedEntries = context.People.Where(x => !IDs.Contains(x.People_id)).ToList();
            foreach (var item in removedEntries) {
                context.People.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncPeopleSenders(List<PeopleSender> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<PeopleSender>(item);
            }
            //Sync deleted records
            //var removedEntries = context.PeopleSenders
            //    .Where(x => !externalEntries.Any(y => y.People_id == x.People_id && y.Sender_id == x.Sender_id && y.First_use_time == x.First_use_time)).ToList();
            //foreach (var item in removedEntries) {
            //    context.PeopleSenders.Remove(item);
            //}

            return context.SaveChanges();
        }

        public int SyncPositions(List<Position> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Position>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Position_id).ToList();
            var removedEntries = context.Positions.Where(x => !IDs.Contains(x.Position_id)).ToList();
            foreach (var item in removedEntries) {
                context.Positions.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncPositionReports(List<PositionReport> potentialEntries) {
            if (!potentialEntries.Any()) {
                return 0;
            }
            var context = new PositionContext(DBSource.Internal);
            var minTime = potentialEntries.Min(x => x.Report_time);
            var existingEntries = context.PositionReports.Where(x => x.Report_time >= minTime).ToList();
            foreach (var item in potentialEntries) {
                if (!existingEntries.Any(x => x.Sender_id == item.Sender_id && x.Position_id == item.Position_id && x.Report_time == item.Report_time)) {
                    context.PositionReports.Add(item);
                }
            }
            return context.SaveChanges();
        }

        public int SyncProducts(List<Product> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Product>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Product_id).ToList();
            var removedEntries = context.Products.Where(x => !IDs.Contains(x.Product_id)).ToList();
            foreach (var item in removedEntries) {
                context.Products.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncRanks(List<Rank> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Rank>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Rank_id).ToList();
            var removedEntries = context.Ranks.Where(x => !IDs.Contains(x.Rank_id)).ToList();
            foreach (var item in removedEntries) {
                context.Ranks.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncReceivers(List<Receiver> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Receiver>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Receiver_id).ToList();
            var removedEntries = context.Receivers.Where(x => !IDs.Contains(x.Receiver_id)).ToList();
            foreach (var item in removedEntries) {
                context.Receivers.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncCurrentAlarmReports(List<CurrentAlarmReport> newEntries) {
            var context = new PositionContext(DBSource.Internal);
            foreach (var item in newEntries) {
                context.CurrentAlarmReports.Add(item);
            }
            if (newEntries.Any()) {
                return context.SaveChanges();
            }
            return 0;
        }

        public int SyncRegions(List<Region> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Region>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Region_id).ToList();
            var removedEntries = context.Regions.Where(x => !IDs.Contains(x.Region_id)).ToList();
            foreach (var item in removedEntries) {
                context.Regions.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncRegionPositionSets(List<RegionPositionSet> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<RegionPositionSet>(item);
            }
            //Sync deleted records
            //var removedEntries = context.RegionPositionSets
            //    .Where(x => !externalEntries.Any(y => y.Region_id == x.Region_id && y.Position_id == x.Position_id)).ToList();
            //foreach (var item in removedEntries) {
            //    context.RegionPositionSets.Remove(item);
            //}

            return context.SaveChanges();
        }

        public int SyncSenders(List<Sender> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<Sender>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Sender_id).ToList();
            var removedEntries = context.Senders.Where(x => !IDs.Contains(x.Sender_id)).ToList();
            foreach (var item in removedEntries) {
                context.Senders.Remove(item);
            }

            return context.SaveChanges();
        }

        public int SyncWorkTypes(List<WorkType> externalEntries) {
            var context = new PositionContext(DBSource.Internal);
            //Sync added/updated records
            foreach (var item in externalEntries) {
                context.AddOrUpdate<WorkType>(item);
            }
            //Sync deleted records
            var IDs = externalEntries.Select(x => x.Worktype_id).ToList();
            var removedEntries = context.WorkTypes.Where(x => !IDs.Contains(x.Worktype_id)).ToList();
            foreach (var item in removedEntries) {
                context.WorkTypes.Remove(item);
            }

            return context.SaveChanges();
        }

        #region Get<Entries>
        public List<AlarmReport> GetAlarmReports(DBSource source) {
            var context = new PositionContext(source);
            return context.AlarmReports.ToList();
        }

        public List<AlarmType> GetAlarmTypes(DBSource source) {
            var context = new PositionContext(source);
            return context.AlarmTypes.ToList();
        }

        public List<Branch> GetBranches(DBSource source) {
            var context = new PositionContext(source);
            return context.Branches.ToList();
        }

        public List<Department> GetDepartments(DBSource source) {
            var context = new PositionContext(source);
            return context.Departments.ToList();
        }

        public List<Lamp> GetLamps(DBSource source) {
            var context = new PositionContext(source);
            return context.Lamps.ToList();
        }

        public List<Person> GetPeople(DBSource source) {
            var context = new PositionContext(source);
            return context.People.ToList();
        }

        public List<PeopleSender> GetPeopleSenders(DBSource source) {
            var context = new PositionContext(source);
            return context.PeopleSenders.ToList();
        }

        public List<Position> GetPositions(DBSource source) {
            var context = new PositionContext(source);
            return context.Positions.ToList();
        }

        public List<Product> GetProducts(DBSource source) {
            var context = new PositionContext(source);
            return context.Products.ToList();
        }

        public List<Rank> GetRanks(DBSource source) {
            var context = new PositionContext(source);
            return context.Ranks.ToList();
        }

        public List<Receiver> GetReceivers(DBSource source) {
            var context = new PositionContext(source);
            return context.Receivers.ToList();
        }

        public List<Region> GetRegions(DBSource source) {
            var context = new PositionContext(source);
            return context.Regions.ToList();
        }

        public List<RegionPositionSet> GetRegionPositionSets(DBSource source) {
            var context = new PositionContext(source);
            return context.RegionPositionSets.ToList();
        }

        public List<Sender> GetSenders(DBSource source) {
            var context = new PositionContext(source);
            return context.Senders.ToList();
        }

        public List<WorkType> GetWorkTypes(DBSource source) {
            var context = new PositionContext(source);
            return context.WorkTypes.ToList();
        }

        public List<AlarmReport> GetExternalAlarmReportsFrom(int lastId, int maxRows = 0) {
            var context = new PositionContext(DBSource.External);
            if (maxRows > 0) {
                var query = string.Format("SELECT TOP {0} * FROM AlarmReport WHERE Alarm_id > {1} ORDER BY Alarm_id", maxRows, lastId);
                var list = context.Database.SqlQuery<AlarmReport>(query).ToList();
                return list;
            }
            else {
                return context.AlarmReports.Where(x => x.Alarm_id > lastId).ToList();
            }
        }

        public List<CurrentAlarmReport> GetExternalCurrentAlarmReportsFrom(int lastId) {
            var context = new PositionContext(DBSource.External);
            return context.CurrentAlarmReports.Where(x => x.Alarm_id > lastId).ToList();
        }

        public List<CurrentPositionReport> GetExternalCurrentPositionReportsFrom(DateTime lastTime) {
            var context = new PositionContext(DBSource.External);
            var query = context.Database.SqlQuery<CurrentPositionReport>(string.Format("SELECT * FROM CurrentPositionReport WHERE Report_time>'{0}'", lastTime.ToString("yyyyMMdd HH:mm:ss")));
            var list = query.ToList();
            return list;
        }

        public List<PositionReport> GetExternalPositionReportsFrom(DateTime lastTime, int maxRows = 0) {
            var context = new PositionContext(DBSource.External);
            var top = maxRows > 0 ? string.Format("TOP {0}", maxRows) : "";
            var query = string.Format("SELECT {0} * FROM PositionReport WHERE Report_time>'{1}'", top, lastTime.ToString("yyyyMMdd HH:mm:ss"));
            var list = context.Database.SqlQuery<PositionReport>(query).ToList();
            return list;
        }
        #endregion

        #region Get<Entry>MaxId
        public int GetAlarmReportMaxId() {
            var context = new PositionContext(DBSource.Internal);
            int maxId = 0;
            if (context.AlarmReports.Any()) {
                maxId = context.AlarmReports.Max(x => x.Alarm_id);
            }
            return maxId;
        }

        public int GetCurrentAlarmReportMaxId() {
            var context = new PositionContext(DBSource.Internal);
            int maxId = 0;
            if (context.CurrentAlarmReports.Any()) {
                maxId = context.CurrentAlarmReports.Max(x => x.Alarm_id);
            }
            return maxId;
        }

        public DateTime GetCurrentPositionReportMaxTime() {
            var context = new PositionContext(DBSource.Internal);
            DateTime maxTime = new DateTime(2000, 1, 1);
            if (context.CurrentPositionReports.Any()) {
                maxTime = context.CurrentPositionReports.Max(x => x.Report_time);
            }
            return maxTime;
        }

        public DateTime GetPositionReportMaxTime() {
            var context = new PositionContext(DBSource.Internal);
            DateTime maxTime = new DateTime(2000, 1, 1);
            if (context.PositionReports.Any()) {
                maxTime = context.PositionReports.Max(x => x.Report_time);
            }
            return maxTime;
        }
        #endregion
    }
}
