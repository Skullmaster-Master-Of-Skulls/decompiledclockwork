using System;

namespace System.ServiceModel.Discovery.VersionCD1
{
	// Token: 0x02000074 RID: 116
	[MessageContract(IsWrapped = false)]
	internal class ResolveMatchesMessageCD1
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x00006351 File Offset: 0x00004551
		private ResolveMatchesMessageCD1()
		{
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000FFF3 File Offset: 0x0000E1F3
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x0000FFFB File Offset: 0x0000E1FB
		[MessageHeader(Name = "AppSequence", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public DiscoveryMessageSequenceCD1 MessageSequence { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x00010004 File Offset: 0x0000E204
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x0001000C File Offset: 0x0000E20C
		[MessageBodyMember(Name = "ResolveMatches", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2008/09")]
		public ResolveMatchesCD1 ResolveMatches { get; private set; }

		// Token: 0x0600058C RID: 1420 RVA: 0x00010015 File Offset: 0x0000E215
		public static ResolveMatchesMessageCD1 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ResolveMatchesMessageCD1
			{
				MessageSequence = DiscoveryMessageSequenceCD1.FromDiscoveryMessageSequence(messageSequence),
				ResolveMatches = ResolveMatchesCD1.Create(endpointDiscoveryMetadata)
			};
		}
	}
}
