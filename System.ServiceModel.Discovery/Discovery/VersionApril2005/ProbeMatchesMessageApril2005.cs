using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000088 RID: 136
	[MessageContract(IsWrapped = false)]
	internal class ProbeMatchesMessageApril2005
	{
		// Token: 0x06000620 RID: 1568 RVA: 0x00006351 File Offset: 0x00004551
		private ProbeMatchesMessageApril2005()
		{
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00010E5C File Offset: 0x0000F05C
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x00010E64 File Offset: 0x0000F064
		[MessageHeader(Name = "AppSequence", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public DiscoveryMessageSequenceApril2005 MessageSequence { get; private set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x00010E6D File Offset: 0x0000F06D
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x00010E75 File Offset: 0x0000F075
		[MessageBodyMember(Name = "ProbeMatches", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public ProbeMatchesApril2005 ProbeMatches { get; private set; }

		// Token: 0x06000625 RID: 1573 RVA: 0x00010E7E File Offset: 0x0000F07E
		public static ProbeMatchesMessageApril2005 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ProbeMatchesMessageApril2005
			{
				MessageSequence = DiscoveryMessageSequenceApril2005.FromDiscoveryMessageSequence(messageSequence),
				ProbeMatches = ProbeMatchesApril2005.Create(endpointDiscoveryMetadata)
			};
		}
	}
}
