using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentLog
{
	// Token: 0x02000B3C RID: 2876
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogAppCreationReq : BaseMsmqMessageReq
	{
		// Token: 0x17001636 RID: 5686
		// (get) Token: 0x06003C7E RID: 15486 RVA: 0x0001D595 File Offset: 0x0001B795
		// (set) Token: 0x06003C7F RID: 15487 RVA: 0x0001D59D File Offset: 0x0001B79D
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001637 RID: 5687
		// (get) Token: 0x06003C80 RID: 15488 RVA: 0x0001D5A6 File Offset: 0x0001B7A6
		// (set) Token: 0x06003C81 RID: 15489 RVA: 0x0001D5AE File Offset: 0x0001B7AE
		[DataMember]
		public eAppointmentModifiedItemType AppointmentLogFields { get; set; }
	}
}
