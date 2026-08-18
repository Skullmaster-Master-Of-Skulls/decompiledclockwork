using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000073 RID: 115
	[DataContract(Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
	internal class ResolveMatchesCD1
	{
		// Token: 0x06000583 RID: 1411 RVA: 0x00006351 File Offset: 0x00004551
		private ResolveMatchesCD1()
		{
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000FFBF File Offset: 0x0000E1BF
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0000FFC7 File Offset: 0x0000E1C7
		[DataMember(EmitDefaultValue = false, Name = "ResolveMatch")]
		public EndpointDiscoveryMetadataCD1 ResolveMatch { get; private set; }

		// Token: 0x06000586 RID: 1414 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		public static ResolveMatchesCD1 Create(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			ResolveMatchesCD1 resolveMatchesCD = new ResolveMatchesCD1();
			if (endpointDiscoveryMetadata != null)
			{
				resolveMatchesCD.ResolveMatch = EndpointDiscoveryMetadataCD1.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata);
			}
			return resolveMatchesCD;
		}
	}
}
