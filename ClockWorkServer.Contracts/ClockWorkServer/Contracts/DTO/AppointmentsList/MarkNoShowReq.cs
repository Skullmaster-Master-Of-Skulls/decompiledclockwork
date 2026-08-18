using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AEA RID: 2794
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkNoShowReq : BaseMessageReq
	{
		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06003B16 RID: 15126 RVA: 0x0001CC44 File Offset: 0x0001AE44
		// (set) Token: 0x06003B17 RID: 15127 RVA: 0x0001CC4C File Offset: 0x0001AE4C
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06003B18 RID: 15128 RVA: 0x0001CC55 File Offset: 0x0001AE55
		// (set) Token: 0x06003B19 RID: 15129 RVA: 0x0001CC5D File Offset: 0x0001AE5D
		[DataMember]
		public bool NewNoShow { get; set; }
	}
}
