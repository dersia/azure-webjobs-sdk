﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.WebJobs.Host.Converters;
using Microsoft.Azure.Cosmos.Table;

namespace Microsoft.Azure.WebJobs.Host.Bindings.StorageAccount
{
    internal class StringToCloudTableStorageAccountConverter : IConverter<string, CloudStorageAccount>
    {
        public CloudStorageAccount Convert(string input)
        {
            return CloudStorageAccount.Parse(input);
        }
    }
}
