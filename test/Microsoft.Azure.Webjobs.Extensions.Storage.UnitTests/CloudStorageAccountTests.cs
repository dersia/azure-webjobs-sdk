// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Blobs;
using Microsoft.Azure.WebJobs.Host.Indexers;
using Microsoft.Azure.WebJobs.Host.TestCommon;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Microsoft.Azure.WebJobs.Host.FunctionalTests
{
    public class CloudStorageAccountTests
    {
        [Fact]
        public async Task CloudStorageAccount_CanCall()
        {
            // Arrange
            Azure.Storage.CloudStorageAccount realAccount = Azure.Storage.CloudStorageAccount.DevelopmentStorageAccount;
            Cosmos.Table.CloudStorageAccount realTableAccount = Cosmos.Table.CloudStorageAccount.DevelopmentStorageAccount;

            StorageAccount account = StorageAccount.New(realAccount, realTableAccount);

            // Act
            var t = typeof(CloudStorageAccountProgram);
            var result = await FunctionalTest.CallAsync<Azure.Storage.CloudStorageAccount>(
                account, 
                t, 
                t.GetMethod(nameof(CloudStorageAccountProgram.BindToCloudStorageAccount)), 
                null, // args
                (s) => CloudStorageAccountProgram.TaskSource = s);

            // Assert
            Assert.NotNull(result);
            Assert.Same(realAccount, result);

            // Act Table
            var tableT = typeof(CloudStorageAccountProgram);
            var tableResult = await FunctionalTest.CallAsync<Cosmos.Table.CloudStorageAccount>(
                account,
                tableT,
                t.GetMethod(nameof(CloudStorageAccountProgram.BindToCloudTableStorageAccount)),
                null, // args
                (s) => CloudStorageAccountProgram.TableTaskSource = s);

            // Assert Table
            Assert.NotNull(tableResult);
            Assert.Same(realTableAccount, tableResult);
        }

        private class CloudStorageAccountProgram
        {
            public static TaskCompletionSource<Azure.Storage.CloudStorageAccount> TaskSource { get; set; }
            public static TaskCompletionSource<Cosmos.Table.CloudStorageAccount> TableTaskSource { get; set; }

            [NoAutomaticTrigger]
            public static void BindToCloudStorageAccount(Azure.Storage.CloudStorageAccount account)
            {
                TaskSource.TrySetResult(account);
            }

            [NoAutomaticTrigger]
            public static void BindToCloudTableStorageAccount(Cosmos.Table.CloudStorageAccount account)
            {
                TableTaskSource.TrySetResult(account);
            }
        }
    }
}
