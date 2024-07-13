namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblHoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblHoaDon()
        {
            this.tblChiTietHDs = new HashSet<tblChiTietHD>();
        }
    
        public string sMaHD { get; set; }
        public string sMaNV { get; set; }
        public Nullable<System.DateTime> dNgaylap { get; set; }
        public Nullable<double> fTongHD { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChiTietHD> tblChiTietHDs { get; set; }
        public virtual tblNhanVien tblNhanVien { get; set; }
    }
}
