using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
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
            ViewBag.cboMaTieuChuan = new SelectList(db.dm_tieuchuan.ToList().OrderBy(n => n.ma_tieuchuan), "ma_tieuchuan", "ma_tieuchuan");
            return View();
        }
        [ValidateInput(false)]
        public ActionResult DanhMucTieuChiPartial()
        {
            var model = db.dm_tieuchi;
            ViewBag.cboMaTieuChuan = new SelectList(db.dm_tieuchuan.ToList().OrderBy(n => n.ma_tieuchuan), "ma_tieuchuan", "ma_tieuchuan");
            return PartialView("_DanhMucTieuChiPartial", model.OrderByDescending(n => n.ngaytao).ToList());
        }
        public ActionResult SaveNewDocument(FormCollection f)
        {
            string txtMaTC = f["txtNew_ma_tieuchi"].ToString();
            string txtTenTC = f["txtNew_ten_tieuchi"].ToString();
            string txtNoiDung = f["txtNew_noidung"].ToString();
            string txtMaTieuChuan = f["txtNew_ma_tieuchuan"].ToString();
            ViewBag.cboMaTieuChuan = new SelectList(db.dm_tieuchuan.ToList().OrderBy(n => n.ma_tieuchuan), "ma_tieuchuan", "ma_tieuchuan");
            //ViewBag.cboMaTieuChuan = db.dm_tieuchuan.ToList().OrderBy(n => n.ten_tieuchuan).Select(i => new { TenTC = i.ten_tieuchuan, MaTC = i.ma_tieuchuan });

            Guid id = System.Guid.NewGuid();
            var model = db.dm_tieuchi;
            if (ModelState.IsValid)
            {
                try
                {
                    db.sp_ThemMoiTieuChi(id, txtMaTC, txtTenTC, txtMaTieuChuan, "Linh", DateTime.Now, txtNoiDung);
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
        public ActionResult SaveEditDocument(FormCollection f)
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
                        db.sp_CapNhatTieuChi(txtId, txtMaTC, txtTenTC, txtMaTieuChuan, "Linh", DateTime.Now, txtNoiDung);
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
            return View("Index");
            //return Content("<script type='text/javascript'>setInterval(function(){alert('Lưu thành công !!');window.reload(true);},500);</script>");
        }
        public ActionResult Xoa(System.Guid id)
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
    }
}