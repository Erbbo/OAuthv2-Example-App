using Google.Apis.Drive.v2;
using OAuth_React.Net.Authorize;
using OAuth_React.Net.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OAuth_React.Net.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthorize<GoogleDriveService> _authorize;
        public HomeController(IAuthorize<GoogleDriveService> authorize)
        {
            _authorize = authorize;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Show username and token stored in database
        /// </summary>
        /// <returns></returns>
        public JsonResult ShowAuthenticationValues()
        {
            JsonResult json = null;
            using (var googleService = _authorize.Authorize())
            {
                json = Json(_authorize.ResponseCredentials(), JsonRequestBehavior.AllowGet);
            }

            return json;
        }

        /// <summary>
        /// Authorize with access token and display Urls
        /// </summary>
        /// <returns></returns>
        public JsonResult ShowDriveData()
        {
            var fileUrls = new List<File>();
            JsonResult json = null;

            try
            {
                using (var drive = _authorize.Authorize().Drive)
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
    }
}