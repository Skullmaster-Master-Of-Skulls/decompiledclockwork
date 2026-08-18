using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A06 RID: 2566
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestBaseByIdResp
	{
		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x00019E03 File Offset: 0x00018003
		// (set) Token: 0x0600353E RID: 13630 RVA: 0x00019E0B File Offset: 0x0001800B
		[DataMember]
		public ClassTestBaseDTO ClassTestBase { get; set; }
	}
}
