using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A0 RID: 160
	[MessageContract(IsWrapped = false)]
	internal class ProbeMatchesMessage11
	{
		// Token: 0x060006E4 RID: 1764 RVA: 0x00006351 File Offset: 0x00004551
		private ProbeMatchesMessage11()
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x000120F8 File Offset: 0x000102F8
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00012100 File Offset: 0x00010300
		[MessageHeader(Name = "AppSequence", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public DiscoveryMessageSequence11 MessageSequence { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x00012109 File Offset: 0x00010309
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x00012111 File Offset: 0x00010311
		[MessageBodyMember(Name = "ProbeMatches", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public ProbeMatches11 ProbeMatches { get; private set; }

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001211A File Offset: 0x0001031A
		public static ProbeMatchesMessage11 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ProbeMatchesMessage11
			{
				MessageSequence = DiscoveryMessageSequence11.FromDiscoveryMessageSequence(messageSequence),
				ProbeMatches = ProbeMatches11.Create(endpointDiscoveryMetadata)
			};
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00012139 File Offset: 0x00010339
		public static ProbeMatchesMessage11 Create(DiscoveryMessageSequence messageSequence, Collection<EndpointDiscoveryMetadata> endpointDiscoveryMetadatas)
		{
			return new ProbeMatchesMessage11
			{
				MessageSequence = DiscoveryMessageSequence11.FromDiscoveryMessageSequence(messageSequence),
				ProbeMatches = ProbeMatches11.Create(endpointDiscoveryMetadatas)
			};
		}
	}
}
