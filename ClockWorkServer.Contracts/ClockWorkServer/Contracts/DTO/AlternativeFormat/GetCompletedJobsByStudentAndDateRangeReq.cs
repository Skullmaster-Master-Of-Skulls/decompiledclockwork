using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BCE RID: 3022
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetCompletedJobsByStudentAndDateRangeReq : BaseMessageReq
	{
		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x06003FBC RID: 16316 RVA: 0x0001F548 File Offset: 0x0001D748
		// (set) Token: 0x06003FBD RID: 16317 RVA: 0x0001F550 File Offset: 0x0001D750
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x06003FBE RID: 16318 RVA: 0x0001F559 File Offset: 0x0001D759
		// (set) Token: 0x06003FBF RID: 16319 RVA: 0x0001F561 File Offset: 0x0001D761
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x06003FC0 RID: 16320 RVA: 0x0001F56A File Offset: 0x0001D76A
		// (set) Token: 0x06003FC1 RID: 16321 RVA: 0x0001F572 File Offset: 0x0001D772
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x06003FC2 RID: 16322 RVA: 0x0001F57B File Offset: 0x0001D77B
		// (set) Token: 0x06003FC3 RID: 16323 RVA: 0x0001F583 File Offset: 0x0001D783
		[DataMember]
		public int CampusId { get; set; }
	}
}
