using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B29 RID: 2857
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsAndAvailabilityResp
	{
		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x06003C2B RID: 15403 RVA: 0x0001D375 File Offset: 0x0001B575
		// (set) Token: 0x06003C2C RID: 15404 RVA: 0x0001D37D File Offset: 0x0001B57D
		[DataMember]
		public AppointmentsWithAvailabilityAndTimetableDTO AppointmentsWithAvailabilityAndTimetable { get; set; }
	}
}
