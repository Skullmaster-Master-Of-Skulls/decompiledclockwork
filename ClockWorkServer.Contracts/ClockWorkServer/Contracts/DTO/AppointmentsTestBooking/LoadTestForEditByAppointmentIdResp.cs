using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A32 RID: 2610
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestForEditByAppointmentIdResp
	{
		// Token: 0x1700136C RID: 4972
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x0001A1FF File Offset: 0x000183FF
		// (set) Token: 0x060035E2 RID: 13794 RVA: 0x0001A207 File Offset: 0x00018407
		[DataMember]
		public TestForEditDTO TestForEdit { get; set; }
	}
}
