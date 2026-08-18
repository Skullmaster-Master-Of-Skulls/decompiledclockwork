using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200001D RID: 29
	public class DiscoveryEndpoint : ServiceEndpoint
	{
		// Token: 0x06000170 RID: 368 RVA: 0x0000625C File Offset: 0x0000445C
		public DiscoveryEndpoint() : this(DiscoveryVersion.DefaultDiscoveryVersion, ServiceDiscoveryMode.Managed)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000626A File Offset: 0x0000446A
		public DiscoveryEndpoint(Binding binding, EndpointAddress endpointAddress) : this(DiscoveryVersion.DefaultDiscoveryVersion, ServiceDiscoveryMode.Managed, binding, endpointAddress)
		{
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000627A File Offset: 0x0000447A
		public DiscoveryEndpoint(DiscoveryVersion discoveryVersion, ServiceDiscoveryMode discoveryMode) : this(discoveryVersion, discoveryMode, null, null)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006288 File Offset: 0x00004488
		public DiscoveryEndpoint(DiscoveryVersion discoveryVersion, ServiceDiscoveryMode discoveryMode, Binding binding, EndpointAddress endpointAddress) : base(DiscoveryEndpoint.GetDiscoveryContract(discoveryVersion, discoveryMode))
		{
			base.IsSystemEndpoint = true;
			this.discoveryOperationContextExtension = new DiscoveryOperationContextExtension(TimeSpan.Zero, discoveryMode, discoveryVersion);
			base.Behaviors.Add(new DiscoveryOperationContextExtensionInitializer(this.discoveryOperationContextExtension));
			base.Behaviors.Add(new DiscoveryEndpointValidator());
			base.Address = endpointAddress;
			base.Binding = binding;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000062F0 File Offset: 0x000044F0
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000062FD File Offset: 0x000044FD
		public TimeSpan MaxResponseDelay
		{
			get
			{
				return this.discoveryOperationContextExtension.MaxResponseDelay;
			}
			set
			{
				TimeoutHelper.ThrowIfNegativeArgument(value, "value");
				this.discoveryOperationContextExtension.MaxResponseDelay = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00006316 File Offset: 0x00004516
		public DiscoveryVersion DiscoveryVersion
		{
			get
			{
				return this.discoveryOperationContextExtension.DiscoveryVersion;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00006323 File Offset: 0x00004523
		public ServiceDiscoveryMode DiscoveryMode
		{
			get
			{
				return this.discoveryOperationContextExtension.DiscoveryMode;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006330 File Offset: 0x00004530
		private static ContractDescription GetDiscoveryContract(DiscoveryVersion discoveryVersion, ServiceDiscoveryMode discoveryMode)
		{
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			return discoveryVersion.Implementation.GetDiscoveryContract(discoveryMode);
		}

		// Token: 0x04000062 RID: 98
		private readonly DiscoveryOperationContextExtension discoveryOperationContextExtension;
	}
}
