using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200076A RID: 1898
	[__DynamicallyInvokable]
	public abstract class RequestContext : IDisposable
	{
		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x0600486F RID: 18543
		[__DynamicallyInvokable]
		public abstract Message RequestMessage { [__DynamicallyInvokable] get; }

		// Token: 0x06004870 RID: 18544
		[__DynamicallyInvokable]
		public abstract void Abort();

		// Token: 0x06004871 RID: 18545
		[__DynamicallyInvokable]
		public abstract void Close();

		// Token: 0x06004872 RID: 18546
		[__DynamicallyInvokable]
		public abstract void Close(TimeSpan timeout);

		// Token: 0x06004873 RID: 18547
		[__DynamicallyInvokable]
		public abstract void Reply(Message message);

		// Token: 0x06004874 RID: 18548
		[__DynamicallyInvokable]
		public abstract void Reply(Message message, TimeSpan timeout);

		// Token: 0x06004875 RID: 18549
		[__DynamicallyInvokable]
		public abstract IAsyncResult BeginReply(Message message, AsyncCallback callback, object state);

		// Token: 0x06004876 RID: 18550
		[__DynamicallyInvokable]
		public abstract IAsyncResult BeginReply(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004877 RID: 18551
		[__DynamicallyInvokable]
		public abstract void EndReply(IAsyncResult result);

		// Token: 0x06004878 RID: 18552 RVA: 0x0010BDD4 File Offset: 0x00109FD4
		[__DynamicallyInvokable]
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004879 RID: 18553 RVA: 0x0010BDDD File Offset: 0x00109FDD
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600487A RID: 18554 RVA: 0x0010BDDF File Offset: 0x00109FDF
		[__DynamicallyInvokable]
		protected RequestContext()
		{
		}
	}
}
