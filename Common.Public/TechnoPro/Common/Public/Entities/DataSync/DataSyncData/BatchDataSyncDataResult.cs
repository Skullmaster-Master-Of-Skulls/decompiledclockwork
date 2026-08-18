using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003F5 RID: 1013
	public class BatchDataSyncDataResult
	{
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x00022C59 File Offset: 0x00020E59
		// (set) Token: 0x06001F05 RID: 7941 RVA: 0x00022C61 File Offset: 0x00020E61
		public IDictionary<string, eDataSyncDataItemStatus> BatchResults { get; set; }
	}
}
