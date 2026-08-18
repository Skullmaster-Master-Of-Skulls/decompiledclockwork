using System;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000918 RID: 2328
	internal class ReliableChannelFactory<TChannel, InnerChannel> : ChannelFactoryBase<TChannel>, IReliableFactorySettings where InnerChannel : class, IChannel
	{
		// Token: 0x06005917 RID: 22807 RVA: 0x001460CC File Offset: 0x001442CC
		public ReliableChannelFactory(ReliableSessionBindingElement settings, IChannelFactory<InnerChannel> innerChannelFactory, Binding binding) : base(binding)
		{
			this.acknowledgementInterval = settings.AcknowledgementInterval;
			this.flowControlEnabled = settings.FlowControlEnabled;
			this.inactivityTimeout = settings.InactivityTimeout;
			this.maxPendingChannels = settings.MaxPendingChannels;
			this.maxRetryCount = settings.MaxRetryCount;
			this.maxTransferWindowSize = settings.MaxTransferWindowSize;
			this.messageVersion = binding.MessageVersion;
			this.ordered = settings.Ordered;
			this.reliableMessagingVersion = settings.ReliableMessagingVersion;
			this.innerChannelFactory = innerChannelFactory;
			this.faultHelper = new SendFaultHelper(binding.SendTimeout, binding.CloseTimeout);
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06005918 RID: 22808 RVA: 0x0014616A File Offset: 0x0014436A
		public TimeSpan AcknowledgementInterval
		{
			get
			{
				return this.acknowledgementInterval;
			}
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06005919 RID: 22809 RVA: 0x00146172 File Offset: 0x00144372
		public FaultHelper FaultHelper
		{
			get
			{
				return this.faultHelper;
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x0600591A RID: 22810 RVA: 0x0014617A File Offset: 0x0014437A
		public bool FlowControlEnabled
		{
			get
			{
				return this.flowControlEnabled;
			}
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x0600591B RID: 22811 RVA: 0x00146182 File Offset: 0x00144382
		public TimeSpan InactivityTimeout
		{
			get
			{
				return this.inactivityTimeout;
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x0600591C RID: 22812 RVA: 0x0014618A File Offset: 0x0014438A
		protected IChannelFactory<InnerChannel> InnerChannelFactory
		{
			get
			{
				return this.innerChannelFactory;
			}
		}

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x00146192 File Offset: 0x00144392
		public int MaxPendingChannels
		{
			get
			{
				return this.maxPendingChannels;
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x0014619A File Offset: 0x0014439A
		public int MaxRetryCount
		{
			get
			{
				return this.maxRetryCount;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x0600591F RID: 22815 RVA: 0x001461A2 File Offset: 0x001443A2
		public MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06005920 RID: 22816 RVA: 0x001461AA File Offset: 0x001443AA
		public int MaxTransferWindowSize
		{
			get
			{
				return this.maxTransferWindowSize;
			}
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06005921 RID: 22817 RVA: 0x001461B2 File Offset: 0x001443B2
		public bool Ordered
		{
			get
			{
				return this.ordered;
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06005922 RID: 22818 RVA: 0x001461BA File Offset: 0x001443BA
		public ReliableMessagingVersion ReliableMessagingVersion
		{
			get
			{
				return this.reliableMessagingVersion;
			}
		}

		// Token: 0x06005923 RID: 22819 RVA: 0x001461C4 File Offset: 0x001443C4
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IChannelFactory<TChannel>))
			{
				return (T)((object)this);
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			return this.innerChannelFactory.GetProperty<T>();
		}

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x0014620F File Offset: 0x0014440F
		public TimeSpan SendTimeout
		{
			get
			{
				return base.InternalSendTimeout;
			}
		}

		// Token: 0x06005925 RID: 22821 RVA: 0x00146217 File Offset: 0x00144417
		protected override void OnAbort()
		{
			base.OnAbort();
			this.faultHelper.Abort();
			this.innerChannelFactory.Abort();
		}

		// Token: 0x06005926 RID: 22822 RVA: 0x00146235 File Offset: 0x00144435
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerChannelFactory.Open(timeout);
		}

		// Token: 0x06005927 RID: 22823 RVA: 0x00146243 File Offset: 0x00144443
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelFactory.BeginOpen(callback, state);
		}

		// Token: 0x06005928 RID: 22824 RVA: 0x00146252 File Offset: 0x00144452
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerChannelFactory.EndOpen(result);
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x00146260 File Offset: 0x00144460
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeoutHelper.RemainingTime());
			this.faultHelper.Close(timeoutHelper.RemainingTime());
			this.innerChannelFactory.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x0600592A RID: 22826 RVA: 0x001462A8 File Offset: 0x001444A8
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(base.OnBeginClose),
				new OperationWithTimeoutBeginCallback(this.faultHelper.BeginClose),
				new OperationWithTimeoutBeginCallback(this.innerChannelFactory.BeginClose)
			}, new OperationEndCallback[]
			{
				new OperationEndCallback(base.OnEndClose),
				new OperationEndCallback(this.faultHelper.EndClose),
				new OperationEndCallback(this.innerChannelFactory.EndClose)
			}, callback, state);
		}

		// Token: 0x0600592B RID: 22827 RVA: 0x0014633B File Offset: 0x0014453B
		protected override void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x0600592C RID: 22828 RVA: 0x00146344 File Offset: 0x00144544
		protected override TChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			LateBoundChannelParameterCollection channelParameters = new LateBoundChannelParameterCollection();
			IClientReliableChannelBinder binder = ClientReliableChannelBinder<InnerChannel>.CreateBinder(address, via, this.InnerChannelFactory, MaskingMode.All, TolerateFaultsMode.IfNotSecuritySession, channelParameters, this.DefaultCloseTimeout, this.DefaultSendTimeout);
			if (typeof(TChannel) == typeof(IOutputSessionChannel))
			{
				if (typeof(InnerChannel) == typeof(IDuplexChannel) || typeof(InnerChannel) == typeof(IDuplexSessionChannel))
				{
					return (TChannel)((object)new ReliableOutputSessionChannelOverDuplex(this, this, binder, this.faultHelper, channelParameters));
				}
				return (TChannel)((object)new ReliableOutputSessionChannelOverRequest(this, this, binder, this.faultHelper, channelParameters));
			}
			else
			{
				if (typeof(TChannel) == typeof(IDuplexSessionChannel))
				{
					return (TChannel)((object)new ClientReliableDuplexSessionChannel(this, this, binder, this.faultHelper, channelParameters, WsrmUtilities.NextSequenceId()));
				}
				return (TChannel)((object)new ReliableRequestSessionChannel(this, this, binder, this.faultHelper, channelParameters, WsrmUtilities.NextSequenceId()));
			}
		}

		// Token: 0x04003649 RID: 13897
		private TimeSpan acknowledgementInterval;

		// Token: 0x0400364A RID: 13898
		private FaultHelper faultHelper;

		// Token: 0x0400364B RID: 13899
		private bool flowControlEnabled;

		// Token: 0x0400364C RID: 13900
		private TimeSpan inactivityTimeout;

		// Token: 0x0400364D RID: 13901
		private int maxPendingChannels;

		// Token: 0x0400364E RID: 13902
		private int maxRetryCount;

		// Token: 0x0400364F RID: 13903
		private int maxTransferWindowSize;

		// Token: 0x04003650 RID: 13904
		private MessageVersion messageVersion;

		// Token: 0x04003651 RID: 13905
		private bool ordered;

		// Token: 0x04003652 RID: 13906
		private ReliableMessagingVersion reliableMessagingVersion;

		// Token: 0x04003653 RID: 13907
		private IChannelFactory<InnerChannel> innerChannelFactory;
	}
}
