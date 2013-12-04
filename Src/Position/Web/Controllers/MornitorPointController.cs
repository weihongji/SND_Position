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
        private DataAccess.Dao dao = new DataAccess.Dao();

        public ActionResult Index() {
            var model = new MonitorPointIndexModel {
                MonitorSystems = dao.GetMonitorSystems()
            };

            return View(model);
        }

        public ActionResult Show(int id, MapSize size) {
            var model = new MonitorPointModel();
            model.System = dao.GetMonitorSystem(id);
            model.Map = dao.GetMonitorMap(id, size);
            model.ContentList = dao.GetMonitorContents(id);
            model.MonitorList = dao.GetMonitorPoints(id);
            return View(model);
        }

        public ActionResult Edit(int id) {
            var model = new MonitorPointModel();
            model.System = dao.GetMonitorSystem(id);
            model.Map = dao.GetMonitorMap(id, MapSize.Small);
            model.ContentList = dao.GetMonitorContents(id);
            model.MonitorList = dao.GetMonitorPoints(id);
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
                var monitorContent = dao.GetMonitorContent(typeId);
                point = new MonitorPoint(id, left, top, typeId, monitorContent);
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
    }
}
