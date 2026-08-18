using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C49 RID: 3145
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllCompletedStudentMediaRequestByStudentAndDateReq : BaseReportMessageReq
	{
		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x060041B7 RID: 16823 RVA: 0x00020208 File Offset: 0x0001E408
		// (set) Token: 0x060041B8 RID: 16824 RVA: 0x00020210 File Offset: 0x0001E410
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x060041B9 RID: 16825 RVA: 0x00020219 File Offset: 0x0001E419
		// (set) Token: 0x060041BA RID: 16826 RVA: 0x00020221 File Offset: 0x0001E421
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x060041BB RID: 16827 RVA: 0x0002022A File Offset: 0x0001E42A
		// (set) Token: 0x060041BC RID: 16828 RVA: 0x00020232 File Offset: 0x0001E432
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x060041BD RID: 16829 RVA: 0x0002023B File Offset: 0x0001E43B
		// (set) Token: 0x060041BE RID: 16830 RVA: 0x00020243 File Offset: 0x0001E443
		[DataMember]
		public int CampusId { get; set; }
	}
}
