using Google.Apis.Json;
using Google.Apis.Util.Store;
using OAuth_React.Net.Models;
using System;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace OAuth_React.Net.DbManager
{
    public class DataStore : IDataStore
    {
        private DataOperations<User> _operations;

        public DataStore()
        {
            _operations = new DataOperations<User>();
        }

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return await SetUserToken<T>(key);
        }

        private Task<T> SetUserToken<T>(string key)
        {
            var tcs = new TaskCompletionSource<T>();
            try
            {
                var refreshToken = _operations.FetchToken(key);
                tcs.SetResult(NewtonsoftJsonSerializer.Instance.Deserialize<T>(refreshToken));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return tcs.Task;
        }

        public Task StoreAsync<T>(string key, T value)
        {
            var json = NewtonsoftJsonSerializer.Instance.Serialize(value);
            return Task.Run(() =>
                _operations.SaveChanges(new User
                {
                    UserName = key,
                    UserToken = json
                }));
        }
    }
}