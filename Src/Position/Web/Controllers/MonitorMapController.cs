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
                System = _dao.GetMonitorSystem(id)
            };
            var maps = _dao.GetMonitorMaps(id);
            model.SmallMap = maps.SingleOrDefault(x => x.Scale == (int)MapScale.Small);
            model.MedimMap = maps.SingleOrDefault(x => x.Scale == (int)MapScale.Medim);
            model.LargeMap = maps.SingleOrDefault(x => x.Scale == (int)MapScale.Large);
            return View(model);
        }

        public ActionResult Upload(int id, HttpPostedFileBase smallMap, HttpPostedFileBase medimMap, HttpPostedFileBase largeMap) {
            SaveMap(id, smallMap, MapScale.Small);
            SaveMap(id, medimMap, MapScale.Medim);
            SaveMap(id, largeMap, MapScale.Large);

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
    }
}
