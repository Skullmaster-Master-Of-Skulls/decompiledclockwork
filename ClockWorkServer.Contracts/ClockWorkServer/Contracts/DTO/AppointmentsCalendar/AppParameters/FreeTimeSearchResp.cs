using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B27 RID: 2855
	[DataContract(Namespace = "http://tpro.ca")]
	public class FreeTimeSearchResp
	{
		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06003C25 RID: 15397 RVA: 0x0001D353 File Offset: 0x0001B553
		// (set) Token: 0x06003C26 RID: 15398 RVA: 0x0001D35B File Offset: 0x0001B55B
		[DataMember]
		public IList<BaseBasicAppointmentDTO> AvailableSlots { get; set; }
	}
}
