using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B6 RID: 1974
	internal class ContextOutputChannel : ContextOutputChannelBase<IOutputChannel>, IOutputChannel, IChannel, ICommunicationObject
	{
		// Token: 0x06004AA2 RID: 19106 RVA: 0x00112227 File Offset: 0x00110427
		public ContextOutputChannel(ChannelManagerBase channelManager, IOutputChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism, Uri callbackAddress, bool contextManagementEnabled) : base(channelManager, innerChannel)
		{
			this.contextProtocol = new ClientContextProtocol(contextExchangeMechanism, base.InnerChannel.Via, this, callbackAddress, contextManagementEnabled);
		}

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06004AA3 RID: 19107 RVA: 0x0011224D File Offset: 0x0011044D
		protected override ContextProtocol ContextProtocol
		{
			get
			{
				return this.contextProtocol;
			}
		}

		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x00112255 File Offset: 0x00110455
		protected override bool IsClient
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002F21 RID: 12065
		private ClientContextProtocol contextProtocol;
	}
}
