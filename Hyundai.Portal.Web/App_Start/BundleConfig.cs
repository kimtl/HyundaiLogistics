using System.Web;
using System.Web.Optimization;
using System;

namespace Hyundai
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo/2014.1.318/kendoScript").Include(
                        "~/Scripts/kendo/2014.1.318/kendo.web.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/jquery.validate.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                        "~/Scripts/common.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                "~/Content/themes/base/jquery-ui.css"));
            bundles.Add(new StyleBundle("~/Content/kendo/2014.1.318/kendoCss").Include("~/Content/kendo/2014.1.318/kendo.common-bootstrap.min.css",
                "~/Content/kendo/2014.1.318/kendo.bootstrap.min.css"));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap.lumen.css"));
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList != null)
            {
                ignoreList.Clear();
                ignoreList.Ignore("*.intellisense.js");
                ignoreList.Ignore("*-vsdoc.js");
                ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
                //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
                //signoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
                return;
            }
            else
            {
                throw new ArgumentNullException("ignoreList");
            }
        }
    }
}