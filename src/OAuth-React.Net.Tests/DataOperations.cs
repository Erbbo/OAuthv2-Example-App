using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OAuth_React.Net.Models;
using OAuth_React.Net.DbManager;
using Google.Apis.Json;
using System.Web.Script.Serialization;

namespace OAuth_React.Net.Tests
{
    [TestClass]
    public class DataOperations
    {
        [TestMethod]
        public void FetchProfilesTest()
        {
            var jsonString = @"{'AccessToken':'ya29.CjHzAkRgA3UP_xkSsrFAY3qbJaCy_zzTntC8sBx7XPHGBswe0QyFSQ_jM_U5cg_3eXFc','TokenType':'Bearer','ExpiresInSeconds':3600,'RefreshToken:','1 / f8f1oMRI8WG1ZLJJa7JgaFqopLRAAMWdj2d60rvhsjU','Scope':null,'Issued':'\/ Date(1464657945327)\/ '}";
            var serialized = new JavaScriptSerializer().Serialize(jsonString);
            var deserialize = NewtonsoftJsonSerializer.Instance.Deserialize<string>(serialized);
        }
    }
}
