using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000B3C RID: 2876
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class EditorExportSettings : ObjectWithState
	{
		// Token: 0x06006C90 RID: 27792 RVA: 0x00193604 File Offset: 0x00191804
		public EditorExportSettings(StateBag OwnerStateBag) : base("ees_", OwnerStateBag)
		{
		}

		// Token: 0x1700239F RID: 9119
		// (get) Token: 0x06006C91 RID: 27793 RVA: 0x00193612 File Offset: 0x00191812
		// (set) Token: 0x06006C92 RID: 27794 RVA: 0x00193641 File Offset: 0x00191841
		[DefaultValue("RadEditorExport")]
		[NotifyParentProperty(true)]
		[Description("")]
		public string FileName
		{
			get
			{
				if (base.ViewState["_fn"] == null)
				{
					return "RadEditorExport";
				}
				return (string)base.ViewState["_fn"];
			}
			set
			{
				base.ViewState["_fn"] = value;
			}
		}

		// Token: 0x170023A0 RID: 9120
		// (get) Token: 0x06006C93 RID: 27795 RVA: 0x00193654 File Offset: 0x00191854
		// (set) Token: 0x06006C94 RID: 27796 RVA: 0x0019367F File Offset: 0x0019187F
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("")]
		public bool OpenInNewWindow
		{
			get
			{
				return base.ViewState["_osw"] != null && (bool)base.ViewState["_osw"];
			}
			set
			{
				base.ViewState["_osw"] = value;
			}
		}

		// Token: 0x170023A1 RID: 9121
		// (get) Token: 0x06006C95 RID: 27797 RVA: 0x00193697 File Offset: 0x00191897
		[NotifyParentProperty(true)]
		[Category("Pdf")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public GridPdfSettings Pdf
		{
			get
			{
				if (this._pdfSettings == null)
				{
					this._pdfSettings = new GridPdfSettings(base.OwnerViewState);
				}
				return this._pdfSettings;
			}
		}

		// Token: 0x170023A2 RID: 9122
		// (get) Token: 0x06006C96 RID: 27798 RVA: 0x001936B8 File Offset: 0x001918B8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Rtf")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public EditorRtfSettings Rtf
		{
			get
			{
				if (this._rtfSettings == null)
				{
					this._rtfSettings = new EditorRtfSettings(base.OwnerViewState);
				}
				return this._rtfSettings;
			}
		}

		// Token: 0x170023A3 RID: 9123
		// (get) Token: 0x06006C97 RID: 27799 RVA: 0x001936D9 File Offset: 0x001918D9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Docx")]
		[NotifyParentProperty(true)]
		public EditorDocxSettings Docx
		{
			get
			{
				if (this._docxSettings == null)
				{
					this._docxSettings = new EditorDocxSettings(base.OwnerViewState);
				}
				return this._docxSettings;
			}
		}

		// Token: 0x170023A4 RID: 9124
		// (get) Token: 0x06006C98 RID: 27800 RVA: 0x001936FA File Offset: 0x001918FA
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Markdown")]
		public EditorMarkdownSettings Markdown
		{
			get
			{
				if (this._markdownSettings == null)
				{
					this._markdownSettings = new EditorMarkdownSettings(base.OwnerViewState);
				}
				return this._markdownSettings;
			}
		}

		// Token: 0x04001D32 RID: 7474
		private GridPdfSettings _pdfSettings;

		// Token: 0x04001D33 RID: 7475
		private EditorRtfSettings _rtfSettings;

		// Token: 0x04001D34 RID: 7476
		private EditorDocxSettings _docxSettings;

		// Token: 0x04001D35 RID: 7477
		private EditorMarkdownSettings _markdownSettings;
	}
}
