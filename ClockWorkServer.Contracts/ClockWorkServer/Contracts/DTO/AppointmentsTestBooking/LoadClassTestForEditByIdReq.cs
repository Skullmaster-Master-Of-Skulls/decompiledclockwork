using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A18 RID: 2584
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestForEditByIdReq : BaseMessageReq
	{
		// Token: 0x17001349 RID: 4937
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x00019FAC File Offset: 0x000181AC
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x00019FB4 File Offset: 0x000181B4
		[DataMember]
		public int ExamId { get; set; }
	}
}
