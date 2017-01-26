
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
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        
        #region IHttpHandler Members
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            AddBindings();
        }
        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        
        private void AddBindings()
        {
            kernel.Bind<IAuthProvider>().To<DatabaseProvider>();
        }

        #endregion
    }
}
