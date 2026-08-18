using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Startup
{
	// Token: 0x02000265 RID: 613
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCacheClusterFullResp
	{
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x00006A73 File Offset: 0x00004C73
		// (set) Token: 0x06000E26 RID: 3622 RVA: 0x00006A7B File Offset: 0x00004C7B
		[DataMember]
		public CacheClusterFullDTO Info { get; set; }
	}
}
