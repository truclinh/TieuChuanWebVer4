using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TieuChuanWebVer4.Models;
namespace TieuChuanWebVer4.Controllers
{
    public class QuanLyDuLieuController : Controller
    {
        // GET: QuanLyDuLieu
        QL_TieuChuan2Entities db = new QL_TieuChuan2Entities();
        [HttpGet]
        public ActionResult GetGoogleDriveFiles()
        {
            return View(GoogleDriveFilesRepository.GetDriveFiles());
        }
        [HttpGet]
        public ActionResult GetSubGoogleDriveFiles(string folderId)
        {
            return View(GoogleDriveFilesRepository.GetSubDriveFiles(folderId));
        }
        [HttpGet]
        public ActionResult LayDuLieu()
        {
            db.sp_LayDuLieu();
            List<GoogleDriveFiles> lstDataMaster = GoogleDriveFilesRepository.GetDriveFiles();
            foreach (var item in lstDataMaster)
            {
                if (!item.Name.Contains("MUCLUC") && !item.Name.Contains("MUC LUC") && !item.Name.Contains("BOSUNG") && !item.Name.Contains("TAILIEUHUONGDAN") && !item.Name.Contains("PHIEUKIEMTRA"))
                {
                    List<GoogleDriveFiles> lstDataDetail = GoogleDriveFilesRepository.GetSubDriveFiles(item.Id);
                    foreach (var item1 in lstDataDetail)
                    {
                        Guid soid = Guid.NewGuid();
                        db.sp_CapNhapMaster(soid, item1.Name, item.Name, "Linh", DateTime.Now);
                        List<GoogleDriveFiles> lstDataDetailDoc = GoogleDriveFilesRepository.GetSubDriveFiles(item1.Id);
                        foreach (var item2 in lstDataDetailDoc)
                        {
                            Guid id = Guid.NewGuid();
                            db.sp_CapNhapDetail(id, soid, item2.Name, item2.Id, item2.Num);
                        }
                    }
                }
                //db.sp_CapNhapMaster(soid,item.Name,item.);
            }
            return RedirectToAction("GetGoogleDriveFiles", "QuanLyDuLieu");
        }
    }
}