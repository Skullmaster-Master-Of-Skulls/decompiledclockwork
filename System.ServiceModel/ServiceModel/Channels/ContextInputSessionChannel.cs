using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007BA RID: 1978
	internal class ContextInputSessionChannel : ContextInputChannelBase<IInputSessionChannel>, IInputSessionChannel, IInputChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
	{
		// Token: 0x06004AB8 RID: 19128 RVA: 0x00112412 File Offset: 0x00110612
		public ContextInputSessionChannel(ChannelManagerBase channelManager, IInputSessionChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism) : base(channelManager, innerChannel, contextExchangeMechanism)
		{
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x0011241D File Offset: 0x0011061D
		public IInputSession Session
		{
			get
			{
				return base.InnerChannel.Session;
			}
		}
	}
}
