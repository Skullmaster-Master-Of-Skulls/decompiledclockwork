using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C47 RID: 3143
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllCompletedStudentMediaRequestByDateReq : BaseReportMessageReq
	{
		// Token: 0x17001839 RID: 6201
		// (get) Token: 0x060041AB RID: 16811 RVA: 0x000201B3 File Offset: 0x0001E3B3
		// (set) Token: 0x060041AC RID: 16812 RVA: 0x000201BB File Offset: 0x0001E3BB
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x000201C4 File Offset: 0x0001E3C4
		// (set) Token: 0x060041AE RID: 16814 RVA: 0x000201CC File Offset: 0x0001E3CC
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x000201D5 File Offset: 0x0001E3D5
		// (set) Token: 0x060041B0 RID: 16816 RVA: 0x000201DD File Offset: 0x0001E3DD
		[DataMember]
		public int CampusId { get; set; }
	}
}
