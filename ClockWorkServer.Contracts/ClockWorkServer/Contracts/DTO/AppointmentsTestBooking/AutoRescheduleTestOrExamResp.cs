using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D6 RID: 2518
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoRescheduleTestOrExamResp
	{
		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000196D8 File Offset: 0x000178D8
		// (set) Token: 0x06003452 RID: 13394 RVA: 0x000196E0 File Offset: 0x000178E0
		[DataMember]
		public AutoRescheduleTestExamResultDTO AutoRescheduleTestExamPreviewResult { get; set; }
	}
}
