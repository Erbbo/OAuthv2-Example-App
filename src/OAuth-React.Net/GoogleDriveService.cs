using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Threading;

namespace OAuth_React.Net
{
    public class GoogleDriveService : IDisposable
    {
        private readonly ClientSecrets _applicationCredentials;
        private readonly string _applicationName;
        public UserCredential UserCredential { get; set; }

        public DriveService Drive
        {
            get
            {
                return new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = UserCredential,
                    ApplicationName = _applicationName
                });
            }
        }

        public GoogleDriveService(ClientSecrets applicationCredentials, string applicationName)
        {
            _applicationCredentials = applicationCredentials;
            _applicationName = applicationName;
        }

        public void Authorize(string[] scopes, IDataStore dataStore)
        {
            UserCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    _applicationCredentials,
                    scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    dataStore).Result;
        }

        public void Dispose()
        {
            Drive.Dispose();
        }
    }
}