using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000765 RID: 1893
	internal class ReplyChannelAcceptor : SingletonChannelAcceptor<IReplyChannel, ReplyChannel, RequestContext>
	{
		// Token: 0x0600484D RID: 18509 RVA: 0x0010B78B File Offset: 0x0010998B
		public ReplyChannelAcceptor(ChannelManagerBase channelManager) : base(channelManager)
		{
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x0010B794 File Offset: 0x00109994
		protected override ReplyChannel OnCreateChannel()
		{
			return new ReplyChannel(base.ChannelManager, null);
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x0010B7A2 File Offset: 0x001099A2
		protected override void OnTraceMessageReceived(RequestContext requestContext)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262163, SR.GetString("TraceCodeMessageReceived"), MessageTransmitTraceRecord.CreateReceiveTraceRecord((requestContext == null) ? null : requestContext.RequestMessage), this, null);
			}
		}
	}
}
