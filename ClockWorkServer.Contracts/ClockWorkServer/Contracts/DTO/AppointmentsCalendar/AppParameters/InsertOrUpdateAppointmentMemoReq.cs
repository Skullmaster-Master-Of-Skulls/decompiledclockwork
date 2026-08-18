using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B25 RID: 2853
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentMemoReq : BaseMessageReq
	{
		// Token: 0x17001611 RID: 5649
		// (get) Token: 0x06003C1D RID: 15389 RVA: 0x0001D320 File Offset: 0x0001B520
		// (set) Token: 0x06003C1E RID: 15390 RVA: 0x0001D328 File Offset: 0x0001B528
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001612 RID: 5650
		// (get) Token: 0x06003C1F RID: 15391 RVA: 0x0001D331 File Offset: 0x0001B531
		// (set) Token: 0x06003C20 RID: 15392 RVA: 0x0001D339 File Offset: 0x0001B539
		[DataMember]
		public string MemoText { get; set; }
	}
}
