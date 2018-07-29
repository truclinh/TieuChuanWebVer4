using DevExpress.Web.Mvc;
using DevExpress.XtraRichEdit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class DanhMucTieuChuanController : Controller
    {
        private static Guid iGuid;
        // GET: DanhMucTieuChuan
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
        public ActionResult DanhMucTieuChuanPartial()
        {
            var model = db.dm_tieuchuan;
            return PartialView("_DanhMucTieuChuanPartial", model.OrderByDescending(n => n.ma_tieuchuan).ToList());
        }

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
                var da = db.dm_tieuchuan.SingleOrDefault(n => n.id == id);
                return View(da);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //View Chỉnh sửa
        public ActionResult Xoa(System.Guid id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.dm_tieuchuan;
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
                //Guid id = (Guid)Session["id"];
                Guid id = iGuid;
                string txtMaTC = f["txtNew_ma_tieuchuan"].ToString();
                string txtTenTC = f["txtNew_ten_tieuchuan"].ToString();
                //Guid id = System.Guid.NewGuid();
                var doc = DevExpress.Web.Office.DocumentManager.FindDocument("document" + id);
                string richEditString = Encoding.UTF8.GetString(doc.SaveCopy());
                var model = db.dm_tieuchuan;

                dm_tieuchuan item = new dm_tieuchuan();
                if (ModelState.IsValid)
                {
                    try
                    {

                        //db.sp_ThemMoiTieuChuan(id, txtMaTC, txtTenTC, Session["TenNguoiDung"].ToString(), DateTime.Now, richEditString);
                        item.id = id;
                        item.ma_tieuchuan = txtMaTC;
                        item.ten_tieuchuan = txtTenTC;
                        item.nguoitao = Session["TenNguoiDung"].ToString();
                        item.ngaytao = DateTime.Now;
                        item.noidung = richEditString;
                        db.dm_tieuchuan.Add(item);
                        db.SaveChanges();
                        //Thread.Sleep(10000);
                        //model.Add(item);
                        // db.SaveChanges();
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
                string txtMaTC = f["txt_ma_tieuchuan"].ToString();
                string txtTenTC = f["txt_ten_tieuchuan"].ToString();
                var doc = DevExpress.Web.Office.DocumentManager.FindDocument("document" + id);
                string richEditString = Encoding.UTF8.GetString(doc.SaveCopy());
                //string richEditString = Encoding.UTF8.GetString(RichEditExtension.SaveCopy("NoiDung", DocumentFormat.Html));

                var model = db.dm_tieuchuan;
                if (ModelState.IsValid)
                {
                    try
                    {
                        var modelItem = model.FirstOrDefault(it => it.id == id);
                        if (modelItem != null)
                        {
                            //db.sp_CapNhatTieuChuan(id, txtMaTC, txtTenTC, Session["TenNguoiDung"].ToString(), DateTime.Now, richEditString);
                            //Thread.Sleep(10000);
                            //Guid _id = id;
                           // dm_tieuchuan dm = new dm_tieuchuan();

                            modelItem.ma_tieuchuan = txtMaTC;
                            modelItem.ten_tieuchuan = txtTenTC;
                            modelItem.noidung = richEditString;
                            modelItem.nguoisua = Session["TenNguoiDung"].ToString();
                            modelItem.ngaysua = DateTime.Now;
                            db.Entry(modelItem).State = EntityState.Modified;
                           // UpdateModel(modelItem);
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
                // return Content("<script type='text/javascript'>setInterval(function(){window.close();alert('Lưu thành công !!');window.opener.location.reload(true);},2000);</script>");
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        public ActionResult NoiDungPartial(System.Guid? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.dm_tieuchuan;
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

        public ActionResult NoiDungThemMoiPartial()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.dm_tieuchuan;
                //var x = model.SingleOrDefault(n => n.id == new Guid("D4EF2CE0-72DE-49CD-8BC7-158CBB8CEB3F"));
                dm_tieuchuan x = new dm_tieuchuan();
                x.id = Guid.NewGuid();
                // Session["id"] = x.id;
                iGuid = x.id;
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
                var model = db.dm_tieuchuan;
                //var x = model.SingleOrDefault(n => n.id == new Guid("D4EF2CE0-72DE-49CD-8BC7-158CBB8CEB3F"));
                dm_tieuchuan x = new dm_tieuchuan();
                x.id = Guid.NewGuid();
                x.noidung = "";
                x.noidungbyte = Encoding.UTF8.GetBytes(x.noidung);
                return PartialView("_NoiDungThemMoiPartial", x);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //--------------------------------------End Hiện lên nội dung trong Word
        //public ActionResult DocumentPartial(string box)
        //{
        //    var da=db.hs_noidung.Where(n.)
        //    return PartialView("_DocumentPartial")
        //}
    }
}