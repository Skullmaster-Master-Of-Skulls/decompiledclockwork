using System;
using System.Text;

namespace Telerik.Web.UI
{
	// Token: 0x02000C27 RID: 3111
	public class PivotGridDataCell : PivotGridCell
	{
		// Token: 0x0600762A RID: 30250 RVA: 0x001B70EC File Offset: 0x001B52EC
		public PivotGridDataCell(RadPivotGrid ownerPivotGrid) : base(ownerPivotGrid)
		{
		}

		// Token: 0x1700266B RID: 9835
		// (get) Token: 0x0600762B RID: 30251 RVA: 0x001B70F5 File Offset: 0x001B52F5
		// (set) Token: 0x0600762C RID: 30252 RVA: 0x001B70FD File Offset: 0x001B52FD
		public int RowIndex { get; internal set; }

		// Token: 0x1700266C RID: 9836
		// (get) Token: 0x0600762D RID: 30253 RVA: 0x001B7106 File Offset: 0x001B5306
		// (set) Token: 0x0600762E RID: 30254 RVA: 0x001B710E File Offset: 0x001B530E
		public int ColumnIndex { get; internal set; }

		// Token: 0x1700266D RID: 9837
		// (get) Token: 0x0600762F RID: 30255 RVA: 0x001B7117 File Offset: 0x001B5317
		// (set) Token: 0x06007630 RID: 30256 RVA: 0x001B711F File Offset: 0x001B531F
		public object[] ParentRowIndexes { get; set; }

		// Token: 0x1700266E RID: 9838
		// (get) Token: 0x06007631 RID: 30257 RVA: 0x001B7128 File Offset: 0x001B5328
		// (set) Token: 0x06007632 RID: 30258 RVA: 0x001B7130 File Offset: 0x001B5330
		public object[] ParentColumnIndexes { get; set; }

		// Token: 0x1700266F RID: 9839
		// (get) Token: 0x06007633 RID: 30259 RVA: 0x001B7139 File Offset: 0x001B5339
		public override bool CanExpand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002670 RID: 9840
		// (get) Token: 0x06007634 RID: 30260 RVA: 0x001B713C File Offset: 0x001B533C
		// (set) Token: 0x06007635 RID: 30261 RVA: 0x001B7144 File Offset: 0x001B5344
		public string FormattedValue { get; set; }

		// Token: 0x06007636 RID: 30262 RVA: 0x001B7150 File Offset: 0x001B5350
		public string GetToolTipString(string text)
		{
			if (this.ParentColumnIndexes == null || this.ParentRowIndexes == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (this.ParentColumnIndexes.Length > 0)
			{
				stringBuilder.AppendFormat("{0};", this.ParentColumnIndexes[this.ParentColumnIndexes.Length - 1]);
			}
			stringBuilder.AppendFormat("{0}{1};", base.OwnerPivotGrid.Localization.ToolTipsValueText, text);
			stringBuilder.Append(base.OwnerPivotGrid.Localization.ToolTipsRowText);
			for (int i = 0; i < this.ParentRowIndexes.Length; i++)
			{
				object obj = this.ParentRowIndexes[i];
				stringBuilder.Append(obj.ToString());
				if (i < this.ParentRowIndexes.Length - 1)
				{
					stringBuilder.Append(" - ");
				}
			}
			stringBuilder.Append(";");
			stringBuilder.Append(base.OwnerPivotGrid.Localization.ToolTipsColumnText);
			for (int j = 0; j < this.ParentColumnIndexes.Length; j++)
			{
				object obj2 = this.ParentColumnIndexes[j];
				stringBuilder.Append(obj2.ToString());
				if (j < this.ParentColumnIndexes.Length - 1)
				{
					stringBuilder.Append(" - ");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x17002671 RID: 9841
		// (get) Token: 0x06007637 RID: 30263 RVA: 0x001B7284 File Offset: 0x001B5484
		// (set) Token: 0x06007638 RID: 30264 RVA: 0x001B728C File Offset: 0x001B548C
		public PivotGridDataCellType CellType { get; set; }

		// Token: 0x17002672 RID: 9842
		// (get) Token: 0x06007639 RID: 30265 RVA: 0x001B7298 File Offset: 0x001B5498
		public bool IsTotalCell
		{
			get
			{
				bool result = false;
				if (this.CellType == PivotGridDataCellType.ColumnTotalDataCell || this.CellType == PivotGridDataCellType.RowTotalDataCell || this.CellType == PivotGridDataCellType.RowAndColumnTotal || this.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal || this.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x17002673 RID: 9843
		// (get) Token: 0x0600763A RID: 30266 RVA: 0x001B72D8 File Offset: 0x001B54D8
		public bool IsGrandTotalCell
		{
			get
			{
				bool result = false;
				if (this.CellType == PivotGridDataCellType.ColumnGrandTotalDataCell || this.CellType == PivotGridDataCellType.RowGrandTotalDataCell || this.CellType == PivotGridDataCellType.RowAndColumnGrandTotal || this.CellType == PivotGridDataCellType.RowGrandTotalColumnTotal || this.CellType == PivotGridDataCellType.ColumnGrandTotalRowTotal)
				{
					result = true;
				}
				return result;
			}
		}
	}
}
