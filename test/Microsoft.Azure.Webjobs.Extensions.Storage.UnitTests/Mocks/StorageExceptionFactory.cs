// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Azure.Storage;

namespace Microsoft.Azure.WebJobs
{
    internal static class StorageExceptionFactory
    {
        public static StorageException Create(int httpStatusCode) 
            => Create(httpStatusCode, string.Empty);

        public static StorageException Create(int httpStatusCode, string errorCode)
        {
            var result = new RequestResult();
            result.HttpStatusCode = httpStatusCode;
            result.SetInternalProperty(nameof(result.ExtendedErrorInformation), new StorageExtendedErrorInformation());
            result.ExtendedErrorInformation.SetInternalProperty(nameof(result.ExtendedErrorInformation.ErrorCode), errorCode);
            return new StorageException(result, null, null);
        }

        public static Microsoft.Azure.Cosmos.Table.StorageException CreateForTable(int httpStatusCode) 
            => CreateForTable(httpStatusCode, string.Empty);

        public static Microsoft.Azure.Cosmos.Table.StorageException CreateForTable(int httpStatusCode, string errorCode)
        {
            var result = new Microsoft.Azure.Cosmos.Table.RequestResult();
            result.HttpStatusCode = httpStatusCode;
            result.SetInternalProperty(nameof(result.ExtendedErrorInformation), new Microsoft.Azure.Cosmos.Table.StorageExtendedErrorInformation());
            result.ExtendedErrorInformation.SetInternalProperty(nameof(result.ExtendedErrorInformation.ErrorCode), errorCode);
            return new Microsoft.Azure.Cosmos.Table.StorageException(result, null, null);
        }
    }
}
