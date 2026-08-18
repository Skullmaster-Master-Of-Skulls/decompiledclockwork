using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000028 RID: 40
	public abstract class DiscoveryServiceExtension : IExtension<ServiceHostBase>
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00006E7E File Offset: 0x0000507E
		protected DiscoveryServiceExtension()
		{
			this.publishedEndpoints = new DiscoveryServiceExtension.PublishedEndpointCollection();
			this.readOnlyPublishedEndpoints = new ReadOnlyCollection<EndpointDiscoveryMetadata>(this.publishedEndpoints);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00006EA2 File Offset: 0x000050A2
		public ReadOnlyCollection<EndpointDiscoveryMetadata> PublishedEndpoints
		{
			get
			{
				return this.readOnlyPublishedEndpoints;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00006EAA File Offset: 0x000050AA
		internal Collection<EndpointDiscoveryMetadata> InternalPublishedEndpoints
		{
			get
			{
				return this.publishedEndpoints;
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006EB2 File Offset: 0x000050B2
		void IExtension<ServiceHostBase>.Attach(ServiceHostBase owner)
		{
			if (owner == null)
			{
				throw FxTrace.Exception.ArgumentNull("owner");
			}
			if (this.owner != null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryExtensionAlreadyAttached));
			}
			this.owner = owner;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00006EEB File Offset: 0x000050EB
		void IExtension<ServiceHostBase>.Detach(ServiceHostBase owner)
		{
			if (owner == null)
			{
				throw FxTrace.Exception.ArgumentNull("owner");
			}
			if (this.owner != null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryExtensionCannotBeDetached));
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006F20 File Offset: 0x00005120
		internal DiscoveryService ValidateAndGetDiscoveryService()
		{
			DiscoveryService discoveryService = this.GetDiscoveryService();
			if (discoveryService == null)
			{
				throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryMethodImplementationReturnsNull("GetDiscoveryService", base.GetType())));
			}
			return discoveryService;
		}

		// Token: 0x06000238 RID: 568
		protected abstract DiscoveryService GetDiscoveryService();

		// Token: 0x0400007A RID: 122
		private ServiceHostBase owner;

		// Token: 0x0400007B RID: 123
		private DiscoveryServiceExtension.PublishedEndpointCollection publishedEndpoints;

		// Token: 0x0400007C RID: 124
		private ReadOnlyCollection<EndpointDiscoveryMetadata> readOnlyPublishedEndpoints;

		// Token: 0x020000CE RID: 206
		private class PublishedEndpointCollection : NonNullItemCollection<EndpointDiscoveryMetadata>
		{
			// Token: 0x060007E8 RID: 2024 RVA: 0x00014C71 File Offset: 0x00012E71
			protected override void InsertItem(int index, EndpointDiscoveryMetadata item)
			{
				base.InsertItem(index, item);
				item.Open();
			}

			// Token: 0x060007E9 RID: 2025 RVA: 0x00014C81 File Offset: 0x00012E81
			protected override void SetItem(int index, EndpointDiscoveryMetadata item)
			{
				base.SetItem(index, item);
				item.Open();
			}
		}
	}
}
