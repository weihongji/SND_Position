using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class MonitorPointController : Controller
    {
        const MapSize SettingMapSize = MapSize.Large;

        private DataAccess.Dao dao = new DataAccess.Dao();

        public ActionResult Index() {
            var model = dao.GetMonitorSystems();
            return View(model);
        }

        public ActionResult Show([Bind(Prefix = "id")] int systemId, MapSize size) {
            var model = new MonitorPointModel();
            model.System = dao.GetMonitorSystem(systemId);
            model.Map = dao.GetMonitorMap(systemId, size);
            if (model.Map == null) {
                return View("MapNotSet", model.System);
            }
            model.ContentList = dao.GetMonitorContents(systemId);
            model.MonitorList = dao.GetMonitorPoints(systemId, size);
            return View(model);
        }

        public ActionResult Edit([Bind(Prefix = "id")] int systemId) {
            var model = new MonitorPointModel();
            model.System = dao.GetMonitorSystem(systemId);
            model.Map = dao.GetMonitorMap(systemId, SettingMapSize);
            if (model.Map == null) {
                return View("MapNotSet", model.System);
            }
            model.ContentList = dao.GetMonitorContents(systemId);
            model.MonitorList = dao.GetMonitorPoints(systemId);
            return View(model);
        }

        public ActionResult LoadMonitorPoints(int typeId, string id) {
            int includeId;
            if (!int.TryParse(id, out includeId)) {
                includeId = 0;
            }
            var pointers = dao.GetNotPinnedMonitorPoints(typeId, includeId);
            return Json(pointers, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveMonitorPointPosition(int id, int left, int top, int typeId, int originalId) {
            MonitorPoint point = null;
            string error = string.Empty;
            try {
                point = dao.SaveMonitorPointPosition(id, left, top, typeId, originalId);
            }
            catch (Exception ex) {
                error = ex.Message;
            }
            return Json(new
            {
                success = string.IsNullOrEmpty(error),
                entity = point,
                title = point == null ? "" : point.ToString(),
                message = error
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMonitorPoint(int id) {
            int result = 0;
            string error = string.Empty;
            try {
                result = dao.DeleteMonitorPointPosition(id);
            }
            catch (Exception ex) {
                error = ex.Message;
            }
            return Json(new { success = result > 0, message = error }, JsonRequestBehavior.AllowGet);
        }
    }
}
