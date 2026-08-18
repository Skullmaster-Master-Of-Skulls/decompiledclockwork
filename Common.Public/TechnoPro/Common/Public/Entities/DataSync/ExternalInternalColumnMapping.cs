using System;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003C7 RID: 967
	public class ExternalInternalColumnMapping
	{
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x000214D1 File Offset: 0x0001F6D1
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x000214D9 File Offset: 0x0001F6D9
		public string ClockWorkTableName { get; set; }

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x000214E2 File Offset: 0x0001F6E2
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x000214EA File Offset: 0x0001F6EA
		public string ClockWorkColumnName { get; set; }

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x000214F3 File Offset: 0x0001F6F3
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x000214FB File Offset: 0x0001F6FB
		public string ExternalColumnName { get; set; }

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x00021504 File Offset: 0x0001F704
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0002150C File Offset: 0x0001F70C
		public bool IsClockWorkDataEncrypted { get; set; }
	}
}
