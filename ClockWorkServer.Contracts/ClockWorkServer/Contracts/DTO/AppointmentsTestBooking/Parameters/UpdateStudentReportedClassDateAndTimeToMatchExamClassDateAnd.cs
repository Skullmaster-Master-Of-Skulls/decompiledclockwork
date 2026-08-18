using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Parameters
{
	// Token: 0x02000A64 RID: 2660
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateStudentReportedClassDateAndTimeToMatchExamClassDateAndTimeReq : BaseMessageReq
	{
		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060037CD RID: 14285 RVA: 0x0001B1D8 File Offset: 0x000193D8
		// (set) Token: 0x060037CE RID: 14286 RVA: 0x0001B1E0 File Offset: 0x000193E0
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
