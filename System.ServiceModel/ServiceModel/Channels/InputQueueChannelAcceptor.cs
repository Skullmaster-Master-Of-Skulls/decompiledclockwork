using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000748 RID: 1864
	internal class InputQueueChannelAcceptor<TChannel> : ChannelAcceptor<TChannel> where TChannel : class, IChannel
	{
		// Token: 0x06004744 RID: 18244 RVA: 0x00109228 File Offset: 0x00107428
		public InputQueueChannelAcceptor(ChannelManagerBase channelManager) : base(channelManager)
		{
			this.channelQueue = TraceUtility.CreateInputQueue<TChannel>();
		}

		// Token: 0x17001213 RID: 4627
		// (get) Token: 0x06004745 RID: 18245 RVA: 0x0010923C File Offset: 0x0010743C
		public int PendingCount
		{
			get
			{
				return this.channelQueue.PendingCount;
			}
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x00109249 File Offset: 0x00107449
		public override TChannel AcceptChannel(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			return this.channelQueue.Dequeue(timeout);
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x0010925D File Offset: 0x0010745D
		public override IAsyncResult BeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			return this.channelQueue.BeginDequeue(timeout, callback, state);
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x00109273 File Offset: 0x00107473
		public void Dispatch()
		{
			this.channelQueue.Dispatch();
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x00109280 File Offset: 0x00107480
		public override TChannel EndAcceptChannel(IAsyncResult result)
		{
			return this.channelQueue.EndDequeue(result);
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x0010928E File Offset: 0x0010748E
		public void EnqueueAndDispatch(TChannel channel)
		{
			this.channelQueue.EnqueueAndDispatch(channel);
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x0010929C File Offset: 0x0010749C
		public void EnqueueAndDispatch(TChannel channel, Action dequeuedCallback)
		{
			this.channelQueue.EnqueueAndDispatch(channel, dequeuedCallback);
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x001092AB File Offset: 0x001074AB
		public bool EnqueueWithoutDispatch(TChannel channel, Action dequeuedCallback)
		{
			return this.channelQueue.EnqueueWithoutDispatch(channel, dequeuedCallback);
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x001092BA File Offset: 0x001074BA
		public virtual bool EnqueueWithoutDispatch(Exception exception, Action dequeuedCallback)
		{
			return this.channelQueue.EnqueueWithoutDispatch(exception, dequeuedCallback);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x001092C9 File Offset: 0x001074C9
		public void EnqueueAndDispatch(TChannel channel, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.channelQueue.EnqueueAndDispatch(channel, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x001092D9 File Offset: 0x001074D9
		public virtual void EnqueueAndDispatch(Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.channelQueue.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x001092E9 File Offset: 0x001074E9
		public void FaultQueue()
		{
			base.Fault();
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x001092F1 File Offset: 0x001074F1
		protected override void OnClosed()
		{
			base.OnClosed();
			this.channelQueue.Dispose();
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x00109304 File Offset: 0x00107504
		protected override void OnFaulted()
		{
			this.channelQueue.Shutdown(() => base.ChannelManager.GetPendingException());
			base.OnFaulted();
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x00109323 File Offset: 0x00107523
		public override bool WaitForChannel(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			return this.channelQueue.WaitForItem(timeout);
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x00109337 File Offset: 0x00107537
		public override IAsyncResult BeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			return this.channelQueue.BeginWaitForItem(timeout, callback, state);
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0010934D File Offset: 0x0010754D
		public override bool EndWaitForChannel(IAsyncResult result)
		{
			return this.channelQueue.EndWaitForItem(result);
		}

		// Token: 0x04002DA9 RID: 11689
		private InputQueue<TChannel> channelQueue;
	}
}
