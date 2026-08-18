using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AD1 RID: 2769
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteListAppointmentReq : BaseMessageReq
	{
		// Token: 0x1700157D RID: 5501
		// (get) Token: 0x06003AA1 RID: 15009 RVA: 0x0001C936 File Offset: 0x0001AB36
		// (set) Token: 0x06003AA2 RID: 15010 RVA: 0x0001C93E File Offset: 0x0001AB3E
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
