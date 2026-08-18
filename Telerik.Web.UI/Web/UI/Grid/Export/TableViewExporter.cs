using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using Telerik.Web.Apoc;
using Telerik.Web.Apoc.Render.Pdf;
using Telerik.Web.UI.ExportInfrastructure;
using Telerik.Web.UI.GridExcelBuilder;
using Telerik.Windows.Documents.Spreadsheet.Model;

namespace Telerik.Web.UI.Grid.Export
{
	// Token: 0x020011B3 RID: 4531
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class TableViewExporter
	{
		// Token: 0x0600B9EF RID: 47599 RVA: 0x00294308 File Offset: 0x00292508
		public TableViewExporter(GridTableView tableView, string fileName, bool dataOnly, bool ignorePaging)
		{
			this.tableView = tableView;
			this.fileName = fileName;
			this.ignorePaging = ignorePaging;
			this.dataOnly = dataOnly;
			this.tableView.OwnerGrid.IsExporting = true;
		}

		// Token: 0x0600B9F0 RID: 47600 RVA: 0x00294354 File Offset: 0x00292554
		public TableViewExporter(GridTableView tableView, GridExportSettings setting)
		{
			this.tableView = tableView;
			this.fileName = setting.FileName;
			this.ignorePaging = setting.IgnorePaging;
			this.dataOnly = setting.ExportOnlyData;
			this.openInNewWindow = setting.OpenInNewWindow;
			this.tableView.OwnerGrid.IsExporting = true;
		}

		// Token: 0x0600B9F1 RID: 47601 RVA: 0x002943BC File Offset: 0x002925BC
		internal void ExportToCSV()
		{
			Page page = this.GetPage();
			this.exportFormat = ExportType.Csv;
			page.SetRenderMethodDelegate(new RenderMethod(this.CSVExportRenderPage));
			page.PreRender += this.page_PreRender;
		}

		// Token: 0x0600B9F2 RID: 47602 RVA: 0x002943FC File Offset: 0x002925FC
		public string GenerateXlsxOutput()
		{
			XlsxRenderer xlsxRenderer = this.GetXlsxRenderer();
			byte[] bytes = xlsxRenderer.Render(null);
			return Encoding.GetEncoding(1252).GetString(bytes);
		}

		// Token: 0x0600B9F3 RID: 47603 RVA: 0x00294428 File Offset: 0x00292628
		public object GenerateXlsxOutput<T>()
		{
			XlsxRenderer xlsxRenderer = this.GetXlsxRenderer();
			if (typeof(T) == typeof(Workbook))
			{
				return xlsxRenderer.CreateWorkbook();
			}
			if (typeof(T) == typeof(byte[]))
			{
				return xlsxRenderer.Render(null);
			}
			byte[] bytes = xlsxRenderer.Render(null);
			return Encoding.GetEncoding(1252).GetString(bytes);
		}

		// Token: 0x0600B9F4 RID: 47604 RVA: 0x0029449C File Offset: 0x0029269C
		private XlsxRenderer GetXlsxRenderer()
		{
			if (this.tableView.OwnerGrid.ExportSettings.HideStructureColumns)
			{
				this.HideStructureColumnCells(this.tableView);
			}
			this.PrepareForExport();
			GridInfrastructureExporter gridInfrastructureExporter = new GridInfrastructureExporter(this.tableView);
			ExportStructure structure = gridInfrastructureExporter.GenerateStructure();
			return new XlsxRenderer(structure);
		}

		// Token: 0x0600B9F5 RID: 47605 RVA: 0x002944F0 File Offset: 0x002926F0
		public void ExportToExcel()
		{
			Page page = this.GetPage();
			switch (this.tableView.OwnerGrid.ExportSettings.Excel.Format)
			{
			case GridExcelExportFormat.Html:
				this.exportFormat = ExportType.Excel;
				break;
			case GridExcelExportFormat.ExcelML:
				this.exportFormat = ExportType.ExcelML;
				this.tableView.UseAllDataFields = true;
				break;
			case GridExcelExportFormat.Biff:
				this.exportFormat = ExportType.ExcelBiff;
				break;
			case GridExcelExportFormat.Xlsx:
				this.exportFormat = ExportType.ExcelXlsx;
				break;
			}
			page.SetRenderMethodDelegate(new RenderMethod(this.ExcelExportRenderPage));
			page.PreRender += this.page_PreRender;
		}

		// Token: 0x0600B9F6 RID: 47606 RVA: 0x00294589 File Offset: 0x00292789
		private void page_PreRender(object sender, EventArgs e)
		{
			this.PrepareForExport();
		}

		// Token: 0x0600B9F7 RID: 47607 RVA: 0x00294594 File Offset: 0x00292794
		public void ExportToWord()
		{
			Page page = this.GetPage();
			this.exportFormat = ((this.tableView.OwnerGrid.ExportSettings.Word.Format == GridWordExportFormat.Html) ? ExportType.Word : ExportType.WordDocx);
			page.SetRenderMethodDelegate(new RenderMethod(this.WordExportRenderPage));
			page.PreRender += this.page_PreRender;
		}

		// Token: 0x0600B9F8 RID: 47608 RVA: 0x00294688 File Offset: 0x00292888
		public void ExportToPdf()
		{
			Page page = this.GetPage();
			this.exportFormat = ExportType.Pdf;
			page.SetRenderMethodDelegate(new RenderMethod(this.PdfExportRenderPage));
			page.PreRender += this.page_PreRender;
			page.PreRenderComplete += delegate(object sender, EventArgs args)
			{
				if (string.IsNullOrEmpty(this.tableView.OwnerGrid.Skin.Trim()) && this.tableView.Width == Unit.Empty)
				{
					this.tableView.Width = Unit.Percentage(100.0);
				}
				GridPdfSettings pdf = this.tableView.OwnerGrid.ExportSettings.Pdf;
				if (pdf.BorderType != GridPdfSettings.GridPdfBorderType.Separate)
				{
					this.tableView.OwnerGrid.BorderStyle = BorderStyle.None;
					this.PrepareGridBorders(this.tableView);
				}
			};
		}

		// Token: 0x0600B9F9 RID: 47609 RVA: 0x002946DC File Offset: 0x002928DC
		private void PrepareGridBorders(GridTableView tableView)
		{
			GridPdfSettings pdf = tableView.OwnerGrid.ExportSettings.Pdf;
			if (pdf.BorderType == GridPdfSettings.GridPdfBorderType.OuterBorders)
			{
				AttributeCollection attributes;
				(attributes = tableView.Attributes)["style"] = attributes["style"] + this.GenerateBorderStyleString(pdf);
			}
			else if (pdf.BorderType == GridPdfSettings.GridPdfBorderType.AllBorders || pdf.BorderType == GridPdfSettings.GridPdfBorderType.TopAndBottom)
			{
				tableView.BorderStyle = BorderStyle.None;
				AttributeCollection attributes2;
				(attributes2 = tableView.Attributes)["style"] = attributes2["style"] + this.GenerateBorderStyleString(pdf);
				foreach (GridItem gridItem in tableView.GetItems(TableViewExporter.itemsToIterate))
				{
					foreach (object obj in gridItem.Cells)
					{
						TableCell tableCell = (TableCell)obj;
						AttributeCollection attributes3;
						(attributes3 = tableCell.Attributes)["style"] = attributes3["style"] + this.GenerateBorderStyleString(pdf);
					}
				}
			}
			else if (pdf.BorderType == GridPdfSettings.GridPdfBorderType.NoBorder)
			{
				tableView.BorderStyle = BorderStyle.None;
			}
			foreach (GridNestedViewItem gridNestedViewItem in tableView.GetItems(new GridItemType[]
			{
				GridItemType.NestedView
			}))
			{
				if (gridNestedViewItem.NestedTableViews.Length > 0)
				{
					foreach (GridTableView gridTableView in gridNestedViewItem.NestedTableViews)
					{
						this.PrepareGridBorders(gridTableView);
					}
				}
			}
		}

		// Token: 0x0600B9FA RID: 47610 RVA: 0x00294894 File Offset: 0x00292A94
		private string GenerateBorderStyleString(GridPdfSettings gridPdfSettings)
		{
			string arg = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			string[] array;
			if (gridPdfSettings.BorderType == GridPdfSettings.GridPdfBorderType.TopAndBottom)
			{
				array = new string[]
				{
					"top",
					"bottom"
				};
			}
			else
			{
				array = new string[]
				{
					"top",
					"bottom",
					"left",
					"right"
				};
			}
			switch (gridPdfSettings.BorderStyle)
			{
			case GridPdfSettings.GridPdfBorderStyle.Medium:
				arg = "1pt";
				break;
			case GridPdfSettings.GridPdfBorderStyle.Thick:
				arg = "2pt";
				break;
			case GridPdfSettings.GridPdfBorderStyle.Thin:
				arg = "0.5pt";
				break;
			}
			foreach (string arg2 in array)
			{
				stringBuilder.AppendFormat("border-{0}-style: {1}; ", arg2, "solid");
				stringBuilder.AppendFormat("border-{0}-color: {1}; ", arg2, TableViewExporter.ConvertColorToHexString(gridPdfSettings.BorderColor));
				stringBuilder.AppendFormat("border-{0}-width: {1}; ", arg2, arg);
			}
			stringBuilder.AppendFormat("border-collapse: collapse; ", new object[0]);
			return stringBuilder.ToString();
		}

		// Token: 0x0600B9FB RID: 47611 RVA: 0x002949A3 File Offset: 0x00292BA3
		private static string ConvertColorToHexString(Color c)
		{
			return string.Format("#{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
		}

		// Token: 0x0600B9FC RID: 47612 RVA: 0x002949D4 File Offset: 0x00292BD4
		private void PdfExportRenderPage(HtmlTextWriter nullWriter, Control page)
		{
			HtmlForm form = TableViewExporter.GetForm(this.tableView);
			form.SetRenderMethodDelegate(new RenderMethod(this.PdfExportRenderForm));
			HtmlTextWriter writer = new HtmlTextWriter(TextWriter.Null);
			form.RenderControl(writer);
		}

		// Token: 0x0600B9FD RID: 47613 RVA: 0x00294A14 File Offset: 0x00292C14
		private void CSVExportRenderPage(HtmlTextWriter nullWriter, Control page)
		{
			HtmlForm form = TableViewExporter.GetForm(this.tableView);
			form.SetRenderMethodDelegate(new RenderMethod(this.CSVExportRenderForm));
			HtmlTextWriter writer = new HtmlTextWriter(TextWriter.Null);
			form.RenderControl(writer);
		}

		// Token: 0x0600B9FE RID: 47614 RVA: 0x00294A54 File Offset: 0x00292C54
		private void ExcelExportRenderPage(HtmlTextWriter nullWriter, Control page)
		{
			HtmlForm form = TableViewExporter.GetForm(this.tableView);
			form.SetRenderMethodDelegate(new RenderMethod(this.ExcelExportRenderForm));
			HtmlTextWriter writer = new HtmlTextWriter(TextWriter.Null);
			form.RenderControl(writer);
		}

		// Token: 0x0600B9FF RID: 47615 RVA: 0x00294A94 File Offset: 0x00292C94
		private void WordExportRenderPage(HtmlTextWriter nullWriter, Control page)
		{
			HtmlForm form = TableViewExporter.GetForm(this.tableView);
			form.SetRenderMethodDelegate(new RenderMethod(this.WordExportRenderForm));
			HtmlTextWriter writer = new HtmlTextWriter(TextWriter.Null);
			form.RenderControl(writer);
		}

		// Token: 0x0600BA00 RID: 47616 RVA: 0x00294AD4 File Offset: 0x00292CD4
		private string GetXhtmlEntitiesDtd()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string result;
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Telerik.Web.UI.Grid.Resources.XhtmlEntities.dtd"))
			{
				using (TextReader textReader = new StreamReader(manifestResourceStream))
				{
					result = textReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x0600BA01 RID: 47617 RVA: 0x00294B34 File Offset: 0x00292D34
		private void PdfExportRenderForm(HtmlTextWriter nullWriter, Control form)
		{
			GridPdfSettings pdf = this.tableView.OwnerGrid.ExportSettings.Pdf;
			this.ClearHtmlWriter();
			Page page = this.GetPage();
			HttpResponse response = page.Response;
			string contentType = "application/pdf";
			string fileExtension = ".pdf";
			this.ConfigureResponse(contentType, fileExtension, true, response);
			this.tableView.GetGridTable().Exporting = true;
			this.tableView.GetGridTable().ShouldRenderColgroup = true;
			GridTable gridTable = this.tableView.GetGridTable();
			if (gridTable != null)
			{
				foreach (object obj in gridTable.Rows)
				{
					GridItem gridItem = (GridItem)obj;
					gridItem.PrepareItemVisibility();
				}
			}
			if (this.tableView.OwnerGrid.ExportSettings.HideStructureColumns)
			{
				this.HideStructureColumnCells(this.tableView);
			}
			this.tableView.RenderControl(this.htmlWriter);
			StringBuilder stringBuilder = new StringBuilder(this.htmlWriter.InnerWriter.ToString());
			GridPdfExportingArgs gridPdfExportingArgs = new GridPdfExportingArgs(stringBuilder.ToString());
			this.tableView.OwnerGrid.CallOnPdfExporting(gridPdfExportingArgs);
			stringBuilder = new StringBuilder(gridPdfExportingArgs.RawHTML);
			string arg = HttpUtility.HtmlEncode(this.tableView.OwnerGrid.ExportSettings.Pdf.PageTitle);
			string format = "<table style='width:100%'><tr><td style='text-align:{3}'>{0}</td><td style='text-align:{4}'>{1}</td><td style='text-align:{5}'>{2}</td></tr></table>";
			bool flag = (pdf.PageHeader != null && !pdf.PageHeader.IsEmpty) || (pdf.PageFooter != null && !pdf.PageFooter.IsEmpty);
			if (flag)
			{
				stringBuilder.Append(string.Format(format, new object[]
				{
					pdf.PageHeader.LeftCell.Text,
					pdf.PageHeader.MiddleCell.Text,
					pdf.PageHeader.RightCell.Text,
					pdf.PageHeader.LeftCell.TextAlign.ToString().ToLower(),
					pdf.PageHeader.MiddleCell.TextAlign.ToString().ToLower(),
					pdf.PageHeader.RightCell.TextAlign.ToString().ToLower()
				}));
				stringBuilder.Append(string.Format(format, new object[]
				{
					pdf.PageFooter.LeftCell.Text,
					pdf.PageFooter.MiddleCell.Text,
					pdf.PageFooter.RightCell.Text,
					pdf.PageFooter.LeftCell.TextAlign.ToString().ToLower(),
					pdf.PageFooter.MiddleCell.TextAlign.ToString().ToLower(),
					pdf.PageFooter.RightCell.TextAlign.ToString().ToLower()
				}));
			}
			XmlDocument xmlDocument = new XmlDocument();
			string text = stringBuilder.ToString();
			text = TableViewExporter.EscapeAmpersands(text);
			string xhtmlEntitiesDtd = this.GetXhtmlEntitiesDtd();
			text = string.Format("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n{0}\r\n    <html xmlns=\"http://www.w3.org/1999/xhtml\" \r\n      xmlns:fo=\"http://www.w3.org/1999/XSL/Format\">\r\n<head>\r\n    <title>{2}</title>\r\n</head>\r\n    <body>{1}</body>\r\n</html>", xhtmlEntitiesDtd, text, arg);
			try
			{
				xmlDocument.LoadXml(text);
			}
			catch (XmlException ex)
			{
				string[] array = Regex.Split(text, Environment.NewLine);
				string text2 = array[ex.LineNumber - 1];
				string format2 = "Invalid XHTML. RadGrid has to render correct XHTML in order to export to PDF.\r\nParse error:\r\n{0}\r\nat line:\r\n{1}";
				string message = string.Format(format2, ex.Message, text2.Trim());
				throw new GridPdfExportException(message);
			}
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform(false);
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Telerik.Web.UI.Grid.Resources.xhtml2fo.xsl"))
			{
				XmlDocument xmlDocument2 = new XmlDocument();
				xmlDocument2.Load(manifestResourceStream);
				xslCompiledTransform.Load(xmlDocument2);
			}
			StringWriter stringWriter = new StringWriter();
			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			this.AddXhtmlToXslFoTransformParameters(xsltArgumentList);
			xslCompiledTransform.Transform(xmlDocument, xsltArgumentList, stringWriter);
			string text3 = stringWriter.ToString().Replace("pxpx", "px");
			if (flag)
			{
				text3 = this.AppendHeaderFooterTablesToPage(text3, pdf);
			}
			StringReader inputReader = new StringReader(text3);
			ApocDriver apocDriver = ApocDriver.Make();
			apocDriver.BaseDirectory = new DirectoryInfo(TableViewExporter.GetTemporaryDir());
			PdfRendererOptions options = new PdfRendererOptions();
			this.ConfigurePdfOptions(options);
			apocDriver.Options = options;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				apocDriver.Render(inputReader, memoryStream);
				byte[] bytes = memoryStream.ToArray();
				Encoding encoding = Encoding.GetEncoding(1252);
				string @string = encoding.GetString(bytes);
				GridExportingArgs e = new GridExportingArgs(@string, ExportType.Pdf);
				this.tableView.OwnerGrid.CallOnGridExporting(e);
				response.BinaryWrite(encoding.GetBytes(@string));
			}
		}

		// Token: 0x0600BA02 RID: 47618 RVA: 0x0029503C File Offset: 0x0029323C
		private string AppendHeaderFooterTablesToPage(string fo, GridPdfSettings settings)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("fo", TableViewExporter.foNS);
			xmlDocument.LoadXml(fo);
			XmlNodeList xmlNodeList = xmlDocument.SelectNodes("//fo:static-content[@flow-name='page-header' or @flow-name='page-footer']", xmlNamespaceManager);
			XmlNodeList xmlNodeList2 = xmlDocument.SelectNodes("//fo:block[@role='html:body']/fo:table", xmlNamespaceManager);
			XmlNode xmlNode = xmlDocument.ImportNode(xmlNodeList2[xmlNodeList2.Count - 2], true);
			XmlNode xmlNode2 = xmlDocument.ImportNode(xmlNodeList2[xmlNodeList2.Count - 1], true);
			this.AppendColgroup(xmlDocument, xmlNode);
			this.AppendColgroup(xmlDocument, xmlNode2);
			xmlNodeList[0].AppendChild(xmlNode);
			xmlNodeList[1].AppendChild(xmlNode2);
			XmlNode parentNode = xmlNodeList2[0].ParentNode;
			parentNode.RemoveChild(xmlNodeList2[xmlNodeList2.Count - 2]);
			parentNode.RemoveChild(xmlNodeList2[xmlNodeList2.Count - 1]);
			return xmlDocument.OuterXml;
		}

		// Token: 0x0600BA03 RID: 47619 RVA: 0x0029512C File Offset: 0x0029332C
		private void AppendColgroup(XmlDocument doc, XmlNode table)
		{
			for (int i = 0; i < 3; i++)
			{
				XmlElement xmlElement = doc.CreateElement("fo:table-column", TableViewExporter.foNS);
				XmlAttribute xmlAttribute = doc.CreateAttribute("role");
				xmlAttribute.Value = "html:col";
				xmlElement.Attributes.Append(xmlAttribute);
				table.PrependChild(xmlElement);
			}
		}

		// Token: 0x0600BA04 RID: 47620 RVA: 0x00295184 File Offset: 0x00293384
		public static string GetTemporaryDir()
		{
			string text = Path.GetTempPath();
			if (string.IsNullOrEmpty(text))
			{
				foreach (string text2 in TableViewExporter.tempDirEnvVars)
				{
					text = Environment.GetEnvironmentVariable(text2);
					if (!string.IsNullOrEmpty(text2))
					{
						break;
					}
				}
				if (string.IsNullOrEmpty(text))
				{
					text = "/tmp";
				}
			}
			return text;
		}

		// Token: 0x0600BA05 RID: 47621 RVA: 0x002951D8 File Offset: 0x002933D8
		private void ConfigurePdfOptions(PdfRendererOptions options)
		{
			options.FontType = this.tableView.OwnerGrid.ExportSettings.Pdf.FontType;
			options.EnableAdd = this.tableView.OwnerGrid.ExportSettings.Pdf.AllowAdd;
			options.EnableCopy = this.tableView.OwnerGrid.ExportSettings.Pdf.AllowCopy;
			options.EnableModify = this.tableView.OwnerGrid.ExportSettings.Pdf.AllowModify;
			options.EnablePrinting = this.tableView.OwnerGrid.ExportSettings.Pdf.AllowPrinting;
			options.ForceTextWrap = this.tableView.OwnerGrid.ExportSettings.Pdf.ForceTextWrap;
			switch (this.tableView.OwnerGrid.ExportSettings.Pdf.ContentFilter)
			{
			case GridPdfFilter.NoFilter:
				options.Filter = PdfRendererOptions.PdfFilter.NoFilter;
				break;
			case GridPdfFilter.Ascii85:
				options.Filter = PdfRendererOptions.PdfFilter.Ascii85;
				break;
			case GridPdfFilter.AsciiHex:
				options.Filter = PdfRendererOptions.PdfFilter.AsciiHex;
				break;
			case GridPdfFilter.Flate:
				options.Filter = PdfRendererOptions.PdfFilter.Flate;
				break;
			default:
				options.Filter = PdfRendererOptions.PdfFilter.NoFilter;
				break;
			}
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.DisableContentEncryption)
			{
				options.DisableSecurity = true;
			}
			else if (!string.IsNullOrEmpty(this.tableView.OwnerGrid.ExportSettings.Pdf.UserPassword))
			{
				options.UserPassword = this.tableView.OwnerGrid.ExportSettings.Pdf.UserPassword;
			}
			if (!string.IsNullOrEmpty(this.tableView.OwnerGrid.ExportSettings.Pdf.Author))
			{
				options.Author = this.tableView.OwnerGrid.ExportSettings.Pdf.Author;
			}
			if (!string.IsNullOrEmpty(this.tableView.OwnerGrid.ExportSettings.Pdf.Title))
			{
				options.Title = this.tableView.OwnerGrid.ExportSettings.Pdf.Title;
			}
			if (!string.IsNullOrEmpty(this.tableView.OwnerGrid.ExportSettings.Pdf.Subject))
			{
				options.Subject = this.tableView.OwnerGrid.ExportSettings.Pdf.Subject;
			}
			if (!string.IsNullOrEmpty(this.tableView.OwnerGrid.ExportSettings.Pdf.Creator))
			{
				options.Creator = this.tableView.OwnerGrid.ExportSettings.Pdf.Creator;
			}
			if (!string.IsNullOrEmpty(this.tableView.OwnerGrid.ExportSettings.Pdf.Producer))
			{
				options.Producer = this.tableView.OwnerGrid.ExportSettings.Pdf.Producer;
			}
			if (!string.IsNullOrEmpty(this.tableView.OwnerGrid.ExportSettings.Pdf.DefaultFontFamily))
			{
				options.DefaultFontFamily = this.tableView.OwnerGrid.ExportSettings.Pdf.DefaultFontFamily;
			}
			foreach (string keyword in this.tableView.OwnerGrid.ExportSettings.Pdf.Keywords)
			{
				options.AddKeyword(keyword);
			}
		}

		// Token: 0x0600BA06 RID: 47622 RVA: 0x00295524 File Offset: 0x00293724
		private void AddXhtmlToXslFoTransformParameters(XsltArgumentList xslArg)
		{
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.PageWidth != Unit.Empty)
			{
				xslArg.AddParam("page-width", "", this.tableView.OwnerGrid.ExportSettings.Pdf.PageWidth.ToString());
			}
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.PageHeight != Unit.Empty)
			{
				xslArg.AddParam("page-height", "", this.tableView.OwnerGrid.ExportSettings.Pdf.PageHeight.ToString());
			}
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.PageTopMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-top", "", this.tableView.OwnerGrid.ExportSettings.Pdf.PageTopMargin.ToString());
			}
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.PageBottomMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-bottom", "", this.tableView.OwnerGrid.ExportSettings.Pdf.PageBottomMargin.ToString());
			}
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.PageLeftMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-left", "", this.tableView.OwnerGrid.ExportSettings.Pdf.PageLeftMargin.ToString());
			}
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.PageRightMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-right", "", this.tableView.OwnerGrid.ExportSettings.Pdf.PageRightMargin.ToString());
			}
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.PageHeaderMargin != Unit.Empty)
			{
				xslArg.AddParam("page-header-margin", "", this.tableView.OwnerGrid.ExportSettings.Pdf.PageHeaderMargin.ToString());
			}
			if (this.tableView.OwnerGrid.ExportSettings.Pdf.PageFooterMargin != Unit.Empty)
			{
				xslArg.AddParam("page-footer-margin", "", this.tableView.OwnerGrid.ExportSettings.Pdf.PageFooterMargin.ToString());
			}
		}

		// Token: 0x0600BA07 RID: 47623 RVA: 0x00295834 File Offset: 0x00293A34
		private void CSVExportRenderForm(HtmlTextWriter nullWriter, Control form)
		{
			this.ClearHtmlWriter();
			GridCsvSettings csv = this.tableView.OwnerGrid.ExportSettings.Csv;
			GridCsvEncoding encoding = csv.Encoding;
			byte[] array = null;
			Page page = this.GetPage();
			HttpResponse response = page.Response;
			Encoding encoding2;
			switch (encoding)
			{
			case GridCsvEncoding.Ascii:
				encoding2 = Encoding.ASCII;
				break;
			case GridCsvEncoding.Default:
				encoding2 = Encoding.Default;
				break;
			case GridCsvEncoding.Windows1252:
				encoding2 = Encoding.GetEncoding(1252);
				break;
			case GridCsvEncoding.Unicode:
				encoding2 = Encoding.Unicode;
				array = new byte[]
				{
					byte.MaxValue,
					254
				};
				break;
			case GridCsvEncoding.Utf7:
				encoding2 = Encoding.UTF7;
				array = new byte[]
				{
					43,
					47,
					118,
					56,
					45
				};
				break;
			case GridCsvEncoding.Utf32:
			{
				encoding2 = Encoding.UTF32;
				byte[] array2 = new byte[4];
				array2[0] = byte.MaxValue;
				array2[1] = 254;
				array = array2;
				break;
			}
			default:
				encoding2 = Encoding.UTF8;
				array = new byte[]
				{
					239,
					187,
					191
				};
				break;
			}
			string webName = encoding2.WebName;
			string csvContentType = this.GetCsvContentType(webName);
			string csvFileExtension = this.GetCsvFileExtension();
			this.ConfigureResponse(csvContentType, csvFileExtension, true, response);
			StringBuilder stringBuilder = new StringBuilder();
			if (this.tableView.ShowHeader)
			{
				this.RenderCsvHeaderContent(stringBuilder);
				stringBuilder.Append("\r\n");
			}
			this.RenderCsvItems(stringBuilder);
			GridExportingArgs gridExportingArgs = new GridExportingArgs(stringBuilder.ToString(), ExportType.Csv);
			this.tableView.OwnerGrid.CallOnGridExporting(gridExportingArgs);
			if (array != null && csv.EnableBomHeader)
			{
				response.BinaryWrite(array);
			}
			response.BinaryWrite(encoding2.GetBytes(gridExportingArgs.ExportOutput));
		}

		// Token: 0x0600BA08 RID: 47624 RVA: 0x002959E0 File Offset: 0x00293BE0
		private string GetCsvContentType(string charset)
		{
			string result;
			if (this.tableView.OwnerGrid.ExportSettings.Csv.ColumnDelimiter == GridCsvDelimiter.Comma && this.tableView.OwnerGrid.ExportSettings.Csv.RowDelimiter == GridCsvDelimiter.NewLine)
			{
				result = string.Format("text/csv; charset={0};", charset);
			}
			else
			{
				result = string.Format("text/xls; charset={0};", charset);
			}
			return result;
		}

		// Token: 0x0600BA09 RID: 47625 RVA: 0x00295A44 File Offset: 0x00293C44
		private string GetCsvFileExtension()
		{
			string result;
			if (this.tableView.OwnerGrid.ExportSettings.Csv.ColumnDelimiter == GridCsvDelimiter.Comma && this.tableView.OwnerGrid.ExportSettings.Csv.RowDelimiter == GridCsvDelimiter.NewLine)
			{
				if (this.tableView.OwnerGrid.ExportSettings.Csv.FileExtension.ToLower() != "csv")
				{
					result = string.Format(".{0}", this.tableView.OwnerGrid.ExportSettings.Csv.FileExtension);
				}
				else
				{
					result = ".csv";
				}
			}
			else if (this.tableView.OwnerGrid.ExportSettings.Csv.FileExtension.ToLower() != "csv")
			{
				result = string.Format(".{0}", this.tableView.OwnerGrid.ExportSettings.Csv.FileExtension);
			}
			else
			{
				result = ".csv";
			}
			return result;
		}

		// Token: 0x0600BA0A RID: 47626 RVA: 0x00295B70 File Offset: 0x00293D70
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void RenderCsvItems(StringBuilder sb)
		{
			string format = this.tableView.OwnerGrid.ExportSettings.Csv.EncloseDataWithQuotes ? "\"{0}\"" : "{0}";
			string format2 = this.tableView.OwnerGrid.ExportSettings.Csv.EncloseDataWithQuotes ? "\"{0}\"{1}" : "{0}{1}";
			string arg = GridCsvSettings.DelimiterAsString(this.tableView.OwnerGrid.ExportSettings.Csv.ColumnDelimiter);
			string value = GridCsvSettings.DelimiterAsString(this.tableView.OwnerGrid.ExportSettings.Csv.RowDelimiter);
			List<GridColumn> list = new List<GridColumn>(this.tableView.RenderColumns).FindAll((GridColumn column) => column.Visible && column.Display && !(column is GridExpandColumn) && !(column is GridRowIndicatorColumn) && !(column is GridGroupSplitterColumn));
			foreach (object obj in this.tableView.Items)
			{
				GridItem gridItem = (GridItem)obj;
				if (gridItem.Visible && gridItem.Display && gridItem is GridDataItem)
				{
					int num = 0;
					foreach (GridColumn gridColumn in list)
					{
						string text = string.Empty;
						if (!(gridColumn is GridTemplateColumn) || !this.tableView.OwnerGrid.ExportSettings.ExportOnlyData)
						{
							text = this.ExtractTextFromCellControls(((GridDataItem)gridItem)[gridColumn.UniqueName]);
						}
						if (++num < list.Count)
						{
							sb.AppendFormat(format2, text.Replace("\"", "\"\""), arg);
						}
						else
						{
							sb.AppendFormat(format, text.Replace("\"", "\"\""));
						}
					}
					sb.Append(value);
				}
			}
		}

		// Token: 0x0600BA0B RID: 47627 RVA: 0x00295DA8 File Offset: 0x00293FA8
		private string ExtractTextFromCellControls(TableCell cell)
		{
			string text = string.Empty;
			if (cell.Controls.Count == 0)
			{
				text = cell.Text;
			}
			else
			{
				foreach (object obj in cell.Controls)
				{
					Control control = (Control)obj;
					if (control.Visible)
					{
						if (control is ITextControl)
						{
							text += (control as ITextControl).Text.Trim();
						}
						else if (control is HyperLink)
						{
							text += (control as HyperLink).Text.Trim();
						}
						else if (control is IButtonControl)
						{
							text += (control as IButtonControl).Text.Trim();
						}
						else if (control is ICheckBoxControl)
						{
							text += (control as ICheckBoxControl).Checked.ToString();
						}
					}
				}
			}
			if (!(text == "&nbsp;"))
			{
				return text.Replace("&nbsp;", " ");
			}
			return string.Empty;
		}

		// Token: 0x0600BA0C RID: 47628 RVA: 0x00295ED8 File Offset: 0x002940D8
		private string GetTemplateColumnHeaderText(GridTemplateColumn column)
		{
			if (this.tableView.OwnerGrid.ExportSettings.ExportOnlyData)
			{
				return string.Empty;
			}
			GridHeaderItem gridHeaderItem = this.tableView.GetItems(new GridItemType[]
			{
				GridItemType.Header
			})[0] as GridHeaderItem;
			string text = string.Empty;
			if (column.HeaderTemplate == null)
			{
				text = gridHeaderItem[column.UniqueName].Text;
				if (string.IsNullOrEmpty(text))
				{
					text = this.ExtractTextFromCellControls(gridHeaderItem[column.UniqueName]);
				}
			}
			else
			{
				text = this.ExtractTextFromCellControls(gridHeaderItem[column.UniqueName]);
			}
			return text;
		}

		// Token: 0x0600BA0D RID: 47629 RVA: 0x00295FA4 File Offset: 0x002941A4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void RenderCsvHeaderContent(StringBuilder sb)
		{
			string format = this.tableView.OwnerGrid.ExportSettings.Csv.EncloseDataWithQuotes ? "\"{0}\"" : "{0}";
			string format2 = this.tableView.OwnerGrid.ExportSettings.Csv.EncloseDataWithQuotes ? "\"{0}\"{1}" : "{0}{1}";
			string arg = GridCsvSettings.DelimiterAsString(this.tableView.OwnerGrid.ExportSettings.Csv.ColumnDelimiter);
			List<GridColumn> list = new List<GridColumn>(this.tableView.RenderColumns).FindAll((GridColumn column) => column.Visible && column.Display && !(column is GridExpandColumn) && !(column is GridRowIndicatorColumn) && !(column is GridGroupSplitterColumn));
			int num = 0;
			foreach (GridColumn gridColumn in list)
			{
				string text = string.Empty;
				if (gridColumn is GridTemplateColumn)
				{
					text = this.GetTemplateColumnHeaderText(gridColumn as GridTemplateColumn);
				}
				else
				{
					text = gridColumn.HeaderText;
				}
				if (++num < list.Count)
				{
					sb.AppendFormat(format2, text.Replace("\"", "\"\""), arg);
				}
				else
				{
					sb.AppendFormat(format, text.Replace("\"", "\"\""));
				}
			}
		}

		// Token: 0x0600BA0E RID: 47630 RVA: 0x00296104 File Offset: 0x00294304
		private void ExcelExportRenderForm(HtmlTextWriter nullWriter, Control form)
		{
			this.ClearHtmlWriter();
			Page page = this.GetPage();
			HttpResponse response = page.Response;
			if (this.tableView.OwnerGrid.ExportSettings.Excel.Format == GridExcelExportFormat.Html)
			{
				this.tableView.GetGridTable().Exporting = true;
				this.tableView.GetGridTable().ShouldRenderColgroup = true;
				GridTable gridTable = this.tableView.GetGridTable();
				if (gridTable != null)
				{
					foreach (object obj in gridTable.Rows)
					{
						GridItem item = (GridItem)obj;
						this.PrepareTableItemsVisibility(item, true, true);
					}
				}
				this.ConfigureResponse("application/vnd.ms-excel", "." + this.tableView.OwnerGrid.ExportSettings.Excel.FileExtension.ToLower(), false, response);
				this.PrepareHTMLExportDocumentFormat(this.htmlWriter, new TFunc<string[]>(this.PrepareExcelDocument));
				if (this.tableView.OwnerGrid.ExportSettings.HideStructureColumns)
				{
					this.HideStructureColumnCells(this.tableView);
				}
				this.tableView.RenderControl(this.htmlWriter);
				this.htmlWriter.Write("</body>");
				this.htmlWriter.Write("</html>");
				GridExportingArgs gridExportingArgs = new GridExportingArgs(this.htmlWriter.InnerWriter.ToString(), ExportType.Excel);
				this.tableView.OwnerGrid.CallOnGridExporting(gridExportingArgs);
				response.Write(gridExportingArgs.ExportOutput);
				return;
			}
			if (this.tableView.OwnerGrid.ExportSettings.Excel.Format == GridExcelExportFormat.Biff)
			{
				this.ConfigureResponse("application/vnd.ms-excel", "." + this.tableView.OwnerGrid.ExportSettings.Excel.FileExtension.ToLower(), false, response);
				if (this.tableView.OwnerGrid.ExportSettings.HideStructureColumns)
				{
					this.HideStructureColumnCells(this.tableView);
				}
				GridInfrastructureExporter gridInfrastructureExporter = new GridInfrastructureExporter(this.tableView);
				ExportStructure exportStructure = gridInfrastructureExporter.GenerateStructure();
				XlsBiffRenderer xlsBiffRenderer = new XlsBiffRenderer(exportStructure);
				GridBiffExportingEventArgs e = new GridBiffExportingEventArgs(exportStructure);
				this.tableView.OwnerGrid.CallOnBiffExporting(e);
				this.tableView.OwnerGrid.CallOnInfrastructureExporting(new GridInfrastructureExportingEventArgs(exportStructure, ExportType.ExcelBiff));
				byte[] array = xlsBiffRenderer.Render();
				GridExportingArgs e2 = new GridExportingArgs(Encoding.GetEncoding(1252).GetString(array), ExportType.ExcelBiff);
				this.tableView.OwnerGrid.CallOnGridExporting(e2);
				response.BinaryWrite(array);
				return;
			}
			if (this.tableView.OwnerGrid.ExportSettings.Excel.Format == GridExcelExportFormat.Xlsx)
			{
				this.tableView.OwnerGrid.ExportSettings.Excel.FileExtension = "xlsx";
				this.ConfigureResponse("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "." + this.tableView.OwnerGrid.ExportSettings.Excel.FileExtension.ToLower(), false, response);
				if (this.tableView.OwnerGrid.ExportSettings.HideStructureColumns)
				{
					this.HideStructureColumnCells(this.tableView);
				}
				GridInfrastructureExporter gridInfrastructureExporter2 = new GridInfrastructureExporter(this.tableView);
				ExportStructure exportStructure2 = gridInfrastructureExporter2.GenerateStructure();
				XlsxRenderer xlsxRenderer = new XlsxRenderer(exportStructure2);
				this.tableView.OwnerGrid.CallOnInfrastructureExporting(new GridInfrastructureExportingEventArgs(exportStructure2, ExportType.ExcelXlsx));
				xlsxRenderer.AutoFitWidthMode = this.tableView.OwnerGrid.ExportSettings.Excel.AutoFitColumnWidth;
				byte[] array2 = xlsxRenderer.Render(null);
				GridExportingArgs e3 = new GridExportingArgs(Encoding.GetEncoding(1252).GetString(array2), ExportType.ExcelXlsx);
				this.tableView.OwnerGrid.CallOnGridExporting(e3);
				response.BinaryWrite(array2);
				return;
			}
			if (this.tableView.OwnerGrid.ExportSettings.Excel.Format == GridExcelExportFormat.ExcelML)
			{
				this.ConfigureResponse("application/vnd.ms-excel", "." + this.tableView.OwnerGrid.ExportSettings.Excel.FileExtension.ToLower(), false, response);
				if (!this.tableView.OwnerGrid.ExportSettings.ExportOnlyData)
				{
					throw new InvalidOperationException("ExcelML exports only data. Please, set ExportOnlyData property to 'true'.");
				}
				this.tableView.Visible = false;
				this.tableView.OwnerGrid.Visible = false;
				this.tableView.OwnerGrid.RebindForExport();
				this.tableView.OwnerGrid.IsExporting = true;
				GridEnumerableBase resolvedDataSource = this.tableView._resolvedDataSource;
				if (resolvedDataSource.GroupingDataSet != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					WorkBook workBook = new WorkBook();
					ExcelMLResponseBuilder excelMLResponseBuilder = new ExcelMLResponseBuilder(workBook, this.tableView, resolvedDataSource);
					excelMLResponseBuilder.AppendStyles();
					WorksheetElement worksheetElement = new WorksheetElement();
					string name = "Worksheet1";
					if (!string.IsNullOrEmpty(this.tableView.OwnerGrid.ExportSettings.Excel.WorksheetName))
					{
						name = this.tableView.OwnerGrid.ExportSettings.Excel.WorksheetName;
					}
					worksheetElement.Name = name;
					workBook.Worksheets.Add(worksheetElement);
					excelMLResponseBuilder.BuildExcelTable(0);
					workBook.Render(stringBuilder);
					GridExportingArgs gridExportingArgs2 = new GridExportingArgs(stringBuilder.ToString(), ExportType.ExcelML);
					this.tableView.OwnerGrid.CallOnGridExporting(gridExportingArgs2);
					response.Write(gridExportingArgs2.ExportOutput);
					this.tableView._resolvedDataSource = null;
					return;
				}
			}
		}

		// Token: 0x0600BA0F RID: 47631 RVA: 0x00296684 File Offset: 0x00294884
		private void ApplyCellFormating(GridItem item, bool isExcel)
		{
			GridDataItem gridDataItem = item as GridDataItem;
			if (gridDataItem != null)
			{
				GridColumn[] renderColumns = item.OwnerTableView.RenderColumns;
				foreach (GridColumn gridColumn in renderColumns)
				{
					TableCell tableCell = gridDataItem[gridColumn.UniqueName];
					if (isExcel)
					{
						if (gridColumn.DataTypeIsSet)
						{
							if (gridColumn.DataType == typeof(double) || gridColumn.DataType == typeof(decimal))
							{
								tableCell.Style["mso-number-format"] = "0\\.00";
							}
							else if (gridColumn.DataType == typeof(short) || gridColumn.DataType == typeof(ushort) || gridColumn.DataType == typeof(int) || gridColumn.DataType == typeof(uint) || gridColumn.DataType == typeof(long) || gridColumn.DataType == typeof(ulong) || gridColumn.DataType == typeof(float))
							{
								tableCell.Style["mso-number-format"] = "0";
							}
						}
						this.tableView.OwnerGrid.CallOnExcelExportCellFormatting(new ExcelExportCellFormattingEventArgs(gridColumn, tableCell));
					}
					this.tableView.OwnerGrid.CallOnExportCellFormatting(new ExportCellFormattingEventArgs(gridColumn, tableCell));
				}
			}
		}

		// Token: 0x0600BA10 RID: 47632 RVA: 0x0029681C File Offset: 0x00294A1C
		private void PrepareTableItemsVisibility(GridItem item, bool callCellFormatting, bool isExcel)
		{
			if (item.ItemType == GridItemType.NestedView)
			{
				foreach (GridTableView gridTableView in ((GridNestedViewItem)item).NestedTableViews)
				{
					GridTable gridTable = gridTableView.GetGridTable();
					if (gridTable != null)
					{
						gridTable.Exporting = true;
						gridTable.ShouldRenderColgroup = true;
						foreach (object obj in gridTable.Rows)
						{
							GridItem item2 = (GridItem)obj;
							this.PrepareTableItemsVisibility(item2, callCellFormatting, isExcel);
						}
					}
				}
			}
			if (callCellFormatting)
			{
				this.ApplyCellFormating(item, isExcel);
			}
			item.PrepareItemVisibility();
		}

		// Token: 0x0600BA11 RID: 47633 RVA: 0x002968D8 File Offset: 0x00294AD8
		private void WordExportRenderForm(HtmlTextWriter nullWriter, Control form)
		{
			this.ClearHtmlWriter();
			Page page = this.GetPage();
			HttpResponse response = page.Response;
			if (this.tableView.OwnerGrid.ExportSettings.Word.Format == GridWordExportFormat.Html)
			{
				this.tableView.GetGridTable().Exporting = true;
				this.tableView.GetGridTable().ShouldRenderColgroup = true;
				GridTable gridTable = this.tableView.GetGridTable();
				if (gridTable != null)
				{
					foreach (object obj in gridTable.Rows)
					{
						GridItem item = (GridItem)obj;
						this.PrepareTableItemsVisibility(item, true, false);
					}
				}
				string contentType = "application/vnd.ms-word";
				string fileExtension = ".doc";
				bool isMacOffice = page.Request.Browser.Platform == null || page.Request.Browser.Platform.ToLower().IndexOf("mac") == -1;
				this.ConfigureResponse(contentType, fileExtension, isMacOffice, response);
				this.PrepareHTMLExportDocumentFormat(this.htmlWriter, new TFunc<string[]>(this.PrepareWordDocument));
				if (this.tableView.OwnerGrid.ExportSettings.HideStructureColumns)
				{
					this.HideStructureColumnCells(this.tableView);
				}
				this.tableView.RenderControl(this.htmlWriter);
				this.htmlWriter.Write("</body>");
				this.htmlWriter.Write("</html>");
				GridExportingArgs gridExportingArgs = new GridExportingArgs(this.htmlWriter.InnerWriter.ToString(), ExportType.Word);
				this.tableView.OwnerGrid.CallOnGridExporting(gridExportingArgs);
				response.Write(gridExportingArgs.ExportOutput);
				return;
			}
			if (this.tableView.OwnerGrid.ExportSettings.Word.Format == GridWordExportFormat.Docx)
			{
				this.ConfigureResponse("application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx", false, response);
				GridInfrastructureExporter gridInfrastructureExporter = new GridInfrastructureExporter(this.tableView);
				ExportStructure exportStructure = gridInfrastructureExporter.GenerateStructure();
				DocxRenderer docxRenderer = new DocxRenderer(exportStructure);
				this.tableView.OwnerGrid.CallOnInfrastructureExporting(new GridInfrastructureExportingEventArgs(exportStructure, ExportType.WordDocx));
				byte[] array = docxRenderer.Render();
				GridExportingArgs e = new GridExportingArgs(Encoding.GetEncoding(1252).GetString(array), ExportType.WordDocx);
				this.tableView.OwnerGrid.CallOnGridExporting(e);
				response.BinaryWrite(array);
			}
		}

		// Token: 0x0600BA12 RID: 47634 RVA: 0x00296B3C File Offset: 0x00294D3C
		private void PrepareHTMLExportDocumentFormat(TextWriter output, TFunc<string[]> @params)
		{
			output.Write(string.Format(this.htmlDocumentFormat, @params()));
		}

		// Token: 0x0600BA13 RID: 47635 RVA: 0x00296B58 File Offset: 0x00294D58
		private string[] PrepareWordDocument()
		{
			GridHTMLExportingEventArgs gridHTMLExportingEventArgs = this.CallOnHTMLExporting();
			this.ValidateXmlOptions("w", "word", gridHTMLExportingEventArgs.XmlOptions);
			return new string[]
			{
				"w",
				"word",
				gridHTMLExportingEventArgs.Styles.ToString(),
				gridHTMLExportingEventArgs.XmlOptions
			};
		}

		// Token: 0x0600BA14 RID: 47636 RVA: 0x00296BB4 File Offset: 0x00294DB4
		private string[] PrepareExcelDocument()
		{
			GridHTMLExportingEventArgs gridHTMLExportingEventArgs = this.CallOnHTMLExporting();
			if (!string.IsNullOrEmpty(gridHTMLExportingEventArgs.XmlOptions))
			{
				this.ValidateXmlOptions("x", "excel", gridHTMLExportingEventArgs.XmlOptions);
			}
			return new string[]
			{
				"x",
				"excel",
				gridHTMLExportingEventArgs.Styles.ToString(),
				gridHTMLExportingEventArgs.XmlOptions
			};
		}

		// Token: 0x0600BA15 RID: 47637 RVA: 0x00296C1C File Offset: 0x00294E1C
		private void ValidateXmlOptions(string prefix, string docType, string xml)
		{
			string arg = string.Format("<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:{0}='urn:schemas-microsoft-com:office:{1}'>", prefix, docType);
			string arg2 = "</html>";
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(string.Format("{0}{1}{2}", arg, xml, arg2));
			}
			catch (Exception innerException)
			{
				throw new ArgumentException("Invalid XML format.", xml, innerException);
			}
		}

		// Token: 0x0600BA16 RID: 47638 RVA: 0x00296C78 File Offset: 0x00294E78
		private GridHTMLExportingEventArgs CallOnHTMLExporting()
		{
			GridHTMLExportingEventArgs gridHTMLExportingEventArgs = new GridHTMLExportingEventArgs();
			GridExportSettings exportSettings = this.tableView.OwnerGrid.ExportSettings;
			if (exportSettings.Excel.DefaultCellAlignment != HorizontalAlign.NotSet)
			{
				gridHTMLExportingEventArgs.Styles.AppendFormat("td {{ text-align: {0}; }}", exportSettings.Excel.DefaultCellAlignment.ToString().ToLower());
			}
			this.tableView.OwnerGrid.CallOnHTMLExporting(gridHTMLExportingEventArgs);
			return gridHTMLExportingEventArgs;
		}

		// Token: 0x0600BA17 RID: 47639 RVA: 0x00296CE8 File Offset: 0x00294EE8
		private Page GetPage()
		{
			Page page = this.tableView.Page ?? this.tableView.OwnerGrid.Page;
			if (page == null)
			{
				throw new InvalidOperationException("RadGrid must be databound before exporting.");
			}
			return page;
		}

		// Token: 0x0600BA18 RID: 47640 RVA: 0x00296D24 File Offset: 0x00294F24
		private void ConfigureResponse(string contentType, string fileExtension, bool isMacOffice, HttpResponse response)
		{
			response.Clear();
			response.Buffer = true;
			response.ContentType = contentType;
			response.ContentEncoding = Encoding.UTF8;
			response.Charset = "";
			if (GridTableViewHelper.IsBrowser("IE") || GridTableViewHelper.IsBrowser("InternetExplorer"))
			{
				this.fileName = HttpUtility.UrlEncode(this.fileName, Encoding.UTF8);
			}
			string text = this.fileName + fileExtension;
			text = text.Replace("\n", " ").Replace("\r", " ");
			if (!this.openInNewWindow)
			{
				response.AddHeader("Content-Disposition", "inline;filename=\"" + text + "\"");
				return;
			}
			response.AddHeader("Content-Disposition", "attachment;filename=\"" + text + "\"");
		}

		// Token: 0x0600BA19 RID: 47641 RVA: 0x00296DFC File Offset: 0x00294FFC
		private void PrepareForExport()
		{
			if (this.ignorePaging)
			{
				this.tableView.OwnerGrid.CurrentPageIndex = (this.tableView.CurrentPageIndex = 0);
			}
			this.PrepareForExportInternal(this.tableView, this.ignorePaging, this.dataOnly);
		}

		// Token: 0x0600BA1A RID: 47642 RVA: 0x00296E48 File Offset: 0x00295048
		internal void PrepareForExportInternal(GridTableView tableView, bool ignorePaging, bool dataOnly)
		{
			if (tableView.OwnerGrid.ExportSettings.HideStructureColumns)
			{
				tableView.OwnerGrid.ClientSettings.Resizing.ShowRowIndicatorColumn = false;
			}
			if (tableView.OwnerGrid.ExportSettings.HideNonDataBoundColumns)
			{
				this.HideColumnsRecursively(tableView);
			}
			if (this.exportFormat == ExportType.Word && tableView.HierarchyLoadMode == GridChildLoadMode.Client)
			{
				tableView.HierarchyLoadMode = GridChildLoadMode.ServerBind;
				if (!tableView.OwnerGrid.ExportSettings.IgnorePaging)
				{
					tableView.Rebind();
				}
			}
			if ((this.exportFormat == ExportType.ExcelBiff || this.exportFormat == ExportType.ExcelXlsx || this.exportFormat == ExportType.WordDocx) && tableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll)
			{
				tableView.OwnerGrid.ClientSettings.Scrolling.AllowScroll = false;
				tableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders = false;
				if (!tableView.OwnerGrid.ExportSettings.IgnorePaging)
				{
					tableView.Rebind();
				}
			}
			tableView.PrepareColumnsForExport();
			if (ignorePaging)
			{
				if (tableView.AllowCustomPaging || tableView.OwnerGrid.AllowCustomPaging)
				{
					this.tableView.OwnerGrid.PageSize = Math.Max(this.tableView.VirtualItemCount, 1);
				}
				tableView.OwnerGrid.AllowPaging = false;
				tableView.OwnerGrid.MasterTableView.AllowPaging = false;
				if (tableView != tableView.OwnerGrid.MasterTableView)
				{
					tableView.OwnerGrid.Page.UnregisterRequiresControlState(this.tableView);
					tableView.AllowPaging = false;
				}
				if (this.exportFormat != ExportType.ExcelML)
				{
					tableView.OwnerGrid.RebindForExport();
				}
			}
			if ((tableView.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings || (tableView.OwnerGrid.ExportSettings.HideStructureColumns && tableView.OwnerGrid.ClientSettings.Resizing.AllowRowResize)) && !ignorePaging && this.exportFormat != ExportType.ExcelML)
			{
				tableView.OwnerGrid.RebindForExport();
			}
			this.HideColumnsRecursively(tableView);
			tableView.PrepareExport();
			tableView.OwnerGrid.ClientSettings.Scrolling.UseStaticHeaders = false;
			if (dataOnly)
			{
				GridItem[] items = tableView.GetItems(new GridItemType[]
				{
					GridItemType.CommandItem,
					GridItemType.StatusBar
				});
				foreach (GridItem gridItem in items)
				{
					gridItem.Visible = false;
				}
				tableView.ClearTableViewControls(tableView, this, tableView);
			}
			if (tableView.OwnerGrid.ExportSettings.HideStructureColumns && this.exportFormat != ExportType.Word && this.exportFormat != ExportType.Excel && this.exportFormat != ExportType.ExcelML && this.exportFormat != ExportType.Pdf)
			{
				this.HideStructureColumnCells(tableView);
			}
		}

		// Token: 0x0600BA1B RID: 47643 RVA: 0x002970D4 File Offset: 0x002952D4
		private void HideColumnsRecursively(GridTableView tableView)
		{
			foreach (GridColumn gridColumn in tableView.RenderColumns)
			{
				if (!gridColumn.Exportable || (tableView.OwnerGrid.ExportSettings.HideNonDataBoundColumns && !(gridColumn is IGridDataColumn)))
				{
					gridColumn.Visible = false;
				}
			}
			foreach (GridNestedViewItem gridNestedViewItem in tableView.GetItems(new GridItemType[]
			{
				GridItemType.NestedView
			}))
			{
				if (gridNestedViewItem.NestedTableViews.Length > 0)
				{
					this.HideColumnsRecursively(gridNestedViewItem.NestedTableViews[0]);
				}
			}
		}

		// Token: 0x0600BA1C RID: 47644 RVA: 0x00297174 File Offset: 0x00295374
		private void HideStructureColumnCells(GridTableView tableView)
		{
			bool flag = false;
			foreach (object obj in tableView.GetGridTable().Rows)
			{
				GridItem gridItem = (GridItem)obj;
				if (gridItem.OwnerTableView == tableView.OwnerGrid.MasterTableView)
				{
					if (gridItem is GridMultiRowItem)
					{
						using (IEnumerator enumerator2 = gridItem.Controls.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								GridItem gridItem2 = (GridItem)obj2;
								if (gridItem2 is GridHeaderItem && !flag)
								{
									this.HideFirstColumnCell(gridItem2);
								}
								if (gridItem2 is GridHeaderItem && !flag)
								{
									flag = true;
								}
							}
							continue;
						}
					}
					this.HideFirstColumnCell(gridItem);
				}
			}
		}

		// Token: 0x0600BA1D RID: 47645 RVA: 0x00297260 File Offset: 0x00295460
		private void HideFirstColumnCell(GridItem item)
		{
			if (item.Cells.Count > 1)
			{
				item.Cells[0].Visible = false;
			}
		}

		// Token: 0x0600BA1E RID: 47646 RVA: 0x00297284 File Offset: 0x00295484
		private void ClearHtmlWriter()
		{
			StringWriter writer = new StringWriter();
			this.htmlWriter = new HtmlTextWriter(writer);
		}

		// Token: 0x0600BA1F RID: 47647 RVA: 0x002972A4 File Offset: 0x002954A4
		internal static HtmlForm GetForm(Control control)
		{
			HtmlForm htmlForm = TableViewExporter.SafeGetForm(control);
			if (htmlForm == null)
			{
				throw new Exception("Telerik RadGrid must be placed inside a <form> tag with runat='server'.");
			}
			return htmlForm;
		}

		// Token: 0x0600BA20 RID: 47648 RVA: 0x002972C8 File Offset: 0x002954C8
		private static HtmlForm SafeGetForm(Control control)
		{
			GridTableView gridTableView = control as GridTableView;
			HtmlForm form;
			if (gridTableView != null)
			{
				form = gridTableView.OwnerGrid.Page.Form;
			}
			else
			{
				form = control.Page.Form;
			}
			return form;
		}

		// Token: 0x0600BA21 RID: 47649 RVA: 0x00297301 File Offset: 0x00295501
		internal static string EscapeAmpersands(string input)
		{
			return Regex.Replace(input, "&(?![#0-9a-zA-Z]+;)", "&amp;");
		}

		// Token: 0x0400312B RID: 12587
		private string fileName;

		// Token: 0x0400312C RID: 12588
		private bool ignorePaging;

		// Token: 0x0400312D RID: 12589
		private bool dataOnly;

		// Token: 0x0400312E RID: 12590
		private bool openInNewWindow;

		// Token: 0x0400312F RID: 12591
		private HtmlTextWriter htmlWriter;

		// Token: 0x04003130 RID: 12592
		private ExportType exportFormat;

		// Token: 0x04003131 RID: 12593
		private GridTableView tableView;

		// Token: 0x04003132 RID: 12594
		private static GridItemType[] itemsToIterate = new GridItemType[]
		{
			GridItemType.AlternatingItem,
			GridItemType.CommandItem,
			GridItemType.DetailTemplateItem,
			GridItemType.Footer,
			GridItemType.GroupFooter,
			GridItemType.GroupHeader,
			GridItemType.Header,
			GridItemType.Item,
			GridItemType.SelectedItem
		};

		// Token: 0x04003133 RID: 12595
		private static readonly string foNS = "http://www.w3.org/1999/XSL/Format";

		// Token: 0x04003134 RID: 12596
		private static string[] tempDirEnvVars = new string[]
		{
			"Temp",
			"TMP",
			"TEMP"
		};

		// Token: 0x04003135 RID: 12597
		private readonly string htmlDocumentFormat = "<!DOCTYPE HTML PUBLIC \"-//IETF//DTD HTML//EN\">\n<html xmlns:o='urn:schemas-microsoft-com:office:office' xmlns:{0}='urn:schemas-microsoft-com:office:{1}' xmlns='http://www.w3.org/TR/REC-html40'>\n<head>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\n<style>\n{2}\n</style>\n<!--[if gte mso 9]>\n{3}\n<![endif]-->\n</head>\n<body>\n";
	}
}
