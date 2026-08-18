using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000013 RID: 19
	public sealed class DiscoveryClientBindingElement : BindingElement
	{
		// Token: 0x0600011D RID: 285 RVA: 0x0000510C File Offset: 0x0000330C
		public DiscoveryClientBindingElement()
		{
			this.FindCriteria = new FindCriteria();
			this.DiscoveryEndpointProvider = new UdpDiscoveryEndpointProvider();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000512A File Offset: 0x0000332A
		public DiscoveryClientBindingElement(DiscoveryEndpointProvider discoveryEndpointProvider, FindCriteria findCriteria)
		{
			if (discoveryEndpointProvider == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryEndpointProvider");
			}
			if (findCriteria == null)
			{
				throw FxTrace.Exception.ArgumentNull("findCriteria");
			}
			this.findCriteria = findCriteria;
			this.discoveryEndpointProvider = discoveryEndpointProvider;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005166 File Offset: 0x00003366
		private DiscoveryClientBindingElement(DiscoveryClientBindingElement elementToBeCloned) : base(elementToBeCloned)
		{
			this.discoveryEndpointProvider = elementToBeCloned.DiscoveryEndpointProvider;
			this.findCriteria = elementToBeCloned.FindCriteria.Clone();
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000518C File Offset: 0x0000338C
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00005194 File Offset: 0x00003394
		public DiscoveryEndpointProvider DiscoveryEndpointProvider
		{
			get
			{
				return this.discoveryEndpointProvider;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.discoveryEndpointProvider = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000051B0 File Offset: 0x000033B0
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000051B8 File Offset: 0x000033B8
		public FindCriteria FindCriteria
		{
			get
			{
				return this.findCriteria;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.findCriteria = value;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000051D4 File Offset: 0x000033D4
		public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw FxTrace.Exception.ArgumentNull("context");
			}
			return (typeof(TChannel) == typeof(IOutputChannel) || typeof(TChannel) == typeof(IDuplexChannel) || typeof(TChannel) == typeof(IRequestChannel) || typeof(TChannel) == typeof(IOutputSessionChannel) || typeof(TChannel) == typeof(IRequestSessionChannel) || typeof(TChannel) == typeof(IDuplexSessionChannel)) && context.CanBuildInnerChannelFactory<TChannel>();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000052A4 File Offset: 0x000034A4
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw FxTrace.Exception.ArgumentNull("context");
			}
			if (context.Binding.Elements.IndexOf(this) != 0)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryClientBindingElementNotFirst));
			}
			if (this.CanBuildChannelFactory<TChannel>(context))
			{
				return new DiscoveryClientChannelFactory<TChannel>(context.BuildInnerChannelFactory<TChannel>(), this.FindCriteria, this.DiscoveryEndpointProvider);
			}
			throw FxTrace.Exception.Argument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
			{
				typeof(TChannel)
			}));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00005339 File Offset: 0x00003539
		public override bool CanBuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw FxTrace.Exception.ArgumentNull("context");
			}
			return false;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00005350 File Offset: 0x00003550
		public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
		{
			if (context == null)
			{
				throw FxTrace.Exception.ArgumentNull("context");
			}
			throw FxTrace.Exception.Argument("TChannel", SR.GetString("ChannelTypeNotSupported", new object[]
			{
				typeof(TChannel)
			}));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000539C File Offset: 0x0000359C
		public override BindingElement Clone()
		{
			return new DiscoveryClientBindingElement(this);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000053A4 File Offset: 0x000035A4
		public override T GetProperty<T>(BindingContext context)
		{
			if (context == null)
			{
				throw FxTrace.Exception.ArgumentNull("context");
			}
			return context.GetInnerProperty<T>();
		}

		// Token: 0x0400004D RID: 77
		public static readonly EndpointAddress DiscoveryEndpointAddress = new EndpointAddress("http://schemas.microsoft.com/discovery/dynamic");

		// Token: 0x0400004E RID: 78
		private DiscoveryEndpointProvider discoveryEndpointProvider;

		// Token: 0x0400004F RID: 79
		private FindCriteria findCriteria;
	}
}
