using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncData
{
	// Token: 0x020003F8 RID: 1016
	public class DataSyncDataWorkingData
	{
		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x00022CBF File Offset: 0x00020EBF
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x00022CC7 File Offset: 0x00020EC7
		public int ImportUserDataReportId { get; set; }

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00022CD0 File Offset: 0x00020ED0
		// (set) Token: 0x06001F16 RID: 7958 RVA: 0x00022CD8 File Offset: 0x00020ED8
		public IList<DataSyncMapperItemBase> Mappings { get; set; }

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00022CE1 File Offset: 0x00020EE1
		// (set) Token: 0x06001F18 RID: 7960 RVA: 0x00022CE9 File Offset: 0x00020EE9
		public IList<DynamicField> Fields { get; set; }

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x00022CF2 File Offset: 0x00020EF2
		// (set) Token: 0x06001F1A RID: 7962 RVA: 0x00022CFA File Offset: 0x00020EFA
		public IDictionary<int, List<DynamicListItem>> LookupLists { get; set; }
	}
}
