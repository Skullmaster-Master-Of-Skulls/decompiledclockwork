using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003F6 RID: 1014
	public class DataSyncDataItemJob
	{
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x00022C6A File Offset: 0x00020E6A
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x00022C72 File Offset: 0x00020E72
		public eDataSyncDataItemChangeStatus ChangeAction { get; set; }

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x00022C7B File Offset: 0x00020E7B
		// (set) Token: 0x06001F0A RID: 7946 RVA: 0x00022C83 File Offset: 0x00020E83
		public DataSyncMapperItemBase MapperItem { get; set; }

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x00022C8C File Offset: 0x00020E8C
		// (set) Token: 0x06001F0C RID: 7948 RVA: 0x00022C94 File Offset: 0x00020E94
		public DataSyncDataItemBase DataItem { get; set; }
	}
}
