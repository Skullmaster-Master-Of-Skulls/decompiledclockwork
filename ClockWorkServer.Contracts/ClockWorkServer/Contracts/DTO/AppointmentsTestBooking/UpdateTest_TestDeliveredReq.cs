using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E7 RID: 2535
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateTest_TestDeliveredReq : BaseMessageReq
	{
		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x00019B9F File Offset: 0x00017D9F
		// (set) Token: 0x060034D7 RID: 13527 RVA: 0x00019BA7 File Offset: 0x00017DA7
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x060034D8 RID: 13528 RVA: 0x00019BB0 File Offset: 0x00017DB0
		// (set) Token: 0x060034D9 RID: 13529 RVA: 0x00019BB8 File Offset: 0x00017DB8
		[DataMember]
		public string TestDelivered { get; set; }
	}
}
