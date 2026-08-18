using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B16 RID: 2838
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentExtendedInfoResp
	{
		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x0001D1AA File Offset: 0x0001B3AA
		// (set) Token: 0x06003BE3 RID: 15331 RVA: 0x0001D1B2 File Offset: 0x0001B3B2
		[DataMember]
		public int OrganizerPersonId { get; set; }
	}
}
