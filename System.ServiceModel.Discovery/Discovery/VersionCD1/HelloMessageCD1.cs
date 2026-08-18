using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000066 RID: 102
	[MessageContract(IsWrapped = false)]
	internal class HelloMessageCD1
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x00006351 File Offset: 0x00004551
		private HelloMessageCD1()
		{
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000FCB6 File Offset: 0x0000DEB6
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000FCBE File Offset: 0x0000DEBE
		[MessageHeader(Name = "AppSequence", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public DiscoveryMessageSequenceCD1 MessageSequence { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000FCC7 File Offset: 0x0000DEC7
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000FCCF File Offset: 0x0000DECF
		[MessageBodyMember(Name = "Hello", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public EndpointDiscoveryMetadataCD1 Hello { get; private set; }

		// Token: 0x0600053B RID: 1339 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		public static HelloMessageCD1 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new HelloMessageCD1
			{
				MessageSequence = DiscoveryMessageSequenceCD1.FromDiscoveryMessageSequence(messageSequence),
				Hello = EndpointDiscoveryMetadataCD1.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}
	}
}
