using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007BF RID: 1983
	internal class ContextRequestSessionChannel : ContextRequestChannelBase<IRequestSessionChannel>, IRequestSessionChannel, IRequestChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
		// Token: 0x06004AE1 RID: 19169 RVA: 0x001128CE File Offset: 0x00110ACE
		public ContextRequestSessionChannel(ChannelManagerBase channelManager, IRequestSessionChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism, Uri callbackAddress, bool contextManagementEnabled) : base(channelManager, innerChannel, contextExchangeMechanism, callbackAddress, contextManagementEnabled)
		{
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x001128DD File Offset: 0x00110ADD
		public IOutputSession Session
		{
			get
			{
				return base.InnerChannel.Session;
			}
		}
	}
}
