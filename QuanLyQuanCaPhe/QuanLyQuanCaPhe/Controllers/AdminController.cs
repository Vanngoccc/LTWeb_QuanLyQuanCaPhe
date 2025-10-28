using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyQuanCaPhe.Models;

namespace QuanLyQuanCaPhe.Controllers
{
    [Authorize] 
    public class AdminController : Controller
    {
        // [GET] /Admin/Index
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Session["Role"] == null || (int)Session["Role"] != 1)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index", "Home");
            }
            var donHangHoanThanh = db.DonHangs.Where(dh => dh.TrangThai == 2);

            decimal tongDoanhThu = donHangHoanThanh.Any() ? donHangHoanThanh.Sum(dh => dh.TongTien) : 0;

            DateTime homNay = DateTime.Today;
            decimal doanhThuHomNay = donHangHoanThanh
                .Where(dh => dh.NgayDat >= homNay)
                .Sum(dh => (decimal?)dh.TongTien) ?? 0; 

            int donHangMoi = db.DonHangs.Count(dh => dh.TrangThai == 0);

            int tongKhachHang = db.Users.Count(u => u.Role == 0);

            ViewBag.TongDoanhThu = tongDoanhThu;
            ViewBag.DoanhThuHomNay = doanhThuHomNay;
            ViewBag.DonHangMoi = donHangMoi;
            ViewBag.TongKhachHang = tongKhachHang;
            return View();
        }
    }
}