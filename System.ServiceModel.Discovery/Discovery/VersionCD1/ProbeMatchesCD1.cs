using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200006D RID: 109
	[CollectionDataContract(ItemName = "ProbeMatch", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
	internal class ProbeMatchesCD1 : Collection<EndpointDiscoveryMetadataCD1>
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0000FDB1 File Offset: 0x0000DFB1
		private ProbeMatchesCD1()
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000FDB9 File Offset: 0x0000DFB9
		public static ProbeMatchesCD1 Create(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ProbeMatchesCD1
			{
				EndpointDiscoveryMetadataCD1.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public static ProbeMatchesCD1 Create(Collection<EndpointDiscoveryMetadata> endpointDiscoveryMetadatas)
		{
			ProbeMatchesCD1 probeMatchesCD = new ProbeMatchesCD1();
			if (endpointDiscoveryMetadatas != null)
			{
				foreach (EndpointDiscoveryMetadata endpointDiscoveryMetadata in endpointDiscoveryMetadatas)
				{
					probeMatchesCD.Add(EndpointDiscoveryMetadataCD1.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata));
				}
			}
			return probeMatchesCD;
		}
	}
}
