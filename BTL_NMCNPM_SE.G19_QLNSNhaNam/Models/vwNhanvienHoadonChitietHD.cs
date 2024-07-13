namespace BTL_NMCNPM_SE.G19_QLNSNhaNam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwNhanvienHoadonChitietHD
    {
        public string sMaHD { get; set; }
        public Nullable<System.DateTime> dNgaylap { get; set; }
        public string sMaNV { get; set; }
        public string sTenNV { get; set; }
        public string sMaSach { get; set; }
        public string sTensach { get; set; }
        public Nullable<int> iSoLuong { get; set; }
        public Nullable<double> fDongia { get; set; }
        public Nullable<double> fThanhtien { get; set; }
    }
}
