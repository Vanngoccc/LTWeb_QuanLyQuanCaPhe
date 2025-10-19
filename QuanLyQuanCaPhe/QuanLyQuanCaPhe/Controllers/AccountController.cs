using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyQuanCaPhe.Models; 
using System.Web.Mvc;
using System.Web.Security;

namespace QuanLyQuanCaPhe.Controllers
{
    public class AccountController : Controller
    {
        // [GET] HIỂN THỊ Đăng Kí
        private ApplicationDbContext _context;
        public AccountController()
        {
            _context = new ApplicationDbContext(); 
        }
        public ActionResult Register()
        {
            return View();
        }

        // [POST] XỬ LÍ Đăng Kí
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var exitstingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (exitstingUser != null)
                {
                    ModelState.AddModelError("Email", "Email đã được sử dụng.");
                    return View(user);
                }
                user.MatKhau = System.Web.Helpers.Crypto.HashPassword(user.MatKhau);
                user.Role = 0;

                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        // [GET] HIỂN THỊ Đăng Nhập
        public ActionResult Login()
        {
            return View();
        }
        // [POST] XỬ LÍ Đăng Nhập
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user != null && System.Web.Helpers.Crypto.VerifyHashedPassword(user.MatKhau, model.MatKhau))
                {
                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    Session["UserID"] = user.UserID;
                    Session["HoTen"] = user.HoTen;
                    Session["Role"] = user.Role;
                    if ((int)Session["Role"] == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "Email hoặc Mật khẩu không đúng.");
            }
            return View(model);
        }
        // [GET] Đăng Xuất
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home"); 
        }
    }
}