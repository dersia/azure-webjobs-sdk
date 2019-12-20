// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace Microsoft.Azure.WebJobs.Host.Blobs.Bindings
{
    internal class DelegatingCloudBlobStream : CloudBlobStream
    {
        private readonly CloudBlobStream _inner;

        public DelegatingCloudBlobStream(CloudBlobStream inner) 
            => _inner = inner;

        public override bool CanRead 
            => _inner.CanRead;

        public override bool CanSeek 
            => _inner.CanSeek;

        public override bool CanTimeout 
            => _inner.CanTimeout;

        public override bool CanWrite 
            => _inner.CanWrite;

        public override long Length 
            => _inner.Length;

        public override long Position
        {
            get => _inner.Position;
            set => _inner.Position = value;
        }

        public override int ReadTimeout
        {
            get => _inner.ReadTimeout;
            set => _inner.ReadTimeout = value;
        }

        public override int WriteTimeout
        {
            get => _inner.WriteTimeout;
            set => _inner.WriteTimeout = value;
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback,
            object state) 
            => _inner.BeginRead(buffer, offset, count, callback, state);

        public override int EndRead(IAsyncResult asyncResult) 
            => _inner.EndRead(asyncResult);

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback,
            object state) 
            => _inner.BeginWrite(buffer, offset, count, callback, state);

        public override void EndWrite(IAsyncResult asyncResult) 
            => _inner.EndWrite(asyncResult);

        public override void Close() 
            => _inner.Close();

        public override Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken) 
            => _inner.CopyToAsync(destination, bufferSize, cancellationToken);

        public override void Flush() 
            => _inner.Flush();

        public override Task FlushAsync(CancellationToken cancellationToken) 
            => _inner.FlushAsync(cancellationToken);

        public override int Read(byte[] buffer, int offset, int count) 
            => _inner.Read(buffer, offset, count);

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) 
            => _inner.ReadAsync(buffer, offset, count, cancellationToken);

        public override int ReadByte() 
            => _inner.ReadByte();

        public override long Seek(long offset, SeekOrigin origin) 
            => _inner.Seek(offset, origin);

        public override void SetLength(long value) 
            => _inner.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count) 
            => _inner.Write(buffer, offset, count);

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken) 
            => _inner.WriteAsync(buffer, offset, count, cancellationToken);

        public override void WriteByte(byte value) 
            => _inner.WriteByte(value);

        public override Task CommitAsync() 
            => _inner.CommitAsync();

        public override void Commit() 
            => _inner.Commit();

        public override ICancellableAsyncResult BeginCommit(AsyncCallback callback, object state) 
            => _inner.BeginCommit(callback, state);

        public override void EndCommit(IAsyncResult asyncResult) 
            => _inner.EndCommit(asyncResult);

        public override ICancellableAsyncResult BeginFlush(AsyncCallback callback, object state) 
            => _inner.BeginFlush(callback, state);

        public override void EndFlush(IAsyncResult asyncResult) 
            => _inner.EndFlush(asyncResult);
    }
}
