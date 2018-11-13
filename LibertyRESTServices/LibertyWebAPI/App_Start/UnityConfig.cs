using LibertyWebAPI.BusinessServices;
using LibertyWebAPI.BusinessServices.Contracts;
using LibertyWebAPI.DataModel.Contracts;
using LibertyWebAPI.DataModel.Repositories;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace LibertyWebAPI
{
    /// <summary>
    /// Liberty API Unity Configuration
    /// </summary>
    public static class UnityConfig
    {
        /// <summary>
        /// RegisterComponents (called from Application_Start)
        /// </summary>
        /// <param name="config"></param>
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            RegisterTypes(container);

            //GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            config.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container); // Fix for default constructor missing error
        }

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types
        /// such as controllers or API controllers (unless you want to
        /// change the defaults), as Unity allows resolving a concrete type
        /// even if it was not previously registered.</remarks>
        private static void RegisterTypes(UnityContainer container)
        {
            // register all your components with the container here

            container.RegisterType<ICatalogService, CatalogService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICatalogRepository, CatalogRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<ICategoryService, CategoryService>(new HierarchicalLifetimeManager());
            container.RegisterType<ICategoryRepository, CategoryRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IInstitutionService, InstitutionService>(new HierarchicalLifetimeManager());
            container.RegisterType<IInstitutionRepository, InstitutionRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<ILastRepricedService, LastRepricedService>(new HierarchicalLifetimeManager());
            container.RegisterType<ILastRepricedRepository, LastRepricedRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IOrderSubmitService, OrderSubmitService>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrderSubmitRepository, OrderSubmitRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IProductService, ProductService>(new HierarchicalLifetimeManager());
            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IAccessoryCategoryService, AccessoryCategoryService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccessoryCategoryRepository, AccessoryCategoryRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IAccessoryService, AccessoryService>(new HierarchicalLifetimeManager());
            container.RegisterType<IAccessoryRepository, AccessoryRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IShippingOptionsService, ShippingOptionsService>(new HierarchicalLifetimeManager());
            container.RegisterType<IShippingOptionsRepository, ShippingOptionsRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IOrderPricingService, OrderPricingService>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrderPricingRepository, OrderPricingRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IOrderHistoryService, OrderHistoryService>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrderHistoryRepository, OrderHistoryRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IFOASaveService, FOASaveService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFOASaveRepository, FOASaveRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IFOABranchListService, FOABranchListService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFOABranchListRepository, FOABranchListRepository>(new HierarchicalLifetimeManager());

            container.RegisterType<IFOAAccountValidationService, FOAAccountValidationService>(new HierarchicalLifetimeManager());
            container.RegisterType<IFOAAccountValidationRepository, FOAAccountValidationRepository>(new HierarchicalLifetimeManager());
        }
    }
}