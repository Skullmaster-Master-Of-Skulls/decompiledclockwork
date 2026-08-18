using System;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000005 RID: 5
	public class AnnouncementEndpoint : ServiceEndpoint
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002DEB File Offset: 0x00000FEB
		public AnnouncementEndpoint() : this(DiscoveryVersion.DefaultDiscoveryVersion)
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public AnnouncementEndpoint(Binding binding, EndpointAddress address) : this(DiscoveryVersion.DefaultDiscoveryVersion, binding, address)
		{
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E07 File Offset: 0x00001007
		public AnnouncementEndpoint(DiscoveryVersion discoveryVersion) : this(discoveryVersion, null, null)
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E12 File Offset: 0x00001012
		public AnnouncementEndpoint(DiscoveryVersion discoveryVersion, Binding binding, EndpointAddress address) : base(AnnouncementEndpoint.GetAnnouncementContract(discoveryVersion))
		{
			base.EndpointBehaviors.Add(new DispatcherSynchronizationBehavior
			{
				AsynchronousSendEnabled = true
			});
			this.discoveryVersion = discoveryVersion;
			base.Address = address;
			base.Binding = binding;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002E4C File Offset: 0x0000104C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002E54 File Offset: 0x00001054
		public TimeSpan MaxAnnouncementDelay
		{
			get
			{
				return this.maxAnnouncementDelay;
			}
			set
			{
				TimeoutHelper.ThrowIfNegativeArgument(value, "value");
				this.maxAnnouncementDelay = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E68 File Offset: 0x00001068
		public DiscoveryVersion DiscoveryVersion
		{
			get
			{
				return this.discoveryVersion;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E70 File Offset: 0x00001070
		private static ContractDescription GetAnnouncementContract(DiscoveryVersion discoveryVersion)
		{
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			return discoveryVersion.Implementation.GetAnnouncementContract();
		}

		// Token: 0x04000015 RID: 21
		private TimeSpan maxAnnouncementDelay;

		// Token: 0x04000016 RID: 22
		private DiscoveryVersion discoveryVersion;
	}
}
