using System;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004D4 RID: 1236
	public class ExternalAppointmentId
	{
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x000280FC File Offset: 0x000262FC
		// (set) Token: 0x0600253F RID: 9535 RVA: 0x00028104 File Offset: 0x00026304
		public int ClockWorkAppId { get; set; }

		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x0002810D File Offset: 0x0002630D
		// (set) Token: 0x06002541 RID: 9537 RVA: 0x00028115 File Offset: 0x00026315
		public string GlobalAppId { get; set; }

		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x0002811E File Offset: 0x0002631E
		// (set) Token: 0x06002543 RID: 9539 RVA: 0x00028126 File Offset: 0x00026326
		public string UniqueId { get; set; }

		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06002544 RID: 9540 RVA: 0x0002812F File Offset: 0x0002632F
		// (set) Token: 0x06002545 RID: 9541 RVA: 0x00028137 File Offset: 0x00026337
		public string UniqueId2 { get; set; }
	}
}
