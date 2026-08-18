using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005C2 RID: 1474
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkReservationAsCompletedReq : BaseMessageReq
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x0000DD70 File Offset: 0x0000BF70
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x0000DD78 File Offset: 0x0000BF78
		[DataMember]
		public int ReservationId { get; set; }
	}
}
