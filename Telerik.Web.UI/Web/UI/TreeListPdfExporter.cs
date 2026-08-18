using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using Telerik.Web.Apoc;
using Telerik.Web.Apoc.Render.Pdf;

namespace Telerik.Web.UI
{
	// Token: 0x02001226 RID: 4646
	internal class TreeListPdfExporter : TreeListExporter
	{
		// Token: 0x0600BFB9 RID: 49081 RVA: 0x002A8252 File Offset: 0x002A6452
		internal TreeListPdfExporter(RadTreeList treeList) : base(treeList)
		{
			this.treeList = this.treeList;
		}

		// Token: 0x0600BFBA RID: 49082 RVA: 0x002A8270 File Offset: 0x002A6470
		internal void ExportToPdf()
		{
			this.treeList.CurrentExportFormat = new ExportFormat?(ExportFormat.Pdf);
			this.page = base.GetPage(this.treeList);
			this.page.SetRenderMethodDelegate(new RenderMethod(this.ExportRenderPage));
			this.page.PreRender += delegate(object sender, EventArgs args)
			{
				this.PrepareForExport();
			};
		}

		// Token: 0x0600BFBB RID: 49083 RVA: 0x002A82D0 File Offset: 0x002A64D0
		protected override void PrepareForExport()
		{
			bool flag = false;
			this.treeList.GetTreeListTable().BorderColor = this.treeList.BorderColor;
			this.treeList.GetTreeListTable().BorderStyle = this.treeList.BorderStyle;
			this.treeList.GetTreeListTable().BorderWidth = this.treeList.BorderWidth;
			if (this.treeList.HasStaticHeaders)
			{
				this.treeList.ClientSettings.Scrolling.AllowScroll = false;
				this.treeList.ClientSettings.Scrolling.UseStaticHeaders = false;
				flag = true;
			}
			if (this.treeList.ExportSettings.IgnorePaging)
			{
				this.treeList.AllowPaging = false;
				flag = true;
			}
			if (flag)
			{
				this.treeList.Rebind();
			}
			this.treeList.GetTreeListTable().Width = ((this.treeList.Width == Unit.Empty) ? Unit.Percentage(100.0) : this.treeList.Width);
			this.treeList.Width = Unit.Empty;
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
			foreach (TreeListDataItem treeListDataItem in this.treeList.Items)
			{
				Control control = treeListDataItem.FindControl("ExpandCollapseButton");
				if (control != null && !this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.IsDefault)
				{
					TableCell tableCell = control.Parent as TableCell;
					tableCell.MergeStyle(this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle);
				}
			}
		}

		// Token: 0x0600BFBC RID: 49084 RVA: 0x002A84E4 File Offset: 0x002A66E4
		protected override void ExportRenderPage(HtmlTextWriter writer, Control pageCtrl)
		{
			base.ExportRenderPage(writer, pageCtrl);
		}

		// Token: 0x0600BFBD RID: 49085 RVA: 0x002A84F0 File Offset: 0x002A66F0
		protected override void ExportRenderForm(HtmlTextWriter writer, Control pageCtrl)
		{
			HttpResponse response = this.page.Response;
			StringWriter writer2 = new StringWriter();
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(writer2);
			this.treeList.RenderControl(htmlTextWriter);
			string text = htmlTextWriter.InnerWriter.ToString();
			TreeListPdfExportingEventArgs treeListPdfExportingEventArgs = new TreeListPdfExportingEventArgs(text);
			this.treeList.CallOnPdfExporting(treeListPdfExportingEventArgs);
			text = treeListPdfExportingEventArgs.RawHtml;
			this.ConfigureResponse(ExportFormat.Pdf, response);
			XmlDocument xmlDocument = new XmlDocument();
			string text2 = Regex.Replace(text, "&(?![#0-9a-zA-Z]+;)", "&amp;");
			string embeddedResource = TreeListExporter.GetEmbeddedResource("Telerik.Web.UI.Grid.Resources.XhtmlEntities.dtd");
			string embeddedResource2 = TreeListExporter.GetEmbeddedResource("Telerik.Web.UI.Grid.Resources.PageTemplate.xml");
			string arg = "";
			text2 = string.Format(embeddedResource2, embeddedResource, text2, arg);
			try
			{
				xmlDocument.LoadXml(text2);
			}
			catch (XmlException ex)
			{
				string[] array = Regex.Split(text2, Environment.NewLine);
				string arg2 = array[ex.LineNumber - 1].Trim();
				string format = "XHTML validation failed! Parse error {0} at line: {1}";
				throw new TreeListPdfExportException(string.Format(format, ex.Message, arg2));
			}
			this.ApplyExpandCollapseStyle(xmlDocument);
			this.RemoveDivWidthHeight(xmlDocument);
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform(false);
			XmlDocument xmlDocument2 = new XmlDocument();
			xmlDocument2.LoadXml(TreeListExporter.GetEmbeddedResource("Telerik.Web.UI.Grid.Resources.xhtml2fo.xsl"));
			xslCompiledTransform.Load(xmlDocument2);
			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			StringWriter stringWriter = new StringWriter();
			this.AddXhtmlToXslFoTransformParameters(xsltArgumentList);
			xslCompiledTransform.Transform(xmlDocument, xsltArgumentList, stringWriter);
			string s = stringWriter.ToString().Replace("pxpx", "px");
			StringReader inputReader = new StringReader(s);
			ApocDriver apocDriver = ApocDriver.Make();
			apocDriver.BaseDirectory = new DirectoryInfo(TreeListExporter.GetTemporaryDir());
			PdfRendererOptions options = new PdfRendererOptions();
			this.ConfigurePdfOptions(options);
			apocDriver.Options = options;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				apocDriver.Render(inputReader, memoryStream);
				byte[] array2 = memoryStream.ToArray();
				TreeListExportingEventArgs args = new TreeListExportingEventArgs(array2, ExportFormat.Pdf);
				this.treeList.CallOnExporting(args);
				response.BinaryWrite(array2);
			}
		}

		// Token: 0x0600BFBE RID: 49086 RVA: 0x002A86F8 File Offset: 0x002A68F8
		private void ApplyExpandCollapseStyle(XmlDocument xmlDocument)
		{
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("input");
			foreach (object obj in elementsByTagName)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode namedItem = xmlNode.Attributes.GetNamedItem("class");
				if (namedItem != null)
				{
					string value = namedItem.Value;
					if (value.Contains("rtlCollapse"))
					{
						if (string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.CollapseImageUrl))
						{
							XmlNode newChild = xmlDocument.CreateTextNode(this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.CollapseText);
							xmlNode.ParentNode.AppendChild(newChild);
						}
						else
						{
							XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("src");
							XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("alt");
							XmlAttribute xmlAttribute3 = xmlDocument.CreateAttribute("style");
							Unit collapseImageWidth = this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.CollapseImageWidth;
							Unit collapseImageHeight = this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.CollapseImageHeight;
							xmlAttribute.Value = this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.CollapseImageUrl;
							xmlAttribute2.Value = "Collapse";
							if (!collapseImageWidth.IsEmpty)
							{
								XmlAttribute xmlAttribute4 = xmlAttribute3;
								xmlAttribute4.Value += string.Format("width: {0};", collapseImageWidth.ToString());
							}
							if (!collapseImageHeight.IsEmpty)
							{
								XmlAttribute xmlAttribute5 = xmlAttribute3;
								xmlAttribute5.Value += string.Format("height: {0};", collapseImageHeight.ToString());
							}
							XmlElement xmlElement = xmlDocument.CreateElement("img", xmlNode.ParentNode.NamespaceURI);
							xmlElement.Attributes.Append(xmlAttribute2);
							xmlElement.Attributes.Append(xmlAttribute);
							xmlElement.Attributes.Append(xmlAttribute3);
							xmlNode.ParentNode.AppendChild(xmlElement);
						}
					}
					else if (value.Contains("rtlExpand"))
					{
						if (string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.ExpandImageUrl))
						{
							XmlNode newChild = xmlDocument.CreateTextNode(this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.ExpandText);
							xmlNode.ParentNode.AppendChild(newChild);
						}
						else
						{
							XmlAttribute xmlAttribute6 = xmlDocument.CreateAttribute("src");
							XmlAttribute xmlAttribute7 = xmlDocument.CreateAttribute("alt");
							XmlAttribute xmlAttribute8 = xmlDocument.CreateAttribute("style");
							Unit expandImageWidth = this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.ExpandImageWidth;
							Unit expandImageHeight = this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.ExpandImageHeight;
							xmlAttribute6.Value = this.treeList.ExportSettings.Pdf.ExpandCollapseCellStyle.ExpandImageUrl;
							xmlAttribute7.Value = "Expand";
							if (!expandImageWidth.IsEmpty)
							{
								XmlAttribute xmlAttribute9 = xmlAttribute8;
								xmlAttribute9.Value += string.Format("width: {0};", expandImageWidth.ToString());
							}
							if (!expandImageHeight.IsEmpty)
							{
								XmlAttribute xmlAttribute10 = xmlAttribute8;
								xmlAttribute10.Value += string.Format("height: {0};", expandImageHeight.ToString());
							}
							XmlElement xmlElement = xmlDocument.CreateElement("img", xmlNode.ParentNode.NamespaceURI);
							xmlElement.Attributes.Append(xmlAttribute7);
							xmlElement.Attributes.Append(xmlAttribute6);
							xmlElement.Attributes.Append(xmlAttribute8);
							xmlNode.ParentNode.AppendChild(xmlElement);
						}
					}
				}
			}
		}

		// Token: 0x0600BFBF RID: 49087 RVA: 0x002A8AE0 File Offset: 0x002A6CE0
		private void RemoveDivWidthHeight(XmlDocument xmlDocument)
		{
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("div");
			foreach (object obj in elementsByTagName)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNode namedItem = xmlNode.Attributes.GetNamedItem("style");
				if (namedItem != null)
				{
					string text = namedItem.Value;
					if (text.Contains("width") || text.Contains("height"))
					{
						string pattern = "(width|height):\\s*0*[1-9][0-9]*(px|pt|mm|cm|pc|in|\\%){1};?";
						string value = Regex.Match(text, pattern).Value;
						if (value != null)
						{
							text = Regex.Replace(text, pattern, string.Empty);
							if (string.IsNullOrEmpty(text))
							{
								xmlNode.Attributes.Remove((XmlAttribute)namedItem);
							}
							else
							{
								namedItem.Value = text;
							}
						}
					}
				}
			}
		}

		// Token: 0x0600BFC0 RID: 49088 RVA: 0x002A8BC8 File Offset: 0x002A6DC8
		private void AddXhtmlToXslFoTransformParameters(XsltArgumentList xslArg)
		{
			Unit unit = this.treeList.ExportSettings.Pdf.PageWidth;
			Unit unit2 = this.treeList.ExportSettings.Pdf.PageHeight;
			if (this.treeList.ExportSettings.Pdf.RotatePaper)
			{
				Unit unit3 = unit;
				unit = unit2;
				unit2 = unit3;
			}
			if (unit != Unit.Empty)
			{
				xslArg.AddParam("page-width", "", unit.ToString());
			}
			if (unit2 != Unit.Empty)
			{
				xslArg.AddParam("page-height", "", unit2.ToString());
			}
			if (this.treeList.ExportSettings.Pdf.PageTopMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-top", "", this.treeList.ExportSettings.Pdf.PageTopMargin.ToString());
			}
			if (this.treeList.ExportSettings.Pdf.PageBottomMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-bottom", "", this.treeList.ExportSettings.Pdf.PageBottomMargin.ToString());
			}
			if (this.treeList.ExportSettings.Pdf.PageLeftMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-left", "", this.treeList.ExportSettings.Pdf.PageLeftMargin.ToString());
			}
			if (this.treeList.ExportSettings.Pdf.PageRightMargin != Unit.Empty)
			{
				xslArg.AddParam("page-margin-right", "", this.treeList.ExportSettings.Pdf.PageRightMargin.ToString());
			}
			if (this.treeList.ExportSettings.Pdf.PageHeaderMargin != Unit.Empty)
			{
				xslArg.AddParam("page-header-margin", "", this.treeList.ExportSettings.Pdf.PageHeaderMargin.ToString());
			}
			if (this.treeList.ExportSettings.Pdf.PageFooterMargin != Unit.Empty)
			{
				xslArg.AddParam("page-footer-margin", "", this.treeList.ExportSettings.Pdf.PageFooterMargin.ToString());
			}
		}

		// Token: 0x0600BFC1 RID: 49089 RVA: 0x002A8E70 File Offset: 0x002A7070
		private void ConfigurePdfOptions(PdfRendererOptions options)
		{
			options.FontType = this.treeList.ExportSettings.Pdf.FontType;
			options.EnableAdd = this.treeList.ExportSettings.Pdf.AllowAdd;
			options.EnableCopy = this.treeList.ExportSettings.Pdf.AllowCopy;
			options.EnableModify = this.treeList.ExportSettings.Pdf.AllowModify;
			options.EnablePrinting = this.treeList.ExportSettings.Pdf.AllowPrinting;
			if (!string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.UserPassword))
			{
				options.UserPassword = this.treeList.ExportSettings.Pdf.UserPassword;
			}
			if (!string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.Author))
			{
				options.Author = this.treeList.ExportSettings.Pdf.Author;
			}
			if (!string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.Title))
			{
				options.Title = this.treeList.ExportSettings.Pdf.Title;
			}
			if (!string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.Subject))
			{
				options.Subject = this.treeList.ExportSettings.Pdf.Subject;
			}
			if (!string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.Creator))
			{
				options.Creator = this.treeList.ExportSettings.Pdf.Creator;
			}
			if (!string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.Producer))
			{
				options.Producer = this.treeList.ExportSettings.Pdf.Producer;
			}
			if (!string.IsNullOrEmpty(this.treeList.ExportSettings.Pdf.DefaultFontFamily))
			{
				options.DefaultFontFamily = this.treeList.ExportSettings.Pdf.DefaultFontFamily;
			}
			foreach (string keyword in this.treeList.ExportSettings.Pdf.Keywords)
			{
				options.AddKeyword(keyword);
			}
		}

		// Token: 0x0600BFC2 RID: 49090 RVA: 0x002A90B4 File Offset: 0x002A72B4
		protected override void ConfigureResponse(ExportFormat exportFormat, HttpResponse response)
		{
			string contentType = string.Empty;
			string arg = string.Empty;
			if (exportFormat == ExportFormat.Pdf)
			{
				contentType = "application/pdf";
				arg = ".pdf";
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
