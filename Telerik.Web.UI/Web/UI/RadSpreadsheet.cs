using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Extensions;
using Telerik.Web.Spreadsheet;
using Telerik.Web.UI.Spreadsheet;

namespace Telerik.Web.UI
{
	// Token: 0x020008CA RID: 2250
	[RequiredScript(typeof(Html5Menu), 3)]
	[ClientScriptResource("Telerik.Web.UI.RadSpreadsheet", "Telerik.Web.UI.Spreadsheet.RadSpreadsheetScripts.js", LoadOrder = 5)]
	[TelerikToolboxCategory("Data")]
	[ToolboxBitmap(typeof(RadSpreadsheet), "Telerik.Web.UI.Spreadsheet.png")]
	[ToolboxData("<{0}:RadSpreadsheet runat=\"server\"></{0}:RadSpreadsheet>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[LightweightRendering]
	[Designer("Telerik.Web.Design.RadSpreadsheetDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredScript(typeof(Html5Spreadsheet), 4)]
	[EmbeddedSkin("Spreadsheet", typeof(RadSpreadsheet))]
	[EmbeddedSkin("Spreadsheet", "Default", typeof(RadSpreadsheet))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.common.css", RenderMode.Lightweight, typeof(RadSpreadsheet))]
	[RequiredScript(typeof(jQueryPlugins), 1)]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadSpreadsheet))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(ModalExtender), 2)]
	[RequiredScript(typeof(MaterialRipple))]
	public class RadSpreadsheet : RadDataBoundControl, ISpreadsheet, INamingContainer, ILocalizableControl, ICallbackEventHandler
	{
		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x00102F01 File Offset: 0x00101101
		// (set) Token: 0x060054A4 RID: 21668 RVA: 0x00102F35 File Offset: 0x00101135
		[Category("Data")]
		[Description("The name of the custom provider to use, as configured in web.config.")]
		[DefaultValue("Integrated")]
		[PersistenceMode(PersistenceMode.Attribute)]
		public string ProviderName
		{
			get
			{
				if (!base.DesignMode)
				{
					return this.Provider.Name;
				}
				return (string)(this.ViewState["ProviderName"] ?? "Integrated");
			}
			set
			{
				if (!base.DesignMode)
				{
					this.Provider = SpreadsheetProviderFactory.GetProvider(this, value);
					return;
				}
				this.ViewState["ProviderName"] = ((value == string.Empty) ? null : value);
			}
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x060054A5 RID: 21669 RVA: 0x00102F6E File Offset: 0x0010116E
		// (set) Token: 0x060054A6 RID: 21670 RVA: 0x00102F76 File Offset: 0x00101176
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SpreadsheetProviderBase Provider
		{
			get
			{
				return this._provider;
			}
			set
			{
				this._provider = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x060054A7 RID: 21671 RVA: 0x00102F85 File Offset: 0x00101185
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual List<Worksheet> Sheets
		{
			get
			{
				if (this._sheets == null)
				{
					this._sheets = new List<Worksheet>();
				}
				return this._sheets;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x060054A8 RID: 21672 RVA: 0x00102FA0 File Offset: 0x001011A0
		// (set) Token: 0x060054A9 RID: 21673 RVA: 0x00102FA8 File Offset: 0x001011A8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual ISpreadsheetAdapterFactory AdapterFactory
		{
			get
			{
				return this._adapterFactory;
			}
			set
			{
				this._adapterFactory = value;
			}
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x060054AA RID: 21674 RVA: 0x00102FB1 File Offset: 0x001011B1
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets a toolbar with acollection of tools that will be shown in the spreadsheet.")]
		public SpreadsheetToolbar Toolbar
		{
			get
			{
				if (this._toolbar == null)
				{
					this._toolbar = new SpreadsheetToolbar();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._toolbar).TrackViewState();
					}
				}
				return this._toolbar;
			}
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x060054AB RID: 21675 RVA: 0x00102FDF File Offset: 0x001011DF
		[Description("Gets a collection of context menus that will be shown in the Spreadsheet.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SpreadsheetContextMenus ContextMenus
		{
			get
			{
				if (this._contextMenus == null && !base.DesignMode)
				{
					this._contextMenus = new SpreadsheetContextMenus(this);
				}
				return this._contextMenus;
			}
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x060054AC RID: 21676 RVA: 0x00103003 File Offset: 0x00101203
		// (set) Token: 0x060054AD RID: 21677 RVA: 0x00103027 File Offset: 0x00101227
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Gets the template for the filter menu that will be shown in the Spreadsheet.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ITemplate FilterMenuTemplate
		{
			get
			{
				if (this._filterMenuTemplate == null && !base.DesignMode)
				{
					this._filterMenuTemplate = new FilterMenuTemplate(this);
				}
				return this._filterMenuTemplate;
			}
			set
			{
				this._filterMenuTemplate = value;
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x060054AE RID: 21678 RVA: 0x00103030 File Offset: 0x00101230
		// (set) Token: 0x060054AF RID: 21679 RVA: 0x00103054 File Offset: 0x00101254
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the template for the custom format dialog that will be shown in the Spreadsheet.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate CustomFormatTemplate
		{
			get
			{
				if (this._customFormatTemplate == null && !base.DesignMode)
				{
					this._customFormatTemplate = new CustomFormatTemplate(this);
				}
				return this._customFormatTemplate;
			}
			set
			{
				this._customFormatTemplate = value;
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x060054B0 RID: 21680 RVA: 0x0010305D File Offset: 0x0010125D
		// (set) Token: 0x060054B1 RID: 21681 RVA: 0x00103081 File Offset: 0x00101281
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the template for the validation dialog that will be shown in the Spreadsheet.")]
		public ITemplate ValidationTemplate
		{
			get
			{
				if (this._validationTemplate == null && !base.DesignMode)
				{
					this._validationTemplate = new ValidationTemplate(this);
				}
				return this._validationTemplate;
			}
			set
			{
				this._validationTemplate = value;
			}
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x060054B2 RID: 21682 RVA: 0x0010308A File Offset: 0x0010128A
		// (set) Token: 0x060054B3 RID: 21683 RVA: 0x001030AE File Offset: 0x001012AE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the template for the hyperlink dialog that will be shown in the Spreadsheet.")]
		public ITemplate HyperlinkTemplate
		{
			get
			{
				if (this._hyperlinkTemplate == null && !base.DesignMode)
				{
					this._hyperlinkTemplate = new HyperlinkTemplate(this);
				}
				return this._hyperlinkTemplate;
			}
			set
			{
				this._hyperlinkTemplate = value;
			}
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x060054B4 RID: 21684 RVA: 0x001030B7 File Offset: 0x001012B7
		public string ResolvedSkin
		{
			get
			{
				return base.RuntimeSkin;
			}
		}

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x060054B5 RID: 21685 RVA: 0x001030BF File Offset: 0x001012BF
		// (set) Token: 0x060054B6 RID: 21686 RVA: 0x001030E1 File Offset: 0x001012E1
		[ClientControlProperty]
		[Description("Gets or sets a value indicating the number of columns.")]
		[ClientPropertyName("columnsCount")]
		[Category("Appearance")]
		[DefaultValue(50)]
		public int ColumnsCount
		{
			get
			{
				return (int)(this.ViewState["ColumnsCount"] ?? 50);
			}
			set
			{
				this.ViewState["ColumnsCount"] = value;
			}
		}

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x060054B7 RID: 21687 RVA: 0x001030F9 File Offset: 0x001012F9
		// (set) Token: 0x060054B8 RID: 21688 RVA: 0x00103122 File Offset: 0x00101322
		[Description("Gets or sets a value indicating the width of the columns.")]
		[ClientPropertyName("columnWidth")]
		[Category("Appearance")]
		[ClientControlProperty]
		[DefaultValue(64.0)]
		public double ColumnWidth
		{
			get
			{
				return (double)(this.ViewState["ColumnWidth"] ?? 64.0);
			}
			set
			{
				this.ViewState["ColumnWidth"] = value;
			}
		}

		// Token: 0x17001BFE RID: 7166
		// (get) Token: 0x060054B9 RID: 21689 RVA: 0x0010313A File Offset: 0x0010133A
		// (set) Token: 0x060054BA RID: 21690 RVA: 0x00103163 File Offset: 0x00101363
		[DefaultValue(20.0)]
		[ClientPropertyName("columnHeaderHeight")]
		[Category("Appearance")]
		[Description("Gets or sets a value indicating the height of the column headers.")]
		[ClientControlProperty]
		public double ColumnHeaderHeight
		{
			get
			{
				return (double)(this.ViewState["ColumnHeaderHeight"] ?? 20.0);
			}
			set
			{
				this.ViewState["ColumnHeaderHeight"] = value;
			}
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x060054BB RID: 21691 RVA: 0x0010317B File Offset: 0x0010137B
		// (set) Token: 0x060054BC RID: 21692 RVA: 0x001031A0 File Offset: 0x001013A0
		[ClientPropertyName("rowsCount")]
		[DefaultValue(200)]
		[Category("Appearance")]
		[Description("Gets or sets a value indicating the number of rows.")]
		[ClientControlProperty]
		public int RowsCount
		{
			get
			{
				return (int)(this.ViewState["RowsCount"] ?? 200);
			}
			set
			{
				this.ViewState["RowsCount"] = value;
			}
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x060054BD RID: 21693 RVA: 0x001031B8 File Offset: 0x001013B8
		// (set) Token: 0x060054BE RID: 21694 RVA: 0x001031E1 File Offset: 0x001013E1
		[ClientControlProperty]
		[Category("Appearance")]
		[Description("Gets or sets a value indicating the height of the rows.")]
		[DefaultValue(20.0)]
		[ClientPropertyName("rowHeight")]
		public double RowHeight
		{
			get
			{
				return (double)(this.ViewState["RowHeight"] ?? 20.0);
			}
			set
			{
				this.ViewState["RowHeight"] = value;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x060054BF RID: 21695 RVA: 0x001031F9 File Offset: 0x001013F9
		// (set) Token: 0x060054C0 RID: 21696 RVA: 0x00103222 File Offset: 0x00101422
		[Description("Gets or sets a value indicating the width of the row headers.")]
		[ClientPropertyName("rowHeaderWidth")]
		[ClientControlProperty]
		[Category("Appearance")]
		[DefaultValue(32.0)]
		public double RowHeaderWidth
		{
			get
			{
				return (double)(this.ViewState["RowHeaderWidth"] ?? 32.0);
			}
			set
			{
				this.ViewState["RowHeaderWidth"] = value;
			}
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x060054C1 RID: 21697 RVA: 0x0010323A File Offset: 0x0010143A
		public override RenderMode ResolvedRenderMode
		{
			get
			{
				return RenderMode.Lightweight;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x060054C2 RID: 21698 RVA: 0x0010323D File Offset: 0x0010143D
		// (set) Token: 0x060054C3 RID: 21699 RVA: 0x0010325D File Offset: 0x0010145D
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Category("Misc")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
				this._localization = null;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x060054C4 RID: 21700 RVA: 0x0010327D File Offset: 0x0010147D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public SpreadsheetStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new SpreadsheetStrings(new LocalizationProvider("RadSpreadsheet", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x060054C5 RID: 21701 RVA: 0x001032BC File Offset: 0x001014BC
		// (set) Token: 0x060054C6 RID: 21702 RVA: 0x001032DC File Offset: 0x001014DC
		[Category("Misc")]
		[DefaultValue("")]
		[Description("Gets or sets a value indicating where RadSpreadsheet will look for its .resx localization files.")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17001C06 RID: 7174
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x0010332F File Offset: 0x0010152F
		// (set) Token: 0x060054C8 RID: 21704 RVA: 0x0010334F File Offset: 0x0010154F
		[Category("Client-side events")]
		[ClientPropertyName("render")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientRender
		{
			get
			{
				return (string)(this.ViewState["OnClientRender"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRender"] = value;
			}
		}

		// Token: 0x17001C07 RID: 7175
		// (get) Token: 0x060054C9 RID: 21705 RVA: 0x00103362 File Offset: 0x00101562
		// (set) Token: 0x060054CA RID: 21706 RVA: 0x00103382 File Offset: 0x00101582
		[ClientPropertyName("change")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientChange
		{
			get
			{
				return (string)(this.ViewState["OnClientChange"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientChange"] = value;
			}
		}

		// Token: 0x17001C08 RID: 7176
		// (get) Token: 0x060054CB RID: 21707 RVA: 0x00103395 File Offset: 0x00101595
		// (set) Token: 0x060054CC RID: 21708 RVA: 0x001033B5 File Offset: 0x001015B5
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("select")]
		public string OnClientSelect
		{
			get
			{
				return (string)(this.ViewState["OnClientSelect"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSelect"] = value;
			}
		}

		// Token: 0x17001C09 RID: 7177
		// (get) Token: 0x060054CD RID: 21709 RVA: 0x001033C8 File Offset: 0x001015C8
		// (set) Token: 0x060054CE RID: 21710 RVA: 0x001033E8 File Offset: 0x001015E8
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("changing")]
		[Category("Client-side events")]
		public string OnClientChanging
		{
			get
			{
				return (string)(this.ViewState["OnClientChanging"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientChanging"] = value;
			}
		}

		// Token: 0x17001C0A RID: 7178
		// (get) Token: 0x060054CF RID: 21711 RVA: 0x001033FB File Offset: 0x001015FB
		// (set) Token: 0x060054D0 RID: 21712 RVA: 0x0010341B File Offset: 0x0010161B
		[ClientPropertyName("changeFormat")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientChangeFormat
		{
			get
			{
				return (string)(this.ViewState["OnClientChangeFormat"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientChangeFormat"] = value;
			}
		}

		// Token: 0x17001C0B RID: 7179
		// (get) Token: 0x060054D1 RID: 21713 RVA: 0x0010342E File Offset: 0x0010162E
		// (set) Token: 0x060054D2 RID: 21714 RVA: 0x0010344E File Offset: 0x0010164E
		[ClientPropertyName("insertSheet")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnClientInsertSheet
		{
			get
			{
				return (string)(this.ViewState["OnClientInsertSheet"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientInsertSheet"] = value;
			}
		}

		// Token: 0x17001C0C RID: 7180
		// (get) Token: 0x060054D3 RID: 21715 RVA: 0x00103461 File Offset: 0x00101661
		// (set) Token: 0x060054D4 RID: 21716 RVA: 0x00103481 File Offset: 0x00101681
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("removeSheet")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnClientRemoveSheet
		{
			get
			{
				return (string)(this.ViewState["OnClientRemoveSheet"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRemoveSheet"] = value;
			}
		}

		// Token: 0x17001C0D RID: 7181
		// (get) Token: 0x060054D5 RID: 21717 RVA: 0x00103494 File Offset: 0x00101694
		// (set) Token: 0x060054D6 RID: 21718 RVA: 0x001034B4 File Offset: 0x001016B4
		[DefaultValue("")]
		[ClientPropertyName("renameSheet")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientRenameSheet
		{
			get
			{
				return (string)(this.ViewState["OnClientRenameSheet"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientRenameSheet"] = value;
			}
		}

		// Token: 0x17001C0E RID: 7182
		// (get) Token: 0x060054D7 RID: 21719 RVA: 0x001034C7 File Offset: 0x001016C7
		// (set) Token: 0x060054D8 RID: 21720 RVA: 0x001034E7 File Offset: 0x001016E7
		[Category("Client-side events")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("selectSheet")]
		[ClientControlEvent]
		public string OnClientSelectSheet
		{
			get
			{
				return (string)(this.ViewState["OnClientSelectSheet"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientSelectSheet"] = value;
			}
		}

		// Token: 0x17001C0F RID: 7183
		// (get) Token: 0x060054D9 RID: 21721 RVA: 0x001034FA File Offset: 0x001016FA
		// (set) Token: 0x060054DA RID: 21722 RVA: 0x0010351A File Offset: 0x0010171A
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("unhideColumn")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientUnhideColumn
		{
			get
			{
				return (string)(this.ViewState["OnClientUnhideColumn"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUnhideColumn"] = value;
			}
		}

		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x060054DB RID: 21723 RVA: 0x0010352D File Offset: 0x0010172D
		// (set) Token: 0x060054DC RID: 21724 RVA: 0x0010354D File Offset: 0x0010174D
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("unhideRow")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnClientUnhideRow
		{
			get
			{
				return (string)(this.ViewState["OnClientUnhideRow"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUnhideRow"] = value;
			}
		}

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x060054DD RID: 21725 RVA: 0x00103560 File Offset: 0x00101760
		// (set) Token: 0x060054DE RID: 21726 RVA: 0x00103580 File Offset: 0x00101780
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("hideColumn")]
		[Category("Client-side events")]
		public string OnClientHideColumn
		{
			get
			{
				return (string)(this.ViewState["OnClientHideColumn"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientHideColumn"] = value;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x060054DF RID: 21727 RVA: 0x00103593 File Offset: 0x00101793
		// (set) Token: 0x060054E0 RID: 21728 RVA: 0x001035B3 File Offset: 0x001017B3
		[ClientPropertyName("hideRow")]
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientHideRow
		{
			get
			{
				return (string)(this.ViewState["OnClientHideRow"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientHideRow"] = value;
			}
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x060054E1 RID: 21729 RVA: 0x001035C6 File Offset: 0x001017C6
		// (set) Token: 0x060054E2 RID: 21730 RVA: 0x001035E6 File Offset: 0x001017E6
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[ClientPropertyName("deleteColumn")]
		[ClientControlEvent]
		public string OnClientDeleteColumn
		{
			get
			{
				return (string)(this.ViewState["OnClientDeleteColumn"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDeleteColumn"] = value;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x060054E3 RID: 21731 RVA: 0x001035F9 File Offset: 0x001017F9
		// (set) Token: 0x060054E4 RID: 21732 RVA: 0x00103619 File Offset: 0x00101819
		[ClientPropertyName("deleteRow")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientDeleteRow
		{
			get
			{
				return (string)(this.ViewState["OnClientDeleteRow"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientDeleteRow"] = value;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x060054E5 RID: 21733 RVA: 0x0010362C File Offset: 0x0010182C
		// (set) Token: 0x060054E6 RID: 21734 RVA: 0x0010364C File Offset: 0x0010184C
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("insertColumn")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientInsertColumn
		{
			get
			{
				return (string)(this.ViewState["OnClientInsertColumn"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientInsertColumn"] = value;
			}
		}

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x060054E7 RID: 21735 RVA: 0x0010365F File Offset: 0x0010185F
		// (set) Token: 0x060054E8 RID: 21736 RVA: 0x0010367F File Offset: 0x0010187F
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("insertRow")]
		[DefaultValue("")]
		public string OnClientInsertRow
		{
			get
			{
				return (string)(this.ViewState["OnClientInsertRow"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientInsertRow"] = value;
			}
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x060054E9 RID: 21737 RVA: 0x00103692 File Offset: 0x00101892
		// (set) Token: 0x060054EA RID: 21738 RVA: 0x001036B2 File Offset: 0x001018B2
		[ClientPropertyName("excelExport")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		public string OnClientExcelExport
		{
			get
			{
				return (string)(this.ViewState["OnClientExcelExport"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientExcelExport"] = value;
			}
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x001036C5 File Offset: 0x001018C5
		// (set) Token: 0x060054EC RID: 21740 RVA: 0x001036E5 File Offset: 0x001018E5
		[ClientControlEvent]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("excelImport")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnClientExcelImport
		{
			get
			{
				return (string)(this.ViewState["OnClientExcelImport"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientExcelImport"] = value;
			}
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x001036F8 File Offset: 0x001018F8
		// (set) Token: 0x060054EE RID: 21742 RVA: 0x00103718 File Offset: 0x00101918
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("pdfExport")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientPdfExport
		{
			get
			{
				return (string)(this.ViewState["OnClientPdfExport"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPdfExport"] = value;
			}
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x060054EF RID: 21743 RVA: 0x0010372B File Offset: 0x0010192B
		// (set) Token: 0x060054F0 RID: 21744 RVA: 0x0010374B File Offset: 0x0010194B
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("copy")]
		[Category("Client-side events")]
		public string OnClientCopy
		{
			get
			{
				return (string)(this.ViewState["OnClientCopy"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientCopy"] = value;
			}
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x060054F1 RID: 21745 RVA: 0x0010375E File Offset: 0x0010195E
		// (set) Token: 0x060054F2 RID: 21746 RVA: 0x0010377E File Offset: 0x0010197E
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("cut")]
		[Category("Client-side events")]
		public string OnClientCut
		{
			get
			{
				return (string)(this.ViewState["OnClientCut"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientCut"] = value;
			}
		}

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x060054F3 RID: 21747 RVA: 0x00103791 File Offset: 0x00101991
		// (set) Token: 0x060054F4 RID: 21748 RVA: 0x001037B1 File Offset: 0x001019B1
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("paste")]
		public string OnClientPaste
		{
			get
			{
				return (string)(this.ViewState["OnClientPaste"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientPaste"] = value;
			}
		}

		// Token: 0x060054F5 RID: 21749 RVA: 0x001037C4 File Offset: 0x001019C4
		public RadSpreadsheet()
		{
			this.LoadProvider();
			this.LoadAdapterFactory();
		}

		// Token: 0x060054F6 RID: 21750 RVA: 0x001037D8 File Offset: 0x001019D8
		protected virtual void LoadAdapterFactory()
		{
			this._adapterFactory = new SpreadsheetAdapterFactory(this);
		}

		// Token: 0x060054F7 RID: 21751 RVA: 0x001037E6 File Offset: 0x001019E6
		protected override void OnDataPropertyChanged()
		{
			base.OnDataPropertyChanged();
			this._dataPropertyChanged = true;
		}

		// Token: 0x060054F8 RID: 21752 RVA: 0x001037F5 File Offset: 0x001019F5
		private void ClearChildControls()
		{
			base.ClearChildState();
			base.ChildControlsCreated = false;
			this.Controls.Clear();
		}

		// Token: 0x060054F9 RID: 21753 RVA: 0x0010380F File Offset: 0x00101A0F
		protected override void CreateChildControls()
		{
			this._dataPropertyChanged = false;
			this.CreateToolbar();
			this.CreateContextMenus();
			this.CreateCalendarDropDown();
			this.CreateListBoxDropDown();
			this.CreateFilterMenu();
			this.CreateCustomFormat();
			this.CreateValidation();
			this.CreateHyperlink();
		}

		// Token: 0x060054FA RID: 21754 RVA: 0x00103848 File Offset: 0x00101A48
		private void CreateListBoxDropDown()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rssSlide";
			webControl.Style.Add("display", "none");
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
			webControl2.CssClass = string.Format("{0} radSkin_{1} {2}", "rssPopup", base.RuntimeSkin.ToString(), "rssSkin");
			RadListBox child = new RadListBox
			{
				RenderMode = RenderMode.Lightweight,
				ID = "SpreadsheetListBoxDropDown",
				Skin = this.ResolvedSkin,
				EnableEmbeddedSkins = this.EnableEmbeddedSkins,
				EnableViewState = false
			};
			webControl2.Controls.Add(child);
			webControl.Controls.Add(webControl2);
			this.Controls.Add(webControl);
		}

		// Token: 0x060054FB RID: 21755 RVA: 0x00103908 File Offset: 0x00101B08
		private void CreateCalendarDropDown()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rssSlide";
			webControl.Style.Add("display", "none");
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
			webControl2.CssClass = string.Format("{0} radSkin_{1} {2}", "rssPopup", base.RuntimeSkin.ToString(), "rssSkin");
			RadCalendar child = new RadCalendar
			{
				RenderMode = RenderMode.Lightweight,
				ID = "SpreadsheetCalendarDropDown",
				EnableMultiSelect = false,
				Skin = this.ResolvedSkin,
				EnableEmbeddedSkins = this.EnableEmbeddedSkins,
				EnableViewState = false
			};
			webControl2.Controls.Add(child);
			webControl.Controls.Add(webControl2);
			this.Controls.Add(webControl);
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x001039D0 File Offset: 0x00101BD0
		private void CreateToolbar()
		{
			WebControl child = this.AdapterFactory.CreateAdapter().CreateToolbar(this.Toolbar);
			this.Controls.Add(child);
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x00103A00 File Offset: 0x00101C00
		protected virtual void CreateContextMenus()
		{
			SpreadsheetContextMenu cellContextMenu = this.ContextMenus.CellContextMenu;
			if (cellContextMenu.Enabled)
			{
				this.ContextMenus.PopulateContextMenu(cellContextMenu, SpreadsheetContextMenus.DefaultCellContextMenuItems);
				this.Controls.Add(cellContextMenu);
			}
			SpreadsheetContextMenu rowHeaderContextMenu = this.ContextMenus.RowHeaderContextMenu;
			if (rowHeaderContextMenu.Enabled)
			{
				this.ContextMenus.PopulateContextMenu(rowHeaderContextMenu, SpreadsheetContextMenus.DefaultRowHeaderContextMenuItems);
				this.Controls.Add(rowHeaderContextMenu);
			}
			SpreadsheetContextMenu columnHeaderContextMenu = this.ContextMenus.ColumnHeaderContextMenu;
			if (columnHeaderContextMenu.Enabled)
			{
				this.ContextMenus.PopulateContextMenu(columnHeaderContextMenu, SpreadsheetContextMenus.DefaultColumnHeaderContextMenuItems);
				this.Controls.Add(columnHeaderContextMenu);
			}
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x00103AA0 File Offset: 0x00101CA0
		private void CreateFilterMenu()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rssSlide";
			webControl.Style.Add("display", "none");
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
			webControl2.CssClass = string.Format("{0} radSkin_{1} {2} {3}", new object[]
			{
				"rssPopup",
				base.RuntimeSkin.ToString(),
				"rssFilterMenuPopup",
				"rssSkin"
			});
			Panel panel = new Panel
			{
				CssClass = "rssFilterMenu"
			};
			NamingContainer namingContainer = new NamingContainer
			{
				ID = "FilterMenu"
			};
			this.FilterMenuTemplate.InstantiateIn(namingContainer);
			panel.Controls.Add(namingContainer);
			webControl2.Controls.Add(panel);
			webControl.Controls.Add(webControl2);
			this.Controls.Add(webControl);
		}

		// Token: 0x060054FF RID: 21759 RVA: 0x00103B8C File Offset: 0x00101D8C
		private void CreateCustomFormat()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rssCustomFormatTemplate";
			webControl.Style.Add("display", "none");
			NamingContainer namingContainer = new NamingContainer
			{
				ID = "CustomFormat"
			};
			this.CustomFormatTemplate.InstantiateIn(namingContainer);
			webControl.Controls.Add(namingContainer);
			this.Controls.Add(webControl);
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00103BF8 File Offset: 0x00101DF8
		private void CreateValidation()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rssValidationTemplate";
			webControl.Style.Add("display", "none");
			this.Controls.Add(webControl);
			NamingContainer namingContainer = new NamingContainer
			{
				ID = "Validation"
			};
			webControl.Controls.Add(namingContainer);
			this.ValidationTemplate.InstantiateIn(namingContainer);
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x00103C64 File Offset: 0x00101E64
		private void CreateHyperlink()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rssHyperlinkTemplate";
			webControl.Style.Add("display", "none");
			this.Controls.Add(webControl);
			NamingContainer namingContainer = new NamingContainer
			{
				ID = "Hyperlink"
			};
			webControl.Controls.Add(namingContainer);
			this.HyperlinkTemplate.InstantiateIn(namingContainer);
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x00103CD0 File Offset: 0x00101ED0
		protected override void OnPreRender(EventArgs e)
		{
			if (this._dataPropertyChanged)
			{
				this.EnsureDataBound();
			}
			this.ClearChildControls();
			this.EnsureChildControls();
			base.OnPreRender(e);
			if (this.Culture.Name != "en-US")
			{
				string script = this.Culture.Format();
				ScriptManager.RegisterStartupScript(this.Page, typeof(RadSpreadsheet), "SpreadsheetCultureScript", script, true);
			}
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x00103D3D File Offset: 0x00101F3D
		protected virtual void LoadProvider()
		{
			this.ProviderName = "Integrated";
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x00103D4A File Offset: 0x00101F4A
		protected override void EnsureDataBound()
		{
			base.EnsureDataBound();
			if (base.RequiresDataBinding && this.ProviderName != "Integrated")
			{
				this.DataBind();
			}
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x00103D72 File Offset: 0x00101F72
		protected override void PerformSelect()
		{
			if (base.DesignMode)
			{
				return;
			}
			base.RequiresDataBinding = false;
			this.OnDataBinding(EventArgs.Empty);
			this.BindSheets(this.Provider.GetSheets());
			this.OnDataBound(EventArgs.Empty);
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x00103DAB File Offset: 0x00101FAB
		protected virtual void BindSheets(List<Worksheet> sheets)
		{
			this.Sheets.Clear();
			this.Sheets.AddRange(sheets);
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x00103DC4 File Offset: 0x00101FC4
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			descriptor.AddProperty("_uniqueId", this.UniqueID);
			descriptor.AddScriptProperty("sheetsData", this.Serializer.Serialize(this.Sheets));
			this.DescribeContextMenus(descriptor);
			this.DescribeLocalization(descriptor);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00103E40 File Offset: 0x00102040
		private void DescribeContextMenus(IScriptDescriptor descriptor)
		{
			if (this.ContextMenus.CellContextMenu.Enabled)
			{
				descriptor.AddProperty("_cellContextMenuID", this.ContextMenus.CellContextMenu.ClientID);
			}
			if (this.ContextMenus.RowHeaderContextMenu.Enabled)
			{
				descriptor.AddProperty("_rowHeaderContextMenuID", this.ContextMenus.RowHeaderContextMenu.ClientID);
			}
			if (this.ContextMenus.ColumnHeaderContextMenu.Enabled)
			{
				descriptor.AddProperty("_columnHeaderContextMenuID", this.ContextMenus.ColumnHeaderContextMenu.ClientID);
			}
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x00103ED4 File Offset: 0x001020D4
		private void DescribeLocalization(IScriptDescriptor descriptor)
		{
			string text = this.Serializer.Serialize(this.Localization);
			if (!text.IsEmptySerializedObject())
			{
				descriptor.AddScriptProperty("localization", text);
			}
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x0600550A RID: 21770 RVA: 0x00103F08 File Offset: 0x00102108
		protected internal JavaScriptSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this._serializer = new JavaScriptSerializer();
					this._serializer.MaxJsonLength = int.MaxValue;
					this._serializer.RegisterConverters(new JavaScriptConverter[]
					{
						new SpreadsheetConverter(),
						new LocalizationConverter()
					});
				}
				return this._serializer;
			}
		}

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x0600550B RID: 21771 RVA: 0x00103F61 File Offset: 0x00102161
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x0600550C RID: 21772 RVA: 0x00103F64 File Offset: 0x00102164
		protected override string CssClassFormatString
		{
			get
			{
				return "RadSpreadsheet RadSpreadsheet_{0} radSkin_{0} rssSkin";
			}
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x0600550D RID: 21773 RVA: 0x00103F6B File Offset: 0x0010216B
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x00103F6F File Offset: 0x0010216F
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				return;
			}
			base.Render(writer);
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x00103F81 File Offset: 0x00102181
		public string GetCallbackResult()
		{
			return "OK";
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x00103F88 File Offset: 0x00102188
		public void RaiseCallbackEvent(string eventArgument)
		{
			Workbook workbook = Workbook.FromJson(eventArgument);
			this.Provider.SaveWorkbook(workbook);
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x00103FA8 File Offset: 0x001021A8
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<double>(descriptor, "columnHeaderHeight", this.ColumnHeaderHeight, 20.0);
			base.DescribeProperty<int>(descriptor, "columnsCount", this.ColumnsCount, 50);
			base.DescribeProperty<double>(descriptor, "columnWidth", this.ColumnWidth, 64.0);
			base.DescribeProperty<double>(descriptor, "rowHeaderWidth", this.RowHeaderWidth, 32.0);
			base.DescribeProperty<double>(descriptor, "rowHeight", this.RowHeight, 20.0);
			base.DescribeProperty<int>(descriptor, "rowsCount", this.RowsCount, 200);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005512 RID: 21778 RVA: 0x00104054 File Offset: 0x00102254
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "render", this.OnClientRender);
			RadDataBoundControl.DescribeEvent(descriptor, "change", this.OnClientChange);
			RadDataBoundControl.DescribeEvent(descriptor, "select", this.OnClientSelect);
			RadDataBoundControl.DescribeEvent(descriptor, "changing", this.OnClientChanging);
			RadDataBoundControl.DescribeEvent(descriptor, "changeFormat", this.OnClientChangeFormat);
			RadDataBoundControl.DescribeEvent(descriptor, "insertSheet", this.OnClientInsertSheet);
			RadDataBoundControl.DescribeEvent(descriptor, "removeSheet", this.OnClientRemoveSheet);
			RadDataBoundControl.DescribeEvent(descriptor, "renameSheet", this.OnClientRenameSheet);
			RadDataBoundControl.DescribeEvent(descriptor, "selectSheet", this.OnClientSelectSheet);
			RadDataBoundControl.DescribeEvent(descriptor, "unhideColumn", this.OnClientUnhideColumn);
			RadDataBoundControl.DescribeEvent(descriptor, "unhideRow", this.OnClientUnhideRow);
			RadDataBoundControl.DescribeEvent(descriptor, "hideColumn", this.OnClientHideColumn);
			RadDataBoundControl.DescribeEvent(descriptor, "hideRow", this.OnClientHideRow);
			RadDataBoundControl.DescribeEvent(descriptor, "deleteColumn", this.OnClientDeleteColumn);
			RadDataBoundControl.DescribeEvent(descriptor, "deleteRow", this.OnClientDeleteRow);
			RadDataBoundControl.DescribeEvent(descriptor, "insertColumn", this.OnClientInsertColumn);
			RadDataBoundControl.DescribeEvent(descriptor, "insertRow", this.OnClientInsertRow);
			RadDataBoundControl.DescribeEvent(descriptor, "excelExport", this.OnClientExcelExport);
			RadDataBoundControl.DescribeEvent(descriptor, "excelImport", this.OnClientExcelImport);
			RadDataBoundControl.DescribeEvent(descriptor, "pdfExport", this.OnClientPdfExport);
			RadDataBoundControl.DescribeEvent(descriptor, "copy", this.OnClientCopy);
			RadDataBoundControl.DescribeEvent(descriptor, "cut", this.OnClientCut);
			RadDataBoundControl.DescribeEvent(descriptor, "paste", this.OnClientPaste);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400146D RID: 5229
		private SpreadsheetProviderBase _provider;

		// Token: 0x0400146E RID: 5230
		private List<Worksheet> _sheets;

		// Token: 0x0400146F RID: 5231
		private SpreadsheetToolbar _toolbar;

		// Token: 0x04001470 RID: 5232
		private JavaScriptSerializer _serializer;

		// Token: 0x04001471 RID: 5233
		private SpreadsheetStrings _localization;

		// Token: 0x04001472 RID: 5234
		private bool _dataPropertyChanged;

		// Token: 0x04001473 RID: 5235
		private ISpreadsheetAdapterFactory _adapterFactory;

		// Token: 0x04001474 RID: 5236
		private SpreadsheetContextMenus _contextMenus;

		// Token: 0x04001475 RID: 5237
		private ITemplate _filterMenuTemplate;

		// Token: 0x04001476 RID: 5238
		private ITemplate _customFormatTemplate;

		// Token: 0x04001477 RID: 5239
		private ITemplate _validationTemplate;

		// Token: 0x04001478 RID: 5240
		private ITemplate _hyperlinkTemplate;
	}
}
