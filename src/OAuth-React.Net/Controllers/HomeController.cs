using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using OAuth_React.Net.DbManager;
using OAuth_React.Net.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Web.Mvc;

namespace OAuth_React.Net.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GoogleDrive()
        {
            IList<Models.File> fileUrls = new List<Models.File>();
            UserCredential credential = null;
            JsonResult json = null;
            string[] scopes = new string[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
            var clientAuth = new ClientSecrets
            {
                ClientId = ConfigurationManager.AppSettings["clientId"],
                ClientSecret = ConfigurationManager.AppSettings["clientSecret"]
            };

            try
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    clientAuth,
                    scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new DataStore()).Result;

                GoogleDriveService service = new GoogleDriveService();
                var gs = service.GetDriveService(credential);
                FilesResource.ListRequest listRequest = gs.Files.List();
                var files = listRequest.Execute().Items;
                if (files != null && files.Count > 0)
                {
                    files.ForEach(x =>
                    {
                        if (x.ExportLinks != null)
                        {
                            x.ExportLinks.AddUrls(y =>
                            {
                                fileUrls.Add(new Models.File
                                {
                                    FileType = y.Key,
                                    FileUrl = y.Value
                                });
                            });
                        }
                    });
                }

                gs.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            if (fileUrls != null && fileUrls.Count > 0)
            {
                json = Json(new Models.File
                {
                    FileUrls = fileUrls
                },

                JsonRequestBehavior.AllowGet);
            }

            return json;
        }

        public JsonResult Facebook()
        {
            return new JsonResult();
        }
    }
}