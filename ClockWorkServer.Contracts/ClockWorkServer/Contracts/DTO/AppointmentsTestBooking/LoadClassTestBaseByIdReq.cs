using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A07 RID: 2567
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestBaseByIdReq : BaseMessageReq
	{
		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06003540 RID: 13632 RVA: 0x00019E14 File Offset: 0x00018014
		// (set) Token: 0x06003541 RID: 13633 RVA: 0x00019E1C File Offset: 0x0001801C
		[DataMember]
		public int ExamId { get; set; }
	}
}
