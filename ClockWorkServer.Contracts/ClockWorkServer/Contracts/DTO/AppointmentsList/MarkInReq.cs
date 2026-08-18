using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsList
{
	// Token: 0x02000AE9 RID: 2793
	[DataContract(Namespace = "http://tpro.ca")]
	public class MarkInReq : BaseMessageReq
	{
		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x0001CC22 File Offset: 0x0001AE22
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x0001CC2A File Offset: 0x0001AE2A
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x0001CC33 File Offset: 0x0001AE33
		// (set) Token: 0x06003B14 RID: 15124 RVA: 0x0001CC3B File Offset: 0x0001AE3B
		[DataMember]
		public bool NewIn { get; set; }
	}
}
