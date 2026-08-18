using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200094D RID: 2381
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentIconReq : BaseMessageReq
	{
		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x00017C81 File Offset: 0x00015E81
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x00017C89 File Offset: 0x00015E89
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x00017C92 File Offset: 0x00015E92
		// (set) Token: 0x060030BB RID: 12475 RVA: 0x00017C9A File Offset: 0x00015E9A
		[DataMember]
		public int IconNum { get; set; }
	}
}
