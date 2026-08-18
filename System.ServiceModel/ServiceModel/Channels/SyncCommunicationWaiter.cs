using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000758 RID: 1880
	internal class SyncCommunicationWaiter : ICommunicationWaiter, IDisposable
	{
		// Token: 0x060047D1 RID: 18385 RVA: 0x0010A31C File Offset: 0x0010851C
		public SyncCommunicationWaiter(object mutex)
		{
			this.mutex = mutex;
			this.waitHandle = new ManualResetEvent(false);
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x060047D2 RID: 18386 RVA: 0x0010A337 File Offset: 0x00108537
		private object ThisLock
		{
			get
			{
				return this.mutex;
			}
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0010A340 File Offset: 0x00108540
		public void Dispose()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.closed)
				{
					this.closed = true;
					this.waitHandle.Close();
				}
			}
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x0010A398 File Offset: 0x00108598
		public void Signal()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.closed)
				{
					this.waitHandle.Set();
				}
			}
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x0010A3E8 File Offset: 0x001085E8
		public CommunicationWaitResult Wait(TimeSpan timeout, bool aborting)
		{
			if (this.closed)
			{
				return CommunicationWaitResult.Aborted;
			}
			if (timeout < TimeSpan.Zero)
			{
				return CommunicationWaitResult.Expired;
			}
			if (aborting)
			{
				this.result = CommunicationWaitResult.Aborted;
			}
			bool flag = !TimeoutHelper.WaitOne(this.waitHandle, timeout);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.result == CommunicationWaitResult.Waiting)
				{
					this.result = (flag ? CommunicationWaitResult.Expired : CommunicationWaitResult.Succeeded);
				}
			}
			object thisLock2 = this.ThisLock;
			lock (thisLock2)
			{
				if (!this.closed)
				{
					this.waitHandle.Set();
				}
			}
			return this.result;
		}

		// Token: 0x04002DC9 RID: 11721
		private bool closed;

		// Token: 0x04002DCA RID: 11722
		private object mutex;

		// Token: 0x04002DCB RID: 11723
		private CommunicationWaitResult result;

		// Token: 0x04002DCC RID: 11724
		private ManualResetEvent waitHandle;
	}
}
