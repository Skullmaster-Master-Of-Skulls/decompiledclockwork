using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200072B RID: 1835
	internal class DuplexSessionChannelWrapper : InputChannelWrapper, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
	{
		// Token: 0x060045CB RID: 17867 RVA: 0x001056B8 File Offset: 0x001038B8
		public DuplexSessionChannelWrapper(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel, Message firstMessage) : base(channelManager, innerChannel, firstMessage)
		{
		}

		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x001056C3 File Offset: 0x001038C3
		private new IDuplexSessionChannel InnerChannel
		{
			get
			{
				return (IDuplexSessionChannel)base.InnerChannel;
			}
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x060045CD RID: 17869 RVA: 0x001056D0 File Offset: 0x001038D0
		public IDuplexSession Session
		{
			get
			{
				return this.InnerChannel.Session;
			}
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x001056DD File Offset: 0x001038DD
		public EndpointAddress RemoteAddress
		{
			get
			{
				return this.InnerChannel.RemoteAddress;
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x001056EA File Offset: 0x001038EA
		public Uri Via
		{
			get
			{
				return this.InnerChannel.Via;
			}
		}

		// Token: 0x060045D0 RID: 17872 RVA: 0x001056F7 File Offset: 0x001038F7
		public void Send(Message message)
		{
			this.InnerChannel.Send(message);
		}

		// Token: 0x060045D1 RID: 17873 RVA: 0x00105705 File Offset: 0x00103905
		public void Send(Message message, TimeSpan timeout)
		{
			this.InnerChannel.Send(message, timeout);
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x00105714 File Offset: 0x00103914
		public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
		{
			return this.InnerChannel.BeginSend(message, callback, state);
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x00105724 File Offset: 0x00103924
		public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.InnerChannel.BeginSend(message, timeout, callback, state);
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x00105736 File Offset: 0x00103936
		public void EndSend(IAsyncResult result)
		{
			this.InnerChannel.EndSend(result);
		}
	}
}
