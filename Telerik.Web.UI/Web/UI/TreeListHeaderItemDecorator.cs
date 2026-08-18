using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001244 RID: 4676
	internal class TreeListHeaderItemDecorator : TreeListItemDecorator
	{
		// Token: 0x0600C0E2 RID: 49378 RVA: 0x002AF3D6 File Offset: 0x002AD5D6
		public TreeListHeaderItemDecorator(TreeListItem item) : base(item)
		{
		}

		// Token: 0x0600C0E3 RID: 49379 RVA: 0x002AF3E0 File Offset: 0x002AD5E0
		protected override void SetItemStyle(RadTreeList owner)
		{
			if (owner.CurrentExportFormat == ExportFormat.Excel || owner.CurrentExportFormat == ExportFormat.ExcelXlsx)
			{
				TreeListExcelStyle treeListExcelStyle = new TreeListExcelStyle();
				treeListExcelStyle.CopyFrom(owner.HeaderStyle);
				treeListExcelStyle.CopyFrom(owner.ExportSettings.Excel.HeaderStyle);
				base.Item.MergeStyle(treeListExcelStyle);
				return;
			}
			if (owner.CurrentExportFormat == ExportFormat.Word)
			{
				TreeListWordStyle treeListWordStyle = new TreeListWordStyle();
				treeListWordStyle.CopyFrom(owner.HeaderStyle);
				treeListWordStyle.CopyFrom(owner.ExportSettings.Word.HeaderStyle);
				base.Item.MergeStyle(treeListWordStyle);
				return;
			}
			if (owner.CurrentExportFormat == ExportFormat.Pdf)
			{
				TreeListPdfStyle treeListPdfStyle = new TreeListPdfStyle();
				treeListPdfStyle.CopyFrom(owner.HeaderStyle);
				treeListPdfStyle.CopyFrom(owner.ExportSettings.Pdf.HeaderStyle);
				if (!treeListPdfStyle.LineHeight.IsEmpty)
				{
					base.Item.Style.Add("line-height", treeListPdfStyle.LineHeight.ToString());
				}
				base.Item.MergeStyle(treeListPdfStyle);
			}
		}

		// Token: 0x0600C0E4 RID: 49380 RVA: 0x002AF53C File Offset: 0x002AD73C
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object,System.Object)")]
		public override void DecorateItem(RadTreeList owner, TreeListColumn[] columnArray)
		{
			if (columnArray.Length == 0 || base.Item.Cells.Count == 0)
			{
				return;
			}
			base.DecorateItem(owner, columnArray);
			int num = 0;
			TableCell tableCell = null;
			while ((tableCell == null || !tableCell.Visible) && num < base.Item.Cells.Count)
			{
				if (!columnArray[num].Visible || !columnArray[num].Display)
				{
					num++;
				}
				else
				{
					tableCell = base.Item.Cells[num++];
				}
			}
			if (tableCell != null)
			{
				tableCell.ColumnSpan = owner.MostNestedIndex + 2;
				TableCell tableCell2 = tableCell;
				tableCell2.CssClass = tableCell2.CssClass + " rtlCF " + owner.HeaderStyle.CssClass;
			}
			TableCell tableCell3 = base.Item.Cells[base.Item.Cells.Count - 1];
			TableCell tableCell4 = tableCell3;
			tableCell4.CssClass = tableCell4.CssClass + " rtlCL " + owner.HeaderStyle.CssClass;
			if (columnArray.Length > 0)
			{
				if (tableCell != null)
				{
					TableCell tableCell5 = tableCell;
					tableCell5.CssClass = tableCell5.CssClass + " " + columnArray[0].HeaderStyle.CssClass;
				}
				TableCell tableCell6 = tableCell3;
				tableCell6.CssClass = tableCell6.CssClass + " " + columnArray[columnArray.Length - 1].HeaderStyle.CssClass;
			}
			int num2 = columnArray.Length;
			int num3 = 0;
			foreach (TreeListColumn treeListColumn in columnArray)
			{
				TableCell tableCell7 = base.Item.Cells[num3++];
				this.PrepareCellInColumn(base.Item.OwnerTreeList, treeListColumn, tableCell7);
				if (!treeListColumn.Visible)
				{
					num2--;
					tableCell7.Visible = false;
				}
				else if (!treeListColumn.Display)
				{
					num2--;
					tableCell7.Style["display"] = "none";
				}
			}
			if (num2 == 0)
			{
				base.Item.Cells.Add(new TreeListTableHeaderCell(true)
				{
					CssClass = string.Format("{0} {1}", "rtlCF", "rtlCL")
				});
			}
		}

		// Token: 0x0600C0E5 RID: 49381 RVA: 0x002AF74C File Offset: 0x002AD94C
		protected override void PrepareCellInColumn(RadTreeList owner, TreeListColumn column, TableCell cell)
		{
			if (owner.CurrentExportFormat == ExportFormat.Pdf)
			{
				TreeListPdfStyle treeListPdfStyle = new TreeListPdfStyle();
				treeListPdfStyle.CopyFrom(owner.HeaderStyle);
				treeListPdfStyle.CopyFrom(column.HeaderStyle);
				treeListPdfStyle.CopyFrom(owner.ExportSettings.Pdf.HeaderStyle);
				if (!treeListPdfStyle.Width.IsEmpty)
				{
					column.HeaderStyle.Width = treeListPdfStyle.Width;
				}
				cell.MergeStyle(treeListPdfStyle);
			}
			else if (owner.CurrentExportFormat == ExportFormat.Excel || owner.CurrentExportFormat == ExportFormat.ExcelXlsx)
			{
				TreeListExcelStyle treeListExcelStyle = new TreeListExcelStyle();
				treeListExcelStyle.CopyFrom(owner.HeaderStyle);
				treeListExcelStyle.CopyFrom(column.HeaderStyle);
				treeListExcelStyle.CopyFrom(owner.ExportSettings.Excel.HeaderStyle);
				if (!treeListExcelStyle.Width.IsEmpty)
				{
					column.HeaderStyle.Width = treeListExcelStyle.Width;
				}
				cell.MergeStyle(treeListExcelStyle);
			}
			else if (owner.CurrentExportFormat == ExportFormat.Word)
			{
				TreeListWordStyle treeListWordStyle = new TreeListWordStyle();
				treeListWordStyle.CopyFrom(owner.HeaderStyle);
				treeListWordStyle.CopyFrom(column.HeaderStyle);
				treeListWordStyle.CopyFrom(owner.ExportSettings.Word.HeaderStyle);
				if (!treeListWordStyle.Width.IsEmpty)
				{
					column.HeaderStyle.Width = treeListWordStyle.Width;
				}
				cell.MergeStyle(treeListWordStyle);
			}
			else
			{
				cell.MergeStyle(column.HeaderStyle);
				cell.MergeStyle(owner.HeaderStyle);
			}
			cell.Width = Unit.Empty;
			this.PrepareSortIcon(owner, column, cell);
		}

		// Token: 0x0600C0E6 RID: 49382 RVA: 0x002AF920 File Offset: 0x002ADB20
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)")]
		protected void PrepareSortIcon(RadTreeList owner, TreeListColumn column, TableCell cell)
		{
			string sortExpressionInternal = column.GetSortExpressionInternal();
			if (!string.IsNullOrEmpty(sortExpressionInternal) && owner.SortExpressions.ContainsExpression(sortExpressionInternal))
			{
				TreeListSortExpression expression = owner.SortExpressions.GetExpression(sortExpressionInternal);
				Control control = cell.FindControl(string.Format("{0}_SortIconButton", column.UniqueName));
				if (control == null)
				{
					return;
				}
				bool flag = base.Item.OwnerTreeList.ResolvedRenderMode == RenderMode.Lightweight || base.Item.OwnerTreeList.ResolvedRenderMode == RenderMode.Mobile;
				Button button = control as Button;
				LinkButton linkButton = control as LinkButton;
				switch (expression.SortOrder)
				{
				case TreeListSortOrder.None:
					button.Visible = false;
					break;
				case TreeListSortOrder.Ascending:
					if (!flag)
					{
						button.CssClass = "rtlSortAsc";
						button.ToolTip = owner.SortingSettings.SortedAscToolTip;
						return;
					}
					linkButton.CssClass = "t-button rtlActionButton rtlSortAsc";
					linkButton.Text = string.Format(CultureInfo.InvariantCulture, "<span class=\"{0} {1}\"></span>", new object[]
					{
						"t-font-icon rtlIcon",
						"rtlSortAscIcon"
					});
					linkButton.ToolTip = owner.SortingSettings.SortedAscToolTip;
					if (base.Item.OwnerTreeList.EnableAriaSupport)
					{
						linkButton.Attributes.Add("aria-label", owner.SortingSettings.SortedAscToolTip);
						return;
					}
					break;
				case TreeListSortOrder.Descending:
					if (!flag)
					{
						button.CssClass = "rtlSortDesc";
						button.ToolTip = owner.SortingSettings.SortedDescToolTip;
						return;
					}
					linkButton.CssClass = "t-button rtlActionButton rtlSortDesc";
					linkButton.Text = string.Format(CultureInfo.InvariantCulture, "<span class=\"{0} {1}\"></span>", new object[]
					{
						"t-font-icon rtlIcon",
						"rtlSortDescIcon"
					});
					linkButton.ToolTip = owner.SortingSettings.SortedDescToolTip;
					if (base.Item.OwnerTreeList.EnableAriaSupport)
					{
						linkButton.Attributes.Add("aria-label", owner.SortingSettings.SortedDescToolTip);
						return;
					}
					break;
				default:
					return;
				}
			}
		}
	}
}
