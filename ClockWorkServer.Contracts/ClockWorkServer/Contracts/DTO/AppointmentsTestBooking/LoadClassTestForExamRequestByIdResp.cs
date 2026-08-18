using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A13 RID: 2579
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestForExamRequestByIdResp
	{
		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06003572 RID: 13682 RVA: 0x00019F57 File Offset: 0x00018157
		// (set) Token: 0x06003573 RID: 13683 RVA: 0x00019F5F File Offset: 0x0001815F
		[DataMember]
		public ClassTestForExamRequestDTO ClassTestForExamRequest { get; set; }
	}
}
