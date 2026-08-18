using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C30 RID: 3120
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteStudentMediaReq : BaseReportMessageReq
	{
		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x0600415E RID: 16734 RVA: 0x0001FFE8 File Offset: 0x0001E1E8
		// (set) Token: 0x0600415F RID: 16735 RVA: 0x0001FFF0 File Offset: 0x0001E1F0
		[DataMember]
		public StudentMediaRequestDTO MediaRequest { get; set; }
	}
}
