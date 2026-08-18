using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200008C RID: 140
	[DataContract(Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
	internal class ResolveMatchesApril2005
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x00006351 File Offset: 0x00004551
		private ResolveMatchesApril2005()
		{
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00010FA6 File Offset: 0x0000F1A6
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00010FAE File Offset: 0x0000F1AE
		[DataMember(EmitDefaultValue = false, Name = "ResolveMatch")]
		public EndpointDiscoveryMetadataApril2005 ResolveMatch { get; private set; }

		// Token: 0x0600063C RID: 1596 RVA: 0x00010FB8 File Offset: 0x0000F1B8
		public static ResolveMatchesApril2005 Create(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			ResolveMatchesApril2005 resolveMatchesApril = new ResolveMatchesApril2005();
			if (endpointDiscoveryMetadata != null)
			{
				resolveMatchesApril.ResolveMatch = EndpointDiscoveryMetadataApril2005.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata);
			}
			return resolveMatchesApril;
		}
	}
}
