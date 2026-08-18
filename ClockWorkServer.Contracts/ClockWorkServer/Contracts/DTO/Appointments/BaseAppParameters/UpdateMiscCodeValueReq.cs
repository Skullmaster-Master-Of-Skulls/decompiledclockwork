using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000967 RID: 2407
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMiscCodeValueReq : BaseMessageReq
	{
		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x00017F3A File Offset: 0x0001613A
		// (set) Token: 0x06003125 RID: 12581 RVA: 0x00017F42 File Offset: 0x00016142
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x00017F4B File Offset: 0x0001614B
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x00017F53 File Offset: 0x00016153
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x00017F5C File Offset: 0x0001615C
		// (set) Token: 0x06003129 RID: 12585 RVA: 0x00017F64 File Offset: 0x00016164
		[DataMember]
		public int MiscCodeValue { get; set; }
	}
}
