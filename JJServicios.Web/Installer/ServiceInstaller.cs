using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using JJServicios.DB.Contracts;
using JJServicios.DB.Contracts.Repositories;
using JJServicios.DB.Impl;

namespace JJServicios.Web.Installer
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestylePerWebRequest().Configure(x => x.Named(x.Implementation.FullName)));
       
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>().LifestyleTransient());

            container.Register(Component.For<IDeleteAdoRepository>().ImplementedBy<DeleteAdoRepository>().LifestyleTransient());
            
        }
    }
}
