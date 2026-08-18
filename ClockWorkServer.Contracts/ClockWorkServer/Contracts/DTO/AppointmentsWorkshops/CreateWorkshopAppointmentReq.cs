using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000909 RID: 2313
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateWorkshopAppointmentReq : BaseMessageReq
	{
		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x0001654C File Offset: 0x0001474C
		// (set) Token: 0x06002EFE RID: 12030 RVA: 0x00016554 File Offset: 0x00014754
		[DataMember]
		public WorkshopAppointmentDTO WorkshopAppointment { get; set; }
	}
}
