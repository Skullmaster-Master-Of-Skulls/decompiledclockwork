using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Win32;

namespace System.Net.Cache
{
	// Token: 0x02000584 RID: 1412
	internal class SingleItemRequestCache : WinInetCache
	{
		// Token: 0x06002B52 RID: 11090 RVA: 0x000BC0E9 File Offset: 0x000BB0E9
		internal SingleItemRequestCache(bool useWinInet) : base(true, true, false)
		{
			this._UseWinInet = useWinInet;
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000BC0FC File Offset: 0x000BB0FC
		internal override Stream Retrieve(string key, out RequestCacheEntry cacheEntry)
		{
			Stream result;
			if (!this.TryRetrieve(key, out cacheEntry, out result))
			{
				FileNotFoundException ex = new FileNotFoundException(null, key);
				throw new IOException(SR.GetString("net_cache_retrieve_failure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x000BC140 File Offset: 0x000BB140
		internal override Stream Store(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata)
		{
			Stream result;
			if (!this.TryStore(key, contentLength, expiresUtc, lastModifiedUtc, maxStale, entryMetadata, systemMetadata, out result))
			{
				FileNotFoundException ex = new FileNotFoundException(null, key);
				throw new IOException(SR.GetString("net_cache_retrieve_failure", new object[]
				{
					ex.Message
				}), ex);
			}
			return result;
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x000BC190 File Offset: 0x000BB190
		internal override void Remove(string key)
		{
			if (!this.TryRemove(key))
			{
				FileNotFoundException ex = new FileNotFoundException(null, key);
				throw new IOException(SR.GetString("net_cache_retrieve_failure", new object[]
				{
					ex.Message
				}), ex);
			}
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000BC1D0 File Offset: 0x000BB1D0
		internal override void Update(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata)
		{
			if (!this.TryUpdate(key, expiresUtc, lastModifiedUtc, lastSynchronizedUtc, maxStale, entryMetadata, systemMetadata))
			{
				FileNotFoundException ex = new FileNotFoundException(null, key);
				throw new IOException(SR.GetString("net_cache_retrieve_failure", new object[]
				{
					ex.Message
				}), ex);
			}
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x000BC21C File Offset: 0x000BB21C
		internal override bool TryRetrieve(string key, out RequestCacheEntry cacheEntry, out Stream readStream)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SingleItemRequestCache.FrozenCacheEntry frozenCacheEntry = this._Entry;
			cacheEntry = null;
			readStream = null;
			if (frozenCacheEntry == null || frozenCacheEntry.Key != key)
			{
				RequestCacheEntry entry;
				Stream stream;
				if (!this._UseWinInet || !base.TryRetrieve(key, out entry, out stream))
				{
					return false;
				}
				frozenCacheEntry = new SingleItemRequestCache.FrozenCacheEntry(key, entry, stream);
				stream.Close();
				this._Entry = frozenCacheEntry;
			}
			cacheEntry = SingleItemRequestCache.FrozenCacheEntry.Create(frozenCacheEntry);
			readStream = new SingleItemRequestCache.ReadOnlyStream(frozenCacheEntry.StreamBytes);
			return true;
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x000BC298 File Offset: 0x000BB298
		internal override bool TryStore(string key, long contentLength, DateTime expiresUtc, DateTime lastModifiedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, out Stream writeStream)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			RequestCacheEntry requestCacheEntry = new RequestCacheEntry();
			requestCacheEntry.IsPrivateEntry = base.IsPrivateCache;
			requestCacheEntry.StreamSize = contentLength;
			requestCacheEntry.ExpiresUtc = expiresUtc;
			requestCacheEntry.LastModifiedUtc = lastModifiedUtc;
			requestCacheEntry.LastAccessedUtc = DateTime.UtcNow;
			requestCacheEntry.LastSynchronizedUtc = DateTime.UtcNow;
			requestCacheEntry.MaxStale = maxStale;
			requestCacheEntry.HitCount = 0;
			requestCacheEntry.UsageCount = 0;
			requestCacheEntry.IsPartialEntry = false;
			requestCacheEntry.EntryMetadata = entryMetadata;
			requestCacheEntry.SystemMetadata = systemMetadata;
			writeStream = null;
			Stream realWriteStream = null;
			if (this._UseWinInet)
			{
				base.TryStore(key, contentLength, expiresUtc, lastModifiedUtc, maxStale, entryMetadata, systemMetadata, out realWriteStream);
			}
			writeStream = new SingleItemRequestCache.WriteOnlyStream(key, this, requestCacheEntry, realWriteStream);
			return true;
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x000BC350 File Offset: 0x000BB350
		private void Commit(string key, RequestCacheEntry tempEntry, byte[] allBytes)
		{
			SingleItemRequestCache.FrozenCacheEntry entry = new SingleItemRequestCache.FrozenCacheEntry(key, tempEntry, allBytes);
			this._Entry = entry;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000BC370 File Offset: 0x000BB370
		internal override bool TryRemove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (this._UseWinInet)
			{
				base.TryRemove(key);
			}
			SingleItemRequestCache.FrozenCacheEntry entry = this._Entry;
			if (entry != null && entry.Key == key)
			{
				this._Entry = null;
			}
			return true;
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000BC3BC File Offset: 0x000BB3BC
		internal override bool TryUpdate(string key, DateTime expiresUtc, DateTime lastModifiedUtc, DateTime lastSynchronizedUtc, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			SingleItemRequestCache.FrozenCacheEntry frozenCacheEntry = SingleItemRequestCache.FrozenCacheEntry.Create(this._Entry);
			if (frozenCacheEntry == null || frozenCacheEntry.Key != key)
			{
				return true;
			}
			frozenCacheEntry.ExpiresUtc = expiresUtc;
			frozenCacheEntry.LastModifiedUtc = lastModifiedUtc;
			frozenCacheEntry.LastSynchronizedUtc = lastSynchronizedUtc;
			frozenCacheEntry.MaxStale = maxStale;
			frozenCacheEntry.EntryMetadata = entryMetadata;
			frozenCacheEntry.SystemMetadata = systemMetadata;
			this._Entry = frozenCacheEntry;
			return true;
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000BC42C File Offset: 0x000BB42C
		internal override void UnlockEntry(Stream stream)
		{
		}

		// Token: 0x040029BD RID: 10685
		private bool _UseWinInet;

		// Token: 0x040029BE RID: 10686
		private SingleItemRequestCache.FrozenCacheEntry _Entry;

		// Token: 0x02000585 RID: 1413
		private sealed class FrozenCacheEntry : RequestCacheEntry
		{
			// Token: 0x06002B5D RID: 11101 RVA: 0x000BC42E File Offset: 0x000BB42E
			public FrozenCacheEntry(string key, RequestCacheEntry entry, Stream stream) : this(key, entry, SingleItemRequestCache.FrozenCacheEntry.GetBytes(stream))
			{
			}

			// Token: 0x06002B5E RID: 11102 RVA: 0x000BC440 File Offset: 0x000BB440
			public FrozenCacheEntry(string key, RequestCacheEntry entry, byte[] streamBytes)
			{
				this._Key = key;
				this._StreamBytes = streamBytes;
				base.IsPrivateEntry = entry.IsPrivateEntry;
				base.StreamSize = entry.StreamSize;
				base.ExpiresUtc = entry.ExpiresUtc;
				base.HitCount = entry.HitCount;
				base.LastAccessedUtc = entry.LastAccessedUtc;
				entry.LastModifiedUtc = entry.LastModifiedUtc;
				base.LastSynchronizedUtc = entry.LastSynchronizedUtc;
				base.MaxStale = entry.MaxStale;
				base.UsageCount = entry.UsageCount;
				base.IsPartialEntry = entry.IsPartialEntry;
				base.EntryMetadata = entry.EntryMetadata;
				base.SystemMetadata = entry.SystemMetadata;
			}

			// Token: 0x06002B5F RID: 11103 RVA: 0x000BC4F4 File Offset: 0x000BB4F4
			private static byte[] GetBytes(Stream stream)
			{
				bool flag = false;
				byte[] array;
				if (stream.CanSeek)
				{
					array = new byte[stream.Length];
				}
				else
				{
					flag = true;
					array = new byte[8192];
				}
				int num = 0;
				for (;;)
				{
					int num2 = stream.Read(array, num, array.Length - num);
					if (num2 == 0)
					{
						break;
					}
					if ((num += num2) == array.Length && flag)
					{
						byte[] array2 = new byte[array.Length + 8192];
						Buffer.BlockCopy(array, 0, array2, 0, num);
						array = array2;
					}
				}
				if (flag)
				{
					byte[] array3 = new byte[num];
					Buffer.BlockCopy(array, 0, array3, 0, num);
					array = array3;
				}
				return array;
			}

			// Token: 0x06002B60 RID: 11104 RVA: 0x000BC581 File Offset: 0x000BB581
			public static SingleItemRequestCache.FrozenCacheEntry Create(SingleItemRequestCache.FrozenCacheEntry clonedObject)
			{
				if (clonedObject != null)
				{
					return (SingleItemRequestCache.FrozenCacheEntry)clonedObject.MemberwiseClone();
				}
				return null;
			}

			// Token: 0x170008FD RID: 2301
			// (get) Token: 0x06002B61 RID: 11105 RVA: 0x000BC593 File Offset: 0x000BB593
			public byte[] StreamBytes
			{
				get
				{
					return this._StreamBytes;
				}
			}

			// Token: 0x170008FE RID: 2302
			// (get) Token: 0x06002B62 RID: 11106 RVA: 0x000BC59B File Offset: 0x000BB59B
			public string Key
			{
				get
				{
					return this._Key;
				}
			}

			// Token: 0x040029BF RID: 10687
			private byte[] _StreamBytes;

			// Token: 0x040029C0 RID: 10688
			private string _Key;
		}

		// Token: 0x02000586 RID: 1414
		internal class ReadOnlyStream : Stream
		{
			// Token: 0x06002B63 RID: 11107 RVA: 0x000BC5A4 File Offset: 0x000BB5A4
			internal ReadOnlyStream(byte[] bytes)
			{
				this._Bytes = bytes;
				this._Offset = 0;
				this._Disposed = false;
				this._ReadTimeout = (this._WriteTimeout = -1);
			}

			// Token: 0x170008FF RID: 2303
			// (get) Token: 0x06002B64 RID: 11108 RVA: 0x000BC5DC File Offset: 0x000BB5DC
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000900 RID: 2304
			// (get) Token: 0x06002B65 RID: 11109 RVA: 0x000BC5DF File Offset: 0x000BB5DF
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000901 RID: 2305
			// (get) Token: 0x06002B66 RID: 11110 RVA: 0x000BC5E2 File Offset: 0x000BB5E2
			public override bool CanTimeout
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000902 RID: 2306
			// (get) Token: 0x06002B67 RID: 11111 RVA: 0x000BC5E5 File Offset: 0x000BB5E5
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000903 RID: 2307
			// (get) Token: 0x06002B68 RID: 11112 RVA: 0x000BC5E8 File Offset: 0x000BB5E8
			public override long Length
			{
				get
				{
					return (long)this._Bytes.Length;
				}
			}

			// Token: 0x17000904 RID: 2308
			// (get) Token: 0x06002B69 RID: 11113 RVA: 0x000BC5F3 File Offset: 0x000BB5F3
			// (set) Token: 0x06002B6A RID: 11114 RVA: 0x000BC5FC File Offset: 0x000BB5FC
			public override long Position
			{
				get
				{
					return (long)this._Offset;
				}
				set
				{
					if (value < 0L || value > (long)this._Bytes.Length)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					this._Offset = (int)value;
				}
			}

			// Token: 0x17000905 RID: 2309
			// (get) Token: 0x06002B6B RID: 11115 RVA: 0x000BC622 File Offset: 0x000BB622
			// (set) Token: 0x06002B6C RID: 11116 RVA: 0x000BC62A File Offset: 0x000BB62A
			public override int ReadTimeout
			{
				get
				{
					return this._ReadTimeout;
				}
				set
				{
					if (value <= 0 && value != -1)
					{
						throw new ArgumentOutOfRangeException(SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._ReadTimeout = value;
				}
			}

			// Token: 0x17000906 RID: 2310
			// (get) Token: 0x06002B6D RID: 11117 RVA: 0x000BC64B File Offset: 0x000BB64B
			// (set) Token: 0x06002B6E RID: 11118 RVA: 0x000BC653 File Offset: 0x000BB653
			public override int WriteTimeout
			{
				get
				{
					return this._WriteTimeout;
				}
				set
				{
					if (value <= 0 && value != -1)
					{
						throw new ArgumentOutOfRangeException(SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._WriteTimeout = value;
				}
			}

			// Token: 0x06002B6F RID: 11119 RVA: 0x000BC674 File Offset: 0x000BB674
			public override void Flush()
			{
			}

			// Token: 0x06002B70 RID: 11120 RVA: 0x000BC678 File Offset: 0x000BB678
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				int num = this.Read(buffer, offset, count);
				LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, callback);
				lazyAsyncResult.InvokeCallback(num);
				return lazyAsyncResult;
			}

			// Token: 0x06002B71 RID: 11121 RVA: 0x000BC6A8 File Offset: 0x000BB6A8
			public override int EndRead(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
					{
						"EndRead"
					}));
				}
				lazyAsyncResult.EndCalled = true;
				return (int)lazyAsyncResult.InternalWaitForCompletion();
			}

			// Token: 0x06002B72 RID: 11122 RVA: 0x000BC704 File Offset: 0x000BB704
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (this._Disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0 || offset > buffer.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (count < 0 || count > buffer.Length - offset)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (this._Offset == this._Bytes.Length)
				{
					return 0;
				}
				int num = this._Offset;
				count = Math.Min(count, this._Bytes.Length - num);
				System.Buffer.BlockCopy(this._Bytes, num, buffer, offset, count);
				num += count;
				this._Offset = num;
				return count;
			}

			// Token: 0x06002B73 RID: 11123 RVA: 0x000BC7AA File Offset: 0x000BB7AA
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06002B74 RID: 11124 RVA: 0x000BC7BB File Offset: 0x000BB7BB
			public override void EndWrite(IAsyncResult asyncResult)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06002B75 RID: 11125 RVA: 0x000BC7CC File Offset: 0x000BB7CC
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06002B76 RID: 11126 RVA: 0x000BC7E0 File Offset: 0x000BB7E0
			public override long Seek(long offset, SeekOrigin origin)
			{
				switch (origin)
				{
				case SeekOrigin.Begin:
					this.Position = offset;
					return offset;
				case SeekOrigin.Current:
					return this.Position += offset;
				case SeekOrigin.End:
					return this.Position = (long)this._Bytes.Length - offset;
				default:
					throw new ArgumentException(SR.GetString("net_invalid_enum", new object[]
					{
						"SeekOrigin"
					}), "origin");
				}
			}

			// Token: 0x06002B77 RID: 11127 RVA: 0x000BC85C File Offset: 0x000BB85C
			public override void SetLength(long length)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06002B78 RID: 11128 RVA: 0x000BC870 File Offset: 0x000BB870
			protected override void Dispose(bool disposing)
			{
				try
				{
					this._Disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x17000907 RID: 2311
			// (get) Token: 0x06002B79 RID: 11129 RVA: 0x000BC8A0 File Offset: 0x000BB8A0
			internal byte[] Buffer
			{
				get
				{
					return this._Bytes;
				}
			}

			// Token: 0x040029C1 RID: 10689
			private byte[] _Bytes;

			// Token: 0x040029C2 RID: 10690
			private int _Offset;

			// Token: 0x040029C3 RID: 10691
			private bool _Disposed;

			// Token: 0x040029C4 RID: 10692
			private int _ReadTimeout;

			// Token: 0x040029C5 RID: 10693
			private int _WriteTimeout;
		}

		// Token: 0x02000587 RID: 1415
		private class WriteOnlyStream : Stream
		{
			// Token: 0x06002B7A RID: 11130 RVA: 0x000BC8A8 File Offset: 0x000BB8A8
			public WriteOnlyStream(string key, SingleItemRequestCache cache, RequestCacheEntry cacheEntry, Stream realWriteStream)
			{
				this._Key = key;
				this._Cache = cache;
				this._TempEntry = cacheEntry;
				this._RealStream = realWriteStream;
				this._Buffers = new ArrayList();
			}

			// Token: 0x17000908 RID: 2312
			// (get) Token: 0x06002B7B RID: 11131 RVA: 0x000BC8D8 File Offset: 0x000BB8D8
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x06002B7C RID: 11132 RVA: 0x000BC8DB File Offset: 0x000BB8DB
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700090A RID: 2314
			// (get) Token: 0x06002B7D RID: 11133 RVA: 0x000BC8DE File Offset: 0x000BB8DE
			public override bool CanTimeout
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700090B RID: 2315
			// (get) Token: 0x06002B7E RID: 11134 RVA: 0x000BC8E1 File Offset: 0x000BB8E1
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700090C RID: 2316
			// (get) Token: 0x06002B7F RID: 11135 RVA: 0x000BC8E4 File Offset: 0x000BB8E4
			public override long Length
			{
				get
				{
					throw new NotSupportedException(SR.GetString("net_writeonlystream"));
				}
			}

			// Token: 0x1700090D RID: 2317
			// (get) Token: 0x06002B80 RID: 11136 RVA: 0x000BC8F5 File Offset: 0x000BB8F5
			// (set) Token: 0x06002B81 RID: 11137 RVA: 0x000BC906 File Offset: 0x000BB906
			public override long Position
			{
				get
				{
					throw new NotSupportedException(SR.GetString("net_writeonlystream"));
				}
				set
				{
					throw new NotSupportedException(SR.GetString("net_writeonlystream"));
				}
			}

			// Token: 0x1700090E RID: 2318
			// (get) Token: 0x06002B82 RID: 11138 RVA: 0x000BC917 File Offset: 0x000BB917
			// (set) Token: 0x06002B83 RID: 11139 RVA: 0x000BC91F File Offset: 0x000BB91F
			public override int ReadTimeout
			{
				get
				{
					return this._ReadTimeout;
				}
				set
				{
					if (value <= 0 && value != -1)
					{
						throw new ArgumentOutOfRangeException(SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._ReadTimeout = value;
				}
			}

			// Token: 0x1700090F RID: 2319
			// (get) Token: 0x06002B84 RID: 11140 RVA: 0x000BC940 File Offset: 0x000BB940
			// (set) Token: 0x06002B85 RID: 11141 RVA: 0x000BC948 File Offset: 0x000BB948
			public override int WriteTimeout
			{
				get
				{
					return this._WriteTimeout;
				}
				set
				{
					if (value <= 0 && value != -1)
					{
						throw new ArgumentOutOfRangeException(SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._WriteTimeout = value;
				}
			}

			// Token: 0x06002B86 RID: 11142 RVA: 0x000BC969 File Offset: 0x000BB969
			public override void Flush()
			{
			}

			// Token: 0x06002B87 RID: 11143 RVA: 0x000BC96B File Offset: 0x000BB96B
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06002B88 RID: 11144 RVA: 0x000BC97C File Offset: 0x000BB97C
			public override int EndRead(IAsyncResult asyncResult)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06002B89 RID: 11145 RVA: 0x000BC98D File Offset: 0x000BB98D
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06002B8A RID: 11146 RVA: 0x000BC99E File Offset: 0x000BB99E
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06002B8B RID: 11147 RVA: 0x000BC9AF File Offset: 0x000BB9AF
			public override void SetLength(long length)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06002B8C RID: 11148 RVA: 0x000BC9C0 File Offset: 0x000BB9C0
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.Write(buffer, offset, count);
				LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, callback);
				lazyAsyncResult.InvokeCallback(null);
				return lazyAsyncResult;
			}

			// Token: 0x06002B8D RID: 11149 RVA: 0x000BC9EC File Offset: 0x000BB9EC
			public override void EndWrite(IAsyncResult asyncResult)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
					{
						"EndWrite"
					}));
				}
				lazyAsyncResult.EndCalled = true;
				lazyAsyncResult.InternalWaitForCompletion();
			}

			// Token: 0x06002B8E RID: 11150 RVA: 0x000BCA44 File Offset: 0x000BBA44
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this._Disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				if (buffer == null)
				{
					throw new ArgumentNullException("buffer");
				}
				if (offset < 0 || offset > buffer.Length)
				{
					throw new ArgumentOutOfRangeException("offset");
				}
				if (count < 0 || count > buffer.Length - offset)
				{
					throw new ArgumentOutOfRangeException("count");
				}
				if (this._RealStream != null)
				{
					try
					{
						this._RealStream.Write(buffer, offset, count);
					}
					catch
					{
						this._RealStream.Close();
						this._RealStream = null;
					}
				}
				byte[] array = new byte[count];
				Buffer.BlockCopy(buffer, offset, array, 0, count);
				this._Buffers.Add(array);
				this._TotalSize += (long)count;
			}

			// Token: 0x06002B8F RID: 11151 RVA: 0x000BCB10 File Offset: 0x000BBB10
			protected override void Dispose(bool disposing)
			{
				this._Disposed = true;
				base.Dispose(disposing);
				if (disposing)
				{
					if (this._RealStream != null)
					{
						try
						{
							this._RealStream.Close();
						}
						catch
						{
						}
					}
					byte[] array = new byte[this._TotalSize];
					int num = 0;
					for (int i = 0; i < this._Buffers.Count; i++)
					{
						byte[] array2 = (byte[])this._Buffers[i];
						Buffer.BlockCopy(array2, 0, array, num, array2.Length);
						num += array2.Length;
					}
					this._Cache.Commit(this._Key, this._TempEntry, array);
				}
			}

			// Token: 0x040029C6 RID: 10694
			private string _Key;

			// Token: 0x040029C7 RID: 10695
			private SingleItemRequestCache _Cache;

			// Token: 0x040029C8 RID: 10696
			private RequestCacheEntry _TempEntry;

			// Token: 0x040029C9 RID: 10697
			private Stream _RealStream;

			// Token: 0x040029CA RID: 10698
			private long _TotalSize;

			// Token: 0x040029CB RID: 10699
			private ArrayList _Buffers;

			// Token: 0x040029CC RID: 10700
			private bool _Disposed;

			// Token: 0x040029CD RID: 10701
			private int _ReadTimeout;

			// Token: 0x040029CE RID: 10702
			private int _WriteTimeout;
		}
	}
}
