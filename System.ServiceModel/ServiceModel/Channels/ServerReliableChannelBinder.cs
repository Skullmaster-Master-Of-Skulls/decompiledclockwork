using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200095C RID: 2396
	internal abstract class ServerReliableChannelBinder<TChannel> : ReliableChannelBinder<TChannel>, IServerReliableChannelBinder, IReliableChannelBinder where TChannel : class, IChannel
	{
		// Token: 0x06005CDD RID: 23773 RVA: 0x00156E34 File Offset: 0x00155034
		protected ServerReliableChannelBinder(ChannelBuilder builder, EndpointAddress remoteAddress, MessageFilter filter, int priority, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(default(TChannel), maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
		{
			this.listener = builder.BuildChannelListener<TChannel>(filter, priority);
			this.remoteAddress = remoteAddress;
		}

		// Token: 0x06005CDE RID: 23774 RVA: 0x00156E7B File Offset: 0x0015507B
		protected ServerReliableChannelBinder(TChannel channel, EndpointAddress cachedLocalAddress, EndpointAddress remoteAddress, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(channel, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
		{
			this.cachedLocalAddress = cachedLocalAddress;
			this.remoteAddress = remoteAddress;
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06005CDF RID: 23775 RVA: 0x00156EA7 File Offset: 0x001550A7
		protected override bool CanGetChannelForReceive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06005CE0 RID: 23776 RVA: 0x00156EAA File Offset: 0x001550AA
		public override EndpointAddress LocalAddress
		{
			get
			{
				if (this.cachedLocalAddress != null)
				{
					return this.cachedLocalAddress;
				}
				return this.GetInnerChannelLocalAddress();
			}
		}

		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06005CE1 RID: 23777 RVA: 0x00156EC7 File Offset: 0x001550C7
		protected override bool MustCloseChannel
		{
			get
			{
				return this.MustOpenChannel || this.HasSession;
			}
		}

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x00156ED9 File Offset: 0x001550D9
		protected override bool MustOpenChannel
		{
			get
			{
				return this.listener != null;
			}
		}

		// Token: 0x17001638 RID: 5688
		// (get) Token: 0x06005CE3 RID: 23779 RVA: 0x00156EE4 File Offset: 0x001550E4
		public override EndpointAddress RemoteAddress
		{
			get
			{
				return this.remoteAddress;
			}
		}

		// Token: 0x06005CE4 RID: 23780 RVA: 0x00156EEC File Offset: 0x001550EC
		private void AddAddressedProperty(Message message)
		{
			message.Properties.Add(ServerReliableChannelBinder<TChannel>.addressedPropertyName, new object());
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x00156F03 File Offset: 0x00155103
		protected override void AddOutputHeaders(Message message)
		{
			if (this.GetAddressedProperty(message) == null)
			{
				this.RemoteAddress.ApplyTo(message);
				this.AddAddressedProperty(message);
			}
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x00156F24 File Offset: 0x00155124
		public bool AddressResponse(Message request, Message response)
		{
			if (this.GetAddressedProperty(response) != null)
			{
				throw Fx.AssertAndThrow("The binder can't address a response twice");
			}
			try
			{
				RequestReplyCorrelator.PrepareReply(response, request);
			}
			catch (MessageHeaderException exception)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
			}
			bool flag = true;
			try
			{
				flag = RequestReplyCorrelator.AddressReply(response, request);
			}
			catch (MessageHeaderException exception2)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				}
			}
			if (flag)
			{
				this.AddAddressedProperty(response);
			}
			return flag;
		}

		// Token: 0x06005CE7 RID: 23783 RVA: 0x00156FA4 File Offset: 0x001551A4
		protected override IAsyncResult BeginTryGetChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.pendingChannelEvent.BeginTryWait(timeout, callback, state);
		}

		// Token: 0x06005CE8 RID: 23784 RVA: 0x00156FB4 File Offset: 0x001551B4
		public IAsyncResult BeginWaitForRequest(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (base.DefaultMaskingMode != MaskingMode.None)
			{
				throw Fx.AssertAndThrow("This method was implemented only for the case where we do not mask exceptions.");
			}
			if (base.ValidateInputOperation(timeout))
			{
				return new ServerReliableChannelBinder<TChannel>.WaitForRequestAsyncResult(this, timeout, callback, state);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005CE9 RID: 23785 RVA: 0x00156FE4 File Offset: 0x001551E4
		private bool CompleteAcceptChannel(IAsyncResult result)
		{
			TChannel tchannel = this.listener.EndAcceptChannel(result);
			if (tchannel == null)
			{
				return false;
			}
			if (!this.UseNewChannel(tchannel))
			{
				tchannel.Abort();
			}
			return true;
		}

		// Token: 0x06005CEA RID: 23786 RVA: 0x00157024 File Offset: 0x00155224
		public static IServerReliableChannelBinder CreateBinder(ChannelBuilder builder, EndpointAddress remoteAddress, MessageFilter filter, int priority, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout)
		{
			Type typeFromHandle = typeof(TChannel);
			if (typeFromHandle == typeof(IDuplexChannel))
			{
				return new ServerReliableChannelBinder<TChannel>.DuplexServerReliableChannelBinder(builder, remoteAddress, filter, priority, MaskingMode.None, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IDuplexSessionChannel))
			{
				return new ServerReliableChannelBinder<TChannel>.DuplexSessionServerReliableChannelBinder(builder, remoteAddress, filter, priority, MaskingMode.None, faultMode, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IReplyChannel))
			{
				return new ServerReliableChannelBinder<TChannel>.ReplyServerReliableChannelBinder(builder, remoteAddress, filter, priority, MaskingMode.None, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IReplySessionChannel))
			{
				return new ServerReliableChannelBinder<TChannel>.ReplySessionServerReliableChannelBinder(builder, remoteAddress, filter, priority, MaskingMode.None, faultMode, defaultCloseTimeout, defaultSendTimeout);
			}
			throw Fx.AssertAndThrow("ServerReliableChannelBinder supports creation of IDuplexChannel, IDuplexSessionChannel, IReplyChannel, and IReplySessionChannel only.");
		}

		// Token: 0x06005CEB RID: 23787 RVA: 0x001570D0 File Offset: 0x001552D0
		public static IServerReliableChannelBinder CreateBinder(TChannel channel, EndpointAddress cachedLocalAddress, EndpointAddress remoteAddress, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout)
		{
			Type typeFromHandle = typeof(TChannel);
			if (typeFromHandle == typeof(IDuplexChannel))
			{
				return new ServerReliableChannelBinder<TChannel>.DuplexServerReliableChannelBinder((IDuplexChannel)((object)channel), cachedLocalAddress, remoteAddress, MaskingMode.All, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IDuplexSessionChannel))
			{
				return new ServerReliableChannelBinder<TChannel>.DuplexSessionServerReliableChannelBinder((IDuplexSessionChannel)((object)channel), cachedLocalAddress, remoteAddress, MaskingMode.All, faultMode, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IReplyChannel))
			{
				return new ServerReliableChannelBinder<TChannel>.ReplyServerReliableChannelBinder((IReplyChannel)((object)channel), cachedLocalAddress, remoteAddress, MaskingMode.All, defaultCloseTimeout, defaultSendTimeout);
			}
			if (typeFromHandle == typeof(IReplySessionChannel))
			{
				return new ServerReliableChannelBinder<TChannel>.ReplySessionServerReliableChannelBinder((IReplySessionChannel)((object)channel), cachedLocalAddress, remoteAddress, MaskingMode.All, faultMode, defaultCloseTimeout, defaultSendTimeout);
			}
			throw Fx.AssertAndThrow("ServerReliableChannelBinder supports creation of IDuplexChannel, IDuplexSessionChannel, IReplyChannel, and IReplySessionChannel only.");
		}

		// Token: 0x06005CEC RID: 23788 RVA: 0x0015719C File Offset: 0x0015539C
		protected override bool EndTryGetChannel(IAsyncResult result)
		{
			if (!this.pendingChannelEvent.EndTryWait(result))
			{
				return false;
			}
			TChannel tchannel = default(TChannel);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State != CommunicationState.Faulted && base.State != CommunicationState.Closing && base.State != CommunicationState.Closed)
				{
					if (!base.Synchronizer.SetChannel(this.pendingChannel))
					{
						tchannel = this.pendingChannel;
					}
					this.pendingChannel = default(TChannel);
					this.pendingChannelEvent.Reset();
				}
			}
			if (tchannel != null)
			{
				tchannel.Abort();
			}
			return true;
		}

		// Token: 0x06005CED RID: 23789 RVA: 0x00157250 File Offset: 0x00155450
		public bool EndWaitForRequest(IAsyncResult result)
		{
			ServerReliableChannelBinder<TChannel>.WaitForRequestAsyncResult waitForRequestAsyncResult = result as ServerReliableChannelBinder<TChannel>.WaitForRequestAsyncResult;
			if (waitForRequestAsyncResult != null)
			{
				return waitForRequestAsyncResult.End();
			}
			CompletedAsyncResult.End(result);
			return true;
		}

		// Token: 0x06005CEE RID: 23790 RVA: 0x00157278 File Offset: 0x00155478
		private object GetAddressedProperty(Message message)
		{
			object result;
			message.Properties.TryGetValue(ServerReliableChannelBinder<TChannel>.addressedPropertyName, out result);
			return result;
		}

		// Token: 0x06005CEF RID: 23791
		protected abstract EndpointAddress GetInnerChannelLocalAddress();

		// Token: 0x06005CF0 RID: 23792 RVA: 0x00157299 File Offset: 0x00155499
		private bool IsListenerExceptionNullOrHandleable(Exception e)
		{
			return e == null || (this.listener.State != CommunicationState.Faulted && base.IsHandleable(e));
		}

		// Token: 0x06005CF1 RID: 23793 RVA: 0x001572B7 File Offset: 0x001554B7
		protected override void OnAbort()
		{
			if (this.listener != null)
			{
				this.listener.Abort();
			}
		}

		// Token: 0x06005CF2 RID: 23794 RVA: 0x001572CC File Offset: 0x001554CC
		private void OnAcceptChannelComplete(IAsyncResult result)
		{
			Exception ex = null;
			Exception ex2 = null;
			bool flag = false;
			try
			{
				flag = this.CompleteAcceptChannel(result);
			}
			catch (Exception ex3)
			{
				if (Fx.IsFatal(ex3))
				{
					throw;
				}
				if (base.IsHandleable(ex3))
				{
					ex = ex3;
				}
				else
				{
					ex2 = ex3;
				}
			}
			if (flag)
			{
				this.StartAccepting();
				return;
			}
			if (ex2 != null)
			{
				base.Fault(ex2);
				return;
			}
			if (ex != null && this.listener.State == CommunicationState.Opened)
			{
				this.StartAccepting();
				return;
			}
			if (this.listener.State == CommunicationState.Faulted)
			{
				base.Fault(ex);
			}
		}

		// Token: 0x06005CF3 RID: 23795 RVA: 0x00157358 File Offset: 0x00155558
		private static void OnAcceptChannelCompleteStatic(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ServerReliableChannelBinder<TChannel> serverReliableChannelBinder = (ServerReliableChannelBinder<TChannel>)result.AsyncState;
				serverReliableChannelBinder.OnAcceptChannelComplete(result);
			}
		}

		// Token: 0x06005CF4 RID: 23796 RVA: 0x00157380 File Offset: 0x00155580
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.listener != null)
			{
				return this.listener.BeginClose(timeout, callback, state);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005CF5 RID: 23797 RVA: 0x001573A0 File Offset: 0x001555A0
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.listener != null)
			{
				return this.listener.BeginOpen(timeout, callback, state);
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005CF6 RID: 23798
		protected abstract IAsyncResult OnBeginWaitForRequest(TChannel channel, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06005CF7 RID: 23799 RVA: 0x001573C0 File Offset: 0x001555C0
		protected override void OnClose(TimeSpan timeout)
		{
			if (this.listener != null)
			{
				this.listener.Close(timeout);
			}
		}

		// Token: 0x06005CF8 RID: 23800 RVA: 0x001573D8 File Offset: 0x001555D8
		protected override void OnShutdown()
		{
			TChannel tchannel = default(TChannel);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				tchannel = this.pendingChannel;
				this.pendingChannel = default(TChannel);
				this.pendingChannelEvent.Set();
			}
			if (tchannel != null)
			{
				tchannel.Abort();
			}
		}

		// Token: 0x06005CF9 RID: 23801
		protected abstract bool OnWaitForRequest(TChannel channel, TimeSpan timeout);

		// Token: 0x06005CFA RID: 23802 RVA: 0x0015744C File Offset: 0x0015564C
		protected override void OnEndClose(IAsyncResult result)
		{
			if (this.listener != null)
			{
				this.listener.EndClose(result);
				return;
			}
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005CFB RID: 23803 RVA: 0x00157469 File Offset: 0x00155669
		protected override void OnEndOpen(IAsyncResult result)
		{
			if (this.listener != null)
			{
				this.listener.EndOpen(result);
				this.StartAccepting();
				return;
			}
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005CFC RID: 23804
		protected abstract bool OnEndWaitForRequest(TChannel channel, IAsyncResult result);

		// Token: 0x06005CFD RID: 23805 RVA: 0x0015748C File Offset: 0x0015568C
		protected override void OnOpen(TimeSpan timeout)
		{
			if (this.listener != null)
			{
				this.listener.Open(timeout);
				this.StartAccepting();
			}
		}

		// Token: 0x06005CFE RID: 23806 RVA: 0x001574A8 File Offset: 0x001556A8
		private void StartAccepting()
		{
			Exception e = null;
			Exception ex = null;
			while (this.listener.State == CommunicationState.Opened)
			{
				e = null;
				ex = null;
				try
				{
					IAsyncResult asyncResult = this.listener.BeginAcceptChannel(TimeSpan.MaxValue, ServerReliableChannelBinder<TChannel>.onAcceptChannelComplete, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					if (!this.CompleteAcceptChannel(asyncResult))
					{
						break;
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					if (!base.IsHandleable(ex2))
					{
						ex = ex2;
						break;
					}
					e = ex2;
				}
			}
			if (ex != null)
			{
				base.Fault(ex);
				return;
			}
			if (this.listener.State == CommunicationState.Faulted)
			{
				base.Fault(e);
				return;
			}
		}

		// Token: 0x06005CFF RID: 23807 RVA: 0x00157548 File Offset: 0x00155748
		protected override bool TryGetChannel(TimeSpan timeout)
		{
			if (!this.pendingChannelEvent.Wait(timeout))
			{
				return false;
			}
			TChannel tchannel = default(TChannel);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State != CommunicationState.Faulted && base.State != CommunicationState.Closing && base.State != CommunicationState.Closed)
				{
					if (!base.Synchronizer.SetChannel(this.pendingChannel))
					{
						tchannel = this.pendingChannel;
					}
					this.pendingChannel = default(TChannel);
					this.pendingChannelEvent.Reset();
				}
			}
			if (tchannel != null)
			{
				tchannel.Abort();
			}
			return true;
		}

		// Token: 0x06005D00 RID: 23808 RVA: 0x001575FC File Offset: 0x001557FC
		public bool UseNewChannel(IChannel channel)
		{
			TChannel tchannel = default(TChannel);
			TChannel tchannel2 = default(TChannel);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (!base.Synchronizer.TolerateFaults || base.State == CommunicationState.Faulted || base.State == CommunicationState.Closing || base.State == CommunicationState.Closed)
				{
					return false;
				}
				tchannel = this.pendingChannel;
				this.pendingChannel = (TChannel)((object)channel);
				tchannel2 = base.Synchronizer.AbortCurentChannel();
			}
			if (tchannel != null)
			{
				tchannel.Abort();
			}
			this.pendingChannelEvent.Set();
			if (tchannel2 != null)
			{
				tchannel2.Abort();
			}
			return true;
		}

		// Token: 0x06005D01 RID: 23809 RVA: 0x001576C8 File Offset: 0x001558C8
		public bool WaitForRequest(TimeSpan timeout)
		{
			if (base.DefaultMaskingMode != MaskingMode.None)
			{
				throw Fx.AssertAndThrow("This method was implemented only for the case where we do not mask exceptions.");
			}
			if (!base.ValidateInputOperation(timeout))
			{
				return true;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			bool result;
			for (;;)
			{
				bool autoAborted = false;
				try
				{
					TChannel tchannel;
					bool flag = !base.Synchronizer.TryGetChannelForInput(true, timeoutHelper.RemainingTime(), out tchannel);
					if (tchannel == null)
					{
						result = flag;
					}
					else
					{
						try
						{
							result = this.OnWaitForRequest(tchannel, timeoutHelper.RemainingTime());
						}
						finally
						{
							autoAborted = base.Synchronizer.Aborting;
							base.Synchronizer.ReturnChannel();
						}
					}
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (!base.HandleException(ex, base.DefaultMaskingMode, autoAborted))
					{
						throw;
					}
					continue;
				}
				break;
			}
			return result;
		}

		// Token: 0x04003757 RID: 14167
		private static string addressedPropertyName = "MessageAddressedByBinderProperty";

		// Token: 0x04003758 RID: 14168
		private IChannelListener<TChannel> listener;

		// Token: 0x04003759 RID: 14169
		private static AsyncCallback onAcceptChannelComplete = Fx.ThunkCallback(new AsyncCallback(ServerReliableChannelBinder<TChannel>.OnAcceptChannelCompleteStatic));

		// Token: 0x0400375A RID: 14170
		private EndpointAddress cachedLocalAddress;

		// Token: 0x0400375B RID: 14171
		private TChannel pendingChannel;

		// Token: 0x0400375C RID: 14172
		private InterruptibleWaitObject pendingChannelEvent = new InterruptibleWaitObject(false, false);

		// Token: 0x0400375D RID: 14173
		private EndpointAddress remoteAddress;

		// Token: 0x02000DE3 RID: 3555
		private abstract class DuplexServerReliableChannelBinder<TDuplexChannel> : ServerReliableChannelBinder<TDuplexChannel> where TDuplexChannel : class, IDuplexChannel
		{
			// Token: 0x06008088 RID: 32904 RVA: 0x001DDE58 File Offset: 0x001DC058
			protected DuplexServerReliableChannelBinder(ChannelBuilder builder, EndpointAddress remoteAddress, MessageFilter filter, int priority, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(builder, remoteAddress, filter, priority, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x06008089 RID: 32905 RVA: 0x001DDE78 File Offset: 0x001DC078
			protected DuplexServerReliableChannelBinder(TDuplexChannel channel, EndpointAddress cachedLocalAddress, EndpointAddress remoteAddress, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(channel, cachedLocalAddress, remoteAddress, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C76 RID: 7286
			// (get) Token: 0x0600808A RID: 32906 RVA: 0x001DDE8B File Offset: 0x001DC08B
			public override bool CanSendAsynchronously
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600808B RID: 32907 RVA: 0x001DDE90 File Offset: 0x001DC090
			protected override EndpointAddress GetInnerChannelLocalAddress()
			{
				IDuplexChannel duplexChannel = base.Synchronizer.CurrentChannel;
				return (duplexChannel == null) ? null : duplexChannel.LocalAddress;
			}

			// Token: 0x0600808C RID: 32908 RVA: 0x001DDEBC File Offset: 0x001DC0BC
			protected override IAsyncResult OnBeginSend(TDuplexChannel channel, Message message, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginSend(message, timeout, callback, state);
			}

			// Token: 0x0600808D RID: 32909 RVA: 0x001DDECF File Offset: 0x001DC0CF
			protected override IAsyncResult OnBeginTryReceive(TDuplexChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginTryReceive(timeout, callback, state);
			}

			// Token: 0x0600808E RID: 32910 RVA: 0x001DDEE0 File Offset: 0x001DC0E0
			protected override IAsyncResult OnBeginWaitForRequest(TDuplexChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginWaitForMessage(timeout, callback, state);
			}

			// Token: 0x0600808F RID: 32911 RVA: 0x001DDEF1 File Offset: 0x001DC0F1
			protected override void OnEndSend(TDuplexChannel channel, IAsyncResult result)
			{
				channel.EndSend(result);
			}

			// Token: 0x06008090 RID: 32912 RVA: 0x001DDF00 File Offset: 0x001DC100
			protected override bool OnEndTryReceive(TDuplexChannel channel, IAsyncResult result, out RequestContext requestContext)
			{
				Message message;
				bool flag = channel.EndTryReceive(result, out message);
				if (flag)
				{
					this.OnMessageReceived(message);
				}
				requestContext = base.WrapMessage(message);
				return flag;
			}

			// Token: 0x06008091 RID: 32913 RVA: 0x001DDF30 File Offset: 0x001DC130
			protected override bool OnEndWaitForRequest(TDuplexChannel channel, IAsyncResult result)
			{
				return channel.EndWaitForMessage(result);
			}

			// Token: 0x06008092 RID: 32914
			protected abstract void OnMessageReceived(Message message);

			// Token: 0x06008093 RID: 32915 RVA: 0x001DDF3E File Offset: 0x001DC13E
			protected override void OnSend(TDuplexChannel channel, Message message, TimeSpan timeout)
			{
				channel.Send(message, timeout);
			}

			// Token: 0x06008094 RID: 32916 RVA: 0x001DDF50 File Offset: 0x001DC150
			protected override bool OnTryReceive(TDuplexChannel channel, TimeSpan timeout, out RequestContext requestContext)
			{
				Message message;
				bool flag = channel.TryReceive(timeout, out message);
				if (flag)
				{
					this.OnMessageReceived(message);
				}
				requestContext = base.WrapMessage(message);
				return flag;
			}

			// Token: 0x06008095 RID: 32917 RVA: 0x001DDF80 File Offset: 0x001DC180
			protected override bool OnWaitForRequest(TDuplexChannel channel, TimeSpan timeout)
			{
				return channel.WaitForMessage(timeout);
			}
		}

		// Token: 0x02000DE4 RID: 3556
		private sealed class DuplexServerReliableChannelBinder : ServerReliableChannelBinder<TChannel>.DuplexServerReliableChannelBinder<IDuplexChannel>
		{
			// Token: 0x06008096 RID: 32918 RVA: 0x001DDF90 File Offset: 0x001DC190
			public DuplexServerReliableChannelBinder(ChannelBuilder builder, EndpointAddress remoteAddress, MessageFilter filter, int priority, MaskingMode maskingMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(builder, remoteAddress, filter, priority, maskingMode, TolerateFaultsMode.Never, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x06008097 RID: 32919 RVA: 0x001DDFAF File Offset: 0x001DC1AF
			public DuplexServerReliableChannelBinder(IDuplexChannel channel, EndpointAddress cachedLocalAddress, EndpointAddress remoteAddress, MaskingMode maskingMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(channel, cachedLocalAddress, remoteAddress, maskingMode, TolerateFaultsMode.Never, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C77 RID: 7287
			// (get) Token: 0x06008098 RID: 32920 RVA: 0x001DDFC1 File Offset: 0x001DC1C1
			public override bool HasSession
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06008099 RID: 32921 RVA: 0x001DDFC4 File Offset: 0x001DC1C4
			public override ISession GetInnerSession()
			{
				return null;
			}

			// Token: 0x0600809A RID: 32922 RVA: 0x001DDFC7 File Offset: 0x001DC1C7
			protected override bool HasSecuritySession(IDuplexChannel channel)
			{
				return false;
			}

			// Token: 0x0600809B RID: 32923 RVA: 0x001DDFCA File Offset: 0x001DC1CA
			protected override void OnMessageReceived(Message message)
			{
			}
		}

		// Token: 0x02000DE5 RID: 3557
		private sealed class DuplexSessionServerReliableChannelBinder : ServerReliableChannelBinder<TChannel>.DuplexServerReliableChannelBinder<IDuplexSessionChannel>
		{
			// Token: 0x0600809C RID: 32924 RVA: 0x001DDFCC File Offset: 0x001DC1CC
			public DuplexSessionServerReliableChannelBinder(ChannelBuilder builder, EndpointAddress remoteAddress, MessageFilter filter, int priority, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(builder, remoteAddress, filter, priority, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x0600809D RID: 32925 RVA: 0x001DDFEC File Offset: 0x001DC1EC
			public DuplexSessionServerReliableChannelBinder(IDuplexSessionChannel channel, EndpointAddress cachedLocalAddress, EndpointAddress remoteAddress, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(channel, cachedLocalAddress, remoteAddress, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C78 RID: 7288
			// (get) Token: 0x0600809E RID: 32926 RVA: 0x001DDFFF File Offset: 0x001DC1FF
			public override bool HasSession
			{
				get
				{
					return true;
				}
			}

			// Token: 0x0600809F RID: 32927 RVA: 0x001DE002 File Offset: 0x001DC202
			protected override IAsyncResult BeginCloseChannel(IDuplexSessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return ReliableChannelBinderHelper.BeginCloseDuplexSessionChannel(this, channel, timeout, callback, state);
			}

			// Token: 0x060080A0 RID: 32928 RVA: 0x001DE00F File Offset: 0x001DC20F
			protected override void CloseChannel(IDuplexSessionChannel channel, TimeSpan timeout)
			{
				ReliableChannelBinderHelper.CloseDuplexSessionChannel(this, channel, timeout);
			}

			// Token: 0x060080A1 RID: 32929 RVA: 0x001DE019 File Offset: 0x001DC219
			protected override void EndCloseChannel(IDuplexSessionChannel channel, IAsyncResult result)
			{
				ReliableChannelBinderHelper.EndCloseDuplexSessionChannel(channel, result);
			}

			// Token: 0x060080A2 RID: 32930 RVA: 0x001DE022 File Offset: 0x001DC222
			public override ISession GetInnerSession()
			{
				return base.Synchronizer.CurrentChannel.Session;
			}

			// Token: 0x060080A3 RID: 32931 RVA: 0x001DE034 File Offset: 0x001DC234
			protected override bool HasSecuritySession(IDuplexSessionChannel channel)
			{
				return channel.Session is ISecuritySession;
			}

			// Token: 0x060080A4 RID: 32932 RVA: 0x001DE044 File Offset: 0x001DC244
			protected override void OnMessageReceived(Message message)
			{
				if (message == null)
				{
					base.Synchronizer.OnReadEof();
				}
			}
		}

		// Token: 0x02000DE6 RID: 3558
		private abstract class ReplyServerReliableChannelBinder<TReplyChannel> : ServerReliableChannelBinder<TReplyChannel> where TReplyChannel : class, IReplyChannel
		{
			// Token: 0x060080A5 RID: 32933 RVA: 0x001DE054 File Offset: 0x001DC254
			public ReplyServerReliableChannelBinder(ChannelBuilder builder, EndpointAddress remoteAddress, MessageFilter filter, int priority, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(builder, remoteAddress, filter, priority, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x060080A6 RID: 32934 RVA: 0x001DE074 File Offset: 0x001DC274
			public ReplyServerReliableChannelBinder(TReplyChannel channel, EndpointAddress cachedLocalAddress, EndpointAddress remoteAddress, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(channel, cachedLocalAddress, remoteAddress, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C79 RID: 7289
			// (get) Token: 0x060080A7 RID: 32935 RVA: 0x001DE087 File Offset: 0x001DC287
			public override bool CanSendAsynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060080A8 RID: 32936 RVA: 0x001DE08C File Offset: 0x001DC28C
			protected override EndpointAddress GetInnerChannelLocalAddress()
			{
				IReplyChannel replyChannel = base.Synchronizer.CurrentChannel;
				return (replyChannel == null) ? null : replyChannel.LocalAddress;
			}

			// Token: 0x060080A9 RID: 32937 RVA: 0x001DE0B8 File Offset: 0x001DC2B8
			protected override IAsyncResult OnBeginTryReceive(TReplyChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginTryReceiveRequest(timeout, callback, state);
			}

			// Token: 0x060080AA RID: 32938 RVA: 0x001DE0C9 File Offset: 0x001DC2C9
			protected override IAsyncResult OnBeginWaitForRequest(TReplyChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return channel.BeginWaitForRequest(timeout, callback, state);
			}

			// Token: 0x060080AB RID: 32939 RVA: 0x001DE0DC File Offset: 0x001DC2DC
			protected override bool OnEndTryReceive(TReplyChannel channel, IAsyncResult result, out RequestContext requestContext)
			{
				bool flag = channel.EndTryReceiveRequest(result, out requestContext);
				if (flag && requestContext == null)
				{
					this.OnReadNullMessage();
				}
				requestContext = base.WrapRequestContext(requestContext);
				return flag;
			}

			// Token: 0x060080AC RID: 32940 RVA: 0x001DE10F File Offset: 0x001DC30F
			protected override bool OnEndWaitForRequest(TReplyChannel channel, IAsyncResult result)
			{
				return channel.EndWaitForRequest(result);
			}

			// Token: 0x060080AD RID: 32941 RVA: 0x001DE11D File Offset: 0x001DC31D
			protected virtual void OnReadNullMessage()
			{
			}

			// Token: 0x060080AE RID: 32942 RVA: 0x001DE120 File Offset: 0x001DC320
			protected override bool OnTryReceive(TReplyChannel channel, TimeSpan timeout, out RequestContext requestContext)
			{
				bool flag = channel.TryReceiveRequest(timeout, out requestContext);
				if (flag && requestContext == null)
				{
					this.OnReadNullMessage();
				}
				requestContext = base.WrapRequestContext(requestContext);
				return flag;
			}

			// Token: 0x060080AF RID: 32943 RVA: 0x001DE153 File Offset: 0x001DC353
			protected override bool OnWaitForRequest(TReplyChannel channel, TimeSpan timeout)
			{
				return channel.WaitForRequest(timeout);
			}
		}

		// Token: 0x02000DE7 RID: 3559
		private sealed class ReplyServerReliableChannelBinder : ServerReliableChannelBinder<TChannel>.ReplyServerReliableChannelBinder<IReplyChannel>
		{
			// Token: 0x060080B0 RID: 32944 RVA: 0x001DE164 File Offset: 0x001DC364
			public ReplyServerReliableChannelBinder(ChannelBuilder builder, EndpointAddress remoteAddress, MessageFilter filter, int priority, MaskingMode maskingMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(builder, remoteAddress, filter, priority, maskingMode, TolerateFaultsMode.Never, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x060080B1 RID: 32945 RVA: 0x001DE183 File Offset: 0x001DC383
			public ReplyServerReliableChannelBinder(IReplyChannel channel, EndpointAddress cachedLocalAddress, EndpointAddress remoteAddress, MaskingMode maskingMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(channel, cachedLocalAddress, remoteAddress, maskingMode, TolerateFaultsMode.Never, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C7A RID: 7290
			// (get) Token: 0x060080B2 RID: 32946 RVA: 0x001DE195 File Offset: 0x001DC395
			public override bool HasSession
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060080B3 RID: 32947 RVA: 0x001DE198 File Offset: 0x001DC398
			public override ISession GetInnerSession()
			{
				return null;
			}

			// Token: 0x060080B4 RID: 32948 RVA: 0x001DE19B File Offset: 0x001DC39B
			protected override bool HasSecuritySession(IReplyChannel channel)
			{
				return false;
			}
		}

		// Token: 0x02000DE8 RID: 3560
		private sealed class ReplySessionServerReliableChannelBinder : ServerReliableChannelBinder<TChannel>.ReplyServerReliableChannelBinder<IReplySessionChannel>
		{
			// Token: 0x060080B5 RID: 32949 RVA: 0x001DE1A0 File Offset: 0x001DC3A0
			public ReplySessionServerReliableChannelBinder(ChannelBuilder builder, EndpointAddress remoteAddress, MessageFilter filter, int priority, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(builder, remoteAddress, filter, priority, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x060080B6 RID: 32950 RVA: 0x001DE1C0 File Offset: 0x001DC3C0
			public ReplySessionServerReliableChannelBinder(IReplySessionChannel channel, EndpointAddress cachedLocalAddress, EndpointAddress remoteAddress, MaskingMode maskingMode, TolerateFaultsMode faultMode, TimeSpan defaultCloseTimeout, TimeSpan defaultSendTimeout) : base(channel, cachedLocalAddress, remoteAddress, maskingMode, faultMode, defaultCloseTimeout, defaultSendTimeout)
			{
			}

			// Token: 0x17001C7B RID: 7291
			// (get) Token: 0x060080B7 RID: 32951 RVA: 0x001DE1D3 File Offset: 0x001DC3D3
			public override bool HasSession
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060080B8 RID: 32952 RVA: 0x001DE1D6 File Offset: 0x001DC3D6
			protected override IAsyncResult BeginCloseChannel(IReplySessionChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return ReliableChannelBinderHelper.BeginCloseReplySessionChannel(this, channel, timeout, callback, state);
			}

			// Token: 0x060080B9 RID: 32953 RVA: 0x001DE1E3 File Offset: 0x001DC3E3
			protected override void CloseChannel(IReplySessionChannel channel, TimeSpan timeout)
			{
				ReliableChannelBinderHelper.CloseReplySessionChannel(this, channel, timeout);
			}

			// Token: 0x060080BA RID: 32954 RVA: 0x001DE1ED File Offset: 0x001DC3ED
			protected override void EndCloseChannel(IReplySessionChannel channel, IAsyncResult result)
			{
				ReliableChannelBinderHelper.EndCloseReplySessionChannel(channel, result);
			}

			// Token: 0x060080BB RID: 32955 RVA: 0x001DE1F6 File Offset: 0x001DC3F6
			public override ISession GetInnerSession()
			{
				return base.Synchronizer.CurrentChannel.Session;
			}

			// Token: 0x060080BC RID: 32956 RVA: 0x001DE208 File Offset: 0x001DC408
			protected override bool HasSecuritySession(IReplySessionChannel channel)
			{
				return channel.Session is ISecuritySession;
			}

			// Token: 0x060080BD RID: 32957 RVA: 0x001DE218 File Offset: 0x001DC418
			protected override void OnReadNullMessage()
			{
				base.Synchronizer.OnReadEof();
			}
		}

		// Token: 0x02000DE9 RID: 3561
		private sealed class WaitForRequestAsyncResult : ReliableChannelBinder<TChannel>.InputAsyncResult<ServerReliableChannelBinder<TChannel>>
		{
			// Token: 0x060080BE RID: 32958 RVA: 0x001DE225 File Offset: 0x001DC425
			public WaitForRequestAsyncResult(ServerReliableChannelBinder<TChannel> binder, TimeSpan timeout, AsyncCallback callback, object state) : base(binder, true, timeout, binder.DefaultMaskingMode, callback, state)
			{
				if (base.Start())
				{
					base.Complete(true);
				}
			}

			// Token: 0x060080BF RID: 32959 RVA: 0x001DE248 File Offset: 0x001DC448
			protected override IAsyncResult BeginInput(ServerReliableChannelBinder<TChannel> binder, TChannel channel, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return binder.OnBeginWaitForRequest(channel, timeout, callback, state);
			}

			// Token: 0x060080C0 RID: 32960 RVA: 0x001DE256 File Offset: 0x001DC456
			protected override bool EndInput(ServerReliableChannelBinder<TChannel> binder, TChannel channel, IAsyncResult result, out bool complete)
			{
				complete = true;
				return binder.OnEndWaitForRequest(channel, result);
			}
		}
	}
}
