using System;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000956 RID: 2390
	internal sealed class ReliableRequestSessionChannel : RequestChannel, IRequestSessionChannel, IRequestChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
		// Token: 0x06005C4B RID: 23627 RVA: 0x00153CA8 File Offset: 0x00151EA8
		public ReliableRequestSessionChannel(ChannelManagerBase factory, IReliableFactorySettings settings, IClientReliableChannelBinder binder, FaultHelper faultHelper, LateBoundChannelParameterCollection channelParameters, UniqueId inputID) : base(factory, binder.RemoteAddress, binder.Via, true)
		{
			this.settings = settings;
			this.binder = binder;
			this.session = new ClientReliableSession(this, settings, binder, faultHelper, inputID);
			this.session.PollingCallback = new ClientReliableSession.PollingHandler(this.PollingCallback);
			this.session.UnblockChannelCloseCallback = new ChannelReliableSession.UnblockChannelCloseHandler(this.UnblockClose);
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.shutdownHandle = new InterruptibleWaitObject(false);
			}
			else
			{
				this.replyAckConsistencyGuard = new Guard(int.MaxValue);
			}
			this.binder.Faulted += this.OnBinderFaulted;
			this.binder.OnException += this.OnBinderException;
			this.channelParameters = channelParameters;
			channelParameters.SetChannel(this);
		}

		// Token: 0x17001620 RID: 5664
		// (get) Token: 0x06005C4C RID: 23628 RVA: 0x00153D90 File Offset: 0x00151F90
		public IOutputSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06005C4D RID: 23629 RVA: 0x00153D98 File Offset: 0x00151F98
		private void AddAcknowledgementHeader(Message message, bool force)
		{
			if (this.ranges.Count == 0)
			{
				return;
			}
			WsrmUtilities.AddAcknowledgementHeader(this.settings.ReliableMessagingVersion, message, this.session.InputID, this.ranges, this.isLastKnown);
		}

		// Token: 0x06005C4E RID: 23630 RVA: 0x00153DD0 File Offset: 0x00151FD0
		private IAsyncResult BeginCloseBinder(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.binder.BeginClose(timeout, MaskingMode.Handled, callback, state);
		}

		// Token: 0x06005C4F RID: 23631 RVA: 0x00153DE1 File Offset: 0x00151FE1
		private IAsyncResult BeginTerminateSequence(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.CreateTerminateRequestor();
			return this.terminateRequestor.BeginRequest(timeout, callback, state);
		}

		// Token: 0x06005C50 RID: 23632 RVA: 0x00153DF8 File Offset: 0x00151FF8
		private void CloseSequence(TimeSpan timeout)
		{
			this.CreateCloseRequestor();
			Message reply = this.closeRequestor.Request(timeout);
			this.ProcessCloseOrTerminateReply(true, reply);
		}

		// Token: 0x06005C51 RID: 23633 RVA: 0x00153E20 File Offset: 0x00152020
		private IAsyncResult BeginCloseSequence(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.CreateCloseRequestor();
			return this.closeRequestor.BeginRequest(timeout, callback, state);
		}

		// Token: 0x06005C52 RID: 23634 RVA: 0x00153E38 File Offset: 0x00152038
		private void EndCloseSequence(IAsyncResult result)
		{
			Message reply = this.closeRequestor.EndRequest(result);
			this.ProcessCloseOrTerminateReply(true, reply);
		}

		// Token: 0x06005C53 RID: 23635 RVA: 0x00153E5C File Offset: 0x0015205C
		private void ConfigureRequestor(ReliableRequestor requestor)
		{
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			requestor.MessageVersion = this.settings.MessageVersion;
			requestor.Binder = this.binder;
			requestor.SetRequestResponsePattern();
			requestor.MessageHeader = new WsrmAcknowledgmentHeader(reliableMessagingVersion, this.session.InputID, this.ranges, true, -1);
		}

		// Token: 0x06005C54 RID: 23636 RVA: 0x00153EB8 File Offset: 0x001520B8
		private Message CreateAckRequestedMessage()
		{
			Message message = WsrmUtilities.CreateAckRequestedMessage(this.settings.MessageVersion, this.settings.ReliableMessagingVersion, this.session.OutputID);
			this.AddAcknowledgementHeader(message, true);
			return message;
		}

		// Token: 0x06005C55 RID: 23637 RVA: 0x00153EF5 File Offset: 0x001520F5
		protected override IAsyncRequest CreateAsyncRequest(Message message, AsyncCallback callback, object state)
		{
			return new ReliableRequestSessionChannel.AsyncRequest(this, callback, state);
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x00153F00 File Offset: 0x00152100
		private void CreateCloseRequestor()
		{
			RequestReliableRequestor requestReliableRequestor = new RequestReliableRequestor();
			this.ConfigureRequestor(requestReliableRequestor);
			requestReliableRequestor.TimeoutString1Index = "TimeoutOnClose";
			requestReliableRequestor.MessageAction = WsrmIndex.GetCloseSequenceActionHeader(this.settings.MessageVersion.Addressing);
			requestReliableRequestor.MessageBody = new CloseSequence(this.session.OutputID, this.connection.Last);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosed();
				this.closeRequestor = requestReliableRequestor;
			}
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x00153F9C File Offset: 0x0015219C
		protected override IRequest CreateRequest(Message message)
		{
			return new ReliableRequestSessionChannel.SyncRequest(this);
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00153FA4 File Offset: 0x001521A4
		private void CreateTerminateRequestor()
		{
			RequestReliableRequestor requestReliableRequestor = new RequestReliableRequestor();
			this.ConfigureRequestor(requestReliableRequestor);
			requestReliableRequestor.MessageAction = WsrmIndex.GetTerminateSequenceActionHeader(this.settings.MessageVersion.Addressing, this.settings.ReliableMessagingVersion);
			requestReliableRequestor.MessageBody = new TerminateSequence(this.settings.ReliableMessagingVersion, this.session.OutputID, this.connection.Last);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosed();
				this.terminateRequestor = requestReliableRequestor;
				this.session.CloseSession();
			}
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x00154058 File Offset: 0x00152258
		private void EndCloseBinder(IAsyncResult result)
		{
			this.binder.EndClose(result);
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x00154068 File Offset: 0x00152268
		private void EndTerminateSequence(IAsyncResult result)
		{
			Message message = this.terminateRequestor.EndRequest(result);
			if (message != null)
			{
				this.ProcessCloseOrTerminateReply(false, message);
			}
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x0015408D File Offset: 0x0015228D
		private Exception GetInvalidAddException()
		{
			if (base.State == CommunicationState.Faulted)
			{
				return base.GetTerminalException();
			}
			return base.CreateClosedException();
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x001540A8 File Offset: 0x001522A8
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IRequestSessionChannel))
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

		// Token: 0x06005C5D RID: 23645 RVA: 0x0015415C File Offset: 0x0015235C
		protected override void OnAbort()
		{
			if (this.connection != null)
			{
				this.connection.Abort(this);
			}
			if (this.shutdownHandle != null)
			{
				this.shutdownHandle.Abort(this);
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
			base.OnAbort();
		}

		// Token: 0x06005C5E RID: 23646 RVA: 0x001541C4 File Offset: 0x001523C4
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(this.connection.BeginClose),
				new OperationWithTimeoutBeginCallback(this.BeginWaitForShutdown),
				flag ? new OperationWithTimeoutBeginCallback(this.BeginCloseSequence) : null,
				new OperationWithTimeoutBeginCallback(this.BeginTerminateSequence),
				new OperationWithTimeoutBeginCallback(this.session.BeginClose),
				new OperationWithTimeoutBeginCallback(this.BeginCloseBinder)
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				new OperationEndCallback(this.connection.EndClose),
				new OperationEndCallback(this.EndWaitForShutdown),
				flag ? new OperationEndCallback(this.EndCloseSequence) : null,
				new OperationEndCallback(this.EndTerminateSequence),
				new OperationEndCallback(this.session.EndClose),
				new OperationEndCallback(this.EndCloseBinder)
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x06005C5F RID: 23647 RVA: 0x001542D2 File Offset: 0x001524D2
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ReliableChannelOpenAsyncResult(this.binder, this.session, timeout, callback, state);
		}

		// Token: 0x06005C60 RID: 23648 RVA: 0x001542E8 File Offset: 0x001524E8
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

		// Token: 0x06005C61 RID: 23649 RVA: 0x00154340 File Offset: 0x00152540
		private void OnBinderFaulted(IReliableChannelBinder sender, Exception exception)
		{
			this.binder.Abort();
			if (base.State == CommunicationState.Opening || base.State == CommunicationState.Opened || base.State == CommunicationState.Closing)
			{
				exception = new CommunicationException(SR.GetString("EarlySecurityFaulted"), exception);
				this.session.OnLocalFault(exception, null, null);
			}
		}

		// Token: 0x06005C62 RID: 23650 RVA: 0x00154394 File Offset: 0x00152594
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.connection.Close(timeoutHelper.RemainingTime());
			this.WaitForShutdown(timeoutHelper.RemainingTime());
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				this.CloseSequence(timeoutHelper.RemainingTime());
			}
			this.TerminateSequence(timeoutHelper.RemainingTime());
			this.session.Close(timeoutHelper.RemainingTime());
			this.binder.Close(timeoutHelper.RemainingTime(), MaskingMode.Handled);
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x00154419 File Offset: 0x00152619
		protected override void OnClosed()
		{
			base.OnClosed();
			this.binder.Faulted -= this.OnBinderFaulted;
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x00154438 File Offset: 0x00152638
		private IAsyncResult OnConnectionBeginSend(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException, AsyncCallback callback, object state)
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
			this.AddAcknowledgementHeader(attemptInfo.Message, false);
			ReliableBinderRequestAsyncResult reliableBinderRequestAsyncResult = new ReliableBinderRequestAsyncResult(callback, state);
			reliableBinderRequestAsyncResult.Binder = this.binder;
			reliableBinderRequestAsyncResult.MessageAttemptInfo = attemptInfo;
			reliableBinderRequestAsyncResult.MaskingMode = (maskUnhandledException ? MaskingMode.Unhandled : MaskingMode.None);
			if (attemptInfo.RetryCount < this.settings.MaxRetryCount)
			{
				reliableBinderRequestAsyncResult.MaskingMode |= MaskingMode.Handled;
				reliableBinderRequestAsyncResult.SaveHandledException = false;
			}
			else
			{
				reliableBinderRequestAsyncResult.SaveHandledException = true;
			}
			reliableBinderRequestAsyncResult.Begin(timeout);
			return reliableBinderRequestAsyncResult;
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x00154528 File Offset: 0x00152728
		private void OnConnectionEndSend(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			Exception ex;
			Message message = ReliableBinderRequestAsyncResult.End(result, out ex);
			ReliableBinderRequestAsyncResult reliableBinderRequestAsyncResult = (ReliableBinderRequestAsyncResult)result;
			if (reliableBinderRequestAsyncResult.MessageAttemptInfo.RetryCount == this.settings.MaxRetryCount)
			{
				this.maxRetryCountException = ex;
			}
			if (message != null)
			{
				this.ProcessReply(message, (ReliableRequestSessionChannel.IReliableRequest)reliableBinderRequestAsyncResult.MessageAttemptInfo.State, reliableBinderRequestAsyncResult.MessageAttemptInfo.GetSequenceNumber());
			}
		}

		// Token: 0x06005C66 RID: 23654 RVA: 0x001545A4 File Offset: 0x001527A4
		private void OnConnectionSend(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException)
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
					this.AddAcknowledgementHeader(attemptInfo.Message, false);
					this.session.OnLocalActivity();
					Message message2 = null;
					MaskingMode maskingMode = maskUnhandledException ? MaskingMode.Unhandled : MaskingMode.None;
					if (attemptInfo.RetryCount < this.settings.MaxRetryCount)
					{
						maskingMode |= MaskingMode.Handled;
						message2 = this.binder.Request(attemptInfo.Message, timeout, maskingMode);
					}
					else
					{
						try
						{
							message2 = this.binder.Request(attemptInfo.Message, timeout, maskingMode);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							if (!this.binder.IsHandleable(ex))
							{
								throw;
							}
							this.maxRetryCountException = ex;
						}
					}
					if (message2 != null)
					{
						this.ProcessReply(message2, (ReliableRequestSessionChannel.IReliableRequest)attemptInfo.State, attemptInfo.GetSequenceNumber());
					}
				}
			}
		}

		// Token: 0x06005C67 RID: 23655 RVA: 0x00154708 File Offset: 0x00152908
		private void OnConnectionSendAckRequested(TimeSpan timeout)
		{
		}

		// Token: 0x06005C68 RID: 23656 RVA: 0x0015470A File Offset: 0x0015290A
		private IAsyncResult OnConnectionBeginSendAckRequested(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005C69 RID: 23657 RVA: 0x00154713 File Offset: 0x00152913
		private void OnConnectionEndSendAckRequested(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06005C6A RID: 23658 RVA: 0x0015471B File Offset: 0x0015291B
		private void OnComponentFaulted(Exception faultException, WsrmFault fault)
		{
			this.session.OnLocalFault(faultException, fault, null);
		}

		// Token: 0x06005C6B RID: 23659 RVA: 0x0015472B File Offset: 0x0015292B
		private void OnComponentException(Exception exception)
		{
			this.session.OnUnknownException(exception);
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x00154739 File Offset: 0x00152939
		protected override void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x00154741 File Offset: 0x00152941
		protected override void OnEndOpen(IAsyncResult result)
		{
			ReliableChannelOpenAsyncResult.End(result);
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x00154749 File Offset: 0x00152949
		protected override void OnFaulted()
		{
			this.session.OnFaulted();
			this.UnblockClose();
			base.OnFaulted();
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x00154764 File Offset: 0x00152964
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
					this.binder.Close(timeoutHelper.RemainingTime());
				}
			}
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x001547CC File Offset: 0x001529CC
		protected override void OnOpened()
		{
			base.OnOpened();
			this.connection = new ReliableOutputConnection(this.session.OutputID, this.settings.MaxTransferWindowSize, this.settings.MessageVersion, this.settings.ReliableMessagingVersion, this.session.InitiationTime, false, base.DefaultSendTimeout);
			ReliableOutputConnection reliableOutputConnection = this.connection;
			reliableOutputConnection.Faulted = (ComponentFaultedHandler)Delegate.Combine(reliableOutputConnection.Faulted, new ComponentFaultedHandler(this.OnComponentFaulted));
			ReliableOutputConnection reliableOutputConnection2 = this.connection;
			reliableOutputConnection2.OnException = (ComponentExceptionHandler)Delegate.Combine(reliableOutputConnection2.OnException, new ComponentExceptionHandler(this.OnComponentException));
			this.connection.BeginSendHandler = new BeginSendHandler(this.OnConnectionBeginSend);
			this.connection.EndSendHandler = new EndSendHandler(this.OnConnectionEndSend);
			this.connection.SendHandler = new SendHandler(this.OnConnectionSend);
			this.connection.BeginSendAckRequestedHandler = new OperationWithTimeoutBeginCallback(this.OnConnectionBeginSendAckRequested);
			this.connection.EndSendAckRequestedHandler = new OperationEndCallback(this.OnConnectionEndSendAckRequested);
			this.connection.SendAckRequestedHandler = new OperationWithTimeoutCallback(this.OnConnectionSendAckRequested);
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x00154900 File Offset: 0x00152B00
		private static void OnPollingComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableRequestSessionChannel reliableRequestSessionChannel = (ReliableRequestSessionChannel)result.AsyncState;
				reliableRequestSessionChannel.EndSendAckRequestedMessage(result);
			}
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x00154928 File Offset: 0x00152B28
		private void PollingCallback()
		{
			IAsyncResult asyncResult = this.BeginSendAckRequestedMessage(base.DefaultSendTimeout, MaskingMode.All, ReliableRequestSessionChannel.onPollingComplete, this);
			if (asyncResult.CompletedSynchronously)
			{
				this.EndSendAckRequestedMessage(asyncResult);
			}
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x00154958 File Offset: 0x00152B58
		private void ProcessCloseOrTerminateReply(bool close, Message reply)
		{
			if (reply == null)
			{
				throw Fx.AssertAndThrow("Argument reply cannot be null.");
			}
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			if (reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
				{
					WsrmMessageInfo wsrmMessageInfo = this.closeRequestor.GetInfo();
					if (wsrmMessageInfo != null)
					{
						return;
					}
					try
					{
						wsrmMessageInfo = WsrmMessageInfo.Get(this.settings.MessageVersion, reliableMessagingVersion, this.binder.Channel, this.binder.GetInnerSession(), reply);
						this.session.ProcessInfo(wsrmMessageInfo, null, true);
						this.session.VerifyDuplexProtocolElements(wsrmMessageInfo, null, true);
						WsrmFault wsrmFault = close ? WsrmUtilities.ValidateCloseSequenceResponse(this.session, this.closeRequestor.MessageId, wsrmMessageInfo, this.connection.Last) : WsrmUtilities.ValidateTerminateSequenceResponse(this.session, this.terminateRequestor.MessageId, wsrmMessageInfo, this.connection.Last);
						if (wsrmFault != null)
						{
							this.session.OnLocalFault(null, wsrmFault, null);
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(wsrmFault.CreateException());
						}
						return;
					}
					finally
					{
						reply.Close();
					}
				}
				throw Fx.AssertAndThrow("Reliable messaging version not supported.");
			}
			if (close)
			{
				throw Fx.AssertAndThrow("Close does not exist in Feb2005.");
			}
			reply.Close();
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x00154A90 File Offset: 0x00152C90
		private void ProcessReply(Message reply, ReliableRequestSessionChannel.IReliableRequest request, long requestSequenceNumber)
		{
			WsrmMessageInfo wsrmMessageInfo = WsrmMessageInfo.Get(this.settings.MessageVersion, this.settings.ReliableMessagingVersion, this.binder.Channel, this.binder.GetInnerSession(), reply);
			if (!this.session.ProcessInfo(wsrmMessageInfo, null))
			{
				return;
			}
			if (!this.session.VerifyDuplexProtocolElements(wsrmMessageInfo, null))
			{
				return;
			}
			bool flag = this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			if (wsrmMessageInfo.WsrmHeaderFault != null)
			{
				wsrmMessageInfo.Message.Close();
				if (!(wsrmMessageInfo.WsrmHeaderFault is UnknownSequenceFault))
				{
					throw Fx.AssertAndThrow("Fault must be UnknownSequence fault.");
				}
				if (this.terminateRequestor == null)
				{
					throw Fx.AssertAndThrow("If we start getting UnknownSequence, terminateRequestor cannot be null.");
				}
				this.terminateRequestor.SetInfo(wsrmMessageInfo);
				return;
			}
			else
			{
				if (wsrmMessageInfo.AcknowledgementInfo == null)
				{
					WsrmFault wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedReplyMissingAcknowledgement"), SR.GetString("ReplyMissingAcknowledgement"));
					wsrmMessageInfo.Message.Close();
					this.session.OnLocalFault(wsrmFault.CreateException(), wsrmFault, null);
					return;
				}
				if (flag && wsrmMessageInfo.TerminateSequenceInfo != null)
				{
					UniqueId sequenceID = (wsrmMessageInfo.TerminateSequenceInfo.Identifier == this.session.OutputID) ? this.session.InputID : this.session.OutputID;
					WsrmFault wsrmFault2 = SequenceTerminatedFault.CreateProtocolFault(sequenceID, SR.GetString("SequenceTerminatedUnsupportedTerminateSequence"), SR.GetString("UnsupportedTerminateSequenceExceptionString"));
					wsrmMessageInfo.Message.Close();
					this.session.OnLocalFault(wsrmFault2.CreateException(), wsrmFault2, null);
					return;
				}
				if (flag && wsrmMessageInfo.AcknowledgementInfo.Final)
				{
					wsrmMessageInfo.Message.Close();
					if (this.closeRequestor == null)
					{
						string @string = SR.GetString("UnsupportedCloseExceptionString");
						string string2 = SR.GetString("SequenceTerminatedUnsupportedClose");
						WsrmFault wsrmFault3 = SequenceTerminatedFault.CreateProtocolFault(this.session.OutputID, string2, @string);
						this.session.OnLocalFault(wsrmFault3.CreateException(), wsrmFault3, null);
						return;
					}
					WsrmFault wsrmFault4 = WsrmUtilities.ValidateFinalAck(this.session, wsrmMessageInfo, this.connection.Last);
					if (wsrmFault4 == null)
					{
						this.closeRequestor.SetInfo(wsrmMessageInfo);
						return;
					}
					this.session.OnLocalFault(wsrmFault4.CreateException(), wsrmFault4, null);
					return;
				}
				else
				{
					int quotaRemaining = -1;
					if (this.settings.FlowControlEnabled)
					{
						quotaRemaining = wsrmMessageInfo.AcknowledgementInfo.BufferRemaining;
					}
					if (wsrmMessageInfo.SequencedMessageInfo != null && !ReliableInputConnection.CanMerge(wsrmMessageInfo.SequencedMessageInfo.SequenceNumber, this.ranges))
					{
						wsrmMessageInfo.Message.Close();
						return;
					}
					bool flag2 = this.replyAckConsistencyGuard != null && this.replyAckConsistencyGuard.Enter();
					try
					{
						this.connection.ProcessTransferred(requestSequenceNumber, wsrmMessageInfo.AcknowledgementInfo.Ranges, quotaRemaining);
						this.session.OnRemoteActivity(this.connection.Strategy.QuotaRemaining == 0);
						if (wsrmMessageInfo.SequencedMessageInfo != null)
						{
							object thisLock = base.ThisLock;
							lock (thisLock)
							{
								this.ranges = this.ranges.MergeWith(wsrmMessageInfo.SequencedMessageInfo.SequenceNumber);
							}
						}
					}
					finally
					{
						if (flag2)
						{
							this.replyAckConsistencyGuard.Exit();
						}
					}
					if (request != null)
					{
						if (WsrmUtilities.IsWsrmAction(this.settings.ReliableMessagingVersion, wsrmMessageInfo.Action))
						{
							wsrmMessageInfo.Message.Close();
							request.Set(null);
						}
						else
						{
							request.Set(wsrmMessageInfo.Message);
						}
					}
					if (this.shutdownHandle != null && this.connection.CheckForTermination())
					{
						this.shutdownHandle.Set();
					}
					if (request != null)
					{
						request.Complete();
					}
					return;
				}
			}
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x00154E38 File Offset: 0x00153038
		private IAsyncResult BeginSendAckRequestedMessage(TimeSpan timeout, MaskingMode maskingMode, AsyncCallback callback, object state)
		{
			this.session.OnLocalActivity();
			ReliableBinderRequestAsyncResult reliableBinderRequestAsyncResult = new ReliableBinderRequestAsyncResult(callback, state);
			reliableBinderRequestAsyncResult.Binder = this.binder;
			reliableBinderRequestAsyncResult.MaskingMode = maskingMode;
			reliableBinderRequestAsyncResult.Message = this.CreateAckRequestedMessage();
			reliableBinderRequestAsyncResult.Begin(timeout);
			return reliableBinderRequestAsyncResult;
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x00154E80 File Offset: 0x00153080
		private void EndSendAckRequestedMessage(IAsyncResult result)
		{
			Message message = ReliableBinderRequestAsyncResult.End(result);
			if (message != null)
			{
				this.ProcessReply(message, null, 0L);
			}
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x00154EA4 File Offset: 0x001530A4
		private void TerminateSequence(TimeSpan timeout)
		{
			this.CreateTerminateRequestor();
			Message message = this.terminateRequestor.Request(timeout);
			if (message != null)
			{
				this.ProcessCloseOrTerminateReply(false, message);
			}
		}

		// Token: 0x06005C78 RID: 23672 RVA: 0x00154ED0 File Offset: 0x001530D0
		private void UnblockClose()
		{
			base.FaultPendingRequests();
			if (this.connection != null)
			{
				this.connection.Fault(this);
			}
			if (this.shutdownHandle != null)
			{
				this.shutdownHandle.Fault(this);
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

		// Token: 0x06005C79 RID: 23673 RVA: 0x00154F30 File Offset: 0x00153130
		private void WaitForShutdown(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.shutdownHandle.Wait(timeoutHelper.RemainingTime());
				return;
			}
			this.isLastKnown = true;
			this.replyAckConsistencyGuard.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x00154F84 File Offset: 0x00153184
		private IAsyncResult BeginWaitForShutdown(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				return this.shutdownHandle.BeginWait(timeout, callback, state);
			}
			this.isLastKnown = true;
			return this.replyAckConsistencyGuard.BeginClose(timeout, callback, state);
		}

		// Token: 0x06005C7B RID: 23675 RVA: 0x00154FBC File Offset: 0x001531BC
		private void EndWaitForShutdown(IAsyncResult result)
		{
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.shutdownHandle.EndWait(result);
				return;
			}
			this.replyAckConsistencyGuard.EndClose(result);
		}

		// Token: 0x0400372E RID: 14126
		private IClientReliableChannelBinder binder;

		// Token: 0x0400372F RID: 14127
		private ChannelParameterCollection channelParameters;

		// Token: 0x04003730 RID: 14128
		private ReliableRequestor closeRequestor;

		// Token: 0x04003731 RID: 14129
		private ReliableOutputConnection connection;

		// Token: 0x04003732 RID: 14130
		private bool isLastKnown;

		// Token: 0x04003733 RID: 14131
		private Exception maxRetryCountException;

		// Token: 0x04003734 RID: 14132
		private static AsyncCallback onPollingComplete = Fx.ThunkCallback(new AsyncCallback(ReliableRequestSessionChannel.OnPollingComplete));

		// Token: 0x04003735 RID: 14133
		private SequenceRangeCollection ranges = SequenceRangeCollection.Empty;

		// Token: 0x04003736 RID: 14134
		private Guard replyAckConsistencyGuard;

		// Token: 0x04003737 RID: 14135
		private ClientReliableSession session;

		// Token: 0x04003738 RID: 14136
		private IReliableFactorySettings settings;

		// Token: 0x04003739 RID: 14137
		private InterruptibleWaitObject shutdownHandle;

		// Token: 0x0400373A RID: 14138
		private ReliableRequestor terminateRequestor;

		// Token: 0x02000DD6 RID: 3542
		private interface IReliableRequest : IRequestBase
		{
			// Token: 0x0600804E RID: 32846
			void Set(Message reply);

			// Token: 0x0600804F RID: 32847
			void Complete();
		}

		// Token: 0x02000DD7 RID: 3543
		private class SyncRequest : ReliableRequestSessionChannel.IReliableRequest, IRequestBase, IRequest
		{
			// Token: 0x06008050 RID: 32848 RVA: 0x001DD563 File Offset: 0x001DB763
			public SyncRequest(ReliableRequestSessionChannel parent)
			{
				this.parent = parent;
			}

			// Token: 0x17001C6E RID: 7278
			// (get) Token: 0x06008051 RID: 32849 RVA: 0x001DD57D File Offset: 0x001DB77D
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x06008052 RID: 32850 RVA: 0x001DD588 File Offset: 0x001DB788
			public void Abort(RequestChannel channel)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.completed)
					{
						this.aborted = true;
						this.completed = true;
						if (this.completedHandle != null)
						{
							this.completedHandle.Set();
						}
					}
				}
			}

			// Token: 0x06008053 RID: 32851 RVA: 0x001DD5EC File Offset: 0x001DB7EC
			public void Fault(RequestChannel channel)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.completed)
					{
						this.faulted = true;
						this.completed = true;
						if (this.completedHandle != null)
						{
							this.completedHandle.Set();
						}
					}
				}
			}

			// Token: 0x06008054 RID: 32852 RVA: 0x001DD650 File Offset: 0x001DB850
			public void Complete()
			{
			}

			// Token: 0x06008055 RID: 32853 RVA: 0x001DD652 File Offset: 0x001DB852
			public void SendRequest(Message message, TimeSpan timeout)
			{
				this.originalTimeout = timeout;
				if (!this.parent.connection.AddMessage(message, timeout, this))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.parent.GetInvalidAddException());
				}
			}

			// Token: 0x06008056 RID: 32854 RVA: 0x001DD688 File Offset: 0x001DB888
			public void Set(Message reply)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.completed)
					{
						this.reply = reply;
						this.completed = true;
						if (this.completedHandle != null)
						{
							this.completedHandle.Set();
						}
						return;
					}
				}
				if (reply != null)
				{
					reply.Close();
				}
			}

			// Token: 0x06008057 RID: 32855 RVA: 0x001DD6F8 File Offset: 0x001DB8F8
			public Message WaitForReply(TimeSpan timeout)
			{
				bool flag = true;
				Message result;
				try
				{
					bool flag2 = false;
					if (!this.completed)
					{
						bool flag3 = false;
						object obj = this.ThisLock;
						lock (obj)
						{
							if (!this.completed)
							{
								flag3 = true;
								this.completedHandle = new ManualResetEvent(false);
							}
						}
						if (flag3)
						{
							flag2 = !TimeoutHelper.WaitOne(this.completedHandle, timeout);
							object obj2 = this.ThisLock;
							lock (obj2)
							{
								if (!this.completed)
								{
									this.completed = true;
								}
								else
								{
									flag2 = false;
								}
							}
							this.completedHandle.Close();
						}
					}
					if (this.aborted)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.parent.CreateClosedException());
					}
					if (this.faulted)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.parent.GetTerminalException());
					}
					if (flag2)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("TimeoutOnRequest", new object[]
						{
							this.originalTimeout
						})));
					}
					flag = false;
					result = this.reply;
				}
				finally
				{
					if (flag)
					{
						WsrmFault fault = SequenceTerminatedFault.CreateCommunicationFault(this.parent.session.InputID, SR.GetString("SequenceTerminatedReliableRequestThrew"), null);
						this.parent.session.OnLocalFault(null, fault, null);
						if (this.completedHandle != null)
						{
							this.completedHandle.Close();
						}
					}
				}
				return result;
			}

			// Token: 0x06008058 RID: 32856 RVA: 0x001DD8B4 File Offset: 0x001DBAB4
			public void OnReleaseRequest()
			{
			}

			// Token: 0x04004950 RID: 18768
			private bool aborted;

			// Token: 0x04004951 RID: 18769
			private bool completed;

			// Token: 0x04004952 RID: 18770
			private ManualResetEvent completedHandle;

			// Token: 0x04004953 RID: 18771
			private bool faulted;

			// Token: 0x04004954 RID: 18772
			private TimeSpan originalTimeout;

			// Token: 0x04004955 RID: 18773
			private Message reply;

			// Token: 0x04004956 RID: 18774
			private ReliableRequestSessionChannel parent;

			// Token: 0x04004957 RID: 18775
			private object thisLock = new object();
		}

		// Token: 0x02000DD8 RID: 3544
		private class AsyncRequest : AsyncResult, ReliableRequestSessionChannel.IReliableRequest, IRequestBase, IAsyncRequest, IAsyncResult
		{
			// Token: 0x06008059 RID: 32857 RVA: 0x001DD8B6 File Offset: 0x001DBAB6
			public AsyncRequest(ReliableRequestSessionChannel parent, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
			}

			// Token: 0x17001C6F RID: 7279
			// (get) Token: 0x0600805A RID: 32858 RVA: 0x001DD8D2 File Offset: 0x001DBAD2
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x0600805B RID: 32859 RVA: 0x001DD8DA File Offset: 0x001DBADA
			public void Abort(RequestChannel channel)
			{
				if (this.ShouldComplete())
				{
					base.Complete(false, this.parent.CreateClosedException());
				}
			}

			// Token: 0x0600805C RID: 32860 RVA: 0x001DD8F6 File Offset: 0x001DBAF6
			public void Fault(RequestChannel channel)
			{
				if (this.ShouldComplete())
				{
					base.Complete(false, this.parent.GetTerminalException());
				}
			}

			// Token: 0x0600805D RID: 32861 RVA: 0x001DD914 File Offset: 0x001DBB14
			private void AddCompleted(IAsyncResult result)
			{
				Exception ex = null;
				try
				{
					if (!this.parent.connection.EndAddMessage(result))
					{
						ex = this.parent.GetInvalidAddException();
					}
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null && this.ShouldComplete())
				{
					base.Complete(result.CompletedSynchronously, ex);
				}
			}

			// Token: 0x0600805E RID: 32862 RVA: 0x001DD97C File Offset: 0x001DBB7C
			public void BeginSendRequest(Message message, TimeSpan timeout)
			{
				this.parent.connection.BeginAddMessage(message, timeout, this, Fx.ThunkCallback(new AsyncCallback(this.AddCompleted)), null);
			}

			// Token: 0x0600805F RID: 32863 RVA: 0x001DD9A4 File Offset: 0x001DBBA4
			public void Complete()
			{
				if (this.ShouldComplete())
				{
					base.Complete(false, null);
				}
			}

			// Token: 0x06008060 RID: 32864 RVA: 0x001DD9B6 File Offset: 0x001DBBB6
			public Message End()
			{
				AsyncResult.End<ReliableRequestSessionChannel.AsyncRequest>(this);
				return this.reply;
			}

			// Token: 0x06008061 RID: 32865 RVA: 0x001DD9C8 File Offset: 0x001DBBC8
			public void Set(Message reply)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.set)
					{
						this.reply = reply;
						this.set = true;
						return;
					}
				}
				if (reply != null)
				{
					reply.Close();
				}
			}

			// Token: 0x06008062 RID: 32866 RVA: 0x001DDA24 File Offset: 0x001DBC24
			private bool ShouldComplete()
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.completed)
					{
						return false;
					}
					this.completed = true;
				}
				return true;
			}

			// Token: 0x06008063 RID: 32867 RVA: 0x001DDA74 File Offset: 0x001DBC74
			public void OnReleaseRequest()
			{
			}

			// Token: 0x04004958 RID: 18776
			private bool completed;

			// Token: 0x04004959 RID: 18777
			private ReliableRequestSessionChannel parent;

			// Token: 0x0400495A RID: 18778
			private Message reply;

			// Token: 0x0400495B RID: 18779
			private bool set;

			// Token: 0x0400495C RID: 18780
			private object thisLock = new object();
		}
	}
}
