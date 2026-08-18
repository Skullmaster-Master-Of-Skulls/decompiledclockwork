using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A12 RID: 2578
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestForExamRequestByIdReq : BaseMessageReq
	{
		// Token: 0x17001343 RID: 4931
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x00019F46 File Offset: 0x00018146
		// (set) Token: 0x06003570 RID: 13680 RVA: 0x00019F4E File Offset: 0x0001814E
		[DataMember]
		public int ExamId { get; set; }
	}
}
