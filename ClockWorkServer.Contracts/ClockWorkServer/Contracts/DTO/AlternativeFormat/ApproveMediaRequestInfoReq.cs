using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C52 RID: 3154
	[DataContract(Namespace = "http://tpro.ca")]
	public class ApproveMediaRequestInfoReq : BaseReportMessageReq
	{
		// Token: 0x17001847 RID: 6215
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x000202A1 File Offset: 0x0001E4A1
		// (set) Token: 0x060041D3 RID: 16851 RVA: 0x000202A9 File Offset: 0x0001E4A9
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }

		// Token: 0x17001848 RID: 6216
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x000202B2 File Offset: 0x0001E4B2
		// (set) Token: 0x060041D5 RID: 16853 RVA: 0x000202BA File Offset: 0x0001E4BA
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x17001849 RID: 6217
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x000202C3 File Offset: 0x0001E4C3
		// (set) Token: 0x060041D7 RID: 16855 RVA: 0x000202CB File Offset: 0x0001E4CB
		[DataMember]
		public bool WasPreApprove { get; set; }
	}
}
