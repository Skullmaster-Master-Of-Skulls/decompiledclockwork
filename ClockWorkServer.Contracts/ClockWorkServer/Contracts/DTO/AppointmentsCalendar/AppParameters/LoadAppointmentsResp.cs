using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B1C RID: 2844
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentsResp
	{
		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x0001D221 File Offset: 0x0001B421
		// (set) Token: 0x06003BF7 RID: 15351 RVA: 0x0001D229 File Offset: 0x0001B429
		[DataMember]
		public List<AppointmentDTO> Appointments { get; set; }
	}
}
