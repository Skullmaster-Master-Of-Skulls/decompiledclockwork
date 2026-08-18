using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A15 RID: 2581
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestsForDisplayResp
	{
		// Token: 0x17001347 RID: 4935
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x00019F8A File Offset: 0x0001818A
		// (set) Token: 0x0600357B RID: 13691 RVA: 0x00019F92 File Offset: 0x00018192
		[DataMember]
		public IList<ClassTestForDisplayDTO> ClassTestsForDisplay { get; set; }
	}
}
