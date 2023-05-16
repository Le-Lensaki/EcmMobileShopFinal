using EcmMobileShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcmMobileShop.Areas.OneTechAdmin.Controllers
{
    public class HomeAdminController : Controller
    {
        private EcmMobileShopEntities ecmMobile = new EcmMobileShopEntities();
        [Authorize(Roles = "Admin")]
        // GET: OneTechAdmin/Home
        public ActionResult Index()
        {
            var topSellingProducts = ecmMobile.tb_SANPHAM
                         .Join(ecmMobile.tb_CHITIETHOADON,
                               sp => sp.IdSP,
                               cthd => cthd.tb_CT_SANPHAM.IdSP,
                               (sp, cthd) => new { sp, cthd })
                         .GroupBy(x => new { x.sp.IdSP, x.sp.TenSP })
                         .Select(g => new {
                             TenSanPham = g.Key.TenSP,
                             SoLuongBan = g.Sum(x => x.cthd.SoLuongBan),
                             TongTienBan = g.Sum(x => x.cthd.SoLuongBan * x.cthd.GiaBan)
                         })
                         .OrderByDescending(x => x.SoLuongBan)
                         .Take(5)
                         .ToList();



            var modelCollection = new ModelCollection();
            modelCollection.AddModel(topSellingProducts);
            
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult getDataNamNay()
        {
           
                double[] data = new double[12];

            // Lấy danh sách đơn hàng mới từ cơ sở dữ liệu
            var orders = ecmMobile.tb_HOADON
                         .Where(hd => hd.NgayLap != null && hd.NgayLap.Value.Year == DateTime.Now.Year)
                         .ToList();


            // Tính doanh số bán được của từng tháng
            foreach (var order in orders)
                {
                // Tính tổng số tiền bán được của hoá đơn đó
                double total = ecmMobile.tb_CHITIETHOADON
                                .Where(cthd => cthd.IdHD == order.IdHD)
                                .Sum(cthd => cthd.SoLuongBan * cthd.GiaBan)
                                .GetValueOrDefault();


                // Lấy tháng của Ngaylap trong hoá đơn
                int month = order.NgayLap.HasValue ? order.NgayLap.Value.Month : 0;

                // Tính tổng doanh số của tháng đó
                data[month - 1] += total;
                }

                string arrayString = "var dataNamNay = [" + string.Join(",", data) + "];";

                return Content(arrayString);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult getDoanhThu()
        {
            double Tong = 0;

            // Lấy danh sách đơn hàng mới từ cơ sở dữ liệu
            var orders = ecmMobile.tb_HOADON
                         .Where(hd => hd.NgayLap != null)
                         .ToList();

            // Tính doanh số bán được của từng đơn hàng
            foreach (var order in orders)
            {
                // Tính tổng số tiền bán được của hoá đơn đó
                double total = ecmMobile.tb_CHITIETHOADON
                                .Where(cthd => cthd.IdHD == order.IdHD)
                                .Sum(cthd => cthd.SoLuongBan * cthd.GiaBan)
                                .GetValueOrDefault();

                // Cộng tổng doanh thu bán được của đơn hàng đó vào biến Tong
                Tong += total;
            }

            return Content(Tong.ToString());
        }


        [Authorize(Roles = "Admin")]
        public ActionResult getPhanTramDoanhSo()
        {
            // Lấy doanh thu của tháng này
            double doanhThuThangNay = ecmMobile.tb_CHITIETHOADON
            .Where(cthd => cthd.tb_HOADON.NgayLap != null && cthd.tb_HOADON.NgayLap.Value.Year == DateTime.Now.Year && cthd.tb_HOADON.NgayLap.Value.Month == DateTime.Now.Month)
            .Sum(cthd => cthd.SoLuongBan * cthd.GiaBan)
            .GetValueOrDefault();
            // Lấy doanh thu của tháng trước
            DateTime thangTruoc = DateTime.Now.AddMonths(-1);
            double doanhThuThangTruoc = ecmMobile.tb_CHITIETHOADON
                                         .Where(cthd => cthd.tb_HOADON.NgayLap != null && cthd.tb_HOADON.NgayLap.Value.Year == thangTruoc.Year && cthd.tb_HOADON.NgayLap.Value.Month == thangTruoc.Month)
                                         .Sum(cthd => cthd.SoLuongBan * cthd.GiaBan)
                                         .GetValueOrDefault();

            if(doanhThuThangTruoc == 0)
            {
                return Content("100");
            }    
            // Tính phần trăm tăng/giảm so với tháng trước
            double phanTram = (doanhThuThangNay - doanhThuThangTruoc) / doanhThuThangTruoc * 100;

            return Content(phanTram.ToString());
        }





            [Authorize(Roles = "Admin")]
        public ActionResult getDataNamTruoc()
        {
            double[] data = new double[12];

            // Lấy danh sách đơn hàng mới từ cơ sở dữ liệu
            var orders = ecmMobile.tb_HOADON
                         .Where(hd => hd.NgayLap != null && hd.NgayLap.Value.Year == DateTime.Now.Year - 1)
                         .ToList();


            // Tính doanh số bán được của từng tháng
            foreach (var order in orders)
            {
                // Tính tổng số tiền bán được của hoá đơn đó
                double total = ecmMobile.tb_CHITIETHOADON
                                .Where(cthd => cthd.IdHD == order.IdHD)
                                .Sum(cthd => cthd.SoLuongBan * cthd.GiaBan)
                                .GetValueOrDefault();


                // Lấy tháng của Ngaylap trong hoá đơn
                int month = order.NgayLap.HasValue ? order.NgayLap.Value.Month : 0;

                // Tính tổng doanh số của tháng đó
                data[month - 1] += total;
            }

            string arrayString = "var dataNamTruoc = [" + string.Join(",", data) + "];";

            return Content(arrayString);
        }




    }
}