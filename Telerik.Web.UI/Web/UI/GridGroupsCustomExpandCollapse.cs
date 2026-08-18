using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010DF RID: 4319
	public class GridGroupsCustomExpandCollapse : GridCommandEventArgs
	{
		// Token: 0x0600B0DC RID: 45276 RVA: 0x002648E8 File Offset: 0x00262AE8
		public GridGroupsCustomExpandCollapse(GridItem item, object commandSource, object argument) : base(item, commandSource, "GroupsCustomExpandCollapse", argument)
		{
			string[] array = argument.ToString().Split(new char[]
			{
				';'
			});
			this.type = array[0];
			string a;
			if ((a = array[0]) != null)
			{
				if (a == "expandLevel")
				{
					int.TryParse(array[1], out this.expandLevel);
					return;
				}
				if (a == "expandToLevel")
				{
					int.TryParse(array[1], out this.expandToLevel);
					return;
				}
				if (a == "collapseLevel")
				{
					int.TryParse(array[1], out this.expandLevel);
					return;
				}
				if (a == "collapseToLevel")
				{
					int.TryParse(array[1], out this.expandToLevel);
					return;
				}
				if (!(a == "expandChildren"))
				{
					return;
				}
				int.TryParse(array[1], out this.rowIndex);
				int.TryParse(array[2], out this.groupLevel);
			}
		}

		// Token: 0x0600B0DD RID: 45277 RVA: 0x00264A14 File Offset: 0x00262C14
		public override void ExecuteCommand(object source)
		{
			bool expanded = this.type.Contains("expand");
			if (this.expandToLevel > 0)
			{
				IEnumerable<GridItem> enumerable = from item in base.Item.OwnerTableView.GetItems(new GridItemType[]
				{
					GridItemType.GroupHeader
				})
				where item.GroupLevel <= this.expandToLevel
				select item;
				using (IEnumerator<GridItem> enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						GridItem gridItem = enumerator.Current;
						gridItem.Expanded = expanded;
					}
					return;
				}
			}
			if (this.expandLevel > 0)
			{
				IEnumerable<GridItem> enumerable2 = from item in base.Item.OwnerTableView.GetItems(new GridItemType[]
				{
					GridItemType.GroupHeader
				})
				where item.GroupLevel == this.expandLevel
				select item;
				using (IEnumerator<GridItem> enumerator2 = enumerable2.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						GridItem gridItem2 = enumerator2.Current;
						GridGroupHeaderItem gridGroupHeaderItem = (GridGroupHeaderItem)gridItem2;
						gridGroupHeaderItem.Expanded = expanded;
						if (!gridGroupHeaderItem.Visible)
						{
							gridGroupHeaderItem.SetChildrenVisible(false);
						}
					}
					return;
				}
			}
			if (this.type == "expandChildren")
			{
				GridGroupHeaderItem gridGroupHeaderItem2 = base.Item.OwnerTableView.GetItems(new GridItemType[]
				{
					GridItemType.GroupHeader
				}).SingleOrDefault((GridItem i) => i.RowIndex == this.rowIndex && i.GroupLevel == this.groupLevel) as GridGroupHeaderItem;
				TableRowCollection rows = base.Item.OwnerTableView.GetGridTable().Rows;
				for (;;)
				{
					this.rowIndex++;
					GridGroupHeaderItem gridGroupHeaderItem3 = rows[this.rowIndex] as GridGroupHeaderItem;
					if (gridGroupHeaderItem3 != null)
					{
						if (gridGroupHeaderItem3.GroupLevel <= gridGroupHeaderItem2.GroupLevel)
						{
							break;
						}
						gridGroupHeaderItem3.Expanded = true;
					}
				}
				return;
			}
		}

		// Token: 0x04002E73 RID: 11891
		private int expandToLevel;

		// Token: 0x04002E74 RID: 11892
		private int expandLevel;

		// Token: 0x04002E75 RID: 11893
		private int rowIndex;

		// Token: 0x04002E76 RID: 11894
		private int groupLevel;

		// Token: 0x04002E77 RID: 11895
		private string type;
	}
}
