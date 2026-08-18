using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000564 RID: 1380
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateLocationResp
	{
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0000D0C3 File Offset: 0x0000B2C3
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0000D0CB File Offset: 0x0000B2CB
		[DataMember]
		public int LocationId { get; set; }
	}
}
