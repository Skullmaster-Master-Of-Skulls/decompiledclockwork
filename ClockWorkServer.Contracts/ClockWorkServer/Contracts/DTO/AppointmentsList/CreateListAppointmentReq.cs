using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AC5 RID: 2757
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateListAppointmentReq : BaseMessageReq
	{
		// Token: 0x17001577 RID: 5495
		// (get) Token: 0x06003A89 RID: 14985 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
		// (set) Token: 0x06003A8A RID: 14986 RVA: 0x0001C8D8 File Offset: 0x0001AAD8
		[DataMember]
		public ListAppointmentDTO Appointment { get; set; }
	}
}
