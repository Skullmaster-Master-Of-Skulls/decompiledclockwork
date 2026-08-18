using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace TechnoPro.Common.WCF.Discovery
{
	// Token: 0x02000014 RID: 20
	public static class DiscoveryHelper
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003C78 File Offset: 0x00001E78
		public static IList<EndpointAddress> DiscoverAddresses<T>(int durationInSeconds = 5, Uri scope = null)
		{
			DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
			FindCriteria findCriteria = new FindCriteria(typeof(T))
			{
				Duration = TimeSpan.FromSeconds((double)durationInSeconds)
			};
			bool flag = scope != null;
			if (flag)
			{
				findCriteria.Scopes.Add(scope);
			}
			FindResponse findResponse = discoveryClient.Find(findCriteria);
			return (from e in findResponse.Endpoints
			select e.Address).ToList<EndpointAddress>();
		}
	}
}
