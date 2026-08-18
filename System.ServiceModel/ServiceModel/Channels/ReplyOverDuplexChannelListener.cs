using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200075E RID: 1886
	internal class ReplyOverDuplexChannelListener : ReplyOverDuplexChannelListenerBase<IReplyChannel, IDuplexChannel>
	{
		// Token: 0x0600481E RID: 18462 RVA: 0x0010B17B File Offset: 0x0010937B
		public ReplyOverDuplexChannelListener(BindingContext context) : base(context)
		{
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0010B184 File Offset: 0x00109384
		protected override IReplyChannel CreateWrappedChannel(ChannelManagerBase channelManager, IDuplexChannel innerChannel)
		{
			return new ReplyOverDuplexChannel(channelManager, innerChannel);
		}
	}
}
