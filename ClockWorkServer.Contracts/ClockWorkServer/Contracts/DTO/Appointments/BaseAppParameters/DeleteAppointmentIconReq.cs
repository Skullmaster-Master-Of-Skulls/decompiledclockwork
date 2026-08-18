using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000954 RID: 2388
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAppointmentIconReq : BaseMessageReq
	{
		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x00017D2B File Offset: 0x00015F2B
		// (set) Token: 0x060030D4 RID: 12500 RVA: 0x00017D33 File Offset: 0x00015F33
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x00017D3C File Offset: 0x00015F3C
		// (set) Token: 0x060030D6 RID: 12502 RVA: 0x00017D44 File Offset: 0x00015F44
		[DataMember]
		public int IconNum { get; set; }
	}
}
