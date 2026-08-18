using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE8 RID: 2792
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAppointmentByIdReq : BaseMessageReq
	{
		// Token: 0x170015A7 RID: 5543
		// (get) Token: 0x06003B0C RID: 15116 RVA: 0x0001CC00 File Offset: 0x0001AE00
		// (set) Token: 0x06003B0D RID: 15117 RVA: 0x0001CC08 File Offset: 0x0001AE08
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06003B0E RID: 15118 RVA: 0x0001CC11 File Offset: 0x0001AE11
		// (set) Token: 0x06003B0F RID: 15119 RVA: 0x0001CC19 File Offset: 0x0001AE19
		[DataMember]
		public bool LoadIsStudentsFirstAppointment { get; set; }
	}
}
