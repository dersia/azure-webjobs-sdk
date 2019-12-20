// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FakeStorage
{
    public class FakeAccount
    {
        internal readonly string _accountName = "fakeaccount";
        internal readonly string _accountKey = "key1";
        internal Microsoft.Azure.Storage.Auth.StorageCredentials _creds;
        internal Microsoft.Azure.Cosmos.Table.StorageCredentials _tableCreds;

        internal readonly MemoryBlobStore _blobStore = new MemoryBlobStore();
        internal readonly MemoryTableStore Store = new MemoryTableStore();
        internal readonly MemoryQueueStore _queueStore = new MemoryQueueStore();

        public string Name => _accountName;

        public FakeAccount()
        {
            _creds = new Microsoft.Azure.Storage.Auth.StorageCredentials(_accountName, _accountKey);
            _tableCreds = new Microsoft.Azure.Cosmos.Table.StorageCredentials(_accountName, _accountKey);
        }

        public CloudQueueClient CreateCloudQueueClient()
        {
            return new FakeQueueClient(this);
        }

        public CloudTableClient CreateCloudTableClient()
        {
            return new FakeTableClient(this);
        }

        public CloudBlobClient CreateCloudBlobClient()
        {
            return new FakeStorageBlobClient(this);
        }

        // For testing, set a blob instance. 
        public void SetBlob(string containerName, string blobName, ICloudBlob blob)
        {
            _blobStore.SetBlob(containerName, blobName, blob);
        }
    }
}