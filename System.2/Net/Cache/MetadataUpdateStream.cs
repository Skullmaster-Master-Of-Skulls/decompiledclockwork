using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200031D RID: 797
	internal class MetadataUpdateStream : BaseWrapperStream, ICloseEx
	{
		// Token: 0x06001C8A RID: 7306 RVA: 0x00087A5C File Offset: 0x00085C5C
		internal MetadataUpdateStream(Stream parentStream, RequestCache cache, string key, DateTime expiresGMT, DateTime lastModifiedGMT, DateTime lastSynchronizedGMT, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, bool isStrictCacheErrors) : base(parentStream)
		{
			this.m_Cache = cache;
			this.m_Key = key;
			this.m_Expires = expiresGMT;
			this.m_LastModified = lastModifiedGMT;
			this.m_LastSynchronized = lastSynchronizedGMT;
			this.m_MaxStale = maxStale;
			this.m_EntryMetadata = entryMetadata;
			this.m_SystemMetadata = systemMetadata;
			this.m_IsStrictCacheErrors = isStrictCacheErrors;
		}

		// Token: 0x06001C8B RID: 7307 RVA: 0x00087AB6 File Offset: 0x00085CB6
		private MetadataUpdateStream(Stream parentStream, RequestCache cache, string key, bool isStrictCacheErrors) : base(parentStream)
		{
			this.m_Cache = cache;
			this.m_Key = key;
			this.m_CacheDestroy = true;
			this.m_IsStrictCacheErrors = isStrictCacheErrors;
		}

		// Token: 0x17000709 RID: 1801
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00087ADC File Offset: 0x00085CDC
		public override bool CanRead
		{
			get
			{
				return base.WrappedStream.CanRead;
			}
		}

		// Token: 0x1700070A RID: 1802
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x00087AE9 File Offset: 0x00085CE9
		public override bool CanSeek
		{
			get
			{
				return base.WrappedStream.CanSeek;
			}
		}

		// Token: 0x1700070B RID: 1803
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x00087AF6 File Offset: 0x00085CF6
		public override bool CanWrite
		{
			get
			{
				return base.WrappedStream.CanWrite;
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x00087B03 File Offset: 0x00085D03
		public override long Length
		{
			get
			{
				return base.WrappedStream.Length;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x00087B10 File Offset: 0x00085D10
		// (set) Token: 0x06001C91 RID: 7313 RVA: 0x00087B1D File Offset: 0x00085D1D
		public override long Position
		{
			get
			{
				return base.WrappedStream.Position;
			}
			set
			{
				base.WrappedStream.Position = value;
			}
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00087B2B File Offset: 0x00085D2B
		public override long Seek(long offset, SeekOrigin origin)
		{
			return base.WrappedStream.Seek(offset, origin);
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00087B3A File Offset: 0x00085D3A
		public override void SetLength(long value)
		{
			base.WrappedStream.SetLength(value);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00087B48 File Offset: 0x00085D48
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.WrappedStream.Write(buffer, offset, count);
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00087B58 File Offset: 0x00085D58
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.WrappedStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00087B6C File Offset: 0x00085D6C
		public override void EndWrite(IAsyncResult asyncResult)
		{
			base.WrappedStream.EndWrite(asyncResult);
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00087B7A File Offset: 0x00085D7A
		public override void Flush()
		{
			base.WrappedStream.Flush();
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00087B87 File Offset: 0x00085D87
		public override int Read(byte[] buffer, int offset, int count)
		{
			return base.WrappedStream.Read(buffer, offset, count);
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00087B97 File Offset: 0x00085D97
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return base.WrappedStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001C9A RID: 7322 RVA: 0x00087BAB File Offset: 0x00085DAB
		public override int EndRead(IAsyncResult asyncResult)
		{
			return base.WrappedStream.EndRead(asyncResult);
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x00087BB9 File Offset: 0x00085DB9
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00087BC3 File Offset: 0x00085DC3
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00087BCD File Offset: 0x00085DCD
		public override bool CanTimeout
		{
			get
			{
				return base.WrappedStream.CanTimeout;
			}
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00087BDA File Offset: 0x00085DDA
		// (set) Token: 0x06001C9F RID: 7327 RVA: 0x00087BE7 File Offset: 0x00085DE7
		public override int ReadTimeout
		{
			get
			{
				return base.WrappedStream.ReadTimeout;
			}
			set
			{
				base.WrappedStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00087BF5 File Offset: 0x00085DF5
		// (set) Token: 0x06001CA1 RID: 7329 RVA: 0x00087C02 File Offset: 0x00085E02
		public override int WriteTimeout
		{
			get
			{
				return base.WrappedStream.WriteTimeout;
			}
			set
			{
				base.WrappedStream.WriteTimeout = value;
			}
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00087C10 File Offset: 0x00085E10
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			try
			{
				if (Interlocked.Increment(ref this._Disposed) == 1 && disposing)
				{
					ICloseEx closeEx = base.WrappedStream as ICloseEx;
					if (closeEx != null)
					{
						closeEx.CloseEx(closeState);
					}
					else
					{
						base.WrappedStream.Close();
					}
					if (this.m_CacheDestroy)
					{
						if (this.m_IsStrictCacheErrors)
						{
							this.m_Cache.Remove(this.m_Key);
						}
						else
						{
							this.m_Cache.TryRemove(this.m_Key);
						}
					}
					else if (this.m_IsStrictCacheErrors)
					{
						this.m_Cache.Update(this.m_Key, this.m_Expires, this.m_LastModified, this.m_LastSynchronized, this.m_MaxStale, this.m_EntryMetadata, this.m_SystemMetadata);
					}
					else
					{
						this.m_Cache.TryUpdate(this.m_Key, this.m_Expires, this.m_LastModified, this.m_LastSynchronized, this.m_MaxStale, this.m_EntryMetadata, this.m_SystemMetadata);
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x04001BA5 RID: 7077
		private RequestCache m_Cache;

		// Token: 0x04001BA6 RID: 7078
		private string m_Key;

		// Token: 0x04001BA7 RID: 7079
		private DateTime m_Expires;

		// Token: 0x04001BA8 RID: 7080
		private DateTime m_LastModified;

		// Token: 0x04001BA9 RID: 7081
		private DateTime m_LastSynchronized;

		// Token: 0x04001BAA RID: 7082
		private TimeSpan m_MaxStale;

		// Token: 0x04001BAB RID: 7083
		private StringCollection m_EntryMetadata;

		// Token: 0x04001BAC RID: 7084
		private StringCollection m_SystemMetadata;

		// Token: 0x04001BAD RID: 7085
		private bool m_CacheDestroy;

		// Token: 0x04001BAE RID: 7086
		private bool m_IsStrictCacheErrors;

		// Token: 0x04001BAF RID: 7087
		private int _Disposed;
	}
}
