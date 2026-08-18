using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment
{
	// Token: 0x020004E7 RID: 1255
	public class LoadAsAppointmentModifiedHistoryReq : BaseMessageReq
	{
		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001A96 RID: 6806 RVA: 0x0000C471 File Offset: 0x0000A671
		// (set) Token: 0x06001A97 RID: 6807 RVA: 0x0000C479 File Offset: 0x0000A679
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
