//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Team9_winxshop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            this.ChiTietDonHangs = new HashSet<ChiTietDonHang>();
            this.ChiTietThanhToans = new HashSet<ChiTietThanhToan>();
        }
    
        public int MaDH { get; set; }
        public string Email { get; set; }
        public int MaTT { get; set; }
        public string DiaChiNguoiNhan { get; set; }
        public string SDT { get; set; }
        public int TongTien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietThanhToan> ChiTietThanhToans { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual TrangThaiDonHang TrangThaiDonHang { get; set; }
    }
}
