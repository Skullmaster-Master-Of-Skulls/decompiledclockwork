using System;
using System.Collections.Specialized;
using System.IO;
using System.Threading;

namespace System.Net.Cache
{
	// Token: 0x0200057E RID: 1406
	internal class MetadataUpdateStream : Stream, ICloseEx
	{
		// Token: 0x06002AF1 RID: 10993 RVA: 0x000B6DCC File Offset: 0x000B5DCC
		internal MetadataUpdateStream(Stream parentStream, RequestCache cache, string key, DateTime expiresGMT, DateTime lastModifiedGMT, DateTime lastSynchronizedGMT, TimeSpan maxStale, StringCollection entryMetadata, StringCollection systemMetadata, bool isStrictCacheErrors)
		{
			if (parentStream == null)
			{
				throw new ArgumentNullException("parentStream");
			}
			this.m_ParentStream = parentStream;
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

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000B6E3A File Offset: 0x000B5E3A
		private MetadataUpdateStream(Stream parentStream, RequestCache cache, string key, bool isStrictCacheErrors)
		{
			if (parentStream == null)
			{
				throw new ArgumentNullException("parentStream");
			}
			this.m_ParentStream = parentStream;
			this.m_Cache = cache;
			this.m_Key = key;
			this.m_CacheDestroy = true;
			this.m_IsStrictCacheErrors = isStrictCacheErrors;
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000B6E74 File Offset: 0x000B5E74
		public override bool CanRead
		{
			get
			{
				return this.m_ParentStream.CanRead;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000B6E81 File Offset: 0x000B5E81
		public override bool CanSeek
		{
			get
			{
				return this.m_ParentStream.CanSeek;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x000B6E8E File Offset: 0x000B5E8E
		public override bool CanWrite
		{
			get
			{
				return this.m_ParentStream.CanWrite;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000B6E9B File Offset: 0x000B5E9B
		public override long Length
		{
			get
			{
				return this.m_ParentStream.Length;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x000B6EA8 File Offset: 0x000B5EA8
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x000B6EB5 File Offset: 0x000B5EB5
		public override long Position
		{
			get
			{
				return this.m_ParentStream.Position;
			}
			set
			{
				this.m_ParentStream.Position = value;
			}
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000B6EC3 File Offset: 0x000B5EC3
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.m_ParentStream.Seek(offset, origin);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000B6ED2 File Offset: 0x000B5ED2
		public override void SetLength(long value)
		{
			this.m_ParentStream.SetLength(value);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000B6EE0 File Offset: 0x000B5EE0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.m_ParentStream.Write(buffer, offset, count);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x000B6EF0 File Offset: 0x000B5EF0
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_ParentStream.BeginWrite(buffer, offset, count, callback, state);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000B6F04 File Offset: 0x000B5F04
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this.m_ParentStream.EndWrite(asyncResult);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000B6F12 File Offset: 0x000B5F12
		public override void Flush()
		{
			this.m_ParentStream.Flush();
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000B6F1F File Offset: 0x000B5F1F
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.m_ParentStream.Read(buffer, offset, count);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000B6F2F File Offset: 0x000B5F2F
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this.m_ParentStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x000B6F43 File Offset: 0x000B5F43
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this.m_ParentStream.EndRead(asyncResult);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000B6F51 File Offset: 0x000B5F51
		protected sealed override void Dispose(bool disposing)
		{
			this.Dispose(disposing, CloseExState.Normal);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000B6F61 File Offset: 0x000B5F61
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			this.Dispose(true, closeState);
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x000B6F6B File Offset: 0x000B5F6B
		public override bool CanTimeout
		{
			get
			{
				return this.m_ParentStream.CanTimeout;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x000B6F78 File Offset: 0x000B5F78
		// (set) Token: 0x06002B06 RID: 11014 RVA: 0x000B6F85 File Offset: 0x000B5F85
		public override int ReadTimeout
		{
			get
			{
				return this.m_ParentStream.ReadTimeout;
			}
			set
			{
				this.m_ParentStream.ReadTimeout = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002B07 RID: 11015 RVA: 0x000B6F93 File Offset: 0x000B5F93
		// (set) Token: 0x06002B08 RID: 11016 RVA: 0x000B6FA0 File Offset: 0x000B5FA0
		public override int WriteTimeout
		{
			get
			{
				return this.m_ParentStream.WriteTimeout;
			}
			set
			{
				this.m_ParentStream.WriteTimeout = value;
			}
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000B6FB0 File Offset: 0x000B5FB0
		protected virtual void Dispose(bool disposing, CloseExState closeState)
		{
			if (Interlocked.Increment(ref this._Disposed) == 1)
			{
				ICloseEx closeEx = this.m_ParentStream as ICloseEx;
				if (closeEx != null)
				{
					closeEx.CloseEx(closeState);
				}
				else
				{
					this.m_ParentStream.Close();
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
				if (!disposing)
				{
					this.m_Cache = null;
					this.m_Key = null;
					this.m_EntryMetadata = null;
					this.m_SystemMetadata = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0400299F RID: 10655
		private Stream m_ParentStream;

		// Token: 0x040029A0 RID: 10656
		private RequestCache m_Cache;

		// Token: 0x040029A1 RID: 10657
		private string m_Key;

		// Token: 0x040029A2 RID: 10658
		private DateTime m_Expires;

		// Token: 0x040029A3 RID: 10659
		private DateTime m_LastModified;

		// Token: 0x040029A4 RID: 10660
		private DateTime m_LastSynchronized;

		// Token: 0x040029A5 RID: 10661
		private TimeSpan m_MaxStale;

		// Token: 0x040029A6 RID: 10662
		private StringCollection m_EntryMetadata;

		// Token: 0x040029A7 RID: 10663
		private StringCollection m_SystemMetadata;

		// Token: 0x040029A8 RID: 10664
		private bool m_CacheDestroy;

		// Token: 0x040029A9 RID: 10665
		private bool m_IsStrictCacheErrors;

		// Token: 0x040029AA RID: 10666
		private int _Disposed;
	}
}
