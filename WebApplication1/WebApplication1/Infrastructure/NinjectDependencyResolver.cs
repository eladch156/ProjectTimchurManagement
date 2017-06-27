using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using WebApplication1.AuthAbstract;
using Ninject.Parameters;
using Ninject.Syntax;
using System.Configuration;
using WebApplication1.Infrastructure.AuthAbstract;

namespace WebApplication1.Infrastructure
{
    /// <summary>
    /// Implementation of a Ninject-based dependency resolver
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        
        #region IHttpHandler Members
        /// <summary>
        /// Constructor, sets kernel and adds binding.
        /// </summary>
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        /// <summary>
        /// Return false in case Managed Handler cannot be reused for another request.
        /// Usually this would be false in case of some state information preserved per request.
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
        /// <summary>
        /// Extracts service of the type requested.
        /// </summary>
        /// <param name="serviceType">Type of service requested.</param>
        /// <returns>The service extracted from the kernel.</returns>
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        /// <summary>
        /// Extracts all the services of a given type.
        /// </summary>
        /// <param name="serviceType">Type of services requested.</param>
        /// <returns>Collection of the services from the kernel.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        /// <summary>
        /// Adds database authorization binding to the kernel.
        /// </summary>
        private void AddBindings()
        {
            kernel.Bind<IAuthProvider>().To<DatabaseProvider>();
        }
        #endregion
    }
}
