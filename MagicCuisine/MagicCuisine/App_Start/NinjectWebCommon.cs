[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MagicCuisine.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MagicCuisine.App_Start.NinjectWebCommon), "Stop")]

namespace MagicCuisine.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Data;
    using MagicCuisine.Helpers.Contracts;
    using MagicCuisine.Helpers;
    using Data.UnitOfWork;
    using Services.Contracts;
    using Services;
    using Data.Repository.Contracts;
    using Data.Repository;
    using AutoMapper;
    using Services.Providers;
    using MagicCuisine.Providers;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<CuisineDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IFileHelper>().To<FileHelper>().InRequestScope();

            kernel.Bind<IAddressService>().To<AddressService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            kernel.Bind<IRecipeService>().To<RecipeService>().InRequestScope();
            kernel.Bind<ICommentService>().To<CommentService>().InRequestScope();

            kernel.Bind<ICountryRepository>().To<CountryRepository>().InRequestScope();
            kernel.Bind<ITownRepository>().To<TownRepository>().InRequestScope();
            kernel.Bind<IAddessRepository>().To<AddessRepository>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IRecipeRepository>().To<RecipeRepository>().InRequestScope();
            kernel.Bind<ICommentRepository>().To<CommentRepository>().InRequestScope();

            kernel.Bind<IDateProvider>().To<DateProvider>().InSingletonScope();
            kernel.Bind<IMapProvider>().To<MapProvider>().InSingletonScope();
        }
    }
}
