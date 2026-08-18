using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200007F RID: 127
	[MessageContract(IsWrapped = false)]
	internal class HelloMessageApril2005
	{
		// Token: 0x060005F8 RID: 1528 RVA: 0x00006351 File Offset: 0x00004551
		private HelloMessageApril2005()
		{
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x00010CE2 File Offset: 0x0000EEE2
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x00010CEA File Offset: 0x0000EEEA
		[MessageHeader(Name = "AppSequence", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public DiscoveryMessageSequenceApril2005 MessageSequence { get; private set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00010CF3 File Offset: 0x0000EEF3
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x00010CFB File Offset: 0x0000EEFB
		[MessageBodyMember(Name = "Hello", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public EndpointDiscoveryMetadataApril2005 Hello { get; private set; }

		// Token: 0x060005FD RID: 1533 RVA: 0x00010D04 File Offset: 0x0000EF04
		public static HelloMessageApril2005 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new HelloMessageApril2005
			{
				MessageSequence = DiscoveryMessageSequenceApril2005.FromDiscoveryMessageSequence(messageSequence),
				Hello = EndpointDiscoveryMetadataApril2005.FromEndpointDiscoveryMetadata(endpointDiscoveryMetadata)
			};
		}
	}
}
