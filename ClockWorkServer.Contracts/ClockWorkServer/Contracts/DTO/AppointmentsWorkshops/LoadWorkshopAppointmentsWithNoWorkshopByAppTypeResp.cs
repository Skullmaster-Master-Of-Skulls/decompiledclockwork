using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008F8 RID: 2296
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopAppointmentsWithNoWorkshopByAppTypeResp
	{
		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x0001645E File Offset: 0x0001465E
		// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x00016466 File Offset: 0x00014666
		[DataMember]
		public IList<WorkshopAppointmentDTO> WorkshopAppointments { get; set; }
	}
}
