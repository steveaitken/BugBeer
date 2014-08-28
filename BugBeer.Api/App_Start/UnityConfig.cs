using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using BugBeer.Models;
using BugBeer.Dal.Mocks;
using BugBeer.Dal;

namespace BugBeer.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            
            container.RegisterInstance<IBugBeerRepository<Beer>>(MockBeerRepo.Default);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}