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
            if (Session["TaiKhoan"] != null)
            {
                ViewBag.TK = new SelectList(db.ht_dm_nsd.ToList().Where(n => n.ma_nsd != Session["TenDangNhap"].ToString()).OrderBy(n => n.ten_nsd), "ma_nsd", "ten_nsd");
                ViewBag.Check = db.ht_dm_menu.ToList().OrderBy(n => n.ten_menu);
                return View();
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //-------------------------------------- Get dữ liệu CHƯA phân quyền
        public JsonResult getDanhMucPhanQuyen(string ma_nsd)
        {
            List<spDanhMucPhanQuyen_Result> obj = new List<spDanhMucPhanQuyen_Result>();
            var lstDMPQ = db.spDanhMucPhanQuyen(ma_nsd).ToList();
            obj = lstDMPQ;
            // var obj = new { ChuaPhanQuyen = lstDMPQ };
            // return Json(new SelectList(lstDMPQ, "ten_menu", "ma_menu", JsonRequestBehavior.AllowGet));
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        //-------------------------------------- Get dữ liệu ĐÃ phân quyền
        public JsonResult getPhanQuyenNguoiDung(string ma_nsd)
        {
            List<spPhanQuyenNguoiDung_Result> obj = new List<spPhanQuyenNguoiDung_Result>();
            var lstPQND = db.spPhanQuyenNguoiDung(ma_nsd).ToList();
            //return Json(new SelectList(lstPQND, "ten_menu", "ma_menu", JsonRequestBehavior.AllowGet));
            // return Json(new SelectList(lstPQND, "ten_menu", "ma_menu", JsonRequestBehavior.AllowGet));
            obj = lstPQND;
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult CapNhatPhanQuyen(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                string s = "";
                string mansd = f["mansd"].ToString();
                db.sp_XoaPhanQuyen(mansd);
                foreach (var item in f)
                {
                    if (item.ToString() != "DXScript" && item.ToString() != "DXCss" && item.ToString() != "mansd")
                    {
                        var da = db.ht_dm_menu.SingleOrDefault(n => n.ma_menu == item.ToString());
                        db.sp_CapNhatPhanQuyen1(Guid.NewGuid(), mansd, item.ToString(), da.ten_menu.ToString(), da.ma_nhom.ToString());
                    }
                }
                return RedirectToAction("Index", "PhanQuyen");
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
    }
}