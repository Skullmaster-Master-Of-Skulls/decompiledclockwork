using System;
using System.IO;

namespace System.Net
{
	// Token: 0x020004D1 RID: 1233
	internal sealed class SyncMemoryStream : MemoryStream
	{
		// Token: 0x06002663 RID: 9827 RVA: 0x0009C314 File Offset: 0x0009B314
		internal SyncMemoryStream(byte[] bytes) : base(bytes, false)
		{
			this.m_ReadTimeout = (this.m_WriteTimeout = -1);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0009C33C File Offset: 0x0009B33C
		internal SyncMemoryStream(int initialCapacity) : base(initialCapacity)
		{
			this.m_ReadTimeout = (this.m_WriteTimeout = -1);
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0009C360 File Offset: 0x0009B360
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			int num = this.Read(buffer, offset, count);
			return new LazyAsyncResult(null, state, callback, num);
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x0009C388 File Offset: 0x0009B388
		public override int EndRead(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			return (int)lazyAsyncResult.InternalWaitForCompletion();
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x0009C3A7 File Offset: 0x0009B3A7
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.Write(buffer, offset, count);
			return new LazyAsyncResult(null, state, callback, null);
		}

		// Token: 0x06002668 RID: 9832 RVA: 0x0009C3C0 File Offset: 0x0009B3C0
		public override void EndWrite(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			lazyAsyncResult.InternalWaitForCompletion();
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002669 RID: 9833 RVA: 0x0009C3DB File Offset: 0x0009B3DB
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600266A RID: 9834 RVA: 0x0009C3DE File Offset: 0x0009B3DE
		// (set) Token: 0x0600266B RID: 9835 RVA: 0x0009C3E6 File Offset: 0x0009B3E6
		public override int ReadTimeout
		{
			get
			{
				return this.m_ReadTimeout;
			}
			set
			{
				this.m_ReadTimeout = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600266C RID: 9836 RVA: 0x0009C3EF File Offset: 0x0009B3EF
		// (set) Token: 0x0600266D RID: 9837 RVA: 0x0009C3F7 File Offset: 0x0009B3F7
		public override int WriteTimeout
		{
			get
			{
				return this.m_WriteTimeout;
			}
			set
			{
				this.m_WriteTimeout = value;
			}
		}

		// Token: 0x040025F3 RID: 9715
		private int m_ReadTimeout;

		// Token: 0x040025F4 RID: 9716
		private int m_WriteTimeout;
	}
}
