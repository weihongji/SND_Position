using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Map
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.custom.js",
                        "~/Scripts/jquery-ui.datepicker-zh-CN.js"));

            bundles.Add(new StyleBundle("~/Content/themes/ui-lightness/css").Include(
                        "~/Content/themes/ui-lightness/jquery-ui-1.10.3.custom.css",
                        "~/Content/themes/ui-lightness/jquery-ui-1.10.3.custom.min.css"));
        }
    }
}