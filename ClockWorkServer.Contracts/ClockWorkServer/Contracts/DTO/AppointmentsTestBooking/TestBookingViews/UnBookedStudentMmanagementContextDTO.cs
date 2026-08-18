using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews
{
	// Token: 0x02000A43 RID: 2627
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnBookedStudentMmanagementContextDTO
	{
		// Token: 0x17001389 RID: 5001
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x0001A3EC File Offset: 0x000185EC
		// (set) Token: 0x0600362D RID: 13869 RVA: 0x0001A3F4 File Offset: 0x000185F4
		[DataMember]
		public int ReportId { get; set; }
	}
}
