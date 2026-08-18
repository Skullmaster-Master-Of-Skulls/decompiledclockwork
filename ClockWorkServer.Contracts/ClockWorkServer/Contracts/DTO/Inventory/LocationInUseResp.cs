using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x0200056C RID: 1388
	[DataContract(Namespace = "http://tpro.ca")]
	public class LocationInUseResp
	{
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x0000D13A File Offset: 0x0000B33A
		// (set) Token: 0x06001C9C RID: 7324 RVA: 0x0000D142 File Offset: 0x0000B342
		[DataMember]
		public bool InUse { get; set; }
	}
}
