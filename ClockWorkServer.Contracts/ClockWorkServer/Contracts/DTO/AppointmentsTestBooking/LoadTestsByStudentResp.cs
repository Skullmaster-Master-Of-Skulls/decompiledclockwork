using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A3A RID: 2618
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsByStudentResp
	{
		// Token: 0x1700137F RID: 4991
		// (get) Token: 0x0600360F RID: 13839 RVA: 0x0001A342 File Offset: 0x00018542
		// (set) Token: 0x06003610 RID: 13840 RVA: 0x0001A34A File Offset: 0x0001854A
		[DataMember]
		public List<TestDTO> Tests { get; set; }
	}
}
