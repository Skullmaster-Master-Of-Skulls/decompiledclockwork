using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020019F9 RID: 6649
	public class GridGroupFooterItem : GridItem
	{
		// Token: 0x17004DB2 RID: 19890
		// (get) Token: 0x06010187 RID: 65927 RVA: 0x0039E35A File Offset: 0x0039C55A
		// (set) Token: 0x06010188 RID: 65928 RVA: 0x0039E375 File Offset: 0x0039C575
		public IDictionary AggregatesValues
		{
			get
			{
				if (this.aggregatesValues == null)
				{
					this.aggregatesValues = new ListDictionary();
				}
				return this.aggregatesValues;
			}
			set
			{
				this.aggregatesValues = (ListDictionary)value;
			}
		}

		// Token: 0x17004DB3 RID: 19891
		// (get) Token: 0x06010189 RID: 65929 RVA: 0x0039E383 File Offset: 0x0039C583
		// (set) Token: 0x0601018A RID: 65930 RVA: 0x0039E38B File Offset: 0x0039C58B
		public virtual GridGroupHeaderItem GroupHeaderItem { get; internal set; }

		// Token: 0x0601018B RID: 65931 RVA: 0x0039E394 File Offset: 0x0039C594
		internal GridGroupAggregateObject CreateGroupAggregateObject()
		{
			if (this.AggregatesValues != null && this.AggregatesValues.Count > 0)
			{
				return new GridGroupAggregateObject(this.AggregatesValues);
			}
			return null;
		}

		// Token: 0x17004DB4 RID: 19892
		// (get) Token: 0x0601018C RID: 65932 RVA: 0x0039E3BC File Offset: 0x0039C5BC
		public override object DataItem
		{
			get
			{
				ITemplate groupFooterTemplate = base.OwnerTableView.GroupFooterTemplate;
				if (groupFooterTemplate != null)
				{
					return this.CreateGroupAggregateObject();
				}
				return base.DataItem;
			}
		}

		// Token: 0x0601018D RID: 65933 RVA: 0x0039E3E8 File Offset: 0x0039C5E8
		public override void Initialize(GridColumn[] columns)
		{
			ITemplate groupFooterTemplate = base.OwnerTableView.GroupFooterTemplate;
			if (groupFooterTemplate != null)
			{
				int num = 0;
				TableCellCollection cells = this.Cells;
				TableCell tableCell;
				foreach (GridColumn gridColumn in columns)
				{
					tableCell = this.CreateCellObject();
					cells.Add(tableCell);
					gridColumn.InitializeCell(tableCell, num, this);
					cells.Remove(tableCell);
					GridGroupSplitterColumn gridGroupSplitterColumn = gridColumn as GridGroupSplitterColumn;
					if (gridGroupSplitterColumn != null && gridGroupSplitterColumn.CorrespondingExpression.Index != base.GroupLevel)
					{
						num++;
					}
				}
				for (int j = 0; j < base.OwnerTableView.GroupByExpressions.Count; j++)
				{
					tableCell = this.CreateCellObject();
					cells.Add(tableCell);
				}
				base.OwnerTableView.previousGroupFooterItemLevel = base.GroupLevel;
				int fromCellIndex = num + 1;
				tableCell = this.CreateCellObject();
				tableCell.ColumnSpan = base.CalcColSpan(columns, fromCellIndex, -1);
				cells.Add(tableCell);
				groupFooterTemplate.InstantiateIn(tableCell);
				return;
			}
			base.Initialize(columns);
		}

		// Token: 0x0601018E RID: 65934 RVA: 0x0039E4E6 File Offset: 0x0039C6E6
		public GridGroupFooterItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.GroupFooter)
		{
		}

		// Token: 0x0601018F RID: 65935 RVA: 0x0039E4F4 File Offset: 0x0039C6F4
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.OwnerTableView.OwnerGrid.EmptySkin())
			{
				this.CssClass = string.Format("{0}{1}", base.OwnerTableView.RenderFooterStyle.CssClass, (!string.IsNullOrEmpty(this.CssClass)) ? (" " + this.CssClass) : "");
			}
			base.Render(writer);
		}

		// Token: 0x17004DB5 RID: 19893
		public TableCell this[string columnUniqueName]
		{
			get
			{
				GridColumn[] renderColumns = base.OwnerTableView.RenderColumns;
				int num = 0;
				bool flag = false;
				foreach (GridColumn gridColumn in renderColumns)
				{
					if (gridColumn.UniqueName.Trim().ToUpper() == columnUniqueName.Trim().ToUpper())
					{
						flag = true;
						break;
					}
					num++;
				}
				if (flag)
				{
					return this.Cells[num];
				}
				throw new GridException("Cannot find a cell bound to column name '" + columnUniqueName + "'");
			}
		}

		// Token: 0x040048E8 RID: 18664
		private ListDictionary aggregatesValues;
	}
}
