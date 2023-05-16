using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcmMobileShop.Models
{
    public class ReportSLB : tb_SANPHAM
    {
        [Display(Name = "Total")]
        public double Total { get; set; } = 0;

        [Display(Name = "Total")]
        public double TotalMoney { get; set; } = 0;

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        public void CalculateTotal(IEnumerable<tb_CHITIETHOADON> cthds)
        {
            Total = cthds.Where(cthd => cthd.tb_CT_SANPHAM.IdSP == this.IdSP)
                         .Sum(cthd => cthd.SoLuongBan)
                         .GetValueOrDefault();

            TotalMoney = cthds.Where(cthd => cthd.tb_CT_SANPHAM.IdSP == this.IdSP)
                         .Sum(cthd => cthd.SoLuongBan * cthd.GiaBan)
                         .GetValueOrDefault();

            var hoaDons = cthds.Select(cthd => cthd.tb_HOADON)
                               .Distinct()
                               .Where(hd => hd.NgayLap != null && hd.NgayLap.Value.Year == this.Year && hd.NgayLap.Value.Month == this.Month);

            Month = this.Month;
            Year = this.Year;

            if (hoaDons.Any())
            {
                var hoaDon = hoaDons.First();
                Month = hoaDon.NgayLap.Value.Month;
                Year = hoaDon.NgayLap.Value.Year;
            }
        }
    }

}