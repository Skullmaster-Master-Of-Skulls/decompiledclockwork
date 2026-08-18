using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x0200093E RID: 2366
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentCancelInfoReq : BaseMessageReq
	{
		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x00017B93 File Offset: 0x00015D93
		// (set) Token: 0x0600308E RID: 12430 RVA: 0x00017B9B File Offset: 0x00015D9B
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x00017BA4 File Offset: 0x00015DA4
		// (set) Token: 0x06003090 RID: 12432 RVA: 0x00017BAC File Offset: 0x00015DAC
		[DataMember]
		public AppCancelInfoDTO AppCancelInfo { get; set; }
	}
}
