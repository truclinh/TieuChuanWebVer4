using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class PhanQuyenController : Controller
    {
        // GET: PhanQuyen
        QL_TieuChuan2Entities db = new QL_TieuChuan2Entities();
        public ActionResult Index()
        {
            ViewBag.TK = new SelectList(db.ht_dm_nsd.ToList().OrderBy(n => n.ten_nsd), "ma_nsd", "ten_nsd");
            return View();
        }
        public JsonResult getDanhMucPhanQuyen(string ma_nsd)
        {
            var lstDMPQ = db.spDanhMucPhanQuyen(ma_nsd);
            return Json(new SelectList(lstDMPQ, "ten_menu", "ma_menu", JsonRequestBehavior.AllowGet));
        }
        public JsonResult getPhanQuyenNguoiDung(string ma_nsd)
        {
            var lstPQND = db.spPhanQuyenNguoiDung(ma_nsd);
            return Json(new SelectList(lstPQND, "ten_menu", "ma_menu", JsonRequestBehavior.AllowGet));
        }
    }
}