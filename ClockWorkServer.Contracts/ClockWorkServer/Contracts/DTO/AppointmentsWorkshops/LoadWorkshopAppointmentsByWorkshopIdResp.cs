using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008FA RID: 2298
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopAppointmentsByWorkshopIdResp
	{
		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x000164A2 File Offset: 0x000146A2
		// (set) Token: 0x06002EDB RID: 11995 RVA: 0x000164AA File Offset: 0x000146AA
		[DataMember]
		public IList<WorkshopAppointmentDTO> WorkshopAppointments { get; set; }
	}
}
