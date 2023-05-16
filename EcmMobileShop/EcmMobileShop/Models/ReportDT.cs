using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcmMobileShop.Models
{
    public class ReportDT : tb_HOADON
    {

        [Display(Name = "Total")]
        public double Total { get; set; } = 0 ;

        [Display(Name = "TenKH")]
        public string TenKH { get; set; } = "";

        [Display(Name = "Month")]
        public int Month { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        public void CalculateTotal(IEnumerable<tb_CHITIETHOADON> cthds)
        {
            Total = cthds.Where(cthd => cthd.IdHD == this.IdHD)
                         .Sum(cthd => cthd.SoLuongBan * cthd.GiaBan)
                         .GetValueOrDefault();

            Month = this.NgayLap?.Month ?? 0;
            Year = this.NgayLap?.Year ?? 0;

            TenKH = this.tb_KHACHHANG?.TenKH ?? "";

        }

    }
}