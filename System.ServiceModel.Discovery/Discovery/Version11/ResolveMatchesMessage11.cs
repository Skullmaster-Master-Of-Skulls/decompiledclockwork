using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A6 RID: 166
	[MessageContract(IsWrapped = false)]
	internal class ResolveMatchesMessage11
	{
		// Token: 0x06000707 RID: 1799 RVA: 0x00006351 File Offset: 0x00004551
		private ResolveMatchesMessage11()
		{
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x000122C7 File Offset: 0x000104C7
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x000122CF File Offset: 0x000104CF
		[MessageHeader(Name = "AppSequence", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public DiscoveryMessageSequence11 MessageSequence { get; private set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x000122D8 File Offset: 0x000104D8
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x000122E0 File Offset: 0x000104E0
		[MessageBodyMember(Name = "ResolveMatches", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public ResolveMatches11 ResolveMatches { get; private set; }

		// Token: 0x0600070C RID: 1804 RVA: 0x000122E9 File Offset: 0x000104E9
		public static ResolveMatchesMessage11 Create(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata)
		{
			return new ResolveMatchesMessage11
			{
				MessageSequence = DiscoveryMessageSequence11.FromDiscoveryMessageSequence(messageSequence),
				ResolveMatches = ResolveMatches11.Create(endpointDiscoveryMetadata)
			};
		}
	}
}
