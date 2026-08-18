using System;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos
{
	// Token: 0x020003DF RID: 991
	public class DataSyncInfoMapItem
	{
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x000220BF File Offset: 0x000202BF
		// (set) Token: 0x06001E9B RID: 7835 RVA: 0x000220C7 File Offset: 0x000202C7
		public int ClockWorkControlId { get; set; }

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x000220D0 File Offset: 0x000202D0
		// (set) Token: 0x06001E9D RID: 7837 RVA: 0x000220D8 File Offset: 0x000202D8
		public int ClockWorkSecondaryId { get; set; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x000220E1 File Offset: 0x000202E1
		// (set) Token: 0x06001E9F RID: 7839 RVA: 0x000220E9 File Offset: 0x000202E9
		public string ExternalFieldName { get; set; }
	}
}
