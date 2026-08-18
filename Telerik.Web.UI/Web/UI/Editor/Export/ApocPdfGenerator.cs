using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using Telerik.Web.Apoc;
using Telerik.Web.Apoc.Render.Pdf;
using Telerik.Web.UI.Grid.Export;

namespace Telerik.Web.UI.Editor.Export
{
	// Token: 0x02000B47 RID: 2887
	public class ApocPdfGenerator : RadEditorExportTemplate
	{
		// Token: 0x06006CC4 RID: 27844 RVA: 0x00193C34 File Offset: 0x00191E34
		public ApocPdfGenerator(RadEditor radEditor) : base(radEditor)
		{
		}

		// Token: 0x06006CC5 RID: 27845 RVA: 0x00193C40 File Offset: 0x00191E40
		private bool HasFooterHeader()
		{
			GridPdfSettings pdf = this.editor.ExportSettings.Pdf;
			return (pdf.PageHeader != null && !pdf.PageHeader.IsEmpty) || (pdf.PageFooter != null && !pdf.PageFooter.IsEmpty);
		}

		// Token: 0x06006CC6 RID: 27846 RVA: 0x00193C90 File Offset: 0x00191E90
		protected internal override string GetHtmlContent()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.GetHtmlContent());
			GridPdfSettings pdf = this.editor.ExportSettings.Pdf;
			string format = "<table style='width:100%'><tr><td style='text-align:{3}'>{0}</td><td style='text-align:{4}'>{1}</td><td style='text-align:{5}'>{2}</td></tr></table>";
			if (this.HasFooterHeader())
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
			return stringBuilder.ToString();
		}

		// Token: 0x06006CC7 RID: 27847 RVA: 0x00193E48 File Offset: 0x00192048
		protected internal override string GenerateOutput()
		{
			StringReader inputReader = this.XsltTransformXmlDocument();
			string result = "";
			this._driver = ApocDriver.Make();
			this._driver.BaseDirectory = new DirectoryInfo(TableViewExporter.GetTemporaryDir());
			this._driver.Options = this.ExtractApocOptions(this.editor.ExportSettings);
			try
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					this._driver.Render(inputReader, memoryStream);
					byte[] bytes = memoryStream.ToArray();
					result = this.ResponseWriteEncoding.GetString(bytes);
				}
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06006CC8 RID: 27848 RVA: 0x00193EF4 File Offset: 0x001920F4
		private StringReader XsltTransformXmlDocument()
		{
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform(false);
			Assembly assembly = typeof(RadEditor).Assembly;
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream("Telerik.Web.UI.Grid.Resources.xhtml2fo.xsl"))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(manifestResourceStream);
				xslCompiledTransform.Load(xmlDocument);
			}
			StringWriter stringWriter = new StringWriter();
			XsltArgumentList arguments = this.ExtractXslTransformArguments(this.editor.ExportSettings);
			xslCompiledTransform.Transform(base.XmlContent, arguments, stringWriter);
			string text = stringWriter.ToString().Replace("pxpx", "px");
			if (this.HasFooterHeader())
			{
				text = this.AppendHeaderFooterTablesToPage(text, this.editor.ExportSettings.Pdf);
			}
			return new StringReader(text);
		}

		// Token: 0x06006CC9 RID: 27849 RVA: 0x00193FC4 File Offset: 0x001921C4
		private string AppendHeaderFooterTablesToPage(string fo, GridPdfSettings settings)
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("fo", ApocPdfGenerator.foNS);
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

		// Token: 0x06006CCA RID: 27850 RVA: 0x001940B4 File Offset: 0x001922B4
		private void AppendColgroup(XmlDocument doc, XmlNode table)
		{
			for (int i = 0; i < 3; i++)
			{
				XmlElement xmlElement = doc.CreateElement("fo:table-column", ApocPdfGenerator.foNS);
				XmlAttribute xmlAttribute = doc.CreateAttribute("role");
				xmlAttribute.Value = "html:col";
				xmlElement.Attributes.Append(xmlAttribute);
				table.PrependChild(xmlElement);
			}
		}

		// Token: 0x06006CCB RID: 27851 RVA: 0x0019410C File Offset: 0x0019230C
		private PdfRendererOptions ExtractApocOptions(EditorExportSettings exportSettings)
		{
			PdfRendererOptions pdfRendererOptions = new PdfRendererOptions();
			pdfRendererOptions.FontType = exportSettings.Pdf.FontType;
			pdfRendererOptions.EnableAdd = exportSettings.Pdf.AllowAdd;
			pdfRendererOptions.EnableCopy = exportSettings.Pdf.AllowCopy;
			pdfRendererOptions.EnableModify = exportSettings.Pdf.AllowModify;
			pdfRendererOptions.EnablePrinting = exportSettings.Pdf.AllowPrinting;
			switch (exportSettings.Pdf.ContentFilter)
			{
			case GridPdfFilter.NoFilter:
				pdfRendererOptions.Filter = PdfRendererOptions.PdfFilter.NoFilter;
				break;
			case GridPdfFilter.Ascii85:
				pdfRendererOptions.Filter = PdfRendererOptions.PdfFilter.Ascii85;
				break;
			case GridPdfFilter.AsciiHex:
				pdfRendererOptions.Filter = PdfRendererOptions.PdfFilter.AsciiHex;
				break;
			case GridPdfFilter.Flate:
				pdfRendererOptions.Filter = PdfRendererOptions.PdfFilter.Flate;
				break;
			default:
				pdfRendererOptions.Filter = PdfRendererOptions.PdfFilter.NoFilter;
				break;
			}
			if (exportSettings.Pdf.DisableContentEncryption)
			{
				pdfRendererOptions.DisableSecurity = true;
			}
			else if (!string.IsNullOrEmpty(exportSettings.Pdf.UserPassword))
			{
				pdfRendererOptions.UserPassword = exportSettings.Pdf.UserPassword;
			}
			if (!string.IsNullOrEmpty(exportSettings.Pdf.Author))
			{
				pdfRendererOptions.Author = exportSettings.Pdf.Author;
			}
			if (!string.IsNullOrEmpty(exportSettings.Pdf.Title))
			{
				pdfRendererOptions.Title = exportSettings.Pdf.Title;
			}
			if (!string.IsNullOrEmpty(exportSettings.Pdf.Subject))
			{
				pdfRendererOptions.Subject = exportSettings.Pdf.Subject;
			}
			if (!string.IsNullOrEmpty(exportSettings.Pdf.Creator))
			{
				pdfRendererOptions.Creator = exportSettings.Pdf.Creator;
			}
			if (!string.IsNullOrEmpty(exportSettings.Pdf.Producer))
			{
				pdfRendererOptions.Producer = exportSettings.Pdf.Producer;
			}
			if (!string.IsNullOrEmpty(exportSettings.Pdf.DefaultFontFamily))
			{
				pdfRendererOptions.DefaultFontFamily = exportSettings.Pdf.DefaultFontFamily;
			}
			foreach (string keyword in exportSettings.Pdf.Keywords)
			{
				pdfRendererOptions.AddKeyword(keyword);
			}
			return pdfRendererOptions;
		}

		// Token: 0x06006CCC RID: 27852 RVA: 0x001942FC File Offset: 0x001924FC
		private XsltArgumentList ExtractXslTransformArguments(EditorExportSettings exportSettings)
		{
			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			if (exportSettings.Pdf.PageWidth != Unit.Empty)
			{
				xsltArgumentList.AddParam("page-width", "", exportSettings.Pdf.PageWidth.ToString());
			}
			if (exportSettings.Pdf.PageHeight != Unit.Empty)
			{
				xsltArgumentList.AddParam("page-height", "", exportSettings.Pdf.PageHeight.ToString());
			}
			if (exportSettings.Pdf.PageTopMargin != Unit.Empty)
			{
				xsltArgumentList.AddParam("page-margin-top", "", exportSettings.Pdf.PageTopMargin.ToString());
			}
			if (exportSettings.Pdf.PageBottomMargin != Unit.Empty)
			{
				xsltArgumentList.AddParam("page-margin-bottom", "", exportSettings.Pdf.PageBottomMargin.ToString());
			}
			if (exportSettings.Pdf.PageLeftMargin != Unit.Empty)
			{
				xsltArgumentList.AddParam("page-margin-left", "", exportSettings.Pdf.PageLeftMargin.ToString());
			}
			if (exportSettings.Pdf.PageRightMargin != Unit.Empty)
			{
				xsltArgumentList.AddParam("page-margin-right", "", exportSettings.Pdf.PageRightMargin.ToString());
			}
			if (exportSettings.Pdf.PageHeaderMargin != Unit.Empty)
			{
				xsltArgumentList.AddParam("page-header-margin", "", exportSettings.Pdf.PageHeaderMargin.ToString());
			}
			if (exportSettings.Pdf.PageFooterMargin != Unit.Empty)
			{
				xsltArgumentList.AddParam("page-footer-margin", "", exportSettings.Pdf.PageFooterMargin.ToString());
			}
			return xsltArgumentList;
		}

		// Token: 0x06006CCD RID: 27853 RVA: 0x00194515 File Offset: 0x00192715
		protected override string ValidateContentForExport(string content)
		{
			content = this.CapitalizeFonts(content);
			content = this.RemoveLocalFilesUrls(content);
			content = base.ValidateContentForExport(content);
			return content;
		}

		// Token: 0x06006CCE RID: 27854 RVA: 0x00194534 File Offset: 0x00192734
		private string CapitalizeFonts(string content)
		{
			string pattern = "<[^>]*font-family\\s*:([^>\";']*)[;\"'][^>]*>";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			MatchEvaluator evaluator = new MatchEvaluator(this.CapitalizeFontsMatchEvaulator);
			content = regex.Replace(content, evaluator);
			return content;
		}

		// Token: 0x06006CCF RID: 27855 RVA: 0x00194568 File Offset: 0x00192768
		private string CapitalizeFontsMatchEvaulator(Match m)
		{
			string value = m.Groups[0].Value;
			string newValue = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(m.Groups[1].Value);
			return value.Replace(m.Groups[1].Value, newValue);
		}

		// Token: 0x06006CD0 RID: 27856 RVA: 0x001945C0 File Offset: 0x001927C0
		private string RemoveLocalFilesUrls(string content)
		{
			string pattern = "<[^>]*[\"';]([^>\"';]*\\s*:\\s*url\\s*\\(\\s*file://[^>\"']*\\)\\s*;?)[^>]*>";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			MatchEvaluator evaluator = new MatchEvaluator(this.RemoveLocalFilesUrlsMatchEvaluator);
			content = regex.Replace(content, evaluator);
			return content;
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x001945F4 File Offset: 0x001927F4
		private string RemoveLocalFilesUrlsMatchEvaluator(Match m)
		{
			string value = m.Groups[0].Value;
			return value.Replace(m.Groups[1].Value, "");
		}

		// Token: 0x170023B2 RID: 9138
		// (get) Token: 0x06006CD2 RID: 27858 RVA: 0x0019462F File Offset: 0x0019282F
		protected override string ContentType
		{
			get
			{
				return "application/pdf";
			}
		}

		// Token: 0x170023B3 RID: 9139
		// (get) Token: 0x06006CD3 RID: 27859 RVA: 0x00194636 File Offset: 0x00192836
		protected override string FileExtension
		{
			get
			{
				return ".pdf";
			}
		}

		// Token: 0x170023B4 RID: 9140
		// (get) Token: 0x06006CD4 RID: 27860 RVA: 0x0019463D File Offset: 0x0019283D
		protected override Encoding ResponseWriteEncoding
		{
			get
			{
				return Encoding.GetEncoding(1252);
			}
		}

		// Token: 0x170023B5 RID: 9141
		// (get) Token: 0x06006CD5 RID: 27861 RVA: 0x00194649 File Offset: 0x00192849
		protected override ExportType ExportType
		{
			get
			{
				return ExportType.Pdf;
			}
		}

		// Token: 0x170023B6 RID: 9142
		// (get) Token: 0x06006CD6 RID: 27862 RVA: 0x0019464C File Offset: 0x0019284C
		public ApocDriver Driver
		{
			get
			{
				return this._driver;
			}
		}

		// Token: 0x04001D46 RID: 7494
		private ApocDriver _driver;

		// Token: 0x04001D47 RID: 7495
		private static readonly string foNS = "http://www.w3.org/1999/XSL/Format";
	}
}
