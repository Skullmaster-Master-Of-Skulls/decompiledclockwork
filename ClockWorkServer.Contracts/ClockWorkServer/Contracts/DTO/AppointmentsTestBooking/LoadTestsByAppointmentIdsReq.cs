using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A27 RID: 2599
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsByAppointmentIdsReq : BaseMessageReq
	{
		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x0001A0CD File Offset: 0x000182CD
		// (set) Token: 0x060035B3 RID: 13747 RVA: 0x0001A0D5 File Offset: 0x000182D5
		[DataMember]
		public IList<int> AppointmentIds { get; set; }
	}
}
