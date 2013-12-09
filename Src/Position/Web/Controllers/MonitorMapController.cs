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

        public ActionResult Index() {
            var model = new MonitorMapIndexModel {
                MonitorSystems = _dao.GetMonitorSystems()
            };
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit([Bind(Prefix = "id")] int systemId) {
            var model = new MonitorMapModel {
                System = _dao.GetMonitorSystem(systemId),
                MapNames = new List<string> { "小地图", "中地图", "大地图" }
            };

            var maps = _dao.GetMonitorMaps(systemId);
            var map = maps.SingleOrDefault(x => x.SizeType == (int)MapSize.Small);
            model.Maps.Add(map ?? new MonitorMap { MonitorSystemId = systemId, DisplayName = "尚未设置", Scale=10000, SizeType = (int)MapSize.Small });

            map = maps.SingleOrDefault(x => x.SizeType == (int)MapSize.Medim);
            model.Maps.Add(map ?? new MonitorMap { MonitorSystemId = systemId, DisplayName = "尚未设置", Scale = 10000, SizeType = (int)MapSize.Medim });

            map = maps.SingleOrDefault(x => x.SizeType == (int)MapSize.Large);
            model.Maps.Add(map ?? new MonitorMap { MonitorSystemId = systemId, DisplayName = "尚未设置", Scale = 10000, SizeType = (int)MapSize.Large });

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "id")] int systemId, int[] StartX, int[] StartY, int[] Scale, HttpPostedFileBase[] MapFile) {
            var mapList = _dao.GetMonitorMaps(systemId);
            for (int i = 0; i < 3; i++) {
                SaveMap(mapList, systemId, StartX[i], StartY[i], Scale[i], (MapSize)(i + 1), MapFile[i]);
            }

            return RedirectToAction("Edit", new { id = systemId });
        }

        private int SaveMap(List < MonitorMap > mapList, int systemId, int startX, int startY, int scale, MapSize size, HttpPostedFileBase uploadedFile) {
            var tracked = mapList.FirstOrDefault(x => x.MonitorSystemId == systemId && x.SizeType == (int)size);
            if (tracked == null) {
                tracked = new MonitorMap {
                    MonitorSystemId = systemId,
                    SizeType = (int)size,
                    Name = "",
                    DisplayName = ""
                };
            }
            tracked.StartX= startX;
            tracked.StartY = startY;
            tracked.Scale = scale;

            if (uploadedFile != null && uploadedFile.ContentLength > 0) {
                var ext = Path.GetExtension(uploadedFile.FileName);
                var mapName = string.Format("Map_{0}_{1}{2}", systemId, (int)size, ext);
                var displayName = Path.GetFileName(uploadedFile.FileName);
                var filePath = "";
                if (!string.IsNullOrEmpty(tracked.Name)) {
                    filePath = Path.Combine(HttpContext.Server.MapPath("~/Images"), tracked.Name);
                    if (System.IO.File.Exists(filePath)) {
                        System.IO.File.Delete(filePath);
                    }
                }

                filePath = Path.Combine(HttpContext.Server.MapPath("~/Images"), mapName);
                uploadedFile.SaveAs(filePath);

                tracked.Name = mapName;
                tracked.DisplayName = displayName;
            }
            return _dao.SaveMonitorMap(tracked);
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
    }
}
