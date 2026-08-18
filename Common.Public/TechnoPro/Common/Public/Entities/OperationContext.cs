using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public class OperationContext
	{
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000E3E1 File Offset: 0x0000C5E1
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000E3E9 File Offset: 0x0000C5E9
		public virtual int WhoAmI { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000E3F2 File Offset: 0x0000C5F2
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000E3FA File Offset: 0x0000C5FA
		public ApplicationContext AppContext { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000E403 File Offset: 0x0000C603
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000E40B File Offset: 0x0000C60B
		public string TenantId { get; set; }
	}
}
