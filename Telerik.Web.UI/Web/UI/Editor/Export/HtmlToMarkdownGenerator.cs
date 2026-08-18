using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Xsl;

namespace Telerik.Web.UI.Editor.Export
{
	// Token: 0x02000B46 RID: 2886
	internal class HtmlToMarkdownGenerator : RadEditorExportTemplate
	{
		// Token: 0x06006CBD RID: 27837 RVA: 0x00193A24 File Offset: 0x00191C24
		public HtmlToMarkdownGenerator(RadEditor radEditor) : base(radEditor)
		{
		}

		// Token: 0x06006CBE RID: 27838 RVA: 0x00193A30 File Offset: 0x00191C30
		protected internal override string GenerateOutput()
		{
			string result = "";
			XslCompiledTransform xslCompiledTransform = new XslCompiledTransform(false);
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("Telerik.Web.UI.Editor.Export.Markdown.Markdown.xsl"))
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.PreserveWhitespace = true;
				xmlDocument.CreateEntityReference("nbsp");
				xmlDocument.Load(manifestResourceStream);
				XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
				xmlNamespaceManager.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");
				XmlNode xmlNode = xmlDocument.SelectSingleNode("/xsl:apply-templates/xsl:param[@name='mode']", xmlNamespaceManager);
				if (xmlNode != null)
				{
					xmlNode.Attributes["select"].Value = "markdown";
				}
				xslCompiledTransform.Load(xmlDocument, new XsltSettings(false, true)
				{
					EnableDocumentFunction = true,
					EnableScript = true
				}, new XmlUrlResolver());
			}
			try
			{
				StringWriter stringWriter = new StringWriter();
				XsltArgumentList arguments = this.ExtractXslTarasformArguments(this.editor.ExportSettings);
				xslCompiledTransform.Transform(base.XmlContent, arguments, stringWriter);
				result = stringWriter.ToString();
			}
			catch (Exception)
			{
			}
			return result;
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x00193B58 File Offset: 0x00191D58
		private XsltArgumentList ExtractXslTarasformArguments(EditorExportSettings exportSettings)
		{
			XsltArgumentList xsltArgumentList = new XsltArgumentList();
			if (exportSettings.Markdown.HeaderStyle == EditorMarkdownHeaderStyle.atx)
			{
				xsltArgumentList.AddParam("h-style", "", "atx");
			}
			if (exportSettings.Markdown.AnchorStyle == EditorMarkdownElemetStyle.html)
			{
				xsltArgumentList.AddParam("a-style", "", "html");
			}
			if (exportSettings.Markdown.ImgStyle == EditorMarkdownElemetStyle.html)
			{
				xsltArgumentList.AddParam("img-style", "", "html");
			}
			if (exportSettings.Markdown.TableStyle == EditorMarkdownTableStyle.html)
			{
				xsltArgumentList.AddParam("table-style", "", "html");
			}
			if (exportSettings.Markdown.UnparseablesStyle == EditorMarkdownUnparseablesStyle.html)
			{
				xsltArgumentList.AddParam("unparseables", "", "html");
			}
			return xsltArgumentList;
		}

		// Token: 0x170023AE RID: 9134
		// (get) Token: 0x06006CC0 RID: 27840 RVA: 0x00193C1B File Offset: 0x00191E1B
		protected override string ContentType
		{
			get
			{
				return "text/plain";
			}
		}

		// Token: 0x170023AF RID: 9135
		// (get) Token: 0x06006CC1 RID: 27841 RVA: 0x00193C22 File Offset: 0x00191E22
		protected override string FileExtension
		{
			get
			{
				return ".txt";
			}
		}

		// Token: 0x170023B0 RID: 9136
		// (get) Token: 0x06006CC2 RID: 27842 RVA: 0x00193C29 File Offset: 0x00191E29
		protected override string XmlTemplate
		{
			get
			{
				return "<?xml version=\"1.0\" encoding=\"utf-8\"?>{0}<body>{2}</body>";
			}
		}

		// Token: 0x170023B1 RID: 9137
		// (get) Token: 0x06006CC3 RID: 27843 RVA: 0x00193C30 File Offset: 0x00191E30
		protected override ExportType ExportType
		{
			get
			{
				return ExportType.Markdown;
			}
		}
	}
}
