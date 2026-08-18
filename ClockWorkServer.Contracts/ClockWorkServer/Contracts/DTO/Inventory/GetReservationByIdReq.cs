using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B2 RID: 1458
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationByIdReq : BaseMessageReq
	{
		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		[DataMember]
		public int ReservationId { get; set; }
	}
}
