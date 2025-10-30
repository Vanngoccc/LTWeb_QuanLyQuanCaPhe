using System;
using System.Linq;
using System.Web.Mvc;
using QuanLyQuanCaPhe.Models;

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
    }
}
