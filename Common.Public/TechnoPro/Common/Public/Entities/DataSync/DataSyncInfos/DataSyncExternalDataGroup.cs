using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos
{
	// Token: 0x020003DC RID: 988
	public class DataSyncExternalDataGroup
	{
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00022004 File Offset: 0x00020204
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x0002200C File Offset: 0x0002020C
		public IList<DataSyncExternalData> Items { get; set; }

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x00022015 File Offset: 0x00020215
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x0002201D File Offset: 0x0002021D
		public int ClockWorkControlId { get; set; }
	}
}
