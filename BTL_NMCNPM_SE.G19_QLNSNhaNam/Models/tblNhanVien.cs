//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblNhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblNhanVien()
        {
            this.tblHoaDons = new HashSet<tblHoaDon>();
            this.tblNhaps = new HashSet<tblNhap>();
        }
    
        public string sMaNV { get; set; }
        public string sTenNV { get; set; }
        public Nullable<System.DateTime> dNgaysinh { get; set; }
        public Nullable<bool> bGioitinh { get; set; }
        public string sDiachi { get; set; }
        public Nullable<System.DateTime> dNgayvaolam { get; set; }
        public string sSĐT { get; set; }
        public Nullable<double> fLuong { get; set; }
        public Nullable<bool> bVaitro { get; set; }
        public Nullable<bool> bTrangthai { get; set; }
        public string sCCCD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblHoaDon> tblHoaDons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblNhap> tblNhaps { get; set; }
        public virtual tblTaiKhoan tblTaiKhoan { get; set; }
    }
}
