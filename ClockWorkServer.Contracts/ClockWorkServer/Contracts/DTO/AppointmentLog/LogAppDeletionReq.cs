using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentLog
{
	// Token: 0x02000B3B RID: 2875
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogAppDeletionReq : BaseMsmqMessageReq
	{
		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x0001D573 File Offset: 0x0001B773
		// (set) Token: 0x06003C7A RID: 15482 RVA: 0x0001D57B File Offset: 0x0001B77B
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x06003C7B RID: 15483 RVA: 0x0001D584 File Offset: 0x0001B784
		// (set) Token: 0x06003C7C RID: 15484 RVA: 0x0001D58C File Offset: 0x0001B78C
		[DataMember]
		public eAppointmentModifiedItemType AppointmentLogFields { get; set; }
	}
}
