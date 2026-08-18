using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000965 RID: 2405
	public class TreeListCommandItem : TreeListItem
	{
		// Token: 0x06005B93 RID: 23443 RVA: 0x001171D6 File Offset: 0x001153D6
		public TreeListCommandItem(RadTreeList ownerTreeList, TreeListItemType itemType, bool isDataBinding) : base(ownerTreeList, itemType, isDataBinding)
		{
		}

		// Token: 0x17001E31 RID: 7729
		// (get) Token: 0x06005B94 RID: 23444 RVA: 0x001171E1 File Offset: 0x001153E1
		// (set) Token: 0x06005B95 RID: 23445 RVA: 0x001171E9 File Offset: 0x001153E9
		public TableCell CommandItemContentCell { get; internal set; }

		// Token: 0x06005B96 RID: 23446 RVA: 0x001171F2 File Offset: 0x001153F2
		public override void Initialize(IList<TreeListColumn> columns)
		{
			this.InitializeCommandItem();
			this.CallOnItemCreated();
			if (this.IsDataBinding)
			{
				this.DataBind();
				this.CallOnItemDataBound();
			}
		}

		// Token: 0x17001E32 RID: 7730
		// (get) Token: 0x06005B97 RID: 23447 RVA: 0x00117214 File Offset: 0x00115414
		internal override bool IsExportable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005B98 RID: 23448 RVA: 0x00117218 File Offset: 0x00115418
		private void InitializeCommandItem()
		{
			TreeListCommandItemSettings commandItemSettings = base.OwnerTreeList.CommandItemSettings;
			this.CommandItemContentCell = this.CreateCellObject();
			this.Cells.Add(this.CommandItemContentCell);
			Panel panel = new Panel
			{
				CssClass = "rtlCommandCellLeft"
			};
			Panel child = new Panel
			{
				CssClass = "rtlCommandCellRight"
			};
			this.CommandItemContentCell.Controls.Add(panel);
			this.CommandItemContentCell.Controls.Add(child);
			if (commandItemSettings.ShowExportToExcelButton)
			{
				this.InitializeExportToExcelButton(this.CommandItemContentCell, false);
			}
			if (commandItemSettings.ShowExportToPdfButton)
			{
				this.InitializeExportToPdfButton(this.CommandItemContentCell, false);
			}
			if (commandItemSettings.ShowExportToWordButton)
			{
				this.InitializeExportToWordButton(this.CommandItemContentCell, false);
			}
			if (panel.Controls.Count == 0)
			{
				panel.Visible = false;
			}
		}

		// Token: 0x06005B99 RID: 23449 RVA: 0x001172F0 File Offset: 0x001154F0
		private void InitializeExportToExcelButton(TableCell contentCell, bool isLeft)
		{
			Button button = new ElasticButton("t-font-icon rtlIcon rtlExpXLSIcon");
			button.ID = "ExportToExcelButton";
			button.CssClass = "t-button rtlActionButton rtlExpXLS";
			button.CommandName = "ExportToExcel";
			button.Text = base.OwnerTreeList.CommandItemSettings.ExportToExcelText;
			button.ToolTip = button.Text;
			button.UseSubmitBehavior = false;
			contentCell.Controls[isLeft ? 0 : 1].Controls.Add(button);
		}

		// Token: 0x06005B9A RID: 23450 RVA: 0x00117370 File Offset: 0x00115570
		private void InitializeExportToWordButton(TableCell contentCell, bool isLeft)
		{
			Button button = new ElasticButton("t-font-icon rtlIcon rtlExpDOCIcon");
			button.ID = "ExportToWordButton";
			button.CssClass = "t-button rtlActionButton rtlExpDOC";
			button.CommandName = "ExportToWord";
			button.Text = base.OwnerTreeList.CommandItemSettings.ExportToWordText;
			button.ToolTip = button.Text;
			button.UseSubmitBehavior = false;
			contentCell.Controls[isLeft ? 0 : 1].Controls.Add(button);
		}

		// Token: 0x06005B9B RID: 23451 RVA: 0x001173F0 File Offset: 0x001155F0
		private void InitializeExportToPdfButton(TableCell contentCell, bool isLeft)
		{
			Button button = new ElasticButton("t-font-icon rtlIcon rtlExpPDFIcon");
			button.ID = "ExportToPdfButton";
			button.CssClass = "t-button rtlActionButton rtlExpPDF";
			button.CommandName = "ExportToPdf";
			button.Text = base.OwnerTreeList.CommandItemSettings.ExportToPdfText;
			button.ToolTip = button.Text;
			button.UseSubmitBehavior = false;
			contentCell.Controls[isLeft ? 0 : 1].Controls.Add(button);
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x06005B9C RID: 23452 RVA: 0x0011746F File Offset: 0x0011566F
		public override TableRowSection TableSection
		{
			get
			{
				if (!this.IsTopItem)
				{
					return TableRowSection.TableFooter;
				}
				return TableRowSection.TableHeader;
			}
		}

		// Token: 0x17001E34 RID: 7732
		// (get) Token: 0x06005B9D RID: 23453 RVA: 0x0011747C File Offset: 0x0011567C
		// (set) Token: 0x06005B9E RID: 23454 RVA: 0x00117484 File Offset: 0x00115684
		public bool IsTopItem { get; internal set; }
	}
}
