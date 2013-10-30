using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLogic;
using SharedEntity;

namespace ServiceHost
{
    public static class ServiceLogic
    {
        private static Logic logic = new Logic();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static int maxRowsEachSync;

        static ServiceLogic() {
            if (!int.TryParse(ConfigurationManager.AppSettings["maxRowsEachSync"], out maxRowsEachSync)) {
                maxRowsEachSync = 0;
            }
        }

        public static void SyncDataAtSecond() {
            log.DebugFormat("Start sync second-refresh data{0}{1}", Environment.NewLine, new string('-', 50));
            logic.SyncCurrentAlarmReports();
            logic.SyncCurrentPositionReports();
            logic.SyncPositionReports(maxRowsEachSync);
            log.DebugFormat("End sync second-refresh data{0}", Environment.NewLine);
        }

        public static void SyncDataAtMinute() {
            log.DebugFormat("Start sync minute-refresh data{0}{1}", Environment.NewLine, new string('=', 50));
            logic.SyncAlarmReports(maxRowsEachSync);
            logic.SyncAlarmTypes();
            logic.SyncBranches();
            logic.SyncDepartments();
            logic.SyncLamps();
            logic.SyncPeople();
            logic.SyncPeopleSenders();
            logic.SyncPositions();
            logic.SyncProducts();
            logic.SyncRanks();
            logic.SyncReceivers();
            logic.SyncRegions();
            logic.SyncRegionPositionSets();
            logic.SyncSenders();
            logic.SyncWorkTypes();
            log.DebugFormat("End sync minute-refresh data{0}", Environment.NewLine);
        }
    }
}
