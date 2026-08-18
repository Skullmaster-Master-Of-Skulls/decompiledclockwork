using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005C1 RID: 1473
	[DataContract(Namespace = "http://tpro.ca")]
	public class MakeReservationResp
	{
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0000DD5F File Offset: 0x0000BF5F
		// (set) Token: 0x06001E5B RID: 7771 RVA: 0x0000DD67 File Offset: 0x0000BF67
		[DataMember]
		public int ReservationId { get; set; }
	}
}
