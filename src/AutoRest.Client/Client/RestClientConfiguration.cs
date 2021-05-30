using System;
using System.Collections.Generic;
using System.Reflection;
using AutoRest.Client.Processing;
using RestSharp;

namespace AutoRest.Client.Client
{
    public class RestClientConfiguration
    {
        public Action<IRestClient> ClientConfiguration { get; set; }

        public IValueResolver ValueResolver { get; set; }

        internal List<Assembly> AssembliesToScan { get; }

        internal List<object> Middlewares { get; }

        internal List<Type> MiddlewareTypes { get; }

        public RestClientConfiguration()
        {
            AssembliesToScan = new List<Assembly>();
            Middlewares = new List<object>();
            MiddlewareTypes = new List<Type>();
        }

        public void AddMiddleware(object middleware)
        {
            Middlewares.Add(middleware);
        }

        public void AddMiddleware(Type middleware)
        {
            MiddlewareTypes.Add(middleware);
        }

        public void AddMiddlewares(IEnumerable<Assembly> assemblies)
        {
            AssembliesToScan.AddRange(assemblies);
        }
    }
}