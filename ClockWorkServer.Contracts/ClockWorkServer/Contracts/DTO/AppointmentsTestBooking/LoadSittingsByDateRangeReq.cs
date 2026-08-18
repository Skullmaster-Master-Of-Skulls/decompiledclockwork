using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A25 RID: 2597
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadSittingsByDateRangeReq : BaseMessageReq
	{
		// Token: 0x17001357 RID: 4951
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x0001A09A File Offset: 0x0001829A
		// (set) Token: 0x060035AB RID: 13739 RVA: 0x0001A0A2 File Offset: 0x000182A2
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x0001A0AB File Offset: 0x000182AB
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x0001A0B3 File Offset: 0x000182B3
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
