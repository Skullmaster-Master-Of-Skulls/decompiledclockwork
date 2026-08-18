using System;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003D8 RID: 984
	public class DataSyncResult
	{
		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x00021F3F File Offset: 0x0002013F
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x00021F47 File Offset: 0x00020147
		public eDataSyncStatus Status { get; set; }

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x00021F50 File Offset: 0x00020150
		// (set) Token: 0x06001E6B RID: 7787 RVA: 0x00021F58 File Offset: 0x00020158
		public DataSyncError SyncError { get; set; }
	}
}
