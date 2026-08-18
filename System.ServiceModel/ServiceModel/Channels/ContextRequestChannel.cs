using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007BE RID: 1982
	internal class ContextRequestChannel : ContextRequestChannelBase<IRequestChannel>, IRequestChannel, IChannel, ICommunicationObject
	{
		// Token: 0x06004AE0 RID: 19168 RVA: 0x001128BF File Offset: 0x00110ABF
		public ContextRequestChannel(ChannelManagerBase channelManager, IRequestChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism, Uri callbackAddress, bool contextManagementEnabled) : base(channelManager, innerChannel, contextExchangeMechanism, callbackAddress, contextManagementEnabled)
		{
		}
	}
}
