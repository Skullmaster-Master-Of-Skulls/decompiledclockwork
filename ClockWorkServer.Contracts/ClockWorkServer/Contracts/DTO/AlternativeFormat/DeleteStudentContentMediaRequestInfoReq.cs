using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C4E RID: 3150
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteStudentContentMediaRequestInfoReq : BaseReportMessageReq
	{
		// Token: 0x17001845 RID: 6213
		// (get) Token: 0x060041CA RID: 16842 RVA: 0x0002027F File Offset: 0x0001E47F
		// (set) Token: 0x060041CB RID: 16843 RVA: 0x00020287 File Offset: 0x0001E487
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }
	}
}
