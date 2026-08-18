using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ACF RID: 2767
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnMarkListAppointmentAsTentativeReq : BaseMessageReq
	{
		// Token: 0x1700157C RID: 5500
		// (get) Token: 0x06003A9D RID: 15005 RVA: 0x0001C925 File Offset: 0x0001AB25
		// (set) Token: 0x06003A9E RID: 15006 RVA: 0x0001C92D File Offset: 0x0001AB2D
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
