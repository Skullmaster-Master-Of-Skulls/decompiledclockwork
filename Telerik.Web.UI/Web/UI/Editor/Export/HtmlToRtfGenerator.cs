using System;
using Telerik.Web.UI.Editor.Dpl;

namespace Telerik.Web.UI.Editor.Export
{
	// Token: 0x02000B48 RID: 2888
	internal class HtmlToRtfGenerator : RadEditorExportTemplate
	{
		// Token: 0x06006CD8 RID: 27864 RVA: 0x00194660 File Offset: 0x00192860
		public HtmlToRtfGenerator(RadEditor radEditor) : base(radEditor)
		{
		}

		// Token: 0x170023B7 RID: 9143
		// (get) Token: 0x06006CD9 RID: 27865 RVA: 0x00194669 File Offset: 0x00192869
		// (set) Token: 0x06006CDA RID: 27866 RVA: 0x00194671 File Offset: 0x00192871
		public IDplExportProxy DplExportProxy { get; set; }

		// Token: 0x06006CDB RID: 27867 RVA: 0x0019467C File Offset: 0x0019287C
		protected internal override string GenerateOutput()
		{
			object radFlowDocument = this.DplExportProxy.ConvertHtmlToRadFlowDocument(this.GetHtmlContent());
			this.ApplyDocxExportSettings(radFlowDocument, this.editor.ExportSettings.Rtf);
			return this.DplExportProxy.ExportToRtf(radFlowDocument);
		}

		// Token: 0x06006CDC RID: 27868 RVA: 0x001946BE File Offset: 0x001928BE
		private void ApplyDocxExportSettings(object radFlowDocument, EditorRtfSettings settings)
		{
			this.DplExportProxy.SetPageHeader(radFlowDocument, settings.PageHeader, settings.HeaderFontSizeInPoints);
			this.DplExportProxy.SetDefaultFont(radFlowDocument, settings.DefaultFontName, settings.DefaultFontSizeInPoints);
		}

		// Token: 0x06006CDD RID: 27869 RVA: 0x001946F0 File Offset: 0x001928F0
		protected internal override string GetHtmlContent()
		{
			return this.DplExportProxy.ValidateHtmlForExport(base.GetHtmlContent());
		}

		// Token: 0x06006CDE RID: 27870 RVA: 0x00194703 File Offset: 0x00192903
		protected internal override void InitializeXmlContent()
		{
		}

		// Token: 0x170023B8 RID: 9144
		// (get) Token: 0x06006CDF RID: 27871 RVA: 0x00194705 File Offset: 0x00192905
		protected override string ContentType
		{
			get
			{
				return "application/rtf";
			}
		}

		// Token: 0x170023B9 RID: 9145
		// (get) Token: 0x06006CE0 RID: 27872 RVA: 0x0019470C File Offset: 0x0019290C
		protected override string FileExtension
		{
			get
			{
				return ".rtf";
			}
		}

		// Token: 0x170023BA RID: 9146
		// (get) Token: 0x06006CE1 RID: 27873 RVA: 0x00194713 File Offset: 0x00192913
		protected override ExportType ExportType
		{
			get
			{
				return ExportType.Rtf;
			}
		}
	}
}
