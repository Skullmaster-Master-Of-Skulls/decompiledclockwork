using System;

namespace TechnoPro.Common.Core.FlatFileDataSync
{
	// Token: 0x02000005 RID: 5
	public class ExternalInternalColumnMapping
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00003014 File Offset: 0x00001214
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000301C File Offset: 0x0000121C
		public string ClockWorkTableName { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00003025 File Offset: 0x00001225
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000302D File Offset: 0x0000122D
		public string ClockWorkColumnName { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00003036 File Offset: 0x00001236
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000303E File Offset: 0x0000123E
		public string ExternalColumnName { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003047 File Offset: 0x00001247
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000304F File Offset: 0x0000124F
		public bool IsClockWorkDataEncrypted { get; set; }
	}
}
