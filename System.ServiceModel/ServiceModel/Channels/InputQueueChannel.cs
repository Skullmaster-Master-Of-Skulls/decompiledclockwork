using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000747 RID: 1863
	internal abstract class InputQueueChannel<TDisposable> : ChannelBase where TDisposable : class, IDisposable
	{
		// Token: 0x0600472A RID: 18218 RVA: 0x00108FF8 File Offset: 0x001071F8
		protected InputQueueChannel(ChannelManagerBase channelManager) : base(channelManager)
		{
			this.inputQueue = TraceUtility.CreateInputQueue<TDisposable>();
		}

		// Token: 0x17001211 RID: 4625
		// (get) Token: 0x0600472B RID: 18219 RVA: 0x0010900C File Offset: 0x0010720C
		public int InternalPendingItems
		{
			get
			{
				return this.inputQueue.PendingCount;
			}
		}

		// Token: 0x17001212 RID: 4626
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x00109019 File Offset: 0x00107219
		public int PendingItems
		{
			get
			{
				base.ThrowIfDisposedOrNotOpen();
				return this.InternalPendingItems;
			}
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x00109027 File Offset: 0x00107227
		public void EnqueueAndDispatch(TDisposable item)
		{
			this.EnqueueAndDispatch(item, null);
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x00109031 File Offset: 0x00107231
		public void EnqueueAndDispatch(TDisposable item, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.OnEnqueueItem(item);
			this.inputQueue.EnqueueAndDispatch(item, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x00109048 File Offset: 0x00107248
		public void EnqueueAndDispatch(Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread)
		{
			this.inputQueue.EnqueueAndDispatch(exception, dequeuedCallback, canDispatchOnThisThread);
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x00109058 File Offset: 0x00107258
		public void EnqueueAndDispatch(TDisposable item, Action dequeuedCallback)
		{
			this.OnEnqueueItem(item);
			this.inputQueue.EnqueueAndDispatch(item, dequeuedCallback);
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x0010906E File Offset: 0x0010726E
		public bool EnqueueWithoutDispatch(Exception exception, Action dequeuedCallback)
		{
			return this.inputQueue.EnqueueWithoutDispatch(exception, dequeuedCallback);
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x0010907D File Offset: 0x0010727D
		public bool EnqueueWithoutDispatch(TDisposable item, Action dequeuedCallback)
		{
			this.OnEnqueueItem(item);
			return this.inputQueue.EnqueueWithoutDispatch(item, dequeuedCallback);
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x00109093 File Offset: 0x00107293
		public void Dispatch()
		{
			this.inputQueue.Dispatch();
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x001090A0 File Offset: 0x001072A0
		public void Shutdown()
		{
			this.inputQueue.Shutdown();
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x001090AD File Offset: 0x001072AD
		protected override void OnFaulted()
		{
			base.OnFaulted();
			this.inputQueue.Shutdown(() => base.GetPendingException());
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x001090CC File Offset: 0x001072CC
		protected virtual void OnEnqueueItem(TDisposable item)
		{
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x001090CE File Offset: 0x001072CE
		protected IAsyncResult BeginDequeue(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			return this.inputQueue.BeginDequeue(timeout, callback, state);
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x001090E4 File Offset: 0x001072E4
		protected bool EndDequeue(IAsyncResult result, out TDisposable item)
		{
			bool result2 = this.inputQueue.EndDequeue(result, out item);
			if (item == null)
			{
				base.ThrowIfFaulted();
				base.ThrowIfAborted();
			}
			return result2;
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x0010911C File Offset: 0x0010731C
		protected bool Dequeue(TimeSpan timeout, out TDisposable item)
		{
			base.ThrowIfNotOpened();
			bool result = this.inputQueue.Dequeue(timeout, out item);
			if (item == null)
			{
				base.ThrowIfFaulted();
				base.ThrowIfAborted();
			}
			return result;
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x00109158 File Offset: 0x00107358
		protected bool WaitForItem(TimeSpan timeout)
		{
			base.ThrowIfNotOpened();
			bool result = this.inputQueue.WaitForItem(timeout);
			base.ThrowIfFaulted();
			base.ThrowIfAborted();
			return result;
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x00109185 File Offset: 0x00107385
		protected IAsyncResult BeginWaitForItem(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfNotOpened();
			return this.inputQueue.BeginWaitForItem(timeout, callback, state);
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x0010919C File Offset: 0x0010739C
		protected bool EndWaitForItem(IAsyncResult result)
		{
			bool result2 = this.inputQueue.EndWaitForItem(result);
			base.ThrowIfFaulted();
			base.ThrowIfAborted();
			return result2;
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x001091C3 File Offset: 0x001073C3
		protected override void OnClosing()
		{
			base.OnClosing();
			this.inputQueue.Shutdown(() => base.GetPendingException());
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x001091E2 File Offset: 0x001073E2
		protected override void OnAbort()
		{
			this.inputQueue.Close();
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x001091EF File Offset: 0x001073EF
		protected override void OnClose(TimeSpan timeout)
		{
			this.inputQueue.Close();
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x001091FC File Offset: 0x001073FC
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.inputQueue.Close();
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x00109210 File Offset: 0x00107410
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x04002DA8 RID: 11688
		private InputQueue<TDisposable> inputQueue;
	}
}
