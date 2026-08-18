using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020007B7 RID: 1975
	internal class ContextOutputSessionChannel : ContextOutputChannelBase<IOutputSessionChannel>, IOutputSessionChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
	{
		// Token: 0x06004AA5 RID: 19109 RVA: 0x00112258 File Offset: 0x00110458
		public ContextOutputSessionChannel(ChannelManagerBase channelManager, IOutputSessionChannel innerChannel, ContextExchangeMechanism contextExchangeMechanism, Uri callbackAddress, bool contextManagementEnabled) : base(channelManager, innerChannel)
		{
			this.contextProtocol = new ClientContextProtocol(contextExchangeMechanism, base.InnerChannel.Via, this, callbackAddress, contextManagementEnabled);
		}

		// Token: 0x170012CB RID: 4811
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x0011227E File Offset: 0x0011047E
		public IOutputSession Session
		{
			get
			{
				return base.InnerChannel.Session;
			}
		}

		// Token: 0x170012CC RID: 4812
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x0011228B File Offset: 0x0011048B
		protected override ContextProtocol ContextProtocol
		{
			get
			{
				return this.contextProtocol;
			}
		}

		// Token: 0x170012CD RID: 4813
		// (get) Token: 0x06004AA8 RID: 19112 RVA: 0x00112293 File Offset: 0x00110493
		protected override bool IsClient
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04002F22 RID: 12066
		private ClientContextProtocol contextProtocol;
	}
}
