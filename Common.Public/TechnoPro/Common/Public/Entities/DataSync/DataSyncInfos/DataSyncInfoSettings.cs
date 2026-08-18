using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos
{
	// Token: 0x020003E0 RID: 992
	public class DataSyncInfoSettings
	{
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06001EA1 RID: 7841 RVA: 0x000220F2 File Offset: 0x000202F2
		// (set) Token: 0x06001EA2 RID: 7842 RVA: 0x000220FA File Offset: 0x000202FA
		public bool OverwriteClockWorkValuesWithExternalEmptyValue { get; set; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x00022103 File Offset: 0x00020303
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x0002210B File Offset: 0x0002030B
		public eDynamicFormType DynamicFormType { get; set; }

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00022114 File Offset: 0x00020314
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x0002211C File Offset: 0x0002031C
		public bool AutomaticallyCreateLookupListItemsIfTheyDontExistInClockWork { get; set; }
	}
}
