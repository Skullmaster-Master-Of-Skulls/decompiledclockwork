using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001144 RID: 4420
	public class GridHeaderItem : GridItem
	{
		// Token: 0x0600B422 RID: 46114 RVA: 0x00276C40 File Offset: 0x00274E40
		public GridHeaderItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.Header)
		{
		}

		// Token: 0x0600B423 RID: 46115 RVA: 0x00276C4C File Offset: 0x00274E4C
		protected override TableCell CreateCellObject()
		{
			return new GridTableHeaderCell();
		}

		// Token: 0x17003A38 RID: 14904
		// (get) Token: 0x0600B424 RID: 46116 RVA: 0x00276C53 File Offset: 0x00274E53
		// (set) Token: 0x0600B425 RID: 46117 RVA: 0x00276C5B File Offset: 0x00274E5B
		internal int NumberOfHeaders { get; set; }

		// Token: 0x17003A39 RID: 14905
		// (get) Token: 0x0600B426 RID: 46118 RVA: 0x00276C64 File Offset: 0x00274E64
		// (set) Token: 0x0600B427 RID: 46119 RVA: 0x00276C6C File Offset: 0x00274E6C
		internal int Level { get; set; }

		// Token: 0x17003A3A RID: 14906
		// (get) Token: 0x0600B428 RID: 46120 RVA: 0x00276C75 File Offset: 0x00274E75
		// (set) Token: 0x0600B429 RID: 46121 RVA: 0x00276C7D File Offset: 0x00274E7D
		internal ArrayList MultiHeaderCells { get; set; }

		// Token: 0x0600B42A RID: 46122 RVA: 0x00276C88 File Offset: 0x00274E88
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		internal GridTableHeaderCell GetParentHeaderCellByColumnGroupName(string columnGroupName, int level)
		{
			GridHeaderItem gridHeaderItem = base.OwnerTableView.GetItems(new GridItemType[]
			{
				GridItemType.Header
			})[level - 1] as GridHeaderItem;
			for (int i = 0; i < gridHeaderItem.MultiHeaderCells.Count; i++)
			{
				GridColumnGroup gridColumnGroup = gridHeaderItem.MultiHeaderCells[i] as GridColumnGroup;
				if (gridColumnGroup != null && gridColumnGroup.Name == columnGroupName)
				{
					return gridHeaderItem.Cells[i] as GridTableHeaderCell;
				}
			}
			return null;
		}

		// Token: 0x0600B42B RID: 46123 RVA: 0x00276D04 File Offset: 0x00274F04
		public override void Initialize(GridColumn[] columns)
		{
			TableCellCollection cells = this.Cells;
			if (base.OwnerTableView.ColumnGroups != null && base.OwnerTableView.ColumnGroups.Count > 0)
			{
				for (int i = 0; i < this.MultiHeaderCells.Count; i++)
				{
					GridColumnGroup gridColumnGroup = this.MultiHeaderCells[i] as GridColumnGroup;
					GridTableHeaderCell gridTableHeaderCell = this.CreateCellObject() as GridTableHeaderCell;
					cells.Add(gridTableHeaderCell);
					if (gridColumnGroup != null)
					{
						gridTableHeaderCell.Text = gridColumnGroup.HeaderText;
						gridTableHeaderCell.ColumnSpan = gridColumnGroup.ColSpan;
						GridTableView ownerTableView = base.OwnerTableView;
						ownerTableView.hiddenColumnHeaderSpans += gridColumnGroup.VisibleColSpan.ToString();
						GridTableView ownerTableView2 = base.OwnerTableView;
						ownerTableView2.hiddenColumnHeaderSpans += ";";
						if (gridColumnGroup.ParentGroupName != string.Empty)
						{
							gridTableHeaderCell._parentHeaderCell = this.GetParentHeaderCellByColumnGroupName(gridColumnGroup.ParentGroupName, this.Level);
						}
					}
					else
					{
						GridColumn gridColumn = this.MultiHeaderCells[i] as GridColumn;
						gridTableHeaderCell.RowSpan = this.NumberOfHeaders - gridColumn.RowSpan + 1;
						gridTableHeaderCell.HeaderID = string.Concat(new object[]
						{
							this.OwnerID,
							gridColumn.UniqueName,
							i,
							"_MultiHeader",
							gridColumn.OrderIndex
						});
						if (gridColumn.ColumnGroupName != string.Empty)
						{
							gridTableHeaderCell._parentHeaderCell = this.GetParentHeaderCellByColumnGroupName(gridColumn.ColumnGroupName, this.Level);
						}
						gridColumn.InitializeCell(gridTableHeaderCell, -1, this);
					}
				}
				return;
			}
			for (int j = 0; j < columns.Length; j++)
			{
				TableCell cell = this.CreateCellObject();
				cells.Add(cell);
				columns[j].InitializeCell(cell, j, this);
			}
		}

		// Token: 0x0600B42C RID: 46124 RVA: 0x00276EF0 File Offset: 0x002750F0
		internal void AdjustColSpan()
		{
			if (base.OwnerTableView.ColumnGroups != null && base.OwnerTableView.ColumnGroups.Count > 0)
			{
				for (int i = 0; i < this.MultiHeaderCells.Count; i++)
				{
					GridColumnGroup gridColumnGroup = this.MultiHeaderCells[i] as GridColumnGroup;
					GridTableHeaderCell gridTableHeaderCell = this.Cells[i] as GridTableHeaderCell;
					if (gridColumnGroup != null)
					{
						gridTableHeaderCell.Text = gridColumnGroup.HeaderText;
						gridTableHeaderCell.ColumnSpan = gridColumnGroup.ColSpan;
						GridTableView ownerTableView = base.OwnerTableView;
						ownerTableView.hiddenColumnHeaderSpans += gridColumnGroup.VisibleColSpan.ToString();
						GridTableView ownerTableView2 = base.OwnerTableView;
						ownerTableView2.hiddenColumnHeaderSpans += ";";
						if (gridColumnGroup.ParentGroupName != string.Empty)
						{
							gridTableHeaderCell._parentHeaderCell = this.GetParentHeaderCellByColumnGroupName(gridColumnGroup.ParentGroupName, this.Level);
						}
					}
				}
			}
		}

		// Token: 0x17003A3B RID: 14907
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
	}
}
