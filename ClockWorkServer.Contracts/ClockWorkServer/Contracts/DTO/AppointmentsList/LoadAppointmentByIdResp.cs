using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE7 RID: 2791
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentByIdResp
	{
		// Token: 0x170015A6 RID: 5542
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x0001CBEF File Offset: 0x0001ADEF
		// (set) Token: 0x06003B0A RID: 15114 RVA: 0x0001CBF7 File Offset: 0x0001ADF7
		[DataMember]
		public ListAppointmentDTO Appointment { get; set; }
	}
}
