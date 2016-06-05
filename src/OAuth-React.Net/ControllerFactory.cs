using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace OAuth_React.Net
{
    public class ControllerFactory : DefaultControllerFactory
    {
        /// <summary>
        /// Get controller instances by request context and controller type and 
        /// return instance as an IController type to execute request context.
        /// </summary>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            try
            {
                if (controllerType == null)
                    throw new ArgumentNullException("controllerType");

                return IocUnityContainer.Container.Resolve(controllerType, string.Empty) as IController;
            }
            catch
            {
                return null;
            }
        }
    }
}