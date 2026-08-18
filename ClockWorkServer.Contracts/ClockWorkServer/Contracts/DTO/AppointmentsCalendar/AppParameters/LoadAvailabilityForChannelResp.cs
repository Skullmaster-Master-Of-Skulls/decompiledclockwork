using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.StudentAppointmentBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B08 RID: 2824
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityForChannelResp
	{
		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06003BB0 RID: 15280 RVA: 0x0001D078 File Offset: 0x0001B278
		// (set) Token: 0x06003BB1 RID: 15281 RVA: 0x0001D080 File Offset: 0x0001B280
		[DataMember]
		public IList<ChannelCalendarWithAvailabilityDTO> ChannelCalendarsWithAvailabilities { get; set; }
	}
}
