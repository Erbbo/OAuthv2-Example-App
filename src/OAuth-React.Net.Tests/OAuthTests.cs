using Google.Apis.Auth.OAuth2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OAuth_React.Net.Authorize;
using OAuth_React.Net.Controllers;
using System.Web.Mvc;
using Xunit;
using Shouldly;

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
    }
}
