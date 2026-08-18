using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000969 RID: 2409
	[DataContract(Namespace = "http://tpro.ca")]
	public class IsAttendeeDoubleBookedReq : BaseMessageReq
	{
		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x00017F8F File Offset: 0x0001618F
		// (set) Token: 0x06003131 RID: 12593 RVA: 0x00017F97 File Offset: 0x00016197
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x00017FA0 File Offset: 0x000161A0
		// (set) Token: 0x06003133 RID: 12595 RVA: 0x00017FA8 File Offset: 0x000161A8
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x00017FB1 File Offset: 0x000161B1
		// (set) Token: 0x06003135 RID: 12597 RVA: 0x00017FB9 File Offset: 0x000161B9
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x00017FC2 File Offset: 0x000161C2
		// (set) Token: 0x06003137 RID: 12599 RVA: 0x00017FCA File Offset: 0x000161CA
		[DataMember]
		public int AppointmentIdToSkip { get; set; }
	}
}
