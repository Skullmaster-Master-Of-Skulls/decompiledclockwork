using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001247 RID: 4679
	internal class TreeListDetailTemplateItemDecorator : TreeListItemDecorator
	{
		// Token: 0x0600C0ED RID: 49389 RVA: 0x002AFC61 File Offset: 0x002ADE61
		public TreeListDetailTemplateItemDecorator(TreeListItem item) : base(item)
		{
		}

		// Token: 0x0600C0EE RID: 49390 RVA: 0x002AFC6C File Offset: 0x002ADE6C
		public override void DecorateItem(RadTreeList owner, TreeListColumn[] columnArray)
		{
			if (columnArray.Length == 0 || base.Item.Cells.Count == 0)
			{
				return;
			}
			this.SetItemStyle(owner);
			TreeListDetailTemplateItem treeListDetailTemplateItem = base.Item as TreeListDetailTemplateItem;
			int num = columnArray.Length;
			if (treeListDetailTemplateItem != null)
			{
				foreach (TreeListColumn treeListColumn in columnArray)
				{
					if (!treeListColumn.Visible || !treeListColumn.Display)
					{
						num--;
					}
				}
				TableCell tableCell = base.Item.Cells[base.Item.Cells.Count - 1];
				tableCell.ColumnSpan = num + treeListDetailTemplateItem.ParentItem.OwnerTreeList.MostNestedIndex - treeListDetailTemplateItem.ParentItem.HierarchyIndex.NestedLevel;
				TableCell tableCell2 = tableCell;
				tableCell2.CssClass += " rtlCF rtlCL";
				tableCell.CssClass = tableCell.CssClass.TrimStart(new char[]
				{
					' '
				}).TrimEnd(new char[]
				{
					' '
				});
			}
		}

		// Token: 0x0600C0EF RID: 49391 RVA: 0x002AFD78 File Offset: 0x002ADF78
		protected override void SetItemStyle(RadTreeList owner)
		{
			if (((TreeListDetailTemplateItem)base.Item).ParentItem.IsAlternatingItem())
			{
				base.Item.MergeStyle(owner.AlternatingItemStyle);
			}
			else
			{
				base.Item.MergeStyle(owner.ItemStyle);
			}
			TreeListItem item = base.Item;
			item.CssClass += " rtlDetailItem";
		}
	}
}
