using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200096F RID: 2415
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAttendeesByAppointmentIdsReq : BaseMessageReq
	{
		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x0001807D File Offset: 0x0001627D
		// (set) Token: 0x06003153 RID: 12627 RVA: 0x00018085 File Offset: 0x00016285
		[DataMember]
		public IList<int> AppointmentIds { get; set; }
	}
}
