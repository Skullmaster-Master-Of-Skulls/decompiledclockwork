using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x0200005E RID: 94
	[MessageContract(IsWrapped = false)]
	internal class ByeMessageCD1
	{
		// Token: 0x060004E6 RID: 1254 RVA: 0x00006351 File Offset: 0x00004551
		private ByeMessageCD1()
		{
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000F19B File Offset: 0x0000D39B
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0000F1A3 File Offset: 0x0000D3A3
		[MessageHeader(Name = "AppSequence", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public DiscoveryMessageSequenceCD1 MessageSequence { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000F1AC File Offset: 0x0000D3AC
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		[MessageBodyMember(Name = "Bye", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public EndpointDiscoveryMetadataCD1 Bye { get; private set; }

		// Token: 0x060004EB RID: 1259 RVA: 0x0000F1BD File Offset: 0x0000D3BD
		public static ByeMessageCD1 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ByeMessageCD1
			{
				MessageSequence = DiscoveryMessageSequenceCD1.FromDiscoveryMessageSequence(messageSequence),
				Bye = EndpointDiscoveryMetadataCD1.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}
	}
}
