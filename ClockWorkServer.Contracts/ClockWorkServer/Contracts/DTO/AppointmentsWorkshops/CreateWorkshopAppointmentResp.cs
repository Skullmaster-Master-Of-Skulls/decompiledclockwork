using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x0200090A RID: 2314
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateWorkshopAppointmentResp
	{
		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x06002F00 RID: 12032 RVA: 0x0001655D File Offset: 0x0001475D
		// (set) Token: 0x06002F01 RID: 12033 RVA: 0x00016565 File Offset: 0x00014765
		[DataMember]
		public int WorkshopAppointmentId { get; set; }
	}
}
