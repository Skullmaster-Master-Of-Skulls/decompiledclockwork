using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C54 RID: 3156
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaRequestInfoPreApprovalReq : BaseReportMessageReq
	{
		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x060041DC RID: 16860 RVA: 0x000202E5 File Offset: 0x0001E4E5
		// (set) Token: 0x060041DD RID: 16861 RVA: 0x000202ED File Offset: 0x0001E4ED
		[DataMember]
		public MediaContentRequestedInfoDTO BaseReportMessageReq { get; set; }

		// Token: 0x1700184C RID: 6220
		// (get) Token: 0x060041DE RID: 16862 RVA: 0x000202F6 File Offset: 0x0001E4F6
		// (set) Token: 0x060041DF RID: 16863 RVA: 0x000202FE File Offset: 0x0001E4FE
		[DataMember]
		public int StudentId { get; set; }
	}
}
