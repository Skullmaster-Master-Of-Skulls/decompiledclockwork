using System;
using System.Runtime;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000015 RID: 21
	internal class DiscoveryClientChannelFactory<TChannel> : ChannelFactoryBase<TChannel>
	{
		// Token: 0x0600013F RID: 319 RVA: 0x00005BA2 File Offset: 0x00003DA2
		public DiscoveryClientChannelFactory(IChannelFactory<TChannel> innerChannelFactory, FindCriteria findCriteria, DiscoveryEndpointProvider discoveryEndpointProvider)
		{
			this.findCriteria = findCriteria;
			this.discoveryEndpointProvider = discoveryEndpointProvider;
			this.innerChannelFactory = innerChannelFactory;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005BC0 File Offset: 0x00003DC0
		protected override TChannel OnCreateChannel(EndpointAddress address, Uri via)
		{
			if (!address.Equals(DiscoveryClientBindingElement.DiscoveryEndpointAddress))
			{
				throw FxTrace.Exception.Argument("address", SR.DiscoveryEndpointAddressIncorrect("address", address.Uri, DiscoveryClientBindingElement.DiscoveryEndpointAddress.Uri));
			}
			if (!via.Equals(DiscoveryClientBindingElement.DiscoveryEndpointAddress.Uri))
			{
				throw FxTrace.Exception.Argument("via", SR.DiscoveryEndpointAddressIncorrect("via", via, DiscoveryClientBindingElement.DiscoveryEndpointAddress.Uri));
			}
			if (typeof(TChannel) == typeof(IOutputChannel))
			{
				return (TChannel)((object)new DiscoveryClientOutputChannel<IOutputChannel>(this, (IChannelFactory<IOutputChannel>)this.innerChannelFactory, this.findCriteria, this.discoveryEndpointProvider));
			}
			if (typeof(TChannel) == typeof(IRequestChannel))
			{
				return (TChannel)((object)new DiscoveryClientRequestChannel<IRequestChannel>(this, (IChannelFactory<IRequestChannel>)this.innerChannelFactory, this.findCriteria, this.discoveryEndpointProvider));
			}
			if (typeof(TChannel) == typeof(IDuplexChannel))
			{
				return (TChannel)((object)new DiscoveryClientDuplexChannel<IDuplexChannel>(this, (IChannelFactory<IDuplexChannel>)this.innerChannelFactory, this.findCriteria, this.discoveryEndpointProvider));
			}
			if (typeof(TChannel) == typeof(IOutputSessionChannel))
			{
				return (TChannel)((object)new DiscoveryClientOutputSessionChannel(this, (IChannelFactory<IOutputSessionChannel>)this.innerChannelFactory, this.findCriteria, this.discoveryEndpointProvider));
			}
			if (typeof(TChannel) == typeof(IRequestSessionChannel))
			{
				return (TChannel)((object)new DiscoveryClientRequestSessionChannel(this, (IChannelFactory<IRequestSessionChannel>)this.innerChannelFactory, this.findCriteria, this.discoveryEndpointProvider));
			}
			if (typeof(TChannel) == typeof(IDuplexSessionChannel))
			{
				return (TChannel)((object)new DiscoveryClientDuplexSessionChannel(this, (IChannelFactory<IDuplexSessionChannel>)this.innerChannelFactory, this.findCriteria, this.discoveryEndpointProvider));
			}
			throw FxTrace.Exception.Argument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
			{
				typeof(TChannel)
			}));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005DDC File Offset: 0x00003FDC
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

		// Token: 0x06000142 RID: 322 RVA: 0x00005E27 File Offset: 0x00004027
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.innerChannelFactory.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005E37 File Offset: 0x00004037
		protected override void OnEndOpen(IAsyncResult result)
		{
			this.innerChannelFactory.EndOpen(result);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00005E45 File Offset: 0x00004045
		protected override void OnOpen(TimeSpan timeout)
		{
			this.innerChannelFactory.Open(timeout);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005E54 File Offset: 0x00004054
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(base.OnBeginClose), new ChainedEndHandler(base.OnEndClose), new ChainedBeginHandler(this.innerChannelFactory.BeginClose), new ChainedEndHandler(this.innerChannelFactory.EndClose));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005EA5 File Offset: 0x000040A5
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00005EB0 File Offset: 0x000040B0
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.OnClose(timeoutHelper.RemainingTime());
			this.innerChannelFactory.Close(timeoutHelper.RemainingTime());
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005EE4 File Offset: 0x000040E4
		protected override void OnAbort()
		{
			base.OnAbort();
			this.innerChannelFactory.Abort();
		}

		// Token: 0x0400005B RID: 91
		private DiscoveryEndpointProvider discoveryEndpointProvider;

		// Token: 0x0400005C RID: 92
		private FindCriteria findCriteria;

		// Token: 0x0400005D RID: 93
		private IChannelFactory<TChannel> innerChannelFactory;
	}
}
