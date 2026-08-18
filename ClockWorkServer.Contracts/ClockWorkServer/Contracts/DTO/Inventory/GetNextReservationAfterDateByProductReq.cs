using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005BE RID: 1470
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNextReservationAfterDateByProductReq : BaseMessageReq
	{
		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x0000DD0A File Offset: 0x0000BF0A
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x0000DD12 File Offset: 0x0000BF12
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x0000DD1B File Offset: 0x0000BF1B
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x0000DD23 File Offset: 0x0000BF23
		[DataMember]
		public DateTime Date { get; set; }
	}
}
