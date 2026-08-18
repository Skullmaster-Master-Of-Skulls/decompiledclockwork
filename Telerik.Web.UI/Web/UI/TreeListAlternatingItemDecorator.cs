using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001242 RID: 4674
	internal class TreeListAlternatingItemDecorator : TreeListItemDecorator
	{
		// Token: 0x0600C0DA RID: 49370 RVA: 0x002AEEE6 File Offset: 0x002AD0E6
		public TreeListAlternatingItemDecorator(TreeListItem item) : base(item)
		{
		}

		// Token: 0x0600C0DB RID: 49371 RVA: 0x002AEEF0 File Offset: 0x002AD0F0
		protected override void SetItemStyle(RadTreeList owner)
		{
			if (owner.CurrentExportFormat == ExportFormat.Pdf)
			{
				TreeListPdfStyle treeListPdfStyle = new TreeListPdfStyle();
				treeListPdfStyle.CopyFrom(owner.AlternatingItemStyle);
				treeListPdfStyle.CopyFrom(owner.ExportSettings.Pdf.AlternatingItemStyle);
				base.Item.MergeStyle(treeListPdfStyle);
				if (!treeListPdfStyle.LineHeight.IsEmpty)
				{
					base.Item.Style.Add("line-height", treeListPdfStyle.LineHeight.ToString());
				}
			}
			else if (owner.CurrentExportFormat == ExportFormat.Excel || owner.CurrentExportFormat == ExportFormat.ExcelXlsx)
			{
				TreeListExcelStyle treeListExcelStyle = new TreeListExcelStyle();
				treeListExcelStyle.CopyFrom(owner.AlternatingItemStyle);
				treeListExcelStyle.CopyFrom(owner.ExportSettings.Excel.AlternatingItemStyle);
				base.Item.MergeStyle(treeListExcelStyle);
			}
			else if (owner.CurrentExportFormat == ExportFormat.Word)
			{
				TreeListWordStyle treeListWordStyle = new TreeListWordStyle();
				treeListWordStyle.CopyFrom(owner.AlternatingItemStyle);
				treeListWordStyle.CopyFrom(owner.ExportSettings.Word.AlternatingItemStyle);
				base.Item.MergeStyle(treeListWordStyle);
			}
			else
			{
				base.Item.MergeStyle(owner.AlternatingItemStyle);
			}
			if (base.Item is TreeListDataItem)
			{
				base.Item.Attributes["id"] = this.GetRowID();
			}
		}

		// Token: 0x0600C0DC RID: 49372 RVA: 0x002AF090 File Offset: 0x002AD290
		protected override void PrepareCellInColumn(RadTreeList owner, TreeListColumn column, TableCell cell)
		{
			if (owner.CurrentExportFormat == ExportFormat.Excel || owner.CurrentExportFormat == ExportFormat.ExcelXlsx)
			{
				TreeListExcelStyle treeListExcelStyle = new TreeListExcelStyle();
				treeListExcelStyle.CopyFrom(owner.AlternatingItemStyle);
				treeListExcelStyle.CopyFrom(column.ItemStyle);
				treeListExcelStyle.CopyFrom(owner.ExportSettings.Excel.AlternatingItemStyle);
				cell.MergeStyle(treeListExcelStyle);
				return;
			}
			if (owner.CurrentExportFormat == ExportFormat.Word)
			{
				TreeListWordStyle treeListWordStyle = new TreeListWordStyle();
				treeListWordStyle.CopyFrom(owner.AlternatingItemStyle);
				treeListWordStyle.CopyFrom(column.ItemStyle);
				treeListWordStyle.CopyFrom(owner.ExportSettings.Word.AlternatingItemStyle);
				cell.MergeStyle(treeListWordStyle);
				return;
			}
			if (owner.CurrentExportFormat == ExportFormat.Pdf)
			{
				TreeListPdfStyle treeListPdfStyle = new TreeListPdfStyle();
				treeListPdfStyle.CopyFrom(owner.AlternatingItemStyle);
				treeListPdfStyle.CopyFrom(column.ItemStyle);
				treeListPdfStyle.CopyFrom(owner.ExportSettings.Pdf.AlternatingItemStyle);
				cell.MergeStyle(treeListPdfStyle);
				return;
			}
			base.PrepareCellInColumn(owner, column, cell);
		}
	}
}
