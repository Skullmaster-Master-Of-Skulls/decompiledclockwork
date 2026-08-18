using System;
using Telerik.Web.UI.Editor.Dpl;

namespace Telerik.Web.UI.Editor.Export
{
	// Token: 0x020002A6 RID: 678
	internal class HtmlToDocxGenerator : RadEditorExportTemplate
	{
		// Token: 0x06001806 RID: 6150 RVA: 0x0004FB8D File Offset: 0x0004DD8D
		public HtmlToDocxGenerator(RadEditor radEditor) : base(radEditor)
		{
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0004FB96 File Offset: 0x0004DD96
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x0004FB9E File Offset: 0x0004DD9E
		public IDplExportProxy DplExportProxy { get; set; }

		// Token: 0x06001809 RID: 6153 RVA: 0x0004FBA8 File Offset: 0x0004DDA8
		protected internal override string GenerateOutput()
		{
			object radFlowDocument = this.DplExportProxy.ConvertHtmlToRadFlowDocument(this.GetHtmlContent());
			this.ApplyDocxExportSettings(radFlowDocument, this.editor.ExportSettings.Docx);
			return this.DplExportProxy.ExportToDocx(radFlowDocument);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0004FBEA File Offset: 0x0004DDEA
		private void ApplyDocxExportSettings(object radFlowDocument, EditorDocxSettings settings)
		{
			this.DplExportProxy.SetPageHeader(radFlowDocument, settings.PageHeader, settings.HeaderFontSizeInPoints);
			this.DplExportProxy.SetDefaultFont(radFlowDocument, settings.DefaultFontName, settings.DefaultFontSizeInPoints);
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0004FC1C File Offset: 0x0004DE1C
		protected internal override string GetHtmlContent()
		{
			return this.DplExportProxy.ValidateHtmlForExport(base.GetHtmlContent());
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0004FC2F File Offset: 0x0004DE2F
		protected internal override void InitializeXmlContent()
		{
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0004FC31 File Offset: 0x0004DE31
		protected override string ContentType
		{
			get
			{
				return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x0004FC38 File Offset: 0x0004DE38
		protected override string FileExtension
		{
			get
			{
				return ".docx";
			}
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0004FC3F File Offset: 0x0004DE3F
		protected override ExportType ExportType
		{
			get
			{
				return ExportType.Word;
			}
		}
	}
}
