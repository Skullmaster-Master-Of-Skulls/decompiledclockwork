using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000692 RID: 1682
	internal class FieldSolveOrderNode
	{
		// Token: 0x06003D0E RID: 15630 RVA: 0x000C4B91 File Offset: 0x000C2D91
		public FieldSolveOrderNode()
		{
			this.Parents = new List<FieldSolveOrderNode>();
			this.Children = new List<FieldSolveOrderNode>();
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x06003D0F RID: 15631 RVA: 0x000C4BAF File Offset: 0x000C2DAF
		// (set) Token: 0x06003D10 RID: 15632 RVA: 0x000C4BB7 File Offset: 0x000C2DB7
		public int? Index { get; set; }

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06003D11 RID: 15633 RVA: 0x000C4BC0 File Offset: 0x000C2DC0
		// (set) Token: 0x06003D12 RID: 15634 RVA: 0x000C4BC8 File Offset: 0x000C2DC8
		public int LowLink { get; set; }

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06003D13 RID: 15635 RVA: 0x000C4BD1 File Offset: 0x000C2DD1
		// (set) Token: 0x06003D14 RID: 15636 RVA: 0x000C4BD9 File Offset: 0x000C2DD9
		public bool IsError { get; set; }

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06003D15 RID: 15637 RVA: 0x000C4BE2 File Offset: 0x000C2DE2
		// (set) Token: 0x06003D16 RID: 15638 RVA: 0x000C4BEA File Offset: 0x000C2DEA
		public IList<FieldSolveOrderNode> Parents { get; private set; }

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06003D17 RID: 15639 RVA: 0x000C4BF3 File Offset: 0x000C2DF3
		// (set) Token: 0x06003D18 RID: 15640 RVA: 0x000C4BFB File Offset: 0x000C2DFB
		public IList<FieldSolveOrderNode> Children { get; private set; }
	}
}
