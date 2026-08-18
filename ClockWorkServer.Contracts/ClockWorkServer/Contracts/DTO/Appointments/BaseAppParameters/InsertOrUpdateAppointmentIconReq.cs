using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000952 RID: 2386
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentIconReq : BaseMessageReq
	{
		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x00017CF8 File Offset: 0x00015EF8
		// (set) Token: 0x060030CC RID: 12492 RVA: 0x00017D00 File Offset: 0x00015F00
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x00017D09 File Offset: 0x00015F09
		// (set) Token: 0x060030CE RID: 12494 RVA: 0x00017D11 File Offset: 0x00015F11
		[DataMember]
		public AppointmentIconDTO AppIcon { get; set; }
	}
}
