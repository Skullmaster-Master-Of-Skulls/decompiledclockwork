using System;
using System.Collections.ObjectModel;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200006E RID: 110
	[MessageContract(IsWrapped = false)]
	internal class ProbeMatchesMessageCD1
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x00006351 File Offset: 0x00004551
		private ProbeMatchesMessageCD1()
		{
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000FE24 File Offset: 0x0000E024
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0000FE2C File Offset: 0x0000E02C
		[MessageHeader(Name = "AppSequence", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public DiscoveryMessageSequenceCD1 MessageSequence { get; private set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000FE35 File Offset: 0x0000E035
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0000FE3D File Offset: 0x0000E03D
		[MessageBodyMember(Name = "ProbeMatches", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public ProbeMatchesCD1 ProbeMatches { get; private set; }

		// Token: 0x06000569 RID: 1385 RVA: 0x0000FE46 File Offset: 0x0000E046
		public static ProbeMatchesMessageCD1 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ProbeMatchesMessageCD1
			{
				MessageSequence = DiscoveryMessageSequenceCD1.FromDiscoveryMessageSequence(messageSequence),
				ProbeMatches = ProbeMatchesCD1.Create(endpointDiscoveryMetadata)
			};
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0000FE65 File Offset: 0x0000E065
		public static ProbeMatchesMessageCD1 Create(DiscoveryMessageSequence messageSequence, Collection<EndpointDiscoveryMetadata> endpointDiscoveryMetadatas)
		{
			return new ProbeMatchesMessageCD1
			{
				MessageSequence = DiscoveryMessageSequenceCD1.FromDiscoveryMessageSequence(messageSequence),
				ProbeMatches = ProbeMatchesCD1.Create(endpointDiscoveryMetadatas)
			};
		}
	}
}
