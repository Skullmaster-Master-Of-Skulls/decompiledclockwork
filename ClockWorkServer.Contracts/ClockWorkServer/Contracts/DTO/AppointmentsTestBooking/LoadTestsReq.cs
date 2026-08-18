using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E6 RID: 2534
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestsReq : BaseMessageReq
	{
		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x00019B6C File Offset: 0x00017D6C
		// (set) Token: 0x060034D0 RID: 13520 RVA: 0x00019B74 File Offset: 0x00017D74
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x00019B7D File Offset: 0x00017D7D
		// (set) Token: 0x060034D2 RID: 13522 RVA: 0x00019B85 File Offset: 0x00017D85
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x00019B8E File Offset: 0x00017D8E
		// (set) Token: 0x060034D4 RID: 13524 RVA: 0x00019B96 File Offset: 0x00017D96
		[DataMember]
		public bool HideCancelled { get; set; }
	}
}
