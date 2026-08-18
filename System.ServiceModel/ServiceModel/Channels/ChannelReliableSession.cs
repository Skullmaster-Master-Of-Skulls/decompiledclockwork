using System;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000957 RID: 2391
	internal abstract class ChannelReliableSession : ISession
	{
		// Token: 0x06005C7D RID: 23677 RVA: 0x00155004 File Offset: 0x00153204
		protected ChannelReliableSession(ChannelBase channel, IReliableFactorySettings settings, IReliableChannelBinder binder, FaultHelper faultHelper)
		{
			this.channel = channel;
			this.settings = settings;
			this.binder = binder;
			this.faultHelper = faultHelper;
			this.inactivityTimer = new InterruptibleTimer(this.settings.InactivityTimeout, new WaitCallback(this.OnInactivityElapsed), null);
			this.initiationTime = ReliableMessagingConstants.UnknownInitiationTime;
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06005C7E RID: 23678 RVA: 0x00155084 File Offset: 0x00153284
		protected ChannelBase Channel
		{
			get
			{
				return this.channel;
			}
		}

		// Token: 0x17001622 RID: 5666
		// (get) Token: 0x06005C7F RID: 23679 RVA: 0x0015508C File Offset: 0x0015328C
		protected Guard Guard
		{
			get
			{
				return this.guard;
			}
		}

		// Token: 0x17001623 RID: 5667
		// (get) Token: 0x06005C80 RID: 23680 RVA: 0x00155094 File Offset: 0x00153294
		public string Id
		{
			get
			{
				UniqueId sequenceID = this.SequenceID;
				if (sequenceID == null)
				{
					return null;
				}
				return sequenceID.ToString();
			}
		}

		// Token: 0x17001624 RID: 5668
		// (get) Token: 0x06005C81 RID: 23681 RVA: 0x001550B9 File Offset: 0x001532B9
		// (set) Token: 0x06005C82 RID: 23682 RVA: 0x001550C1 File Offset: 0x001532C1
		public TimeSpan InitiationTime
		{
			get
			{
				return this.initiationTime;
			}
			protected set
			{
				this.initiationTime = value;
			}
		}

		// Token: 0x17001625 RID: 5669
		// (get) Token: 0x06005C83 RID: 23683 RVA: 0x001550CA File Offset: 0x001532CA
		// (set) Token: 0x06005C84 RID: 23684 RVA: 0x001550D2 File Offset: 0x001532D2
		public UniqueId InputID
		{
			get
			{
				return this.inputID;
			}
			protected set
			{
				this.inputID = value;
			}
		}

		// Token: 0x17001626 RID: 5670
		// (get) Token: 0x06005C85 RID: 23685 RVA: 0x001550DB File Offset: 0x001532DB
		protected FaultHelper FaultHelper
		{
			get
			{
				return this.faultHelper;
			}
		}

		// Token: 0x17001627 RID: 5671
		// (get) Token: 0x06005C86 RID: 23686 RVA: 0x001550E3 File Offset: 0x001532E3
		// (set) Token: 0x06005C87 RID: 23687 RVA: 0x001550EB File Offset: 0x001532EB
		public UniqueId OutputID
		{
			get
			{
				return this.outputID;
			}
			protected set
			{
				this.outputID = value;
			}
		}

		// Token: 0x17001628 RID: 5672
		// (get) Token: 0x06005C88 RID: 23688
		public abstract UniqueId SequenceID { get; }

		// Token: 0x17001629 RID: 5673
		// (get) Token: 0x06005C89 RID: 23689 RVA: 0x001550F4 File Offset: 0x001532F4
		public IReliableFactorySettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x1700162A RID: 5674
		// (get) Token: 0x06005C8A RID: 23690 RVA: 0x001550FC File Offset: 0x001532FC
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x1700162B RID: 5675
		// (set) Token: 0x06005C8B RID: 23691 RVA: 0x00155104 File Offset: 0x00153304
		public ChannelReliableSession.UnblockChannelCloseHandler UnblockChannelCloseCallback
		{
			set
			{
				this.unblockChannelCloseCallback = value;
			}
		}

		// Token: 0x06005C8C RID: 23692 RVA: 0x00155110 File Offset: 0x00153310
		public virtual void Abort()
		{
			this.guard.Abort();
			this.inactivityTimer.Abort();
			object obj = this.ThisLock;
			bool flag2;
			lock (obj)
			{
				if (this.faulted == ChannelReliableSession.SessionFaultState.CleanedUp)
				{
					return;
				}
				flag2 = (this.canSendFault && this.faulted != ChannelReliableSession.SessionFaultState.RemotelyFaulted);
				this.faulted = ChannelReliableSession.SessionFaultState.CleanedUp;
			}
			if (flag2 && this.binder.State == CommunicationState.Opened && this.binder.Connected && (this.binder.CanSendAsynchronously || this.replyFaultContext != null))
			{
				if (this.terminatingFault == null)
				{
					UniqueId uniqueId = this.InputID ?? this.OutputID;
					if (uniqueId != null)
					{
						WsrmFault wsrmFault = SequenceTerminatedFault.CreateCommunicationFault(uniqueId, SR.GetString("SequenceTerminatedOnAbort"), null);
						this.terminatingFault = wsrmFault.CreateMessage(this.settings.MessageVersion, this.settings.ReliableMessagingVersion);
					}
				}
				if (this.terminatingFault != null)
				{
					this.AddFinalRanges();
					this.faultHelper.SendFaultAsync(this.binder, this.replyFaultContext, this.terminatingFault);
					return;
				}
			}
			if (this.terminatingFault != null)
			{
				this.terminatingFault.Close();
			}
			if (this.replyFaultContext != null)
			{
				this.replyFaultContext.Abort();
			}
			this.binder.Abort();
		}

		// Token: 0x06005C8D RID: 23693 RVA: 0x00155284 File Offset: 0x00153484
		private void AddFinalRanges()
		{
			if (this.finalRanges != null)
			{
				WsrmUtilities.AddAcknowledgementHeader(this.settings.ReliableMessagingVersion, this.terminatingFault, this.InputID, this.finalRanges, true);
			}
		}

		// Token: 0x06005C8E RID: 23694 RVA: 0x001552B1 File Offset: 0x001534B1
		public virtual IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.guard.BeginClose(timeout, callback, state);
		}

		// Token: 0x06005C8F RID: 23695
		public abstract IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x06005C90 RID: 23696 RVA: 0x001552C1 File Offset: 0x001534C1
		public virtual void Close(TimeSpan timeout)
		{
			this.guard.Close(timeout);
			this.inactivityTimer.Abort();
		}

		// Token: 0x06005C91 RID: 23697 RVA: 0x001552DA File Offset: 0x001534DA
		public void CloseSession()
		{
			this.isSessionClosed = true;
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x001552E3 File Offset: 0x001534E3
		public virtual void EndClose(IAsyncResult result)
		{
			this.guard.EndClose(result);
			this.inactivityTimer.Abort();
		}

		// Token: 0x06005C93 RID: 23699
		public abstract void EndOpen(IAsyncResult result);

		// Token: 0x06005C94 RID: 23700 RVA: 0x001552FC File Offset: 0x001534FC
		protected virtual void FaultCore()
		{
			if (TD.ReliableSessionChannelFaultedIsEnabled())
			{
				TD.ReliableSessionChannelFaulted(this.Id);
			}
			this.inactivityTimer.Abort();
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x0015531C File Offset: 0x0015351C
		public void OnLocalFault(Exception e, WsrmFault fault, RequestContext context)
		{
			Message faultMessage = (fault == null) ? null : fault.CreateMessage(this.settings.MessageVersion, this.settings.ReliableMessagingVersion);
			this.OnLocalFault(e, faultMessage, context);
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x00155358 File Offset: 0x00153558
		public void OnLocalFault(Exception e, Message faultMessage, RequestContext context)
		{
			if (this.channel.Aborted || this.channel.State == CommunicationState.Faulted || this.channel.State == CommunicationState.Closed)
			{
				if (faultMessage != null)
				{
					faultMessage.Close();
				}
				if (context != null)
				{
					context.Abort();
				}
				return;
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.faulted != ChannelReliableSession.SessionFaultState.NotFaulted)
				{
					return;
				}
				this.faulted = ChannelReliableSession.SessionFaultState.LocallyFaulted;
				this.terminatingFault = faultMessage;
				this.replyFaultContext = context;
			}
			this.FaultCore();
			this.channel.Fault(e);
			this.UnblockChannelIfNecessary();
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x00155408 File Offset: 0x00153608
		public void OnRemoteFault(WsrmFault fault)
		{
			this.OnRemoteFault(WsrmFault.CreateException(fault));
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x00155418 File Offset: 0x00153618
		public void OnRemoteFault(Exception e)
		{
			if (this.channel.Aborted || this.channel.State == CommunicationState.Faulted || this.channel.State == CommunicationState.Closed)
			{
				return;
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.faulted != ChannelReliableSession.SessionFaultState.NotFaulted)
				{
					return;
				}
				this.faulted = ChannelReliableSession.SessionFaultState.RemotelyFaulted;
			}
			this.FaultCore();
			this.channel.Fault(e);
			this.UnblockChannelIfNecessary();
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x001554A8 File Offset: 0x001536A8
		public virtual void OnFaulted()
		{
			this.FaultCore();
			object obj = this.ThisLock;
			bool flag2;
			lock (obj)
			{
				if (this.faulted == ChannelReliableSession.SessionFaultState.NotFaulted)
				{
					return;
				}
				if (this.faulted == ChannelReliableSession.SessionFaultState.CleanedUp)
				{
					return;
				}
				flag2 = (this.canSendFault && this.faulted != ChannelReliableSession.SessionFaultState.RemotelyFaulted);
				this.faulted = ChannelReliableSession.SessionFaultState.CleanedUp;
			}
			if (flag2 && this.binder.State == CommunicationState.Opened && this.binder.Connected && (this.binder.CanSendAsynchronously || this.replyFaultContext != null) && this.terminatingFault != null)
			{
				this.AddFinalRanges();
				this.faultHelper.SendFaultAsync(this.binder, this.replyFaultContext, this.terminatingFault);
				return;
			}
			if (this.terminatingFault != null)
			{
				this.terminatingFault.Close();
			}
			if (this.replyFaultContext != null)
			{
				this.replyFaultContext.Abort();
			}
			this.binder.Abort();
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x001555B4 File Offset: 0x001537B4
		private void OnInactivityElapsed(object state)
		{
			string @string = SR.GetString("SequenceTerminatedInactivityTimeoutExceeded", new object[]
			{
				this.settings.InactivityTimeout
			});
			if (TD.InactivityTimeoutIsEnabled())
			{
				TD.InactivityTimeout(@string);
			}
			WsrmFault wsrmFault;
			Exception e;
			if (this.SequenceID != null)
			{
				string string2 = SR.GetString("SequenceTerminatedInactivityTimeoutExceeded", new object[]
				{
					this.settings.InactivityTimeout
				});
				wsrmFault = SequenceTerminatedFault.CreateCommunicationFault(this.SequenceID, string2, @string);
				e = wsrmFault.CreateException();
			}
			else
			{
				wsrmFault = null;
				e = new CommunicationException(@string);
			}
			this.OnLocalFault(e, wsrmFault, null);
		}

		// Token: 0x06005C9B RID: 23707
		public abstract void OnLocalActivity();

		// Token: 0x06005C9C RID: 23708 RVA: 0x0015564D File Offset: 0x0015384D
		public void OnUnknownException(Exception e)
		{
			this.canSendFault = false;
			this.OnLocalFault(e, null, null);
		}

		// Token: 0x06005C9D RID: 23709
		public abstract void Open(TimeSpan timeout);

		// Token: 0x06005C9E RID: 23710 RVA: 0x0015565F File Offset: 0x0015385F
		public virtual void OnRemoteActivity(bool fastPolling)
		{
			this.inactivityTimer.Set();
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x0015566C File Offset: 0x0015386C
		public bool ProcessInfo(WsrmMessageInfo info, RequestContext context)
		{
			return this.ProcessInfo(info, context, false);
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x00155678 File Offset: 0x00153878
		public bool ProcessInfo(WsrmMessageInfo info, RequestContext context, bool throwException)
		{
			Exception ex;
			if (info.ParsingException != null)
			{
				WsrmFault fault;
				if (this.SequenceID != null)
				{
					string @string = SR.GetString("CouldNotParseWithAction", new object[]
					{
						info.Action
					});
					fault = SequenceTerminatedFault.CreateProtocolFault(this.SequenceID, @string, null);
				}
				else
				{
					fault = null;
				}
				ex = new ProtocolException(SR.GetString("MessageExceptionOccurred"), info.ParsingException);
				this.OnLocalFault(throwException ? null : ex, fault, context);
			}
			else if (info.FaultReply != null)
			{
				ex = info.FaultException;
				this.OnLocalFault(throwException ? null : ex, info.FaultReply, context);
			}
			else if (info.WsrmHeaderFault != null && info.WsrmHeaderFault.SequenceID != this.InputID && info.WsrmHeaderFault.SequenceID != this.OutputID)
			{
				ex = new ProtocolException(SR.GetString("WrongIdentifierFault", new object[]
				{
					FaultException.GetSafeReasonText(info.WsrmHeaderFault.Reason)
				}));
				this.OnLocalFault(throwException ? null : ex, null, context);
			}
			else
			{
				if (info.FaultInfo == null)
				{
					return true;
				}
				if (this.isSessionClosed)
				{
					UnknownSequenceFault unknownSequenceFault = info.FaultInfo as UnknownSequenceFault;
					if (unknownSequenceFault != null)
					{
						UniqueId sequenceID = unknownSequenceFault.SequenceID;
						if ((this.OutputID != null && this.OutputID == sequenceID) || (this.InputID != null && this.InputID == sequenceID))
						{
							if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
							{
								info.Message.Close();
								return false;
							}
							if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
							{
								return true;
							}
							throw Fx.AssertAndThrow("Unknown version.");
						}
					}
				}
				ex = info.FaultException;
				if (context != null)
				{
					context.Close();
				}
				this.OnRemoteFault(throwException ? null : ex);
			}
			info.Message.Close();
			if (throwException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(ex);
			}
			return false;
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x00155872 File Offset: 0x00153A72
		public void SetFinalAck(SequenceRangeCollection finalRanges)
		{
			this.finalRanges = finalRanges;
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x0015587B File Offset: 0x00153A7B
		public virtual void StartInactivityTimer()
		{
			this.inactivityTimer.Set();
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x00155888 File Offset: 0x00153A88
		private void UnblockChannelIfNecessary()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.faulted == ChannelReliableSession.SessionFaultState.NotFaulted)
				{
					throw Fx.AssertAndThrow("This method must be called from a fault thread.");
				}
				if (this.faulted == ChannelReliableSession.SessionFaultState.CleanedUp)
				{
					return;
				}
			}
			this.OnFaulted();
			this.unblockChannelCloseCallback();
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x001558F4 File Offset: 0x00153AF4
		public bool VerifyDuplexProtocolElements(WsrmMessageInfo info, RequestContext context)
		{
			return this.VerifyDuplexProtocolElements(info, context, false);
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x00155900 File Offset: 0x00153B00
		public bool VerifyDuplexProtocolElements(WsrmMessageInfo info, RequestContext context, bool throwException)
		{
			WsrmFault wsrmFault = this.VerifyDuplexProtocolElements(info);
			if (wsrmFault == null)
			{
				return true;
			}
			if (throwException)
			{
				Exception exception = wsrmFault.CreateException();
				this.OnLocalFault(null, wsrmFault, context);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			this.OnLocalFault(wsrmFault.CreateException(), wsrmFault, context);
			return false;
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x00155948 File Offset: 0x00153B48
		protected virtual WsrmFault VerifyDuplexProtocolElements(WsrmMessageInfo info)
		{
			if (info.AcknowledgementInfo != null && info.AcknowledgementInfo.SequenceID != this.OutputID)
			{
				return new UnknownSequenceFault(info.AcknowledgementInfo.SequenceID);
			}
			if (info.AckRequestedInfo != null && info.AckRequestedInfo.SequenceID != this.InputID)
			{
				return new UnknownSequenceFault(info.AckRequestedInfo.SequenceID);
			}
			if (info.SequencedMessageInfo != null && info.SequencedMessageInfo.SequenceID != this.InputID)
			{
				return new UnknownSequenceFault(info.SequencedMessageInfo.SequenceID);
			}
			if (info.TerminateSequenceInfo != null && info.TerminateSequenceInfo.Identifier != this.InputID)
			{
				if (this.Settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
				{
					return SequenceTerminatedFault.CreateProtocolFault(this.OutputID, SR.GetString("SequenceTerminatedUnexpectedTerminateSequence"), SR.GetString("UnexpectedTerminateSequence"));
				}
				if (info.TerminateSequenceInfo.Identifier == this.OutputID)
				{
					return null;
				}
				return new UnknownSequenceFault(info.TerminateSequenceInfo.Identifier);
			}
			else if (info.TerminateSequenceResponseInfo != null)
			{
				WsrmUtilities.AssertWsrm11(this.settings.ReliableMessagingVersion);
				if (info.TerminateSequenceResponseInfo.Identifier == this.OutputID)
				{
					return null;
				}
				return new UnknownSequenceFault(info.TerminateSequenceResponseInfo.Identifier);
			}
			else if (info.CloseSequenceInfo != null)
			{
				WsrmUtilities.AssertWsrm11(this.settings.ReliableMessagingVersion);
				if (info.CloseSequenceInfo.Identifier == this.InputID)
				{
					return null;
				}
				if (info.CloseSequenceInfo.Identifier == this.OutputID)
				{
					return SequenceTerminatedFault.CreateProtocolFault(this.OutputID, SR.GetString("SequenceTerminatedUnsupportedClose"), SR.GetString("UnsupportedCloseExceptionString"));
				}
				return new UnknownSequenceFault(info.CloseSequenceInfo.Identifier);
			}
			else
			{
				if (info.CloseSequenceResponseInfo == null)
				{
					return null;
				}
				WsrmUtilities.AssertWsrm11(this.settings.ReliableMessagingVersion);
				if (info.CloseSequenceResponseInfo.Identifier == this.OutputID)
				{
					return null;
				}
				if (info.CloseSequenceResponseInfo.Identifier == this.InputID)
				{
					return SequenceTerminatedFault.CreateProtocolFault(this.InputID, SR.GetString("SequenceTerminatedUnexpectedCloseSequenceResponse"), SR.GetString("UnexpectedCloseSequenceResponse"));
				}
				return new UnknownSequenceFault(info.CloseSequenceResponseInfo.Identifier);
			}
		}

		// Token: 0x06005CA7 RID: 23719 RVA: 0x00155B9F File Offset: 0x00153D9F
		public bool VerifySimplexProtocolElements(WsrmMessageInfo info, RequestContext context)
		{
			return this.VerifySimplexProtocolElements(info, context, false);
		}

		// Token: 0x06005CA8 RID: 23720 RVA: 0x00155BAC File Offset: 0x00153DAC
		public bool VerifySimplexProtocolElements(WsrmMessageInfo info, RequestContext context, bool throwException)
		{
			WsrmFault wsrmFault = this.VerifySimplexProtocolElements(info);
			if (wsrmFault == null)
			{
				return true;
			}
			info.Message.Close();
			if (throwException)
			{
				Exception exception = wsrmFault.CreateException();
				this.OnLocalFault(null, wsrmFault, context);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(exception);
			}
			this.OnLocalFault(wsrmFault.CreateException(), wsrmFault, context);
			return false;
		}

		// Token: 0x06005CA9 RID: 23721
		protected abstract WsrmFault VerifySimplexProtocolElements(WsrmMessageInfo info);

		// Token: 0x0400373B RID: 14139
		private IReliableChannelBinder binder;

		// Token: 0x0400373C RID: 14140
		private bool canSendFault = true;

		// Token: 0x0400373D RID: 14141
		private ChannelBase channel;

		// Token: 0x0400373E RID: 14142
		private ChannelReliableSession.SessionFaultState faulted;

		// Token: 0x0400373F RID: 14143
		private FaultHelper faultHelper;

		// Token: 0x04003740 RID: 14144
		private SequenceRangeCollection finalRanges;

		// Token: 0x04003741 RID: 14145
		private Guard guard = new Guard(int.MaxValue);

		// Token: 0x04003742 RID: 14146
		private InterruptibleTimer inactivityTimer;

		// Token: 0x04003743 RID: 14147
		private TimeSpan initiationTime;

		// Token: 0x04003744 RID: 14148
		private UniqueId inputID;

		// Token: 0x04003745 RID: 14149
		private bool isSessionClosed;

		// Token: 0x04003746 RID: 14150
		private UniqueId outputID;

		// Token: 0x04003747 RID: 14151
		private RequestContext replyFaultContext;

		// Token: 0x04003748 RID: 14152
		private IReliableFactorySettings settings;

		// Token: 0x04003749 RID: 14153
		private Message terminatingFault;

		// Token: 0x0400374A RID: 14154
		private object thisLock = new object();

		// Token: 0x0400374B RID: 14155
		private ChannelReliableSession.UnblockChannelCloseHandler unblockChannelCloseCallback;

		// Token: 0x02000DD9 RID: 3545
		private enum SessionFaultState
		{
			// Token: 0x0400495E RID: 18782
			NotFaulted,
			// Token: 0x0400495F RID: 18783
			LocallyFaulted,
			// Token: 0x04004960 RID: 18784
			RemotelyFaulted,
			// Token: 0x04004961 RID: 18785
			CleanedUp
		}

		// Token: 0x02000DDA RID: 3546
		// (Invoke) Token: 0x06008065 RID: 32869
		public delegate void UnblockChannelCloseHandler();
	}
}
