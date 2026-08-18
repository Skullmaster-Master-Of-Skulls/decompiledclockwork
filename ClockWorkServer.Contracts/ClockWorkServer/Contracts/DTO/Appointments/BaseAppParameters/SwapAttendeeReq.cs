using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000957 RID: 2391
	[DataContract(Namespace = "http://tpro.ca")]
	public class SwapAttendeeReq : BaseMessageReq
	{
		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x00017D5E File Offset: 0x00015F5E
		// (set) Token: 0x060030DD RID: 12509 RVA: 0x00017D66 File Offset: 0x00015F66
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x00017D6F File Offset: 0x00015F6F
		// (set) Token: 0x060030DF RID: 12511 RVA: 0x00017D77 File Offset: 0x00015F77
		[DataMember]
		public int OldPersonId { get; set; }

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x00017D80 File Offset: 0x00015F80
		// (set) Token: 0x060030E1 RID: 12513 RVA: 0x00017D88 File Offset: 0x00015F88
		[DataMember]
		public int NewPersonId { get; set; }
	}
}
