using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

namespace DataAccess
{
    public class Dao
    {
        private PositionContext context;

        public Dao() {
            context = new PositionContext();
        }

        public List<Region> GetRegions() {
            return context.Regions.ToList();
        }

        public List<Department> GetDepartments() {
            return context.Departments.ToList();
        }

        public List<Rank> GetRanks() {
            return context.Ranks.ToList();
        }

        public List<Branch> GetBranches() {
            return context.Branches.ToList();
        }

        public List<Receiver> GetReceivers() {
            return context.Receivers.ToList();
        }

        public List<Position> GetPositions() {
            return context.Positions.ToList();
        }

        public List<Map> GetMaps() {
            return context.Maps.ToList();
        }

        public Map GetMap(MapScale scale) {
            var map = context.Maps.Where(x => x.Scale == (int)scale).FirstOrDefault();
            if (map == null) {
                throw new ArgumentException(string.Format("Cannot find map of scale {0}.", scale));
            }
            return map;
        }

        public List<PeopleOverviewReportItem> GetPeopleOverviewReport() {
            var query = context.Database.SqlQuery<PeopleOverviewReportItem>("spPeopleOverview");
            return query.ToList();
        }

        public List<PeopleSearchReportItem> GetPeopleSearchReport(PeopleSearchCriteria criteria) {
            var paramNames = new List<string>();
            var paramObjects = new List<SqlParameter>();
            int i = 0;
            string p;

            if (criteria.SenderId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@senderId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.SenderId.Value));
            }
            if (!string.IsNullOrEmpty(criteria.LampId)) {
                p = "@p" + i++.ToString();
                paramNames.Add("@lampId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.LampId));
            }
            if (!string.IsNullOrEmpty(criteria.PersonName)) {
                p = "@p" + i++.ToString();
                paramNames.Add("@peopleName = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.PersonName));
            }
            if (criteria.PersonId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@peopleId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.PersonId));
            }
            if (criteria.DeptId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@deptId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.DeptId.Value));
            }
            if (criteria.RankId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@rankId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.RankId.Value));
            }
            if (criteria.ReportForTime.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@forTime = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.ReportForTime.Value));
            }
            if (criteria.WorkPlace != WorkPlace.Any) {
                p = "@p" + i++.ToString();
                paramNames.Add("@isInWell = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.WorkPlace == WorkPlace.Well));
            }

            var query = context.Database.SqlQuery<PeopleSearchReportItem>("spPeopleSearch " + string.Join(", ", paramNames.ToArray()), paramObjects.ToArray());
            var list = query.ToList();

            //Assign indexes
            int index = 1;
            list.ForEach(x => x.Index = index++);

            return list;
        }

        public List<PositionSearchReportItem> GetPositionSearchReport(PositionSearchCriteria criteria) {
            var paramNames = new List<string>();
            var paramObjects = new List<SqlParameter>();
            int i = 0;
            string p;

            if (criteria.RegionId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@regionId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.RegionId.Value));
            }
            if (criteria.BranchId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@branchId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.BranchId.Value));
            }
            if (criteria.ReceiverId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@receiverId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.ReceiverId.Value));
            }
            if (criteria.PositionId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@positionId = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.PositionId.Value));
            }
            if (criteria.ReportForTime.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@forTime = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.ReportForTime.Value));
            }

            var query = context.Database.SqlQuery<PositionSearchReportItem>("spPositionSearch " + string.Join(", ", paramNames.ToArray()), paramObjects.ToArray());
            var list = query.ToList();

            //Assign indexes
            int index = 1;
            list.ForEach(x => x.Index = index++);

            return list;
        }

        public List<PeopleCountReportItem> GetPeopleCountReport(PeopleCountSearchType searchType, DateTime? reportForTime) {
            string sp;
            switch (searchType) {
                case PeopleCountSearchType.Region:
                    sp = "spPeopleCountByRegion";
                    break;
                case PeopleCountSearchType.Department:
                    sp = "spPeopleCountByDepartment";
                    break;
                case PeopleCountSearchType.WorkType:
                    sp = "spPeopleCountByWorkType";
                    break;
                case PeopleCountSearchType.Rank:
                    sp = "spPeopleCountByRank";
                    break;
                default:
                    throw new ArgumentException("Invalid search type for PeopleCountReport.");
            }

            var sqlText = string.Format("{0} @forTime = {1}", sp, reportForTime == null ? "null" : "'" + reportForTime.Value.ToString("yyyyMMdd HH:mm:ss") + "'");
            var query = context.Database.SqlQuery<PeopleCountReportItem>(sqlText);
            var list = query.ToList();

            //Assign indexes
            int index = 1;
            list.ForEach(x => x.Index = index++);

            return list;
        }

        public List<AlarmReportItem> GetAlarmReport(AlarmReportCriteria criteria) {
            var paramNames = new List<string>();
            var paramObjects = new List<SqlParameter>();
            int i = 0;
            string p;

            if (criteria.Type.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@Alarm_type = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.Type.Value));
            }
            if (criteria.Param1.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@Alarm_param1 = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.Param1.Value));
            }
            if (criteria.Param2.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@Alarm_param2 = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.Param2.Value));
            }
            if (criteria.ProcessStatus.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@Process_status = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.ProcessStatus.Value));
            }
            if (criteria.StartAt.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@Begin_time = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.StartAt.Value));
            }
            if (criteria.EndAt.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@End_time = " + p);
                paramObjects.Add(new SqlParameter(p, criteria.EndAt.Value));
            }

            var sp = "spQueryAlarmListCurrent";
            if (criteria.StartAt.HasValue && (DateTime.Now - criteria.StartAt.Value).Days > 0) { // Request a report of prior to today
                sp = "spQueryAlarmList";
            }
            var query = context.Database.SqlQuery<AlarmReportItem>(sp + " " + string.Join(", ", paramNames.ToArray()), paramObjects.ToArray());
            var list = query.ToList();

            return list;
        }

        public MonitorType GetMonitorType(int id) {
            var type = context.MonitorTypes.Find(id);
            if (type == null) {
                throw new ArgumentException(string.Format("Cannot find monitor type with id {0}.", id));
            }
            return type;
        }

        public List<MonitorType> GetMonitorTypes() {
            return context.MonitorTypes.ToList();
        }

        public List<MonitorPoint> GetMonitorPoints(MonitorType type) {
            var query = context.MonitorPoints.Include(x => x.MonitorType);
            if (type != null && type.Id > 0) {
                query = query.Where(x => x.MonitorTypeId == type.Id);
            }
            return query.ToList();
        }

        public int SaveMonitorPoint(MonitorPoint entity) {
            if (entity == null) {
                return 0;
            }
            if (entity.Id > 0) {
                var tracked = context.MonitorPoints.Find(entity.Id);
                if (tracked == null) {
                    return 0;
                }
                else {
                    context.Entry(tracked).CurrentValues.SetValues(entity);
                    entity = tracked;
                }
            }
            else {
                entity = context.MonitorPoints.Add(entity);
            }
            return context.SaveChanges();
        }

        public int DeleteMonitorPoint(int id) {
            var tracked = context.MonitorPoints.Find(id);
            if (tracked == null) {
                throw new ArgumentException(string.Format("Cannot find monitor point with id {0}.", id));
            }
            context.MonitorPoints.Remove(tracked);
            return context.SaveChanges();
        }

        public int SaveMonitorPointPosition(MonitorPoint point) {
            if (point.Id == 0) {
                var entity = new MonitorPoint {
                    X = point.X,
                    Y = point.Y
                };
                context.MonitorPoints.Add(point);
            }
            else {
                var tracked = context.MonitorPoints.Find(point.Id);
                tracked.X = point.X;
                tracked.Y = point.Y;
            }
            return context.SaveChanges();
        }
    }
}
