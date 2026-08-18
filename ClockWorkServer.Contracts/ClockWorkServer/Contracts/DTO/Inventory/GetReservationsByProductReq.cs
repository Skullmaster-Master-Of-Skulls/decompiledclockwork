using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B4 RID: 1460
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByProductReq : BaseMessageReq
	{
		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0000DBFA File Offset: 0x0000BDFA
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x0000DC02 File Offset: 0x0000BE02
		[DataMember]
		public string ProductUniqueId { get; set; }
	}
}
