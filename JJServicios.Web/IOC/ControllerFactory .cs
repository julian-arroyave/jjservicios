using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;

namespace JJServicios.Web.IOC
{
    /// <summary>
    /// Controller factory the class is to be used to eliminate hard-coded dependencies 
    /// between controllers and other components
    /// </summary>
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        public override void ReleaseController(IController controller)
        {
            _container.Release(controller.GetType());
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
         return (IController)_container.Resolve(controllerType);
        }
    }
}