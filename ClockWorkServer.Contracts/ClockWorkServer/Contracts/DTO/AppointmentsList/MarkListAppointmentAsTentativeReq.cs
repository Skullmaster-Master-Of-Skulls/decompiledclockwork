using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ACD RID: 2765
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkListAppointmentAsTentativeReq : BaseMessageReq
	{
		// Token: 0x1700157B RID: 5499
		// (get) Token: 0x06003A99 RID: 15001 RVA: 0x0001C914 File Offset: 0x0001AB14
		// (set) Token: 0x06003A9A RID: 15002 RVA: 0x0001C91C File Offset: 0x0001AB1C
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
