using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C4A RID: 3146
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateStudentContentMediaRequestInfoReq : BaseReportMessageReq
	{
		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x060041C0 RID: 16832 RVA: 0x0002024C File Offset: 0x0001E44C
		// (set) Token: 0x060041C1 RID: 16833 RVA: 0x00020254 File Offset: 0x0001E454
		[DataMember]
		public MediaContentRequestedInfoDTO MediaContentRequestedInfo { get; set; }
	}
}
