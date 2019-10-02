using Microsoft.Extensions.DependencyInjection;
using System;

namespace Ioc
{
    public sealed class ContainerFactory
    {
        private static readonly ContainerFactory factory = new ContainerFactory();

        internal IServiceCollection Services { get; set; }

        private ContainerFactory() { }

        public static ContainerFactory Instance = factory;

        public T GetService<T>()
        {
            if (Services == null)
                throw new ApplicationException("ServiceCollection not added to ContainerFactoy! Consider adding the ServiceCollection extension method 'AddServicesToContainerFactory()'");

            var service = Services.BuildServiceProvider().GetService<T>();

            if (service == null)
                throw new ApplicationException($"Service of type '{typeof(T).Name}' not registered!");

              return service;
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static void AddServicesToContainerFactory(this IServiceCollection services)
        {
            ContainerFactory.Instance.Services = services;
        }
    }
}
