using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A5D RID: 2653
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteExamRequestReq : BaseMessageReq
	{
		// Token: 0x17001440 RID: 5184
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x0001B10C File Offset: 0x0001930C
		// (set) Token: 0x060037AF RID: 14255 RVA: 0x0001B114 File Offset: 0x00019314
		[DataMember]
		public int ExamRequestId { get; set; }
	}
}
