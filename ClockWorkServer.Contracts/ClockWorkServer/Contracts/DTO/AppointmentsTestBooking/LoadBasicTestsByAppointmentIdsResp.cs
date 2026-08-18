using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A28 RID: 2600
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadBasicTestsByAppointmentIdsResp
	{
		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x060035B5 RID: 13749 RVA: 0x0001A0DE File Offset: 0x000182DE
		// (set) Token: 0x060035B6 RID: 13750 RVA: 0x0001A0E6 File Offset: 0x000182E6
		[DataMember]
		public IList<BasicTestDTO> Tests { get; set; }
	}
}
