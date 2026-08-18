using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C4C RID: 3148
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddStudentContentMediaRequestInfoReq : BaseReportMessageReq
	{
		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x060041C4 RID: 16836 RVA: 0x0002025D File Offset: 0x0001E45D
		// (set) Token: 0x060041C5 RID: 16837 RVA: 0x00020265 File Offset: 0x0001E465
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }
	}
}
