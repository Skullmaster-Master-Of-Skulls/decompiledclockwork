using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C48 RID: 3144
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllCompletedStudentMediaRequestByStudentReq : BaseReportMessageReq
	{
		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x060041B2 RID: 16818 RVA: 0x000201E6 File Offset: 0x0001E3E6
		// (set) Token: 0x060041B3 RID: 16819 RVA: 0x000201EE File Offset: 0x0001E3EE
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x060041B4 RID: 16820 RVA: 0x000201F7 File Offset: 0x0001E3F7
		// (set) Token: 0x060041B5 RID: 16821 RVA: 0x000201FF File Offset: 0x0001E3FF
		[DataMember]
		public int CampusId { get; set; }
	}
}
