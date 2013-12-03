using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Web.Models;
using DataAccess.Models;
using System.IO;

namespace Web.Controllers
{
    public class MonitorMapController : Controller
    {
        private DataAccess.Dao _dao = new DataAccess.Dao();
        private List<MonitorMap> _mapList = null;

        public ActionResult Index() {
            var model = new MonitorMapIndexModel {
                MonitorSystems = _dao.GetMonitorSystems()
            };
            return View(model);
        }

        public ActionResult Edit(int id) {
            var model = new MonitorMapModel {
                System = _dao.GetMonitorSystem(id),
                MapNames = new List<string> { "小地图", "中地图", "大地图" }
            };

            var maps = _dao.GetMonitorMaps(id);
            var map = maps.SingleOrDefault(x => x.Scale == (int)MapScale.Small);
            model.Maps.Add(map ?? new MonitorMap { MonitorSystemId = id, DisplayName = "尚未设置", Scale = (int)MapScale.Small });

            map = maps.SingleOrDefault(x => x.Scale == (int)MapScale.Medim);
            model.Maps.Add(map ?? new MonitorMap { MonitorSystemId = id, DisplayName = "尚未设置", Scale = (int)MapScale.Medim });

            map = maps.SingleOrDefault(x => x.Scale == (int)MapScale.Large);
            model.Maps.Add(map ?? new MonitorMap { MonitorSystemId = id, DisplayName = "尚未设置", Scale = (int)MapScale.Large });

            return View(model);
        }

        public ActionResult Upload(int id, HttpPostedFileBase SmallMap, HttpPostedFileBase MedimMap, HttpPostedFileBase LargeMap) {
            SaveMap(id, SmallMap, MapScale.Small);
            SaveMap(id, MedimMap, MapScale.Medim);
            SaveMap(id, LargeMap, MapScale.Large);

            return RedirectToAction("Edit", new { id = id });
        }

        private int SaveMap(int systemId, HttpPostedFileBase uploadedFile, MapScale scale) {
            if (uploadedFile != null && uploadedFile.ContentLength > 0) {
                var displayName = Path.GetFileName(uploadedFile.FileName);
                var ext = Path.GetExtension(uploadedFile.FileName);
                var mapName = string.Format("Map_{0}_{1}{2}", systemId, (int)scale, ext);
                var filePath = Path.Combine(HttpContext.Server.MapPath("~/Images"), mapName);

                var map = new MonitorMap {
                    Name = mapName,
                    DisplayName = displayName,
                    MonitorSystemId = systemId,
                    Scale = (int)scale
                };

                if (_mapList == null) {
                    _mapList = _dao.GetMonitorMaps(systemId);
                }
                var tracked = _mapList.FirstOrDefault(x => IsSameMap(x.Name, map.Name));
                if (tracked != null) {
                    map.Id = tracked.Id;
                    System.IO.File.Delete(Path.Combine(HttpContext.Server.MapPath("~/Images"), tracked.Name));
                }
                uploadedFile.SaveAs(filePath);
                return _dao.SaveMonitorMap(map);
            }
            return 0;
        }

        private bool IsSameMap(string map1, string map2) {
            var name1 = map1.Substring(0, map1.LastIndexOf('.'));
            var name2 = map2.Substring(0, map2.LastIndexOf('.'));
            return string.Compare(name1, name2, true) == 0;
        }

        [HttpGet]
        public ActionResult StartPoint(int id) {
            var model = new MonitorMapStartPointModel {
                Map = _dao.GetMonitorMap(id),
                Pin = _dao.GetMonitorContent(0)
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult StartPoint(int id, int startX, int startY) {
            var tracked = _dao.GetMonitorMap(id);
            tracked.StartX = startX;
            tracked.StartY = startY;
            _dao.SaveMonitorMap(tracked);
            return RedirectToAction("Edit", new { id = tracked.MonitorSystemId });
        }

        public ActionResult SaveStartPointPosition_Deprecated(int id, int startX, int startY) {
            MonitorMap tracked = null;
            string error = string.Empty;
            try {
                tracked = _dao.GetMonitorMap(id);
                tracked.StartX = startX;
                tracked.StartY = startY;
                _dao.SaveMonitorMap(tracked);
            }
            catch (Exception ex) {
                error = ex.Message;
            }
            return Json(new
            {
                success = string.IsNullOrEmpty(error),
                startX = string.IsNullOrEmpty(error) ? 0 : tracked.StartX,
                startY = string.IsNullOrEmpty(error) ? 0 : tracked.StartY,
                message = error
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
