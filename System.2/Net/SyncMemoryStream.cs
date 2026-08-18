using System;
using System.IO;

namespace System.Net
{
	// Token: 0x020001A7 RID: 423
	internal sealed class SyncMemoryStream : MemoryStream, IRequestLifetimeTracker
	{
		// Token: 0x060010AF RID: 4271 RVA: 0x00059B74 File Offset: 0x00057D74
		internal SyncMemoryStream(byte[] bytes) : base(bytes, false)
		{
			this.m_ReadTimeout = (this.m_WriteTimeout = -1);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00059B9C File Offset: 0x00057D9C
		internal SyncMemoryStream(int initialCapacity) : base(initialCapacity)
		{
			this.m_ReadTimeout = (this.m_WriteTimeout = -1);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00059BC0 File Offset: 0x00057DC0
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			int num = this.Read(buffer, offset, count);
			return new LazyAsyncResult(null, state, callback, num);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00059BE8 File Offset: 0x00057DE8
		public override int EndRead(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			return (int)lazyAsyncResult.InternalWaitForCompletion();
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00059C07 File Offset: 0x00057E07
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			this.Write(buffer, offset, count);
			return new LazyAsyncResult(null, state, callback, null);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00059C20 File Offset: 0x00057E20
		public override void EndWrite(IAsyncResult asyncResult)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)asyncResult;
			lazyAsyncResult.InternalWaitForCompletion();
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060010B5 RID: 4277 RVA: 0x00059C3B File Offset: 0x00057E3B
		public override bool CanTimeout
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x00059C3E File Offset: 0x00057E3E
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x00059C46 File Offset: 0x00057E46
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

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x00059C4F File Offset: 0x00057E4F
		// (set) Token: 0x060010B9 RID: 4281 RVA: 0x00059C57 File Offset: 0x00057E57
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

		// Token: 0x060010BA RID: 4282 RVA: 0x00059C60 File Offset: 0x00057E60
		public void TrackRequestLifetime(long requestStartTimestamp)
		{
			this.m_RequestLifetimeSetter = new RequestLifetimeSetter(requestStartTimestamp);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00059C6E File Offset: 0x00057E6E
		protected override void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			this.m_Disposed = true;
			if (disposing)
			{
				RequestLifetimeSetter.Report(this.m_RequestLifetimeSetter);
			}
			base.Dispose(disposing);
		}

		// Token: 0x040013A8 RID: 5032
		private int m_ReadTimeout;

		// Token: 0x040013A9 RID: 5033
		private int m_WriteTimeout;

		// Token: 0x040013AA RID: 5034
		private RequestLifetimeSetter m_RequestLifetimeSetter;

		// Token: 0x040013AB RID: 5035
		private bool m_Disposed;
	}
}
