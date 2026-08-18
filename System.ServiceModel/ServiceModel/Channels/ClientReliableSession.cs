using System;
using System.Runtime;
using System.ServiceModel.Security;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000958 RID: 2392
	internal class ClientReliableSession : ChannelReliableSession, IOutputSession, ISession
	{
		// Token: 0x06005CAA RID: 23722 RVA: 0x00155C00 File Offset: 0x00153E00
		public ClientReliableSession(ChannelBase channel, IReliableFactorySettings factory, IClientReliableChannelBinder binder, FaultHelper faultHelper, UniqueId inputID) : base(channel, factory, binder, faultHelper)
		{
			this.binder = binder;
			base.InputID = inputID;
			this.pollingTimer = new InterruptibleTimer(this.GetPollingInterval(), new WaitCallback(this.OnPollingTimerElapsed), null);
			if (this.binder.Channel is IRequestChannel)
			{
				this.requestor = new RequestReliableRequestor();
			}
			else if (this.binder.Channel is IDuplexChannel)
			{
				this.requestor = new SendReceiveReliableRequestor
				{
					TimeoutIsSafe = !this.ChannelSupportsOneCreateSequenceAttempt()
				};
			}
			MessageVersion messageVersion = base.Settings.MessageVersion;
			ReliableMessagingVersion reliableMessagingVersion = base.Settings.ReliableMessagingVersion;
			this.requestor.MessageVersion = messageVersion;
			this.requestor.Binder = this.binder;
			this.requestor.IsCreateSequence = true;
			this.requestor.TimeoutString1Index = "TimeoutOnOpen";
			this.requestor.MessageAction = WsrmIndex.GetCreateSequenceActionHeader(messageVersion.Addressing, reliableMessagingVersion);
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && this.binder.GetInnerSession() is ISecureConversationSession)
			{
				this.requestor.MessageHeader = new WsrmUsesSequenceSTRHeader();
			}
			this.requestor.MessageBody = new CreateSequence(base.Settings.MessageVersion.Addressing, reliableMessagingVersion, base.Settings.Ordered, this.binder, base.InputID);
			this.requestor.SetRequestResponsePattern();
		}

		// Token: 0x1700162C RID: 5676
		// (set) Token: 0x06005CAB RID: 23723 RVA: 0x00155D67 File Offset: 0x00153F67
		public ClientReliableSession.PollingHandler PollingCallback
		{
			set
			{
				this.pollingHandler = value;
			}
		}

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06005CAC RID: 23724 RVA: 0x00155D70 File Offset: 0x00153F70
		public override UniqueId SequenceID
		{
			get
			{
				return base.OutputID;
			}
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x00155D78 File Offset: 0x00153F78
		public override void Abort()
		{
			ReliableRequestor reliableRequestor = this.requestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Abort(base.Channel);
			}
			this.pollingTimer.Abort();
			base.Abort();
		}

		// Token: 0x06005CAE RID: 23726 RVA: 0x00155DAC File Offset: 0x00153FAC
		public override IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.pollingHandler == null)
			{
				throw Fx.AssertAndThrow("The client reliable channel must set the polling handler prior to opening the client reliable session.");
			}
			return new ClientReliableSession.OpenAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06005CAF RID: 23727 RVA: 0x00155DCC File Offset: 0x00153FCC
		private bool ChannelSupportsOneCreateSequenceAttempt()
		{
			IDuplexSessionChannel duplexSessionChannel = this.binder.Channel as IDuplexSessionChannel;
			return duplexSessionChannel != null && duplexSessionChannel.Session is ISecuritySession && !(duplexSessionChannel.Session is ISecureConversationSession);
		}

		// Token: 0x06005CB0 RID: 23728 RVA: 0x00155E0F File Offset: 0x0015400F
		public override void Close(TimeSpan timeout)
		{
			base.Close(timeout);
			this.pollingTimer.Abort();
		}

		// Token: 0x06005CB1 RID: 23729 RVA: 0x00155E23 File Offset: 0x00154023
		public override void EndClose(IAsyncResult result)
		{
			base.EndClose(result);
			this.pollingTimer.Abort();
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x00155E37 File Offset: 0x00154037
		public override void EndOpen(IAsyncResult result)
		{
			ClientReliableSession.OpenAsyncResult.End(result);
			this.requestor = null;
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x00155E46 File Offset: 0x00154046
		protected override void FaultCore()
		{
			this.pollingTimer.Abort();
			base.FaultCore();
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x00155E5C File Offset: 0x0015405C
		private TimeSpan GetPollingInterval()
		{
			switch (this.pollingMode)
			{
			case ClientReliableSession.PollingMode.Idle:
				return Ticks.ToTimeSpan(Ticks.FromTimeSpan(base.Settings.InactivityTimeout) / 2L);
			case ClientReliableSession.PollingMode.KeepAlive:
				return WsrmUtilities.CalculateKeepAliveInterval(base.Settings.InactivityTimeout, base.Settings.MaxRetryCount);
			case ClientReliableSession.PollingMode.FastPolling:
			{
				TimeSpan timeSpan = WsrmUtilities.CalculateKeepAliveInterval(base.Settings.InactivityTimeout, base.Settings.MaxRetryCount);
				TimeSpan timeSpan2 = Ticks.ToTimeSpan(Ticks.FromTimeSpan(this.binder.DefaultSendTimeout) / 2L);
				if (timeSpan2 < timeSpan)
				{
					return timeSpan2;
				}
				return timeSpan;
			}
			case ClientReliableSession.PollingMode.NotPolling:
				return TimeSpan.MaxValue;
			default:
				throw Fx.AssertAndThrow("Unknown polling mode.");
			}
		}

		// Token: 0x06005CB5 RID: 23733 RVA: 0x00155F10 File Offset: 0x00154110
		public override void OnFaulted()
		{
			base.OnFaulted();
			ReliableRequestor reliableRequestor = this.requestor;
			if (reliableRequestor != null)
			{
				this.requestor.Fault(base.Channel);
			}
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x00155F40 File Offset: 0x00154140
		private void OnPollingTimerElapsed(object state)
		{
			if (base.Guard.Enter())
			{
				try
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.pollingMode == ClientReliableSession.PollingMode.NotPolling)
						{
							return;
						}
						if (this.pollingMode == ClientReliableSession.PollingMode.Idle)
						{
							this.pollingMode = ClientReliableSession.PollingMode.KeepAlive;
						}
					}
					this.pollingHandler();
					this.pollingTimer.Set(this.GetPollingInterval());
				}
				finally
				{
					base.Guard.Exit();
				}
			}
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x00155FD8 File Offset: 0x001541D8
		public override void OnLocalActivity()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.pollingMode != ClientReliableSession.PollingMode.NotPolling)
				{
					this.pollingTimer.Set(this.GetPollingInterval());
				}
			}
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x00156030 File Offset: 0x00154230
		public override void Open(TimeSpan timeout)
		{
			if (this.pollingHandler == null)
			{
				throw Fx.AssertAndThrow("The client reliable channel must set the polling handler prior to opening the client reliable session.");
			}
			DateTime utcNow = DateTime.UtcNow;
			Message response = this.requestor.Request(timeout);
			this.ProcessCreateSequenceResponse(response, utcNow);
			this.requestor = null;
		}

		// Token: 0x06005CB9 RID: 23737 RVA: 0x00156074 File Offset: 0x00154274
		public override void OnRemoteActivity(bool fastPolling)
		{
			base.OnRemoteActivity(fastPolling);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.pollingMode != ClientReliableSession.PollingMode.NotPolling)
				{
					if (fastPolling)
					{
						this.pollingMode = ClientReliableSession.PollingMode.FastPolling;
					}
					else
					{
						this.pollingMode = ClientReliableSession.PollingMode.Idle;
					}
					this.pollingTimer.Set(this.GetPollingInterval());
				}
			}
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x001560E4 File Offset: 0x001542E4
		private void ProcessCreateSequenceResponse(Message response, DateTime start)
		{
			CreateSequenceResponseInfo createSequenceResponseInfo = null;
			try
			{
				if (response.IsFault)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmUtilities.CreateCSFaultException(base.Settings.MessageVersion, base.Settings.ReliableMessagingVersion, response, this.binder.Channel));
				}
				WsrmMessageInfo wsrmMessageInfo = WsrmMessageInfo.Get(base.Settings.MessageVersion, base.Settings.ReliableMessagingVersion, this.binder.Channel, this.binder.GetInnerSession(), response, true);
				if (wsrmMessageInfo.ParsingException != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(SR.GetString("UnparsableCSResponse"), wsrmMessageInfo.ParsingException));
				}
				base.ProcessInfo(wsrmMessageInfo, null, true);
				createSequenceResponseInfo = wsrmMessageInfo.CreateSequenceResponseInfo;
				string text = null;
				string text2 = null;
				if (createSequenceResponseInfo == null)
				{
					text = SR.GetString("InvalidWsrmResponseChannelNotOpened", new object[]
					{
						"CreateSequence",
						wsrmMessageInfo.Action,
						WsrmIndex.GetCreateSequenceResponseActionString(base.Settings.ReliableMessagingVersion)
					});
				}
				else if (!object.Equals(createSequenceResponseInfo.RelatesTo, this.requestor.MessageId))
				{
					text = SR.GetString("WsrmMessageWithWrongRelatesToExceptionString", new object[]
					{
						"CreateSequence"
					});
					text2 = SR.GetString("WsrmMessageWithWrongRelatesToFaultString", new object[]
					{
						"CreateSequence"
					});
				}
				else if (createSequenceResponseInfo.AcceptAcksTo == null && base.InputID != null)
				{
					if (base.Settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
					{
						text = SR.GetString("CSResponseWithoutOffer");
						text2 = SR.GetString("CSResponseWithoutOfferReason");
					}
					else
					{
						if (base.Settings.ReliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
						{
							throw Fx.AssertAndThrow("Reliable messaging version not supported.");
						}
						text = SR.GetString("CSResponseOfferRejected");
						text2 = SR.GetString("CSResponseOfferRejectedReason");
					}
				}
				else if (createSequenceResponseInfo.AcceptAcksTo != null && base.InputID == null)
				{
					text = SR.GetString("CSResponseWithOffer");
					text2 = SR.GetString("CSResponseWithOfferReason");
				}
				else if (createSequenceResponseInfo.AcceptAcksTo != null && createSequenceResponseInfo.AcceptAcksTo.Uri != this.binder.RemoteAddress.Uri)
				{
					text = SR.GetString("AcksToMustBeSameAsRemoteAddress");
					text2 = SR.GetString("AcksToMustBeSameAsRemoteAddressReason");
				}
				if (text2 != null && createSequenceResponseInfo != null)
				{
					UniqueId identifier = createSequenceResponseInfo.Identifier;
					WsrmFault fault = SequenceTerminatedFault.CreateProtocolFault(identifier, text2, null);
					base.OnLocalFault(null, fault, null);
				}
				if (text != null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ProtocolException(text));
				}
			}
			finally
			{
				if (response != null)
				{
					((IDisposable)response).Dispose();
				}
			}
			base.InitiationTime = DateTime.UtcNow - start;
			base.OutputID = createSequenceResponseInfo.Identifier;
			this.pollingTimer.Set(this.GetPollingInterval());
			base.StartInactivityTimer();
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x001563C4 File Offset: 0x001545C4
		public void ResumePolling(bool fastPolling)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.pollingMode != ClientReliableSession.PollingMode.NotPolling)
				{
					throw Fx.AssertAndThrow("Can't resume polling if pollingMode != PollingMode.NotPolling");
				}
				if (fastPolling)
				{
					this.pollingMode = ClientReliableSession.PollingMode.FastPolling;
				}
				else if (this.oldPollingMode == ClientReliableSession.PollingMode.FastPolling)
				{
					this.pollingMode = ClientReliableSession.PollingMode.Idle;
				}
				else
				{
					this.pollingMode = this.oldPollingMode;
				}
				base.Guard.Exit();
				this.pollingTimer.Set(this.GetPollingInterval());
			}
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x00156458 File Offset: 0x00154658
		public bool StopPolling()
		{
			object thisLock = base.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (this.pollingMode == ClientReliableSession.PollingMode.NotPolling)
				{
					result = false;
				}
				else
				{
					this.oldPollingMode = this.pollingMode;
					this.pollingMode = ClientReliableSession.PollingMode.NotPolling;
					this.pollingTimer.Cancel();
					result = base.Guard.Enter();
				}
			}
			return result;
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x001564CC File Offset: 0x001546CC
		protected override WsrmFault VerifyDuplexProtocolElements(WsrmMessageInfo info)
		{
			WsrmFault wsrmFault = base.VerifyDuplexProtocolElements(info);
			if (wsrmFault != null)
			{
				return wsrmFault;
			}
			if (info.CreateSequenceInfo != null)
			{
				return SequenceTerminatedFault.CreateProtocolFault(base.OutputID, SR.GetString("SequenceTerminatedUnexpectedCS"), SR.GetString("UnexpectedCS"));
			}
			if (info.CreateSequenceResponseInfo != null && info.CreateSequenceResponseInfo.Identifier != base.OutputID)
			{
				return SequenceTerminatedFault.CreateProtocolFault(base.OutputID, SR.GetString("SequenceTerminatedUnexpectedCSROfferId"), SR.GetString("UnexpectedCSROfferId"));
			}
			return null;
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x00156550 File Offset: 0x00154750
		protected override WsrmFault VerifySimplexProtocolElements(WsrmMessageInfo info)
		{
			if (info.AcknowledgementInfo != null && info.AcknowledgementInfo.SequenceID != base.OutputID)
			{
				return new UnknownSequenceFault(info.AcknowledgementInfo.SequenceID);
			}
			if (info.AckRequestedInfo != null)
			{
				return SequenceTerminatedFault.CreateProtocolFault(base.OutputID, SR.GetString("SequenceTerminatedUnexpectedAckRequested"), SR.GetString("UnexpectedAckRequested"));
			}
			if (info.CreateSequenceInfo != null)
			{
				return SequenceTerminatedFault.CreateProtocolFault(base.OutputID, SR.GetString("SequenceTerminatedUnexpectedCS"), SR.GetString("UnexpectedCS"));
			}
			if (info.SequencedMessageInfo != null)
			{
				return new UnknownSequenceFault(info.SequencedMessageInfo.SequenceID);
			}
			if (info.TerminateSequenceInfo != null)
			{
				if (base.Settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
				{
					return SequenceTerminatedFault.CreateProtocolFault(base.OutputID, SR.GetString("SequenceTerminatedUnexpectedTerminateSequence"), SR.GetString("UnexpectedTerminateSequence"));
				}
				if (info.TerminateSequenceInfo.Identifier == base.OutputID)
				{
					return null;
				}
				return new UnknownSequenceFault(info.TerminateSequenceInfo.Identifier);
			}
			else if (info.TerminateSequenceResponseInfo != null)
			{
				WsrmUtilities.AssertWsrm11(base.Settings.ReliableMessagingVersion);
				if (info.TerminateSequenceResponseInfo.Identifier == base.OutputID)
				{
					return null;
				}
				return new UnknownSequenceFault(info.TerminateSequenceResponseInfo.Identifier);
			}
			else if (info.CloseSequenceInfo != null)
			{
				WsrmUtilities.AssertWsrm11(base.Settings.ReliableMessagingVersion);
				if (info.CloseSequenceInfo.Identifier == base.OutputID)
				{
					return SequenceTerminatedFault.CreateProtocolFault(base.OutputID, SR.GetString("SequenceTerminatedUnsupportedClose"), SR.GetString("UnsupportedCloseExceptionString"));
				}
				return new UnknownSequenceFault(info.CloseSequenceInfo.Identifier);
			}
			else
			{
				if (info.CloseSequenceResponseInfo == null)
				{
					return null;
				}
				WsrmUtilities.AssertWsrm11(base.Settings.ReliableMessagingVersion);
				if (info.CloseSequenceResponseInfo.Identifier == base.OutputID)
				{
					return null;
				}
				return new UnknownSequenceFault(info.CloseSequenceResponseInfo.Identifier);
			}
		}

		// Token: 0x0400374C RID: 14156
		private IClientReliableChannelBinder binder;

		// Token: 0x0400374D RID: 14157
		private ClientReliableSession.PollingMode oldPollingMode;

		// Token: 0x0400374E RID: 14158
		private ClientReliableSession.PollingHandler pollingHandler;

		// Token: 0x0400374F RID: 14159
		private ClientReliableSession.PollingMode pollingMode;

		// Token: 0x04003750 RID: 14160
		private InterruptibleTimer pollingTimer;

		// Token: 0x04003751 RID: 14161
		private ReliableRequestor requestor;

		// Token: 0x02000DDB RID: 3547
		// (Invoke) Token: 0x06008069 RID: 32873
		public delegate void PollingHandler();

		// Token: 0x02000DDC RID: 3548
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x0600806C RID: 32876 RVA: 0x001DDA78 File Offset: 0x001DBC78
			public OpenAsyncResult(ClientReliableSession session, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.session = session;
				this.start = DateTime.UtcNow;
				IAsyncResult asyncResult = this.session.requestor.BeginRequest(timeout, ClientReliableSession.OpenAsyncResult.onRequestComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteRequest(asyncResult);
					base.Complete(true);
				}
			}

			// Token: 0x0600806D RID: 32877 RVA: 0x001DDAD0 File Offset: 0x001DBCD0
			private void CompleteRequest(IAsyncResult result)
			{
				Message response = this.session.requestor.EndRequest(result);
				this.session.ProcessCreateSequenceResponse(response, this.start);
			}

			// Token: 0x0600806E RID: 32878 RVA: 0x001DDB01 File Offset: 0x001DBD01
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ClientReliableSession.OpenAsyncResult>(result);
			}

			// Token: 0x0600806F RID: 32879 RVA: 0x001DDB0C File Offset: 0x001DBD0C
			private static void OnRequestCompleteStatic(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				ClientReliableSession.OpenAsyncResult openAsyncResult = (ClientReliableSession.OpenAsyncResult)result.AsyncState;
				Exception exception = null;
				try
				{
					openAsyncResult.CompleteRequest(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				openAsyncResult.Complete(false, exception);
			}

			// Token: 0x04004962 RID: 18786
			private static AsyncCallback onRequestComplete = Fx.ThunkCallback(new AsyncCallback(ClientReliableSession.OpenAsyncResult.OnRequestCompleteStatic));

			// Token: 0x04004963 RID: 18787
			private ClientReliableSession session;

			// Token: 0x04004964 RID: 18788
			private DateTime start;
		}

		// Token: 0x02000DDD RID: 3549
		private enum PollingMode
		{
			// Token: 0x04004966 RID: 18790
			Idle,
			// Token: 0x04004967 RID: 18791
			KeepAlive,
			// Token: 0x04004968 RID: 18792
			FastPolling,
			// Token: 0x04004969 RID: 18793
			NotPolling
		}
	}
}
