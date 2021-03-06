﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.WebJobs.Host.Executors;
using Microsoft.Azure.WebJobs.Host.FunctionalTests.TestDoubles;
using Microsoft.Azure.WebJobs.Host.Loggers;
using Microsoft.Azure.WebJobs.Host.TestCommon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace Microsoft.Azure.WebJobs.Host.FunctionalTests
{
    static class Utility
    {
        public static CloudQueueMessage CreateCloudQueueMessageFromByteArray(byte[] content)
        {
            return new CloudQueueMessage(Convert.ToBase64String(content), true);
        }

        public static IHostBuilder ConfigureDefaultTestHost<TProgram>(this IHostBuilder builder, StorageAccount account)
        {
            return builder.ConfigureDefaultTestHost<TProgram>(b =>
            {
                b.AddAzureStorage();
            })
            .ConfigureServices(services => services.AddFakeStorageAccountProvider(account));
        }

        public static IHostBuilder ConfigureFakeStorageAccount(this IHostBuilder builder)
        {
            return builder.ConfigureServices(services =>
            {
                services.AddFakeStorageAccountProvider();
            });
        }

        public static IServiceCollection AddFakeStorageAccountProvider(this IServiceCollection services)
        {
            // return services.AddFakeStorageAccountProvider(new XFakeStorageAccount()); $$$
            throw new NotImplementedException();
        }

        public static IServiceCollection AddNullLoggerProviders(this IServiceCollection services)
        {
             return services
                .AddSingleton<IFunctionOutputLoggerProvider, NullFunctionOutputLoggerProvider>()
                .AddSingleton<IFunctionInstanceLoggerProvider, NullFunctionInstanceLoggerProvider>();
        }

        public static IServiceCollection AddFakeStorageAccountProvider(this IServiceCollection services, StorageAccount account)
        {
            throw new NotImplementedException();
            /*
            if (account is XFakeStorageAccount)
            {
                services.AddNullLoggerProviders();
            }

            return services.AddSingleton<XStorageAccountProvider>(new FakeStorageAccountProvider(account));
            */
        }

        public static T SetInternalProperty<T>(this T obj, string name, object value)
        {
            var t = obj.GetType();

            var prop = t.GetProperty(name,
              BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            // Reflection has a quirk.  While a property is inherited, the setter may not be. 
            // Need to request the property on the type it was declared. 
            while (!prop.CanWrite)
            {
                t = t.BaseType;
                prop = t.GetProperty(name,
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            }

            prop.SetValue(obj, value);
            return obj;
        }
    }
}
