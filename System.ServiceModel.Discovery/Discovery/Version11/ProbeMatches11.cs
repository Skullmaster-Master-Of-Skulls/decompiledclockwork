using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x0200009F RID: 159
	[CollectionDataContract(ItemName = "ProbeMatch", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
	internal class ProbeMatches11 : Collection<EndpointDiscoveryMetadata11>
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x00012085 File Offset: 0x00010285
		private ProbeMatches11()
		{
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001208D File Offset: 0x0001028D
		public static ProbeMatches11 Create(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ProbeMatches11
			{
				EndpointDiscoveryMetadata11.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000120A0 File Offset: 0x000102A0
		public static ProbeMatches11 Create(Collection<EndpointDiscoveryMetadata> endpointDiscoveryMetadatas)
		{
			ProbeMatches11 probeMatches = new ProbeMatches11();
			if (endpointDiscoveryMetadatas != null)
			{
				foreach (EndpointDiscoveryMetadata endpointDiscoveryMetadata in endpointDiscoveryMetadatas)
				{
					probeMatches.Add(EndpointDiscoveryMetadata11.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata));
				}
			}
			return probeMatches;
		}
	}
}
