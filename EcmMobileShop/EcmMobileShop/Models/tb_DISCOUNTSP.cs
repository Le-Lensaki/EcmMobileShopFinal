//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EcmMobileShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_DISCOUNTSP
    {
        public int IdDCSP { get; set; }
        public int IdSP { get; set; }
        public double GiaTri { get; set; }
        public Nullable<System.DateTime> NgayHH { get; set; }
        public bool TinhTrang { get; set; }
    
        public virtual tb_SANPHAM tb_SANPHAM { get; set; }
    }
}
