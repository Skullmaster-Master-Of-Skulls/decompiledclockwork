using System;
using Telerik.Web.UI.ExportInfrastructure;

namespace Telerik.Web.UI
{
	// Token: 0x0200074C RID: 1868
	public class PivotGridCellExportingArgs : EventArgs
	{
		// Token: 0x17001591 RID: 5521
		// (get) Token: 0x06004232 RID: 16946 RVA: 0x000D0081 File Offset: 0x000CE281
		// (set) Token: 0x06004233 RID: 16947 RVA: 0x000D0089 File Offset: 0x000CE289
		public Cell ExportedCell
		{
			get
			{
				return this._exportedCell;
			}
			set
			{
				this._exportedCell = value;
			}
		}

		// Token: 0x17001592 RID: 5522
		// (get) Token: 0x06004234 RID: 16948 RVA: 0x000D0092 File Offset: 0x000CE292
		// (set) Token: 0x06004235 RID: 16949 RVA: 0x000D009A File Offset: 0x000CE29A
		public PivotGridBaseModelCell PivotGridModelCell
		{
			get
			{
				return this._pivotGridModelCell;
			}
			set
			{
				this._pivotGridModelCell = value;
			}
		}

		// Token: 0x17001593 RID: 5523
		// (get) Token: 0x06004236 RID: 16950 RVA: 0x000D00A3 File Offset: 0x000CE2A3
		public PivotGridCell PivotGridCell
		{
			get
			{
				return this.PivotGridModelCell.BaseCell.DataCell;
			}
		}

		// Token: 0x06004237 RID: 16951 RVA: 0x000D00B5 File Offset: 0x000CE2B5
		public PivotGridCellExportingArgs(Cell exportedCell, PivotGridBaseModelCell pivotGridModelCell)
		{
			this._exportedCell = exportedCell;
			this._pivotGridModelCell = pivotGridModelCell;
		}

		// Token: 0x0400118D RID: 4493
		private Cell _exportedCell;

		// Token: 0x0400118E RID: 4494
		private PivotGridBaseModelCell _pivotGridModelCell;
	}
}
