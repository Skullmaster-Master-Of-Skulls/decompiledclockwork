using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000D2D RID: 3373
	internal class OlapCatalogInfo
	{
		// Token: 0x06007D88 RID: 32136 RVA: 0x001CBBBD File Offset: 0x001C9DBD
		public OlapCatalogInfo()
		{
			this.Cubes = new List<OlapCubeInfo>();
		}

		// Token: 0x17002806 RID: 10246
		// (get) Token: 0x06007D89 RID: 32137 RVA: 0x001CBBD0 File Offset: 0x001C9DD0
		// (set) Token: 0x06007D8A RID: 32138 RVA: 0x001CBBD8 File Offset: 0x001C9DD8
		public IList<OlapCubeInfo> Cubes { get; private set; }

		// Token: 0x17002807 RID: 10247
		// (get) Token: 0x06007D8B RID: 32139 RVA: 0x001CBBE1 File Offset: 0x001C9DE1
		// (set) Token: 0x06007D8C RID: 32140 RVA: 0x001CBBE9 File Offset: 0x001C9DE9
		public string Name { get; set; }

		// Token: 0x17002808 RID: 10248
		// (get) Token: 0x06007D8D RID: 32141 RVA: 0x001CBBF2 File Offset: 0x001C9DF2
		// (set) Token: 0x06007D8E RID: 32142 RVA: 0x001CBBFA File Offset: 0x001C9DFA
		public string Description { get; set; }
	}
}
