using System;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000952 RID: 2386
	internal abstract class ReliableOutputSessionChannel : OutputChannel, IOutputSessionChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
		// Token: 0x06005BC7 RID: 23495 RVA: 0x001507AC File Offset: 0x0014E9AC
		protected ReliableOutputSessionChannel(ChannelManagerBase factory, IReliableFactorySettings settings, IClientReliableChannelBinder binder, FaultHelper faultHelper, LateBoundChannelParameterCollection channelParameters) : base(factory)
		{
			this.settings = settings;
			this.binder = binder;
			this.session = new ClientReliableSession(this, settings, binder, faultHelper, null);
			this.session.PollingCallback = new ClientReliableSession.PollingHandler(this.PollingCallback);
			this.session.UnblockChannelCloseCallback = new ChannelReliableSession.UnblockChannelCloseHandler(this.UnblockClose);
			this.binder.Faulted += this.OnBinderFaulted;
			this.binder.OnException += this.OnBinderException;
			this.channelParameters = channelParameters;
			channelParameters.SetChannel(this);
		}

		// Token: 0x17001610 RID: 5648
		// (get) Token: 0x06005BC8 RID: 23496 RVA: 0x0015084B File Offset: 0x0014EA4B
		protected IReliableChannelBinder Binder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06005BC9 RID: 23497 RVA: 0x00150853 File Offset: 0x0014EA53
		protected ReliableOutputConnection Connection
		{
			get
			{
				return this.connection;
			}
		}

		// Token: 0x17001612 RID: 5650
		// (set) Token: 0x06005BCA RID: 23498 RVA: 0x0015085B File Offset: 0x0014EA5B
		protected Exception MaxRetryCountException
		{
			set
			{
				this.maxRetryCountException = value;
			}
		}

		// Token: 0x17001613 RID: 5651
		// (get) Token: 0x06005BCB RID: 23499 RVA: 0x00150864 File Offset: 0x0014EA64
		protected ChannelReliableSession ReliableSession
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06005BCC RID: 23500 RVA: 0x0015086C File Offset: 0x0014EA6C
		public override EndpointAddress RemoteAddress
		{
			get
			{
				return this.binder.RemoteAddress;
			}
		}

		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06005BCD RID: 23501
		protected abstract bool RequestAcks { get; }

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06005BCE RID: 23502 RVA: 0x00150879 File Offset: 0x0014EA79
		public IOutputSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x06005BCF RID: 23503 RVA: 0x00150881 File Offset: 0x0014EA81
		public override Uri Via
		{
			get
			{
				return this.binder.Via;
			}
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x06005BD0 RID: 23504 RVA: 0x0015088E File Offset: 0x0014EA8E
		protected IReliableFactorySettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x06005BD1 RID: 23505 RVA: 0x00150898 File Offset: 0x0014EA98
		private void CloseSequence(TimeSpan timeout)
		{
			this.CreateCloseRequestor();
			Message reply = this.closeRequestor.Request(timeout);
			this.ProcessCloseOrTerminateReply(true, reply);
		}

		// Token: 0x06005BD2 RID: 23506 RVA: 0x001508C0 File Offset: 0x0014EAC0
		private IAsyncResult BeginCloseSequence(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.CreateCloseRequestor();
			return this.closeRequestor.BeginRequest(timeout, callback, state);
		}

		// Token: 0x06005BD3 RID: 23507 RVA: 0x001508D8 File Offset: 0x0014EAD8
		private void EndCloseSequence(IAsyncResult result)
		{
			Message reply = this.closeRequestor.EndRequest(result);
			this.ProcessCloseOrTerminateReply(true, reply);
		}

		// Token: 0x06005BD4 RID: 23508 RVA: 0x001508FA File Offset: 0x0014EAFA
		private void ConfigureRequestor(ReliableRequestor requestor)
		{
			requestor.MessageVersion = this.settings.MessageVersion;
			requestor.Binder = this.binder;
			requestor.SetRequestResponsePattern();
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x00150920 File Offset: 0x0014EB20
		private void CreateCloseRequestor()
		{
			ReliableRequestor reliableRequestor = this.CreateRequestor();
			this.ConfigureRequestor(reliableRequestor);
			reliableRequestor.TimeoutString1Index = "TimeoutOnClose";
			reliableRequestor.MessageAction = WsrmIndex.GetCloseSequenceActionHeader(this.settings.MessageVersion.Addressing);
			reliableRequestor.MessageBody = new CloseSequence(this.session.OutputID, this.connection.Last);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosed();
				this.closeRequestor = reliableRequestor;
			}
		}

		// Token: 0x06005BD6 RID: 23510
		protected abstract ReliableRequestor CreateRequestor();

		// Token: 0x06005BD7 RID: 23511 RVA: 0x001509BC File Offset: 0x0014EBBC
		private void CreateTerminateRequestor()
		{
			ReliableRequestor reliableRequestor = this.CreateRequestor();
			this.ConfigureRequestor(reliableRequestor);
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			reliableRequestor.MessageAction = WsrmIndex.GetTerminateSequenceActionHeader(this.settings.MessageVersion.Addressing, reliableMessagingVersion);
			reliableRequestor.MessageBody = new TerminateSequence(reliableMessagingVersion, this.session.OutputID, this.connection.Last);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosed();
				this.terminateRequestor = reliableRequestor;
				this.session.CloseSession();
			}
		}

		// Token: 0x06005BD8 RID: 23512 RVA: 0x00150A68 File Offset: 0x0014EC68
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IOutputSessionChannel))
			{
				return (T)((object)this);
			}
			if (typeof(T) == typeof(ChannelParameterCollection))
			{
				return (T)((object)this.channelParameters);
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			T property2 = this.binder.Channel.GetProperty<T>();
			if (property2 == null && typeof(T) == typeof(FaultConverter))
			{
				return (T)((object)FaultConverter.GetDefaultFaultConverter(this.settings.MessageVersion));
			}
			return property2;
		}

		// Token: 0x06005BD9 RID: 23513 RVA: 0x00150B1C File Offset: 0x0014ED1C
		protected override void OnAbort()
		{
			if (this.connection != null)
			{
				this.connection.Abort(this);
			}
			ReliableRequestor reliableRequestor = this.closeRequestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Abort(this);
			}
			reliableRequestor = this.terminateRequestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Abort(this);
			}
			this.session.Abort();
		}

		// Token: 0x06005BDA RID: 23514 RVA: 0x00150B6C File Offset: 0x0014ED6C
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			OperationWithTimeoutBeginCallback[] beginCallbacks = new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(this.connection.BeginClose),
				flag ? new OperationWithTimeoutBeginCallback(this.BeginCloseSequence) : null,
				new OperationWithTimeoutBeginCallback(this.BeginTerminateSequence),
				new OperationWithTimeoutBeginCallback(this.session.BeginClose)
			};
			OperationEndCallback[] endCallbacks = new OperationEndCallback[]
			{
				new OperationEndCallback(this.connection.EndClose),
				flag ? new OperationEndCallback(this.EndCloseSequence) : null,
				new OperationEndCallback(this.EndTerminateSequence),
				new OperationEndCallback(this.session.EndClose)
			};
			return new ReliableChannelCloseAsyncResult(beginCallbacks, endCallbacks, this.binder, timeout, callback, state);
		}

		// Token: 0x06005BDB RID: 23515 RVA: 0x00150C44 File Offset: 0x0014EE44
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReliableChannelOpenAsyncResult(this.binder, this.session, timeout, callback, state);
		}

		// Token: 0x06005BDC RID: 23516 RVA: 0x00150C5A File Offset: 0x0014EE5A
		protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.connection.BeginAddMessage(message, timeout, null, callback, state);
		}

		// Token: 0x06005BDD RID: 23517 RVA: 0x00150C70 File Offset: 0x0014EE70
		private void OnBinderException(IReliableChannelBinder sender, Exception exception)
		{
			if (exception is QuotaExceededException)
			{
				if (base.State == CommunicationState.Opening || base.State == CommunicationState.Opened || base.State == CommunicationState.Closing)
				{
					this.session.OnLocalFault(exception, SequenceTerminatedFault.CreateQuotaExceededFault(this.session.OutputID), null);
					return;
				}
			}
			else
			{
				base.AddPendingException(exception);
			}
		}

		// Token: 0x06005BDE RID: 23518 RVA: 0x00150CC8 File Offset: 0x0014EEC8
		private void OnBinderFaulted(IReliableChannelBinder sender, Exception exception)
		{
			this.binder.Abort();
			if (base.State == CommunicationState.Opening || base.State == CommunicationState.Opened || base.State == CommunicationState.Closing)
			{
				exception = new CommunicationException(SR.GetString("EarlySecurityFaulted"), exception);
				this.session.OnLocalFault(exception, null, null);
			}
		}

		// Token: 0x06005BDF RID: 23519 RVA: 0x00150D1C File Offset: 0x0014EF1C
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.connection.Close(timeoutHelper.RemainingTime());
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				this.CloseSequence(timeoutHelper.RemainingTime());
			}
			this.TerminateSequence(timeoutHelper.RemainingTime());
			this.session.Close(timeoutHelper.RemainingTime());
			this.binder.Close(timeoutHelper.RemainingTime(), MaskingMode.Handled);
		}

		// Token: 0x06005BE0 RID: 23520 RVA: 0x00150D94 File Offset: 0x0014EF94
		protected override void OnClosed()
		{
			base.OnClosed();
			this.binder.Faulted -= this.OnBinderFaulted;
		}

		// Token: 0x06005BE1 RID: 23521
		protected abstract void OnConnectionSend(Message message, TimeSpan timeout, bool saveHandledException, bool maskUnhandledException);

		// Token: 0x06005BE2 RID: 23522
		protected abstract IAsyncResult OnConnectionBeginSend(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException, AsyncCallback callback, object state);

		// Token: 0x06005BE3 RID: 23523
		protected abstract void OnConnectionEndSend(IAsyncResult result);

		// Token: 0x06005BE4 RID: 23524 RVA: 0x00150DB4 File Offset: 0x0014EFB4
		private void OnConnectionSendAckRequestedHandler(TimeSpan timeout)
		{
			this.session.OnLocalActivity();
			using (Message message = WsrmUtilities.CreateAckRequestedMessage(this.settings.MessageVersion, this.settings.ReliableMessagingVersion, this.ReliableSession.OutputID))
			{
				this.OnConnectionSend(message, timeout, false, true);
			}
		}

		// Token: 0x06005BE5 RID: 23525 RVA: 0x00150E1C File Offset: 0x0014F01C
		private IAsyncResult OnConnectionBeginSendAckRequestedHandler(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.session.OnLocalActivity();
			Message message = WsrmUtilities.CreateAckRequestedMessage(this.settings.MessageVersion, this.settings.ReliableMessagingVersion, this.ReliableSession.OutputID);
			return this.OnConnectionBeginSendMessage(message, timeout, callback, state);
		}

		// Token: 0x06005BE6 RID: 23526 RVA: 0x00150E65 File Offset: 0x0014F065
		private void OnConnectionEndSendAckRequestedHandler(IAsyncResult result)
		{
			this.OnConnectionEndSendMessage(result);
		}

		// Token: 0x06005BE7 RID: 23527 RVA: 0x00150E70 File Offset: 0x0014F070
		private void OnConnectionSendHandler(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException)
		{
			using (attemptInfo.Message)
			{
				if (attemptInfo.RetryCount > this.settings.MaxRetryCount)
				{
					if (TD.MaxRetryCyclesExceededIsEnabled())
					{
						TD.MaxRetryCyclesExceeded(SR.GetString("MaximumRetryCountExceeded"));
					}
					this.session.OnLocalFault(new CommunicationException(SR.GetString("MaximumRetryCountExceeded"), this.maxRetryCountException), SequenceTerminatedFault.CreateMaxRetryCountExceededFault(this.session.OutputID), null);
				}
				else
				{
					this.session.OnLocalActivity();
					this.OnConnectionSend(attemptInfo.Message, timeout, attemptInfo.RetryCount == this.settings.MaxRetryCount, maskUnhandledException);
				}
			}
		}

		// Token: 0x06005BE8 RID: 23528 RVA: 0x00150F2C File Offset: 0x0014F12C
		private IAsyncResult OnConnectionBeginSendHandler(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException, AsyncCallback callback, object state)
		{
			if (attemptInfo.RetryCount > this.settings.MaxRetryCount)
			{
				if (TD.MaxRetryCyclesExceededIsEnabled())
				{
					TD.MaxRetryCyclesExceeded(SR.GetString("MaximumRetryCountExceeded"));
				}
				this.session.OnLocalFault(new CommunicationException(SR.GetString("MaximumRetryCountExceeded"), this.maxRetryCountException), SequenceTerminatedFault.CreateMaxRetryCountExceededFault(this.session.OutputID), null);
				return new CompletedAsyncResult(callback, state);
			}
			this.session.OnLocalActivity();
			return this.OnConnectionBeginSend(attemptInfo, timeout, maskUnhandledException, callback, state);
		}

		// Token: 0x06005BE9 RID: 23529 RVA: 0x00150FB6 File Offset: 0x0014F1B6
		private void OnConnectionEndSendHandler(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			this.OnConnectionEndSend(result);
		}

		// Token: 0x06005BEA RID: 23530
		protected abstract void OnConnectionSendMessage(Message message, TimeSpan timeout, MaskingMode maskingMode);

		// Token: 0x06005BEB RID: 23531
		protected abstract IAsyncResult OnConnectionBeginSendMessage(Message message, TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06005BEC RID: 23532
		protected abstract void OnConnectionEndSendMessage(IAsyncResult result);

		// Token: 0x06005BED RID: 23533 RVA: 0x00150FCE File Offset: 0x0014F1CE
		private void OnComponentFaulted(Exception faultException, WsrmFault fault)
		{
			this.session.OnLocalFault(faultException, fault, null);
		}

		// Token: 0x06005BEE RID: 23534 RVA: 0x00150FDE File Offset: 0x0014F1DE
		private void OnComponentException(Exception exception)
		{
			this.ReliableSession.OnUnknownException(exception);
		}

		// Token: 0x06005BEF RID: 23535 RVA: 0x00150FEC File Offset: 0x0014F1EC
		protected override void OnEndClose(IAsyncResult result)
		{
			ReliableChannelCloseAsyncResult.End(result);
		}

		// Token: 0x06005BF0 RID: 23536 RVA: 0x00150FF4 File Offset: 0x0014F1F4
		protected override void OnEndOpen(IAsyncResult result)
		{
			ReliableChannelOpenAsyncResult.End(result);
		}

		// Token: 0x06005BF1 RID: 23537 RVA: 0x00150FFC File Offset: 0x0014F1FC
		protected override void OnEndSend(IAsyncResult result)
		{
			if (!this.connection.EndAddMessage(result))
			{
				this.ThrowInvalidAddException();
			}
		}

		// Token: 0x06005BF2 RID: 23538 RVA: 0x00151012 File Offset: 0x0014F212
		protected override void OnFaulted()
		{
			this.session.OnFaulted();
			this.UnblockClose();
			base.OnFaulted();
		}

		// Token: 0x06005BF3 RID: 23539 RVA: 0x0015102C File Offset: 0x0014F22C
		protected override void OnOpen(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			bool flag = true;
			try
			{
				this.binder.Open(timeoutHelper.RemainingTime());
				this.session.Open(timeoutHelper.RemainingTime());
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.Binder.Close(timeoutHelper.RemainingTime());
				}
			}
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x00151094 File Offset: 0x0014F294
		protected override void OnSend(Message message, TimeSpan timeout)
		{
			if (!this.connection.AddMessage(message, timeout, null))
			{
				this.ThrowInvalidAddException();
			}
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x001510AC File Offset: 0x0014F2AC
		protected override void OnOpened()
		{
			base.OnOpened();
			this.connection = new ReliableOutputConnection(this.session.OutputID, this.Settings.MaxTransferWindowSize, this.Settings.MessageVersion, this.Settings.ReliableMessagingVersion, this.session.InitiationTime, this.RequestAcks, base.DefaultSendTimeout);
			ReliableOutputConnection reliableOutputConnection = this.connection;
			reliableOutputConnection.Faulted = (ComponentFaultedHandler)Delegate.Combine(reliableOutputConnection.Faulted, new ComponentFaultedHandler(this.OnComponentFaulted));
			ReliableOutputConnection reliableOutputConnection2 = this.connection;
			reliableOutputConnection2.OnException = (ComponentExceptionHandler)Delegate.Combine(reliableOutputConnection2.OnException, new ComponentExceptionHandler(this.OnComponentException));
			this.connection.BeginSendHandler = new BeginSendHandler(this.OnConnectionBeginSendHandler);
			this.connection.EndSendHandler = new EndSendHandler(this.OnConnectionEndSendHandler);
			this.connection.SendHandler = new SendHandler(this.OnConnectionSendHandler);
			this.connection.BeginSendAckRequestedHandler = new OperationWithTimeoutBeginCallback(this.OnConnectionBeginSendAckRequestedHandler);
			this.connection.EndSendAckRequestedHandler = new OperationEndCallback(this.OnConnectionEndSendAckRequestedHandler);
			this.connection.SendAckRequestedHandler = new OperationWithTimeoutCallback(this.OnConnectionSendAckRequestedHandler);
		}

		// Token: 0x06005BF6 RID: 23542 RVA: 0x001511E8 File Offset: 0x0014F3E8
		private void PollingCallback()
		{
			using (Message message = WsrmUtilities.CreateAckRequestedMessage(this.Settings.MessageVersion, this.Settings.ReliableMessagingVersion, this.ReliableSession.OutputID))
			{
				this.OnConnectionSendMessage(message, base.DefaultSendTimeout, MaskingMode.All);
			}
		}

		// Token: 0x06005BF7 RID: 23543 RVA: 0x00151248 File Offset: 0x0014F448
		private void ProcessCloseOrTerminateReply(bool close, Message reply)
		{
			if (reply == null)
			{
				throw Fx.AssertAndThrow("Argument reply cannot be null.");
			}
			ReliableRequestor reliableRequestor = close ? this.closeRequestor : this.terminateRequestor;
			WsrmMessageInfo wsrmMessageInfo = reliableRequestor.GetInfo();
			if (wsrmMessageInfo != null)
			{
				return;
			}
			try
			{
				wsrmMessageInfo = WsrmMessageInfo.Get(this.Settings.MessageVersion, this.Settings.ReliableMessagingVersion, this.binder.Channel, this.binder.GetInnerSession(), reply);
				this.ReliableSession.ProcessInfo(wsrmMessageInfo, null, true);
				this.ReliableSession.VerifyDuplexProtocolElements(wsrmMessageInfo, null, true);
				WsrmFault wsrmFault = close ? WsrmUtilities.ValidateCloseSequenceResponse(this.session, reliableRequestor.MessageId, wsrmMessageInfo, this.connection.Last) : WsrmUtilities.ValidateTerminateSequenceResponse(this.session, reliableRequestor.MessageId, wsrmMessageInfo, this.connection.Last);
				if (wsrmFault != null)
				{
					this.ReliableSession.OnLocalFault(null, wsrmFault, null);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(wsrmFault.CreateException());
				}
			}
			finally
			{
				reply.Close();
			}
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x00151350 File Offset: 0x0014F550
		protected void ProcessMessage(Message message)
		{
			bool flag = true;
			WsrmMessageInfo wsrmMessageInfo = WsrmMessageInfo.Get(this.settings.MessageVersion, this.settings.ReliableMessagingVersion, this.binder.Channel, this.binder.GetInnerSession(), message);
			bool flag2 = this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			try
			{
				if (!this.session.ProcessInfo(wsrmMessageInfo, null))
				{
					flag = false;
				}
				else if (!this.ReliableSession.VerifySimplexProtocolElements(wsrmMessageInfo, null))
				{
					flag = false;
				}
				else
				{
					bool flag3 = false;
					if (wsrmMessageInfo.AcknowledgementInfo != null)
					{
						flag3 = (flag2 && wsrmMessageInfo.AcknowledgementInfo.Final);
						int quotaRemaining = -1;
						if (this.settings.FlowControlEnabled)
						{
							quotaRemaining = wsrmMessageInfo.AcknowledgementInfo.BufferRemaining;
						}
						this.connection.ProcessTransferred(wsrmMessageInfo.AcknowledgementInfo.Ranges, quotaRemaining);
					}
					if (flag2)
					{
						WsrmFault wsrmFault = null;
						if (wsrmMessageInfo.TerminateSequenceResponseInfo != null)
						{
							wsrmFault = WsrmUtilities.ValidateTerminateSequenceResponse(this.session, this.terminateRequestor.MessageId, wsrmMessageInfo, this.connection.Last);
							if (wsrmFault == null)
							{
								wsrmFault = this.ProcessRequestorResponse(this.terminateRequestor, "TerminateSequence", wsrmMessageInfo);
							}
						}
						else if (wsrmMessageInfo.CloseSequenceResponseInfo != null)
						{
							wsrmFault = WsrmUtilities.ValidateCloseSequenceResponse(this.session, this.closeRequestor.MessageId, wsrmMessageInfo, this.connection.Last);
							if (wsrmFault == null)
							{
								wsrmFault = this.ProcessRequestorResponse(this.closeRequestor, "CloseSequence", wsrmMessageInfo);
							}
						}
						else if (wsrmMessageInfo.TerminateSequenceInfo != null)
						{
							if (!WsrmUtilities.ValidateWsrmRequest(this.session, wsrmMessageInfo.TerminateSequenceInfo, this.binder, null))
							{
								return;
							}
							WsrmAcknowledgmentInfo acknowledgementInfo = wsrmMessageInfo.AcknowledgementInfo;
							wsrmFault = WsrmUtilities.ValidateFinalAckExists(this.session, acknowledgementInfo);
							if (wsrmFault == null && !this.connection.IsFinalAckConsistent(acknowledgementInfo.Ranges))
							{
								wsrmFault = new InvalidAcknowledgementFault(this.session.OutputID, acknowledgementInfo.Ranges);
							}
							if (wsrmFault == null)
							{
								Message message2 = WsrmUtilities.CreateTerminateResponseMessage(this.settings.MessageVersion, wsrmMessageInfo.TerminateSequenceInfo.MessageId, this.session.OutputID);
								try
								{
									this.OnConnectionSend(message2, base.DefaultSendTimeout, false, true);
								}
								finally
								{
									message2.Close();
								}
								this.session.OnRemoteFault(new ProtocolException(SR.GetString("UnsupportedTerminateSequenceExceptionString")));
								return;
							}
						}
						else if (flag3)
						{
							if (this.closeRequestor == null)
							{
								string @string = SR.GetString("UnsupportedCloseExceptionString");
								string string2 = SR.GetString("SequenceTerminatedUnsupportedClose");
								wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.OutputID, string2, @string);
							}
							else
							{
								wsrmFault = WsrmUtilities.ValidateFinalAck(this.session, wsrmMessageInfo, this.connection.Last);
								if (wsrmFault == null)
								{
									this.closeRequestor.SetInfo(wsrmMessageInfo);
								}
							}
						}
						else if (wsrmMessageInfo.WsrmHeaderFault != null)
						{
							if (!(wsrmMessageInfo.WsrmHeaderFault is UnknownSequenceFault))
							{
								throw Fx.AssertAndThrow("Fault must be UnknownSequence fault.");
							}
							if (this.terminateRequestor == null)
							{
								throw Fx.AssertAndThrow("In wsrm11, if we start getting UnknownSequence, terminateRequestor cannot be null.");
							}
							this.terminateRequestor.SetInfo(wsrmMessageInfo);
						}
						if (wsrmFault != null)
						{
							this.session.OnLocalFault(wsrmFault.CreateException(), wsrmFault, null);
							return;
						}
					}
					this.session.OnRemoteActivity(this.connection.Strategy.QuotaRemaining == 0);
				}
			}
			finally
			{
				if (flag)
				{
					wsrmMessageInfo.Message.Close();
				}
			}
		}

		// Token: 0x06005BF9 RID: 23545
		protected abstract WsrmFault ProcessRequestorResponse(ReliableRequestor requestor, string requestName, WsrmMessageInfo info);

		// Token: 0x06005BFA RID: 23546 RVA: 0x001516C0 File Offset: 0x0014F8C0
		private void TerminateSequence(TimeSpan timeout)
		{
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.session.CloseSession();
				Message message = WsrmUtilities.CreateTerminateMessage(this.settings.MessageVersion, reliableMessagingVersion, this.session.OutputID);
				this.OnConnectionSendMessage(message, timeout, MaskingMode.Handled);
				return;
			}
			if (reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
			{
				throw Fx.AssertAndThrow("Reliable messaging version not supported.");
			}
			this.CreateTerminateRequestor();
			Message message2 = this.terminateRequestor.Request(timeout);
			if (message2 != null)
			{
				this.ProcessCloseOrTerminateReply(false, message2);
				return;
			}
		}

		// Token: 0x06005BFB RID: 23547 RVA: 0x00151748 File Offset: 0x0014F948
		private IAsyncResult BeginTerminateSequence(TimeSpan timeout, AsyncCallback callback, object state)
		{
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.session.CloseSession();
				Message message = WsrmUtilities.CreateTerminateMessage(this.settings.MessageVersion, reliableMessagingVersion, this.session.OutputID);
				return this.OnConnectionBeginSendMessage(message, timeout, callback, state);
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				this.CreateTerminateRequestor();
				return this.terminateRequestor.BeginRequest(timeout, callback, state);
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005BFC RID: 23548 RVA: 0x001517C4 File Offset: 0x0014F9C4
		private void EndTerminateSequence(IAsyncResult result)
		{
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.OnConnectionEndSendMessage(result);
				return;
			}
			Message message = this.terminateRequestor.EndRequest(result);
			if (message != null)
			{
				this.ProcessCloseOrTerminateReply(false, message);
			}
		}

		// Token: 0x06005BFD RID: 23549 RVA: 0x00151803 File Offset: 0x0014FA03
		private void ThrowInvalidAddException()
		{
			if (base.State == CommunicationState.Faulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.GetTerminalException());
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateClosedException());
		}

		// Token: 0x06005BFE RID: 23550 RVA: 0x00151830 File Offset: 0x0014FA30
		private void UnblockClose()
		{
			if (this.connection != null)
			{
				this.connection.Fault(this);
			}
			ReliableRequestor reliableRequestor = this.closeRequestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Fault(this);
			}
			reliableRequestor = this.terminateRequestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Fault(this);
			}
		}

		// Token: 0x04003711 RID: 14097
		private IClientReliableChannelBinder binder;

		// Token: 0x04003712 RID: 14098
		private ChannelParameterCollection channelParameters;

		// Token: 0x04003713 RID: 14099
		private ReliableRequestor closeRequestor;

		// Token: 0x04003714 RID: 14100
		private ReliableOutputConnection connection;

		// Token: 0x04003715 RID: 14101
		private Exception maxRetryCountException;

		// Token: 0x04003716 RID: 14102
		private ClientReliableSession session;

		// Token: 0x04003717 RID: 14103
		private IReliableFactorySettings settings;

		// Token: 0x04003718 RID: 14104
		private ReliableRequestor terminateRequestor;
	}
}
