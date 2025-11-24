using QuanLyQuanCaPhe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace QuanLyQuanCaPhe.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // =========================================
        // 🏠 Trang chủ - Hiển thị một vài sản phẩm nổi bật
        // URL: /Products/Home
        // =========================================
        public ActionResult Home()
        {
            // Lấy 6 sản phẩm mới nhất
            var sanPhams = db.SanPhams
                             .OrderByDescending(sp => sp.MaSP)
                             .Take(6)
                             .ToList();

            return View(sanPhams);
        }

        // =========================================
        // 📦 Trang Danh sách sản phẩm (dssp)
        // URL: /Products/Index hoặc /Products?maLoai=1
        // =========================================
        public ActionResult Index(int? maLoai)
        {
            // Lấy danh sách loại sản phẩm để hiển thị dropdown lọc
            ViewBag.DanhSachLoai = db.LoaiSPs.ToList();

            // Lấy danh sách sản phẩm
            var sanPhams = db.SanPhams.AsQueryable();

            // Nếu có mã loại thì lọc
            if (maLoai.HasValue)
            {
                sanPhams = sanPhams.Where(sp => sp.MaLoai == maLoai.Value);
                ViewBag.MaLoai = maLoai.Value;
            }

            return View(sanPhams.ToList());
        }

        // =========================================
        // 🔍 Trang Chi tiết sản phẩm (sp)
        // URL: /Products/Details/5
        // =========================================
        public ActionResult Details(int id)
        {
            var sp = db.SanPhams.FirstOrDefault(s => s.MaSP == id);
            if (sp == null)
                return HttpNotFound();

            return View(sp);
        }
        // =========================================
        // 🔍 Trang Kết quả Tìm kiếm
        // URL: /Products/Search?q=cafe
        // =========================================
        // =========================================
        // 🔍 Trang Kết quả Tìm kiếm
        // =========================================
        public ActionResult Search(string q)
        {
            // Lấy tất cả sản phẩm
            var sanPhams = db.SanPhams.AsQueryable();

            // 1. Kiểm tra từ khóa (q)
            if (!string.IsNullOrEmpty(q))
            {
                // Xử lý từ khóa để so sánh không phân biệt chữ hoa/thường và xóa khoảng trắng thừa
                string searchQuery = q.Trim().ToLower();

                // 2. Lọc sản phẩm
                sanPhams = sanPhams.Where(sp => sp.TenSP.ToLower().Contains(searchQuery));

                ViewBag.SearchQuery = q;
            }
            else
            {
                // Nếu từ khóa rỗng, trả về danh sách trống
                return View(new List<SanPham>());
            }

            // 3. Trả về kết quả
            // Đảm bảo có ViewBag.DanhSachLoai nếu bạn dùng sidebar lọc trong trang Search
            // ViewBag.DanhSachLoai = db.LoaiSPs.ToList(); 

            return View(sanPhams.ToList());
        }
    }
}