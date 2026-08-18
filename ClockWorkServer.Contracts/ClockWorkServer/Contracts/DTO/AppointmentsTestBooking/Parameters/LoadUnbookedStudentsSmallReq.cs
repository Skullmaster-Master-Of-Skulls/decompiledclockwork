using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A6B RID: 2667
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUnbookedStudentsSmallReq : BaseReportMessageReq
	{
		// Token: 0x1700145E RID: 5214
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x0001B30A File Offset: 0x0001950A
		// (set) Token: 0x060037F9 RID: 14329 RVA: 0x0001B312 File Offset: 0x00019512
		[DataMember]
		public UnBookedStudentMmanagementContextDTO Context { get; set; }
	}
}
