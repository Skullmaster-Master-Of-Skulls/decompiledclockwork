using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x0200018B RID: 395
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkStudentCantFindAvailabilityReq : BaseReportMessageReq
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000956 RID: 2390 RVA: 0x000042C8 File Offset: 0x000024C8
		// (set) Token: 0x06000957 RID: 2391 RVA: 0x000042D0 File Offset: 0x000024D0
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000958 RID: 2392 RVA: 0x000042D9 File Offset: 0x000024D9
		// (set) Token: 0x06000959 RID: 2393 RVA: 0x000042E1 File Offset: 0x000024E1
		[DataMember]
		public IList<int> TutorPids { get; set; }
	}
}
