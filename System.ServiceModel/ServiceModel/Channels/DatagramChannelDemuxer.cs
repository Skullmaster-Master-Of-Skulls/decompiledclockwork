using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000721 RID: 1825
	internal abstract class DatagramChannelDemuxer<TInnerChannel, TInnerItem> : TypedChannelDemuxer, IChannelDemuxer where TInnerChannel : class, IChannel where TInnerItem : class, IDisposable
	{
		// Token: 0x0600453C RID: 17724 RVA: 0x001034E0 File Offset: 0x001016E0
		public DatagramChannelDemuxer(BindingContext context)
		{
			this.filterTable = new MessageFilterTable<IChannelListener>();
			this.innerListener = context.BuildInnerChannelListener<TInnerChannel>();
			if (context.BindingParameters != null)
			{
				this.demuxFailureHandler = context.BindingParameters.Find<IChannelDemuxFailureHandler>();
			}
			this.openSemaphore = new ThreadNeutralSemaphore(1);
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600453D RID: 17725 RVA: 0x0010352F File Offset: 0x0010172F
		protected TInnerChannel InnerChannel
		{
			get
			{
				return this.innerChannel;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600453E RID: 17726 RVA: 0x00103537 File Offset: 0x00101737
		protected IChannelListener<TInnerChannel> InnerListener
		{
			get
			{
				return this.innerListener;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600453F RID: 17727 RVA: 0x0010353F File Offset: 0x0010173F
		protected object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x06004540 RID: 17728 RVA: 0x00103542 File Offset: 0x00101742
		protected IChannelDemuxFailureHandler DemuxFailureHandler
		{
			get
			{
				return this.demuxFailureHandler;
			}
		}

		// Token: 0x06004541 RID: 17729
		protected abstract void AbortItem(TInnerItem item);

		// Token: 0x06004542 RID: 17730
		protected abstract IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004543 RID: 17731
		protected abstract LayeredChannelListener<TChannel> CreateListener<TChannel>(ChannelDemuxerFilter filter) where TChannel : class, IChannel;

		// Token: 0x06004544 RID: 17732
		protected abstract void Dispatch(IChannelListener listener);

		// Token: 0x06004545 RID: 17733
		protected abstract void EndpointNotFound(TInnerItem item);

		// Token: 0x06004546 RID: 17734
		protected abstract TInnerItem EndReceive(IAsyncResult result);

		// Token: 0x06004547 RID: 17735
		protected abstract void EnqueueAndDispatch(IChannelListener listener, TInnerItem item, Action dequeuedCallback, bool canDispatchOnThisThread);

		// Token: 0x06004548 RID: 17736
		protected abstract void EnqueueAndDispatch(IChannelListener listener, Exception exception, Action dequeuedCallback, bool canDispatchOnThisThread);

		// Token: 0x06004549 RID: 17737
		protected abstract Message GetMessage(TInnerItem item);

		// Token: 0x0600454A RID: 17738 RVA: 0x0010354C File Offset: 0x0010174C
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(ChannelDemuxerFilter filter)
		{
			LayeredChannelListener<TChannel> layeredChannelListener = this.CreateListener<TChannel>(filter);
			layeredChannelListener.InnerChannelListener = this.innerListener;
			return layeredChannelListener;
		}

		// Token: 0x0600454B RID: 17739 RVA: 0x00103570 File Offset: 0x00101770
		private bool HandleReceiveResult(IAsyncResult result)
		{
			TInnerItem tinnerItem;
			try
			{
				tinnerItem = this.EndReceive(result);
			}
			catch (CommunicationObjectFaultedException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				return true;
			}
			catch (CommunicationObjectAbortedException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				return true;
			}
			catch (ObjectDisposedException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
				return true;
			}
			catch (CommunicationException exception4)
			{
				DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Information);
				return false;
			}
			catch (TimeoutException ex)
			{
				if (TD.ReceiveTimeoutIsEnabled())
				{
					TD.ReceiveTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				return false;
			}
			catch (Exception exception5)
			{
				if (Fx.IsFatal(exception5))
				{
					throw;
				}
				this.HandleUnknownException(exception5);
				return true;
			}
			if (tinnerItem == null)
			{
				if (this.innerChannel.State == CommunicationState.Opened && DiagnosticUtility.ShouldTraceError)
				{
					TraceUtility.TraceEvent(TraceEventType.Error, 262179, SR.GetString("TraceCodePrematureDatagramEof"), null, this.innerChannel, null);
				}
				return true;
			}
			bool result2;
			try
			{
				result2 = this.ProcessItem(tinnerItem);
			}
			catch (CommunicationException exception6)
			{
				DiagnosticUtility.TraceHandledException(exception6, TraceEventType.Information);
				result2 = false;
			}
			catch (TimeoutException ex2)
			{
				if (TD.ReceiveTimeoutIsEnabled())
				{
					TD.ReceiveTimeout(ex2.Message);
				}
				DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
				result2 = false;
			}
			catch (Exception exception7)
			{
				if (Fx.IsFatal(exception7))
				{
					throw;
				}
				this.HandleUnknownException(exception7);
				result2 = true;
			}
			return result2;
		}

		// Token: 0x0600454C RID: 17740 RVA: 0x00103714 File Offset: 0x00101914
		private IChannelListener MatchListener(Message message)
		{
			IChannelListener result = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.filterTable.GetMatchingValue(message, out result))
				{
					return result;
				}
			}
			return null;
		}

		// Token: 0x0600454D RID: 17741 RVA: 0x00103768 File Offset: 0x00101968
		private void OnItemDequeued()
		{
			this.StartReceiving();
		}

		// Token: 0x0600454E RID: 17742 RVA: 0x00103770 File Offset: 0x00101970
		private static void StartReceivingStatic(object state)
		{
			((DatagramChannelDemuxer<TInnerChannel, TInnerItem>)state).StartReceiving();
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x00103780 File Offset: 0x00101980
		protected void HandleUnknownException(Exception exception)
		{
			DiagnosticUtility.TraceHandledException(exception, TraceEventType.Error);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.filterTable.Count > 0)
				{
					KeyValuePair<MessageFilter, IChannelListener>[] array = new KeyValuePair<MessageFilter, IChannelListener>[this.filterTable.Count];
					this.filterTable.CopyTo(array, 0);
					IChannelListener value = array[0].Value;
					if (this.onItemDequeued == null)
					{
						this.onItemDequeued = new Action(this.OnItemDequeued);
					}
					this.EnqueueAndDispatch(value, exception, this.onItemDequeued, false);
				}
			}
		}

		// Token: 0x06004550 RID: 17744 RVA: 0x00103828 File Offset: 0x00101A28
		private void AbortState()
		{
			if (this.innerChannel != null)
			{
				this.innerChannel.Abort();
			}
			this.innerListener.Abort();
		}

		// Token: 0x06004551 RID: 17745 RVA: 0x00103854 File Offset: 0x00101A54
		public void OnOuterListenerClose(ChannelDemuxerFilter filter, TimeSpan timeout)
		{
			bool flag = false;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.filterTable.ContainsKey(filter.Filter))
				{
					this.filterTable.Remove(filter.Filter);
					int num = this.openCount - 1;
					this.openCount = num;
					if (num == 0)
					{
						flag = true;
					}
				}
			}
			if (flag)
			{
				bool flag3 = false;
				try
				{
					if (this.innerChannel != null)
					{
						this.innerChannel.Close(timeoutHelper.RemainingTime());
					}
					this.innerListener.Close(timeoutHelper.RemainingTime());
					flag3 = true;
				}
				finally
				{
					if (!flag3)
					{
						this.AbortState();
					}
				}
			}
		}

		// Token: 0x06004552 RID: 17746 RVA: 0x00103930 File Offset: 0x00101B30
		public IAsyncResult OnBeginOuterListenerClose(ChannelDemuxerFilter filter, TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.filterTable.ContainsKey(filter.Filter))
				{
					this.filterTable.Remove(filter.Filter);
					int num = this.openCount - 1;
					this.openCount = num;
					if (num == 0)
					{
						flag = true;
					}
				}
			}
			if (!flag)
			{
				return new CompletedAsyncResult(callback, state);
			}
			return new DatagramChannelDemuxer<TInnerChannel, TInnerItem>.CloseAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x001039BC File Offset: 0x00101BBC
		public void OnEndOuterListenerClose(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			DatagramChannelDemuxer<TInnerChannel, TInnerItem>.CloseAsyncResult.End(result);
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x001039D4 File Offset: 0x00101BD4
		public void OnOuterListenerAbort(ChannelDemuxerFilter filter)
		{
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.filterTable.ContainsKey(filter.Filter))
				{
					this.filterTable.Remove(filter.Filter);
					int num = this.openCount - 1;
					this.openCount = num;
					if (num == 0)
					{
						flag = true;
						this.abortOngoingOpen = true;
					}
				}
			}
			if (flag)
			{
				this.AbortState();
			}
		}

		// Token: 0x06004555 RID: 17749 RVA: 0x00103A5C File Offset: 0x00101C5C
		private void ThrowPendingOpenExceptionIfAny()
		{
			if (this.pendingInnerListenerOpenException == null)
			{
				return;
			}
			if (this.pendingInnerListenerOpenException is CommunicationObjectAbortedException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationObjectAbortedException(SR.GetString("PreviousChannelDemuxerOpenFailed", new object[]
				{
					this.pendingInnerListenerOpenException.ToString()
				})));
			}
			if (this.pendingInnerListenerOpenException is CommunicationObjectFaultedException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationObjectFaultedException(SR.GetString("PreviousChannelDemuxerOpenFailed", new object[]
				{
					this.pendingInnerListenerOpenException.ToString()
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationException(SR.GetString("PreviousChannelDemuxerOpenFailed", new object[]
			{
				this.pendingInnerListenerOpenException.ToString()
			})));
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x00103B18 File Offset: 0x00101D18
		private bool ShouldOpenInnerListener(ChannelDemuxerFilter filter, IChannelListener listener)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (listener.State == CommunicationState.Closed || listener.State == CommunicationState.Closing)
				{
					return false;
				}
				this.filterTable.Add(filter.Filter, listener, filter.Priority);
				int num = this.openCount + 1;
				this.openCount = num;
				if (num == 1)
				{
					this.abortOngoingOpen = false;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x00103BA4 File Offset: 0x00101DA4
		public void OnOuterListenerOpen(ChannelDemuxerFilter filter, IChannelListener listener, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.openSemaphore.Enter(timeoutHelper.RemainingTime());
			try
			{
				bool flag = this.ShouldOpenInnerListener(filter, listener);
				if (flag)
				{
					try
					{
						this.innerListener.Open(timeoutHelper.RemainingTime());
						this.innerChannel = this.innerListener.AcceptChannel(timeoutHelper.RemainingTime());
						this.innerChannel.Open(timeoutHelper.RemainingTime());
						object thisLock = this.ThisLock;
						lock (thisLock)
						{
							if (this.abortOngoingOpen)
							{
								this.AbortState();
								return;
							}
						}
						ActionItem.Schedule(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.startReceivingStatic, this);
						return;
					}
					catch (Exception ex)
					{
						this.pendingInnerListenerOpenException = ex;
						throw;
					}
				}
				this.ThrowPendingOpenExceptionIfAny();
			}
			finally
			{
				this.openSemaphore.Exit();
			}
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x00103CA0 File Offset: 0x00101EA0
		public IAsyncResult OnBeginOuterListenerOpen(ChannelDemuxerFilter filter, IChannelListener listener, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult(this, filter, listener, timeout, callback, state);
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x00103CAF File Offset: 0x00101EAF
		public void OnEndOuterListenerOpen(IAsyncResult result)
		{
			DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.End(result);
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x00103CB7 File Offset: 0x00101EB7
		private void OnReceiveComplete(IAsyncResult result)
		{
			if (!this.HandleReceiveResult(result))
			{
				this.StartReceiving();
			}
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x00103CC8 File Offset: 0x00101EC8
		private static void OnReceiveCompleteStatic(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((DatagramChannelDemuxer<TInnerChannel, TInnerItem>)result.AsyncState).OnReceiveComplete(result);
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x00103CE4 File Offset: 0x00101EE4
		private bool ProcessItem(TInnerItem item)
		{
			bool result;
			try
			{
				Message message = null;
				IChannelListener channelListener = null;
				try
				{
					message = this.GetMessage(item);
					channelListener = this.MatchListener(message);
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					return false;
				}
				catch (MultipleFilterMatchesException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					return false;
				}
				catch (XmlException exception3)
				{
					DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
					return false;
				}
				catch (Exception exception4)
				{
					if (Fx.IsFatal(exception4))
					{
						throw;
					}
					this.HandleUnknownException(exception4);
					return true;
				}
				if (channelListener == null)
				{
					ErrorBehavior.ThrowAndCatch(new EndpointNotFoundException(SR.GetString("UnableToDemuxChannel", new object[]
					{
						message.Headers.Action
					})), message);
					this.EndpointNotFound(item);
					item = default(TInnerItem);
					result = false;
				}
				else
				{
					if (this.onItemDequeued == null)
					{
						this.onItemDequeued = new Action(this.OnItemDequeued);
					}
					this.EnqueueAndDispatch(channelListener, item, this.onItemDequeued, false);
					item = default(TInnerItem);
					result = true;
				}
			}
			finally
			{
				if (item != null)
				{
					this.AbortItem(item);
				}
			}
			return result;
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x00103E1C File Offset: 0x0010201C
		private void StartReceiving()
		{
			while (this.innerChannel.State == CommunicationState.Opened)
			{
				IAsyncResult asyncResult;
				try
				{
					asyncResult = this.BeginReceive(TimeSpan.MaxValue, DatagramChannelDemuxer<TInnerChannel, TInnerItem>.onReceiveComplete, this);
				}
				catch (CommunicationObjectFaultedException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					return;
				}
				catch (CommunicationObjectAbortedException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					return;
				}
				catch (ObjectDisposedException exception3)
				{
					DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
					return;
				}
				catch (CommunicationException exception4)
				{
					DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Information);
					continue;
				}
				catch (TimeoutException ex)
				{
					if (TD.ReceiveTimeoutIsEnabled())
					{
						TD.ReceiveTimeout(ex.Message);
					}
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
					continue;
				}
				catch (Exception exception5)
				{
					if (Fx.IsFatal(exception5))
					{
						throw;
					}
					this.HandleUnknownException(exception5);
					return;
				}
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				if (this.HandleReceiveResult(asyncResult))
				{
					return;
				}
			}
		}

		// Token: 0x04002D53 RID: 11603
		private MessageFilterTable<IChannelListener> filterTable;

		// Token: 0x04002D54 RID: 11604
		private TInnerChannel innerChannel;

		// Token: 0x04002D55 RID: 11605
		private IChannelListener<TInnerChannel> innerListener;

		// Token: 0x04002D56 RID: 11606
		private static AsyncCallback onReceiveComplete = Fx.ThunkCallback(new AsyncCallback(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OnReceiveCompleteStatic));

		// Token: 0x04002D57 RID: 11607
		private static Action<object> startReceivingStatic = new Action<object>(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.StartReceivingStatic);

		// Token: 0x04002D58 RID: 11608
		private Action onItemDequeued;

		// Token: 0x04002D59 RID: 11609
		private int openCount;

		// Token: 0x04002D5A RID: 11610
		private IChannelDemuxFailureHandler demuxFailureHandler;

		// Token: 0x04002D5B RID: 11611
		private ThreadNeutralSemaphore openSemaphore;

		// Token: 0x04002D5C RID: 11612
		private Exception pendingInnerListenerOpenException;

		// Token: 0x04002D5D RID: 11613
		private bool abortOngoingOpen;

		// Token: 0x02000CC8 RID: 3272
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x0600799D RID: 31133 RVA: 0x001C59C0 File Offset: 0x001C3BC0
			public OpenAsyncResult(DatagramChannelDemuxer<TInnerChannel, TInnerItem> channelDemuxer, ChannelDemuxerFilter filter, IChannelListener listener, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channelDemuxer = channelDemuxer;
				this.filter = filter;
				this.listener = listener;
				this.timeoutHelper = new TimeoutHelper(timeout);
				if (!this.channelDemuxer.openSemaphore.EnterAsync(this.timeoutHelper.RemainingTime(), DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.waitOverCallback, this))
				{
					return;
				}
				bool flag = false;
				bool flag2 = false;
				try
				{
					flag2 = this.OnWaitOver();
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.Cleanup();
					}
				}
				if (flag2)
				{
					this.Cleanup();
					base.Complete(true);
				}
			}

			// Token: 0x0600799E RID: 31134 RVA: 0x001C5A58 File Offset: 0x001C3C58
			private static void WaitOverCallback(object state, Exception asyncException)
			{
				DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult openAsyncResult = (DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult)state;
				Exception ex = asyncException;
				bool flag = false;
				if (ex != null)
				{
					flag = true;
				}
				else
				{
					try
					{
						flag = openAsyncResult.OnWaitOver();
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						flag = true;
						ex = ex2;
					}
				}
				if (flag)
				{
					openAsyncResult.Cleanup();
					openAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x0600799F RID: 31135 RVA: 0x001C5AB4 File Offset: 0x001C3CB4
			private bool OnWaitOver()
			{
				this.openInnerListener = this.channelDemuxer.ShouldOpenInnerListener(this.filter, this.listener);
				if (!this.openInnerListener)
				{
					this.channelDemuxer.ThrowPendingOpenExceptionIfAny();
					return true;
				}
				return this.OnOpenInnerListener();
			}

			// Token: 0x060079A0 RID: 31136 RVA: 0x001C5AF0 File Offset: 0x001C3CF0
			private bool OnInnerListenerEndOpen(IAsyncResult result)
			{
				this.channelDemuxer.innerListener.EndOpen(result);
				result = this.channelDemuxer.innerListener.BeginAcceptChannel(this.timeoutHelper.RemainingTime(), DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.acceptChannelCallback, this);
				return result.CompletedSynchronously && this.OnEndAcceptChannel(result);
			}

			// Token: 0x060079A1 RID: 31137 RVA: 0x001C5B44 File Offset: 0x001C3D44
			private bool OnOpenInnerListener()
			{
				bool result;
				try
				{
					IAsyncResult asyncResult = this.channelDemuxer.innerListener.BeginOpen(this.timeoutHelper.RemainingTime(), DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.openListenerCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						result = false;
					}
					else
					{
						this.OnInnerListenerEndOpen(asyncResult);
						result = true;
					}
				}
				catch (Exception pendingInnerListenerOpenException)
				{
					this.channelDemuxer.pendingInnerListenerOpenException = pendingInnerListenerOpenException;
					throw;
				}
				return result;
			}

			// Token: 0x060079A2 RID: 31138 RVA: 0x001C5BAC File Offset: 0x001C3DAC
			private static void OpenListenerCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult openAsyncResult = (DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult)result.AsyncState;
				Exception ex = null;
				try
				{
					openAsyncResult.OnInnerListenerEndOpen(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					openAsyncResult.channelDemuxer.pendingInnerListenerOpenException = ex;
				}
				openAsyncResult.Cleanup();
				openAsyncResult.Complete(false, ex);
			}

			// Token: 0x060079A3 RID: 31139 RVA: 0x001C5C18 File Offset: 0x001C3E18
			private static void AcceptChannelCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult openAsyncResult = (DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult)result.AsyncState;
				Exception ex = null;
				bool flag = false;
				try
				{
					flag = openAsyncResult.OnEndAcceptChannel(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
					flag = true;
				}
				if (flag)
				{
					if (ex != null)
					{
						openAsyncResult.channelDemuxer.pendingInnerListenerOpenException = ex;
					}
					openAsyncResult.Cleanup();
					openAsyncResult.Complete(false, ex);
				}
			}

			// Token: 0x060079A4 RID: 31140 RVA: 0x001C5C8C File Offset: 0x001C3E8C
			private bool OnEndAcceptChannel(IAsyncResult result)
			{
				this.channelDemuxer.innerChannel = this.channelDemuxer.innerListener.EndAcceptChannel(result);
				IAsyncResult asyncResult = this.channelDemuxer.innerChannel.BeginOpen(this.timeoutHelper.RemainingTime(), DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.acceptChannelCallback, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				this.OnEndOpenChannel(asyncResult);
				return true;
			}

			// Token: 0x060079A5 RID: 31141 RVA: 0x001C5CF0 File Offset: 0x001C3EF0
			private static void OpenChannelCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult openAsyncResult = (DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult)result.AsyncState;
				Exception ex = null;
				try
				{
					openAsyncResult.OnEndOpenChannel(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					openAsyncResult.channelDemuxer.pendingInnerListenerOpenException = ex;
				}
				openAsyncResult.Cleanup();
				openAsyncResult.Complete(false, ex);
			}

			// Token: 0x060079A6 RID: 31142 RVA: 0x001C5D5C File Offset: 0x001C3F5C
			private void OnEndOpenChannel(IAsyncResult result)
			{
				this.channelDemuxer.innerChannel.EndOpen(result);
				object thisLock = this.channelDemuxer.ThisLock;
				lock (thisLock)
				{
					if (this.channelDemuxer.abortOngoingOpen)
					{
						this.channelDemuxer.AbortState();
						return;
					}
				}
				ActionItem.Schedule(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.startReceivingStatic, this.channelDemuxer);
			}

			// Token: 0x060079A7 RID: 31143 RVA: 0x001C5DDC File Offset: 0x001C3FDC
			private void Cleanup()
			{
				this.channelDemuxer.openSemaphore.Exit();
			}

			// Token: 0x060079A8 RID: 31144 RVA: 0x001C5DEF File Offset: 0x001C3FEF
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult>(result);
			}

			// Token: 0x04004592 RID: 17810
			private static FastAsyncCallback waitOverCallback = new FastAsyncCallback(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.WaitOverCallback);

			// Token: 0x04004593 RID: 17811
			private static AsyncCallback openListenerCallback = Fx.ThunkCallback(new AsyncCallback(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.OpenListenerCallback));

			// Token: 0x04004594 RID: 17812
			private static AsyncCallback acceptChannelCallback = Fx.ThunkCallback(new AsyncCallback(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.AcceptChannelCallback));

			// Token: 0x04004595 RID: 17813
			private static AsyncCallback openChannelCallback = Fx.ThunkCallback(new AsyncCallback(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.OpenChannelCallback));

			// Token: 0x04004596 RID: 17814
			private DatagramChannelDemuxer<TInnerChannel, TInnerItem> channelDemuxer;

			// Token: 0x04004597 RID: 17815
			private ChannelDemuxerFilter filter;

			// Token: 0x04004598 RID: 17816
			private IChannelListener listener;

			// Token: 0x04004599 RID: 17817
			private TimeoutHelper timeoutHelper;

			// Token: 0x0400459A RID: 17818
			private bool openInnerListener;
		}

		// Token: 0x02000CC9 RID: 3273
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x060079AA RID: 31146 RVA: 0x001C5E58 File Offset: 0x001C4058
			public CloseAsyncResult(DatagramChannelDemuxer<TInnerChannel, TInnerItem> channelDemuxer, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channelDemuxer = channelDemuxer;
				this.timeoutHelper = new TimeoutHelper(timeout);
				if (channelDemuxer.innerChannel != null)
				{
					bool flag = false;
					try
					{
						IAsyncResult asyncResult = channelDemuxer.innerChannel.BeginClose(this.timeoutHelper.RemainingTime(), DatagramChannelDemuxer<TInnerChannel, TInnerItem>.CloseAsyncResult.sharedCallback, this);
						if (!asyncResult.CompletedSynchronously)
						{
							flag = true;
							return;
						}
						channelDemuxer.innerChannel.EndClose(asyncResult);
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							this.channelDemuxer.AbortState();
						}
					}
				}
				if (this.OnInnerChannelClosed())
				{
					base.Complete(true);
				}
			}

			// Token: 0x060079AB RID: 31147 RVA: 0x001C5F04 File Offset: 0x001C4104
			private bool OnInnerChannelClosed()
			{
				this.closedInnerChannel = true;
				bool flag = false;
				try
				{
					IAsyncResult asyncResult = this.channelDemuxer.innerListener.BeginClose(this.timeoutHelper.RemainingTime(), DatagramChannelDemuxer<TInnerChannel, TInnerItem>.CloseAsyncResult.sharedCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						flag = true;
						return false;
					}
					this.channelDemuxer.innerListener.EndClose(asyncResult);
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.channelDemuxer.AbortState();
					}
				}
				return true;
			}

			// Token: 0x060079AC RID: 31148 RVA: 0x001C5F84 File Offset: 0x001C4184
			private static void SharedCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				DatagramChannelDemuxer<TInnerChannel, TInnerItem>.CloseAsyncResult closeAsyncResult = (DatagramChannelDemuxer<TInnerChannel, TInnerItem>.CloseAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				bool flag2 = false;
				try
				{
					if (!closeAsyncResult.closedInnerChannel)
					{
						closeAsyncResult.channelDemuxer.innerChannel.EndClose(result);
						flag = closeAsyncResult.OnInnerChannelClosed();
						flag2 = true;
					}
					else
					{
						closeAsyncResult.channelDemuxer.innerListener.EndClose(result);
						flag = true;
						flag2 = true;
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				finally
				{
					if (!flag2)
					{
						closeAsyncResult.channelDemuxer.AbortState();
					}
				}
				if (flag)
				{
					closeAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060079AD RID: 31149 RVA: 0x001C6038 File Offset: 0x001C4238
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<DatagramChannelDemuxer<TInnerChannel, TInnerItem>.CloseAsyncResult>(result);
			}

			// Token: 0x0400459B RID: 17819
			private static AsyncCallback sharedCallback = Fx.ThunkCallback(new AsyncCallback(DatagramChannelDemuxer<TInnerChannel, TInnerItem>.CloseAsyncResult.SharedCallback));

			// Token: 0x0400459C RID: 17820
			private DatagramChannelDemuxer<TInnerChannel, TInnerItem> channelDemuxer;

			// Token: 0x0400459D RID: 17821
			private TimeoutHelper timeoutHelper;

			// Token: 0x0400459E RID: 17822
			private bool closedInnerChannel;
		}
	}
}
