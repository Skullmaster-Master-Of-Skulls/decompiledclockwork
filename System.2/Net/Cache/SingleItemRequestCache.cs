using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Microsoft.Win32;

namespace System.Net.Cache
{
	// Token: 0x02000321 RID: 801
	internal class SingleItemRequestCache : WinInetCache
	{
		// Token: 0x06001CD7 RID: 7383 RVA: 0x0008A4B7 File Offset: 0x000886B7
		internal SingleItemRequestCache(bool useWinInet) : base(true, true, false)
		{
			this._UseWinInet = useWinInet;
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x0008A4CC File Offset: 0x000886CC
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

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0008A510 File Offset: 0x00088710
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

		// Token: 0x06001CDA RID: 7386 RVA: 0x0008A55C File Offset: 0x0008875C
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

		// Token: 0x06001CDB RID: 7387 RVA: 0x0008A59C File Offset: 0x0008879C
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

		// Token: 0x06001CDC RID: 7388 RVA: 0x0008A5E4 File Offset: 0x000887E4
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

		// Token: 0x06001CDD RID: 7389 RVA: 0x0008A660 File Offset: 0x00088860
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

		// Token: 0x06001CDE RID: 7390 RVA: 0x0008A718 File Offset: 0x00088918
		private void Commit(string key, RequestCacheEntry tempEntry, byte[] allBytes)
		{
			SingleItemRequestCache.FrozenCacheEntry entry = new SingleItemRequestCache.FrozenCacheEntry(key, tempEntry, allBytes);
			this._Entry = entry;
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0008A738 File Offset: 0x00088938
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

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0008A784 File Offset: 0x00088984
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

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0008A7F4 File Offset: 0x000889F4
		internal override void UnlockEntry(Stream stream)
		{
		}

		// Token: 0x04001BBB RID: 7099
		private bool _UseWinInet;

		// Token: 0x04001BBC RID: 7100
		private SingleItemRequestCache.FrozenCacheEntry _Entry;

		// Token: 0x020007BD RID: 1981
		private sealed class FrozenCacheEntry : RequestCacheEntry
		{
			// Token: 0x06004356 RID: 17238 RVA: 0x0011C7E9 File Offset: 0x0011A9E9
			public FrozenCacheEntry(string key, RequestCacheEntry entry, Stream stream) : this(key, entry, SingleItemRequestCache.FrozenCacheEntry.GetBytes(stream))
			{
			}

			// Token: 0x06004357 RID: 17239 RVA: 0x0011C7FC File Offset: 0x0011A9FC
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

			// Token: 0x06004358 RID: 17240 RVA: 0x0011C8B0 File Offset: 0x0011AAB0
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

			// Token: 0x06004359 RID: 17241 RVA: 0x0011C93E File Offset: 0x0011AB3E
			public static SingleItemRequestCache.FrozenCacheEntry Create(SingleItemRequestCache.FrozenCacheEntry clonedObject)
			{
				if (clonedObject != null)
				{
					return (SingleItemRequestCache.FrozenCacheEntry)clonedObject.MemberwiseClone();
				}
				return null;
			}

			// Token: 0x17000F46 RID: 3910
			// (get) Token: 0x0600435A RID: 17242 RVA: 0x0011C950 File Offset: 0x0011AB50
			public byte[] StreamBytes
			{
				get
				{
					return this._StreamBytes;
				}
			}

			// Token: 0x17000F47 RID: 3911
			// (get) Token: 0x0600435B RID: 17243 RVA: 0x0011C958 File Offset: 0x0011AB58
			public string Key
			{
				get
				{
					return this._Key;
				}
			}

			// Token: 0x04003465 RID: 13413
			private byte[] _StreamBytes;

			// Token: 0x04003466 RID: 13414
			private string _Key;
		}

		// Token: 0x020007BE RID: 1982
		internal class ReadOnlyStream : Stream, IRequestLifetimeTracker
		{
			// Token: 0x0600435C RID: 17244 RVA: 0x0011C960 File Offset: 0x0011AB60
			internal ReadOnlyStream(byte[] bytes)
			{
				this._Bytes = bytes;
				this._Offset = 0;
				this._Disposed = false;
				this._ReadTimeout = (this._WriteTimeout = -1);
			}

			// Token: 0x17000F48 RID: 3912
			// (get) Token: 0x0600435D RID: 17245 RVA: 0x0011C998 File Offset: 0x0011AB98
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F49 RID: 3913
			// (get) Token: 0x0600435E RID: 17246 RVA: 0x0011C99B File Offset: 0x0011AB9B
			public override bool CanSeek
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F4A RID: 3914
			// (get) Token: 0x0600435F RID: 17247 RVA: 0x0011C99E File Offset: 0x0011AB9E
			public override bool CanTimeout
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F4B RID: 3915
			// (get) Token: 0x06004360 RID: 17248 RVA: 0x0011C9A1 File Offset: 0x0011ABA1
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F4C RID: 3916
			// (get) Token: 0x06004361 RID: 17249 RVA: 0x0011C9A4 File Offset: 0x0011ABA4
			public override long Length
			{
				get
				{
					return (long)this._Bytes.Length;
				}
			}

			// Token: 0x17000F4D RID: 3917
			// (get) Token: 0x06004362 RID: 17250 RVA: 0x0011C9AF File Offset: 0x0011ABAF
			// (set) Token: 0x06004363 RID: 17251 RVA: 0x0011C9B8 File Offset: 0x0011ABB8
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

			// Token: 0x17000F4E RID: 3918
			// (get) Token: 0x06004364 RID: 17252 RVA: 0x0011C9DE File Offset: 0x0011ABDE
			// (set) Token: 0x06004365 RID: 17253 RVA: 0x0011C9E6 File Offset: 0x0011ABE6
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
						throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._ReadTimeout = value;
				}
			}

			// Token: 0x17000F4F RID: 3919
			// (get) Token: 0x06004366 RID: 17254 RVA: 0x0011CA0C File Offset: 0x0011AC0C
			// (set) Token: 0x06004367 RID: 17255 RVA: 0x0011CA14 File Offset: 0x0011AC14
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
						throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._WriteTimeout = value;
				}
			}

			// Token: 0x06004368 RID: 17256 RVA: 0x0011CA3A File Offset: 0x0011AC3A
			public override void Flush()
			{
			}

			// Token: 0x06004369 RID: 17257 RVA: 0x0011CA3C File Offset: 0x0011AC3C
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				int num = this.Read(buffer, offset, count);
				LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, callback);
				lazyAsyncResult.InvokeCallback(num);
				return lazyAsyncResult;
			}

			// Token: 0x0600436A RID: 17258 RVA: 0x0011CA6C File Offset: 0x0011AC6C
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

			// Token: 0x0600436B RID: 17259 RVA: 0x0011CAC8 File Offset: 0x0011ACC8
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

			// Token: 0x0600436C RID: 17260 RVA: 0x0011CB6E File Offset: 0x0011AD6E
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x0600436D RID: 17261 RVA: 0x0011CB7F File Offset: 0x0011AD7F
			public override void EndWrite(IAsyncResult asyncResult)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x0600436E RID: 17262 RVA: 0x0011CB90 File Offset: 0x0011AD90
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x0600436F RID: 17263 RVA: 0x0011CBA4 File Offset: 0x0011ADA4
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

			// Token: 0x06004370 RID: 17264 RVA: 0x0011CC19 File Offset: 0x0011AE19
			public override void SetLength(long length)
			{
				throw new NotSupportedException(SR.GetString("net_readonlystream"));
			}

			// Token: 0x06004371 RID: 17265 RVA: 0x0011CC2C File Offset: 0x0011AE2C
			protected override void Dispose(bool disposing)
			{
				try
				{
					if (!this._Disposed)
					{
						this._Disposed = true;
						if (disposing)
						{
							RequestLifetimeSetter.Report(this.m_RequestLifetimeSetter);
						}
					}
				}
				finally
				{
					base.Dispose(disposing);
				}
			}

			// Token: 0x17000F50 RID: 3920
			// (get) Token: 0x06004372 RID: 17266 RVA: 0x0011CC70 File Offset: 0x0011AE70
			internal byte[] Buffer
			{
				get
				{
					return this._Bytes;
				}
			}

			// Token: 0x06004373 RID: 17267 RVA: 0x0011CC78 File Offset: 0x0011AE78
			void IRequestLifetimeTracker.TrackRequestLifetime(long requestStartTimestamp)
			{
				this.m_RequestLifetimeSetter = new RequestLifetimeSetter(requestStartTimestamp);
			}

			// Token: 0x04003467 RID: 13415
			private byte[] _Bytes;

			// Token: 0x04003468 RID: 13416
			private int _Offset;

			// Token: 0x04003469 RID: 13417
			private bool _Disposed;

			// Token: 0x0400346A RID: 13418
			private int _ReadTimeout;

			// Token: 0x0400346B RID: 13419
			private int _WriteTimeout;

			// Token: 0x0400346C RID: 13420
			private RequestLifetimeSetter m_RequestLifetimeSetter;
		}

		// Token: 0x020007BF RID: 1983
		private class WriteOnlyStream : Stream
		{
			// Token: 0x06004374 RID: 17268 RVA: 0x0011CC86 File Offset: 0x0011AE86
			public WriteOnlyStream(string key, SingleItemRequestCache cache, RequestCacheEntry cacheEntry, Stream realWriteStream)
			{
				this._Key = key;
				this._Cache = cache;
				this._TempEntry = cacheEntry;
				this._RealStream = realWriteStream;
				this._Buffers = new ArrayList();
			}

			// Token: 0x17000F51 RID: 3921
			// (get) Token: 0x06004375 RID: 17269 RVA: 0x0011CCB6 File Offset: 0x0011AEB6
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F52 RID: 3922
			// (get) Token: 0x06004376 RID: 17270 RVA: 0x0011CCB9 File Offset: 0x0011AEB9
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F53 RID: 3923
			// (get) Token: 0x06004377 RID: 17271 RVA: 0x0011CCBC File Offset: 0x0011AEBC
			public override bool CanTimeout
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F54 RID: 3924
			// (get) Token: 0x06004378 RID: 17272 RVA: 0x0011CCBF File Offset: 0x0011AEBF
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F55 RID: 3925
			// (get) Token: 0x06004379 RID: 17273 RVA: 0x0011CCC2 File Offset: 0x0011AEC2
			public override long Length
			{
				get
				{
					throw new NotSupportedException(SR.GetString("net_writeonlystream"));
				}
			}

			// Token: 0x17000F56 RID: 3926
			// (get) Token: 0x0600437A RID: 17274 RVA: 0x0011CCD3 File Offset: 0x0011AED3
			// (set) Token: 0x0600437B RID: 17275 RVA: 0x0011CCE4 File Offset: 0x0011AEE4
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

			// Token: 0x17000F57 RID: 3927
			// (get) Token: 0x0600437C RID: 17276 RVA: 0x0011CCF5 File Offset: 0x0011AEF5
			// (set) Token: 0x0600437D RID: 17277 RVA: 0x0011CCFD File Offset: 0x0011AEFD
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
						throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._ReadTimeout = value;
				}
			}

			// Token: 0x17000F58 RID: 3928
			// (get) Token: 0x0600437E RID: 17278 RVA: 0x0011CD23 File Offset: 0x0011AF23
			// (set) Token: 0x0600437F RID: 17279 RVA: 0x0011CD2B File Offset: 0x0011AF2B
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
						throw new ArgumentOutOfRangeException("value", SR.GetString("net_io_timeout_use_gt_zero"));
					}
					this._WriteTimeout = value;
				}
			}

			// Token: 0x06004380 RID: 17280 RVA: 0x0011CD51 File Offset: 0x0011AF51
			public override void Flush()
			{
			}

			// Token: 0x06004381 RID: 17281 RVA: 0x0011CD53 File Offset: 0x0011AF53
			public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06004382 RID: 17282 RVA: 0x0011CD64 File Offset: 0x0011AF64
			public override int EndRead(IAsyncResult asyncResult)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06004383 RID: 17283 RVA: 0x0011CD75 File Offset: 0x0011AF75
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06004384 RID: 17284 RVA: 0x0011CD86 File Offset: 0x0011AF86
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06004385 RID: 17285 RVA: 0x0011CD97 File Offset: 0x0011AF97
			public override void SetLength(long length)
			{
				throw new NotSupportedException(SR.GetString("net_writeonlystream"));
			}

			// Token: 0x06004386 RID: 17286 RVA: 0x0011CDA8 File Offset: 0x0011AFA8
			public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
			{
				this.Write(buffer, offset, count);
				LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(null, state, callback);
				lazyAsyncResult.InvokeCallback(null);
				return lazyAsyncResult;
			}

			// Token: 0x06004387 RID: 17287 RVA: 0x0011CDD4 File Offset: 0x0011AFD4
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

			// Token: 0x06004388 RID: 17288 RVA: 0x0011CE2C File Offset: 0x0011B02C
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

			// Token: 0x06004389 RID: 17289 RVA: 0x0011CEF8 File Offset: 0x0011B0F8
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

			// Token: 0x0400346D RID: 13421
			private string _Key;

			// Token: 0x0400346E RID: 13422
			private SingleItemRequestCache _Cache;

			// Token: 0x0400346F RID: 13423
			private RequestCacheEntry _TempEntry;

			// Token: 0x04003470 RID: 13424
			private Stream _RealStream;

			// Token: 0x04003471 RID: 13425
			private long _TotalSize;

			// Token: 0x04003472 RID: 13426
			private ArrayList _Buffers;

			// Token: 0x04003473 RID: 13427
			private bool _Disposed;

			// Token: 0x04003474 RID: 13428
			private int _ReadTimeout;

			// Token: 0x04003475 RID: 13429
			private int _WriteTimeout;
		}
	}
}
