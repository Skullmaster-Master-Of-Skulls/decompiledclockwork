using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000565 RID: 1381
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetLocationByIdReq : BaseMessageReq
	{
		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		// (set) Token: 0x06001C89 RID: 7305 RVA: 0x0000D0DC File Offset: 0x0000B2DC
		[DataMember]
		public int LocationId { get; set; }
	}
}
