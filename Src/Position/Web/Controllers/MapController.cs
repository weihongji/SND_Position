using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Web.Models;
using DataAccess.Models;

namespace Web.Controllers
{
    public class MapController : Controller
    {
        private DataAccess.Dao dao = new DataAccess.Dao();
        //
        // GET: /Home/

        public ActionResult Index() {
            var model = new MapMgmtModel();
            model.Map = dao.GetMap(MapScale.Small);
            model.MonitorList = dao.GetMonitorPoints(null);
            return View(model);
        }

        public ActionResult SaveMonitorPoint(MonitorPoint point) {
            int result = 0;
            string error = string.Empty;
            try {
                result = dao.SaveMonitorPoint(point);
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

        public ActionResult SaveMonitorPointPosition(int id, int left, int top, int typeId) {
            int result = 0;
            string error = string.Empty;
            MonitorPoint point = null;
            try {
                var monitorType = dao.GetMonitorType(typeId);
                point = new MonitorPoint(id, left, top, typeId, monitorType);
                result = dao.SaveMonitorPointPosition(point);
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
                result = dao.DeleteMonitorPoint(id);
            }
            catch (Exception ex) {
                error = ex.Message;
            }
            return Json(new { success = result > 0, message = error }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SunSet() {
            return View();
        }
    }
}
