using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000953 RID: 2387
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentIconResp
	{
		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x00017D1A File Offset: 0x00015F1A
		// (set) Token: 0x060030D1 RID: 12497 RVA: 0x00017D22 File Offset: 0x00015F22
		[DataMember]
		public int Id { get; set; }
	}
}
