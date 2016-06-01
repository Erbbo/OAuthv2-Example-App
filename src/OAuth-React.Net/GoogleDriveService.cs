using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;

namespace OAuth_React.Net
{
    public class GoogleDriveService
    {
        public DriveService GetDriveService(UserCredential credential)
        {
            return new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "OAuthDrive",
            });
        }
    }
}