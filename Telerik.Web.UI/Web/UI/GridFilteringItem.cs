using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001141 RID: 4417
	public class GridFilteringItem : GridItem
	{
		// Token: 0x0600B404 RID: 46084 RVA: 0x00275C33 File Offset: 0x00273E33
		public GridFilteringItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.FilteringItem)
		{
			this.Expanded = ownerTableView.IsFilterItemExpanded;
		}

		// Token: 0x0600B405 RID: 46085 RVA: 0x00275C4C File Offset: 0x00273E4C
		internal override void SetItemDecorator(GridItemDecorator newDecorator)
		{
			base.SetItemDecorator(new GridFilterItemDecorator(this));
		}

		// Token: 0x0600B406 RID: 46086 RVA: 0x00275C5A File Offset: 0x00273E5A
		protected override TableCell CreateCellObject()
		{
			return new GridTableCell();
		}

		// Token: 0x17003A2E RID: 14894
		// (get) Token: 0x0600B407 RID: 46087 RVA: 0x00275C61 File Offset: 0x00273E61
		public override bool CanExpand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600B408 RID: 46088 RVA: 0x00275C64 File Offset: 0x00273E64
		protected override bool GetExpandedDefaultValue()
		{
			return true;
		}

		// Token: 0x17003A2F RID: 14895
		// (get) Token: 0x0600B409 RID: 46089 RVA: 0x00275C67 File Offset: 0x00273E67
		// (set) Token: 0x0600B40A RID: 46090 RVA: 0x00275C6F File Offset: 0x00273E6F
		public override bool Expanded
		{
			get
			{
				return base.Expanded;
			}
			set
			{
				base.Expanded = value;
				base.OwnerTableView.IsFilterItemExpanded = value;
			}
		}

		// Token: 0x17003A30 RID: 14896
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
