using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.DataSync.DataSyncInfos
{
	// Token: 0x020003DD RID: 989
	public class DataSyncInfoAction
	{
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x00022026 File Offset: 0x00020226
		// (set) Token: 0x06001E87 RID: 7815 RVA: 0x0002202E File Offset: 0x0002022E
		public eDataSyncInfoActionType ActionType { get; set; }

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x00022037 File Offset: 0x00020237
		// (set) Token: 0x06001E89 RID: 7817 RVA: 0x0002203F File Offset: 0x0002023F
		public DataSyncExternalData ExternalData { get; set; }

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x00022048 File Offset: 0x00020248
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x00022050 File Offset: 0x00020250
		public object ValueToWrite { get; set; }

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x00022059 File Offset: 0x00020259
		// (set) Token: 0x06001E8D RID: 7821 RVA: 0x00022061 File Offset: 0x00020261
		public eDynamicFormType ClockWorkDataType { get; set; }
	}
}
