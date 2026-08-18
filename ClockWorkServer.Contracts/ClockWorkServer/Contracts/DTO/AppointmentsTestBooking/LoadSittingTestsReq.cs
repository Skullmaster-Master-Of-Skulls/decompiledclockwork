using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E4 RID: 2532
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSittingTestsReq : BaseMessageReq
	{
		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x00019B4A File Offset: 0x00017D4A
		// (set) Token: 0x060034CA RID: 13514 RVA: 0x00019B52 File Offset: 0x00017D52
		[DataMember]
		public SittingDTO Sitting { get; set; }
	}
}
