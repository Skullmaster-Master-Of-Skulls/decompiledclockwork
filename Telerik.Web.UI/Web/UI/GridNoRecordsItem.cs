using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200114E RID: 4430
	public class GridNoRecordsItem : GridItem
	{
		// Token: 0x0600B460 RID: 46176 RVA: 0x00277A21 File Offset: 0x00275C21
		public GridNoRecordsItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.NoRecordsItem)
		{
		}

		// Token: 0x17003A49 RID: 14921
		// (get) Token: 0x0600B461 RID: 46177 RVA: 0x00277A2E File Offset: 0x00275C2E
		public TableCell TemplateCell
		{
			get
			{
				return this._templateCell;
			}
		}

		// Token: 0x0600B462 RID: 46178 RVA: 0x00277A36 File Offset: 0x00275C36
		public override void PrepareItemStyle()
		{
			if (this.TemplateCell == null)
			{
				return;
			}
			this.TemplateCell.ColumnSpan = base.CalcColSpan(base.OwnerTableView.RenderColumns, this.calculatedColumnIndex, -1);
			base.PrepareItemStyle();
		}

		// Token: 0x0600B463 RID: 46179 RVA: 0x00277A6C File Offset: 0x00275C6C
		public override void Initialize(GridColumn[] columns)
		{
			int i;
			for (i = 0; i < base.OwnerTableView.GroupByExpressions.Count; i++)
			{
				this.Cells.Add(this.CreateCellObject());
			}
			this.Cells.Add(this.CreateCellObject());
			i++;
			this.Cells.Add(this.CreateCellObject());
			this.calculatedColumnIndex = i + 1;
			TableCell tableCell = new TableCell();
			this.Cells.Add(tableCell);
			tableCell.ColumnSpan = base.CalcColSpan(columns, this.calculatedColumnIndex, -1);
			if (base.OwnerTableView.Dir == GridTableTextDirection.LTR)
			{
				tableCell.Attributes.CssStyle.Add("text-align", "left");
			}
			else
			{
				tableCell.Attributes.CssStyle.Add("text-align", "right");
			}
			this._templateCell = tableCell;
		}

		// Token: 0x0600B464 RID: 46180 RVA: 0x00277B48 File Offset: 0x00275D48
		public override void SetupItem(bool dataBind, object dataItem, GridColumn[] columns, ControlCollection rows)
		{
			rows.Add(this);
			this.Initialize(columns);
			if (base.OwnerTableView.EnableNoRecordsTemplate)
			{
				base.OwnerTableView.GetNoRecordsTemplateInternal().InstantiateIn(this.TemplateCell);
			}
			GridItemEventArgs e = new GridItemEventArgs(this, new GridItemCreated());
			base.OwnerTableView.OwnerGrid.CallOnItemCreated(e);
		}

		// Token: 0x04002F88 RID: 12168
		private TableCell _templateCell;

		// Token: 0x04002F89 RID: 12169
		private int calculatedColumnIndex;
	}
}
