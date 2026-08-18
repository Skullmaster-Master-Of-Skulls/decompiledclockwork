using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x0200090B RID: 2315
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateWorkshopAppointmentReq : BaseMessageReq
	{
		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x0001656E File Offset: 0x0001476E
		// (set) Token: 0x06002F04 RID: 12036 RVA: 0x00016576 File Offset: 0x00014776
		[DataMember]
		public WorkshopAppointmentDTO WorkshopAppointment { get; set; }
	}
}
