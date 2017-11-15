using System;
using System.Collections.Generic;
//using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CardiologicalResearch.Models;
using WebMatrix.WebData;
using CardiologicalResearch.Controllers;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;

namespace CardiologicalResearch
{
    // Примечание: Инструкции по включению классического режима IIS6 или IIS7 
    // см. по ссылке http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        public static IContainer Container { get; set; }

        protected void Application_Start()
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId",
                                                         "UserName", autoCreateTables: true);
            AreaRegistration.RegisterAllAreas();
            System.Data.Entity.Database.SetInitializer<CardioContext>( new ProjectInitializer());
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //RegisterDependencies();
            /*var configuration = new CardiologicalResearch.Migrations.Configuration();
            var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            migrator.Update();
            */
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<EntityRepository>().As<IRepository>().InstancePerLifetimeScope();
            Container = builder.Build();
            var config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(Container);
            RegisterCustomControllerFactory();
        }
        private void RegisterCustomControllerFactory()
        {
            IControllerFactory factory = new ControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(factory);
        }
    }
}