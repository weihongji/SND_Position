using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DataAccess;
using DataAccess.Models;
using Web.Models;
using System.Linq.Expressions;

namespace Web.Controllers
{
    public class ReportController : Controller
    {
        private Dao _dao = new Dao();

        //
        // GET: /Report/

        public ActionResult Index() {
            return View();
        }

        public ActionResult PeopleOverview() {
            var dao = new Dao();
            var list = dao.GetPeopleOverviewReport();
            return View(list);

        }

        public ActionResult PeopleSearch(string SenderId, string LampId, string PeopleName, string PeopleId, string DeptId, string RankId, string UserServerTime, string ReportTime, string WorkPlace) {
            if (Request.QueryString.Count == 0) {
                return View(new List<PeopleSearchReportItem>());
            }

            int tmpInt;
            DateTime tmpTime;

            int? senderId = null;
            int? peopleId = null;
            int? deptId = null;
            int? rankId = null;
            DateTime? reportForTime = null;
            XEnum.WorkPlace workPlace = XEnum.WorkPlace.Any;

            if (int.TryParse(SenderId, out tmpInt)) {
                senderId = tmpInt;
            }
            if (int.TryParse(PeopleId, out tmpInt)) {
                peopleId = tmpInt;
            }
            if (int.TryParse(DeptId, out tmpInt)) {
                deptId = tmpInt;
            }
            if (int.TryParse(RankId, out tmpInt)) {
                rankId = tmpInt;
            }
            if (UserServerTime != "1") {
                if (DateTime.TryParse(ReportTime, out tmpTime)) {
                    reportForTime = tmpTime;
                }
            }
            if (!Enum.TryParse<XEnum.WorkPlace>(WorkPlace, out workPlace)) {
                workPlace = XEnum.WorkPlace.Any;
            }

            var dao = new Dao();
            var list = dao.GetPeopleSearchReport(senderId, LampId, PeopleName, peopleId, deptId, rankId, reportForTime, workPlace);
            return View(list);
        }

        public ActionResult PositionSearch(int? SearchType, int? TypeId, DateTime? ReportTime) {
            var model = new PositionSearchModel();
            if (Request.Form.Count == 0) {
                PopulateListItems(model);
                return View(model);
            }

            var criteria = new PositionSearchCriteria() { ReportForTime = ReportTime };
            if (TypeId.HasValue) {
                var searchType = (PositionSearchType)SearchType.Value;
                switch (searchType) {
                    case PositionSearchType.Region:
                        criteria.RegionId = TypeId;
                        break;
                    case PositionSearchType.Branch:
                        criteria.BranchId = TypeId;
                        break;
                    case PositionSearchType.Receiver:
                        criteria.ReceiverId = TypeId;
                        break;
                    case PositionSearchType.Position:
                        criteria.PositionId = TypeId;
                        break;
                    default:
                        throw new ArgumentException("Invalid SearchType " + SearchType.Value);
                }
            }

            var list = _dao.GetPositionSearchReport(criteria);
            model.ReportItems = list;
            if (Request.IsAjaxRequest()) {
                return PartialView("PositionSearchList", model);
            }
            else {
                PopulateListItems(model);
            }
            return View(model);
        }

        private void PopulateListItems(PositionSearchModel model) {
            model.Regions = ControlHelper.GetListItems(_dao.GetRegions());
            model.Branches = ControlHelper.GetListItems(_dao.GetBranches());
            model.Receivers = ControlHelper.GetListItems(_dao.GetReceivers().Select(x=>x.Receiver_id).Distinct().ToList());
            model.Positions = ControlHelper.GetListItems(_dao.GetPositions());
        }
    }
}
