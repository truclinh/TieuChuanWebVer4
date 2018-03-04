using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TieuChuanWebVer4.Models
{
    public class TieuChuanDTO : ht_dm_nsd
    {
        QL_TieuChuan2Entities db = new QL_TieuChuan2Entities();
        public List<ht_dm_nsd> lstNSD { get; set; }
        public List<dm_khoa> lstKhoa { get; set; }
        public List<dm_bomon> lstBoMon { get; set; }
        public List<dm_bomon> lstBoMonTheoKhoa { get; set; }
        public string tenkhoa { get; set; }

        //-------------------------------------- Function
        public List<ht_dm_nsd> getAllNSD()
        {
            lstNSD = new List<ht_dm_nsd>();
            var query = from tk in db.ht_dm_nsd select tk;
            foreach (var item in query)
            {
                lstNSD.Add(item);

            }
            return lstNSD.ToList();
        }
        public List<dm_khoa> getAllKhoa()
        {
            lstKhoa = new List<dm_khoa>();
            var query = from tk in db.dm_khoa select tk;
            foreach (var item in query)
            {
                lstKhoa.Add(item);

            }
            return lstKhoa.ToList();
        }
        public List<dm_bomon> getAllBoMon()
        {
            lstBoMon = new List<dm_bomon>();
            var query = from tk in db.dm_bomon select tk;
            foreach (var item in query)
            {
                lstBoMon.Add(item);

            }
            return lstBoMon.ToList();
        }
        public List<dm_bomon> getBoMonTheoKhoa(string makhoa)
        {
            lstBoMonTheoKhoa = new List<dm_bomon>();
            var query = from bm in db.dm_bomon where bm.makhoa == makhoa select bm;
            foreach (var item in query)
            {
                lstBoMonTheoKhoa.Add(item);

            }
            return lstBoMonTheoKhoa.ToList();
        }
        public string getTenKhoa(string makhoa)
        {
            var query = from k in db.dm_khoa where k.makhoa == makhoa select k.tenkhoa;
            tenkhoa = query.ToString();
            return tenkhoa ;
        }

        //-------------------------------------- End Function
    }
}