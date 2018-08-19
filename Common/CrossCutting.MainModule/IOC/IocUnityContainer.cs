using System;
using System.Diagnostics;
using CrossCutting.Core.Data;
using CrossCutting.Core.IOC;
using CrossCutting.Core.Logging;
using CrossCutting.MainModule.Logging;
using Microsoft.Practices.Unity;
using Soulstone.Data;

namespace CrossCutting.MainModule.IOC
{
    public class IocUnityContainer : IContainer
    {
        private static IocUnityContainer _container;
        private static UnityContainer _unityContainer;
        private static readonly object LockObject = new object();

        private IocUnityContainer()
        {
            _unityContainer = new UnityContainer();
            RegisterTypes();
        }

        public static IocUnityContainer Instance
        {
            get
            {
                if (_container == null)
                {
                    lock (LockObject)
                    {
                        _container = new IocUnityContainer();
                    }
                }

                return _container;
            }
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }

        public void RegisterInstance(Type type, object instance)
        {
            _unityContainer.RegisterInstance(type, instance);
        }

        public object Resolve(Type type)
        {
            return _unityContainer.Resolve(type);
        }

        public static void RegisterTypes()
        {
            _unityContainer.RegisterType<ILogManager, LogManager>();
            _unityContainer.RegisterType<IApplicationLogger, ApplicationLogger>();
            _unityContainer.RegisterType<ILogWriter, MelLogWriter>(new InjectionConstructor(TraceEventType.Information));

            _unityContainer.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
