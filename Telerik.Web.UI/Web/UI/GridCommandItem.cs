using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001139 RID: 4409
	public class GridCommandItem : GridItem
	{
		// Token: 0x17003A07 RID: 14855
		// (get) Token: 0x0600B394 RID: 45972 RVA: 0x0027182B File Offset: 0x0026FA2B
		// (set) Token: 0x0600B395 RID: 45973 RVA: 0x0027185A File Offset: 0x0026FA5A
		[DefaultValue("Command item")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the caption for the command item.")]
		public virtual string Caption
		{
			get
			{
				if (this.ViewState["Caption"] == null)
				{
					return "Command item";
				}
				return (string)this.ViewState["Caption"];
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x17003A08 RID: 14856
		// (get) Token: 0x0600B396 RID: 45974 RVA: 0x0027186D File Offset: 0x0026FA6D
		// (set) Token: 0x0600B397 RID: 45975 RVA: 0x0027189C File Offset: 0x0026FA9C
		[NotifyParentProperty(true)]
		[DefaultValue("Command item")]
		[Description("Gets or sets the summary attribute for the command item.")]
		public virtual string Summary
		{
			get
			{
				if (this.ViewState["Summary"] == null)
				{
					return "Command item for additional functionalities for the grid like adding a new record and exporting.";
				}
				return (string)this.ViewState["Summary"];
			}
			set
			{
				this.ViewState["Summary"] = value;
			}
		}

		// Token: 0x0600B398 RID: 45976 RVA: 0x002718AF File Offset: 0x0026FAAF
		public GridCommandItem(GridTableView ownerTableView) : base(ownerTableView, -1, -1, GridItemType.CommandItem)
		{
		}

		// Token: 0x0600B399 RID: 45977 RVA: 0x002718BC File Offset: 0x0026FABC
		internal override void SetItemDecorator(GridItemDecorator newDecorator)
		{
			base.SetItemDecorator(new GridCommandItemDecorator(this));
		}

		// Token: 0x0600B39A RID: 45978 RVA: 0x002718CC File Offset: 0x0026FACC
		public override void SetupItem(bool dataBind, object dataItem, GridColumn[] columns, ControlCollection rows)
		{
			rows.Add(this);
			GridItemEventArgs e = new GridItemEventArgs(this, new GridItemCreated());
			this.InitializeCommandItem();
			base.OwnerTableView.OwnerGrid.CallOnItemCreated(e);
			if (dataBind)
			{
				this.DataBind();
				e = new GridItemEventArgs(this, new GridItemDataBound());
				base.OwnerTableView.OwnerGrid.CallOnItemDataBound(e);
			}
		}

		// Token: 0x0600B39B RID: 45979 RVA: 0x0027192C File Offset: 0x0026FB2C
		protected virtual void InitializeCommandItem()
		{
			TableCell tableCell = new TableCell();
			tableCell.CssClass = "rgCommandCell";
			this.Cells.Add(tableCell);
			if (base.OwnerTableView.CommandItemTemplate != null)
			{
				base.OwnerTableView.CommandItemTemplate.InstantiateIn(tableCell);
				return;
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
			{
				this.InitializeMobileDefault(tableCell);
				return;
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				this.InitializeLightweightDefault(tableCell);
				return;
			}
			this.InitializeDesktopDefault(tableCell);
		}

		// Token: 0x0600B39C RID: 45980 RVA: 0x002719B4 File Offset: 0x0026FBB4
		protected virtual void InitializeDesktopDefault(TableCell contentCell)
		{
			Table table = new Table();
			if (!string.IsNullOrEmpty(this.Summary))
			{
				table.Attributes["summary"] = this.Summary;
			}
			if (!string.IsNullOrEmpty(this.Caption))
			{
				table.Caption = string.Format("<span style='display: none'>{0}</span>", this.Caption);
			}
			TableHeaderRow tableHeaderRow = new TableHeaderRow();
			tableHeaderRow.TableSection = TableRowSection.TableHeader;
			table.Rows.Add(tableHeaderRow);
			TableHeaderCell tableHeaderCell = new TableHeaderCell();
			tableHeaderCell.Attributes["scope"] = "col";
			tableHeaderCell.Text = this.Caption;
			tableHeaderRow.Cells.Add(tableHeaderCell);
			table.Width = Unit.Percentage(100.0);
			table.CssClass = "rgCommandTable";
			TableRow tableRow = new TableRow();
			table.Rows.Add(tableRow);
			TableCell tableCell = new TableCell();
			tableCell.HorizontalAlign = base.LeftAlign();
			tableRow.Cells.Add(tableCell);
			if (base.OwnerTableView.CommandItemSettings.ShowAddNewRecordButton)
			{
				this.InitializeInsertButton(tableCell);
			}
			else
			{
				tableCell.Controls.Add(new LiteralControl("&nbsp;"));
			}
			this.InitializeBatchUpdateButtons(tableCell);
			tableCell = new TableCell();
			tableCell.HorizontalAlign = base.RightAlign();
			tableRow.Cells.Add(tableCell);
			if (base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.EnableNextPrevFrozenColumns && base.OwnerTableView == base.OwnerTableView.OwnerGrid.MasterTableView)
			{
				this.InitializeFrozenColumnButtons(tableCell);
			}
			if (base.OwnerTableView.CommandItemSettings.ShowRefreshButton)
			{
				this.InitializeRefreshButton(tableCell);
			}
			else
			{
				tableCell.Controls.Add(new LiteralControl("&nbsp;"));
			}
			if (base.OwnerTableView.CommandItemSettings.ShowExportToExcelButton || base.OwnerTableView.CommandItemSettings.ShowExportToPdfButton || base.OwnerTableView.CommandItemSettings.ShowExportToCsvButton || base.OwnerTableView.CommandItemSettings.ShowExportToWordButton)
			{
				tableCell.Controls.Add(new LiteralControl("&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;"));
			}
			if (base.OwnerTableView.CommandItemSettings.ShowExportToExcelButton)
			{
				this.InitializeExportToExcelButton(tableCell);
				tableCell.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
			}
			if (base.OwnerTableView.CommandItemSettings.ShowExportToPdfButton)
			{
				this.InitializeExportToPdfButton(tableCell);
				tableCell.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
			}
			if (base.OwnerTableView.CommandItemSettings.ShowExportToCsvButton)
			{
				this.InitializeExportToCsvButton(tableCell);
				tableCell.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));
			}
			if (base.OwnerTableView.CommandItemSettings.ShowExportToWordButton)
			{
				this.InitializeExportToWordButton(tableCell);
			}
			contentCell.Controls.Add(table);
		}

		// Token: 0x0600B39D RID: 45981 RVA: 0x00271C90 File Offset: 0x0026FE90
		protected virtual void InitializeLightweightDefault(TableCell contentCell)
		{
			Panel child = new Panel
			{
				CssClass = "rgCommandCellLeft"
			};
			Panel child2 = new Panel
			{
				CssClass = "rgCommandCellRight"
			};
			contentCell.Controls.Add(child);
			contentCell.Controls.Add(child2);
			GridCommandItemSettings commandItemSettings = base.OwnerTableView.CommandItemSettings;
			if (commandItemSettings.ShowAddNewRecordButton)
			{
				this.InitializeInsertButton(contentCell);
			}
			this.InitializeBatchUpdateButtons(contentCell);
			if (base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.EnableNextPrevFrozenColumns && base.OwnerTableView == base.OwnerTableView.OwnerGrid.MasterTableView)
			{
				this.InitializeFrozenColumnButtons(contentCell);
			}
			if (commandItemSettings.ShowRefreshButton)
			{
				this.InitializeRefreshButton(contentCell);
			}
			if (commandItemSettings.ShowExportToExcelButton)
			{
				this.InitializeExportToExcelButton(contentCell);
			}
			if (commandItemSettings.ShowExportToPdfButton)
			{
				this.InitializeExportToPdfButton(contentCell);
			}
			if (commandItemSettings.ShowExportToCsvButton)
			{
				this.InitializeExportToCsvButton(contentCell);
			}
			if (commandItemSettings.ShowExportToWordButton)
			{
				this.InitializeExportToWordButton(contentCell);
			}
			if (base.OwnerTableView.CommandItemSettings.ShowPrintButton)
			{
				this.InitializePrintButton(contentCell);
				if (base.OwnerTableView.OwnerGrid.ClientSettings.ViewState["EnableClientPrint"] == null)
				{
					base.OwnerTableView.OwnerGrid.ClientSettings.EnableClientPrint = true;
				}
			}
		}

		// Token: 0x0600B39E RID: 45982 RVA: 0x00271DD8 File Offset: 0x0026FFD8
		protected virtual void InitializeMobileDefault(TableCell contentCell)
		{
			GridCommandItemSettings commandItemSettings = base.OwnerTableView.CommandItemSettings;
			if (commandItemSettings.ShowAddNewRecordButton)
			{
				contentCell.Controls.Add(RadGrid.CreateButton("Add", base.OwnerTableView.CommandItemSettings.AddNewRecordText, true));
			}
			if (base.OwnerTableView.EditMode == GridEditMode.Batch)
			{
				if (!base.OwnerTableView.CommandItemSettings.IsShowSaveChangesButtonSet || base.OwnerTableView.CommandItemSettings.ShowSaveChangesButton)
				{
					contentCell.Controls.Add(RadGrid.CreateButton("Save", base.OwnerTableView.CommandItemSettings.SaveChangesText, true));
				}
				if (!base.OwnerTableView.CommandItemSettings.IsShowCancelChangesButtonSet || base.OwnerTableView.CommandItemSettings.ShowCancelChangesButton)
				{
					contentCell.Controls.Add(RadGrid.CreateButton("Cancel", base.OwnerTableView.CommandItemSettings.CancelChangesText, true));
				}
			}
			if (commandItemSettings.ShowExportToExcelButton || commandItemSettings.ShowExportToPdfButton || commandItemSettings.ShowExportToCsvButton || commandItemSettings.ShowExportToWordButton || commandItemSettings.ShowPrintButton)
			{
				contentCell.Controls.Add(RadGrid.CreateButton("Export", true));
			}
			if (base.OwnerTableView.OwnerGrid.ClientSettings.AllowColumnsReorder || base.OwnerTableView.OwnerGrid.ClientSettings.AllowColumnHide)
			{
				contentCell.Controls.Add(RadGrid.CreateButton("Menu", true));
			}
			if (base.OwnerTableView.OwnerGrid.ClientSettings.Scrolling.EnableNextPrevFrozenColumns && base.OwnerTableView == base.OwnerTableView.OwnerGrid.MasterTableView)
			{
				this.InitializeFrozenColumnButtons(contentCell);
			}
		}

		// Token: 0x0600B39F RID: 45983 RVA: 0x00271F84 File Offset: 0x00270184
		private void InitializeInsertButton(TableCell cell)
		{
			if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(base.OwnerTableView.CommandItemSettings.AddNewRecordImageUrl))
			{
				LinkButton linkButton = new GridLinkButton();
				linkButton.ID = "InitInsertButton";
				linkButton.CommandName = "InitInsert";
				linkButton.CausesValidation = false;
				if (base.OwnerTableView.EditMode == GridEditMode.Batch)
				{
					linkButton.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "addNewRecord", new string[]
					{
						base.OwnerTableView.ClientID
					});
				}
				else if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					linkButton.OnClientClick = string.Format("if(!$find('{0}').showInsertItem()) return false;", base.OwnerTableView.ClientID);
				}
				string arg = HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.AddNewRecordText);
				linkButton.Text = string.Format("<img style=\"border:0px\" alt=\"\" src=\"{0}\" /> {1}", HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.AddNewRecordImageUrl), arg);
				cell.Controls.Add(linkButton);
				return;
			}
			Button button;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button = new ElasticButton("t-font-icon rgIcon rgAddIcon", "t-text rgButtonText");
				button.CssClass = "t-button t-button-icontext rgActionButton ";
				button.Text = base.OwnerTableView.CommandItemSettings.AddNewRecordText;
				button.ToolTip = button.Text;
				button.UseSubmitBehavior = false;
			}
			else
			{
				button = new Button();
				button.Text = " ";
			}
			button.ID = "AddNewRecordButton";
			button.CommandName = "InitInsert";
			button.CausesValidation = false;
			Button button2 = button;
			button2.CssClass += "rgAdd";
			button.ToolTip = base.OwnerTableView.CommandItemSettings.AddNewRecordText;
			if (base.OwnerTableView.EditMode == GridEditMode.Batch)
			{
				button.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "addNewRecord", new string[]
				{
					base.OwnerTableView.ClientID
				});
			}
			else if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				button.OnClientClick = string.Format("if(!$find('{0}').showInsertItem()) return false;", base.OwnerTableView.ClientID);
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				cell.Controls[0].Controls.Add(button);
			}
			else
			{
				cell.Controls.Add(button);
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight)
			{
				LinkButton linkButton2 = new GridLinkButton();
				linkButton2.ID = "InitInsertButton";
				linkButton2.CommandName = "InitInsert";
				linkButton2.CausesValidation = false;
				linkButton2.Text = HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.AddNewRecordText);
				linkButton2.ToolTip = linkButton2.Text;
				if (base.OwnerTableView.EditMode == GridEditMode.Batch)
				{
					linkButton2.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "addNewRecord", new string[]
					{
						base.OwnerTableView.ClientID
					});
				}
				else if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					linkButton2.OnClientClick = string.Format("if(!$find('{0}').showInsertItem()) return false;", base.OwnerTableView.ClientID);
				}
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					cell.Controls[0].Controls.Add(linkButton2);
					return;
				}
				cell.Controls.Add(linkButton2);
			}
		}

		// Token: 0x0600B3A0 RID: 45984 RVA: 0x002722E8 File Offset: 0x002704E8
		private void InitializeExportToExcelButton(TableCell cell)
		{
			if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(base.OwnerTableView.CommandItemSettings.ExportToExcelImageUrl))
			{
				LinkButton linkButton = new GridLinkButton();
				linkButton.ID = "ExportToExcelButton";
				linkButton.CommandName = "ExportToExcel";
				linkButton.CausesValidation = false;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					linkButton.OnClientClick = string.Format("if(!$find('{0}').exportToExcel()) return false;", base.OwnerTableView.ClientID);
				}
				string arg = HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.ExportToExcelText);
				linkButton.Text = string.Format("<img style=\"border:0px\" alt=\"\" src=\"{0}\" /> {1}", HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.ExportToExcelImageUrl), arg);
				cell.Controls.Add(linkButton);
				return;
			}
			Button button;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button = new ElasticButton("t-font-icon rgIcon rgExpXLSIcon");
				button.CssClass = "t-button rgActionButton ";
				button.Text = base.OwnerTableView.CommandItemSettings.ExportToExcelText;
				button.ToolTip = button.Text;
				if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
				{
					button.Attributes.Add("aria-label", button.Text);
				}
				button.UseSubmitBehavior = false;
			}
			else
			{
				button = new Button();
				button.Text = " ";
			}
			button.ID = "ExportToExcelButton";
			button.CommandName = "ExportToExcel";
			button.CausesValidation = false;
			Button button2 = button;
			button2.CssClass += "rgExpXLS";
			button.ToolTip = base.OwnerTableView.CommandItemSettings.ExportToExcelText;
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				button.OnClientClick = string.Format("if(!$find('{0}').exportToExcel()) return false;", base.OwnerTableView.ClientID);
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				cell.Controls[1].Controls.Add(button);
				return;
			}
			cell.Controls.Add(button);
		}

		// Token: 0x0600B3A1 RID: 45985 RVA: 0x002724F0 File Offset: 0x002706F0
		private void InitializeExportToWordButton(TableCell cell)
		{
			if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(base.OwnerTableView.CommandItemSettings.ExportToWordImageUrl))
			{
				LinkButton linkButton = new GridLinkButton();
				linkButton.ID = "ExportToWordButton";
				linkButton.CommandName = "ExportToWord";
				linkButton.CausesValidation = false;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					linkButton.OnClientClick = string.Format("if(!$find('{0}').exportToWord()) return false;", base.OwnerTableView.ClientID);
				}
				string arg = HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.ExportToWordText);
				linkButton.Text = string.Format("<img style=\"border:0px\" alt=\"\" src=\"{0}\" /> {1}", HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.ExportToWordImageUrl), arg);
				cell.Controls.Add(linkButton);
				return;
			}
			Button button;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button = new ElasticButton("t-font-icon rgIcon rgExpDOCIcon");
				button.CssClass = "t-button rgActionButton ";
				button.Text = base.OwnerTableView.CommandItemSettings.ExportToWordText;
				button.ToolTip = button.Text;
				if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
				{
					button.Attributes.Add("aria-label", button.Text);
				}
				button.UseSubmitBehavior = false;
			}
			else
			{
				button = new Button();
				button.Text = " ";
			}
			button.ID = "ExportToWordButton";
			button.CommandName = "ExportToWord";
			button.CausesValidation = false;
			Button button2 = button;
			button2.CssClass += "rgExpDOC";
			button.ToolTip = base.OwnerTableView.CommandItemSettings.ExportToWordText;
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				button.OnClientClick = string.Format("if(!$find('{0}').exportToWord()) return false;", base.OwnerTableView.ClientID);
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				cell.Controls[1].Controls.Add(button);
				return;
			}
			cell.Controls.Add(button);
		}

		// Token: 0x0600B3A2 RID: 45986 RVA: 0x002726F8 File Offset: 0x002708F8
		private void InitializeExportToPdfButton(TableCell cell)
		{
			if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(base.OwnerTableView.CommandItemSettings.ExportToPdfImageUrl))
			{
				LinkButton linkButton = new GridLinkButton();
				linkButton.ID = "ExportToPdfButton";
				linkButton.CommandName = "ExportToPdf";
				linkButton.CausesValidation = false;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					linkButton.OnClientClick = string.Format("if(!$find('{0}').exportToPdf()) return false;", base.OwnerTableView.ClientID);
				}
				string exportToPdfText = base.OwnerTableView.CommandItemSettings.ExportToPdfText;
				linkButton.Text = string.Format("<img style=\"border:0px\" alt=\"\" src=\"{0}\" /> {1}", HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.ExportToPdfImageUrl), HttpUtility.HtmlEncode(exportToPdfText));
				cell.Controls.Add(linkButton);
				return;
			}
			Button button;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button = new ElasticButton("t-font-icon rgIcon rgExpPDFIcon");
				button.CssClass = "t-button rgActionButton ";
				button.Text = base.OwnerTableView.CommandItemSettings.ExportToPdfText;
				button.ToolTip = button.Text;
				if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
				{
					button.Attributes.Add("aria-label", button.Text);
				}
				button.UseSubmitBehavior = false;
			}
			else
			{
				button = new Button();
				button.Text = " ";
			}
			button.ID = "ExportToPdfButton";
			button.CommandName = "ExportToPdf";
			button.CausesValidation = false;
			Button button2 = button;
			button2.CssClass += "rgExpPDF";
			button.ToolTip = base.OwnerTableView.CommandItemSettings.ExportToPdfText;
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				button.OnClientClick = string.Format("if(!$find('{0}').exportToPdf()) return false;", base.OwnerTableView.ClientID);
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				cell.Controls[1].Controls.Add(button);
				return;
			}
			cell.Controls.Add(button);
		}

		// Token: 0x0600B3A3 RID: 45987 RVA: 0x00272900 File Offset: 0x00270B00
		private void InitializeExportToCsvButton(TableCell cell)
		{
			if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(base.OwnerTableView.CommandItemSettings.ExportToCsvImageUrl))
			{
				LinkButton linkButton = new GridLinkButton();
				linkButton.ID = "ExportToCsvButton";
				linkButton.CommandName = "ExportToCsv";
				linkButton.CausesValidation = false;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					linkButton.OnClientClick = string.Format("if(!$find('{0}').exportToCsv()) return false;", base.OwnerTableView.ClientID);
				}
				string exportToCsvText = base.OwnerTableView.CommandItemSettings.ExportToCsvText;
				linkButton.Text = string.Format("<img style=\"border:0px\" alt=\"\" src=\"{0}\" /> {1}", HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.ExportToCsvImageUrl), HttpUtility.HtmlEncode(exportToCsvText));
				cell.Controls.Add(linkButton);
				return;
			}
			Button button;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button = new ElasticButton("t-font-icon rgIcon rgExpCSVIcon");
				button.CssClass = "t-button rgActionButton ";
				button.Text = base.OwnerTableView.CommandItemSettings.ExportToCsvText;
				button.ToolTip = button.Text;
				if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
				{
					button.Attributes.Add("aria-label", button.Text);
				}
				button.UseSubmitBehavior = false;
			}
			else
			{
				button = new Button();
				button.Text = " ";
			}
			button.ID = "ExportToCsvButton";
			button.CommandName = "ExportToCsv";
			button.CausesValidation = false;
			Button button2 = button;
			button2.CssClass += "rgExpCSV";
			button.ToolTip = base.OwnerTableView.CommandItemSettings.ExportToCsvText;
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				button.OnClientClick = string.Format("if(!$find('{0}').exportToCsv()) return false;", base.OwnerTableView.ClientID);
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				cell.Controls[1].Controls.Add(button);
				return;
			}
			cell.Controls.Add(button);
		}

		// Token: 0x0600B3A4 RID: 45988 RVA: 0x00272B08 File Offset: 0x00270D08
		private void InitializePrintButton(TableCell cell)
		{
			Button button = new ElasticButton("t-font-icon rgIcon rgPrintIcon");
			button.ID = "PrintButton";
			button.CssClass = "t-button rgActionButton rgPrint";
			button.Text = base.OwnerTableView.CommandItemSettings.PrintGridText;
			button.ToolTip = button.Text;
			button.UseSubmitBehavior = false;
			button.CausesValidation = false;
			button.ToolTip = base.OwnerTableView.CommandItemSettings.PrintGridText;
			if (base.OwnerTableView.OwnerGrid.EnableAriaSupport)
			{
				button.Attributes.Add("aria-label", button.Text);
			}
			button.OnClientClick = string.Format("$find('{0}').print(); return false;", this.OwnerGridID);
			cell.Controls[1].Controls.Add(button);
		}

		// Token: 0x0600B3A5 RID: 45989 RVA: 0x00272BD4 File Offset: 0x00270DD4
		private void InitializeFrozenColumnButtons(TableCell cell)
		{
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				ElasticButton elasticButton = new ElasticButton
				{
					OnClientClick = "return false",
					CssClass = "t-button rgActionButton ",
					FirstSpanClass = "t-font-icon rgIcon rgNextIcon"
				};
				elasticButton.ToolTip = base.OwnerTableView.CommandItemSettings.NextFrozenColumnText;
				ElasticButton elasticButton2 = elasticButton;
				elasticButton2.CssClass += "rgNext";
				cell.Controls.Add(elasticButton);
				ElasticButton elasticButton3 = new ElasticButton
				{
					OnClientClick = "return false",
					CssClass = "t-button rgActionButton ",
					FirstSpanClass = "t-font-icon rgIcon rgPrevIcon"
				};
				elasticButton3.ToolTip = base.OwnerTableView.CommandItemSettings.PrevFrozenColumnText;
				ElasticButton elasticButton4 = elasticButton3;
				elasticButton4.CssClass += "rgDisabled rgPrev";
				cell.Controls.Add(elasticButton3);
				return;
			}
			HyperLink hyperLink = new HyperLink();
			Label child = new Label();
			Literal literal = new Literal();
			hyperLink.NavigateUrl = "#";
			hyperLink.CssClass = "rgDisabled rgPrev";
			literal.Text = base.OwnerTableView.CommandItemSettings.PrevFrozenColumnText;
			hyperLink.Controls.Add(child);
			hyperLink.Controls.Add(literal);
			cell.Controls.Add(hyperLink);
			HyperLink hyperLink2 = new HyperLink();
			Label child2 = new Label();
			Literal literal2 = new Literal();
			hyperLink2.NavigateUrl = "#";
			hyperLink2.CssClass = "rgNext";
			literal2.Text = base.OwnerTableView.CommandItemSettings.NextFrozenColumnText;
			hyperLink2.Controls.Add(literal2);
			hyperLink2.Controls.Add(child2);
			cell.Controls.Add(hyperLink2);
		}

		// Token: 0x0600B3A6 RID: 45990 RVA: 0x00272DA8 File Offset: 0x00270FA8
		private void InitializeRefreshButton(TableCell cell)
		{
			if (base.OwnerTableView.OwnerGrid.ShouldRenderImg(base.OwnerTableView.CommandItemSettings.RefreshImageUrl))
			{
				GridLinkButton gridLinkButton = new GridLinkButton();
				gridLinkButton.ID = "RebindGridButton";
				gridLinkButton.CommandName = "RebindGrid";
				gridLinkButton.CausesValidation = false;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					gridLinkButton.OnClientClick = string.Format("if(!$find('{0}').rebind()) return false;", base.OwnerTableView.ClientID);
				}
				string refreshText = base.OwnerTableView.CommandItemSettings.RefreshText;
				gridLinkButton.Text = string.Format("<img style=\"border:0px\" alt=\"\" src=\"{0}\" /> {1}", HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.RefreshImageUrl), HttpUtility.HtmlEncode(refreshText));
				cell.Controls.Add(gridLinkButton);
				return;
			}
			Button button;
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				button = new ElasticButton("t-font-icon rgIcon rgRefreshIcon", "t-text rgButtonText");
				button.CssClass = "t-button t-button-icontext rgActionButton ";
				button.Text = base.OwnerTableView.CommandItemSettings.RefreshText;
				button.ToolTip = button.Text;
				button.UseSubmitBehavior = false;
			}
			else
			{
				button = new Button();
				button.Text = " ";
			}
			button.ID = "RefreshButton";
			button.CommandName = "RebindGrid";
			button.CausesValidation = false;
			if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
			{
				button.OnClientClick = string.Format("if(!$find('{0}').rebind()) return false;", base.OwnerTableView.ClientID);
			}
			button.ToolTip = base.OwnerTableView.CommandItemSettings.RefreshText;
			Button button2 = button;
			button2.CssClass += "rgRefresh";
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
			{
				cell.Controls[1].Controls.Add(button);
			}
			else
			{
				cell.Controls.Add(button);
			}
			if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight)
			{
				GridLinkButton gridLinkButton2 = new GridLinkButton();
				gridLinkButton2.ID = "RebindGridButton";
				gridLinkButton2.CommandName = "RebindGrid";
				gridLinkButton2.CausesValidation = false;
				if (base.OwnerTableView.OwnerGrid.IsClientCommandAssigned)
				{
					gridLinkButton2.OnClientClick = string.Format("if(!$find('{0}').rebind()) return false;", base.OwnerTableView.ClientID);
				}
				gridLinkButton2.Text = HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.RefreshText);
				gridLinkButton2.ToolTip = gridLinkButton2.Text;
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					cell.Controls[1].Controls.Add(gridLinkButton2);
					return;
				}
				cell.Controls.Add(gridLinkButton2);
			}
		}

		// Token: 0x0600B3A7 RID: 45991 RVA: 0x00273050 File Offset: 0x00271250
		protected virtual void InitializeBatchUpdateButtons(TableCell cell)
		{
			GridCommandItemSettings commandItemSettings = base.OwnerTableView.CommandItemSettings;
			if (commandItemSettings.ShowSaveChangesButton || (!commandItemSettings.IsShowSaveChangesButtonSet && base.OwnerTableView.EditMode == GridEditMode.Batch))
			{
				Button button;
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					button = new ElasticButton("t-font-icon rgIcon rgSaveIcon", "t-text rgButtonText");
					button.CssClass = "t-button t-button-icontext rgActionButton ";
					button.Text = base.OwnerTableView.CommandItemSettings.SaveChangesText;
					cell.Controls[0].Controls.Add(button);
				}
				else
				{
					button = new Button();
					cell.Controls.Add(button);
				}
				button.ID = "SaveChangesIcon";
				Button button2 = button;
				button2.CssClass += "rgSave";
				button.ToolTip = base.OwnerTableView.CommandItemSettings.SaveChangesText;
				if (base.OwnerTableView.BatchEditingSettings.SaveAllHierarchyLevels)
				{
					button.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "saveAllChanges", new string[0]);
				}
				else
				{
					button.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "saveChanges", new string[]
					{
						base.OwnerTableView.ClientID
					});
				}
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight)
				{
					LinkButton linkButton = new LinkButton();
					cell.Controls.Add(linkButton);
					linkButton.ID = "SaveChangesButton";
					linkButton.Text = HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.SaveChangesText);
					linkButton.ToolTip = base.OwnerTableView.CommandItemSettings.SaveChangesText;
					linkButton.ValidationGroup = GridBatchEditingHelper.ValidationGroupName;
					if (base.OwnerTableView.BatchEditingSettings.SaveAllHierarchyLevels)
					{
						linkButton.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "saveAllChanges", new string[0]);
					}
					else
					{
						linkButton.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "saveChanges", new string[]
						{
							base.OwnerTableView.ClientID
						});
					}
				}
			}
			if (commandItemSettings.ShowCancelChangesButton || (!commandItemSettings.IsShowCancelChangesButtonSet && base.OwnerTableView.EditMode == GridEditMode.Batch))
			{
				Button button3;
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight)
				{
					button3 = new ElasticButton("t-font-icon rgIcon rgCancelIcon", "t-text rgButtonText");
					button3.CssClass = "t-button t-button-icontext rgActionButton ";
					button3.Text = base.OwnerTableView.CommandItemSettings.CancelChangesText;
					cell.Controls[0].Controls.Add(button3);
				}
				else
				{
					button3 = new Button();
					cell.Controls.Add(button3);
				}
				button3.ID = "CancelChangesIcon";
				button3.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "cancelChanges", new string[]
				{
					base.OwnerTableView.ClientID
				});
				Button button4 = button3;
				button4.CssClass += "rgCancel";
				button3.ToolTip = base.OwnerTableView.CommandItemSettings.CancelChangesText;
				if (base.OwnerTableView.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight)
				{
					LinkButton linkButton2 = new LinkButton();
					cell.Controls.Add(linkButton2);
					linkButton2.ID = "CancelChangesButton";
					linkButton2.Text = HttpUtility.HtmlEncode(base.OwnerTableView.CommandItemSettings.CancelChangesText);
					linkButton2.ToolTip = base.OwnerTableView.CommandItemSettings.CancelChangesText;
					linkButton2.CausesValidation = false;
					linkButton2.OnClientClick = GridBatchEditingHelper.GenerateClientScript(base.OwnerTableView, "cancelChanges", new string[]
					{
						base.OwnerTableView.ClientID
					});
				}
			}
		}

		// Token: 0x0600B3A8 RID: 45992 RVA: 0x002733FC File Offset: 0x002715FC
		public override void PrepareItemStyle()
		{
			if (this.Cells.Count == 0)
			{
				return;
			}
			this.Cells[this.Cells.Count - 1].ColumnSpan = base.CalcColSpan(base.OwnerTableView.RenderColumns, this.Cells.Count - 1, -1);
			base.PrepareItemStyle();
		}

		// Token: 0x0600B3A9 RID: 45993 RVA: 0x00273459 File Offset: 0x00271659
		public override void PrepareItemVisibility()
		{
		}
	}
}
