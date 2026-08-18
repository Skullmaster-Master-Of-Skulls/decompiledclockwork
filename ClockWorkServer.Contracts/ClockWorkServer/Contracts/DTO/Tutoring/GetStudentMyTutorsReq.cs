using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200018C RID: 396
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetStudentMyTutorsReq : BaseReportMessageReq
	{
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x000042EA File Offset: 0x000024EA
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x000042F2 File Offset: 0x000024F2
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x000042FB File Offset: 0x000024FB
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x00004303 File Offset: 0x00002503
		[DataMember]
		public DateTime? StartDateTime { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0000430C File Offset: 0x0000250C
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x00004314 File Offset: 0x00002514
		[DataMember]
		public DateTime? EndDate { get; set; }
	}
}
