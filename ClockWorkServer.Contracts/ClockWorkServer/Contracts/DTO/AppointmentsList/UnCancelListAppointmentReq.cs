using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000ACB RID: 2763
	[DataContract(Namespace = "http://tpro.ca")]
	public class UnCancelListAppointmentReq : BaseMessageReq
	{
		// Token: 0x1700157A RID: 5498
		// (get) Token: 0x06003A95 RID: 14997 RVA: 0x0001C903 File Offset: 0x0001AB03
		// (set) Token: 0x06003A96 RID: 14998 RVA: 0x0001C90B File Offset: 0x0001AB0B
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
