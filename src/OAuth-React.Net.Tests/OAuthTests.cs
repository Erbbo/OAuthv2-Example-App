using Google.Apis.Auth.OAuth2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OAuth_React.Net.Authorize;
using OAuth_React.Net.Controllers;
using System.Web.Mvc;
using Xunit;
using Shouldly;
using System;
using System.Configuration;

namespace OAuth_React.Net.Tests
{
    [TestClass]
    public class OAuthTests
    {
        private readonly Mock<IAuthorize<GoogleDriveService>> _authorizeMock;

        public OAuthTests()
        {
            _authorizeMock = new Mock<IAuthorize<GoogleDriveService>>();
            _authorizeMock.Setup(x => x.Authorize()).Returns(new GoogleDriveService(new ClientSecrets(), "OAuth"));
            _authorizeMock.Setup(x => x.ResponseCredentials()).Returns(
                new Models.UserCredential
                {
                    UserName = "TestUser",
                    UserToken = "TestToken"
                });
        }

        [Fact]
        public void ShowAuthenticationValues()
        {
            var controller = new HomeController(_authorizeMock.Object);
            JsonResult json = controller.ShowAuthenticationValues();
            var result = (Models.UserCredential)json.Data;
            result.UserName.ShouldBe("TestUser");
            result.UserToken.ShouldBe("TestToken");
        }

        [Fact]
        public void ShowAuthenticationFails()
        {
            var controller = new HomeController(_authorizeMock.Object);
            Should.Throw<Exception>(() =>
            {
                controller.ShowDriveData();
            });
        }

        //Will not work until can inject the web.config values
        [Fact(Skip ="Integration test to get drive files")]
        public void ShowDriveIntegrationTest()
        {
            var clientSecrets = new ClientSecrets
            {
                ClientId = ConfigurationManager.AppSettings["clientId"],
                ClientSecret = ConfigurationManager.AppSettings["clientSecret"]
            };
            var googleService = new AuthorizeGoogle();
            var controller = new HomeController(googleService);
            var json = controller.ShowDriveData();
            var result = (Models.File)json.Data;
            result.FileUrls.Count.ShouldBeGreaterThan(5);
        }
    }
}
