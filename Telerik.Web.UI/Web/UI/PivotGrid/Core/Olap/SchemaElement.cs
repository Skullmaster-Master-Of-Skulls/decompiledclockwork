using System;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000716 RID: 1814
	internal class SchemaElement
	{
		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x000CAD3E File Offset: 0x000C8F3E
		// (set) Token: 0x06004068 RID: 16488 RVA: 0x000CAD46 File Offset: 0x000C8F46
		public string Name { get; internal set; }

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x000CAD4F File Offset: 0x000C8F4F
		// (set) Token: 0x0600406A RID: 16490 RVA: 0x000CAD57 File Offset: 0x000C8F57
		public string Caption { get; internal set; }

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x000CAD60 File Offset: 0x000C8F60
		// (set) Token: 0x0600406C RID: 16492 RVA: 0x000CAD68 File Offset: 0x000C8F68
		public string CubeName { get; internal set; }

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x000CAD71 File Offset: 0x000C8F71
		// (set) Token: 0x0600406E RID: 16494 RVA: 0x000CAD79 File Offset: 0x000C8F79
		public string CatalogName { get; internal set; }
	}
}
