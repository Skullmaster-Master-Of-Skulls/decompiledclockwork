using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000729 RID: 1833
	internal class InputSessionChannelWrapper : InputChannelWrapper, IInputSessionChannel, IInputChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
	{
		// Token: 0x060045BF RID: 17855 RVA: 0x0010547D File Offset: 0x0010367D
		public InputSessionChannelWrapper(ChannelManagerBase channelManager, IInputSessionChannel innerChannel, Message firstMessage) : base(channelManager, innerChannel, firstMessage)
		{
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x00105488 File Offset: 0x00103688
		private new IInputSessionChannel InnerChannel
		{
			get
			{
				return (IInputSessionChannel)base.InnerChannel;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x00105495 File Offset: 0x00103695
		public IInputSession Session
		{
			get
			{
				return this.InnerChannel.Session;
			}
		}
	}
}
