using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A5C RID: 2652
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateExamRequestResp
	{
		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x060037AB RID: 14251 RVA: 0x0001B0FB File Offset: 0x000192FB
		// (set) Token: 0x060037AC RID: 14252 RVA: 0x0001B103 File Offset: 0x00019303
		[DataMember]
		public int ExamRequestId { get; set; }
	}
}
