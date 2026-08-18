using System;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000056 RID: 86
	internal class UdpDiscoveryEndpointProvider : DiscoveryEndpointProvider
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public override DiscoveryEndpoint GetDiscoveryEndpoint()
		{
			return new UdpDiscoveryEndpoint();
		}
	}
}
