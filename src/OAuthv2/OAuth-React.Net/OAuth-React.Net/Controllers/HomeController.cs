using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Util.Store;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Web.Mvc;

namespace OAuth_React.Net.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GoogleDrive()
        {
            //Scopes for use with the Google Drive API
            string[] scopes = new string[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
            var clientId = "613448404219-2reltjkbrroe2tatk80qr7utjkdfhfha.apps.googleusercontent.com";
            var clientSecret = "0IUzvr3_LuNAZjuoD3Hhaubu";
           
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                    scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore("Eric.GoogleDrive.Auth.Store")).Result;

            return Json(
                new
                {
                    user = credential.UserId,
                    accessToken = credential.Token.AccessToken,
                    refreshToken = credential.Token.RefreshToken,
                    tokenType = credential.Token.TokenType,
                    expires = credential.Token.ExpiresInSeconds,
                    scope = scopes
                }, 
                JsonRequestBehavior.AllowGet);
        }

        public JsonResult Facebook()
        {
            //Scopes for use with the Google Drive API
            string[] scopes = new string[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
            var clientId = "613448404219-2reltjkbrroe2tatk80qr7utjkdfhfha.apps.googleusercontent.com";
            var clientSecret = "0IUzvr3_LuNAZjuoD3Hhaubu";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret
                },
                    scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore("Eric.GoogleDrive.Auth.Store")).Result;

            return Json(
                new
                {
                    user = credential.UserId,
                    accessToken = credential.Token.AccessToken,
                    refreshToken = credential.Token.RefreshToken,
                    tokenType = credential.Token.TokenType,
                    expires = credential.Token.ExpiresInSeconds,
                    scope = scopes
                },
                JsonRequestBehavior.AllowGet);
        }
    }
}