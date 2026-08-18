using System;
using Telerik.Web.UI.PivotGrid.Core.Fields;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006A3 RID: 1699
	internal struct MapResult
	{
		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06003D44 RID: 15684 RVA: 0x000C5609 File Offset: 0x000C3809
		// (set) Token: 0x06003D45 RID: 15685 RVA: 0x000C5611 File Offset: 0x000C3811
		public bool Success { get; set; }

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06003D46 RID: 15686 RVA: 0x000C561A File Offset: 0x000C381A
		// (set) Token: 0x06003D47 RID: 15687 RVA: 0x000C5622 File Offset: 0x000C3822
		public FieldRoles Role { get; set; }

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06003D48 RID: 15688 RVA: 0x000C562B File Offset: 0x000C382B
		// (set) Token: 0x06003D49 RID: 15689 RVA: 0x000C5633 File Offset: 0x000C3833
		public int Level { get; set; }
	}
}
