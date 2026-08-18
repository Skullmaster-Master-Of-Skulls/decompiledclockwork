using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E5 RID: 2533
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsResp
	{
		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x00019B5B File Offset: 0x00017D5B
		// (set) Token: 0x060034CD RID: 13517 RVA: 0x00019B63 File Offset: 0x00017D63
		[DataMember]
		public List<TestDTO> Tests { get; set; }
	}
}
