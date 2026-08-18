using System;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x02000184 RID: 388
	[SchemaElementName("OutputCache")]
	internal class OutputCacheAsix
	{
		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0005411E File Offset: 0x0005231E
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x00054126 File Offset: 0x00052326
		[Filterable(false)]
		public bool DiskCacheable { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0005412F File Offset: 0x0005232F
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x00054137 File Offset: 0x00052337
		[Filterable(false)]
		public int Duration { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x00054140 File Offset: 0x00052340
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x00054148 File Offset: 0x00052348
		[Filterable(false)]
		public OutputCacheLocation Location { get; set; }

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x00054151 File Offset: 0x00052351
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x00054159 File Offset: 0x00052359
		[Filterable(false)]
		public string SqlDependency { get; set; }

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x00054162 File Offset: 0x00052362
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x0005416A File Offset: 0x0005236A
		[Filterable(false)]
		public string VaryByContentEncoding { get; set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x00054173 File Offset: 0x00052373
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0005417B File Offset: 0x0005237B
		[Filterable(false)]
		public string VaryByCustom { get; set; }

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x00054184 File Offset: 0x00052384
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0005418C File Offset: 0x0005238C
		[Filterable(false)]
		public string VaryByHeader { get; set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00054195 File Offset: 0x00052395
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0005419D File Offset: 0x0005239D
		[Filterable(false)]
		public string VaryByParam { get; set; }
	}
}
