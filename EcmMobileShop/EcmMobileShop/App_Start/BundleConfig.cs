using System.Web;
using System.Web.Optimization;

namespace EcmMobileShop
{
    public class BundleConfig
    {
        // For more information on bundling, visit https:/go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-{version}.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https:/modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.bundle.min.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/adminlte/plugins/jquery-ui/jquery-ui.min.css",
                      "~/adminlte/plugins/fontawesome-free/css/all.min.css",
                      "~/adminlte/plugins/datatables-bs4/css/dataTables.bootstrap4.min.css",
                     "~/adminlte/plugins/datatables-responsive/css/responsive.bootstrap4.min.css",
                     "~/adminlte/plugins/datatables-buttons/css/buttons.bootstrap4.min.css",
                       "~/adminlte/plugins/overlayScrollbars/css/OverlayScrollbars.min.css",
                       "~/adminlte/plugins/sweetalert2/sweetalert2.css",
                        "~/adminlte/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css",
                        "~/adminlte/plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css",
                        "~/adminlte/plugins/icheck-bootstrap/icheck-bootstrap.min.css",
                        
                        "~/adminlte/plugins/daterangepicker/daterangepicker.css",
                        
                      "~/adminlte/css/adminlte.min.css",
                      "~/Content/Site.css"));

            bundles.Add(new ScriptBundle("~/adminlte/js").Include(
                 "~/adminlte/js/adminlte.min.js",
                 "~/adminlte/plugins/jquery-ui/jquery-ui.min.js",


                 "~/adminlte/plugins/datatables/jquery.dataTables.min.js" ,
                "~/adminlte/plugins/datatables-bs4/js/dataTables.bootstrap4.min.js" ,
                "~/adminlte/plugins/datatables-responsive/js/dataTables.responsive.min.js" ,
                "~/adminlte/plugins/datatables-responsive/js/responsive.bootstrap4.min.js" ,
                "~/adminlte/plugins/datatables-buttons/js/dataTables.buttons.min.js" ,
                "~/adminlte/plugins/datatables-buttons/js/buttons.bootstrap4.min.js" ,
                "~/adminlte/plugins/jszip/jszip.min.js" ,
                "~/adminlte/plugins/pdfmake/pdfmake.min.js" ,
                "~/adminlte/plugins/pdfmake/vfs_fonts.js" ,
                "~/adminlte/plugins/datatables-buttons/js/buttons.html5.min.js" ,
                "~/adminlte/plugins/datatables-buttons/js/buttons.print.min.js" ,
                "~/adminlte/plugins/datatables-buttons/js/buttons.colVis.min.js" ,
                "~/adminlte/plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js",
                "~/adminlte/plugins/sweetalert2/sweetalert2.all.js",
                "~/adminlte/plugins/chart.js/Chart.min.js",
                "~/adminlte/plugins/daterangepicker/daterangepicker.js",
                "~/adminlte/plugins/moment/moment.min.js",
                "~/adminlte/plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js",
                "~/adminlte/js/demo.js"
             ));
        }
    }
}
