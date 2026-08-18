using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000662 RID: 1634
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateIconForPerAppointmentDataChangeReq : BaseMessageReq
	{
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x0000F14D File Offset: 0x0000D34D
		// (set) Token: 0x06002138 RID: 8504 RVA: 0x0000F155 File Offset: 0x0000D355
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x0000F15E File Offset: 0x0000D35E
		// (set) Token: 0x0600213A RID: 8506 RVA: 0x0000F166 File Offset: 0x0000D366
		[DataMember]
		public int IconId { get; set; }

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x0000F16F File Offset: 0x0000D36F
		// (set) Token: 0x0600213C RID: 8508 RVA: 0x0000F177 File Offset: 0x0000D377
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600213D RID: 8509 RVA: 0x0000F180 File Offset: 0x0000D380
		// (set) Token: 0x0600213E RID: 8510 RVA: 0x0000F188 File Offset: 0x0000D388
		[DataMember]
		public int ControlIdToActivate { get; set; }
	}
}
