using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Xml;

namespace Telerik.Web.UI.Editor.Export
{
	// Token: 0x020002A5 RID: 677
	public abstract class RadEditorExportTemplate
	{
		// Token: 0x060017EF RID: 6127 RVA: 0x0004F804 File Offset: 0x0004DA04
		public RadEditorExportTemplate(RadEditor radEditor)
		{
			this.editor = radEditor;
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0004F813 File Offset: 0x0004DA13
		// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0004F81B File Offset: 0x0004DA1B
		internal string ExportedOutput { get; set; }

		// Token: 0x060017F2 RID: 6130 RVA: 0x0004F824 File Offset: 0x0004DA24
		public virtual void Export()
		{
			this.InitializeXmlContent();
			this.ExportedOutput = this.GenerateOutput();
			this.FireEditorOnExportEvent(this.ExportedOutput);
			if (!this._args.Cancel)
			{
				this.ConfigureResponse();
				this.editor.Page.SetRenderMethodDelegate(new RenderMethod(this.ExportRenderPage));
			}
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0004F880 File Offset: 0x0004DA80
		private void ExportRenderPage(HtmlTextWriter nullWriter, Control page)
		{
			HtmlForm form = this.editor.Page.Form;
			form.SetRenderMethodDelegate(new RenderMethod(this.ExportRenderForm));
			form.RenderControl(new HtmlTextWriter(TextWriter.Null));
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0004F8C0 File Offset: 0x0004DAC0
		private void ExportRenderForm(HtmlTextWriter nullWriter, Control form)
		{
			this.WritePageResponse(this.ExportedOutput);
		}

		// Token: 0x060017F5 RID: 6133
		protected internal abstract string GenerateOutput();

		// Token: 0x060017F6 RID: 6134 RVA: 0x0004F8CE File Offset: 0x0004DACE
		protected internal virtual string GetHtmlContent()
		{
			return this.editor.Content;
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0004F8DC File Offset: 0x0004DADC
		protected internal virtual void InitializeXmlContent()
		{
			string text = this.GenerateXmlStirng(this.editor.ExportSettings.Pdf.PageTitle, this.GetHtmlContent());
			this.XmlContent = new XmlDocument();
			try
			{
				this.XmlContent.LoadXml(text);
			}
			catch (XmlException ex)
			{
				string[] array = Regex.Split(text, Environment.NewLine);
				string arg = (ex.LineNumber > 0 && ex.LineNumber <= array.Length) ? array[ex.LineNumber - 1].Trim() : string.Empty;
				string format = "Invalid XHTML. RadEditor content should be correct XHTML in order to be exported.\r\nParse error:\r\n{0}\r\nat line:\r\n{1}";
				string message = string.Format(format, ex.Message, arg);
				throw new XmlException(message);
			}
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0004F98C File Offset: 0x0004DB8C
		protected internal virtual string GenerateXmlStirng(string pageTitle, string content)
		{
			string xhtmlEntitiesDtd = this.GetXhtmlEntitiesDtd();
			content = this.ValidateContentForExport(content);
			return string.Format(this.XmlTemplate, xhtmlEntitiesDtd, pageTitle, content);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
		private string GetXhtmlEntitiesDtd()
		{
			Assembly assembly = typeof(RadEditor).Assembly;
			string result;
			using (Stream manifestResourceStream = assembly.GetManifestResourceStream("Telerik.Web.UI.Grid.Resources.XhtmlEntities.dtd"))
			{
				using (TextReader textReader = new StreamReader(manifestResourceStream))
				{
					result = textReader.ReadToEnd();
				}
			}
			return result;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0004FA24 File Offset: 0x0004DC24
		protected virtual string ValidateContentForExport(string content)
		{
			content = this.EscapeAmpersands(content);
			return content;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0004FA30 File Offset: 0x0004DC30
		private string EscapeAmpersands(string content)
		{
			return Regex.Replace(content, "&(?![#0-9a-zA-Z]+;)", "&amp;");
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0004FA42 File Offset: 0x0004DC42
		protected virtual void FireEditorOnExportEvent(string output)
		{
			this._args = new EditorExportingArgs(output, this.ExportType);
			this.editor.OnExportContent(this._args);
			this.ExportedOutput = this._args.ExportOutput;
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0004FA78 File Offset: 0x0004DC78
		protected virtual void WritePageResponse(string output)
		{
			this.editor.Page.Response.BinaryWrite(this.ResponseWriteEncoding.GetBytes(output));
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0004FA9C File Offset: 0x0004DC9C
		protected virtual void ConfigureResponse()
		{
			HttpResponse response = this.editor.Page.Response;
			response.Clear();
			response.Buffer = true;
			response.ContentType = this.ContentType;
			response.ContentEncoding = Encoding.UTF8;
			response.Charset = "";
			string text = this.editor.ExportSettings.FileName + this.FileExtension;
			text = text.Replace("\n", " ").Replace("\r", " ");
			if (!this.editor.ExportSettings.OpenInNewWindow)
			{
				response.AddHeader("Content-Disposition", "inline;filename=\"" + text + "\"");
				return;
			}
			response.AddHeader("Content-Disposition", "attachment;filename=\"" + text + "\"");
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x0004FB6E File Offset: 0x0004DD6E
		// (set) Token: 0x06001800 RID: 6144 RVA: 0x0004FB76 File Offset: 0x0004DD76
		protected internal XmlDocument XmlContent { get; set; }

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06001801 RID: 6145
		protected abstract string ContentType { get; }

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06001802 RID: 6146
		protected abstract string FileExtension { get; }

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06001803 RID: 6147
		protected abstract ExportType ExportType { get; }

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x0004FB7F File Offset: 0x0004DD7F
		protected virtual Encoding ResponseWriteEncoding
		{
			get
			{
				return Encoding.Default;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x0004FB86 File Offset: 0x0004DD86
		protected virtual string XmlTemplate
		{
			get
			{
				return "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n{0}\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\" xmlns:fo=\"http://www.w3.org/1999/XSL/Format\">\r\n\t<head>\r\n\t\t<title>{1}</title>\r\n\t</head>\r\n\t<body>{2}</body>\r\n</html>";
			}
		}

		// Token: 0x04000667 RID: 1639
		private EditorExportingArgs _args;

		// Token: 0x04000668 RID: 1640
		protected RadEditor editor;
	}
}
