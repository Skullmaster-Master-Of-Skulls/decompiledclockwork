using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000961 RID: 2401
	internal class TreeListItemDecorator
	{
		// Token: 0x17001E2B RID: 7723
		// (get) Token: 0x06005B6B RID: 23403 RVA: 0x001162EF File Offset: 0x001144EF
		// (set) Token: 0x06005B6C RID: 23404 RVA: 0x001162F7 File Offset: 0x001144F7
		public TreeListItem Item { get; internal set; }

		// Token: 0x06005B6D RID: 23405 RVA: 0x00116300 File Offset: 0x00114500
		public TreeListItemDecorator(TreeListItem item)
		{
			this.Item = item;
		}

		// Token: 0x06005B6E RID: 23406 RVA: 0x00116310 File Offset: 0x00114510
		public virtual void DecorateItem(RadTreeList owner, TreeListColumn[] columnArray)
		{
			if (columnArray.Length == 0 || this.Item.Cells.Count == 0)
			{
				return;
			}
			this.SetItemStyle(owner);
			TreeListDataItem treeListDataItem = this.Item as TreeListDataItem;
			if (treeListDataItem != null)
			{
				this.PrepareCells(treeListDataItem.HierarchyIndex.NestedLevel, columnArray);
			}
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x00116360 File Offset: 0x00114560
		protected virtual void SetItemStyle(RadTreeList owner)
		{
			if (owner.CurrentExportFormat == ExportFormat.Pdf)
			{
				TreeListPdfStyle treeListPdfStyle = new TreeListPdfStyle();
				treeListPdfStyle.CopyFrom(owner.ItemStyle);
				treeListPdfStyle.CopyFrom(owner.ExportSettings.Pdf.ItemStyle);
				this.Item.MergeStyle(treeListPdfStyle);
				if (!treeListPdfStyle.LineHeight.IsEmpty)
				{
					this.Item.Style.Add("line-height", treeListPdfStyle.LineHeight.ToString());
				}
			}
			else if (owner.CurrentExportFormat == ExportFormat.Excel || owner.CurrentExportFormat == ExportFormat.ExcelXlsx)
			{
				TreeListExcelStyle treeListExcelStyle = new TreeListExcelStyle();
				treeListExcelStyle.CopyFrom(owner.ItemStyle);
				treeListExcelStyle.CopyFrom(owner.ExportSettings.Excel.ItemStyle);
				this.Item.MergeStyle(treeListExcelStyle);
			}
			else if (owner.CurrentExportFormat == ExportFormat.Word)
			{
				TreeListWordStyle treeListWordStyle = new TreeListWordStyle();
				treeListWordStyle.CopyFrom(owner.ItemStyle);
				treeListWordStyle.CopyFrom(owner.ExportSettings.Word.ItemStyle);
				this.Item.MergeStyle(treeListWordStyle);
			}
			else
			{
				this.Item.MergeStyle(owner.ItemStyle);
			}
			if (this.Item is TreeListDataItem)
			{
				this.Item.Attributes["id"] = this.GetRowID();
			}
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x00116500 File Offset: 0x00114700
		protected virtual string GetRowID()
		{
			TreeListDataItem treeListDataItem = this.Item as TreeListDataItem;
			if (treeListDataItem == null)
			{
				return this.Item.ClientID;
			}
			if (this.Item.OwnerTreeList.ClientIDMode != ClientIDMode.AutoID)
			{
				return this.Item.OwnerTreeList.ClientID + "__" + treeListDataItem.DisplayIndex;
			}
			return this.Item.ClientID + "__" + treeListDataItem.DisplayIndex;
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x00116584 File Offset: 0x00114784
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		protected virtual void PrepareCells(int nestedLevel, TreeListColumn[] columnArray)
		{
			int num = nestedLevel + 1;
			if (num + columnArray.Length > this.Item.Cells.Count)
			{
				return;
			}
			int num2 = 0;
			bool flag = false;
			foreach (TreeListColumn treeListColumn in columnArray)
			{
				TableCell tableCell = this.Item.Cells[num2 + num];
				this.PrepareCellInColumn(this.Item.OwnerTreeList, treeListColumn, tableCell);
				if (!treeListColumn.Visible)
				{
					tableCell.Visible = false;
				}
				else if (!treeListColumn.Display)
				{
					tableCell.Style["display"] = "none";
				}
				else
				{
					treeListColumn.PrepareCell(tableCell, this.Item);
					if (!flag)
					{
						flag = true;
						tableCell.ColumnSpan = this.Item.OwnerTreeList.MostNestedIndex + 1 - nestedLevel;
						TableCell tableCell2 = tableCell;
						tableCell2.CssClass += " rtlCF";
					}
					if (num2 == columnArray.Length - 1)
					{
						TableCell tableCell3 = tableCell;
						tableCell3.CssClass += " rtlCL";
					}
				}
				num2++;
			}
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x0011669C File Offset: 0x0011489C
		protected virtual void PrepareCellInColumn(RadTreeList owner, TreeListColumn column, TableCell cell)
		{
			if (owner.CurrentExportFormat == ExportFormat.Excel || owner.CurrentExportFormat == ExportFormat.ExcelXlsx)
			{
				TreeListExcelStyle treeListExcelStyle = new TreeListExcelStyle();
				treeListExcelStyle.CopyFrom(owner.ItemStyle);
				treeListExcelStyle.CopyFrom(column.ItemStyle);
				treeListExcelStyle.CopyFrom(owner.ExportSettings.Excel.ItemStyle);
				cell.MergeStyle(treeListExcelStyle);
				return;
			}
			if (owner.CurrentExportFormat == ExportFormat.Word)
			{
				TreeListWordStyle treeListWordStyle = new TreeListWordStyle();
				treeListWordStyle.CopyFrom(owner.ItemStyle);
				treeListWordStyle.CopyFrom(column.ItemStyle);
				treeListWordStyle.CopyFrom(owner.ExportSettings.Word.ItemStyle);
				cell.MergeStyle(treeListWordStyle);
				return;
			}
			if (owner.CurrentExportFormat == ExportFormat.Pdf)
			{
				TreeListPdfStyle treeListPdfStyle = new TreeListPdfStyle();
				treeListPdfStyle.CopyFrom(owner.ItemStyle);
				treeListPdfStyle.CopyFrom(column.ItemStyle);
				treeListPdfStyle.CopyFrom(owner.ExportSettings.Pdf.ItemStyle);
				cell.MergeStyle(treeListPdfStyle);
				return;
			}
			cell.MergeStyle(column.ItemStyle);
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x001167E0 File Offset: 0x001149E0
		public static void PrepareDataItemsServiceCells(RadTreeList owner)
		{
			TreeListDataItemCollection items = owner.Items;
			if (items.Count == 0)
			{
				return;
			}
			TreeListDataItem treeListDataItem = items[items.Count - 1];
			if (!owner.ShowFooter)
			{
				if (treeListDataItem.DetailItem == null)
				{
					TreeListDataItem treeListDataItem2 = treeListDataItem;
					treeListDataItem2.CssClass += " rtlRBtm";
				}
				else
				{
					TreeListDetailTemplateItem detailItem = treeListDataItem.DetailItem;
					detailItem.CssClass += " rtlRBtm";
				}
			}
			TreeListDataItem treeListDataItem3 = null;
			foreach (TreeListDataItem treeListDataItem4 in items)
			{
				if (treeListDataItem3 != null && treeListDataItem4.HierarchyIndex.NestedLevel < treeListDataItem3.HierarchyIndex.NestedLevel)
				{
					TreeListDataItem treeListDataItem5 = treeListDataItem4;
					treeListDataItem5.CssClass += " rtlROut";
					if (!owner.ShowFooter)
					{
						if (treeListDataItem3.IsInEditMode && owner.EditMode != TreeListEditMode.InPlace)
						{
							if (treeListDataItem3.InsertItem != null)
							{
								TreeListEditableItem insertItem = treeListDataItem3.InsertItem;
								insertItem.CssClass += " rtlRL";
							}
							else if (treeListDataItem3.EditFormItem != null)
							{
								TreeListEditFormItem editFormItem = treeListDataItem3.EditFormItem;
								editFormItem.CssClass += " rtlRL";
							}
						}
						else
						{
							TreeListDataItem treeListDataItem6 = treeListDataItem3;
							treeListDataItem6.CssClass += " rtlRL";
						}
					}
				}
				treeListDataItem3 = treeListDataItem4;
				int num = 0;
				foreach (TreeListSiblingState treeListSiblingState in treeListDataItem4.ItemState.Siblings)
				{
					string text = "rtlL";
					if (treeListSiblingState.HasPrevPageSiblings)
					{
						if (treeListSiblingState.HasNextPageSiblings)
						{
							if (num == treeListDataItem4.HierarchyIndex.NestedLevel)
							{
								text += " rtlL2";
							}
							else
							{
								text += " rtlL0";
							}
						}
						else if (num == treeListDataItem4.HierarchyIndex.NestedLevel)
						{
							text += " rtlL3";
						}
					}
					else if (treeListSiblingState.HasNextPageSiblings)
					{
						if (treeListDataItem4.HierarchyIndex.NestedLevel == 0)
						{
							text += " rtlL1";
						}
						else
						{
							text += " rtlL0";
						}
					}
					treeListDataItem4.Cells[num].CssClass = text;
					if (treeListDataItem4.DetailItem != null && num < treeListDataItem4.DetailItem.Cells.Count)
					{
						TableCell tableCell = treeListDataItem4.DetailItem.Cells[num];
						tableCell.CssClass = tableCell.CssClass + " " + text.Replace("rtlL2", "rtlL0").Replace("rtlL3", "").Replace("rtlL1", "rtlL0");
					}
					num++;
				}
			}
			if (owner.ShowFooter)
			{
				TreeListItemDecorator.PrepareFooterItemsServiceCells(owner);
			}
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x00116ADC File Offset: 0x00114CDC
		internal static void PrepareFooterItemsServiceCells(RadTreeList owner)
		{
			TreeListItem[] items = owner.GetItems(new TreeListItemType[]
			{
				TreeListItemType.FooterItem
			});
			foreach (TreeListFooterItem treeListFooterItem in items)
			{
				TreeListDataItem ownerDataItem = treeListFooterItem.OwnerDataItem;
				if (ownerDataItem.HierarchyIndex.NestedLevel - 1 > treeListFooterItem.HierarchyIndex.NestedLevel)
				{
					TreeListFooterItem treeListFooterItem2 = treeListFooterItem;
					treeListFooterItem2.CssClass += " rtlROut";
				}
				int num = treeListFooterItem.HierarchyIndex.NestedLevel + 2;
				for (int j = 0; j < num; j++)
				{
					string cssClass = ownerDataItem.Cells[j].CssClass;
					if (cssClass.IndexOf("rtlL0") > -1 || cssClass.IndexOf("rtlL2") > -1)
					{
						TableCell tableCell = treeListFooterItem.Cells[j];
						tableCell.CssClass += " rtlL0";
					}
				}
			}
		}
	}
}
