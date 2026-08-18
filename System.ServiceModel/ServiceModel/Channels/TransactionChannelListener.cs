using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A68 RID: 2664
	internal sealed class TransactionChannelListener<TChannel> : DelegatingChannelListener<TChannel>, ITransactionChannelManager where TChannel : class, IChannel
	{
		// Token: 0x06006921 RID: 26913 RVA: 0x00188BA5 File Offset: 0x00186DA5
		public TransactionChannelListener(TransactionProtocol transactionProtocol, IDefaultCommunicationTimeouts timeouts, Dictionary<DirectionalAction, TransactionFlowOption> dictionary, IChannelListener<TChannel> innerListener) : base(timeouts, innerListener)
		{
			this.dictionary = dictionary;
			this.TransactionProtocol = transactionProtocol;
			base.Acceptor = new TransactionChannelListener<TChannel>.TransactionChannelAcceptor(this, innerListener);
			this.standardsManager = SecurityStandardsHelper.CreateStandardsManager(this.TransactionProtocol);
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x06006922 RID: 26914 RVA: 0x00188BDD File Offset: 0x00186DDD
		// (set) Token: 0x06006923 RID: 26915 RVA: 0x00188BE5 File Offset: 0x00186DE5
		public TransactionProtocol TransactionProtocol
		{
			get
			{
				return this.transactionProtocol;
			}
			set
			{
				if (!TransactionProtocol.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("SFxBadTransactionProtocols")));
				}
				this.transactionProtocol = value;
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x06006924 RID: 26916 RVA: 0x00188C10 File Offset: 0x00186E10
		// (set) Token: 0x06006925 RID: 26917 RVA: 0x00188C18 File Offset: 0x00186E18
		public TransactionFlowOption FlowIssuedTokens
		{
			get
			{
				return this.flowIssuedTokens;
			}
			set
			{
				this.flowIssuedTokens = value;
			}
		}

		// Token: 0x1700191C RID: 6428
		// (get) Token: 0x06006926 RID: 26918 RVA: 0x00188C21 File Offset: 0x00186E21
		// (set) Token: 0x06006927 RID: 26919 RVA: 0x00188C29 File Offset: 0x00186E29
		public SecurityStandardsManager StandardsManager
		{
			get
			{
				return this.standardsManager;
			}
			set
			{
				this.standardsManager = ((value != null) ? value : SecurityStandardsHelper.CreateStandardsManager(this.transactionProtocol));
			}
		}

		// Token: 0x1700191D RID: 6429
		// (get) Token: 0x06006928 RID: 26920 RVA: 0x00188C42 File Offset: 0x00186E42
		public IDictionary<DirectionalAction, TransactionFlowOption> Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		// Token: 0x06006929 RID: 26921 RVA: 0x00188C4C File Offset: 0x00186E4C
		public TransactionFlowOption GetTransaction(MessageDirection direction, string action)
		{
			TransactionFlowOption result;
			if (this.dictionary.TryGetValue(new DirectionalAction(direction, action), out result))
			{
				return result;
			}
			if (this.dictionary.TryGetValue(new DirectionalAction(direction, "*"), out result))
			{
				return result;
			}
			return TransactionFlowOption.NotAllowed;
		}

		// Token: 0x04003C29 RID: 15401
		private TransactionFlowOption flowIssuedTokens;

		// Token: 0x04003C2A RID: 15402
		private Dictionary<DirectionalAction, TransactionFlowOption> dictionary;

		// Token: 0x04003C2B RID: 15403
		private SecurityStandardsManager standardsManager;

		// Token: 0x04003C2C RID: 15404
		private TransactionProtocol transactionProtocol;

		// Token: 0x02000E99 RID: 3737
		private class TransactionChannelAcceptor : LayeredChannelAcceptor<TChannel, TChannel>
		{
			// Token: 0x06008425 RID: 33829 RVA: 0x001E867F File Offset: 0x001E687F
			public TransactionChannelAcceptor(TransactionChannelListener<TChannel> listener, IChannelListener<TChannel> innerListener) : base(listener, innerListener)
			{
				this.listener = listener;
			}

			// Token: 0x06008426 RID: 33830 RVA: 0x001E8690 File Offset: 0x001E6890
			protected override TChannel OnAcceptChannel(TChannel innerChannel)
			{
				if (typeof(TChannel) == typeof(IInputSessionChannel))
				{
					return (TChannel)((object)new TransactionChannelListener<TChannel>.TransactionInputSessionChannel(this.listener, (IInputSessionChannel)((object)innerChannel)));
				}
				if (typeof(TChannel) == typeof(IDuplexSessionChannel))
				{
					return (TChannel)((object)new TransactionChannelListener<TChannel>.TransactionDuplexSessionChannel(this.listener, (IDuplexSessionChannel)((object)innerChannel)));
				}
				if (typeof(TChannel) == typeof(IInputChannel))
				{
					return (TChannel)((object)new TransactionChannelListener<TChannel>.TransactionInputChannel(this.listener, (IInputChannel)((object)innerChannel)));
				}
				if (typeof(TChannel) == typeof(IReplyChannel))
				{
					return (TChannel)((object)new TransactionChannelListener<TChannel>.TransactionReplyChannel(this.listener, (IReplyChannel)((object)innerChannel)));
				}
				if (typeof(TChannel) == typeof(IReplySessionChannel))
				{
					return (TChannel)((object)new TransactionChannelListener<TChannel>.TransactionReplySessionChannel(this.listener, (IReplySessionChannel)((object)innerChannel)));
				}
				if (typeof(TChannel) == typeof(IDuplexChannel))
				{
					return (TChannel)((object)new TransactionChannelListener<TChannel>.TransactionDuplexChannel(this.listener, (IDuplexChannel)((object)innerChannel)));
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.listener.CreateChannelTypeNotSupportedException(typeof(TChannel)));
			}

			// Token: 0x04004B9E RID: 19358
			private TransactionChannelListener<TChannel> listener;
		}

		// Token: 0x02000E9A RID: 3738
		private sealed class TransactionInputChannel : TransactionReceiveChannelGeneric<IInputChannel>
		{
			// Token: 0x06008427 RID: 33831 RVA: 0x001E8806 File Offset: 0x001E6A06
			public TransactionInputChannel(ChannelManagerBase channelManager, IInputChannel innerChannel) : base(channelManager, innerChannel, MessageDirection.Input)
			{
			}
		}

		// Token: 0x02000E9B RID: 3739
		private sealed class TransactionReplyChannel : TransactionReplyChannelGeneric<IReplyChannel>
		{
			// Token: 0x06008428 RID: 33832 RVA: 0x001E8811 File Offset: 0x001E6A11
			public TransactionReplyChannel(ChannelManagerBase channelManager, IReplyChannel innerChannel) : base(channelManager, innerChannel)
			{
			}
		}

		// Token: 0x02000E9C RID: 3740
		private sealed class TransactionDuplexChannel : TransactionInputDuplexChannelGeneric<IDuplexChannel>
		{
			// Token: 0x06008429 RID: 33833 RVA: 0x001E881B File Offset: 0x001E6A1B
			public TransactionDuplexChannel(ChannelManagerBase channelManager, IDuplexChannel innerChannel) : base(channelManager, innerChannel)
			{
			}
		}

		// Token: 0x02000E9D RID: 3741
		private sealed class TransactionInputSessionChannel : TransactionReceiveChannelGeneric<IInputSessionChannel>, IInputSessionChannel, IInputChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
		{
			// Token: 0x0600842A RID: 33834 RVA: 0x001E8825 File Offset: 0x001E6A25
			public TransactionInputSessionChannel(ChannelManagerBase channelManager, IInputSessionChannel innerChannel) : base(channelManager, innerChannel, MessageDirection.Input)
			{
			}

			// Token: 0x17001D24 RID: 7460
			// (get) Token: 0x0600842B RID: 33835 RVA: 0x001E8830 File Offset: 0x001E6A30
			public IInputSession Session
			{
				get
				{
					return base.InnerChannel.Session;
				}
			}
		}

		// Token: 0x02000E9E RID: 3742
		private sealed class TransactionReplySessionChannel : TransactionReplyChannelGeneric<IReplySessionChannel>, IReplySessionChannel, IReplyChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
		{
			// Token: 0x0600842C RID: 33836 RVA: 0x001E883D File Offset: 0x001E6A3D
			public TransactionReplySessionChannel(ChannelManagerBase channelManager, IReplySessionChannel innerChannel) : base(channelManager, innerChannel)
			{
			}

			// Token: 0x17001D25 RID: 7461
			// (get) Token: 0x0600842D RID: 33837 RVA: 0x001E8847 File Offset: 0x001E6A47
			public IInputSession Session
			{
				get
				{
					return base.InnerChannel.Session;
				}
			}
		}

		// Token: 0x02000E9F RID: 3743
		private sealed class TransactionDuplexSessionChannel : TransactionInputDuplexChannelGeneric<IDuplexSessionChannel>, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
		{
			// Token: 0x0600842E RID: 33838 RVA: 0x001E8854 File Offset: 0x001E6A54
			public TransactionDuplexSessionChannel(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel) : base(channelManager, innerChannel)
			{
			}

			// Token: 0x17001D26 RID: 7462
			// (get) Token: 0x0600842F RID: 33839 RVA: 0x001E885E File Offset: 0x001E6A5E
			public IDuplexSession Session
			{
				get
				{
					return base.InnerChannel.Session;
				}
			}
		}
	}
}
