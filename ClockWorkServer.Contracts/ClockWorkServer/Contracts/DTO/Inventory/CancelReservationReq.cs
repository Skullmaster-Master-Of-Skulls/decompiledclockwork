using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005C4 RID: 1476
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelReservationReq : BaseMessageReq
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x0000DD81 File Offset: 0x0000BF81
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x0000DD89 File Offset: 0x0000BF89
		[DataMember]
		public int ReservationId { get; set; }
	}
}
