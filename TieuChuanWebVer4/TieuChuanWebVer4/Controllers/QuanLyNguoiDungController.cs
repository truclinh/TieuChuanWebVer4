using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        // GET: QuanLyNguoiDung
        QL_TieuChuan2Entities db1 = new QL_TieuChuan2Entities();
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                TieuChuanDTO tc = new TieuChuanDTO();
                var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
                var lstBoMon = tc.getAllBoMon().OrderBy(n => n.mabomon);
                ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
                ViewBag.DMBoMon = lstBoMon.Select(i => new { TenBM = i.tenbomon, MaBM = i.mabomon });
                ViewBag.DMKhoa1 = new SelectList(db1.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");
                ViewBag.DMBoMon1 = new SelectList(db1.dm_bomon.ToList().OrderBy(n => n.mabomon), "mabomon", "tenbomon");
                return View();
            }
            return RedirectToAction("DangNhap", "TaiKhoan");

        }
        [ValidateInput(false)]
        public ActionResult QuanLyNguoiDungPartial()
        {
            if (Session["TaiKhoan"] != null)
            {
                TieuChuanDTO tc = new TieuChuanDTO();
                var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
                var lstBoMon = tc.getAllBoMon().OrderBy(n => n.mabomon);
                ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
                ViewBag.DMBoMon = lstBoMon.Select(i => new { TenBM = i.tenbomon, MaBM = i.mabomon });
                var model = db1.ht_dm_nsd;
                return PartialView("_QuanLyNguoiDungPartial", model.OrderByDescending(n => n.ngaytao).ToList());
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        //[HttpPost, ValidateInput(false)]
        //public ActionResult QuanLyNguoiDungPartialAddNew(ht_dm_nsd item)
        //{
        //    if (Session["TaiKhoan"] != null)
        //    {
        //        TieuChuanDTO tc = new TieuChuanDTO();
        //        var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
        //        var lstBoMon = tc.getAllBoMon().OrderBy(n => n.mabomon);
        //        //ViewBag.DMKhoa = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");
        //        // ViewBag.DMBoMon = new SelectList(db.dm_bomon.ToList().OrderBy(n => n.mabomon), "mabomon", "tenbomon");
        //        ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
        //        ViewBag.DMBoMon = lstBoMon.Select(i => new { TenBM = i.tenbomon, MaBM = i.mabomon });

        //        var model = db1.ht_dm_nsd;
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                //item.matkhau = Encryptor.MDSHash(item.matkhau);
        //                item.id = Guid.NewGuid();
        //                item.nguoitao = Session["TenDangNhap"].ToString();
        //                item.ngaytao = System.DateTime.Now;
        //                model.Add(item);
        //                db1.SaveChanges();
        //            }
        //            catch (Exception e)
        //            {
        //                ViewData["EditError"] = e.Message;
        //            }
        //        }
        //        else
        //            ViewData["EditError"] = "Please, correct all errors.";
        //        return PartialView("_QuanLyNguoiDungPartial", model.OrderByDescending(n => n.ngaytao).ToList());
        //    }
        //    return RedirectToAction("DangNhap", "TaiKhoan");
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult QuanLyNguoiDungPartialUpdate(ht_dm_nsd item)
        //{
        //    if (Session["TaiKhoan"] != null)
        //    {
        //        TieuChuanDTO tc = new TieuChuanDTO();
        //        var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
        //        var lstBoMon = tc.getAllBoMon().OrderBy(n => n.mabomon);
        //        //ViewBag.DMKhoa = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");
        //        // ViewBag.DMBoMon = new SelectList(db.dm_bomon.ToList().OrderBy(n => n.mabomon), "mabomon", "tenbomon");
        //        ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
        //        ViewBag.DMBoMon = lstBoMon.Select(i => new { TenBM = i.tenbomon, MaBM = i.mabomon });

        //        var model = db1.ht_dm_nsd;
        //        if (ModelState.IsValid)
        //        {
        //            try
        //            {
        //                var modelItem = model.FirstOrDefault(it => it.id == item.id);
        //                if (modelItem != null)
        //                {
        //                    //modelItem.matkhau = Encryptor.MDSHash(modelItem.matkhau);
        //                    modelItem.nguoisua = Session["TenDangNhap"].ToString();
        //                    modelItem.ngaysua = System.DateTime.Now;
        //                    UpdateModel(modelItem);
        //                    db1.SaveChanges();
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                ViewData["EditError"] = e.Message;
        //            }
        //        }
        //        else
        //            ViewData["EditError"] = "Please, correct all errors.";
        //        return PartialView("_QuanLyNguoiDungPartial", model.OrderByDescending(n => n.ngaytao).ToList());
        //    }
        //    return RedirectToAction("DangNhap", "TaiKhoan");
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult QuanLyNguoiDungPartialDelete(System.Guid id)
        //{
        //    if (Session["TaiKhoan"] != null)
        //    {
        //        TieuChuanDTO tc = new TieuChuanDTO();
        //        var lstKhoa = tc.getAllKhoa().OrderBy(n => n.makhoa);
        //        var lstBoMon = tc.getAllBoMon().OrderBy(n => n.mabomon);
        //        //ViewBag.DMKhoa = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");
        //        // ViewBag.DMBoMon = new SelectList(db.dm_bomon.ToList().OrderBy(n => n.mabomon), "mabomon", "tenbomon");
        //        ViewBag.DMKhoa = lstKhoa.Select(i => new { TenKhoa = i.tenkhoa, MaKhoa = i.makhoa });
        //        ViewBag.DMBoMon = lstBoMon.Select(i => new { TenBM = i.tenbomon, MaBM = i.mabomon });

        //        var model = db1.ht_dm_nsd;
        //        if (id != null)
        //        {
        //            try
        //            {
        //                var item = model.FirstOrDefault(it => it.id == id);
        //                if (item != null)
        //                    model.Remove(item);
        //                db1.SaveChanges();
        //            }
        //            catch (Exception e)
        //            {
        //                ViewData["EditError"] = e.Message;
        //            }
        //        }
        //        return PartialView("_QuanLyNguoiDungPartial", model.OrderByDescending(n => n.ngaytao).ToList());
        //    }
        //    return RedirectToAction("DangNhap", "TaiKhoan");
        //}
        public ActionResult SaveNewDocument(FormCollection f)
        {
            string txtMaNSD = f["txtNew_ma_nsd"].ToString();
            string txtTenNSD = f["txtNew_ten_nsd"].ToString();
            string txtMatKhau = f["txtNew_matkhau"].ToString();
            string txtGhiChu = f["txtNew_ghichu"].ToString();
            string txtMaKhoa = f["txtNew_makhoa"].ToString();
            string txtMaBM = f["txtNew_mabomon"].ToString();
            string txtMaNhom = f["txtNew_manhom"].ToString();

            ViewBag.DMKhoa1 = new SelectList(db1.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");
            ViewBag.DMBoMon1 = new SelectList(db1.dm_bomon.ToList().OrderBy(n => n.mabomon), "mabomon", "tenbomon");

            Guid id = System.Guid.NewGuid();
            //  var model = db1.dm_tieuchi;
            if (ModelState.IsValid)
            {
                try
                {
                    db1.sp_ThemMoiQLNguoiDung(id, txtMaNSD, txtTenNSD, txtMatKhau, txtMaKhoa, txtMaBM, txtMaNhom, txtGhiChu, "Linh", DateTime.Now);
                    //model.Add(item);
                    db1.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return RedirectToAction("Index", "QuanLyNguoiDung");
            //return Content("<script type='text/javascript'>setInterval(function(){alert('Lưu thành công !!');window.opener.location.reload(true);},500);</script>");           
        }
        public ActionResult SaveEditDocument(FormCollection f)
        {
            Guid txtId = new Guid(f["txtHiddenId"].ToString());
            string txtMaNSD = f["txt_ma_nsd"].ToString();
            string txtTenNSD = f["txt_ten_nsd"].ToString();
            string txtMatKhau = f["txt_matkhau"].ToString();
            string txtGhiChu = f["txt_ghichu"].ToString();
            string txtMaKhoa = f["txt_makhoa"].ToString();
            string txtMaBM = f["txt_mabomon"].ToString();
            string txtMaNhom = f["txt_manhom"].ToString();

            ViewBag.DMKhoa1 = new SelectList(db1.dm_khoa.ToList().OrderBy(n => n.makhoa), "makhoa", "tenkhoa");
            ViewBag.DMBoMon1 = new SelectList(db1.dm_bomon.ToList().OrderBy(n => n.mabomon), "mabomon", "tenbomon");

            var model = db1.ht_dm_nsd;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.id == txtId);
                    if (modelItem != null)
                    {
                        db1.sp_CapNhatQLNguoiDung(txtId, txtMaNSD, txtTenNSD, txtMatKhau, txtMaKhoa, txtMaBM, txtMaNhom, txtGhiChu, "Linh", DateTime.Now);
                        //UpdateModel(modelItem);
                        db1.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return RedirectToAction("Index", "QuanLyNguoiDung");
            //return Content("<script type='text/javascript'>setInterval(function(){alert('Lưu thành công !!');window.reload(true);},500);</script>");
        }
        public ActionResult Xoa(System.Guid id)
        {
            var model = db1.ht_dm_nsd;
            if (id != null)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.id == id);
                    if (item != null)
                        model.Remove(item);
                    db1.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return RedirectToAction("Index", "QuanLyNguoiDung");
        }
        //public ActionResult SaveDocument()
        //{
        //    string FileName = "test1";
        //    var path = System.Web.HttpContext.Current.Server.MapPath(string.Format("{0}/{1}_{2}", DevExpress.XtraRichEdit.DocumentFormat.Html, FileName, ".doc"));
        //    byte[] lbRTF = null;
        //    lbRTF = RichEditExtension.SaveCopy("RichEditName", DevExpress.XtraRichEdit.DocumentFormat.Html);
        //    System.IO.File.WriteAllBytes(path, lbRTF);
        //    return View("Index");
        //}
        //public ActionResult SaveDocument(byte[] bytes) {
        //    dm_tieuchuan dm = new dm_tieuchuan();
        //    var model =db1.dm_tieuchuan;
        //    var da = model.FirstOrDefault(n => n.noidungbyte == bytes&& n.id=="");
        //    UpdateModel(da);
        //    db1.SaveChanges();
        //    return View("Index");
        //}

 
    }
}