using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Dispatcher;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000727 RID: 1831
	internal abstract class SessionChannelDemuxer<TInnerChannel, TInnerItem> : TypedChannelDemuxer, IChannelDemuxer where TInnerChannel : class, IChannel where TInnerItem : class, IDisposable
	{
		// Token: 0x06004591 RID: 17809 RVA: 0x0010462C File Offset: 0x0010282C
		public SessionChannelDemuxer(BindingContext context, TimeSpan peekTimeout, int maxPendingSessions)
		{
			if (context.BindingParameters != null)
			{
				this.demuxFailureHandler = context.BindingParameters.Find<IChannelDemuxFailureHandler>();
			}
			this.innerListener = context.BuildInnerChannelListener<TInnerChannel>();
			this.filterTable = new MessageFilterTable<InputQueueChannelListener<TInnerChannel>>();
			this.openSemaphore = new ThreadNeutralSemaphore(1);
			this.peekTimeout = peekTimeout;
			this.throttle = new FlowThrottle(SessionChannelDemuxer<TInnerChannel, TInnerItem>.scheduleAcceptStatic, maxPendingSessions, null, null);
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06004592 RID: 17810 RVA: 0x00104695 File Offset: 0x00102895
		protected object ThisLock
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06004593 RID: 17811 RVA: 0x00104698 File Offset: 0x00102898
		protected IChannelDemuxFailureHandler DemuxFailureHandler
		{
			get
			{
				return this.demuxFailureHandler;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06004594 RID: 17812 RVA: 0x001046A0 File Offset: 0x001028A0
		private Action<object> OnStartAccepting
		{
			get
			{
				if (this.onStartAccepting == null)
				{
					this.onStartAccepting = new Action<object>(this.OnStartAcceptingCallback);
				}
				return this.onStartAccepting;
			}
		}

		// Token: 0x06004595 RID: 17813
		protected abstract void AbortItem(TInnerItem item);

		// Token: 0x06004596 RID: 17814
		protected abstract IAsyncResult BeginReceive(TInnerChannel channel, AsyncCallback callback, object state);

		// Token: 0x06004597 RID: 17815
		protected abstract IAsyncResult BeginReceive(TInnerChannel channel, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06004598 RID: 17816
		protected abstract TInnerChannel CreateChannel(ChannelManagerBase channelManager, TInnerChannel innerChannel, TInnerItem firstItem);

		// Token: 0x06004599 RID: 17817
		protected abstract void EndpointNotFound(TInnerChannel channel, TInnerItem item);

		// Token: 0x0600459A RID: 17818
		protected abstract TInnerItem EndReceive(TInnerChannel channel, IAsyncResult result);

		// Token: 0x0600459B RID: 17819
		protected abstract Message GetMessage(TInnerItem item);

		// Token: 0x0600459C RID: 17820 RVA: 0x001046C4 File Offset: 0x001028C4
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(ChannelDemuxerFilter filter)
		{
			return new InputQueueChannelListener<TChannel>(filter, this)
			{
				InnerChannelListener = this.innerListener
			};
		}

		// Token: 0x0600459D RID: 17821 RVA: 0x001046E8 File Offset: 0x001028E8
		private bool BeginAcceptChannel(bool requiresThrottle, out IAsyncResult result)
		{
			result = null;
			if (requiresThrottle && !this.throttle.Acquire(this))
			{
				return false;
			}
			bool flag = true;
			try
			{
				result = this.innerListener.BeginAcceptChannel(TimeSpan.MaxValue, SessionChannelDemuxer<TInnerChannel, TInnerItem>.onAcceptComplete, this);
				flag = false;
			}
			catch (CommunicationObjectFaultedException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				return false;
			}
			catch (CommunicationObjectAbortedException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				return false;
			}
			catch (ObjectDisposedException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
				return false;
			}
			catch (CommunicationException exception4)
			{
				DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Information);
				return true;
			}
			catch (TimeoutException ex)
			{
				if (TD.OpenTimeoutIsEnabled())
				{
					TD.OpenTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				return true;
			}
			catch (Exception exception5)
			{
				if (Fx.IsFatal(exception5))
				{
					throw;
				}
				this.HandleUnknownException(exception5);
				flag = false;
				return false;
			}
			finally
			{
				if (flag)
				{
					this.throttle.Release();
				}
			}
			return true;
		}

		// Token: 0x0600459E RID: 17822 RVA: 0x00104804 File Offset: 0x00102A04
		private bool EndAcceptChannel(IAsyncResult result, out TInnerChannel channel)
		{
			channel = default(TInnerChannel);
			bool flag = true;
			try
			{
				channel = this.innerListener.EndAcceptChannel(result);
				flag = (channel == null);
			}
			catch (CommunicationObjectFaultedException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				return false;
			}
			catch (CommunicationObjectAbortedException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				return false;
			}
			catch (ObjectDisposedException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
				return false;
			}
			catch (CommunicationException exception4)
			{
				DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Information);
				return true;
			}
			catch (TimeoutException ex)
			{
				if (TD.OpenTimeoutIsEnabled())
				{
					TD.OpenTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				return true;
			}
			catch (Exception exception5)
			{
				if (Fx.IsFatal(exception5))
				{
					throw;
				}
				this.HandleUnknownException(exception5);
				flag = false;
				return false;
			}
			finally
			{
				if (flag)
				{
					this.throttle.Release();
				}
			}
			return channel != null;
		}

		// Token: 0x0600459F RID: 17823 RVA: 0x0010492C File Offset: 0x00102B2C
		private void PeekChannel(TInnerChannel channel)
		{
			bool flag = true;
			try
			{
				IAsyncResult asyncResult = new SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult(this, channel, SessionChannelDemuxer<TInnerChannel, TInnerItem>.onPeekComplete, this);
				flag = false;
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				channel = default(TInnerChannel);
				this.HandlePeekResult(asyncResult);
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (TimeoutException ex)
			{
				if (TD.OpenTimeoutIsEnabled())
				{
					TD.OpenTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			catch (ObjectDisposedException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			catch (Exception exception3)
			{
				if (Fx.IsFatal(exception3))
				{
					throw;
				}
				this.HandleUnknownException(exception3);
				flag = false;
			}
			if (channel != null)
			{
				channel.Abort();
			}
			if (flag)
			{
				this.throttle.Release();
			}
		}

		// Token: 0x060045A0 RID: 17824 RVA: 0x00104A08 File Offset: 0x00102C08
		private void HandlePeekResult(IAsyncResult result)
		{
			TInnerChannel tinnerChannel = default(TInnerChannel);
			bool flag = false;
			bool flag2 = true;
			TInnerItem tinnerItem;
			try
			{
				SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult.End(result, out tinnerChannel, out tinnerItem);
				flag2 = (tinnerItem == null);
			}
			catch (ObjectDisposedException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				flag = true;
				return;
			}
			catch (CommunicationException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				flag = true;
				return;
			}
			catch (TimeoutException ex)
			{
				if (TD.OpenTimeoutIsEnabled())
				{
					TD.OpenTimeout(ex.Message);
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				flag = true;
				return;
			}
			catch (Exception exception3)
			{
				if (Fx.IsFatal(exception3))
				{
					throw;
				}
				this.HandleUnknownException(exception3);
				flag2 = false;
				return;
			}
			finally
			{
				if (flag && tinnerChannel != null)
				{
					tinnerChannel.Abort();
				}
				if (flag2)
				{
					this.throttle.Release();
				}
			}
			if (tinnerItem != null)
			{
				flag2 = true;
				try
				{
					this.ProcessItem(tinnerChannel, tinnerItem);
					flag2 = false;
				}
				catch (CommunicationException exception4)
				{
					DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Information);
				}
				catch (TimeoutException ex2)
				{
					if (TD.OpenTimeoutIsEnabled())
					{
						TD.OpenTimeout(ex2.Message);
					}
					DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
				}
				catch (Exception exception5)
				{
					if (Fx.IsFatal(exception5))
					{
						throw;
					}
					this.HandleUnknownException(exception5);
					flag2 = false;
				}
				finally
				{
					if (flag2)
					{
						this.throttle.Release();
					}
				}
			}
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x00104B9C File Offset: 0x00102D9C
		private InputQueueChannelListener<TInnerChannel> MatchListener(Message message)
		{
			InputQueueChannelListener<TInnerChannel> result = null;
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

		// Token: 0x060045A2 RID: 17826 RVA: 0x00104BF0 File Offset: 0x00102DF0
		private static void OnAcceptCompleteStatic(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((SessionChannelDemuxer<TInnerChannel, TInnerItem>)result.AsyncState).OnStartAcceptingCallback(result);
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x00104C0C File Offset: 0x00102E0C
		private static void ScheduleAcceptStatic(object state)
		{
			ActionItem.Schedule(SessionChannelDemuxer<TInnerChannel, TInnerItem>.startAcceptStatic, state);
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x00104C19 File Offset: 0x00102E19
		private static void StartAcceptStatic(object state)
		{
			((SessionChannelDemuxer<TInnerChannel, TInnerItem>)state).StartAccepting(false);
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x00104C28 File Offset: 0x00102E28
		private bool ShouldStartAccepting(ChannelDemuxerFilter filter, IChannelListener listener)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (listener.State == CommunicationState.Closed || listener.State == CommunicationState.Closing)
				{
					return false;
				}
				this.filterTable.Add(filter.Filter, (InputQueueChannelListener<TInnerChannel>)listener, filter.Priority);
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

		// Token: 0x060045A6 RID: 17830 RVA: 0x00104CB8 File Offset: 0x00102EB8
		private void StartAccepting(bool requiresThrottle)
		{
			IAsyncResult asyncResult;
			bool flag = this.BeginAcceptChannel(requiresThrottle, out asyncResult);
			if (flag && (asyncResult == null || asyncResult.CompletedSynchronously))
			{
				ActionItem.Schedule(this.OnStartAccepting, asyncResult);
			}
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x00104CE9 File Offset: 0x00102EE9
		private void OnItemDequeued()
		{
			this.throttle.Release();
		}

		// Token: 0x060045A8 RID: 17832 RVA: 0x00104CF8 File Offset: 0x00102EF8
		private void ThrowPendingOpenExceptionIfAny()
		{
			if (this.pendingExceptionOnOpen == null)
			{
				return;
			}
			if (this.pendingExceptionOnOpen is CommunicationObjectAbortedException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationObjectAbortedException(SR.GetString("PreviousChannelDemuxerOpenFailed", new object[]
				{
					this.pendingExceptionOnOpen.ToString()
				})));
			}
			if (this.pendingExceptionOnOpen is CommunicationObjectFaultedException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationObjectFaultedException(SR.GetString("PreviousChannelDemuxerOpenFailed", new object[]
				{
					this.pendingExceptionOnOpen.ToString()
				})));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new CommunicationException(SR.GetString("PreviousChannelDemuxerOpenFailed", new object[]
			{
				this.pendingExceptionOnOpen.ToString()
			})));
		}

		// Token: 0x060045A9 RID: 17833 RVA: 0x00104DB4 File Offset: 0x00102FB4
		public void OnOuterListenerOpen(ChannelDemuxerFilter filter, IChannelListener listener, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.openSemaphore.Enter(timeoutHelper.RemainingTime());
			try
			{
				bool flag = this.ShouldStartAccepting(filter, listener);
				if (flag)
				{
					try
					{
						this.innerListener.Open(timeoutHelper.RemainingTime());
						this.StartAccepting(true);
						object thisLock = this.ThisLock;
						lock (thisLock)
						{
							if (this.abortOngoingOpen)
							{
								this.innerListener.Abort();
							}
						}
						return;
					}
					catch (Exception ex)
					{
						this.pendingExceptionOnOpen = ex;
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

		// Token: 0x060045AA RID: 17834 RVA: 0x00104E7C File Offset: 0x0010307C
		public IAsyncResult OnBeginOuterListenerOpen(ChannelDemuxerFilter filter, IChannelListener listener, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult(this, filter, listener, timeout, callback, state);
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x00104E8B File Offset: 0x0010308B
		public void OnEndOuterListenerOpen(IAsyncResult result)
		{
			SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.End(result);
		}

		// Token: 0x060045AC RID: 17836 RVA: 0x00104E94 File Offset: 0x00103094
		private bool ShouldCloseInnerListener(ChannelDemuxerFilter filter, bool aborted)
		{
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
						if (aborted)
						{
							this.abortOngoingOpen = true;
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060045AD RID: 17837 RVA: 0x00104F18 File Offset: 0x00103118
		public void OnOuterListenerAbort(ChannelDemuxerFilter filter)
		{
			if (this.ShouldCloseInnerListener(filter, true))
			{
				this.innerListener.Abort();
			}
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x00104F30 File Offset: 0x00103130
		public void OnOuterListenerClose(ChannelDemuxerFilter filter, TimeSpan timeout)
		{
			if (this.ShouldCloseInnerListener(filter, false))
			{
				bool flag = false;
				try
				{
					this.innerListener.Close(timeout);
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.innerListener.Abort();
					}
				}
			}
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x00104F78 File Offset: 0x00103178
		public IAsyncResult OnBeginOuterListenerClose(ChannelDemuxerFilter filter, TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.ShouldCloseInnerListener(filter, false))
			{
				bool flag = false;
				try
				{
					IAsyncResult result = this.innerListener.BeginClose(timeout, callback, state);
					flag = true;
					return result;
				}
				finally
				{
					if (!flag)
					{
						this.innerListener.Abort();
					}
				}
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x00104FD0 File Offset: 0x001031D0
		public void OnEndOuterListenerClose(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			bool flag = false;
			try
			{
				this.innerListener.EndClose(result);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.innerListener.Abort();
				}
			}
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x00105020 File Offset: 0x00103220
		private void OnStartAcceptingCallback(object state)
		{
			IAsyncResult asyncResult = (IAsyncResult)state;
			TInnerChannel channelToPeek = default(TInnerChannel);
			if (asyncResult == null || this.EndAcceptChannel(asyncResult, out channelToPeek))
			{
				this.StartAccepting(channelToPeek);
			}
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x00105054 File Offset: 0x00103254
		private static void OnPeekCompleteStatic(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			SessionChannelDemuxer<TInnerChannel, TInnerItem> sessionChannelDemuxer = (SessionChannelDemuxer<TInnerChannel, TInnerItem>)result.AsyncState;
			bool flag = true;
			try
			{
				sessionChannelDemuxer.HandlePeekResult(result);
				flag = false;
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (ObjectDisposedException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
			catch (Exception exception3)
			{
				if (Fx.IsFatal(exception3))
				{
					throw;
				}
				sessionChannelDemuxer.HandleUnknownException(exception3);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					sessionChannelDemuxer.throttle.Release();
				}
			}
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x001050F4 File Offset: 0x001032F4
		private void ProcessItem(TInnerChannel channel, TInnerItem item)
		{
			InputQueueChannelListener<TInnerChannel> inputQueueChannelListener = null;
			TInnerChannel tinnerChannel = default(TInnerChannel);
			bool flag = true;
			try
			{
				Message message = this.GetMessage(item);
				try
				{
					inputQueueChannelListener = this.MatchListener(message);
					flag = (inputQueueChannelListener == null);
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					return;
				}
				catch (MultipleFilterMatchesException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					return;
				}
				catch (XmlException exception3)
				{
					DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
					return;
				}
				finally
				{
					if (flag)
					{
						this.throttle.Release();
					}
				}
				if (inputQueueChannelListener == null)
				{
					try
					{
						throw TraceUtility.ThrowHelperError(new EndpointNotFoundException(SR.GetString("UnableToDemuxChannel", new object[]
						{
							message.Headers.Action
						})), message);
					}
					catch (EndpointNotFoundException exception4)
					{
						DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Information);
						this.EndpointNotFound(channel, item);
						channel = default(TInnerChannel);
						item = default(TInnerItem);
						return;
					}
				}
				tinnerChannel = this.CreateChannel(inputQueueChannelListener, channel, item);
				channel = default(TInnerChannel);
				item = default(TInnerItem);
			}
			finally
			{
				if (item != null)
				{
					this.AbortItem(item);
				}
				if (channel != null)
				{
					channel.Abort();
				}
			}
			bool flag2 = false;
			try
			{
				if (this.onItemDequeued == null)
				{
					this.onItemDequeued = new Action(this.OnItemDequeued);
				}
				inputQueueChannelListener.InputQueueAcceptor.EnqueueAndDispatch(tinnerChannel, this.onItemDequeued, false);
				flag2 = true;
			}
			catch (Exception exception5)
			{
				if (Fx.IsFatal(exception5))
				{
					throw;
				}
				this.HandleUnknownException(exception5);
			}
			finally
			{
				if (!flag2)
				{
					this.throttle.Release();
					tinnerChannel.Abort();
				}
			}
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x001052C0 File Offset: 0x001034C0
		protected void HandleUnknownException(Exception exception)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.filterTable.Count > 0)
				{
					KeyValuePair<MessageFilter, InputQueueChannelListener<TInnerChannel>>[] array = new KeyValuePair<MessageFilter, InputQueueChannelListener<TInnerChannel>>[this.filterTable.Count];
					this.filterTable.CopyTo(array, 0);
					InputQueueChannelListener<TInnerChannel> value = array[0].Value;
					if (this.onItemDequeued == null)
					{
						this.onItemDequeued = new Action(this.OnItemDequeued);
					}
					value.InputQueueAcceptor.EnqueueAndDispatch(exception, this.onItemDequeued, false);
				}
			}
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x00105364 File Offset: 0x00103564
		private void StartAccepting(TInnerChannel channelToPeek)
		{
			IAsyncResult asyncResult;
			bool flag;
			for (;;)
			{
				flag = this.BeginAcceptChannel(true, out asyncResult);
				if (channelToPeek != null)
				{
					break;
				}
				if (!flag)
				{
					return;
				}
				if (asyncResult != null)
				{
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					if (!this.EndAcceptChannel(asyncResult, out channelToPeek))
					{
						return;
					}
				}
			}
			if (flag && (asyncResult == null || asyncResult.CompletedSynchronously))
			{
				ActionItem.Schedule(this.OnStartAccepting, asyncResult);
			}
			this.PeekChannel(channelToPeek);
		}

		// Token: 0x04002D60 RID: 11616
		private IChannelDemuxFailureHandler demuxFailureHandler;

		// Token: 0x04002D61 RID: 11617
		private MessageFilterTable<InputQueueChannelListener<TInnerChannel>> filterTable;

		// Token: 0x04002D62 RID: 11618
		private IChannelListener<TInnerChannel> innerListener;

		// Token: 0x04002D63 RID: 11619
		private static AsyncCallback onAcceptComplete = Fx.ThunkCallback(new AsyncCallback(SessionChannelDemuxer<TInnerChannel, TInnerItem>.OnAcceptCompleteStatic));

		// Token: 0x04002D64 RID: 11620
		private static AsyncCallback onPeekComplete = Fx.ThunkCallback(new AsyncCallback(SessionChannelDemuxer<TInnerChannel, TInnerItem>.OnPeekCompleteStatic));

		// Token: 0x04002D65 RID: 11621
		private Action onItemDequeued;

		// Token: 0x04002D66 RID: 11622
		private static WaitCallback scheduleAcceptStatic = new WaitCallback(SessionChannelDemuxer<TInnerChannel, TInnerItem>.ScheduleAcceptStatic);

		// Token: 0x04002D67 RID: 11623
		private static Action<object> startAcceptStatic = new Action<object>(SessionChannelDemuxer<TInnerChannel, TInnerItem>.StartAcceptStatic);

		// Token: 0x04002D68 RID: 11624
		private Action<object> onStartAccepting;

		// Token: 0x04002D69 RID: 11625
		private int openCount;

		// Token: 0x04002D6A RID: 11626
		private ThreadNeutralSemaphore openSemaphore;

		// Token: 0x04002D6B RID: 11627
		private Exception pendingExceptionOnOpen;

		// Token: 0x04002D6C RID: 11628
		private bool abortOngoingOpen;

		// Token: 0x04002D6D RID: 11629
		private FlowThrottle throttle;

		// Token: 0x04002D6E RID: 11630
		private TimeSpan peekTimeout;

		// Token: 0x02000CCC RID: 3276
		private class PeekAsyncResult : AsyncResult
		{
			// Token: 0x060079BB RID: 31163 RVA: 0x001C611C File Offset: 0x001C431C
			public PeekAsyncResult(SessionChannelDemuxer<TInnerChannel, TInnerItem> demuxer, TInnerChannel channel, AsyncCallback callback, object state) : base(callback, state)
			{
				this.demuxer = demuxer;
				this.channel = channel;
				IAsyncResult asyncResult = this.channel.BeginOpen(SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult.onOpenComplete, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				if (this.HandleOpenComplete(asyncResult))
				{
					base.Complete(true);
				}
			}

			// Token: 0x060079BC RID: 31164 RVA: 0x001C6170 File Offset: 0x001C4370
			public static void End(IAsyncResult result, out TInnerChannel channel, out TInnerItem item)
			{
				SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult peekAsyncResult = AsyncResult.End<SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult>(result);
				channel = peekAsyncResult.channel;
				item = peekAsyncResult.item;
			}

			// Token: 0x060079BD RID: 31165 RVA: 0x001C619C File Offset: 0x001C439C
			private bool HandleOpenComplete(IAsyncResult result)
			{
				this.channel.EndOpen(result);
				IAsyncResult asyncResult;
				if (this.demuxer.peekTimeout == ChannelDemuxer.UseDefaultReceiveTimeout)
				{
					asyncResult = this.demuxer.BeginReceive(this.channel, SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult.onReceiveComplete, this);
				}
				else
				{
					asyncResult = this.demuxer.BeginReceive(this.channel, this.demuxer.peekTimeout, SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult.onReceiveComplete, this);
				}
				if (asyncResult.CompletedSynchronously)
				{
					this.HandleReceiveComplete(asyncResult);
					return true;
				}
				return false;
			}

			// Token: 0x060079BE RID: 31166 RVA: 0x001C6220 File Offset: 0x001C4420
			private static void OnOpenCompleteStatic(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult peekAsyncResult = (SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult)result.AsyncState;
				bool flag = false;
				Exception exception = null;
				try
				{
					flag = peekAsyncResult.HandleOpenComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
					flag = true;
				}
				if (flag)
				{
					peekAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x060079BF RID: 31167 RVA: 0x001C627C File Offset: 0x001C447C
			private void HandleReceiveComplete(IAsyncResult result)
			{
				this.item = this.demuxer.EndReceive(this.channel, result);
			}

			// Token: 0x060079C0 RID: 31168 RVA: 0x001C6298 File Offset: 0x001C4498
			private static void OnReceiveCompleteStatic(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult peekAsyncResult = (SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					peekAsyncResult.HandleReceiveComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				peekAsyncResult.Complete(false, exception);
			}

			// Token: 0x040045A1 RID: 17825
			private TInnerChannel channel;

			// Token: 0x040045A2 RID: 17826
			private SessionChannelDemuxer<TInnerChannel, TInnerItem> demuxer;

			// Token: 0x040045A3 RID: 17827
			private TInnerItem item;

			// Token: 0x040045A4 RID: 17828
			private static AsyncCallback onOpenComplete = Fx.ThunkCallback(new AsyncCallback(SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult.OnOpenCompleteStatic));

			// Token: 0x040045A5 RID: 17829
			private static AsyncCallback onReceiveComplete = Fx.ThunkCallback(new AsyncCallback(SessionChannelDemuxer<TInnerChannel, TInnerItem>.PeekAsyncResult.OnReceiveCompleteStatic));
		}

		// Token: 0x02000CCD RID: 3277
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x060079C2 RID: 31170 RVA: 0x001C631C File Offset: 0x001C451C
			public OpenAsyncResult(SessionChannelDemuxer<TInnerChannel, TInnerItem> channelDemuxer, ChannelDemuxerFilter filter, IChannelListener listener, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.channelDemuxer = channelDemuxer;
				this.filter = filter;
				this.listener = listener;
				this.timeoutHelper = new TimeoutHelper(timeout);
				if (!this.channelDemuxer.openSemaphore.EnterAsync(this.timeoutHelper.RemainingTime(), SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.waitOverCallback, this))
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

			// Token: 0x060079C3 RID: 31171 RVA: 0x001C63B4 File Offset: 0x001C45B4
			private static void WaitOverCallback(object state, Exception asyncException)
			{
				SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult openAsyncResult = (SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult)state;
				bool flag = false;
				Exception ex = asyncException;
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

			// Token: 0x060079C4 RID: 31172 RVA: 0x001C6410 File Offset: 0x001C4610
			private bool OnWaitOver()
			{
				this.startAccepting = this.channelDemuxer.ShouldStartAccepting(this.filter, this.listener);
				if (!this.startAccepting)
				{
					this.channelDemuxer.ThrowPendingOpenExceptionIfAny();
					return true;
				}
				return this.OnStartAccepting();
			}

			// Token: 0x060079C5 RID: 31173 RVA: 0x001C644C File Offset: 0x001C464C
			private void OnEndInnerListenerOpen(IAsyncResult result)
			{
				this.channelDemuxer.innerListener.EndOpen(result);
				this.channelDemuxer.StartAccepting(true);
				object thisLock = this.channelDemuxer.ThisLock;
				lock (thisLock)
				{
					if (this.channelDemuxer.abortOngoingOpen)
					{
						this.channelDemuxer.innerListener.Abort();
					}
				}
			}

			// Token: 0x060079C6 RID: 31174 RVA: 0x001C64C8 File Offset: 0x001C46C8
			private bool OnStartAccepting()
			{
				bool result;
				try
				{
					IAsyncResult asyncResult = this.channelDemuxer.innerListener.BeginOpen(this.timeoutHelper.RemainingTime(), SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.openListenerCallback, this);
					if (!asyncResult.CompletedSynchronously)
					{
						result = false;
					}
					else
					{
						this.OnEndInnerListenerOpen(asyncResult);
						result = true;
					}
				}
				catch (Exception pendingExceptionOnOpen)
				{
					this.channelDemuxer.pendingExceptionOnOpen = pendingExceptionOnOpen;
					throw;
				}
				return result;
			}

			// Token: 0x060079C7 RID: 31175 RVA: 0x001C6530 File Offset: 0x001C4730
			private static void OpenListenerCallback(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult openAsyncResult = (SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult)result.AsyncState;
				Exception ex = null;
				try
				{
					openAsyncResult.OnEndInnerListenerOpen(result);
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
					openAsyncResult.channelDemuxer.pendingExceptionOnOpen = ex;
				}
				openAsyncResult.Cleanup();
				openAsyncResult.Complete(false, ex);
			}

			// Token: 0x060079C8 RID: 31176 RVA: 0x001C659C File Offset: 0x001C479C
			private void Cleanup()
			{
				this.channelDemuxer.openSemaphore.Exit();
			}

			// Token: 0x060079C9 RID: 31177 RVA: 0x001C65AF File Offset: 0x001C47AF
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult>(result);
			}

			// Token: 0x040045A6 RID: 17830
			private static FastAsyncCallback waitOverCallback = new FastAsyncCallback(SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.WaitOverCallback);

			// Token: 0x040045A7 RID: 17831
			private static AsyncCallback openListenerCallback = Fx.ThunkCallback(new AsyncCallback(SessionChannelDemuxer<TInnerChannel, TInnerItem>.OpenAsyncResult.OpenListenerCallback));

			// Token: 0x040045A8 RID: 17832
			private SessionChannelDemuxer<TInnerChannel, TInnerItem> channelDemuxer;

			// Token: 0x040045A9 RID: 17833
			private ChannelDemuxerFilter filter;

			// Token: 0x040045AA RID: 17834
			private IChannelListener listener;

			// Token: 0x040045AB RID: 17835
			private TimeoutHelper timeoutHelper;

			// Token: 0x040045AC RID: 17836
			private bool startAccepting;
		}
	}
}
