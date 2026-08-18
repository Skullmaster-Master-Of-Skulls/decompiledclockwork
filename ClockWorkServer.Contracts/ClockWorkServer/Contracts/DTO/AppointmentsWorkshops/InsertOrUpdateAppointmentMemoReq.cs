using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000914 RID: 2324
	[DataContract(Namespace = "http://tpro.ca")]
	public class InsertOrUpdateAppointmentMemoReq : BaseMessageReq
	{
		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x00016607 File Offset: 0x00014807
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x0001660F File Offset: 0x0001480F
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x00016618 File Offset: 0x00014818
		// (set) Token: 0x06002F21 RID: 12065 RVA: 0x00016620 File Offset: 0x00014820
		[DataMember]
		public string MemoText { get; set; }
	}
}
