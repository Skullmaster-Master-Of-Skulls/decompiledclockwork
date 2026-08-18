using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200066B RID: 1643
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeAssignedAdvisorBatchReq : BaseMessageReq
	{
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0000F26E File Offset: 0x0000D46E
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x0000F276 File Offset: 0x0000D476
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0000F27F File Offset: 0x0000D47F
		// (set) Token: 0x06002165 RID: 8549 RVA: 0x0000F287 File Offset: 0x0000D487
		[DataMember]
		public int OldAdvisorPersonId { get; set; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x0000F290 File Offset: 0x0000D490
		// (set) Token: 0x06002167 RID: 8551 RVA: 0x0000F298 File Offset: 0x0000D498
		[DataMember]
		public int NewAdvisorPersonId { get; set; }
	}
}
