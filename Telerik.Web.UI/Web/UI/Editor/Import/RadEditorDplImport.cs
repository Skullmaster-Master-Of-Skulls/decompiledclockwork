using System;
using System.IO;
using System.Text;
using Telerik.Web.UI.Editor.Dpl;

namespace Telerik.Web.UI.Editor.Import
{
	// Token: 0x0200029E RID: 670
	public abstract class RadEditorDplImport
	{
		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060017B2 RID: 6066 RVA: 0x0004F324 File Offset: 0x0004D524
		// (remove) Token: 0x060017B3 RID: 6067 RVA: 0x0004F35C File Offset: 0x0004D55C
		public event RadEditorDplImport.ImportContentEventHandler ImportContent;

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060017B4 RID: 6068
		protected abstract string FormatProviderType { get; }

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060017B5 RID: 6069 RVA: 0x0004F391 File Offset: 0x0004D591
		// (set) Token: 0x060017B6 RID: 6070 RVA: 0x0004F399 File Offset: 0x0004D599
		public IDplImportSettings ImportSettings { get; set; }

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060017B7 RID: 6071 RVA: 0x0004F3A2 File Offset: 0x0004D5A2
		// (set) Token: 0x060017B8 RID: 6072 RVA: 0x0004F3AA File Offset: 0x0004D5AA
		public virtual IDplImportProxy DplImportProxy { get; set; }

		// Token: 0x060017B9 RID: 6073 RVA: 0x0004F3B3 File Offset: 0x0004D5B3
		public RadEditorDplImport()
		{
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0004F3BC File Offset: 0x0004D5BC
		internal virtual string Import(Stream stream)
		{
			object radFlowDocument = this.DplImportProxy.ConvertStreamToRadFlowDocument(stream, this.FormatProviderType);
			object htmlFormatProvider = this.DplImportProxy.CreateHtmlFormatProvider();
			this.DplImportProxy.ApplyImportSettings(htmlFormatProvider, this.ImportSettings);
			this.FireOnImportingEvent(radFlowDocument, htmlFormatProvider);
			return this.DplImportProxy.ConvertRadFlowDocumentToHtml(radFlowDocument, htmlFormatProvider);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0004F410 File Offset: 0x0004D610
		internal virtual string Import(string text)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(text);
			string result;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				result = this.Import(memoryStream);
			}
			return result;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0004F458 File Offset: 0x0004D658
		internal void FireOnImportingEvent(object radFlowDocument, object htmlFormatProvider)
		{
			EditorImportingArgs e = new EditorImportingArgs(radFlowDocument, htmlFormatProvider);
			this.ImportContent(this, e);
		}

		// Token: 0x0200029F RID: 671
		// (Invoke) Token: 0x060017BE RID: 6078
		public delegate void ImportContentEventHandler(object sender, EditorImportingArgs e);
	}
}
