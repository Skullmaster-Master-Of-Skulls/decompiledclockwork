using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AEB RID: 2795
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkConfirmedReq : BaseMessageReq
	{
		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x0001CC66 File Offset: 0x0001AE66
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x0001CC6E File Offset: 0x0001AE6E
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x0001CC77 File Offset: 0x0001AE77
		// (set) Token: 0x06003B1E RID: 15134 RVA: 0x0001CC7F File Offset: 0x0001AE7F
		[DataMember]
		public bool NewConfirmed { get; set; }
	}
}
