using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x02000098 RID: 152
	[MessageContract(IsWrapped = false)]
	internal class HelloMessage11
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x00006351 File Offset: 0x00004551
		private HelloMessage11()
		{
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00011F8A File Offset: 0x0001018A
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00011F92 File Offset: 0x00010192
		[MessageHeader(Name = "AppSequence", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public DiscoveryMessageSequence11 MessageSequence { get; private set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00011F9B File Offset: 0x0001019B
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x00011FA3 File Offset: 0x000101A3
		[MessageBodyMember(Name = "Hello", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public EndpointDiscoveryMetadata11 Hello { get; private set; }

		// Token: 0x060006BB RID: 1723 RVA: 0x00011FAC File Offset: 0x000101AC
		public static HelloMessage11 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new HelloMessage11
			{
				MessageSequence = DiscoveryMessageSequence11.FromDiscoveryMessageSequence(messageSequence),
				Hello = EndpointDiscoveryMetadata11.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}
	}
}
