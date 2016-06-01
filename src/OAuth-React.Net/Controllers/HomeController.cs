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
                    foreach (var file in files)
                    {

                    }
                }
                else
                {

                }

                gs.Dispose();
            }
            catch (Exception ex)
            {
                throw new UnauthorizedAccessException(ex.Message, ex.InnerException);
            }

            if (credential != null)
            {
                json = Json(new Models.User
                {
                    UserName = Environment.UserName,
                    UserToken = credential.Token.AccessToken
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