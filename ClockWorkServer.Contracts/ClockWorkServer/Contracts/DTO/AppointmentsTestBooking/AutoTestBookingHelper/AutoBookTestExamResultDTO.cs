using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A99 RID: 2713
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoBookTestExamResultDTO : AutoBookTestExamPreviewResultDTO
	{
		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x06003924 RID: 14628 RVA: 0x0001BBC7 File Offset: 0x00019DC7
		// (set) Token: 0x06003925 RID: 14629 RVA: 0x0001BBCF File Offset: 0x00019DCF
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06003926 RID: 14630 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		// (set) Token: 0x06003927 RID: 14631 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		[DataMember]
		public int ExamId { get; set; }
	}
}
