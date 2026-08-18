using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200121E RID: 4638
	internal class TreeListExportInfrastructureExporter : TreeListExporter
	{
		// Token: 0x0600BF62 RID: 48994 RVA: 0x002A5FBE File Offset: 0x002A41BE
		internal TreeListExportInfrastructureExporter(RadTreeList treeList) : base(treeList)
		{
			this.treeList = this.treeList;
		}

		// Token: 0x0600BF63 RID: 48995 RVA: 0x002A5FDC File Offset: 0x002A41DC
		internal void ExportToWord()
		{
			this.treeList.CurrentExportFormat = new ExportFormat?(ExportFormat.Word);
			this.page = base.GetPage(this.treeList);
			this.page.SetRenderMethodDelegate(new RenderMethod(this.ExportRenderPage));
			this.page.PreRender += delegate(object sender, EventArgs args)
			{
				this.PrepareForExport();
			};
		}

		// Token: 0x0600BF64 RID: 48996 RVA: 0x002A6044 File Offset: 0x002A4244
		internal void ExportToExcel()
		{
			TreeListExcelFormat format = this.treeList.ExportSettings.Excel.Format;
			if (format == TreeListExcelFormat.Biff)
			{
				this.treeList.CurrentExportFormat = new ExportFormat?(ExportFormat.Excel);
			}
			else if (format == TreeListExcelFormat.Xlsx)
			{
				this.treeList.CurrentExportFormat = new ExportFormat?(ExportFormat.ExcelXlsx);
			}
			this.page = base.GetPage(this.treeList);
			this.page.SetRenderMethodDelegate(new RenderMethod(this.ExportRenderPage));
			this.page.PreRender += delegate(object sender, EventArgs args)
			{
				this.PrepareForExport();
			};
		}

		// Token: 0x0600BF65 RID: 48997 RVA: 0x002A60D4 File Offset: 0x002A42D4
		protected override void PrepareForExport()
		{
			if (this.treeList.ExportSettings.IgnorePaging)
			{
				this.treeList.AllowPaging = false;
				this.treeList.Rebind();
			}
			if (this.treeList.ExportSettings.ExportMode != TreeListExportMode.DefaultContent)
			{
				foreach (TreeListPagerItem treeListPagerItem in this.treeList.GetItems(new TreeListItemType[]
				{
					TreeListItemType.PagerItem
				}))
				{
					treeListPagerItem.Visible = false;
				}
				base.ClearControlsRecursively(this.treeList);
			}
		}

		// Token: 0x0600BF66 RID: 48998 RVA: 0x002A615F File Offset: 0x002A435F
		protected override void ExportRenderPage(HtmlTextWriter writer, Control pageCtrl)
		{
			base.ExportRenderPage(writer, pageCtrl);
		}

		// Token: 0x0600BF67 RID: 48999 RVA: 0x002A616C File Offset: 0x002A436C
		protected override void ExportRenderForm(HtmlTextWriter writer, Control pageCtrl)
		{
			HttpResponse response = this.page.Response;
			this.ConfigureResponse(this.treeList.CurrentExportFormat.Value, response);
			TreeListExportInfrastructureRenderer treeListExportInfrastructureRenderer = new TreeListExportInfrastructureRenderer(this.treeList, this.treeList.CurrentExportFormat.Value);
			byte[] array = treeListExportInfrastructureRenderer.Render();
			TreeListExportingEventArgs args = new TreeListExportingEventArgs(array, this.treeList.CurrentExportFormat.Value);
			this.treeList.CallOnExporting(args);
			response.BinaryWrite(array);
		}

		// Token: 0x0600BF68 RID: 49000 RVA: 0x002A61F8 File Offset: 0x002A43F8
		protected override void ConfigureResponse(ExportFormat exportFormat, HttpResponse response)
		{
			string contentType = string.Empty;
			string arg = string.Empty;
			switch (exportFormat)
			{
			case ExportFormat.Excel:
				contentType = "application/vnd.ms-excel";
				arg = ".xls";
				break;
			case ExportFormat.ExcelXlsx:
				contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
				arg = ".xlsx";
				break;
			case ExportFormat.Word:
				contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
				arg = ".docx";
				break;
			}
			response.Clear();
			response.BufferOutput = true;
			response.ContentType = contentType;
			response.ContentEncoding = Encoding.UTF8;
			response.Charset = null;
			string value = string.Format("{0};filename=\"{1}{2}\"", this.treeList.ExportSettings.OpenInNewWindow ? "attachment" : "inline", this.treeList.ExportSettings.FileName, arg);
			response.AddHeader("Content-Disposition", value);
		}
	}
}
