using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.Transactions;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000588 RID: 1416
	internal class ListenerHandler : CommunicationObject, ISessionThrottleNotification
	{
		// Token: 0x06003677 RID: 13943 RVA: 0x000D1C94 File Offset: 0x000CFE94
		internal ListenerHandler(IListenerBinder listenerBinder, ChannelDispatcher channelDispatcher, ServiceHostBase host, ServiceThrottle throttle, IDefaultCommunicationTimeouts timeouts)
		{
			this.listenerBinder = listenerBinder;
			if (this.listenerBinder == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("listenerBinder");
			}
			this.channelDispatcher = channelDispatcher;
			if (this.channelDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("channelDispatcher");
			}
			this.host = host;
			if (this.host == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("host");
			}
			this.throttle = throttle;
			if (this.throttle == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("throttle");
			}
			this.timeouts = timeouts;
			this.endpoints = channelDispatcher.EndpointDispatcherTable;
			this.acceptor = new ErrorHandlingAcceptor(listenerBinder, channelDispatcher);
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000D1D45 File Offset: 0x000CFF45
		internal ChannelDispatcher ChannelDispatcher
		{
			get
			{
				return this.channelDispatcher;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x000D1D4D File Offset: 0x000CFF4D
		internal ListenerChannel Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x0600367A RID: 13946 RVA: 0x000D1D55 File Offset: 0x000CFF55
		protected override TimeSpan DefaultCloseTimeout
		{
			get
			{
				return this.host.CloseTimeout;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x000D1D62 File Offset: 0x000CFF62
		protected override TimeSpan DefaultOpenTimeout
		{
			get
			{
				return this.host.OpenTimeout;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600367C RID: 13948 RVA: 0x000D1D6F File Offset: 0x000CFF6F
		// (set) Token: 0x0600367D RID: 13949 RVA: 0x000D1D77 File Offset: 0x000CFF77
		internal EndpointDispatcherTable Endpoints
		{
			get
			{
				return this.endpoints;
			}
			set
			{
				this.endpoints = value;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x0600367E RID: 13950 RVA: 0x000D1D80 File Offset: 0x000CFF80
		internal ServiceHostBase Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x000D1D88 File Offset: 0x000CFF88
		internal new object ThisLock
		{
			get
			{
				return base.ThisLock;
			}
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000D1D90 File Offset: 0x000CFF90
		protected override void OnOpen(TimeSpan timeout)
		{
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x000D1D92 File Offset: 0x000CFF92
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000D1D9B File Offset: 0x000CFF9B
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000D1DA4 File Offset: 0x000CFFA4
		protected override void OnOpened()
		{
			base.OnOpened();
			this.channelDispatcher.Channels.IncrementActivityCount();
			if (this.channelDispatcher.IsTransactedReceive && this.channelDispatcher.ReceiveContextEnabled && this.channelDispatcher.MaxTransactedBatchSize > 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("IncompatibleBehaviors")));
			}
			this.NewChannelPump();
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x000D1E0F File Offset: 0x000D000F
		internal void NewChannelPump()
		{
			ActionItem.Schedule(ListenerHandler.initiateChannelPump, this);
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000D1E1C File Offset: 0x000D001C
		private static void InitiateChannelPump(object state)
		{
			ListenerHandler listenerHandler = state as ListenerHandler;
			if (!listenerHandler.ChannelDispatcher.IsTransactedAccept)
			{
				listenerHandler.ChannelPump();
				return;
			}
			if (listenerHandler.ChannelDispatcher.AsynchronousTransactedAcceptEnabled)
			{
				listenerHandler.AsyncTransactedChannelPump();
				return;
			}
			listenerHandler.SyncTransactedChannelPump();
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000D1E60 File Offset: 0x000D0060
		private void ChannelPump()
		{
			IChannelListener listener = this.listenerBinder.Listener;
			while (!this.acceptedNull && listener.State != CommunicationState.Faulted)
			{
				if (!this.AcceptAndAcquireThrottle())
				{
					return;
				}
				this.Dispatch();
			}
			this.DoneAccepting();
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x000D1EA4 File Offset: 0x000D00A4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void SyncTransactedChannelPump()
		{
			IChannelListener listener = this.listenerBinder.Listener;
			while (!this.acceptedNull && listener.State != CommunicationState.Faulted)
			{
				this.acceptor.WaitForChannel();
				Transaction transaction;
				if (this.TransactedAccept(out transaction) && null != transaction)
				{
					this.wrappedTransaction = new WrappedTransaction(transaction);
					if (!this.AcquireThrottle())
					{
						return;
					}
					this.Dispatch();
				}
			}
			this.DoneAccepting();
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000D1F10 File Offset: 0x000D0110
		private void AsyncTransactedChannelPump()
		{
			IChannelListener listener = this.listenerBinder.Listener;
			while (!this.acceptedNull && listener.State != CommunicationState.Faulted)
			{
				IAsyncResult asyncResult = this.acceptor.BeginWaitForChannel(ListenerHandler.waitCallback, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.acceptor.EndWaitForChannel(asyncResult);
					if (this.AcceptChannel(listener))
					{
						continue;
					}
				}
				return;
			}
			this.DoneAccepting();
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000D1F70 File Offset: 0x000D0170
		private static void WaitCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ListenerHandler listenerHandler = (ListenerHandler)result.AsyncState;
			IChannelListener listener = listenerHandler.listenerBinder.Listener;
			listenerHandler.acceptor.EndWaitForChannel(result);
			if (listenerHandler.AcceptChannel(listener))
			{
				listenerHandler.AsyncTransactedChannelPump();
			}
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x000D1FBC File Offset: 0x000D01BC
		private bool AcceptChannel(IChannelListener listener)
		{
			Transaction transaction;
			if (this.TransactedAccept(out transaction) && null != transaction)
			{
				this.wrappedTransaction = new WrappedTransaction(transaction);
				if (!this.AcquireThrottle())
				{
					return false;
				}
				this.Dispatch();
			}
			return true;
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000D1FFC File Offset: 0x000D01FC
		private void AbortChannels()
		{
			IChannel[] array = this.channelDispatcher.Channels.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Abort();
			}
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000D2030 File Offset: 0x000D0230
		private bool AcceptAndAcquireThrottle()
		{
			IAsyncResult asyncResult = this.acceptor.BeginTryAccept(TimeSpan.MaxValue, ListenerHandler.acceptCallback, this);
			return asyncResult.CompletedSynchronously && this.HandleEndAccept(asyncResult);
		}

		// Token: 0x0600368D RID: 13965 RVA: 0x000D2068 File Offset: 0x000D0268
		private bool TransactedAccept(out Transaction tx)
		{
			tx = null;
			bool result;
			try
			{
				tx = TransactionBehavior.CreateTransaction(this.ChannelDispatcher.TransactionIsolationLevel, this.ChannelDispatcher.TransactionTimeout);
				IChannelBinder channelBinder = null;
				using (TransactionScope transactionScope = new TransactionScope(tx))
				{
					TimeSpan timeout = TimeoutHelper.Min(this.ChannelDispatcher.TransactionTimeout, this.ChannelDispatcher.DefaultCommunicationTimeouts.ReceiveTimeout);
					if (!this.acceptor.TryAccept(TransactionBehavior.NormalizeTimeout(timeout), out channelBinder))
					{
						return false;
					}
					transactionScope.Complete();
				}
				if (channelBinder != null)
				{
					this.channel = new ListenerChannel(channelBinder);
					this.idleManager = ServiceChannel.SessionIdleManager.CreateIfNeeded(this.channel.Binder, this.channelDispatcher.DefaultCommunicationTimeouts.ReceiveTimeout);
					result = true;
				}
				else
				{
					this.AcceptedNull();
					tx = null;
					result = false;
				}
			}
			catch (CommunicationException exception)
			{
				if (null != tx)
				{
					try
					{
						tx.Rollback();
					}
					catch (TransactionException exception2)
					{
						DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					}
				}
				tx = null;
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				result = false;
			}
			catch (TransactionException exception3)
			{
				tx = null;
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
				result = false;
			}
			return result;
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000D21AC File Offset: 0x000D03AC
		private ListenerChannel CompleteAccept(IAsyncResult result)
		{
			IChannelBinder channelBinder;
			bool flag = this.acceptor.EndTryAccept(result, out channelBinder);
			if (!flag)
			{
				return null;
			}
			if (channelBinder != null)
			{
				return new ListenerChannel(channelBinder);
			}
			this.AcceptedNull();
			return null;
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000D21E0 File Offset: 0x000D03E0
		private bool HandleEndAccept(IAsyncResult result)
		{
			this.channel = this.CompleteAccept(result);
			if (this.channel != null)
			{
				this.idleManager = ServiceChannel.SessionIdleManager.CreateIfNeeded(this.channel.Binder, this.channelDispatcher.DefaultCommunicationTimeouts.ReceiveTimeout);
				return this.AcquireThrottle();
			}
			this.DoneAccepting();
			return true;
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000D2238 File Offset: 0x000D0438
		private static void AcceptCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ListenerHandler listenerHandler = (ListenerHandler)result.AsyncState;
			if (listenerHandler.HandleEndAccept(result))
			{
				listenerHandler.Dispatch();
				listenerHandler.ChannelPump();
			}
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000D226F File Offset: 0x000D046F
		private bool AcquireThrottle()
		{
			return this.channel == null || this.throttle == null || !this.channelDispatcher.Session || this.throttle.AcquireSession(this);
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000D229C File Offset: 0x000D049C
		public void ThrottleAcquired()
		{
			this.Dispatch();
			this.NewChannelPump();
		}

		// Token: 0x06003693 RID: 13971 RVA: 0x000D22AC File Offset: 0x000D04AC
		private void CloseChannel(IChannel channel, TimeSpan timeout)
		{
			try
			{
				if (channel.State != CommunicationState.Closing && channel.State != CommunicationState.Closed)
				{
					ListenerHandler.CloseChannelState state = new ListenerHandler.CloseChannelState(this, channel);
					if (channel is ISessionChannel<IDuplexSession>)
					{
						IDuplexSession session = ((ISessionChannel<IDuplexSession>)channel).Session;
						IAsyncResult asyncResult = session.BeginCloseOutputSession(timeout, Fx.ThunkCallback(new AsyncCallback(ListenerHandler.CloseOutputSessionCallback)), state);
						if (asyncResult.CompletedSynchronously)
						{
							session.EndCloseOutputSession(asyncResult);
						}
					}
					else
					{
						IAsyncResult asyncResult2 = channel.BeginClose(timeout, Fx.ThunkCallback(new AsyncCallback(ListenerHandler.CloseChannelCallback)), state);
						if (asyncResult2.CompletedSynchronously)
						{
							channel.EndClose(asyncResult2);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleError(ex);
				if (channel is ISessionChannel<IDuplexSession>)
				{
					channel.Abort();
				}
			}
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000D2374 File Offset: 0x000D0574
		private static void CloseChannelCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ListenerHandler.CloseChannelState closeChannelState = (ListenerHandler.CloseChannelState)result.AsyncState;
			try
			{
				closeChannelState.Channel.EndClose(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				closeChannelState.ListenerHandler.HandleError(ex);
			}
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000D23D0 File Offset: 0x000D05D0
		public void CloseInput(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			foreach (IChannel channel in this.channelDispatcher.Channels.ToArray())
			{
				if (!this.IsSessionChannel(channel))
				{
					try
					{
						channel.Close(timeoutHelper.RemainingTime());
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						this.HandleError(ex);
					}
				}
			}
		}

		// Token: 0x06003696 RID: 13974 RVA: 0x000D2448 File Offset: 0x000D0648
		private static void CloseOutputSessionCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ListenerHandler.CloseChannelState closeChannelState = (ListenerHandler.CloseChannelState)result.AsyncState;
			try
			{
				((ISessionChannel<IDuplexSession>)closeChannelState.Channel).Session.EndCloseOutputSession(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				closeChannelState.ListenerHandler.HandleError(ex);
				closeChannelState.Channel.Abort();
			}
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000D24B8 File Offset: 0x000D06B8
		private void CloseChannels(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			IChannel[] array = this.channelDispatcher.Channels.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				this.CloseChannel(array[i], timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000D24FC File Offset: 0x000D06FC
		private void Dispatch()
		{
			ListenerChannel listenerChannel = this.channel;
			ServiceChannel.SessionIdleManager sessionIdleManager = this.idleManager;
			this.channel = null;
			this.idleManager = null;
			try
			{
				if (listenerChannel != null)
				{
					ChannelHandler channelHandler = new ChannelHandler(this.listenerBinder.MessageVersion, listenerChannel.Binder, this.throttle, this, listenerChannel.Throttle != null, this.wrappedTransaction, sessionIdleManager);
					if (!listenerChannel.Binder.HasSession)
					{
						this.channelDispatcher.Channels.Add(listenerChannel.Binder.Channel);
					}
					if (listenerChannel.Binder is DuplexChannelBinder)
					{
						DuplexChannelBinder duplexChannelBinder = listenerChannel.Binder as DuplexChannelBinder;
						duplexChannelBinder.ChannelHandler = channelHandler;
						duplexChannelBinder.DefaultCloseTimeout = this.DefaultCloseTimeout;
						if (this.timeouts == null)
						{
							duplexChannelBinder.DefaultSendTimeout = ServiceDefaults.SendTimeout;
						}
						else
						{
							duplexChannelBinder.DefaultSendTimeout = this.timeouts.SendTimeout;
						}
					}
					ChannelHandler.Register(channelHandler);
					listenerChannel = null;
					sessionIdleManager = null;
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.HandleError(ex);
			}
			finally
			{
				if (listenerChannel != null)
				{
					listenerChannel.Binder.Channel.Abort();
					if (this.throttle != null && this.channelDispatcher.Session)
					{
						this.throttle.DeactivateChannel();
					}
					if (sessionIdleManager != null)
					{
						sessionIdleManager.CancelTimer();
					}
				}
			}
		}

		// Token: 0x06003699 RID: 13977 RVA: 0x000D2650 File Offset: 0x000D0850
		private void AcceptedNull()
		{
			this.acceptedNull = true;
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000D265C File Offset: 0x000D085C
		private void DoneAccepting()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (!this.doneAccepting)
				{
					this.doneAccepting = true;
					this.channelDispatcher.Channels.DecrementActivityCount();
				}
			}
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000D26B8 File Offset: 0x000D08B8
		private bool IsSessionChannel(IChannel channel)
		{
			return channel is ISessionChannel<IDuplexSession> || channel is ISessionChannel<IInputSession> || channel is ISessionChannel<IOutputSession>;
		}

		// Token: 0x0600369C RID: 13980 RVA: 0x000D26D8 File Offset: 0x000D08D8
		private void CancelPendingIdleManager()
		{
			ServiceChannel.SessionIdleManager sessionIdleManager = this.idleManager;
			if (sessionIdleManager != null)
			{
				sessionIdleManager.CancelTimer();
			}
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000D26F5 File Offset: 0x000D08F5
		protected override void OnAbort()
		{
			this.CancelPendingIdleManager();
			this.channelDispatcher.Channels.CloseInput();
			this.AbortChannels();
			this.channelDispatcher.Channels.Abort();
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000D2724 File Offset: 0x000D0924
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CancelPendingIdleManager();
			this.channelDispatcher.Channels.CloseInput();
			this.CloseChannels(timeoutHelper.RemainingTime());
			return this.channelDispatcher.Channels.BeginClose(timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000D2778 File Offset: 0x000D0978
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CancelPendingIdleManager();
			this.channelDispatcher.Channels.CloseInput();
			this.CloseChannels(timeoutHelper.RemainingTime());
			this.channelDispatcher.Channels.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000D27C7 File Offset: 0x000D09C7
		protected override void OnEndClose(IAsyncResult result)
		{
			this.channelDispatcher.Channels.EndClose(result);
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000D27DA File Offset: 0x000D09DA
		private bool HandleError(Exception e)
		{
			return this.channelDispatcher.HandleError(e);
		}

		// Token: 0x040028AE RID: 10414
		private static AsyncCallback acceptCallback = Fx.ThunkCallback(new AsyncCallback(ListenerHandler.AcceptCallback));

		// Token: 0x040028AF RID: 10415
		private static Action<object> initiateChannelPump = new Action<object>(ListenerHandler.InitiateChannelPump);

		// Token: 0x040028B0 RID: 10416
		private static AsyncCallback waitCallback = Fx.ThunkCallback(new AsyncCallback(ListenerHandler.WaitCallback));

		// Token: 0x040028B1 RID: 10417
		private readonly ErrorHandlingAcceptor acceptor;

		// Token: 0x040028B2 RID: 10418
		private readonly ChannelDispatcher channelDispatcher;

		// Token: 0x040028B3 RID: 10419
		private ListenerChannel channel;

		// Token: 0x040028B4 RID: 10420
		private ServiceChannel.SessionIdleManager idleManager;

		// Token: 0x040028B5 RID: 10421
		private bool acceptedNull;

		// Token: 0x040028B6 RID: 10422
		private bool doneAccepting;

		// Token: 0x040028B7 RID: 10423
		private EndpointDispatcherTable endpoints;

		// Token: 0x040028B8 RID: 10424
		private readonly ServiceHostBase host;

		// Token: 0x040028B9 RID: 10425
		private readonly IListenerBinder listenerBinder;

		// Token: 0x040028BA RID: 10426
		private readonly ServiceThrottle throttle;

		// Token: 0x040028BB RID: 10427
		private IDefaultCommunicationTimeouts timeouts;

		// Token: 0x040028BC RID: 10428
		private WrappedTransaction wrappedTransaction;

		// Token: 0x02000C93 RID: 3219
		private class CloseChannelState
		{
			// Token: 0x060078DC RID: 30940 RVA: 0x001C343C File Offset: 0x001C163C
			internal CloseChannelState(ListenerHandler listenerHandler, IChannel channel)
			{
				this.listenerHandler = listenerHandler;
				this.channel = channel;
			}

			// Token: 0x17001B75 RID: 7029
			// (get) Token: 0x060078DD RID: 30941 RVA: 0x001C3452 File Offset: 0x001C1652
			internal ListenerHandler ListenerHandler
			{
				get
				{
					return this.listenerHandler;
				}
			}

			// Token: 0x17001B76 RID: 7030
			// (get) Token: 0x060078DE RID: 30942 RVA: 0x001C345A File Offset: 0x001C165A
			internal IChannel Channel
			{
				get
				{
					return this.channel;
				}
			}

			// Token: 0x040044D4 RID: 17620
			private ListenerHandler listenerHandler;

			// Token: 0x040044D5 RID: 17621
			private IChannel channel;
		}
	}
}
