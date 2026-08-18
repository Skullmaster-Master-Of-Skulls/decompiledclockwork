using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009E2 RID: 2530
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateSittingReq : BaseMessageReq
	{
		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x00019B28 File Offset: 0x00017D28
		// (set) Token: 0x060034C4 RID: 13508 RVA: 0x00019B30 File Offset: 0x00017D30
		[DataMember]
		public SittingDTO Sitting { get; set; }
	}
}
