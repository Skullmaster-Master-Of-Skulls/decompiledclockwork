using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200072D RID: 1837
	internal class ReplySessionChannelWrapper : ReplyChannelWrapper, IReplySessionChannel, IReplyChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
	{
		// Token: 0x060045DE RID: 17886 RVA: 0x00105965 File Offset: 0x00103B65
		public ReplySessionChannelWrapper(ChannelManagerBase channelManager, IReplySessionChannel innerChannel, RequestContext firstRequest) : base(channelManager, innerChannel, firstRequest)
		{
		}

		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x060045DF RID: 17887 RVA: 0x00105970 File Offset: 0x00103B70
		private new IReplySessionChannel InnerChannel
		{
			get
			{
				return (IReplySessionChannel)base.InnerChannel;
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x0010597D File Offset: 0x00103B7D
		public IInputSession Session
		{
			get
			{
				return this.InnerChannel.Session;
			}
		}
	}
}
