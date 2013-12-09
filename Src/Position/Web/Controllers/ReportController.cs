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
            var list = _dao.GetPeopleOverviewReport();
            return View(list);

        }

        public ActionResult PeopleSearch(int? SenderId, string LampId, string PersonName, int? PersonId, int? DeptId, int? RankId, DateTime? ReportForTime, WorkPlace? WorkPlace) {
            var model = new PeopleSearchModel();
            if (Request.Form.Count == 0) {
                PopulatePeopleSearchListItems(model);
                return View(model);
            }

            var criteria = new PeopleSearchCriteria {
                SenderId = SenderId,
                LampId = LampId,
                PersonName = PersonName,
                PersonId = PersonId,
                DeptId = DeptId,
                RankId = RankId,
                ReportForTime = ReportForTime,
                WorkPlace = WorkPlace.Value
            };
            var list = _dao.GetPeopleSearchReport(criteria);
            model.ReportItems = list;
            if (Request.IsAjaxRequest()) {
                return PartialView("PeopleSearchList", model);
            }
            else {
                PopulatePeopleSearchListItems(model);
            }
            return View(model);
        }

        private void PopulatePeopleSearchListItems(PeopleSearchModel model) {
            model.Departments = ControlHelper.GetListItems(_dao.GetDepartments());
            model.Departments.Insert(0, new SelectListItem() { Value = "", Text = "所有部门", Selected = true });
            model.Ranks = ControlHelper.GetListItems(_dao.GetRanks());
            model.Ranks.Insert(0, new SelectListItem() { Value = "", Text = "所有职务", Selected = true });
        }

        public ActionResult PositionSearch(int? SearchType, int? TypeId, DateTime? ReportForTime) {
            var model = new PositionSearchModel();
            if (Request.Form.Count == 0) {
                PopulatePositionSearchListItems(model);
                return View(model);
            }

            var criteria = new PositionSearchCriteria() { ReportForTime = ReportForTime };
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
                PopulatePositionSearchListItems(model);
            }
            return View(model);
        }

        private void PopulatePositionSearchListItems(PositionSearchModel model) {
            model.Regions = ControlHelper.GetListItems(_dao.GetRegions());
            model.Branches = ControlHelper.GetListItems(_dao.GetBranches());
            model.Receivers = ControlHelper.GetListItems(_dao.GetReceivers().Select(x=>x.Receiver_id).Distinct().ToList());
            model.Positions = ControlHelper.GetListItems(_dao.GetPositions());
        }

        public ActionResult PeopleCount(int? SearchType, DateTime? ReportForTime) {
            var model = new PeopleCountModel();
            if (SearchType == null) {
                return View(model);
            }
            model.SearchType = (PeopleCountSearchType)SearchType.Value;
            model.ReportItems = _dao.GetPeopleCountReport((PeopleCountSearchType)SearchType.Value, ReportForTime);
            if (Request.IsAjaxRequest()) {
                return PartialView("PeopleCountList", model);
            }
            return View(model);
        }

        public ActionResult AlarmSearch(byte? AlarmType, int? Param1, int? Param2, int? ProcessStatus, DateTime? StartAt, DateTime? EndAt) {
            var model = new AlarmSearchModel();
            if (Request.Form.Count == 0) {
                PopulateAlarmSearchListItems(model);
                return View(model);
            }

            var criteria = new AlarmReportCriteria {
                Type = AlarmType,
                Param1 = Param1,
                Param2 = Param2,
                ProcessStatus = ProcessStatus,
                StartAt = StartAt,
                EndAt = EndAt
            };

            var list = _dao.GetAlarmReport(criteria);
            model.ReportItems = list;
            if (Request.IsAjaxRequest()) {
                return PartialView("AlarmSearchList", model);
            }
            else {
                PopulateAlarmSearchListItems(model);
            }
            return View(model);
        }

        private void PopulateAlarmSearchListItems(AlarmSearchModel model) {
            model.Regions = ControlHelper.GetListItems(_dao.GetRegions());
            model.Regions.Insert(0, new SelectListItem() { Value = "-1", Text = "所有区域", Selected = true });
            model.Branches = ControlHelper.GetListItems(_dao.GetBranches());
            model.Branches.Insert(0, new SelectListItem() { Value = "-1", Text = "所有分站", Selected = true });
            model.Receivers = ControlHelper.GetListItems(_dao.GetReceivers().Select(x => x.Receiver_id).Distinct().ToList());
            model.Receivers.Insert(0, new SelectListItem() { Value = "-1", Text = "所有收发器", Selected = true });
        }
    }
}
