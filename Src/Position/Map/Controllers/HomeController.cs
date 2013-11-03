using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Map.Models;

namespace Map.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index() {
            var model = new MapMgmtModel();
            model.FengYaList.Add(new FengYa {
                Id = 101,
                Name = "风压机01",
                Power = (decimal)5.5,
                X = 146,
                Y = 333
            });
            model.FengYaList.Add(new FengYa {
                Id = 102,
                Name = "风压机02",
                Power = (decimal)6.5,
                X = 494,
                Y = 237
            });
            model.FengYaList.Add(new FengYa {
                Id = 103,
                Name = "风压机03",
                Power = (decimal)5.5,
                X = 630,
                Y = 199
            });
            return View(model);
        }

        public ActionResult GetPins() {
            return Json(new FengYa(), JsonRequestBehavior.AllowGet);
        }
    }
}
