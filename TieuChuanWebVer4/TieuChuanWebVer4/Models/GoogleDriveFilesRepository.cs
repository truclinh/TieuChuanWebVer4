﻿using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;

namespace TieuChuanWebVer4.Models
{
    public class GoogleDriveFilesRepository
    {
        //defined scope.
        public static string[] Scopes = { DriveService.Scope.Drive };

        //create Drive API service.
        public static DriveService GetService()
        {
            //get Credentials from client_secret.json file 
            UserCredential credential;

            using (var stream = new FileStream(@"D:\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                String FolderPath = @"D:\";
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");
                //  dsAuthorizationBroker.RedirectUri = ".";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;

            }
            credential.RefreshTokenAsync(CancellationToken.None);
            //create Drive API service.
            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveRestAPI-v3",
            });
            return service;
        }


        //get all files from Google Drive.
        public static List<GoogleDriveFiles> GetDriveFiles(string drive)
        {
            DriveService service = GetService();

            // define parameters of request.
            FilesResource.ListRequest FileListRequest = service.Files.List();
            // string s = "0B9hgKCBhg5FsZUlVT2swWlVIeGs";
            FileListRequest.Q = "'" + drive + "' in parents and trashed=false ";
            //listRequest.PageSize = 10;
            //listRequest.PageToken = 10;
            FileListRequest.Fields = "nextPageToken, files(id, name, size, version,parents,thumbnailLink,webContentLink,webViewLink,createdTime)";

            //get file list.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFiles> FileList = new List<GoogleDriveFiles>();
            int number = 0;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    if (!file.Name.Contains("MUCLUC") && !file.Name.Contains("MUC LUC") && !file.Name.Contains("BOSUNG") && !file.Name.Contains("TAILIEUHUONGDAN") && !file.Name.Contains("PHIEUKIEMTRA"))
                    {
                        number++;
                        GoogleDriveFiles File = new GoogleDriveFiles
                        {
                            Num = number,
                            Id = file.Id,
                            Name = file.Name,
                            Size = file.Size,
                            Version = file.Version,
                            Parents = file.Parents,
                            ThumbnailLink = file.ThumbnailLink,
                            WebContentLink = file.WebContentLink,
                            WebViewLink = file.WebViewLink,
                            CreatedTime = file.CreatedTime
                        };
                        FileList.Add(File);
                    }

                }
            }

            return FileList;
        }
        public static List<GoogleDriveFiles> GetSubDriveFiles(string folderId)
        {
            DriveService service = GetService();

            // define parameters of request.
            FilesResource.ListRequest FileListRequest = service.Files.List();
            FileListRequest.Q = "'" + folderId + "' in parents and trashed=false ";
            //listRequest.PageSize = 10;
            //listRequest.PageToken = 10;
            FileListRequest.Fields = "nextPageToken, files(id, name, size, version,parents,thumbnailLink,webContentLink,webViewLink,createdTime)";

            //get file list.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFiles> FileList = new List<GoogleDriveFiles>();
            int number = 0;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    //if (file.Parents != null)
                    //{
                    number++;
                    GoogleDriveFiles File = new GoogleDriveFiles
                    {
                        Num = number,
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        Parents = file.Parents,
                        ThumbnailLink = file.ThumbnailLink,
                        WebContentLink = file.WebContentLink,
                        WebViewLink = file.WebViewLink,
                        CreatedTime = file.CreatedTime
                    };
                    FileList.Add(File);
                    // }

                }
            }

            return FileList;
        }
        //file Upload to the Google Drive.
        public static void FileUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                DriveService service = GetService();

                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/GoogleDriveFiles"),
                Path.GetFileName(file.FileName));
                file.SaveAs(path);

                var FileMetaData = new Google.Apis.Drive.v3.Data.File();
                FileMetaData.Name = Path.GetFileName(file.FileName);
                FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);

                FilesResource.CreateMediaUpload request;

                using (var stream = new System.IO.FileStream(path, System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
            }
        }

        //Download file from Google Drive by fileId.
        public static string DownloadGoogleFile(string fileId)
        {
            DriveService service = GetService();

            string FolderPath = System.Web.HttpContext.Current.Server.MapPath("/GoogleDriveFiles/");
            FilesResource.GetRequest request = service.Files.Get(fileId);

            string FileName = request.Execute().Name;
            string FilePath = System.IO.Path.Combine(FolderPath, FileName);

            MemoryStream stream1 = new MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            SaveStream(stream1, FilePath);
                            break;
                        }
                    case DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream1);
            return FilePath;
        }

        // file save to server path
        private static void SaveStream(MemoryStream stream, string FilePath)
        {
            using (System.IO.FileStream file = new FileStream(FilePath, FileMode.Create, FileAccess.ReadWrite))
            {
                stream.WriteTo(file);
            }
        }

        //Delete file from the Google drive
        public static void DeleteFile(GoogleDriveFiles files)
        {
            DriveService service = GetService();
            try
            {
                // Initial validation.
                if (service == null)
                    throw new ArgumentNullException("service");

                if (files == null)
                    throw new ArgumentNullException(files.Id);

                // Make the request.
                service.Files.Delete(files.Id).Execute();
            }
            catch (Exception ex)
            {
                throw new Exception("Request Files.Delete failed.", ex);
            }
        }
    }
}