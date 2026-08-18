using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003F7 RID: 1015
	public class DataSyncDataLoadedItem
	{
		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00022C9D File Offset: 0x00020E9D
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x00022CA5 File Offset: 0x00020EA5
		public DataSyncMapperItemBase MapperItem { get; set; }

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x00022CAE File Offset: 0x00020EAE
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00022CB6 File Offset: 0x00020EB6
		public DataSyncDataItemBase DataItem { get; set; }
	}
}
