using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C32 RID: 3122
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentMediaRequestByIdReq : BaseReportMessageReq
	{
		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06004162 RID: 16738 RVA: 0x0001FFF9 File Offset: 0x0001E1F9
		// (set) Token: 0x06004163 RID: 16739 RVA: 0x00020001 File Offset: 0x0001E201
		[DataMember]
		public int StudentMediaRequestId { get; set; }
	}
}
