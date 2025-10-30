using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanCaPhe.Models
{
    public class DonHang
    {
        [Key]
        public int MaDH { get; set; }

        [ForeignKey("User")] 
        public int MaKH { get; set; } // Mã khách hàng

        public DateTime NgayDat { get; set; }

        public decimal TongTien { get; set; }

        [Required]
        public int TrangThai { get; set; } // 0=Mới đặt, 1=Đang giao, 2=Đã hoàn thành, 3=Đã hủy

        [StringLength(255)]
        public string DiaChiGiao { get; set; }

        // Mối quan hệ: Một DonHang thuộc Một User
        public virtual User User { get; set; }

        // Mối quan hệ: Một DonHang có Nhiều ChiTietDonHang
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}