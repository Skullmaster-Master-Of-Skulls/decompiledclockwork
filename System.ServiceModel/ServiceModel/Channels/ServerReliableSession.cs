using System;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000959 RID: 2393
	internal class ServerReliableSession : ChannelReliableSession, IInputSession, ISession
	{
		// Token: 0x06005CBF RID: 23743 RVA: 0x00156744 File Offset: 0x00154944
		public ServerReliableSession(ChannelBase channel, IReliableFactorySettings listener, IServerReliableChannelBinder binder, FaultHelper faultHelper, UniqueId inputID, UniqueId outputID) : base(channel, listener, binder, faultHelper)
		{
			base.InputID = inputID;
			base.OutputID = outputID;
		}

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06005CC0 RID: 23744 RVA: 0x00156761 File Offset: 0x00154961
		public override UniqueId SequenceID
		{
			get
			{
				return base.InputID;
			}
		}

		// Token: 0x06005CC1 RID: 23745 RVA: 0x00156769 File Offset: 0x00154969
		public override IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06005CC2 RID: 23746 RVA: 0x00156772 File Offset: 0x00154972
		public override void EndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
			base.StartInactivityTimer();
		}

		// Token: 0x06005CC3 RID: 23747 RVA: 0x00156780 File Offset: 0x00154980
		public override void OnLocalActivity()
		{
		}

		// Token: 0x06005CC4 RID: 23748 RVA: 0x00156782 File Offset: 0x00154982
		public override void Open(TimeSpan timeout)
		{
			this.StartInactivityTimer();
		}

		// Token: 0x06005CC5 RID: 23749 RVA: 0x0015678C File Offset: 0x0015498C
		protected override WsrmFault VerifyDuplexProtocolElements(WsrmMessageInfo info)
		{
			WsrmFault wsrmFault = base.VerifyDuplexProtocolElements(info);
			if (wsrmFault != null)
			{
				return wsrmFault;
			}
			if (info.CreateSequenceInfo != null && info.CreateSequenceInfo.OfferIdentifier != base.OutputID)
			{
				return SequenceTerminatedFault.CreateProtocolFault(base.OutputID, SR.GetString("SequenceTerminatedUnexpectedCSOfferId"), SR.GetString("UnexpectedCSOfferId"));
			}
			if (info.CreateSequenceResponseInfo != null)
			{
				return SequenceTerminatedFault.CreateProtocolFault(base.OutputID, SR.GetString("SequenceTerminatedUnexpectedCSR"), SR.GetString("UnexpectedCSR"));
			}
			return null;
		}

		// Token: 0x06005CC6 RID: 23750 RVA: 0x00156810 File Offset: 0x00154A10
		protected override WsrmFault VerifySimplexProtocolElements(WsrmMessageInfo info)
		{
			if (info.AcknowledgementInfo != null)
			{
				return SequenceTerminatedFault.CreateProtocolFault(base.InputID, SR.GetString("SequenceTerminatedUnexpectedAcknowledgement"), SR.GetString("UnexpectedAcknowledgement"));
			}
			if (info.AckRequestedInfo != null && info.AckRequestedInfo.SequenceID != base.InputID)
			{
				return new UnknownSequenceFault(info.AckRequestedInfo.SequenceID);
			}
			if (info.CreateSequenceResponseInfo != null)
			{
				return SequenceTerminatedFault.CreateProtocolFault(base.InputID, SR.GetString("SequenceTerminatedUnexpectedCSR"), SR.GetString("UnexpectedCSR"));
			}
			if (info.SequencedMessageInfo != null && info.SequencedMessageInfo.SequenceID != base.InputID)
			{
				return new UnknownSequenceFault(info.SequencedMessageInfo.SequenceID);
			}
			if (info.TerminateSequenceInfo != null && info.TerminateSequenceInfo.Identifier != base.InputID)
			{
				return new UnknownSequenceFault(info.TerminateSequenceInfo.Identifier);
			}
			if (info.TerminateSequenceResponseInfo != null)
			{
				WsrmUtilities.AssertWsrm11(base.Settings.ReliableMessagingVersion);
				if (info.TerminateSequenceResponseInfo.Identifier == base.InputID)
				{
					return SequenceTerminatedFault.CreateProtocolFault(base.InputID, SR.GetString("SequenceTerminatedUnexpectedTerminateSequenceResponse"), SR.GetString("UnexpectedTerminateSequenceResponse"));
				}
				return new UnknownSequenceFault(info.TerminateSequenceResponseInfo.Identifier);
			}
			else if (info.CloseSequenceInfo != null)
			{
				WsrmUtilities.AssertWsrm11(base.Settings.ReliableMessagingVersion);
				if (info.CloseSequenceInfo.Identifier == base.InputID)
				{
					return null;
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
				if (info.CloseSequenceResponseInfo.Identifier == base.InputID)
				{
					return SequenceTerminatedFault.CreateProtocolFault(base.InputID, SR.GetString("SequenceTerminatedUnexpectedCloseSequenceResponse"), SR.GetString("UnexpectedCloseSequenceResponse"));
				}
				return new UnknownSequenceFault(info.CloseSequenceResponseInfo.Identifier);
			}
		}
	}
}
