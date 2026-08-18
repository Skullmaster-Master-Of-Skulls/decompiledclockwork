using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001240 RID: 4672
	internal class TreeListFooterItemDecorator : TreeListItemDecorator
	{
		// Token: 0x0600C0CD RID: 49357 RVA: 0x002AEB2F File Offset: 0x002ACD2F
		public TreeListFooterItemDecorator(TreeListItem item) : base(item)
		{
			base.Item = item;
		}

		// Token: 0x0600C0CE RID: 49358 RVA: 0x002AEB40 File Offset: 0x002ACD40
		public override void DecorateItem(RadTreeList owner, TreeListColumn[] columnArray)
		{
			if (columnArray.Length == 0 || base.Item.Cells.Count == 0)
			{
				return;
			}
			this.SetItemStyle(owner);
			TreeListFooterItem treeListFooterItem = base.Item as TreeListFooterItem;
			if (treeListFooterItem != null)
			{
				this.PrepareCells(treeListFooterItem.HierarchyIndex.NestedLevel, columnArray);
			}
		}

		// Token: 0x0600C0CF RID: 49359 RVA: 0x002AEB90 File Offset: 0x002ACD90
		protected override void SetItemStyle(RadTreeList owner)
		{
			if (owner.CurrentExportFormat == ExportFormat.Excel)
			{
				TreeListExcelStyle treeListExcelStyle = new TreeListExcelStyle();
				treeListExcelStyle.CopyFrom(owner.FooterItemStyle);
				treeListExcelStyle.CopyFrom(owner.ExportSettings.Excel.FooterItemStyle);
				base.Item.MergeStyle(treeListExcelStyle);
				return;
			}
			TreeListTableItemStyle footerItemStyle = owner.FooterItemStyle;
			base.Item.MergeStyle(footerItemStyle);
		}

		// Token: 0x0600C0D0 RID: 49360 RVA: 0x002AEC04 File Offset: 0x002ACE04
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected override void PrepareCells(int nestedLevel, TreeListColumn[] columnArray)
		{
			int num = nestedLevel + 2;
			if (num + columnArray.Length > base.Item.Cells.Count)
			{
				return;
			}
			int num2 = 0;
			bool flag = false;
			for (int i = 0; i < num; i++)
			{
				TableCell tableCell = base.Item.Cells[i];
				tableCell.CssClass += " rtlL";
			}
			foreach (TreeListColumn treeListColumn in columnArray)
			{
				TableCell tableCell2 = base.Item.Cells[num2 + num];
				this.PrepareCellInColumn(base.Item.OwnerTreeList, treeListColumn, tableCell2);
				if (!treeListColumn.Visible)
				{
					tableCell2.Visible = false;
				}
				else if (!treeListColumn.Display)
				{
					tableCell2.Style["display"] = "none";
				}
				else
				{
					if (!flag)
					{
						flag = true;
						tableCell2.ColumnSpan = base.Item.OwnerTreeList.MostNestedIndex - nestedLevel;
						TableCell tableCell3 = tableCell2;
						tableCell3.CssClass += " rtlCF";
					}
					if (num2 == columnArray.Length - 1)
					{
						TableCell tableCell4 = tableCell2;
						tableCell4.CssClass += " rtlCL";
					}
				}
				num2++;
			}
		}

		// Token: 0x0600C0D1 RID: 49361 RVA: 0x002AED3C File Offset: 0x002ACF3C
		protected override void PrepareCellInColumn(RadTreeList owner, TreeListColumn column, TableCell cell)
		{
			if (owner.CurrentExportFormat == ExportFormat.Excel)
			{
				TreeListExcelStyle treeListExcelStyle = new TreeListExcelStyle();
				treeListExcelStyle.CopyFrom(owner.FooterItemStyle);
				treeListExcelStyle.CopyFrom(column.ItemStyle);
				treeListExcelStyle.CopyFrom(owner.ExportSettings.Excel.FooterItemStyle);
				cell.MergeStyle(treeListExcelStyle);
				return;
			}
			cell.MergeStyle(column.ItemStyle);
		}
	}
}
