using System.Web.Mvc;
using Castle.MicroKernel.Registration;
namespace JJServicios.Web.Installer
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestylePerWebRequest().Configure(x => x.Named(x.Implementation.FullName)));
        }
    }
}
