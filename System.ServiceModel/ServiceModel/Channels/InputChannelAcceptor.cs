using System;
using System.Diagnostics;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000746 RID: 1862
	internal class InputChannelAcceptor : SingletonChannelAcceptor<IInputChannel, InputChannel, Message>
	{
		// Token: 0x06004727 RID: 18215 RVA: 0x00108FBB File Offset: 0x001071BB
		public InputChannelAcceptor(ChannelManagerBase channelManager) : base(channelManager)
		{
		}

		// Token: 0x06004728 RID: 18216 RVA: 0x00108FC4 File Offset: 0x001071C4
		protected override InputChannel OnCreateChannel()
		{
			return new InputChannel(base.ChannelManager, null);
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x00108FD2 File Offset: 0x001071D2
		protected override void OnTraceMessageReceived(Message message)
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				TraceUtility.TraceEvent(TraceEventType.Information, 262163, SR.GetString("TraceCodeMessageReceived"), MessageTransmitTraceRecord.CreateReceiveTraceRecord(message), this, null);
			}
		}
	}
}
