using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000762 RID: 1890
	internal class ReplySessionOverDuplexSessionChannel : ReplyOverDuplexChannelBase<IDuplexSessionChannel>, IReplySessionChannel, IReplyChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
	{
		// Token: 0x06004831 RID: 18481 RVA: 0x0010B2D4 File Offset: 0x001094D4
		public ReplySessionOverDuplexSessionChannel(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel) : base(channelManager, innerChannel)
		{
			this.session = new ReplySessionOverDuplexSessionChannel.ReplySessionOverDuplexSession(innerChannel.Session);
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06004832 RID: 18482 RVA: 0x0010B2EF File Offset: 0x001094EF
		public IInputSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x04002DE2 RID: 11746
		private ReplySessionOverDuplexSessionChannel.ReplySessionOverDuplexSession session;

		// Token: 0x02000CE1 RID: 3297
		private class ReplySessionOverDuplexSession : IInputSession, ISession
		{
			// Token: 0x06007A1A RID: 31258 RVA: 0x001C73A4 File Offset: 0x001C55A4
			public ReplySessionOverDuplexSession(IDuplexSession innerSession)
			{
				if (innerSession == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("innerSession");
				}
				this.innerSession = innerSession;
			}

			// Token: 0x17001BA0 RID: 7072
			// (get) Token: 0x06007A1B RID: 31259 RVA: 0x001C73C6 File Offset: 0x001C55C6
			public string Id
			{
				get
				{
					return this.innerSession.Id;
				}
			}

			// Token: 0x040045D7 RID: 17879
			private IDuplexSession innerSession;
		}
	}
}
