using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200085F RID: 2143
	[DataContract(Namespace = "http://tpro.ca")]
	public class RemoveClockWorkServerJobStepReq : BaseMessageReq
	{
		// Token: 0x17000F52 RID: 3922
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x00014B35 File Offset: 0x00012D35
		// (set) Token: 0x06002BA6 RID: 11174 RVA: 0x00014B3D File Offset: 0x00012D3D
		[DataMember]
		public int JobId { get; set; }

		// Token: 0x17000F53 RID: 3923
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x00014B46 File Offset: 0x00012D46
		// (set) Token: 0x06002BA8 RID: 11176 RVA: 0x00014B4E File Offset: 0x00012D4E
		[DataMember]
		public int JobStepId { get; set; }
	}
}
