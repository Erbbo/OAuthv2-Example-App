namespace OAuth_React.Net.Authorize
{
    public interface IAuthorize<T> where T : class
    {
        T Authorize();
        Models.UserCredential ResponseCredentials();
    }
}
