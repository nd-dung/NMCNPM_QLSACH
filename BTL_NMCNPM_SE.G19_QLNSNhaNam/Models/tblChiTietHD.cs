namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblChiTietHD
    {
        public string sMaHD { get; set; }
        public string sMaSach { get; set; }
        public Nullable<int> iSoLuong { get; set; }
        public Nullable<double> fDongia { get; set; }
        public Nullable<double> fThanhtien { get; set; }
    
        public virtual tblHoaDon tblHoaDon { get; set; }
        public virtual tblSach tblSach { get; set; }
    }
}
