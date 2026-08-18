using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A3B RID: 2619
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearSittingOnAppointmentReq : BaseMessageReq
	{
		// Token: 0x17001380 RID: 4992
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x0001A353 File Offset: 0x00018553
		// (set) Token: 0x06003613 RID: 13843 RVA: 0x0001A35B File Offset: 0x0001855B
		[DataMember]
		public int[] AppointmentIds { get; set; }
	}
}
