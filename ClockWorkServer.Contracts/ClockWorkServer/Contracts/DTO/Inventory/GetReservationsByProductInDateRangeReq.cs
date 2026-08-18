using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x020005B6 RID: 1462
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReservationsByProductInDateRangeReq : BaseMessageReq
	{
		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0000DC24 File Offset: 0x0000BE24
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x0000DC2D File Offset: 0x0000BE2D
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x0000DC35 File Offset: 0x0000BE35
		[DataMember]
		public int AlternateProductId { get; set; }

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x0000DC3E File Offset: 0x0000BE3E
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x0000DC46 File Offset: 0x0000BE46
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0000DC4F File Offset: 0x0000BE4F
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x0000DC57 File Offset: 0x0000BE57
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
