using EcmMobileShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class ReportDTController : Controller
    {
        private EcmMobileShopEntities db = new EcmMobileShopEntities();
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/ReportDT
        public ActionResult Index()
        {
            var reportData = db.tb_HOADON.ToList()
                       .Select(hd => new ReportDT
                       {
                           IdHD = hd.IdHD,
                           NgayLap = hd.NgayLap,
                           Total = 0
                       }).ToList();

            var cthds = db.tb_CHITIETHOADON.ToList();

            foreach (var reportItem in reportData)
            {
                reportItem.CalculateTotal(cthds);
            }

            return View(reportData);
        }
    }
}