using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A4F RID: 2639
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteExamFileReq : BaseMessageReq
	{
		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06003772 RID: 14194 RVA: 0x0001AF85 File Offset: 0x00019185
		// (set) Token: 0x06003773 RID: 14195 RVA: 0x0001AF8D File Offset: 0x0001918D
		[DataMember]
		public int ExamFileId { get; set; }
	}
}
