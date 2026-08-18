using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200056B RID: 1387
	[DataContract(Namespace = "http://tpro.ca")]
	public class LocationInUseReq : BaseMessageReq
	{
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x0000D129 File Offset: 0x0000B329
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x0000D131 File Offset: 0x0000B331
		[DataMember]
		public int LocationId { get; set; }
	}
}
