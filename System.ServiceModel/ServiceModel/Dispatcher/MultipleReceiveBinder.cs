using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000567 RID: 1383
	internal class MultipleReceiveBinder : IChannelBinder
	{
		// Token: 0x060035CE RID: 13774 RVA: 0x000D15AD File Offset: 0x000CF7AD
		public MultipleReceiveBinder(IChannelBinder channelBinder, int size, bool ordered)
		{
			this.ordered = ordered;
			this.channelBinder = channelBinder;
			this.pendingResults = new MultipleReceiveBinder.ReceiveScopeQueue(size);
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060035CF RID: 13775 RVA: 0x000D15CF File Offset: 0x000CF7CF
		public IChannel Channel
		{
			get
			{
				return this.channelBinder.Channel;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000D15DC File Offset: 0x000CF7DC
		public bool HasSession
		{
			get
			{
				return this.channelBinder.HasSession;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060035D1 RID: 13777 RVA: 0x000D15E9 File Offset: 0x000CF7E9
		public Uri ListenUri
		{
			get
			{
				return this.channelBinder.ListenUri;
			}
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060035D2 RID: 13778 RVA: 0x000D15F6 File Offset: 0x000CF7F6
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.channelBinder.LocalAddress;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060035D3 RID: 13779 RVA: 0x000D1603 File Offset: 0x000CF803
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.channelBinder.RemoteAddress;
			}
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000D1610 File Offset: 0x000CF810
		public void Abort()
		{
			this.channelBinder.Abort();
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000D161D File Offset: 0x000CF81D
		public void CloseAfterFault(TimeSpan timeout)
		{
			this.channelBinder.CloseAfterFault(timeout);
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000D162B File Offset: 0x000CF82B
		public bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			return this.channelBinder.TryReceive(timeout, out requestContext);
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000D163C File Offset: 0x000CF83C
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			Fx.AssertAndThrow(this.outstanding == null, "BeginTryReceive should not have a pending result.");
			MultipleReceiveBinder.MultipleReceiveAsyncResult result = new MultipleReceiveBinder.MultipleReceiveAsyncResult(callback, state);
			this.outstanding = result;
			this.EnsurePump(timeout);
			IAsyncResult innerResult;
			if (this.pendingResults.TryDequeueHead(out innerResult))
			{
				this.HandleReceiveRequestComplete(innerResult, true);
			}
			return result;
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000D168C File Offset: 0x000CF88C
		private void EnsurePump(TimeSpan timeout)
		{
			while (!this.pendingResults.IsFull)
			{
				MultipleReceiveBinder.ReceiveScopeSignalGate receiveScopeSignalGate = new MultipleReceiveBinder.ReceiveScopeSignalGate(this);
				this.pendingResults.Enqueue(receiveScopeSignalGate);
				IAsyncResult asyncResult = this.channelBinder.BeginTryReceive(timeout, MultipleReceiveBinder.onInnerReceiveCompleted, receiveScopeSignalGate);
				if (asyncResult.CompletedSynchronously)
				{
					this.SignalReceiveCompleted(asyncResult);
				}
			}
		}

		// Token: 0x060035D9 RID: 13785 RVA: 0x000D16E0 File Offset: 0x000CF8E0
		private static void OnInnerReceiveCompleted(IAsyncResult nestedResult)
		{
			if (nestedResult.CompletedSynchronously)
			{
				return;
			}
			MultipleReceiveBinder.ReceiveScopeSignalGate receiveScopeSignalGate = nestedResult.AsyncState as MultipleReceiveBinder.ReceiveScopeSignalGate;
			receiveScopeSignalGate.Binder.HandleReceiveAndSignalCompletion(nestedResult, false);
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000D170F File Offset: 0x000CF90F
		private void HandleReceiveAndSignalCompletion(IAsyncResult nestedResult, bool completedSynchronosly)
		{
			if (this.SignalReceiveCompleted(nestedResult))
			{
				this.HandleReceiveRequestComplete(nestedResult, completedSynchronosly);
			}
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000D1722 File Offset: 0x000CF922
		private bool SignalReceiveCompleted(IAsyncResult nestedResult)
		{
			if (this.ordered)
			{
				return this.pendingResults.TrySignal((MultipleReceiveBinder.ReceiveScopeSignalGate)nestedResult.AsyncState, nestedResult);
			}
			return this.pendingResults.TrySignalPending(nestedResult);
		}

		// Token: 0x060035DC RID: 13788 RVA: 0x000D1750 File Offset: 0x000CF950
		private void HandleReceiveRequestComplete(IAsyncResult innerResult, bool completedSynchronously)
		{
			MultipleReceiveBinder.MultipleReceiveAsyncResult multipleReceiveAsyncResult = this.outstanding;
			Exception completionException = null;
			try
			{
				Fx.AssertAndThrow(multipleReceiveAsyncResult != null, "HandleReceive invoked without an outstanding result");
				this.outstanding = null;
				RequestContext requestContext;
				multipleReceiveAsyncResult.Valid = this.channelBinder.EndTryReceive(innerResult, out requestContext);
				multipleReceiveAsyncResult.RequestContext = requestContext;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				completionException = ex;
			}
			multipleReceiveAsyncResult.Complete(completedSynchronously, completionException);
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000D17C0 File Offset: 0x000CF9C0
		public bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			return MultipleReceiveBinder.MultipleReceiveAsyncResult.End(result, out requestContext);
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000D17C9 File Offset: 0x000CF9C9
		public RequestContext CreateRequestContext(Message message)
		{
			return this.channelBinder.CreateRequestContext(message);
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000D17D7 File Offset: 0x000CF9D7
		public void Send(Message message, TimeSpan timeout)
		{
			this.channelBinder.Send(message, timeout);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000D17E6 File Offset: 0x000CF9E6
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelBinder.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000D17F8 File Offset: 0x000CF9F8
		public void EndSend(IAsyncResult result)
		{
			this.channelBinder.EndSend(result);
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x000D1806 File Offset: 0x000CFA06
		public Message Request(Message message, TimeSpan timeout)
		{
			return this.channelBinder.Request(message, timeout);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000D1815 File Offset: 0x000CFA15
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelBinder.BeginRequest(message, timeout, callback, state);
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000D1827 File Offset: 0x000CFA27
		public Message EndRequest(IAsyncResult result)
		{
			return this.channelBinder.EndRequest(result);
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x000D1835 File Offset: 0x000CFA35
		public bool WaitForMessage(TimeSpan timeout)
		{
			return this.channelBinder.WaitForMessage(timeout);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x000D1843 File Offset: 0x000CFA43
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelBinder.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x000D1853 File Offset: 0x000CFA53
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return this.channelBinder.EndWaitForMessage(result);
		}

		// Token: 0x040028A1 RID: 10401
		private static AsyncCallback onInnerReceiveCompleted = Fx.ThunkCallback(new AsyncCallback(MultipleReceiveBinder.OnInnerReceiveCompleted));

		// Token: 0x040028A2 RID: 10402
		private MultipleReceiveBinder.MultipleReceiveAsyncResult outstanding;

		// Token: 0x040028A3 RID: 10403
		private IChannelBinder channelBinder;

		// Token: 0x040028A4 RID: 10404
		private MultipleReceiveBinder.ReceiveScopeQueue pendingResults;

		// Token: 0x040028A5 RID: 10405
		private bool ordered;

		// Token: 0x02000C87 RID: 3207
		internal static class MultipleReceiveDefaults
		{
			// Token: 0x040044BA RID: 17594
			internal const int MaxPendingReceives = 1;
		}

		// Token: 0x02000C88 RID: 3208
		private class MultipleReceiveAsyncResult : AsyncResult
		{
			// Token: 0x06007890 RID: 30864 RVA: 0x001C24A8 File Offset: 0x001C06A8
			public MultipleReceiveAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}

			// Token: 0x17001B65 RID: 7013
			// (get) Token: 0x06007891 RID: 30865 RVA: 0x001C24B2 File Offset: 0x001C06B2
			// (set) Token: 0x06007892 RID: 30866 RVA: 0x001C24BA File Offset: 0x001C06BA
			public bool Valid { get; set; }

			// Token: 0x17001B66 RID: 7014
			// (get) Token: 0x06007893 RID: 30867 RVA: 0x001C24C3 File Offset: 0x001C06C3
			// (set) Token: 0x06007894 RID: 30868 RVA: 0x001C24CB File Offset: 0x001C06CB
			public RequestContext RequestContext { get; set; }

			// Token: 0x06007895 RID: 30869 RVA: 0x001C24D4 File Offset: 0x001C06D4
			public new void Complete(bool completedSynchronously, Exception completionException)
			{
				base.Complete(completedSynchronously, completionException);
			}

			// Token: 0x06007896 RID: 30870 RVA: 0x001C24E0 File Offset: 0x001C06E0
			public static bool End(IAsyncResult result, out RequestContext context)
			{
				MultipleReceiveBinder.MultipleReceiveAsyncResult multipleReceiveAsyncResult = AsyncResult.End<MultipleReceiveBinder.MultipleReceiveAsyncResult>(result);
				context = multipleReceiveAsyncResult.RequestContext;
				return multipleReceiveAsyncResult.Valid;
			}
		}

		// Token: 0x02000C89 RID: 3209
		private class ReceiveScopeSignalGate : SignalGate<IAsyncResult>
		{
			// Token: 0x06007897 RID: 30871 RVA: 0x001C2502 File Offset: 0x001C0702
			public ReceiveScopeSignalGate(MultipleReceiveBinder binder)
			{
				this.Binder = binder;
			}

			// Token: 0x17001B67 RID: 7015
			// (get) Token: 0x06007898 RID: 30872 RVA: 0x001C2511 File Offset: 0x001C0711
			// (set) Token: 0x06007899 RID: 30873 RVA: 0x001C2519 File Offset: 0x001C0719
			public MultipleReceiveBinder Binder { get; private set; }
		}

		// Token: 0x02000C8A RID: 3210
		private class ReceiveScopeQueue
		{
			// Token: 0x0600789A RID: 30874 RVA: 0x001C2522 File Offset: 0x001C0722
			public ReceiveScopeQueue(int size)
			{
				this.size = size;
				this.head = 0;
				this.count = 0;
				this.pending = 0;
				this.items = new MultipleReceiveBinder.ReceiveScopeSignalGate[size];
			}

			// Token: 0x17001B68 RID: 7016
			// (get) Token: 0x0600789B RID: 30875 RVA: 0x001C2552 File Offset: 0x001C0752
			internal bool IsFull
			{
				get
				{
					return this.count == this.size;
				}
			}

			// Token: 0x0600789C RID: 30876 RVA: 0x001C2564 File Offset: 0x001C0764
			internal void Enqueue(MultipleReceiveBinder.ReceiveScopeSignalGate receiveScope)
			{
				Fx.AssertAndThrow(this.count < this.size, "Cannot Enqueue into a full queue.");
				this.items[(this.head + this.count) % this.size] = receiveScope;
				this.count++;
			}

			// Token: 0x0600789D RID: 30877 RVA: 0x001C25B4 File Offset: 0x001C07B4
			private void Dequeue()
			{
				Fx.AssertAndThrow(this.count > 0, "Cannot Dequeue and empty queue.");
				this.items[this.head] = null;
				this.head = (this.head + 1) % this.size;
				this.count--;
			}

			// Token: 0x0600789E RID: 30878 RVA: 0x001C2605 File Offset: 0x001C0805
			internal bool TryDequeueHead(out IAsyncResult result)
			{
				Fx.AssertAndThrow(this.count > 0, "Cannot unlock item when queue is empty");
				if (this.items[this.head].Unlock(out result))
				{
					this.Dequeue();
					return true;
				}
				return false;
			}

			// Token: 0x0600789F RID: 30879 RVA: 0x001C2638 File Offset: 0x001C0838
			public bool TrySignal(MultipleReceiveBinder.ReceiveScopeSignalGate scope, IAsyncResult nestedResult)
			{
				if (scope.Signal(nestedResult))
				{
					this.Dequeue();
					return true;
				}
				return false;
			}

			// Token: 0x060078A0 RID: 30880 RVA: 0x001C264C File Offset: 0x001C084C
			public bool TrySignalPending(IAsyncResult result)
			{
				int nextPending = this.GetNextPending();
				if (this.items[nextPending].Signal(result))
				{
					this.Dequeue();
					return true;
				}
				return false;
			}

			// Token: 0x060078A1 RID: 30881 RVA: 0x001C267C File Offset: 0x001C087C
			private int GetNextPending()
			{
				int num = this.pending;
				while (num != (num = Interlocked.CompareExchange(ref this.pending, (num + 1) % this.size, num)))
				{
				}
				return num;
			}

			// Token: 0x040044BE RID: 17598
			private int pending;

			// Token: 0x040044BF RID: 17599
			private int head;

			// Token: 0x040044C0 RID: 17600
			private int count;

			// Token: 0x040044C1 RID: 17601
			private readonly int size;

			// Token: 0x040044C2 RID: 17602
			private MultipleReceiveBinder.ReceiveScopeSignalGate[] items;
		}
	}
}
