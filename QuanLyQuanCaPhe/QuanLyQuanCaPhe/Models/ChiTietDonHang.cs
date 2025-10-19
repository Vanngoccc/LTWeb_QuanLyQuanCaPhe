using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanCaPhe.Models
{
    public class ChiTietDonHang
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("DonHang")]
        public int MaDH { get; set; }

        [Key] 
        [Column(Order = 2)]
        [ForeignKey("SanPham")]
        public int MaSP { get; set; }

        public int SoLuong { get; set; }

        public decimal DonGia { get; set; } 

        public virtual DonHang DonHang { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
}