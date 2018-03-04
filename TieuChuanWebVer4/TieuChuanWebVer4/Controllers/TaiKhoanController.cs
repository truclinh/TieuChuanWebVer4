using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class TaiKhoanController : Controller
    {
        QL_TieuChuan2Entities db = new QL_TieuChuan2Entities();
        // GET: TaiKhoan
        //--------------------------------------Đăng nhập
        [HttpGet]
        public ActionResult DangNhap()
        {
            ViewBag.DMKhoa = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.tenkhoa), "makhoa", "tenkhoa");
            //ViewBag.DMBoMon = new SelectList(db.dm_bomon.ToList().OrderBy(n => n.tenbomon), "mabomon", "tenbomon");
            return View();
        }
        public JsonResult getBM(string makhoa)
        {
            TieuChuanDTO tc = new TieuChuanDTO();
            var lstBM = tc.getBoMonTheoKhoa(makhoa);
            return Json(new SelectList(lstBM, "tenbomon", "mabomon", JsonRequestBehavior.AllowGet));
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string txtTenDN = f["txtTenDangNhap"].ToString();
            //string txtMatKhau =Encryptor.MDSHash(f["txtMatKhau"].ToString());
            string txtMatKhau = f["txtMatKhau"].ToString();
            string valKhoa = f["cboKhoa"].ToString();
            string valBoMon = f["cboBoMon"].ToString();
            ViewBag.DMKhoa = new SelectList(db.dm_khoa.ToList().OrderBy(n => n.tenkhoa), "makhoa", "tenkhoa");
            // ViewBag.DMBoMon = new SelectList(db.dm_bomon.ToList().OrderBy(n => n.tenbomon), "mabomon", "tenbomon");
            if (ModelState.IsValid)
            {
                if (txtTenDN.Length != 0)
                {
                    ht_dm_nsd ad = db.ht_dm_nsd.SingleOrDefault(x => x.ma_nsd == txtTenDN);
                    if (ad == null)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        return View();
                    }
                    else
                    {
                        if (ad.matkhau == txtMatKhau && ad.makhoa == valKhoa && ad.mabomon == valBoMon)
                        {
                            ViewBag.ThongBao = "Đăng nhập thành công";
                            Session["TaiKhoan"] = ad;
                            Session["TenDangNhap"] = ad.ma_nsd;
                            Session["MatKhau"] = ad.matkhau;
                            Session["TenNguoiDung"] = ad.ten_nsd;
                            Session["MaKhoa"] = ad.makhoa;
                            Session["MaBoMon"] = ad.mabomon;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            if (ad.matkhau != txtMatKhau)
                            {
                                ModelState.AddModelError("", "Mật khẩu không đúng");
                                return View();
                            }
                            else if (ad.makhoa != valKhoa && ad.mabomon == valBoMon)
                            {
                                ModelState.AddModelError("", "Khoa được chọn không đúng. Vui lòng chọn lại!");
                                return View();
                            }
                            else if (ad.mabomon != valBoMon && ad.makhoa == valKhoa)
                            {
                                ModelState.AddModelError("", "Bộ môn được chọn không đúng. Vui lòng chọn lại!");
                                return View();
                            }
                        }
                    }
                }
            }
            return View();
        }
        //-------------------------------------- End Đăng nhập

        //-------------------------------------- Đổi mật khẩu
        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f)
        {
            if (Session["TaiKhoan"] != null)
            {
                string txtTenDN = Session["TenDangNhap"].ToString();
                //string txtMatKhau = Encryptor.MDSHash(f["txtMatKhau"].ToString());
                string txtMatKhau = f["txtMatKhau"].ToString();
                string txtMatKhauMoi = f["txtMatKhauMoi"].ToString();
                string txtXacNhanMK = f["txtXacNhanMK"].ToString();
                if (ModelState.IsValid)
                {
                    ht_dm_nsd ad = db.ht_dm_nsd.SingleOrDefault(x => x.ma_nsd == txtTenDN);
                    if (ad == null)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại");
                        return View();
                    }
                    else
                    {
                        if (ad.matkhau == txtMatKhau)
                        {
                            if (txtMatKhauMoi == txtXacNhanMK)
                            {
                                // var mahoaPass = Encryptor.MDSHash(txtMatKhauMoi);
                                //ad.matkhau = mahoaPass;
                                db.Entry(ad).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                ModelState.AddModelError("", "Đổi mật khẩu thành công");
                                return View();
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Mật khẩu sai");
                            return View();
                        }
                    }
                }
                return View();
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //-------------------------------------- End Đổi mật khẩu

        //-------------------------------------- Đăng xuất
        public ActionResult DangXuat()
        {
            if (Session["TaiKhoan"] != null)
            {
                Session["TaiKhoan"] = null;
                Session["TenDangNhap"] = null;
                Session["TenNguoiDung"] = null;
                Session["MaKhoa"] = null;
                Session["MaBoMon"] = null;
            }
            return RedirectToAction("DangNhap", "TaiKhoan");
        }
        //--------------------------------------End Đăng xuất
    }
}