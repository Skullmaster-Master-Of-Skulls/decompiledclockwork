using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A36 RID: 2614
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateInstructorSubmittedTestInfoReq : BaseMessageReq
	{
		// Token: 0x17001373 RID: 4979
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x0001A276 File Offset: 0x00018476
		// (set) Token: 0x060035F4 RID: 13812 RVA: 0x0001A27E File Offset: 0x0001847E
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17001374 RID: 4980
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x0001A287 File Offset: 0x00018487
		// (set) Token: 0x060035F6 RID: 13814 RVA: 0x0001A28F File Offset: 0x0001848F
		[DataMember]
		public int InstructorId { get; set; }
	}
}
