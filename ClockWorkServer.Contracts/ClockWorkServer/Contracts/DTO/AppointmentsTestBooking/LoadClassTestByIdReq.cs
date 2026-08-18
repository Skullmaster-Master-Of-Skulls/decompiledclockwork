using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A1A RID: 2586
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestByIdReq : BaseMessageReq
	{
		// Token: 0x1700134B RID: 4939
		// (get) Token: 0x06003587 RID: 13703 RVA: 0x00019FCE File Offset: 0x000181CE
		// (set) Token: 0x06003588 RID: 13704 RVA: 0x00019FD6 File Offset: 0x000181D6
		[DataMember]
		public int ExamId { get; set; }
	}
}
