﻿using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class NhapThongTinChiTietController : Controller
    {
        private static Guid _iGuid;
        // GET: NhapThongTinChiTiet
        QL_TieuChuan2Entities db = new QL_TieuChuan2Entities();

        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                return View();
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        [ValidateInput(false)]
        public ActionResult NhapThongTinChiTietPartial()
        {
                var model = db.hs_noidung;
                return PartialView("_NhapThongTinChiTietPartial", model.OrderByDescending(n => n.ngaytao).ToList());
        }

        //[HttpPost, ValidateInput(false)]
        //public ActionResult NhapThongTinChiTietPartialAddNew(TieuChuanWebVer4.Models.hs_noidung item)
        //{
        //    var model = db1.hs_noidung;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            model.Add(item);
        //            db1.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    return PartialView("_NhapThongTinChiTietPartial", model.ToList());
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult NhapThongTinChiTietPartialUpdate(TieuChuanWebVer4.Models.hs_noidung item)
        //{
        //    var model = db1.hs_noidung;
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var modelItem = model.FirstOrDefault(it => it.id == item.id);
        //            if (modelItem != null)
        //            {
        //                this.UpdateModel(modelItem);
        //                db1.SaveChanges();
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    else
        //        ViewData["EditError"] = "Please, correct all errors.";
        //    return PartialView("_NhapThongTinChiTietPartial", model.ToList());
        //}
        //[HttpPost, ValidateInput(false)]
        //public ActionResult NhapThongTinChiTietPartialDelete(System.Guid id)
        //{
        //    var model = db1.hs_noidung;
        //    if (id != null)
        //    {
        //        try
        //        {
        //            var item = model.FirstOrDefault(it => it.id == id);
        //            if (item != null)
        //                model.Remove(item);
        //            db1.SaveChanges();
        //        }
        //        catch (Exception e)
        //        {
        //            ViewData["EditError"] = e.Message;
        //        }
        //    }
        //    return PartialView("_NhapThongTinChiTietPartial", model.ToList());
        //}
        //View Thêm mới
        public ActionResult ThemMoi()
        {
            if (Session["TaiKhoan"] != null)
            {
                // var da = db.dm_tieuchuan.SingleOrDefault(n => n.id == id);
                return View();
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //View Chỉnh sửa
        public ActionResult ChinhSua(System.Guid? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var da = db.hs_noidung.SingleOrDefault(n => n.id == id);
                return View(da);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //View Chỉnh sửa
        public ActionResult Xoa(System.Guid id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.hs_noidung;
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
                return RedirectToAction("Index", "DanhMucTieuChuan");
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //-------------------------------------- Hiện lên nội dung trong Word
        public ActionResult SaveNewDocument(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                Guid id = _iGuid;
                string txtMaTC = f["txtNew_ma_tieuchi"].ToString();
                string txtTenTC = f["txtNew_ten_tieuchi"].ToString();
                var doc = DevExpress.Web.Office.DocumentManager.FindDocument("document" + id);
                string richEditString = Encoding.UTF8.GetString(doc.SaveCopy());
                var model = db.hs_noidung;
                hs_noidung item = new hs_noidung();
                if (ModelState.IsValid)
                {
                    try
                    {
                        item.id = id;
                        item.ma_tieuchi = txtMaTC;
                        item.ten_tieuchi = txtTenTC;
                        item.nguoitao = Session["TenNguoiDung"].ToString();
                        item.ngaytao = DateTime.Now;
                        item.noidung = richEditString;
                        db.hs_noidung.Add(item);
                        db.SaveChanges();
                        //db.sp_ThemMoiND(id, txtMaTC, txtTenTC, Session["TenNguoiDung"].ToString(), DateTime.Now, richEditString);
                        //Thread.Sleep(10000);
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";
                return View("Index");
                //return Content("<script type='text/javascript'>setInterval(function(){window.close();alert('Lưu thành công !!');window.opener.location.reload(true);},2000);</script>");
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult SaveEditDocument(Guid id, FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                string txtMaTC = f["txt_ma_tieuchi"].ToString();
                string txtTenTC = f["txt_ten_tieuchi"].ToString();
                var doc = DevExpress.Web.Office.DocumentManager.FindDocument("document" + id);
                string richEditString = Encoding.UTF8.GetString(doc.SaveCopy());
                //string richEditString = Encoding.UTF8.GetString(RichEditExtension.SaveCopy("NoiDung", DocumentFormat.Html));
                var model = db.hs_noidung;
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.id == id);
                        if (modelItem != null)
                        {
                            //db.sp_CapNhatND(id, txtMaTC, txtTenTC, Session["TenNguoiDung"].ToString(), DateTime.Now, richEditString);
                            // Thread.Sleep(10000);
                            modelItem.id = id;
                            modelItem.ma_tieuchi = txtMaTC;
                            modelItem.ten_tieuchi = txtTenTC;
                            modelItem.nguoisua = Session["TenNguoiDung"].ToString();
                            modelItem.ngaysua = DateTime.Now;
                            modelItem.noidung = richEditString;
                            db.Entry(modelItem).State = EntityState.Modified;
                            db.SaveChanges();
                            // db.hs_noidung.Add(modelItem);


                        }
                    }
                    catch (Exception e)
                    {
                        ViewData["EditError"] = e.Message;
                    }
                }
                else
                    ViewData["EditError"] = "Please, correct all errors.";
                return View("Index");
                // return Content("<script type='text/javascript'>setInterval(function(){window.close();alert('Lưu thành công !!');window.opener.location.reload(true);},2000);</script>");
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        public ActionResult NoiDungThemMoiPartial()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.hs_noidung;
                //var x = model.SingleOrDefault(n => n.id == new Guid("D4EF2CE0-72DE-49CD-8BC7-158CBB8CEB3F"));
                hs_noidung x = new hs_noidung();
                x.id = Guid.NewGuid();
                _iGuid = x.id;
                x.noidung = "";
                x.noidungbyte = Encoding.UTF8.GetBytes(x.noidung);
                return PartialView("_NoiDungThemMoiPartial", x);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult NoiDungThemMoi1Partial(System.Guid? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.hs_noidung;
                //var x = model.SingleOrDefault(n => n.id == new Guid("D4EF2CE0-72DE-49CD-8BC7-158CBB8CEB3F"));
                hs_noidung x = new hs_noidung();
                x.id = Guid.NewGuid();
                x.noidung = "";
                x.noidungbyte = Encoding.UTF8.GetBytes(x.noidung);
                return PartialView("_NoiDungThemMoiPartial", x);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //--------------------------------------End Hiện lên nội dung trong Word

        public ActionResult NoiDungPartial(System.Guid? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.hs_noidung;
                //var x = model.SingleOrDefault(n => n.id == new Guid("D4EF2CE0-72DE-49CD-8BC7-158CBB8CEB3F"));
                var x = model.SingleOrDefault(n => n.id == id);
                // byte[] docBytes = RichEditExtension.SaveCopy("RichEditName", DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                byte[] nd = Encoding.UTF8.GetBytes(x.noidung);
                x.noidungbyte = nd;
                //var y = model.SingleOrDefault(n => n.id == (System.Guid)id);
                return PartialView("_NoiDungPartial", x);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //-------------------------------------- 
        public ActionResult Dialog(string dinhdanh, string ma_tieuchi)
        {
            if (Session["TaiKhoan"] != null)
            {
                var da = db.sp_DanhSachTaiLieu(dinhdanh, ma_tieuchi).ToList();
                ViewBag.DinhDanh = dinhdanh.ToString();
                return View(da);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult DanhSachTaiLieuPartial(string dinhdanh, string ma_tieuchi)
        {
            if (Session["TaiKhoan"] != null)
            {
                var da = db.sp_DanhSachTaiLieu(dinhdanh, ma_tieuchi).ToList();
                return PartialView("DanhSachTaiLieuPartial", da);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
    }
}