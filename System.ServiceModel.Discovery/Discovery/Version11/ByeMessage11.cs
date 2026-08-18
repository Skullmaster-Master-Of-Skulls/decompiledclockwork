using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000090 RID: 144
	[MessageContract(IsWrapped = false)]
	internal class ByeMessage11
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x00006351 File Offset: 0x00004551
		private ByeMessage11()
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0001146F File Offset: 0x0000F66F
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00011477 File Offset: 0x0000F677
		[MessageHeader(Name = "AppSequence", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public DiscoveryMessageSequence11 MessageSequence { get; private set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00011480 File Offset: 0x0000F680
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x00011488 File Offset: 0x0000F688
		[MessageBodyMember(Name = "Bye", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public EndpointDiscoveryMetadata11 Bye { get; private set; }

		// Token: 0x0600066B RID: 1643 RVA: 0x00011491 File Offset: 0x0000F691
		public static ByeMessage11 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ByeMessage11
			{
				MessageSequence = DiscoveryMessageSequence11.FromDiscoveryMessageSequence(messageSequence),
				Bye = EndpointDiscoveryMetadata11.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}
	}
}
