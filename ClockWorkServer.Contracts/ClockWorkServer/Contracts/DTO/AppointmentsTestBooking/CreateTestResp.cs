using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A2E RID: 2606
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateTestResp
	{
		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x0001A155 File Offset: 0x00018355
		// (set) Token: 0x060035CA RID: 13770 RVA: 0x0001A15D File Offset: 0x0001835D
		[DataMember]
		public int AppointmentId { get; set; }
	}
}
