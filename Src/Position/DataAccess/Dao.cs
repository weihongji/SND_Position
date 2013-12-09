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
        const MapSize SettingMapSize = MapSize.Large;

        private PositionContext context;
        private MonitorMap _settingMap_hard_to_remember;

        public Dao() {
            context = new PositionContext();
        }

        private MonitorMap SettingMap {
            get {
                if (_settingMap_hard_to_remember == null) {
                    _settingMap_hard_to_remember = context.MonitorMaps.Where(x => x.SizeType == (int)SettingMapSize).First();
                }
                return _settingMap_hard_to_remember;
            }
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

        public List<MonitorSystem> GetMonitorSystems() {
            return context.MonitorSystems.ToList();
        }

        public MonitorSystem GetMonitorSystem(int systemId) {
            var system = context.MonitorSystems.Where(x => x.Id == systemId).FirstOrDefault();
            if (system == null) {
                throw new ArgumentException(string.Format("Monitory system with id {0} cannot be found.", systemId));
            }
            return system;
        }

        public List<MonitorContent> GetMonitorContents(int systemId) {
            return context.MonitorContents.Where(x => x.MonitorSystemId == systemId).ToList();
        }

        public MonitorContent GetMonitorContent(int id) {
            var type = context.MonitorContents.Find(id);
            if (type == null) {
                throw new ArgumentException(string.Format("Cannot find monitor content with id {0}.", id));
            }
            return type;
        }

        public List<MonitorPoint> GetMonitorPoints(int systemId) {
            var query = context.MonitorPoints.Include(x => x.MonitorContent);
            var contentIds = context.MonitorContents.Where(c => c.MonitorSystemId == systemId).Select(c => c.Id);
            query = query.Where(x => contentIds.Contains(x.MonitorContentId.Value));
            var list = query.ToList();
            list.ForEach(x => ConvertMonitorPointPosition(x, true));
            return list;
        }

        public List<MonitorPoint> GetMonitorPoints(int systemId, MapSize size) {
            var query = context.MonitorPoints.Include(x => x.MonitorContent);
            var contentIds = context.MonitorContents.Where(c => c.MonitorSystemId == systemId).Select(c => c.Id);
            query = query.Where(x => contentIds.Contains(x.MonitorContentId.Value));
            var list = query.ToList();

            var currentMap = this.GetMonitorMap(systemId, size);

            decimal ratio = 1;
            if (size != SettingMapSize) {
                ratio = ((decimal)SettingMap.Scale) / currentMap.Scale;
            }

            list.ForEach(p => {
                p.X = currentMap.StartX + (int)(p.OffsetX * ratio);
                p.Y = currentMap.StartY + (int)(p.OffsetY * ratio);
            });
            return list;
        }

        public MonitorPoint GetMonitorPoint(int id) {
            var point = context.MonitorPoints.Single(x => x.Id == id);
            return point;
        }

        public List<MonitorMap> GetMonitorMaps(int systemId) {
            return context.MonitorMaps.Where(x => x.MonitorSystemId == systemId).ToList();
        }

        public MonitorMap GetMonitorMap(int id) {
            var map = context.MonitorMaps.Single(x => x.Id == id);
            return map;
        }

        public MonitorMap GetMonitorMap(int systemId, MapSize size) {
            var map = context.MonitorMaps.Where(x => x.MonitorSystemId == systemId && x.SizeType == (int)size).FirstOrDefault();
            if (map == null) {
                throw new ArgumentException(string.Format("Cannot find map of size {0} for system id {1}.", size, systemId));
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

        public int SaveMonitorPoint(MonitorPoint entity) {
            if (entity == null) {
                return 0;
            }
            ConvertMonitorPointPosition(entity, false);

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
            ConvertMonitorPointPosition(point, false);
            if (point.Id == 0) {
                var entity = new MonitorPoint {
                    OffsetX = point.OffsetX,
                    OffsetY = point.OffsetY
                };
                context.MonitorPoints.Add(point);
            }
            else {
                var tracked = context.MonitorPoints.Find(point.Id);
                tracked.OffsetX = point.OffsetX;
                tracked.OffsetY = point.OffsetY;
            }
            return context.SaveChanges();
        }

        private void ConvertMonitorPointPosition(MonitorPoint point, bool OffsetToPosition) {
            if (point == null) {
                return;
            }
            if (OffsetToPosition) {
                point.X = point.OffsetX.Value + SettingMap.StartX;
                point.Y = point.OffsetY.Value + SettingMap.StartY;
            }
            else {
                point.OffsetX = point.X - SettingMap.StartX;
                point.OffsetY = point.Y - SettingMap.StartY;
            }
        }

        public int SaveMonitorMap(MonitorMap entity) {
            if (entity == null) {
                return 0;
            }

            if (entity.Id > 0) {
                var tracked = context.MonitorMaps.Find(entity.Id);
                if (tracked == null) {
                    return 0;
                }
                else {
                    context.Entry(tracked).CurrentValues.SetValues(entity);
                    entity = tracked;
                }
            }
            else {
                entity = context.MonitorMaps.Add(entity);
            }
            return context.SaveChanges();
        }

    }
}
