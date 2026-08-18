using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000087 RID: 135
	[CollectionDataContract(ItemName = "ProbeMatch", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
	internal class ProbeMatchesApril2005 : Collection<EndpointDiscoveryMetadataApril2005>
	{
		// Token: 0x0600061D RID: 1565 RVA: 0x00010DE7 File Offset: 0x0000EFE7
		private ProbeMatchesApril2005()
		{
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00010DEF File Offset: 0x0000EFEF
		public static ProbeMatchesApril2005 Create(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ProbeMatchesApril2005
			{
				EndpointDiscoveryMetadataApril2005.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00010E04 File Offset: 0x0000F004
		public static ProbeMatchesApril2005 Create(Collection<EndpointDiscoveryMetadata> endpointDiscoveryMetadatas)
		{
			ProbeMatchesApril2005 probeMatchesApril = new ProbeMatchesApril2005();
			if (endpointDiscoveryMetadatas != null)
			{
				foreach (EndpointDiscoveryMetadata endpointDiscoveryMetadata in endpointDiscoveryMetadatas)
				{
					probeMatchesApril.Add(EndpointDiscoveryMetadataApril2005.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata));
				}
			}
			return probeMatchesApril;
		}
	}
}
