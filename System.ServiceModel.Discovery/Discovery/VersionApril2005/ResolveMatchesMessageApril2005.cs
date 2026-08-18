using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200008D RID: 141
	[MessageContract(IsWrapped = false)]
	internal class ResolveMatchesMessageApril2005
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x00006351 File Offset: 0x00004551
		private ResolveMatchesMessageApril2005()
		{
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x00010FDB File Offset: 0x0000F1DB
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x00010FE3 File Offset: 0x0000F1E3
		[MessageHeader(Name = "AppSequence", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public DiscoveryMessageSequenceApril2005 MessageSequence { get; private set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x00010FEC File Offset: 0x0000F1EC
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		[MessageBodyMember(Name = "ResolveMatches", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public ResolveMatchesApril2005 ResolveMatches { get; private set; }

		// Token: 0x06000642 RID: 1602 RVA: 0x00010FFD File Offset: 0x0000F1FD
		public static ResolveMatchesMessageApril2005 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ResolveMatchesMessageApril2005
			{
				MessageSequence = DiscoveryMessageSequenceApril2005.FromDiscoveryMessageSequence(messageSequence),
				ResolveMatches = ResolveMatchesApril2005.Create(endpointDiscoveryMetadata)
			};
		}
	}
}
