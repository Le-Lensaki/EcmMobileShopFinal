using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EcmMobileShop.Models;

namespace EcmMobileShop.Models
{
    public class CartItem : tb_SANPHAM
    {
        public int IdctSP { get; set; }
        public int SoLuong { get; set; }
        public int IdMau { get; set; }
        public double Tien {
            get
            {
                return (double)(SoLuong * Gia);
            }
               
        }
    }
}