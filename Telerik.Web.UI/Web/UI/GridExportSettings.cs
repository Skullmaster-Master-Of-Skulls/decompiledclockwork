using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020010FB RID: 4347
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridExportSettings : ObjectWithState
	{
		// Token: 0x0600B1F5 RID: 45557 RVA: 0x0026AAB9 File Offset: 0x00268CB9
		public GridExportSettings(StateBag OwnerStateBag) : base("ges_", OwnerStateBag)
		{
		}

		// Token: 0x170039A0 RID: 14752
		// (get) Token: 0x0600B1F6 RID: 45558 RVA: 0x0026AAC7 File Offset: 0x00268CC7
		// (set) Token: 0x0600B1F7 RID: 45559 RVA: 0x0026AAF6 File Offset: 0x00268CF6
		[Description("")]
		[DefaultValue("RadGridExport")]
		[NotifyParentProperty(true)]
		public string FileName
		{
			get
			{
				if (base.ViewState["_fn"] == null)
				{
					return "RadGridExport";
				}
				return (string)base.ViewState["_fn"];
			}
			set
			{
				base.ViewState["_fn"] = value;
			}
		}

		// Token: 0x170039A1 RID: 14753
		// (get) Token: 0x0600B1F8 RID: 45560 RVA: 0x0026AB09 File Offset: 0x00268D09
		// (set) Token: 0x0600B1F9 RID: 45561 RVA: 0x0026AB34 File Offset: 0x00268D34
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("")]
		public bool ExportOnlyData
		{
			get
			{
				return base.ViewState["_eod"] != null && (bool)base.ViewState["_eod"];
			}
			set
			{
				base.ViewState["_eod"] = value;
			}
		}

		// Token: 0x170039A2 RID: 14754
		// (get) Token: 0x0600B1FA RID: 45562 RVA: 0x0026AB4C File Offset: 0x00268D4C
		// (set) Token: 0x0600B1FB RID: 45563 RVA: 0x0026AB77 File Offset: 0x00268D77
		[NotifyParentProperty(true)]
		[Description("")]
		[DefaultValue(false)]
		public bool HideStructureColumns
		{
			get
			{
				return base.ViewState["_hsc"] != null && (bool)base.ViewState["_hsc"];
			}
			set
			{
				base.ViewState["_hsc"] = value;
			}
		}

		// Token: 0x170039A3 RID: 14755
		// (get) Token: 0x0600B1FC RID: 45564 RVA: 0x0026AB8F File Offset: 0x00268D8F
		// (set) Token: 0x0600B1FD RID: 45565 RVA: 0x0026ABBA File Offset: 0x00268DBA
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("")]
		public bool HideNonDataBoundColumns
		{
			get
			{
				return base.ViewState["_hndbc"] != null && (bool)base.ViewState["_hndbc"];
			}
			set
			{
				base.ViewState["_hndbc"] = value;
			}
		}

		// Token: 0x170039A4 RID: 14756
		// (get) Token: 0x0600B1FE RID: 45566 RVA: 0x0026ABD2 File Offset: 0x00268DD2
		// (set) Token: 0x0600B1FF RID: 45567 RVA: 0x0026ABFD File Offset: 0x00268DFD
		[DefaultValue(false)]
		[Description("Determines whether the RadGrid styles will be applied to the exported files")]
		public bool UseItemStyles
		{
			get
			{
				return base.ViewState["UseItemStyles"] != null && (bool)base.ViewState["UseItemStyles"];
			}
			set
			{
				base.ViewState["UseItemStyles"] = value;
			}
		}

		// Token: 0x170039A5 RID: 14757
		// (get) Token: 0x0600B200 RID: 45568 RVA: 0x0026AC15 File Offset: 0x00268E15
		// (set) Token: 0x0600B201 RID: 45569 RVA: 0x0026AC40 File Offset: 0x00268E40
		[Description("Determines whether the DataFormatStrings of the columns will be suppressed when exporting. Setting this property to true will cause a rebind when exporting.")]
		[DefaultValue(false)]
		public bool SuppressColumnDataFormatStrings
		{
			get
			{
				return base.ViewState["SuppressColumnDataFormatStrings"] != null && (bool)base.ViewState["SuppressColumnDataFormatStrings"];
			}
			set
			{
				base.ViewState["SuppressColumnDataFormatStrings"] = value;
			}
		}

		// Token: 0x170039A6 RID: 14758
		// (get) Token: 0x0600B202 RID: 45570 RVA: 0x0026AC58 File Offset: 0x00268E58
		// (set) Token: 0x0600B203 RID: 45571 RVA: 0x0026AC83 File Offset: 0x00268E83
		[DefaultValue(false)]
		[Description("")]
		[NotifyParentProperty(true)]
		public bool IgnorePaging
		{
			get
			{
				return base.ViewState["_ip"] != null && (bool)base.ViewState["_ip"];
			}
			set
			{
				base.ViewState["_ip"] = value;
			}
		}

		// Token: 0x170039A7 RID: 14759
		// (get) Token: 0x0600B204 RID: 45572 RVA: 0x0026AC9B File Offset: 0x00268E9B
		// (set) Token: 0x0600B205 RID: 45573 RVA: 0x0026ACC6 File Offset: 0x00268EC6
		[Description("")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
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

		// Token: 0x170039A8 RID: 14760
		// (get) Token: 0x0600B206 RID: 45574 RVA: 0x0026ACDE File Offset: 0x00268EDE
		[NotifyParentProperty(true)]
		[Category("Pdf")]
		[DefaultValue(typeof(GridPdfSettings))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		// Token: 0x170039A9 RID: 14761
		// (get) Token: 0x0600B207 RID: 45575 RVA: 0x0026ACFF File Offset: 0x00268EFF
		[NotifyParentProperty(true)]
		[Category("Excel")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridExcelSettings Excel
		{
			get
			{
				if (this._excelSettings == null)
				{
					this._excelSettings = new GridExcelSettings(base.OwnerViewState);
				}
				return this._excelSettings;
			}
		}

		// Token: 0x170039AA RID: 14762
		// (get) Token: 0x0600B208 RID: 45576 RVA: 0x0026AD20 File Offset: 0x00268F20
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Word")]
		public GridWordSettings Word
		{
			get
			{
				if (this._wordSettings == null)
				{
					this._wordSettings = new GridWordSettings(base.OwnerViewState);
				}
				return this._wordSettings;
			}
		}

		// Token: 0x170039AB RID: 14763
		// (get) Token: 0x0600B209 RID: 45577 RVA: 0x0026AD41 File Offset: 0x00268F41
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Pdf")]
		public GridCsvSettings Csv
		{
			get
			{
				if (this._csvSettings == null)
				{
					this._csvSettings = new GridCsvSettings(base.OwnerViewState);
				}
				return this._csvSettings;
			}
		}

		// Token: 0x04002EB3 RID: 11955
		private GridPdfSettings _pdfSettings;

		// Token: 0x04002EB4 RID: 11956
		private GridExcelSettings _excelSettings;

		// Token: 0x04002EB5 RID: 11957
		private GridWordSettings _wordSettings;

		// Token: 0x04002EB6 RID: 11958
		private GridCsvSettings _csvSettings;
	}
}
