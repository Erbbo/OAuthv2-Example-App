using Google.Apis.Drive.v2;

namespace OAuth_React.Net.Authorize
{
    public interface IDriveService
    {
        DriveService Drive { get; }
    }
}
