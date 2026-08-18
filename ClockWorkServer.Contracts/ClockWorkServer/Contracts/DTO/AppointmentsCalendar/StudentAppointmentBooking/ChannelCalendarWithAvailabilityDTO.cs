using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking
{
	// Token: 0x02000AFE RID: 2814
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChannelCalendarWithAvailabilityDTO
	{
		// Token: 0x170015D2 RID: 5586
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x0001CEF1 File Offset: 0x0001B0F1
		// (set) Token: 0x06003B79 RID: 15225 RVA: 0x0001CEF9 File Offset: 0x0001B0F9
		[DataMember]
		public string CalendarTitle { get; set; }

		// Token: 0x170015D3 RID: 5587
		// (get) Token: 0x06003B7A RID: 15226 RVA: 0x0001CF02 File Offset: 0x0001B102
		// (set) Token: 0x06003B7B RID: 15227 RVA: 0x0001CF0A File Offset: 0x0001B10A
		[DataMember]
		public IList<AvailabilityForChannelCalendarDTO> Availabilities { get; set; }
	}
}
