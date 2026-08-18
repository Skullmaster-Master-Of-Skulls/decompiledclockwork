using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000958 RID: 2392
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateAttendeeNoShowReq : BaseMessageReq
	{
		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x060030E3 RID: 12515 RVA: 0x00017D91 File Offset: 0x00015F91
		// (set) Token: 0x060030E4 RID: 12516 RVA: 0x00017D99 File Offset: 0x00015F99
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x060030E5 RID: 12517 RVA: 0x00017DA2 File Offset: 0x00015FA2
		// (set) Token: 0x060030E6 RID: 12518 RVA: 0x00017DAA File Offset: 0x00015FAA
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x060030E7 RID: 12519 RVA: 0x00017DB3 File Offset: 0x00015FB3
		// (set) Token: 0x060030E8 RID: 12520 RVA: 0x00017DBB File Offset: 0x00015FBB
		[DataMember]
		public bool NewNoShow { get; set; }
	}
}
