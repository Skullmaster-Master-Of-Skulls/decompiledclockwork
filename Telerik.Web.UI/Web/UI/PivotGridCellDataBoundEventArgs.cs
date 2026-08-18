using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000DD1 RID: 3537
	public class PivotGridCellDataBoundEventArgs : EventArgs
	{
		// Token: 0x06008390 RID: 33680 RVA: 0x001DFDE2 File Offset: 0x001DDFE2
		public PivotGridCellDataBoundEventArgs(PivotGridCell cell)
		{
			this.Cell = cell;
		}

		// Token: 0x1700298B RID: 10635
		// (get) Token: 0x06008391 RID: 33681 RVA: 0x001DFDF1 File Offset: 0x001DDFF1
		// (set) Token: 0x06008392 RID: 33682 RVA: 0x001DFDF9 File Offset: 0x001DDFF9
		public PivotGridCell Cell { get; internal set; }
	}
}
