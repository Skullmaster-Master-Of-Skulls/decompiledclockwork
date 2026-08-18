using System;
using System.Web.UI.HtmlControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000391 RID: 913
	internal class GridMobileExportView : GridMobileView
	{
		// Token: 0x06001F74 RID: 8052 RVA: 0x00063993 File Offset: 0x00061B93
		public GridMobileExportView(GridTableView tableView) : base(tableView)
		{
			base.Title = "Export";
			this.CssClass = "rgMobileExportForm";
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06001F75 RID: 8053 RVA: 0x000639B2 File Offset: 0x00061BB2
		public override GridMobileViewType Type
		{
			get
			{
				return GridMobileViewType.Export;
			}
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000639B8 File Offset: 0x00061BB8
		protected override void CreateContent(HtmlGenericControl container)
		{
			if (base.TableView.CommandItemSettings.ShowExportToWordButton)
			{
				container.Controls.Add(base.CreateButton(base.Localization.ExportToWordText, "rgColumnItem rgWordExport"));
			}
			if (base.TableView.CommandItemSettings.ShowExportToCsvButton)
			{
				container.Controls.Add(base.CreateButton(base.Localization.ExportToCsvText, "rgColumnItem rgCsvExport"));
			}
			if (base.TableView.CommandItemSettings.ShowExportToExcelButton)
			{
				container.Controls.Add(base.CreateButton(base.Localization.ExportToExcelText, "rgColumnItem rgExcelExport"));
			}
			if (base.TableView.CommandItemSettings.ShowExportToPdfButton)
			{
				container.Controls.Add(base.CreateButton(base.Localization.ExportToPdfText, "rgColumnItem rgPdfExport"));
			}
			if (base.TableView.CommandItemSettings.ShowPrintButton)
			{
				container.Controls.Add(base.CreateButton(base.Localization.PrintGridText, "rgColumnItem rgPrint"));
				if (base.TableView.OwnerGrid.ClientSettings.ViewState["EnableClientPrint"] == null)
				{
					base.TableView.OwnerGrid.ClientSettings.EnableClientPrint = true;
				}
			}
		}
	}
}
