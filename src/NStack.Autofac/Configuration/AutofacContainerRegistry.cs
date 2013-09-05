#region header
// <copyright file="AutofacContainerRegistry.cs" company="mikegrabski.com">
//    Copyright 2013 Mike Grabski
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>
#endregion

using System;

using Autofac;

namespace NStack.Configuration
{
    /// <summary>
    /// Represents an implementation of <see cref="IContainerRegistry"/> that integrates with Autofac.
    /// </summary>
    public class AutofacContainerRegistry : IContainerRegistry<IContainer>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="AutofacContainerRegistry"/>, creating a new container builder.
        /// </summary>
        public AutofacContainerRegistry()
            : this(new ContainerBuilder())
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public AutofacContainerRegistry(ContainerBuilder builder)
        {
            Builder = builder;
        }

        /// <summary>
        /// Gets the container builder where configuration is stored.
        /// </summary>
        public ContainerBuilder Builder { get; private set; }

        #region Implementation of IContainerRegistry

        /// <summary>
        /// Returns the container.
        /// </summary>
        /// <returns>The container.</returns>
        public IContainer Container()
        {
            return Builder.Build();
        }

        /// <summary>
        /// Registers an implementation of a service using an @delegate in the default scope.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="delegate">The @delegate that will be invoked when the service needs to be resolved.</param>
        /// <param name="name">Optional name of hte service implementation.</param>
        public void Register<TService, TImplementation>(Func<IResolver, TImplementation> @delegate, string name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Builder.Register(c => @delegate(new AutofacResolver(c)))
                    .As<TService>()
                    .InstancePerLifetimeScope();
            }
            else
            {
                Builder.Register(c => @delegate(new AutofacResolver(c)))
                       .Named<TService>(name)
                       .InstancePerLifetimeScope();
            }
        }

        /// <summary>
        /// Registers an implementation type of the specified service type in the default scope.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="name">The name of the service implementation.</param>
        public void Register<TService, TImplementation>(string name = null) where TImplementation : TService
        {
            if (!string.IsNullOrEmpty(name))
            {
                Builder.RegisterType<TImplementation>()
                    .As<TService>()
                    .InstancePerLifetimeScope();
            }
            else
            {
                Builder.RegisterType<TImplementation>()
                    .Named<TService>(name)
                    .InstancePerLifetimeScope();
            }
        }

        /// <summary>
        /// Registers a generic implementation type of the specified generic service type in the default scope.
        /// </summary>
        /// <param name="service">The generic type of the service.</param>
        /// <param name="implementation">The generic type of the implementation.</param>
        /// <param name="name">The name of the service implementation.</param>
        public void RegisterGeneric(Type service, Type implementation, string name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Builder.RegisterGeneric(implementation)
                    .As(service)
                    .InstancePerLifetimeScope();
            }
            else
            {
                Builder.RegisterGeneric(implementation)
                    .Named(name, service)
                    .InstancePerLifetimeScope();
            }
        }

        /// <summary>
        /// Registers a single instance of the implementation type for the specified service type.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="name">The name of the service implementation.</param>
        public void RegisterSingleInstance<TService, TImplementation>(string name = null) where TImplementation : TService
        {
            if (!string.IsNullOrEmpty(name))
            {
                Builder.RegisterType<TImplementation>()
                    .As<TService>()
                    .SingleInstance();
            }
            else
            {
                Builder.RegisterType<TImplementation>()
                    .Named<TService>(name)
                    .SingleInstance();
            }
        }

        /// <summary>
        /// Registers a single instance of an instance as the specified type.
        /// </summary>
        /// <param name="type">The type the instance should be registered as.</param>
        /// <param name="instance">The service instance.</param>
        /// <param name="name">The name of the service instance.</param>
        public void RegisterSingleInstance(Type type, object instance, string name = null)
        {
             if (!string.IsNullOrEmpty(name))
             {
                 Builder.RegisterInstance(instance)
                        .As(type)
                        .SingleInstance();
             }
             else
             {
                 Builder.RegisterInstance(instance)
                        .Named(name, type)
                        .SingleInstance();
             }
        }

        /// <summary>
        /// Registers an instance of the specified service type as the single instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        /// <param name="instance">The service instance.</param>
        /// <param name="name">Optional name of the service implementation.</param>
        public void RegisterSingleInstance<TService, TImplementation>(TImplementation instance, string name = null) where TImplementation : class, TService
        {
            if (!string.IsNullOrEmpty(name))
            {
                Builder.RegisterInstance(instance)
                    .As<TService>()
                    .SingleInstance();
            }
            else
            {
                Builder.RegisterInstance(instance)
                    .Named<TService>(name)
                    .SingleInstance();
            }
        }

        /// <summary>
        /// Registers a single instance of the implementation type for the specified service type, where the implementation is constructed using the specified delegate.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementation">The implementation type.</typeparam>
        /// <param name="delegate">The delegate invoked to construct the implementation.</param>
        /// <param name="name">The name of the service implementation.</param>
        public void RegisterSingleInstance<TService, TImplementation>(Func<IResolver, TImplementation> @delegate, string name = null) where TImplementation : TService
        {
            if (!string.IsNullOrEmpty(name))
            {
                Builder.Register(context => @delegate(new AutofacResolver(context)))
                     .As<TService>()
                     .SingleInstance();
            }
            else
            {
                Builder.Register(context => @delegate(new AutofacResolver(context)))
                    .Named<TService>(name)
                    .SingleInstance();
            }
        }

        #endregion
    }
}