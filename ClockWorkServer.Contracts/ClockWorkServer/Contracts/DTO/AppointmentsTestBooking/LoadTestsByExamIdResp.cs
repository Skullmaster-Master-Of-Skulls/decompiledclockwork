using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A21 RID: 2593
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsByExamIdResp
	{
		// Token: 0x17001353 RID: 4947
		// (get) Token: 0x0600359E RID: 13726 RVA: 0x0001A056 File Offset: 0x00018256
		// (set) Token: 0x0600359F RID: 13727 RVA: 0x0001A05E File Offset: 0x0001825E
		[DataMember]
		public IList<TestDTO> Tests { get; set; }
	}
}
