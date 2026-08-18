using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D2 RID: 2514
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoBookTestOrExamResp
	{
		// Token: 0x170012CA RID: 4810
		// (get) Token: 0x06003439 RID: 13369 RVA: 0x0001962E File Offset: 0x0001782E
		// (set) Token: 0x0600343A RID: 13370 RVA: 0x00019636 File Offset: 0x00017836
		[DataMember]
		public AutoBookTestExamResultDTO AutoBookTestExamResult { get; set; }
	}
}
