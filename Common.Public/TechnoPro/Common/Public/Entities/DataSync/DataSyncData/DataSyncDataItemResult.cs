using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003F4 RID: 1012
	public class DataSyncDataItemResult
	{
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00022C04 File Offset: 0x00020E04
		// (set) Token: 0x06001EFA RID: 7930 RVA: 0x00022C0C File Offset: 0x00020E0C
		public DataSyncDataItemBase ExternalData { get; set; }

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00022C15 File Offset: 0x00020E15
		// (set) Token: 0x06001EFC RID: 7932 RVA: 0x00022C1D File Offset: 0x00020E1D
		public DataSyncDataItemBase ClockWorkData { get; set; }

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x00022C26 File Offset: 0x00020E26
		// (set) Token: 0x06001EFE RID: 7934 RVA: 0x00022C2E File Offset: 0x00020E2E
		public eDataSyncDataItemStatus ResultStatus { get; set; }

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00022C37 File Offset: 0x00020E37
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x00022C3F File Offset: 0x00020E3F
		public string ResultMessage { get; set; }

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00022C48 File Offset: 0x00020E48
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x00022C50 File Offset: 0x00020E50
		public eDataSyncDataItemChangeStatus ChangeStatus { get; set; }
	}
}
