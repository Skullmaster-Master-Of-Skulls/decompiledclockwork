using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C2C RID: 3116
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateStudentMediaReq : BaseReportMessageReq
	{
		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06004154 RID: 16724 RVA: 0x0001FFB5 File Offset: 0x0001E1B5
		// (set) Token: 0x06004155 RID: 16725 RVA: 0x0001FFBD File Offset: 0x0001E1BD
		[DataMember]
		public StudentMediaRequestDTO MediaRequest { get; set; }
	}
}
