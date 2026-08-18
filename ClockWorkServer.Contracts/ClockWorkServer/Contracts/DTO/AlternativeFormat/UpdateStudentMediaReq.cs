using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C2E RID: 3118
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateStudentMediaReq : BaseReportMessageReq
	{
		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x0600415A RID: 16730 RVA: 0x0001FFD7 File Offset: 0x0001E1D7
		// (set) Token: 0x0600415B RID: 16731 RVA: 0x0001FFDF File Offset: 0x0001E1DF
		[DataMember]
		public StudentMediaRequestDTO MediaRequest { get; set; }
	}
}
