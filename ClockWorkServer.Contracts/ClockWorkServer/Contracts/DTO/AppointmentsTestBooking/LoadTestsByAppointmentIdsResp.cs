using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A26 RID: 2598
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsByAppointmentIdsResp
	{
		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x0001A0BC File Offset: 0x000182BC
		// (set) Token: 0x060035B0 RID: 13744 RVA: 0x0001A0C4 File Offset: 0x000182C4
		[DataMember]
		public IList<TestDTO> Tests { get; set; }
	}
}
