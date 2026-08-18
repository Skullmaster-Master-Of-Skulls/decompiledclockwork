using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AC7 RID: 2759
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateListAppointmentReq : BaseMessageReq
	{
		// Token: 0x17001578 RID: 5496
		// (get) Token: 0x06003A8D RID: 14989 RVA: 0x0001C8E1 File Offset: 0x0001AAE1
		// (set) Token: 0x06003A8E RID: 14990 RVA: 0x0001C8E9 File Offset: 0x0001AAE9
		[DataMember]
		public ListAppointmentDTO Appointment { get; set; }
	}
}
