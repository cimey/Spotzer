using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Spotzer.DataLayer;
using Spotzer.DataLayer.DatabaseContext;
using Spotzer.DataLayer.UnitOfWork;
using Spotzer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Spotzer.API.DependencyInjection
{
    public class DIInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<IDataBaseContext>().BasedOn<IDataBaseContext>().WithServiceAllInterfaces());
            //container.Register(Classes.FromAssemblyNamed("EF").BasedOn(typeof(DataContext)));

            //container.Register(Component.For(typeof(IDataBaseContext)).ImplementedBy(typeof(DataBaseContext)));
            //container.Register(Component.For(typeof(IDataBaseContext)).ImplementedBy(typeof(DataBaseContext)));

            //container.Register(Classes.FromAssemblyContaining<IDatabaseContext>().BasedOn<DataBaseContext>());
            container.Register(Component.For<IOrderService>().ImplementedBy<OrderService>());
            container.Register(Component.For<IUnitOfWork>().ImplementedBy<UnitOfWork>());

            container.Register(Classes
           .FromThisAssembly()
           .Pick().If(t => t.Name.EndsWith("Controller"))
           .Configure(configurer => configurer.Named(configurer.Implementation.Name))
           .LifestylePerWebRequest());

        }
    }
}