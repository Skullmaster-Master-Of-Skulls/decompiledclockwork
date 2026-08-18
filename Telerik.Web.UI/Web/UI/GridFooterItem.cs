using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001142 RID: 4418
	public class GridFooterItem : GridItem
	{
		// Token: 0x0600B40C RID: 46092 RVA: 0x00275D0C File Offset: 0x00273F0C
		public GridFooterItem(GridTableView ownerTableView, int itemIndex, int dataSetIndex) : base(ownerTableView, itemIndex, dataSetIndex, GridItemType.Footer)
		{
			if (!ownerTableView.ShowFooter)
			{
				this.Visible = false;
			}
		}

		// Token: 0x17003A31 RID: 14897
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

		// Token: 0x17003A32 RID: 14898
		public TableCell this[GridColumn column]
		{
			get
			{
				return this[column.UniqueName];
			}
		}
	}
}
