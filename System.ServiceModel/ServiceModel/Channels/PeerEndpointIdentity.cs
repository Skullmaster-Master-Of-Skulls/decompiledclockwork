using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A28 RID: 2600
	internal class PeerEndpointIdentity : EndpointIdentity
	{
		// Token: 0x06006745 RID: 26437 RVA: 0x00181B06 File Offset: 0x0017FD06
		public PeerEndpointIdentity()
		{
			base.Initialize(PeerIdentityClaim.Claim());
		}
	}
}
