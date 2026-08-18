using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000761 RID: 1889
	internal class ReplyOverDuplexChannel : ReplyOverDuplexChannelBase<IDuplexChannel>
	{
		// Token: 0x06004830 RID: 18480 RVA: 0x0010B2CA File Offset: 0x001094CA
		public ReplyOverDuplexChannel(ChannelManagerBase channelManager, IDuplexChannel innerChannel) : base(channelManager, innerChannel)
		{
		}
	}
}
