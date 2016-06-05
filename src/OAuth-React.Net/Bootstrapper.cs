using Google.Apis.Drive.v2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Mvc;
using OAuth_React.Net.Authorize;
using System.Web.Mvc;

namespace OAuth_React.Net
{
    public class Bootstrapper : DefaultControllerFactory
    {
        /// <summary>
        /// Initialize the container and resolve using a registration point
        /// </summary>
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainerWithRegistration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        /// <summary>
        /// Build the container without registering types
        /// </summary>
        public static IUnityContainer BuildUnityContainerWithoutRegistration()
        {
            var container = new UnityContainer();
            IocUnityContainer.Container = container;
            return container;
        }

        /// <summary>
        /// Build the container with default manager and register types
        /// </summary>
        private static IUnityContainer BuildUnityContainerWithRegistration()
        {
            var container = new UnityContainer();
            container.RegisterType<IAuthorize<GoogleDriveService>, AuthorizeGoogle>();
            container.RegisterType<DriveService, DriveService>();
            IocUnityContainer.Container = container;
            return container;
        }
    }
}
