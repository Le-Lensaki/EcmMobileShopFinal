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
    
    public partial class tb_HOADON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_HOADON()
        {
            this.tb_CHITIETHOADON = new HashSet<tb_CHITIETHOADON>();
        }
    
        public int IdHD { get; set; }
        public Nullable<int> IdTinhTrangDH { get; set; }
        public Nullable<int> IdKH { get; set; }
        public string DiaChiGiao { get; set; }
        public Nullable<System.DateTime> NgayLap { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_CHITIETHOADON> tb_CHITIETHOADON { get; set; }
        public virtual tb_KHACHHANG tb_KHACHHANG { get; set; }
        public virtual tb_TINHTRANGDH tb_TINHTRANGDH { get; set; }
    }
}