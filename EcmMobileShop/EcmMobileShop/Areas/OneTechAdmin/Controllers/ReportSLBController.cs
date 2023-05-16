using EcmMobileShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class ReportSLBController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/ReportSLB
        public ActionResult Index()
        {
            var reportData = db.tb_SANPHAM.ToList()
                                   .Select(sp => new ReportSLB
                                   {
                                       IdSP = sp.IdSP,
                                       TenSP = sp.TenSP,
                                       Total = 0,
                                       TotalMoney = 0,
                                       Month = DateTime.Now.Month,
                                       Year = DateTime.Now.Year
                                   }).ToList();

            var cthds = db.tb_CHITIETHOADON.ToList();

            foreach (var reportItem in reportData)
            {
                reportItem.CalculateTotal(cthds);
            }
            var filteredReportData = reportData.Where(rd => rd.Total > 0);

            return View(filteredReportData);
        }

    }
}