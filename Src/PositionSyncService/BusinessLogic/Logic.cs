using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess;
using SharedEntity;

namespace BusinessLogic
{
    public class Logic
    {
        private Dao dao = new Dao();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int SyncAlarmReports(int maxRowsPerSync) {
            var maxId = dao.GetAlarmReportMaxId();
            var newEntries = dao.GetExternalAlarmReportsFrom(maxId, maxRowsPerSync);
            log.DebugFormat("{0} records need to be imported.", newEntries.Count);
            int count = 0;
            if (newEntries.Count > 0) {
                count = dao.SyncAlarmReports(newEntries);
            }
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncBranches() {
            var externals = dao.GetBranches(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncBranches(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncCurrentAlarmReports() {
            var maxId = dao.GetCurrentAlarmReportMaxId();
            var newEntries = dao.GetExternalCurrentAlarmReportsFrom(maxId);
            log.DebugFormat("{0} records need to be imported.", newEntries.Count);
            int count = 0;
            if (newEntries.Count > 0) {
                count = dao.SyncCurrentAlarmReports(newEntries);
            }
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncCurrentPositionReports() {
            var maxTime = dao.GetCurrentPositionReportMaxTime().AddMinutes(-5);
            var potentialEntries = dao.GetExternalCurrentPositionReportsFrom(maxTime);
            log.DebugFormat("About {0} records need to check.", potentialEntries.Count);
            int count = 0;
            if (potentialEntries.Count > 0) {
                count = dao.SyncCurrentPositionReports(potentialEntries);
            }
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncDepartments() {
            var externals = dao.GetDepartments(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncDepartments(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncLamps() {
            var externals = dao.GetLamps(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncLamps(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncPeople() {
            var externals = dao.GetPeople(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncPeople(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncPeopleSenders() {
            var externals = dao.GetPeopleSenders(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncPeopleSenders(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncPositions() {
            var externals = dao.GetPositions(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncPositions(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncPositionReports(int maxRowsPerSync) {
            var maxTime = dao.GetPositionReportMaxTime().AddMinutes(-5);
            var potentialEntries = dao.GetExternalPositionReportsFrom(maxTime, maxRowsPerSync);
            log.DebugFormat("About {0} records need to check.", potentialEntries.Count);
            int count = 0;
            if (potentialEntries.Count > 0) {
                count = dao.SyncPositionReports(potentialEntries);
            }
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncProducts() {
            var externals = dao.GetProducts(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncProducts(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncRanks() {
            var externals = dao.GetRanks(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncRanks(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncReceivers() {
            var externals = dao.GetReceivers(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncReceivers(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncRegions() {
            var externals = dao.GetRegions(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncRegions(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncRegionPositionSets() {
            var externals = dao.GetRegionPositionSets(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncRegionPositionSets(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncSenders() {
            var externals = dao.GetSenders(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncSenders(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }

        public int SyncWorkTypes() {
            var externals = dao.GetWorkTypes(DBSource.External);
            log.DebugFormat("{0} records in the external table.", externals.Count);
            var count = dao.SyncWorkTypes(externals);
            if (count > 0) {
                log.InfoFormat("{0} records synchronized.", count);
            }
            else {
                log.DebugFormat("{0} records synchronized.", count);
            }
            return count;
        }
    }
}
