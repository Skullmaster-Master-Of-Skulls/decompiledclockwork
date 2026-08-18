using System;
using System.Collections.Generic;
using System.ServiceModel.Description;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A63 RID: 2659
	internal sealed class TransactionChannelFactory<TChannel> : LayeredChannelFactory<TChannel>, ITransactionChannelManager
	{
		// Token: 0x06006902 RID: 26882 RVA: 0x00188711 File Offset: 0x00186911
		public TransactionChannelFactory(TransactionProtocol transactionProtocol, BindingContext context, Dictionary<DirectionalAction, TransactionFlowOption> dictionary, bool allowWildcardAction) : base(context.Binding, context.BuildInnerChannelFactory<TChannel>())
		{
			this.dictionary = dictionary;
			this.TransactionProtocol = transactionProtocol;
			this.allowWildcardAction = allowWildcardAction;
			this.standardsManager = SecurityStandardsHelper.CreateStandardsManager(this.TransactionProtocol);
		}

		// Token: 0x17001912 RID: 6418
		// (get) Token: 0x06006903 RID: 26883 RVA: 0x0018874C File Offset: 0x0018694C
		// (set) Token: 0x06006904 RID: 26884 RVA: 0x00188754 File Offset: 0x00186954
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

		// Token: 0x17001913 RID: 6419
		// (get) Token: 0x06006905 RID: 26885 RVA: 0x0018877F File Offset: 0x0018697F
		// (set) Token: 0x06006906 RID: 26886 RVA: 0x00188787 File Offset: 0x00186987
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

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x06006907 RID: 26887 RVA: 0x00188790 File Offset: 0x00186990
		// (set) Token: 0x06006908 RID: 26888 RVA: 0x00188798 File Offset: 0x00186998
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

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06006909 RID: 26889 RVA: 0x001887B1 File Offset: 0x001869B1
		public IDictionary<DirectionalAction, TransactionFlowOption> Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		// Token: 0x0600690A RID: 26890 RVA: 0x001887BC File Offset: 0x001869BC
		public TransactionFlowOption GetTransaction(MessageDirection direction, string action)
		{
			TransactionFlowOption result;
			if (this.dictionary.TryGetValue(new DirectionalAction(direction, action), out result))
			{
				return result;
			}
			if (this.allowWildcardAction && this.dictionary.TryGetValue(new DirectionalAction(direction, "*"), out result))
			{
				return result;
			}
			return TransactionFlowOption.NotAllowed;
		}

		// Token: 0x0600690B RID: 26891 RVA: 0x00188808 File Offset: 0x00186A08
		protected override TChannel OnCreateChannel(EndpointAddress remoteAddress, Uri via)
		{
			TChannel innerChannel = ((IChannelFactory<TChannel>)base.InnerChannelFactory).CreateChannel(remoteAddress, via);
			return this.CreateTransactionChannel(innerChannel);
		}

		// Token: 0x0600690C RID: 26892 RVA: 0x00188830 File Offset: 0x00186A30
		private TChannel CreateTransactionChannel(TChannel innerChannel)
		{
			if (typeof(TChannel) == typeof(IDuplexSessionChannel))
			{
				return (TChannel)((object)new TransactionChannelFactory<TChannel>.TransactionDuplexSessionChannel(this, (IDuplexSessionChannel)((object)innerChannel)));
			}
			if (typeof(TChannel) == typeof(IRequestSessionChannel))
			{
				return (TChannel)((object)new TransactionChannelFactory<TChannel>.TransactionRequestSessionChannel(this, (IRequestSessionChannel)((object)innerChannel)));
			}
			if (typeof(TChannel) == typeof(IOutputSessionChannel))
			{
				return (TChannel)((object)new TransactionChannelFactory<TChannel>.TransactionOutputSessionChannel(this, (IOutputSessionChannel)((object)innerChannel)));
			}
			if (typeof(TChannel) == typeof(IOutputChannel))
			{
				return (TChannel)((object)new TransactionChannelFactory<TChannel>.TransactionOutputChannel(this, (IOutputChannel)((object)innerChannel)));
			}
			if (typeof(TChannel) == typeof(IRequestChannel))
			{
				return (TChannel)((object)new TransactionChannelFactory<TChannel>.TransactionRequestChannel(this, (IRequestChannel)((object)innerChannel)));
			}
			if (typeof(TChannel) == typeof(IDuplexChannel))
			{
				return (TChannel)((object)new TransactionChannelFactory<TChannel>.TransactionDuplexChannel(this, (IDuplexChannel)((object)innerChannel)));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateChannelTypeNotSupportedException(typeof(TChannel)));
		}

		// Token: 0x04003C23 RID: 15395
		private TransactionFlowOption flowIssuedTokens;

		// Token: 0x04003C24 RID: 15396
		private SecurityStandardsManager standardsManager;

		// Token: 0x04003C25 RID: 15397
		private Dictionary<DirectionalAction, TransactionFlowOption> dictionary;

		// Token: 0x04003C26 RID: 15398
		private TransactionProtocol transactionProtocol;

		// Token: 0x04003C27 RID: 15399
		private bool allowWildcardAction;

		// Token: 0x02000E93 RID: 3731
		private sealed class TransactionOutputChannel : TransactionOutputChannelGeneric<IOutputChannel>
		{
			// Token: 0x0600841C RID: 33820 RVA: 0x001E861C File Offset: 0x001E681C
			public TransactionOutputChannel(ChannelManagerBase channelManager, IOutputChannel innerChannel) : base(channelManager, innerChannel)
			{
			}
		}

		// Token: 0x02000E94 RID: 3732
		private sealed class TransactionRequestChannel : TransactionRequestChannelGeneric<IRequestChannel>
		{
			// Token: 0x0600841D RID: 33821 RVA: 0x001E8626 File Offset: 0x001E6826
			public TransactionRequestChannel(ChannelManagerBase channelManager, IRequestChannel innerChannel) : base(channelManager, innerChannel)
			{
			}
		}

		// Token: 0x02000E95 RID: 3733
		private sealed class TransactionDuplexChannel : TransactionOutputDuplexChannelGeneric<IDuplexChannel>
		{
			// Token: 0x0600841E RID: 33822 RVA: 0x001E8630 File Offset: 0x001E6830
			public TransactionDuplexChannel(ChannelManagerBase channelManager, IDuplexChannel innerChannel) : base(channelManager, innerChannel)
			{
			}
		}

		// Token: 0x02000E96 RID: 3734
		private sealed class TransactionOutputSessionChannel : TransactionOutputChannelGeneric<IOutputSessionChannel>, IOutputSessionChannel, IOutputChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
		{
			// Token: 0x0600841F RID: 33823 RVA: 0x001E863A File Offset: 0x001E683A
			public TransactionOutputSessionChannel(ChannelManagerBase channelManager, IOutputSessionChannel innerChannel) : base(channelManager, innerChannel)
			{
			}

			// Token: 0x17001D21 RID: 7457
			// (get) Token: 0x06008420 RID: 33824 RVA: 0x001E8644 File Offset: 0x001E6844
			public IOutputSession Session
			{
				get
				{
					return base.InnerChannel.Session;
				}
			}
		}

		// Token: 0x02000E97 RID: 3735
		private sealed class TransactionRequestSessionChannel : TransactionRequestChannelGeneric<IRequestSessionChannel>, IRequestSessionChannel, IRequestChannel, IChannel, ICommunicationObject, ISessionChannel<IOutputSession>
		{
			// Token: 0x06008421 RID: 33825 RVA: 0x001E8651 File Offset: 0x001E6851
			public TransactionRequestSessionChannel(ChannelManagerBase channelManager, IRequestSessionChannel innerChannel) : base(channelManager, innerChannel)
			{
			}

			// Token: 0x17001D22 RID: 7458
			// (get) Token: 0x06008422 RID: 33826 RVA: 0x001E865B File Offset: 0x001E685B
			public IOutputSession Session
			{
				get
				{
					return base.InnerChannel.Session;
				}
			}
		}

		// Token: 0x02000E98 RID: 3736
		private sealed class TransactionDuplexSessionChannel : TransactionOutputDuplexChannelGeneric<IDuplexSessionChannel>, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
		{
			// Token: 0x06008423 RID: 33827 RVA: 0x001E8668 File Offset: 0x001E6868
			public TransactionDuplexSessionChannel(ChannelManagerBase channelManager, IDuplexSessionChannel innerChannel) : base(channelManager, innerChannel)
			{
			}

			// Token: 0x17001D23 RID: 7459
			// (get) Token: 0x06008424 RID: 33828 RVA: 0x001E8672 File Offset: 0x001E6872
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
