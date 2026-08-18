using System;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006A1 RID: 1697
	internal struct GroupMapResult
	{
		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x06003D3D RID: 15677 RVA: 0x000C55D6 File Offset: 0x000C37D6
		// (set) Token: 0x06003D3E RID: 15678 RVA: 0x000C55DE File Offset: 0x000C37DE
		public bool Success { get; set; }

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x000C55E7 File Offset: 0x000C37E7
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x000C55EF File Offset: 0x000C37EF
		public PivotAxis Axis { get; set; }

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000C55F8 File Offset: 0x000C37F8
		// (set) Token: 0x06003D42 RID: 15682 RVA: 0x000C5600 File Offset: 0x000C3800
		public int Index { get; set; }
	}
}
