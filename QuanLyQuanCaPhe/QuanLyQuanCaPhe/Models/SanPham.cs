using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace QuanLyQuanCaPhe.Models
{
    public class SanPham
    {
        [Key]
        public int MaSP { get; set; }

        [Required]
        [StringLength(200)]
        public string TenSP { get; set; }

        public decimal GiaBan { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; } 

        public string MoTaChiTiet { get; set; }

        [ForeignKey("LoaiSP")]
        public int MaLoai { get; set; }

        public virtual LoaiSP LoaiSP { get; set; }
    }
}

