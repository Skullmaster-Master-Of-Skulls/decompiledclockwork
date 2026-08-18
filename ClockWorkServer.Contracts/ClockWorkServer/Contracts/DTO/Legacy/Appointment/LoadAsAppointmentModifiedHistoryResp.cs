using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.Appointment
{
	// Token: 0x020004E8 RID: 1256
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAsAppointmentModifiedHistoryResp
	{
		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x0000C482 File Offset: 0x0000A682
		// (set) Token: 0x06001A9A RID: 6810 RVA: 0x0000C48A File Offset: 0x0000A68A
		[DataMember]
		public IList<AppointmentModifiedHistoryItemDTO> AppointmentHistoryItems { get; set; }
	}
}
