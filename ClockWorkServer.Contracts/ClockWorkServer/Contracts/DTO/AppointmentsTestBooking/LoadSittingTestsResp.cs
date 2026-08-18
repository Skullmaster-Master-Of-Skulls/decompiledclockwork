using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E3 RID: 2531
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSittingTestsResp
	{
		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x00019B39 File Offset: 0x00017D39
		// (set) Token: 0x060034C7 RID: 13511 RVA: 0x00019B41 File Offset: 0x00017D41
		[DataMember]
		public List<TestDTO> Tests { get; set; }
	}
}
