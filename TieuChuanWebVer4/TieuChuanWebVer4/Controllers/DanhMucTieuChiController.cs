using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class DanhMucTieuChiController : Controller
    {
        // GET: DanhMucTieuChi
        QL_TieuChuan2Entities db = new QL_TieuChuan2Entities();
        public ActionResult Index()
        {
            if (Session["TaiKhoan"] != null)
            {
                ViewBag.cboMaTieuChuan = new SelectList(db.dm_tieuchuan.ToList().OrderBy(n => n.ma_tieuchuan), "ma_tieuchuan", "ma_tieuchuan");
                return View();
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        [ValidateInput(false)]
        public ActionResult DanhMucTieuChiPartial()
        {
                var model = db.dm_tieuchi;
                ViewBag.cboMaTieuChuan = new SelectList(db.dm_tieuchuan.ToList().OrderBy(n => n.ma_tieuchuan), "ma_tieuchuan", "ma_tieuchuan");
                return PartialView("_DanhMucTieuChiPartial", model.OrderByDescending(n => n.ma_tieuchi).ToList());
        }
        public ActionResult SaveNewDocument(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                string txtMaTC = f["txtNew_ma_tieuchi"].ToString();
                string txtTenTC = f["txtNew_ten_tieuchi"].ToString();
                string txtNoiDung = f["txtNew_noidung"].ToString();
                string txtMaTieuChuan = f["txtNew_ma_tieuchuan"].ToString();
                ViewBag.cboMaTieuChuan = new SelectList(db.dm_tieuchuan.ToList().OrderBy(n => n.ma_tieuchuan), "ma_tieuchuan", "ma_tieuchuan");
                //ViewBag.cboMaTieuChuan = db.dm_tieuchuan.ToList().OrderBy(n => n.ten_tieuchuan).Select(i => new { TenTC = i.ten_tieuchuan, MaTC = i.ma_tieuchuan });

                Guid id = System.Guid.NewGuid();
                var model = db.dm_tieuchi;
                dm_tieuchi dm = new dm_tieuchi();
                if (ModelState.IsValid)
                {
                    try
                    {
                        // db.sp_ThemMoiTieuChi(id, txtMaTC, txtTenTC, txtMaTieuChuan, Session["TenNguoiDung"].ToString(), DateTime.Now, txtNoiDung);
                        dm.id = id;
                        dm.ma_tieuchi = txtMaTC;
                        dm.ten_tieuchi = txtTenTC;
                        dm.ma_tieuchuan = txtMaTieuChuan;
                        dm.noidung = txtNoiDung;
                        dm.nguoitao = Session["TenNguoiDung"].ToString();
                        dm.ngaytao = DateTime.Now;
                        db.dm_tieuchi.Add(dm);
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
                return View("Index");
                //return Content("<script type='text/javascript'>setInterval(function(){alert('Lưu thành công !!');window.opener.location.reload(true);},500);</script>");           
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult SaveEditDocument(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                Guid txtId = new Guid(f["txtHiddenId"].ToString());
                string txtMaTC = f["txt_ma_tieuchi"].ToString();
                string txtTenTC = f["txt_ten_tieuchi"].ToString();
                string txtNoiDung = f["txt_noidung"].ToString();
                string txtMaTieuChuan = f["txt_ma_tieuchuan"].ToString();
                ViewBag.cboMaTieuChuan = new SelectList(db.dm_tieuchuan.ToList().OrderBy(n => n.ma_tieuchuan), "ma_tieuchuan", "ma_tieuchuan");
                var model = db.dm_tieuchi;
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.id == txtId);
                        if (modelItem != null)
                        {
                            //db.sp_CapNhatTieuChi(txtId, txtMaTC, txtTenTC, txtMaTieuChuan, Session["TenNguoiDung"].ToString(), DateTime.Now, txtNoiDung);
                            //UpdateModel(modelItem);
                            //db.SaveChanges();
                            modelItem.id = txtId;
                            modelItem.ma_tieuchi = txtMaTC;
                            modelItem.ten_tieuchi = txtTenTC;
                            modelItem.ma_tieuchuan = txtMaTieuChuan;
                            modelItem.noidung = txtNoiDung;
                            modelItem.nguoisua= Session["TenNguoiDung"].ToString();
                            modelItem.ngaysua= DateTime.Now;
                            db.Entry(modelItem).State = EntityState.Modified;
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
                return View("Index");
                //return Content("<script type='text/javascript'>setInterval(function(){alert('Lưu thành công !!');window.reload(true);},500);</script>");
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult Xoa(System.Guid id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.dm_tieuchi;
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
            return RedirectToAction("Index", "DanhMucTieuChi");
        }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
    }
}