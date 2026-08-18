using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B9 RID: 1977
	internal class ContextInputChannel : ContextInputChannelBase<IInputChannel>, IInputChannel, IChannel, ICommunicationObject
	{
		// Token: 0x06004AB7 RID: 19127 RVA: 0x00112407 File Offset: 0x00110607
		public ContextInputChannel(ChannelManagerBase channelManager, IInputChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism) : base(channelManager, innerChannel, contextExchangeMechanism)
		{
		}
	}
}
