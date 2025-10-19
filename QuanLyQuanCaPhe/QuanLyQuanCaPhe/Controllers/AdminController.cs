using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace QuanLyQuanCaPhe.Controllers
{
    [Authorize] 
    public class AdminController : Controller
    {
        // [GET] /Admin/Index
        public ActionResult Index()
        {
            if (Session["Role"] == null || (int)Session["Role"] != 1)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập trang này.";
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}