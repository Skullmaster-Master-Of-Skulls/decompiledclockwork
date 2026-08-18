using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000566 RID: 1382
	internal class BufferedReceiveBinder : IChannelBinder
	{
		// Token: 0x060035B4 RID: 13748 RVA: 0x000D1247 File Offset: 0x000CF447
		public BufferedReceiveBinder(IChannelBinder channelBinder)
		{
			this.channelBinder = channelBinder;
			this.inputQueue = new InputQueue<BufferedReceiveBinder.RequestContextWrapper>();
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x000D1261 File Offset: 0x000CF461
		public IChannel Channel
		{
			get
			{
				return this.channelBinder.Channel;
			}
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060035B6 RID: 13750 RVA: 0x000D126E File Offset: 0x000CF46E
		public bool HasSession
		{
			get
			{
				return this.channelBinder.HasSession;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000D127B File Offset: 0x000CF47B
		public Uri ListenUri
		{
			get
			{
				return this.channelBinder.ListenUri;
			}
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x000D1288 File Offset: 0x000CF488
		public EndpointAddress LocalAddress
		{
			get
			{
				return this.channelBinder.LocalAddress;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060035B9 RID: 13753 RVA: 0x000D1295 File Offset: 0x000CF495
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.channelBinder.RemoteAddress;
			}
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000D12A2 File Offset: 0x000CF4A2
		public void Abort()
		{
			this.inputQueue.Close();
			this.channelBinder.Abort();
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x000D12BA File Offset: 0x000CF4BA
		public void CloseAfterFault(TimeSpan timeout)
		{
			this.inputQueue.Close();
			this.channelBinder.CloseAfterFault(timeout);
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000D12D4 File Offset: 0x000CF4D4
		public bool TryReceive(TimeSpan timeout, out RequestContext requestContext)
		{
			if (Interlocked.CompareExchange(ref this.pendingOperationSemaphore, 1, 0) == 0)
			{
				ActionItem.Schedule(BufferedReceiveBinder.tryReceive, this);
			}
			BufferedReceiveBinder.RequestContextWrapper requestContextWrapper;
			bool flag = this.inputQueue.Dequeue(timeout, out requestContextWrapper);
			if (flag && requestContextWrapper != null)
			{
				requestContext = requestContextWrapper.RequestContext;
			}
			else
			{
				requestContext = null;
			}
			return flag;
		}

		// Token: 0x060035BD RID: 13757 RVA: 0x000D1320 File Offset: 0x000CF520
		public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (Interlocked.CompareExchange(ref this.pendingOperationSemaphore, 1, 0) == 0)
			{
				IAsyncResult asyncResult = this.channelBinder.BeginTryReceive(timeout, BufferedReceiveBinder.tryReceiveCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					BufferedReceiveBinder.HandleEndTryReceive(asyncResult);
				}
			}
			return this.inputQueue.BeginDequeue(timeout, callback, state);
		}

		// Token: 0x060035BE RID: 13758 RVA: 0x000D136C File Offset: 0x000CF56C
		public bool EndTryReceive(IAsyncResult result, out RequestContext requestContext)
		{
			BufferedReceiveBinder.RequestContextWrapper requestContextWrapper;
			bool flag = this.inputQueue.EndDequeue(result, out requestContextWrapper);
			if (flag && requestContextWrapper != null)
			{
				requestContext = requestContextWrapper.RequestContext;
			}
			else
			{
				requestContext = null;
			}
			return flag;
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x000D139C File Offset: 0x000CF59C
		public RequestContext CreateRequestContext(Message message)
		{
			return this.channelBinder.CreateRequestContext(message);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x000D13AA File Offset: 0x000CF5AA
		public void Send(Message message, TimeSpan timeout)
		{
			this.channelBinder.Send(message, timeout);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000D13B9 File Offset: 0x000CF5B9
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelBinder.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000D13CB File Offset: 0x000CF5CB
		public void EndSend(IAsyncResult result)
		{
			this.channelBinder.EndSend(result);
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000D13D9 File Offset: 0x000CF5D9
		public Message Request(Message message, TimeSpan timeout)
		{
			return this.channelBinder.Request(message, timeout);
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x000D13E8 File Offset: 0x000CF5E8
		public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelBinder.BeginRequest(message, timeout, callback, state);
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000D13FA File Offset: 0x000CF5FA
		public Message EndRequest(IAsyncResult result)
		{
			return this.channelBinder.EndRequest(result);
		}

		// Token: 0x060035C6 RID: 13766 RVA: 0x000D1408 File Offset: 0x000CF608
		public bool WaitForMessage(TimeSpan timeout)
		{
			return this.channelBinder.WaitForMessage(timeout);
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000D1416 File Offset: 0x000CF616
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.channelBinder.BeginWaitForMessage(timeout, callback, state);
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000D1426 File Offset: 0x000CF626
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return this.channelBinder.EndWaitForMessage(result);
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000D1434 File Offset: 0x000CF634
		internal void InjectRequest(RequestContext requestContext)
		{
			this.inputQueue.EnqueueAndDispatch(new BufferedReceiveBinder.RequestContextWrapper(requestContext));
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000D1448 File Offset: 0x000CF648
		private static void TryReceive(object state)
		{
			BufferedReceiveBinder bufferedReceiveBinder = (BufferedReceiveBinder)state;
			bool flag = false;
			try
			{
				RequestContext requestContext;
				if (bufferedReceiveBinder.channelBinder.TryReceive(TimeSpan.MaxValue, out requestContext))
				{
					flag = bufferedReceiveBinder.inputQueue.EnqueueWithoutDispatch(new BufferedReceiveBinder.RequestContextWrapper(requestContext), null);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				flag = bufferedReceiveBinder.inputQueue.EnqueueWithoutDispatch(exception, null);
			}
			finally
			{
				Interlocked.Exchange(ref bufferedReceiveBinder.pendingOperationSemaphore, 0);
				if (flag)
				{
					bufferedReceiveBinder.inputQueue.Dispatch();
				}
			}
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000D14DC File Offset: 0x000CF6DC
		private static void TryReceiveCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			BufferedReceiveBinder.HandleEndTryReceive(result);
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000D14F0 File Offset: 0x000CF6F0
		private static void HandleEndTryReceive(IAsyncResult result)
		{
			BufferedReceiveBinder bufferedReceiveBinder = (BufferedReceiveBinder)result.AsyncState;
			bool flag = false;
			try
			{
				RequestContext requestContext;
				if (bufferedReceiveBinder.channelBinder.EndTryReceive(result, out requestContext))
				{
					flag = bufferedReceiveBinder.inputQueue.EnqueueWithoutDispatch(new BufferedReceiveBinder.RequestContextWrapper(requestContext), null);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				flag = bufferedReceiveBinder.inputQueue.EnqueueWithoutDispatch(exception, null);
			}
			finally
			{
				Interlocked.Exchange(ref bufferedReceiveBinder.pendingOperationSemaphore, 0);
				if (flag)
				{
					bufferedReceiveBinder.inputQueue.Dispatch();
				}
			}
		}

		// Token: 0x0400289C RID: 10396
		private static Action<object> tryReceive = new Action<object>(BufferedReceiveBinder.TryReceive);

		// Token: 0x0400289D RID: 10397
		private static AsyncCallback tryReceiveCallback = Fx.ThunkCallback(new AsyncCallback(BufferedReceiveBinder.TryReceiveCallback));

		// Token: 0x0400289E RID: 10398
		private IChannelBinder channelBinder;

		// Token: 0x0400289F RID: 10399
		private InputQueue<BufferedReceiveBinder.RequestContextWrapper> inputQueue;

		// Token: 0x040028A0 RID: 10400
		private int pendingOperationSemaphore;

		// Token: 0x02000C86 RID: 3206
		private class RequestContextWrapper
		{
			// Token: 0x0600788D RID: 30861 RVA: 0x001C2488 File Offset: 0x001C0688
			public RequestContextWrapper(RequestContext requestContext)
			{
				this.RequestContext = requestContext;
			}

			// Token: 0x17001B64 RID: 7012
			// (get) Token: 0x0600788E RID: 30862 RVA: 0x001C2497 File Offset: 0x001C0697
			// (set) Token: 0x0600788F RID: 30863 RVA: 0x001C249F File Offset: 0x001C069F
			public RequestContext RequestContext { get; private set; }
		}
	}
}
