using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001243 RID: 4675
	internal class TreeListEditItemDecorator : TreeListItemDecorator
	{
		// Token: 0x0600C0DD RID: 49373 RVA: 0x002AF1D0 File Offset: 0x002AD3D0
		public TreeListEditItemDecorator(TreeListItem item) : base(item)
		{
		}

		// Token: 0x0600C0DE RID: 49374 RVA: 0x002AF1DC File Offset: 0x002AD3DC
		protected override void SetItemStyle(RadTreeList owner)
		{
			TableItemStyle tableItemStyle = new TableItemStyle();
			TreeListDataItem treeListDataItem = base.Item as TreeListDataItem;
			if (treeListDataItem != null)
			{
				tableItemStyle.CopyFrom(owner.EditItemStyle);
				if (treeListDataItem.Selected)
				{
					tableItemStyle.MergeWith(owner.SelectedItemStyle);
					tableItemStyle.CssClass = string.Format("{0} {1}", tableItemStyle.CssClass, owner.SelectedItemStyle.CssClass);
				}
				base.Item.Attributes["id"] = this.GetRowID();
			}
			else if (base.Item is TreeListDataInsertItem)
			{
				tableItemStyle.CopyFrom(owner.EditItemStyle);
			}
			base.Item.MergeStyle(tableItemStyle);
		}

		// Token: 0x0600C0DF RID: 49375 RVA: 0x002AF284 File Offset: 0x002AD484
		public override void DecorateItem(RadTreeList owner, TreeListColumn[] columnArray)
		{
			base.DecorateItem(owner, columnArray);
			TreeListEditFormItem treeListEditFormItem = base.Item as TreeListEditFormItem;
			if (treeListEditFormItem != null)
			{
				this.SetEditFormCellColumnSpan(treeListEditFormItem, columnArray);
				this.PrepareEditItemServiceCells(treeListEditFormItem.ParentItem);
				return;
			}
			ITreeListInsertItem treeListInsertItem = base.Item as ITreeListInsertItem;
			if (treeListInsertItem != null)
			{
				this.PrepareEditItemServiceCells(treeListInsertItem.ParentItem);
				if (treeListInsertItem.ParentItem != null)
				{
					this.PrepareCells(treeListInsertItem.ParentItem.HierarchyIndex.NestedLevel, columnArray);
					return;
				}
				this.PrepareCells(0, columnArray);
			}
		}

		// Token: 0x0600C0E0 RID: 49376 RVA: 0x002AF300 File Offset: 0x002AD500
		private void SetEditFormCellColumnSpan(TreeListEditFormItem editFormItem, IList<TreeListColumn> columnArray)
		{
			int num = base.Item.CalculateCellSpan(columnArray);
			int num2 = (editFormItem.ParentItem != null) ? editFormItem.ParentItem.HierarchyIndex.NestedLevel : 0;
			editFormItem.EditFormCell.ColumnSpan = num + base.Item.OwnerTreeList.MostNestedIndex - num2;
		}

		// Token: 0x0600C0E1 RID: 49377 RVA: 0x002AF358 File Offset: 0x002AD558
		private void PrepareEditItemServiceCells(TreeListDataItem parentItem)
		{
			int num = (parentItem != null) ? (parentItem.HierarchyIndex.NestedLevel + 1) : 1;
			for (int i = 0; i < num; i++)
			{
				TableCell tableCell = base.Item.Cells[i];
				tableCell.CssClass = "rtlL";
				if (parentItem != null && parentItem.ItemState.Siblings[i].HasNextPageSiblings)
				{
					TableCell tableCell2 = tableCell;
					tableCell2.CssClass += " rtlL0";
				}
			}
		}
	}
}
