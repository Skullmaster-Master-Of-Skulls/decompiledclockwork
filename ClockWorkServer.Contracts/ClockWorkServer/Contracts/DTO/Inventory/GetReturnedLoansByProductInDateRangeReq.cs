using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Inventory
{
	// Token: 0x02000552 RID: 1362
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetReturnedLoansByProductInDateRangeReq : BaseMessageReq
	{
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0000CF09 File Offset: 0x0000B109
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x0000CF11 File Offset: 0x0000B111
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0000CF1A File Offset: 0x0000B11A
		// (set) Token: 0x06001C42 RID: 7234 RVA: 0x0000CF22 File Offset: 0x0000B122
		[DataMember]
		public int AlternateProductId { get; set; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x0000CF2B File Offset: 0x0000B12B
		// (set) Token: 0x06001C44 RID: 7236 RVA: 0x0000CF33 File Offset: 0x0000B133
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x0000CF3C File Offset: 0x0000B13C
		// (set) Token: 0x06001C46 RID: 7238 RVA: 0x0000CF44 File Offset: 0x0000B144
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
