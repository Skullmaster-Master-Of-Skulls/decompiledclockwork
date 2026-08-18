using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000078 RID: 120
	[MessageContract(IsWrapped = false)]
	internal class ByeMessageApril2005
	{
		// Token: 0x060005B5 RID: 1461 RVA: 0x00006351 File Offset: 0x00004551
		private ByeMessageApril2005()
		{
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x000104E1 File Offset: 0x0000E6E1
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x000104E9 File Offset: 0x0000E6E9
		[MessageHeader(Name = "AppSequence", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public DiscoveryMessageSequenceApril2005 MessageSequence { get; private set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x000104F2 File Offset: 0x0000E6F2
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x000104FA File Offset: 0x0000E6FA
		[MessageBodyMember(Name = "Bye", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public EndpointDiscoveryMetadataApril2005 Bye { get; private set; }

		// Token: 0x060005BA RID: 1466 RVA: 0x00010503 File Offset: 0x0000E703
		internal static ByeMessageApril2005 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ByeMessageApril2005
			{
				MessageSequence = DiscoveryMessageSequenceApril2005.FromDiscoveryMessageSequence(messageSequence),
				Bye = EndpointDiscoveryMetadataApril2005.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}
	}
}
