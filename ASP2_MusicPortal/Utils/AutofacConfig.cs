using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Core.Activators;
using ASP2_MusicPortal.Models;
using ASP2_MusicPortal.Models.Entities;

namespace ASP2_MusicPortal.Utils
{
    public class MyAtofacContainer
    {

        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //builder.RegisterType<MPRepository>().As<IMPRepository>().WithParameters(new List<Parameter> { new NamedParameter("context", new MusicPortalModel()), new NamedParameter("connectionStr", "MusicPortalLocalDB") });
            builder.RegisterType<MPRepository>().As<IMPRepository>().WithParameter("context", new MusicPortalModel()).InstancePerLifetimeScope();
            //builder.RegisterType<Lazy<MPRepository>>().As<Lazy<IMPRepository>>();
            //builder.RegisterType<MPRepository>().As<IMPRepository>().InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }
    }
}