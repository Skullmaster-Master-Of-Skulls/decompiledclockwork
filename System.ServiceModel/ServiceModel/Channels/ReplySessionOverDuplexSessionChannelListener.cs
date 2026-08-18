using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200075F RID: 1887
	internal class ReplySessionOverDuplexSessionChannelListener : ReplyOverDuplexChannelListenerBase<IReplySessionChannel, IDuplexSessionChannel>
	{
		// Token: 0x06004820 RID: 18464 RVA: 0x0010B18D File Offset: 0x0010938D
		public ReplySessionOverDuplexSessionChannelListener(BindingContext context) : base(context)
		{
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x0010B196 File Offset: 0x00109396
		protected override IReplySessionChannel CreateWrappedChannel(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel)
		{
			return new ReplySessionOverDuplexSessionChannel(channelManager, innerChannel);
		}
	}
}
