using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C50 RID: 3152
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteStudentContentMediaRequestInfoByIdReq : BaseReportMessageReq
	{
		// Token: 0x17001846 RID: 6214
		// (get) Token: 0x060041CE RID: 16846 RVA: 0x00020290 File Offset: 0x0001E490
		// (set) Token: 0x060041CF RID: 16847 RVA: 0x00020298 File Offset: 0x0001E498
		[DataMember]
		public int MediaContentRequestInfoId { get; set; }
	}
}
