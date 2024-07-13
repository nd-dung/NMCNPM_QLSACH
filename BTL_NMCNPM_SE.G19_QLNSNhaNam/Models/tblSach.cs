namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblSach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblSach()
        {
            this.tblChiTietHDs = new HashSet<tblChiTietHD>();
            this.tblChiTietNhaps = new HashSet<tblChiTietNhap>();
        }
    
        public string sMasach { get; set; }
        public string sTensach { get; set; }
        public string sTenTG { get; set; }
        public Nullable<int> iDongia { get; set; }
        public Nullable<int> iSoluong { get; set; }
        public string sNXB { get; set; }
        public string sTheloai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChiTietHD> tblChiTietHDs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblChiTietNhap> tblChiTietNhaps { get; set; }
    }
}
