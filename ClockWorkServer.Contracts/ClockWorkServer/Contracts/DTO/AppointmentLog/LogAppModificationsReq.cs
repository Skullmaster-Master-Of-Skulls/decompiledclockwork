using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentLog
{
	// Token: 0x02000B3A RID: 2874
	[DataContract(Namespace = "http://tpro.ca")]
	public class LogAppModificationsReq : BaseMsmqMessageReq
	{
		// Token: 0x17001632 RID: 5682
		// (get) Token: 0x06003C74 RID: 15476 RVA: 0x0001D551 File Offset: 0x0001B751
		// (set) Token: 0x06003C75 RID: 15477 RVA: 0x0001D559 File Offset: 0x0001B759
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001633 RID: 5683
		// (get) Token: 0x06003C76 RID: 15478 RVA: 0x0001D562 File Offset: 0x0001B762
		// (set) Token: 0x06003C77 RID: 15479 RVA: 0x0001D56A File Offset: 0x0001B76A
		[DataMember]
		public eAppointmentModifiedItemType AppointmentLogFields { get; set; }
	}
}
