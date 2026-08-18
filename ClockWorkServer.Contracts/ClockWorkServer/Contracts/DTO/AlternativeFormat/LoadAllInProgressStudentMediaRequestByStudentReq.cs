using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C3E RID: 3134
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllInProgressStudentMediaRequestByStudentReq : BaseReportMessageReq
	{
		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x0600418A RID: 16778 RVA: 0x000200E7 File Offset: 0x0001E2E7
		// (set) Token: 0x0600418B RID: 16779 RVA: 0x000200EF File Offset: 0x0001E2EF
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x000200F8 File Offset: 0x0001E2F8
		// (set) Token: 0x0600418D RID: 16781 RVA: 0x00020100 File Offset: 0x0001E300
		[DataMember]
		public int CampusId { get; set; }
	}
}
