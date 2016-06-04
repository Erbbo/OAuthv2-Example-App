using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using OAuth_React.Net.DbManager;
using OAuth_React.Net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace OAuth_React.Net.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ShowAuthenticationValues()
        {
            JsonResult json = null;
            GoogleDriveService googleService = Authorize();
            json = Json(new Models.UserCredential
            {
                UserName = googleService.UserCredential.UserId,
                UserToken = googleService.UserCredential.Token.AccessToken
            },
            JsonRequestBehavior.AllowGet);

            return json;
        }

        public JsonResult ShowDriveData()
        {
            var fileUrls = new List<File>();
            JsonResult json = null;

            try
            {
                GoogleDriveService googleService = Authorize();
                using (var drive = googleService.Drive)
                {
                    FilesResource.ListRequest listRequest = drive.Files.List();
                    var files = listRequest.Execute().Items;
                    if (files != null && files.Count > 0)
                    {
                        fileUrls.ExtractFileUrls(files);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            if (fileUrls != null && fileUrls.Count > 0)
            {
                json = Json(new File
                {
                    FileUrls = fileUrls
                },

                JsonRequestBehavior.AllowGet);
            }

            return json;
        }

        private GoogleDriveService Authorize()
        {
            var googleService = new GoogleDriveService(
                new ClientSecrets
                {
                    ClientId = ConfigurationManager.AppSettings["clientId"],
                    ClientSecret = ConfigurationManager.AppSettings["clientSecret"]
                },
                "OAuthDrive");

            googleService.Authorize(new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile }, new DataStore());
            return googleService;
        }
    }
}