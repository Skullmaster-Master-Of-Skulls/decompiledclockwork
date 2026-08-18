using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.Legacy
{
	// Token: 0x02000378 RID: 888
	public class LegacySaveDataResult
	{
		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06001B84 RID: 7044 RVA: 0x0001F5E1 File Offset: 0x0001D7E1
		// (set) Token: 0x06001B85 RID: 7045 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		public int PersonId { get; set; }

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06001B86 RID: 7046 RVA: 0x0001F5F2 File Offset: 0x0001D7F2
		// (set) Token: 0x06001B87 RID: 7047 RVA: 0x0001F5FA File Offset: 0x0001D7FA
		public int ControlId { get; set; }

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x06001B88 RID: 7048 RVA: 0x0001F603 File Offset: 0x0001D803
		// (set) Token: 0x06001B89 RID: 7049 RVA: 0x0001F60B File Offset: 0x0001D80B
		public Exception Exception { get; set; }
	}
}
