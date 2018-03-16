using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class ThongTinBoMonController : Controller
    {
        // GET: ThongTinBoMon
        QL_TieuChuan2Entities db = new QL_TieuChuan2Entities();
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                TieuChuanDTO tc = new TieuChuanDTO();
                var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
                ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
                ViewBag.DMKhoa1 = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");
                return View();
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        [ValidateInput(false)]
        public ActionResult ThongTinBoMonPartial()
        {
            if (Session["TaiKhoan"] != null)
            {
                TieuChuanDTO tc = new TieuChuanDTO();
                var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
                ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
                ViewBag.DMKhoa1 = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");
                var model = db.dm_bomon;
                return PartialView("_ThongTinBoMonPartial", model.ToList());
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        //[HttpPost, ValidateInput(false)]
        //public ActionResult ThongTinBoMonPartialAddNew(TieuChuanWeb2.Models.dm_bomon item)
        //{
        //    if (Session["TaiKhoan"] != null)
        //    {
        //        var model = db.dm_bomon;
        //        TieuChuanDTO tc = new TieuChuanDTO();
        //        var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
        //        ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                item.id = Guid.NewGuid();
        //                model.Add(item);
        //                db.SaveChanges();
        //            }
        //            catch (Exception e)
        //            {
        //                ViewData["EditError"] = e.Message;
        //            }
        //        }
        //        else
        //            ViewData["EditError"] = "Please, correct all errors.";
        //        return PartialView("_ThongTinBoMonPartial", model.ToList());
        //    }
        //    return RedirectToAction("DangNhap", "TaiKhoan");
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult ThongTinBoMonPartialUpdate(TieuChuanWeb2.Models.dm_bomon item)
        //{
        //    if (Session["TaiKhoan"] != null)
        //    {
        //        var model = db.dm_bomon;
        //        TieuChuanDTO tc = new TieuChuanDTO();
        //        var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
        //        ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                var modelItem = model.FirstOrDefault(it => it.id == item.id);
        //                if (modelItem != null)
        //                {
        //                    UpdateModel(modelItem);
        //                    db.SaveChanges();
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                ViewData["EditError"] = e.Message;
        //            }
        //        }
        //        else
        //            ViewData["EditError"] = "Please, correct all errors.";
        //        return PartialView("_ThongTinBoMonPartial", model.ToList());
        //    }
        //    return RedirectToAction("DangNhap", "TaiKhoan");
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult ThongTinBoMonPartialDelete(System.Guid id)
        //{
        //    if (Session["TaiKhoan"] != null)
        //    {
        //        var model = db.dm_bomon;
        //        TieuChuanDTO tc = new TieuChuanDTO();
        //        var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
        //        ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
        //        if (id != null)
        //        {
        //            try
        //            {
        //                var item = model.FirstOrDefault(it => it.id == id);
        //                if (item != null)
        //                    model.Remove(item);
        //                db.SaveChanges();
        //            }
        //            catch (Exception e)
        //            {
        //                ViewData["EditError"] = e.Message;
        //            }
        //        }
        //        return PartialView("_ThongTinBoMonPartial", model.ToList());
        //    }
        //    return RedirectToAction("DangNhap", "TaiKhoan");
        //}
        public ActionResult SaveNewDocument(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                string txtMaBM = f["txtNew_mabomon"].ToString();
                string txtTenBM = f["txtNew_tenbomon"].ToString();
                string txtMaKhoa = f["txtNew_makhoa"].ToString();
                string txtGoogleDrive = f["txtNew_googledrive"].ToString();

                ViewBag.DMKhoa1 = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");

                Guid id = System.Guid.NewGuid();
                var model = db.dm_bomon;
                if (ModelState.IsValid)
                {
                    try
                    {
                        db.sp_ThemMoiThongTinBM(id, txtMaBM, txtTenBM, txtMaKhoa, txtGoogleDrive);
                        //model.Add(item);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";
                return RedirectToAction("Index", "ThongTinBoMon");
                //return Content("<script type='text/javascript'>setInterval(function(){alert('Lưu thành công !!');window.opener.location.reload(true);},500);</script>");           
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult SaveEditDocument(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                Guid txtId = new Guid(f["txtHiddenId"].ToString());
                string txtMaBM = f["txt_mabomon"].ToString();
                string txtTenBM = f["txt_tenbomon"].ToString();
                string txtMaKhoa = f["txt_makhoa"].ToString();
                string txtGoogleDrive = f["txt_googledrive"].ToString();

                ViewBag.DMKhoa1 = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");

                var model = db.dm_bomon;
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.id == txtId);
                        if (modelItem != null)
                        {
                            db.sp_CapNhatThongTinBM(txtId, txtMaBM, txtTenBM, txtMaKhoa, txtGoogleDrive);
                            //UpdateModel(modelItem);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";
                return RedirectToAction("Index", "ThongTinBoMon");
                //return Content("<script type='text/javascript'>setInterval(function(){alert('Lưu thành công !!');window.reload(true);},500);</script>");
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult Xoa(System.Guid id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.dm_bomon;
                if (id != null)
                {
                    try
                    {
                        var item = model.FirstOrDefault(it => it.id == id);
                        if (item != null)
                            model.Remove(item);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                return RedirectToAction("Index", "ThongTinBoMon");
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
    }
}