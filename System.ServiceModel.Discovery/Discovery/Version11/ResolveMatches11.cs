using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A5 RID: 165
	[DataContract(Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
	internal class ResolveMatches11
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x00006351 File Offset: 0x00004551
		private ResolveMatches11()
		{
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00012293 File Offset: 0x00010493
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x0001229B File Offset: 0x0001049B
		[DataMember(EmitDefaultValue = false, Name = "ResolveMatch")]
		public EndpointDiscoveryMetadata11 ResolveMatch { get; private set; }

		// Token: 0x06000706 RID: 1798 RVA: 0x000122A4 File Offset: 0x000104A4
		public static ResolveMatches11 Create(EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			ResolveMatches11 resolveMatches = new ResolveMatches11();
			if (endpointDiscoveryMetadata != null)
			{
				resolveMatches.ResolveMatch = EndpointDiscoveryMetadata11.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata);
			}
			return resolveMatches;
		}
	}
}
