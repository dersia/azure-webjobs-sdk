// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;

namespace Microsoft.Azure.WebJobs.Host.Bindings.StorageAccount
{
    internal class CloudTableStorageAccountBindingProvider : IBindingProvider
    {
        private readonly StorageAccountProvider _accountProvider;

        public CloudTableStorageAccountBindingProvider(StorageAccountProvider accountProvider)
        {
            _accountProvider = accountProvider ?? throw new ArgumentNullException("accountProvider");
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            ParameterInfo parameter = context.Parameter;

            
            
            if (parameter.ParameterType != typeof(CloudStorageAccount))
            {
                return Task.FromResult<IBinding>(null);
            }

            var accountAttribute = TypeUtility.GetHierarchicalAttributeOrNull<StorageAccountAttribute>(parameter);
            var account = _accountProvider.Get(accountAttribute?.Account);
            var binding = new CloudTableStorageAccountBinding(parameter.Name, account.SdkTableObject);
            return Task.FromResult<IBinding>(binding);
        }
    }
}
