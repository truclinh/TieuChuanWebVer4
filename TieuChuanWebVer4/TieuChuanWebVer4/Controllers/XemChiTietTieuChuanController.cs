using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class XemChiTietTieuChuanController : Controller
    {
        // GET: XemChiTietTieuChuan
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
        public ActionResult TreeListPartial()
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = (from g in db.sp_DuLieuGoc()
                             join ct in db.sp_DuLieuChiTiet()
                             on g.ma_nhom equals ct.ma_tieuchi
                             select new
                             {
                                 nhom = g.nhom,
                                 ma_nhom = g.ma_nhom,
                                 ten_nhom = g.ten_nhom,
                                 ma_nhom_cha = g.ma_nhom_cha,
                                 ma_tieuchi = ct.ma_tieuchi,
                                 noidung = ct.noidung,
                                 noidungbyte = Encoding.UTF8.GetBytes(ct.noidung)
                             }
                             ).ToList();
                return PartialView("_TreeListPartial", model);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }

        public ActionResult RichEditPartial(string ma_tieuchi)
        {
            if (Session["TaiKhoan"] != null)
            {
                var model = db.sp_DuLieuChiTiet();
                //var x = model.SingleOrDefault(n => n.id == new Guid("D4EF2CE0-72DE-49CD-8BC7-158CBB8CEB3F"));
                var x = model.SingleOrDefault(n => n.ma_tieuchi == ma_tieuchi);
                // byte[] docBytes = RichEditExtension.SaveCopy("RichEditName", DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                byte[] nd = Encoding.UTF8.GetBytes(x.noidung);
                x.noidungbyte = nd;
                //var y = model.SingleOrDefault(n => n.id == (System.Guid)id);
                return PartialView("_RichEditPartial", x);
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
    }
}