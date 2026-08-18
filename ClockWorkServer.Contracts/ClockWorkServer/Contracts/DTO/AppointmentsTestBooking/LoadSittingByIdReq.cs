using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009EB RID: 2539
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSittingByIdReq : BaseMessageReq
	{
		// Token: 0x17001311 RID: 4881
		// (get) Token: 0x060034E4 RID: 13540 RVA: 0x00019BF4 File Offset: 0x00017DF4
		// (set) Token: 0x060034E5 RID: 13541 RVA: 0x00019BFC File Offset: 0x00017DFC
		[DataMember]
		public int SittingId { get; set; }
	}
}
