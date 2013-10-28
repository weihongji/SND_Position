using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Models;

namespace DataAccess
{
    public class Dao
    {
        public List<Region> GetRegions() {
            var context = new PositionContext();
            return context.Regions.ToList();
        }

        public List<Branch> GetBranches() {
            var context = new PositionContext();
            return context.Branches.ToList();
        }

        public List<Receiver> GetReceivers() {
            var context = new PositionContext();
            return context.Receivers.ToList();
        }

        public List<Position> GetPositions() {
            var context = new PositionContext();
            return context.Positions.ToList();
        }

        public List<PeopleOverviewReportItem> GetPeopleOverviewReport() {
            var context = new PositionContext();
            var query = context.Database.SqlQuery<PeopleOverviewReportItem>("spPeopleOverview");
            return query.ToList();
        }

        public List<PeopleSearchReportItem> GetPeopleSearchReport(int? senderId, string lampId, string peopleName, int? peopleId, int? deptId, int? rankId, DateTime? reportForTime, XEnum.WorkPlace workPlace) {
            var paramNames = new List<string>();
            var paramObjects = new List<SqlParameter>();
            int i = 0;
            string p;

            if (senderId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@senderId = " + p);
                paramObjects.Add(new SqlParameter(p, senderId.Value));
            }
            if (!string.IsNullOrEmpty(lampId)) {
                p = "@p" + i++.ToString();
                paramNames.Add("@lampId = " + p);
                paramObjects.Add(new SqlParameter(p, lampId));
            }
            if (!string.IsNullOrEmpty(peopleName)) {
                p = "@p" + i++.ToString();
                paramNames.Add("@peopleName = " + p);
                paramObjects.Add(new SqlParameter(p, peopleName));
            }
            if (peopleId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@peopleId = " + p);
                paramObjects.Add(new SqlParameter(p, peopleId.Value));
            }
            if (deptId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@deptId = " + p);
                paramObjects.Add(new SqlParameter(p, deptId.Value));
            }
            if (rankId.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@rankId = " + p);
                paramObjects.Add(new SqlParameter(p, rankId.Value));
            }
            if (reportForTime.HasValue) {
                p = "@p" + i++.ToString();
                paramNames.Add("@forTime = " + p);
                paramObjects.Add(new SqlParameter(p, reportForTime.Value));
            }
            if (workPlace != XEnum.WorkPlace.Any) {
                p = "@p" + i++.ToString();
                paramNames.Add("@isInWell = " + p);
                paramObjects.Add(new SqlParameter(p, workPlace == XEnum.WorkPlace.Well));
            }

            var context = new PositionContext();
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

            var context = new PositionContext();
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

            var context = new PositionContext();
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

            var context = new PositionContext();
            var sp = "spQueryAlarmListCurrent";
            if (criteria.StartAt.HasValue && (DateTime.Now - criteria.StartAt.Value).Days > 0) { // Request a report of prior to today
                sp = "spQueryAlarmList";
            }
            var query = context.Database.SqlQuery<AlarmReportItem>(sp + " " + string.Join(", ", paramNames.ToArray()), paramObjects.ToArray());
            var list = query.ToList();

            return list;
        }
    }
}
