using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using OAuth_React.Net.DbManager;
using System;
using System.Configuration;

namespace OAuth_React.Net.Authorize
{
    public class AuthorizeGoogle : IAuthorize<GoogleDriveService>
    {
        private readonly GoogleDriveService _googleDriveService;

        public AuthorizeGoogle()
        {
            _googleDriveService = new GoogleDriveService(new ClientSecrets
            {
                ClientId = ConfigurationManager.AppSettings["clientId"],
                ClientSecret = ConfigurationManager.AppSettings["clientSecret"]
            }, "OAuthDrive");
        }

        public Models.UserCredential ResponseCredentials()
        {
            return new Models.UserCredential
            {
                UserName = _googleDriveService.UserCredential.UserId,
                UserToken = _googleDriveService.UserCredential.Token.AccessToken
            };
        }

        /// <summary>
        /// Authorize the service and return it
        /// </summary>
        /// <returns></returns>
        public GoogleDriveService Authorize()
        {
            _googleDriveService.Authorize(
                new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile }, 
                new DataStore());

            return _googleDriveService;
        }

        public void Dispose()
        {
            _googleDriveService.Dispose();
        }
    }
}