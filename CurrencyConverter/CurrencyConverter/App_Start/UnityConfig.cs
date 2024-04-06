using CurrencyConverter.Services.Interfaces;
using CurrencyConverter.Services.Repositories;
using Unity;
using System.Web.Mvc;
using Unity.AspNet.Mvc;

namespace CurrencyConverter.App_Start
{
    public class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<ICurrencyRepository, CurrencyRepository>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}