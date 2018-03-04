using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TieuChuanWebVer4.Models
{
    public class GoogleDriveFiles
    {
        public int Num { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public long? Size { get; set; }
        public long? Version { get; set; }
        public IList<string> Parents { get; set; }
        public string ThumbnailLink { get; set; }
        public string WebViewLink { get; set; }
        public string WebContentLink { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}