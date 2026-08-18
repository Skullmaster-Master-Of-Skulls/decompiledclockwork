using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200018A RID: 394
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkStudentCantFindTutorReq : BaseReportMessageReq
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600094D RID: 2381 RVA: 0x0000427B File Offset: 0x0000247B
		// (set) Token: 0x0600094E RID: 2382 RVA: 0x00004283 File Offset: 0x00002483
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600094F RID: 2383 RVA: 0x0000428C File Offset: 0x0000248C
		// (set) Token: 0x06000950 RID: 2384 RVA: 0x00004294 File Offset: 0x00002494
		[DataMember]
		public int SearchLucid { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0000429D File Offset: 0x0000249D
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x000042A5 File Offset: 0x000024A5
		[DataMember]
		public string SearchLuc { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x000042AE File Offset: 0x000024AE
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x000042B6 File Offset: 0x000024B6
		[DataMember]
		public string SearchString { get; set; }
	}
}
