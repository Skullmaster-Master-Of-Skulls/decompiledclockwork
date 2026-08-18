using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x0200090D RID: 2317
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopAppointmentReq : BaseMessageReq
	{
		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06002F07 RID: 12039 RVA: 0x0001657F File Offset: 0x0001477F
		// (set) Token: 0x06002F08 RID: 12040 RVA: 0x00016587 File Offset: 0x00014787
		[DataMember]
		public int WorkshopAppointmentId { get; set; }
	}
}
