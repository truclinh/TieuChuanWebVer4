using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        QL_TieuChuan2Entities db = new QL_TieuChuan2Entities();
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View();
        }
        public ActionResult MenuPartial(string ma_nsd)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.ht_nsd_menu.Where(n => n.ma_nsd == ma_nsd).ToList();
                return PartialView("_MenuPartial", model);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
    }
}