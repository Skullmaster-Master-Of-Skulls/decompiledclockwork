using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD0 RID: 3536
	public class PivotGridCellCreatedEventArgs : EventArgs
	{
		// Token: 0x0600838D RID: 33677 RVA: 0x001DFDC2 File Offset: 0x001DDFC2
		public PivotGridCellCreatedEventArgs(PivotGridCell cell)
		{
			this.Cell = cell;
		}

		// Token: 0x1700298A RID: 10634
		// (get) Token: 0x0600838E RID: 33678 RVA: 0x001DFDD1 File Offset: 0x001DDFD1
		// (set) Token: 0x0600838F RID: 33679 RVA: 0x001DFDD9 File Offset: 0x001DDFD9
		public PivotGridCell Cell { get; internal set; }
	}
}
