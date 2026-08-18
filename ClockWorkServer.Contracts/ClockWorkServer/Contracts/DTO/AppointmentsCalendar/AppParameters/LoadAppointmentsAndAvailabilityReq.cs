using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B28 RID: 2856
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsAndAvailabilityReq : BaseMessageReq
	{
		// Token: 0x17001615 RID: 5653
		// (get) Token: 0x06003C28 RID: 15400 RVA: 0x0001D364 File Offset: 0x0001B564
		// (set) Token: 0x06003C29 RID: 15401 RVA: 0x0001D36C File Offset: 0x0001B56C
		[DataMember]
		public AppointmentLoadOptionsDTO LoadOptions { get; set; }
	}
}
