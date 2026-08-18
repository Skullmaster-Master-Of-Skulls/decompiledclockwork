using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003D7 RID: 983
	public class DataSyncPreviewResult
	{
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x00021F0C File Offset: 0x0002010C
		// (set) Token: 0x06001E62 RID: 7778 RVA: 0x00021F14 File Offset: 0x00020114
		public eDataSyncStatus Status { get; set; }

		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x00021F1D File Offset: 0x0002011D
		// (set) Token: 0x06001E64 RID: 7780 RVA: 0x00021F25 File Offset: 0x00020125
		public DataSyncError SyncError { get; set; }

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x00021F2E File Offset: 0x0002012E
		// (set) Token: 0x06001E66 RID: 7782 RVA: 0x00021F36 File Offset: 0x00020136
		public IList<DataSyncExternalData> Data { get; set; }
	}
}
