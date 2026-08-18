using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentSync
{
	// Token: 0x020004DA RID: 1242
	public class ClockWorkExternalApplicationSyncUser
	{
		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x00028250 File Offset: 0x00026450
		// (set) Token: 0x0600256C RID: 9580 RVA: 0x00028258 File Offset: 0x00026458
		public PersonBase ClockWorkUser { get; set; }

		// Token: 0x17000F85 RID: 3973
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x00028261 File Offset: 0x00026461
		// (set) Token: 0x0600256E RID: 9582 RVA: 0x00028269 File Offset: 0x00026469
		public string ExternalApplicationUsername { get; set; }

		// Token: 0x17000F86 RID: 3974
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x00028272 File Offset: 0x00026472
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x0002827A File Offset: 0x0002647A
		public bool SyncIsEnabled { get; set; }
	}
}
