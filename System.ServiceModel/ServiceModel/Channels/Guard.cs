using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000934 RID: 2356
	internal sealed class Guard
	{
		// Token: 0x1400003B RID: 59
		// (add) Token: 0x06005A83 RID: 23171 RVA: 0x0014C700 File Offset: 0x0014A900
		// (remove) Token: 0x06005A84 RID: 23172 RVA: 0x0014C738 File Offset: 0x0014A938
		private event WaitAsyncResult.SignaledHandler Signaled;

		// Token: 0x06005A85 RID: 23173 RVA: 0x0014C76D File Offset: 0x0014A96D
		public Guard() : this(1)
		{
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x0014C776 File Offset: 0x0014A976
		public Guard(int maxCount)
		{
			this.maxCount = maxCount;
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x0014C790 File Offset: 0x0014A990
		public void Abort()
		{
			this.closed = true;
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x0014C79C File Offset: 0x0014A99C
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = false;
			WaitAsyncResult waitAsyncResult = null;
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.closed || this.currentCount == 0)
				{
					flag = true;
				}
				else
				{
					waitAsyncResult = new WaitAsyncResult(timeout, true, callback, state);
					this.Signaled += waitAsyncResult.OnSignaled;
				}
				this.closed = true;
			}
			if (flag)
			{
				return new CompletedAsyncResult(callback, state);
			}
			waitAsyncResult.Begin();
			return waitAsyncResult;
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x0014C824 File Offset: 0x0014AA24
		public void Close(TimeSpan timeout)
		{
			object obj = this.thisLock;
			lock (obj)
			{
				if (this.closed)
				{
					return;
				}
				this.closed = true;
				if (this.currentCount > 0)
				{
					this.closeEvent = new ManualResetEvent(false);
				}
			}
			if (this.closeEvent != null)
			{
				try
				{
					if (!TimeoutHelper.WaitOne(this.closeEvent, timeout))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnOperation", new object[]
						{
							timeout
						})));
					}
				}
				finally
				{
					object obj2 = this.thisLock;
					lock (obj2)
					{
						this.closeEvent.Close();
						this.closeEvent = null;
					}
				}
			}
		}

		// Token: 0x06005A8A RID: 23178 RVA: 0x0014C910 File Offset: 0x0014AB10
		public void EndClose(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			WaitAsyncResult.End(result);
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x0014C928 File Offset: 0x0014AB28
		public bool Enter()
		{
			object obj = this.thisLock;
			bool result;
			lock (obj)
			{
				if (this.closed)
				{
					result = false;
				}
				else if (this.currentCount == this.maxCount)
				{
					result = false;
				}
				else
				{
					this.currentCount++;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x0014C994 File Offset: 0x0014AB94
		public void Exit()
		{
			WaitAsyncResult.SignaledHandler signaledHandler = null;
			object obj = this.thisLock;
			lock (obj)
			{
				this.currentCount--;
				if (this.currentCount < 0)
				{
					throw Fx.AssertAndThrow("Exit can only be called after Enter.");
				}
				if (this.currentCount == 0)
				{
					if (this.closeEvent != null)
					{
						this.closeEvent.Set();
					}
					signaledHandler = this.Signaled;
				}
			}
			if (signaledHandler != null)
			{
				signaledHandler();
			}
		}

		// Token: 0x0400369A RID: 13978
		private ManualResetEvent closeEvent;

		// Token: 0x0400369B RID: 13979
		private int currentCount;

		// Token: 0x0400369C RID: 13980
		private int maxCount;

		// Token: 0x0400369D RID: 13981
		private bool closed;

		// Token: 0x0400369E RID: 13982
		private object thisLock = new object();
	}
}
