using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A29 RID: 2601
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBasicTestsByAppointmentIdsReq : BaseMessageReq
	{
		// Token: 0x1700135C RID: 4956
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x0001A0EF File Offset: 0x000182EF
		// (set) Token: 0x060035B9 RID: 13753 RVA: 0x0001A0F7 File Offset: 0x000182F7
		[DataMember]
		public IList<int> AppointmentIds { get; set; }
	}
}
