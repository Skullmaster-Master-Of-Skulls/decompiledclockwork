using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B36 RID: 2870
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAppointmentDateAndTimeReq : BaseMessageReq
	{
		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x0001D4EB File Offset: 0x0001B6EB
		// (set) Token: 0x06003C65 RID: 15461 RVA: 0x0001D4F3 File Offset: 0x0001B6F3
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700162D RID: 5677
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x0001D4FC File Offset: 0x0001B6FC
		// (set) Token: 0x06003C67 RID: 15463 RVA: 0x0001D504 File Offset: 0x0001B704
		[DataMember]
		public DateTime NewStartDateTime { get; set; }

		// Token: 0x1700162E RID: 5678
		// (get) Token: 0x06003C68 RID: 15464 RVA: 0x0001D50D File Offset: 0x0001B70D
		// (set) Token: 0x06003C69 RID: 15465 RVA: 0x0001D515 File Offset: 0x0001B715
		[DataMember]
		public DateTime NewEndDateTime { get; set; }
	}
}
