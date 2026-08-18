using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D27 RID: 3367
	internal class LevelSchemaElement : UniqueSchemaElement
	{
		// Token: 0x170027F6 RID: 10230
		// (get) Token: 0x06007D5D RID: 32093 RVA: 0x001CB91C File Offset: 0x001C9B1C
		// (set) Token: 0x06007D5E RID: 32094 RVA: 0x001CB924 File Offset: 0x001C9B24
		public string DimensionUniqueName { get; set; }

		// Token: 0x170027F7 RID: 10231
		// (get) Token: 0x06007D5F RID: 32095 RVA: 0x001CB92D File Offset: 0x001C9B2D
		// (set) Token: 0x06007D60 RID: 32096 RVA: 0x001CB935 File Offset: 0x001C9B35
		public string HierarchyUniqueName { get; set; }
	}
}
