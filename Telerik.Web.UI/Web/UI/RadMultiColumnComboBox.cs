using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.DataSourceSettings;
using Telerik.Web.UI.HtmlChart;
using Telerik.Web.UI.MultiColumnComboBox;

namespace Telerik.Web.UI
{
	// Token: 0x02000063 RID: 99
	[ToolboxData("<{0}:RadMultiColumnComboBox Runat=\"server\"></{0}:RadMultiColumnComboBox>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Designer("Telerik.Web.Design.RadMultiColumnComboBoxDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[RequiredScript(typeof(jQueryPlugins))]
	[EmbeddedSkin("DropdownGrid", typeof(RadMultiColumnComboBox))]
	[ClientScriptResource("Telerik.Web.UI.RadMultiColumnComboBox", "Telerik.Web.UI.MultiColumnComboBox.Scripts.RadMultiColumnComboBox.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadMultiColumnComboBox))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadMultiColumnComboBox))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadMultiColumnComboBox))]
	[ParseChildren(ChildrenAsProperties = true)]
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Items")]
	[ValidationProperty("Text")]
	[ControlValueProperty("Value")]
	[TelerikToolboxCategory("Data Editing")]
	[ToolboxBitmap(typeof(RadMultiColumnComboBox), "Telerik.Web.UI.MultiColumnComboBox.png")]
	[EmbeddedSkin("DropdownGrid", "Default", typeof(RadMultiColumnComboBox))]
	[RequiredScript(typeof(Html5MultiColumnComboBox))]
	public class RadMultiColumnComboBox : RadDataBoundControl, ICallbackEventHandler, IPostBackEventHandler, IItemContainer
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000384 RID: 900 RVA: 0x000088DB File Offset: 0x00006ADB
		// (remove) Token: 0x06000385 RID: 901 RVA: 0x000088EE File Offset: 0x00006AEE
		public event RadMultiColumnComboBoxItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadMultiColumnComboBox.MultiColumnComboBoxItemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMultiColumnComboBox.MultiColumnComboBoxItemDataBoundEvent, value);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000386 RID: 902 RVA: 0x00008901 File Offset: 0x00006B01
		// (remove) Token: 0x06000387 RID: 903 RVA: 0x00008914 File Offset: 0x00006B14
		public event RadMultiColumnComboBoxSelectedIndexChangedEventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(RadMultiColumnComboBox.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMultiColumnComboBox.SelectedIndexChangedEvent, value);
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00008928 File Offset: 0x00006B28
		private void RaiseEvent(object eventKey, RadMultiColumnComboBoxItemEventArgs e)
		{
			RadMultiColumnComboBoxItemEventHandler radMultiColumnComboBoxItemEventHandler = (RadMultiColumnComboBoxItemEventHandler)base.Events[eventKey];
			if (radMultiColumnComboBoxItemEventHandler != null)
			{
				radMultiColumnComboBoxItemEventHandler(this, e);
			}
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00008952 File Offset: 0x00006B52
		protected virtual void RaiseSelectedIndexChandedEvent()
		{
			this.OnSelectedIndexChanged();
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0000895C File Offset: 0x00006B5C
		protected virtual void OnSelectedIndexChanged()
		{
			RadMultiColumnComboBoxSelectedIndexChangedEventHandler radMultiColumnComboBoxSelectedIndexChangedEventHandler = (RadMultiColumnComboBoxSelectedIndexChangedEventHandler)base.Events[RadMultiColumnComboBox.SelectedIndexChangedEvent];
			if (radMultiColumnComboBoxSelectedIndexChangedEventHandler != null)
			{
				RadMultiColumnComboBoxSelectedIndexChangedEventArgs e = new RadMultiColumnComboBoxSelectedIndexChangedEventArgs(this.Text, this._oldText, this.Value, this._oldValue);
				radMultiColumnComboBoxSelectedIndexChangedEventHandler(this, e);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600038B RID: 907 RVA: 0x000089A8 File Offset: 0x00006BA8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Animation AnimationSettings
		{
			get
			{
				if (this._animation == null)
				{
					this._animation = new Animation();
				}
				return this._animation;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000089C3 File Offset: 0x00006BC3
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Description("Contains settings about the schema and model of the data used in RadClientDataSource.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual ClientDataSourceSchema Schema
		{
			get
			{
				if (this._schema == null)
				{
					this._schema = new ClientDataSourceSchema();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._schema).TrackViewState();
				}
				return this._schema;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600038D RID: 909 RVA: 0x000089F1 File Offset: 0x00006BF1
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00008A11 File Offset: 0x00006C11
		[DefaultValue("")]
		public string CascadeFrom
		{
			get
			{
				return (string)(this.ViewState["CascadeFrom"] ?? "");
			}
			set
			{
				this.ViewState["CascadeFrom"] = value;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00008A24 File Offset: 0x00006C24
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00008A44 File Offset: 0x00006C44
		[DefaultValue("")]
		public string CascadeFromField
		{
			get
			{
				return (string)(this.ViewState["CascadeFromField"] ?? "");
			}
			set
			{
				this.ViewState["CascadeFromField"] = value;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00008A57 File Offset: 0x00006C57
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00008A77 File Offset: 0x00006C77
		[DefaultValue("")]
		public string CascadeFromParentField
		{
			get
			{
				return (string)(this.ViewState["CascadeFromParentField"] ?? "");
			}
			set
			{
				this.ViewState["CascadeFromParentField"] = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00008A8A File Offset: 0x00006C8A
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00008AAA File Offset: 0x00006CAA
		[DefaultValue("")]
		public string GroupByField
		{
			get
			{
				return (string)(this.ViewState["GroupByField"] ?? "");
			}
			set
			{
				this.ViewState["GroupByField"] = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00008ABD File Offset: 0x00006CBD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MultiColumnComboBoxColumnsCollection ColumnsCollection
		{
			get
			{
				if (this._columns == null)
				{
					this._columns = new MultiColumnComboBoxColumnsCollection();
				}
				return this._columns;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00008AD8 File Offset: 0x00006CD8
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00008AF9 File Offset: 0x00006CF9
		[Category("Behavior")]
		[Bindable(false)]
		[DefaultValue(false)]
		public bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? false);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00008B11 File Offset: 0x00006D11
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00008B32 File Offset: 0x00006D32
		[DefaultValue(true)]
		public bool ClearButton
		{
			get
			{
				return (bool)(this.ViewState["ClearButton"] ?? true);
			}
			set
			{
				this.ViewState["ClearButton"] = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00008B4A File Offset: 0x00006D4A
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00008B6A File Offset: 0x00006D6A
		[DefaultValue("")]
		public string DataTextField
		{
			get
			{
				return (string)(this.ViewState["DataTextField"] ?? "");
			}
			set
			{
				this.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00008B7D File Offset: 0x00006D7D
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00008B9D File Offset: 0x00006D9D
		[DefaultValue("")]
		public string DataValueField
		{
			get
			{
				return (string)(this.ViewState["DataValueField"] ?? "");
			}
			set
			{
				this.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00008BB0 File Offset: 0x00006DB0
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00008BD9 File Offset: 0x00006DD9
		[DefaultValue(200.0)]
		public double Delay
		{
			get
			{
				return (double)(this.ViewState["Delay"] ?? 200.0);
			}
			set
			{
				this.ViewState["Delay"] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00008BF1 File Offset: 0x00006DF1
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00008C11 File Offset: 0x00006E11
		[DefaultValue("")]
		public string DropDownWidth
		{
			get
			{
				return (string)(this.ViewState["DropDownWidth"] ?? "");
			}
			set
			{
				this.ViewState["DropDownWidth"] = value;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00008C24 File Offset: 0x00006E24
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00008C45 File Offset: 0x00006E45
		[DefaultValue(true)]
		public bool Enable
		{
			get
			{
				return (bool)(this.ViewState["Enable"] ?? true);
			}
			set
			{
				this.ViewState["Enable"] = value;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00008C5D File Offset: 0x00006E5D
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00008C7E File Offset: 0x00006E7E
		[DefaultValue(false)]
		public bool EnforceMinLength
		{
			get
			{
				return (bool)(this.ViewState["EnforceMinLength"] ?? false);
			}
			set
			{
				this.ViewState["EnforceMinLength"] = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00008C96 File Offset: 0x00006E96
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00008CB6 File Offset: 0x00006EB6
		[DefaultValue("none")]
		public string Filter
		{
			get
			{
				return (string)(this.ViewState["Filter"] ?? "none");
			}
			set
			{
				this.ViewState["Filter"] = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00008CC9 File Offset: 0x00006EC9
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00008CE5 File Offset: 0x00006EE5
		[DefaultValue(null)]
		public string FilterFields
		{
			get
			{
				return (string)(this.ViewState["FilterFields"] ?? null);
			}
			set
			{
				this.ViewState["FilterFields"] = value;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00008CF8 File Offset: 0x00006EF8
		// (set) Token: 0x060003AB RID: 939 RVA: 0x00008D18 File Offset: 0x00006F18
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(true)]
		[DefaultValue("")]
		[Browsable(true)]
		public string FixedGroupTemplate
		{
			get
			{
				return (string)(this.ViewState["FixedGroupTemplate"] ?? "");
			}
			set
			{
				this.ViewState["FixedGroupTemplate"] = value;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00008D2B File Offset: 0x00006F2B
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00008D4B File Offset: 0x00006F4B
		[Bindable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[DefaultValue("")]
		public string FooterTemplate
		{
			get
			{
				return (string)(this.ViewState["FooterTemplate"] ?? "");
			}
			set
			{
				this.ViewState["FooterTemplate"] = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00008D5E File Offset: 0x00006F5E
		// (set) Token: 0x060003AF RID: 943 RVA: 0x00008D7E File Offset: 0x00006F7E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue("")]
		public string GroupTemplate
		{
			get
			{
				return (string)(this.ViewState["GroupTemplate"] ?? "");
			}
			set
			{
				this.ViewState["GroupTemplate"] = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00008D91 File Offset: 0x00006F91
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00008DC5 File Offset: 0x00006FC5
		[Category("Appearance")]
		[Description("The height of the suggestion popup in pixels. The default value is 200 pixels.")]
		[ClientControlProperty]
		[DefaultValue("200px")]
		[ClientPropertyName("height")]
		public override Unit Height
		{
			get
			{
				if (this.ViewState["Height"] != null)
				{
					return (Unit)this.ViewState["Height"];
				}
				return Unit.Pixel(200);
			}
			set
			{
				this.ViewState["Height"] = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00008DDD File Offset: 0x00006FDD
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00008DFE File Offset: 0x00006FFE
		[DefaultValue(true)]
		public bool HighlightFirst
		{
			get
			{
				return (bool)(this.ViewState["HighlightFirst"] ?? true);
			}
			set
			{
				this.ViewState["HighlightFirst"] = value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00008E16 File Offset: 0x00007016
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00008E37 File Offset: 0x00007037
		[DefaultValue(true)]
		public bool IgnoreCase
		{
			get
			{
				return (bool)(this.ViewState["IgnoreCase"] ?? true);
			}
			set
			{
				this.ViewState["IgnoreCase"] = value;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00008E4F File Offset: 0x0000704F
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00008E78 File Offset: 0x00007078
		[DefaultValue(-1.0)]
		public double Index
		{
			get
			{
				return (double)(this.ViewState["Index"] ?? -1.0);
			}
			set
			{
				this.ViewState["Index"] = value;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00008E90 File Offset: 0x00007090
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00008EB9 File Offset: 0x000070B9
		[DefaultValue(1.0)]
		public double MinLength
		{
			get
			{
				return (double)(this.ViewState["MinLength"] ?? 1.0);
			}
			set
			{
				this.ViewState["MinLength"] = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060003BA RID: 954 RVA: 0x00008ED1 File Offset: 0x000070D1
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00008EF1 File Offset: 0x000070F1
		[Browsable(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Bindable(true)]
		[DefaultValue("NO DATA FOUND.")]
		public string NoDataTemplate
		{
			get
			{
				return (string)(this.ViewState["NoDataTemplate"] ?? "NO DATA FOUND.");
			}
			set
			{
				this.ViewState["NoDataTemplate"] = value;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00008F04 File Offset: 0x00007104
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00008F24 File Offset: 0x00007124
		[DefaultValue("")]
		public string Placeholder
		{
			get
			{
				return (string)(this.ViewState["Placeholder"] ?? "");
			}
			set
			{
				this.ViewState["Placeholder"] = value;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00008F37 File Offset: 0x00007137
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Messages MessagesSettings
		{
			get
			{
				if (this._messages == null)
				{
					this._messages = new Messages();
				}
				return this._messages;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00008F52 File Offset: 0x00007152
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Popup PopupSettings
		{
			get
			{
				if (this._popup == null)
				{
					this._popup = new Popup();
				}
				return this._popup;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00008F6D File Offset: 0x0000716D
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00008F8E File Offset: 0x0000718E
		[DefaultValue(false)]
		public bool Suggest
		{
			get
			{
				return (bool)(this.ViewState["Suggest"] ?? false);
			}
			set
			{
				this.ViewState["Suggest"] = value;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00008FA6 File Offset: 0x000071A6
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00008FC7 File Offset: 0x000071C7
		[DefaultValue(true)]
		public bool SyncValueAndText
		{
			get
			{
				return (bool)(this.ViewState["SyncValueAndText"] ?? true);
			}
			set
			{
				this.ViewState["SyncValueAndText"] = value;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00008FDF File Offset: 0x000071DF
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00008FFF File Offset: 0x000071FF
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(true)]
		[Bindable(true)]
		public string HeaderTemplate
		{
			get
			{
				return (string)(this.ViewState["HeaderTemplate"] ?? "");
			}
			set
			{
				this.ViewState["HeaderTemplate"] = value;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00009012 File Offset: 0x00007212
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00009032 File Offset: 0x00007232
		[Browsable(false)]
		[Bindable(true, BindingDirection.TwoWay)]
		[Description("The text of the MultiColumnComboBox Selected Item")]
		[DefaultValue("")]
		[Category("Setup")]
		public string Text
		{
			get
			{
				return (string)(this.ViewState["Text"] ?? "");
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00009045 File Offset: 0x00007245
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00009065 File Offset: 0x00007265
		[DefaultValue("")]
		[Bindable(true, BindingDirection.TwoWay)]
		[Browsable(false)]
		[Category("Setup")]
		[Description("The value of the MultiColumnComboBox Selected Item")]
		public string Value
		{
			get
			{
				return (string)(this.ViewState["Value"] ?? "");
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00009078 File Offset: 0x00007278
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00009099 File Offset: 0x00007299
		[DefaultValue(false)]
		public bool ValuePrimitive
		{
			get
			{
				return (bool)(this.ViewState["ValuePrimitive"] ?? false);
			}
			set
			{
				this.ViewState["ValuePrimitive"] = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000090B1 File Offset: 0x000072B1
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000090D2 File Offset: 0x000072D2
		[DefaultValue(false)]
		public bool Virtual
		{
			get
			{
				return (bool)(this.ViewState["Virtual"] ?? false);
			}
			set
			{
				this.ViewState["Virtual"] = value;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000090EA File Offset: 0x000072EA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Virtual VirtualSettings
		{
			get
			{
				if (this._virtual == null)
				{
					this._virtual = new Virtual();
				}
				return this._virtual;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00009105 File Offset: 0x00007305
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public MultiColumnComboBoxClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new MultiColumnComboBoxClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00009120 File Offset: 0x00007320
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00009149 File Offset: 0x00007349
		[Category("Behavior")]
		[Description("Gets or sets value indicating whether server-side paging is enabled")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool EnableServerPaging
		{
			get
			{
				object obj = this.ViewState["EnableServerPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableServerPaging"] = value;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00009164 File Offset: 0x00007364
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000918D File Offset: 0x0000738D
		[Category("Behavior")]
		[Description("Gets or sets value indicating whether server-side filtering is enabled")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool EnableServerFiltering
		{
			get
			{
				object obj = this.ViewState["EnableServerFiltering"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableServerFiltering"] = value;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000091A8 File Offset: 0x000073A8
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x000091D2 File Offset: 0x000073D2
		[NotifyParentProperty(true)]
		[SimplePersistenceSetting]
		[DefaultValue(10)]
		[Description("Gets or sets the maximum number of items that would appear in a page")]
		[Category("Behavior")]
		public virtual int PageSize
		{
			get
			{
				object obj = this.ViewState["PageSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["PageSize"] = value;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000091FC File Offset: 0x000073FC
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00009225 File Offset: 0x00007425
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the paging in RadClientDataSource is enabled")]
		[DefaultValue(false)]
		public virtual bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000923D File Offset: 0x0000743D
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("The web service settings to be used for binding this instance of RadMultiColumnComboBox.")]
		public WebServiceDataSourceSettings WebServiceSettings
		{
			get
			{
				if (this._webServiceSettings == null)
				{
					this._webServiceSettings = new WebServiceDataSourceSettings();
				}
				return this._webServiceSettings;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00009258 File Offset: 0x00007458
		[Description("The items of the dropdownlist")]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual MultiColumnComboBoxItemCollection Items
		{
			get
			{
				if (this._itemsCollection == null)
				{
					this._itemsCollection = new MultiColumnComboBoxItemCollection(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._itemsCollection).TrackViewState();
					}
				}
				return this._itemsCollection;
			}
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00009287 File Offset: 0x00007487
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00009290 File Offset: 0x00007490
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "cascade", this.ClientEvents.OnCascade);
			RadDataBoundControl.DescribeEvent(descriptor, "change", this.ClientEvents.OnChange);
			RadDataBoundControl.DescribeEvent(descriptor, "close", this.ClientEvents.OnClose);
			RadDataBoundControl.DescribeEvent(descriptor, "dataBound", this.ClientEvents.OnDataBound);
			RadDataBoundControl.DescribeEvent(descriptor, "filtering", this.ClientEvents.OnFiltering);
			RadDataBoundControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			RadDataBoundControl.DescribeEvent(descriptor, "open", this.ClientEvents.OnOpen);
			RadDataBoundControl.DescribeEvent(descriptor, "select", this.ClientEvents.OnSelect);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000936A File Offset: 0x0000756A
		public RadMultiColumnComboBox()
		{
			this.RegisterJSConverters();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000939C File Offset: 0x0000759C
		private void ParseModelFieldsDefaultValues()
		{
			if (this.Schema != null && this.Schema.Model != null && this.Schema.Model.Fields != null)
			{
				foreach (object obj in this.Schema.Model.Fields)
				{
					ClientDataSourceModelField clientDataSourceModelField = (ClientDataSourceModelField)obj;
					if (clientDataSourceModelField.DefaultValue != null && clientDataSourceModelField.DefaultValue.ToString() != string.Empty)
					{
						object defaultValue = clientDataSourceModelField.DefaultValue;
						switch (clientDataSourceModelField.DataType)
						{
						case ClientDataSourceModelFieldType.Number:
							clientDataSourceModelField.DefaultValue = Convert.ToDecimal(defaultValue);
							continue;
						case ClientDataSourceModelFieldType.Boolean:
							clientDataSourceModelField.DefaultValue = Convert.ToBoolean(defaultValue);
							continue;
						case ClientDataSourceModelFieldType.Date:
							clientDataSourceModelField.DefaultValue = Convert.ToDateTime(defaultValue);
							continue;
						}
						clientDataSourceModelField.DefaultValue = defaultValue;
					}
				}
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003DE RID: 990 RVA: 0x000094B8 File Offset: 0x000076B8
		// (set) Token: 0x060003DF RID: 991 RVA: 0x000094CF File Offset: 0x000076CF
		internal string SerializedDataSource
		{
			get
			{
				return (string)this.ViewState["SerializedDataSource"];
			}
			set
			{
				this.ViewState["SerializedDataSource"] = value;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x000094E2 File Offset: 0x000076E2
		private string DecodeText(string text)
		{
			if (text != null)
			{
				text = HttpUtility.UrlDecode(text).Replace("&squote", "'");
			}
			return text;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000094FF File Offset: 0x000076FF
		protected virtual void BindItems(IEnumerable<MultiColumnComboBoxItem> items)
		{
			this.Items.Clear();
			this.Items.AddRange(items);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00009518 File Offset: 0x00007718
		protected virtual void DescribeItems(IScriptDescriptor descriptor)
		{
			IList<MultiColumnComboBoxItem> allItems = this.GetAllItems();
			if (this.SerializedDataSource != null)
			{
				descriptor.AddScriptProperty("itemsData", this.SerializedDataSource);
				return;
			}
			if (allItems.Count > 0)
			{
				descriptor.AddScriptProperty("itemsData", this.serializer.Serialize(allItems));
				return;
			}
			descriptor.AddScriptProperty("itemsData", "[]");
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00009577 File Offset: 0x00007777
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00009597 File Offset: 0x00007797
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets the name of the validation group to which this validation control belongs.")]
		[Bindable(true)]
		public virtual string ValidationGroup
		{
			get
			{
				return (string)(this.ViewState["ValidationGroup"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000095AA File Offset: 0x000077AA
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x000095CA File Offset: 0x000077CA
		[DefaultValue("")]
		[Themeable(false)]
		[Category("Behavior")]
		[UrlProperty("*.aspx")]
		public virtual string PostBackUrl
		{
			get
			{
				return (string)(this.ViewState["PostBackUrl"] ?? "");
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000095DD File Offset: 0x000077DD
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000095FE File Offset: 0x000077FE
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Whether the control causes validation to fire.")]
		public virtual bool CausesValidation
		{
			get
			{
				return (bool)(this.ViewState["CausesValidation"] ?? true);
			}
			set
			{
				this.ViewState["CausesValidation"] = value;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00009616 File Offset: 0x00007816
		protected override void RaisePostDataChangedEvent()
		{
			if (this.AutoPostBack)
			{
				this.PerformValidation();
			}
			this.OnSelectedIndexChanged();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000962C File Offset: 0x0000782C
		private void PerformValidation()
		{
			if (!this.CausesValidation)
			{
				return;
			}
			this.Page.Validate(this.ValidationGroup);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00009648 File Offset: 0x00007848
		internal virtual bool RequiresValidation()
		{
			return this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00009670 File Offset: 0x00007870
		internal PostBackOptions GetPostBackOptions(Control control, string argument, string validationGroup, string postBackUrl)
		{
			PostBackOptions postBackOptions = new PostBackOptions(control, argument)
			{
				ClientSubmit = true
			};
			if (this.Page != null)
			{
				if (this.RequiresValidation())
				{
					postBackOptions.PerformValidation = true;
					postBackOptions.ValidationGroup = validationGroup;
				}
				if (!string.IsNullOrEmpty(postBackUrl))
				{
					postBackOptions.ActionUrl = postBackUrl;
				}
			}
			return postBackOptions;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000096C0 File Offset: 0x000078C0
		internal PostBackOptions GetPostBackOptions(Control control, string argument)
		{
			string postBackUrl = string.Empty;
			if (!string.IsNullOrEmpty(this.PostBackUrl))
			{
				postBackUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
			}
			return this.GetPostBackOptions(control, argument, this.ValidationGroup, postBackUrl);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00009704 File Offset: 0x00007904
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(this, "arguments"));
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00009740 File Offset: 0x00007940
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
			if (this.AutoPostBack)
			{
				descriptor.AddProperty("_postBackReference", this.GetPostbackEventReference());
			}
			this.DescribeItems(descriptor);
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			if (!string.IsNullOrEmpty(this.ClientDataSourceID))
			{
				try
				{
					Control control = DataSourceControlHelper.FindControl(this, this.ClientDataSourceID);
					descriptor.AddProperty("clientDataSourceID", control.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("clientDataSourceID", this.ClientDataSourceID);
				}
			}
			if (!string.IsNullOrEmpty(this.CascadeFrom))
			{
				try
				{
					Control control2 = CascadeFromControlHelper.FindControl(this, this.CascadeFrom);
					descriptor.AddProperty("cascadeFromClientID", control2.ClientID);
				}
				catch (Exception)
				{
					descriptor.AddProperty("cascadeFromClientID", this.CascadeFrom);
				}
			}
			descriptor.AddProperty("groupByField", this.GroupByField);
			if (this.EnableServerFiltering)
			{
				descriptor.AddProperty("_enableServerFiltering", this.EnableServerFiltering);
			}
			if (this.EnableServerPaging)
			{
				descriptor.AddProperty("_enableServerPaging", this.EnableServerPaging);
			}
			if (this.PageSize != 10)
			{
				descriptor.AddProperty("_pageSize", this.PageSize);
			}
			if (this.AllowPaging)
			{
				descriptor.AddProperty("_allowPaging", this.AllowPaging);
			}
			string text = this.serializer.Serialize(this.WebServiceSettings);
			if (text != "{}")
			{
				descriptor.AddProperty("transport", text);
			}
			descriptor.AddScriptProperty("_schema", this.serializer.Serialize(this.Schema));
			if (base.Attributes.Count > 0)
			{
				descriptor.AddScriptProperty("attributes", this.serializer.Serialize(base.Attributes));
			}
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000994C File Offset: 0x00007B4C
		protected override void PerformDataBinding(IEnumerable data)
		{
			if (data == null && this.DataSource == null)
			{
				return;
			}
			this.Items.Clear();
			ControlDataBinder controlDataBinder = new ControlDataBinder(this);
			controlDataBinder.BindToEnumerableData(data);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00009980 File Offset: 0x00007B80
		internal void AddSerializedData(StringBuilder sb, PropertyDescriptorCollection props, object dataItem, bool isXmlDataSource = false)
		{
			foreach (object obj in props)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				string name = propertyDescriptor.Name;
				sb.Append('"');
				sb.Append(name);
				sb.Append("\":");
				object obj2 = DataBinder.Eval(dataItem, name);
				if (obj2 != null && !obj2.GetType().IsArray)
				{
					object propertyValue = DataBinder.GetPropertyValue(dataItem, name);
					if (propertyValue is string && !isXmlDataSource)
					{
						sb.AppendFormat("\"{0}\",", propertyValue);
					}
					else
					{
						string value = string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
						{
							propertyValue
						});
						if (obj2 is DateTime || obj2 is DateTime?)
						{
							sb.Append(HtmlChartHelper.GetSerializedValueField(value, true)).Append(",");
						}
						else
						{
							sb.Append(HtmlChartHelper.GetSerializedValueField(value, false)).Append(",");
						}
					}
				}
				else
				{
					sb.AppendFormat("{0},", this.serializer.Serialize(obj2));
				}
			}
			HtmlChartHelper.RemoveEndingComma(sb);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00009AD0 File Offset: 0x00007CD0
		private void RenderDesignTimeHtml(HtmlTextWriter writer)
		{
			this.Skin = "Default";
			writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			writer.Write("        <div class='DesignTimeDropDownGrid' style='width: 160px; white-space: normal;'>\r\n                <table summary='multicolumncombobox' style='border-width: 0; border-collapse: collapse; width: 100%'>\r\n                    <tbody>\r\n                        <tr>\r\n                            <td class='rmccbInputCell rmccbInputCellLeft' style='width: 100%;'>\r\n                                <input type='text' class='rmccbInput' value='Item 1' /></td>\r\n                            <td class='rmccbArrowCell rmccbArrowCellRight'><a style='overflow: hidden; display: block; position: relative; outline: none;'></a></td>\r\n                        </tr>\r\n                    </tbody>\r\n                </table>\r\n            </div>");
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00009AF4 File Offset: 0x00007CF4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			base.RenderContents(writer);
			if (base.DesignMode)
			{
				this.RenderDesignTimeHtml(writer);
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00009B13 File Offset: 0x00007D13
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			ScriptObjectBuilder.RegisterCssReferences(this);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00009B24 File Offset: 0x00007D24
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadMultiColumnComboBoxConverter(),
				new CloseConverter(),
				new OpenConverter(),
				new AnimationConverter(),
				new MultiColumnComboBoxColumnConverter(),
				new MessagesConverter(),
				new PopupConverter(),
				new VirtualConverter(),
				new ClientDataSourceJavaScriptConverter(),
				new AttributeCollectionConverter(),
				new MultiColumnComboBoxItemConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00009BBE File Offset: 0x00007DBE
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00009BC2 File Offset: 0x00007DC2
		protected override string CssClassFormatString
		{
			get
			{
				return "RadMultiColumnComboBox RadMultiColumnComboBox_{0}";
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00009BCC File Offset: 0x00007DCC
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			if (array[num] == null)
			{
				this.Items.Clear();
				return;
			}
			((IStateManager)this.Items).LoadViewState(array[num++]);
			((IStateManager)this.AnimationSettings).LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			((IStateManager)this.ColumnsCollection).LoadViewState(array[num++]);
			((IStateManager)this.MessagesSettings).LoadViewState(array[num++]);
			((IStateManager)this.PopupSettings).LoadViewState(array[num++]);
			((IStateManager)this.VirtualSettings).LoadViewState(array[num++]);
			((IStateManager)this.WebServiceSettings).LoadViewState(array[num++]);
			((IStateManager)this.Schema).LoadViewState(array[num++]);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00009CA4 File Offset: 0x00007EA4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Items).SaveViewState(),
				((IStateManager)this.AnimationSettings).SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.ColumnsCollection).SaveViewState(),
				((IStateManager)this.MessagesSettings).SaveViewState(),
				((IStateManager)this.PopupSettings).SaveViewState(),
				((IStateManager)this.VirtualSettings).SaveViewState(),
				((IStateManager)this.WebServiceSettings).SaveViewState(),
				((IStateManager)this.Schema).SaveViewState()
			};
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00009D44 File Offset: 0x00007F44
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Items).TrackViewState();
			((IStateManager)this.AnimationSettings).TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.ColumnsCollection).TrackViewState();
			((IStateManager)this.MessagesSettings).TrackViewState();
			((IStateManager)this.PopupSettings).TrackViewState();
			((IStateManager)this.VirtualSettings).TrackViewState();
			((IStateManager)this.WebServiceSettings).TrackViewState();
			((IStateManager)this.Schema).TrackViewState();
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00009DBA File Offset: 0x00007FBA
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x00009DDB File Offset: 0x00007FDB
		[Description("Comma delimited list of data-field Names")]
		[TypeConverter(typeof(ListConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Category("Data")]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public virtual string[] DataKeyNames
		{
			get
			{
				return (string[])(this.ViewState["DataKeyNames"] ?? new string[0]);
			}
			set
			{
				this.ViewState["DataKeyNames"] = value;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00009DEE File Offset: 0x00007FEE
		IItem IItemContainer.CreateItem()
		{
			return new MultiColumnComboBoxItem(this);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00009E1C File Offset: 0x0000801C
		void IItemContainer.RaiseItemDataBound(IItem item)
		{
			RadMultiColumnComboBoxItemEventHandler radMultiColumnComboBoxItemEventHandler = (RadMultiColumnComboBoxItemEventHandler)base.Events[RadMultiColumnComboBox.MultiColumnComboBoxItemDataBoundEvent];
			MultiColumnComboBoxItem multiColumnComboBoxItem = item as MultiColumnComboBoxItem;
			multiColumnComboBoxItem.TemplateData = new Dictionary<string, object>();
			List<string> list = (from x in this.ColumnsCollection.OfType<MultiColumnComboBoxColumn>()
			select x.Field into x
			where !x.ToLower().StartsWith("attributes.")
			select x).Concat((from x in this.DataKeyNames
			select x.Trim()).ToList<string>()).ToList<string>();
			if (this.DataTextField != null && this.DataTextField.Trim().Length > 0)
			{
				list.Add(this.DataTextField.Trim());
			}
			if (this.DataValueField != null && this.DataValueField.Trim().Length > 0)
			{
				list.Add(this.DataValueField.Trim());
			}
			IEnumerable<string> enumerable = list.Distinct<string>();
			foreach (string text in enumerable)
			{
				try
				{
					object value = DataBinder.Eval(multiColumnComboBoxItem.DataItem, text);
					multiColumnComboBoxItem.TemplateData.Add(text, value);
				}
				catch (Exception)
				{
					throw new Exception("The data item does not contain the " + text + " data field");
				}
			}
			RadMultiColumnComboBoxItemEventArgs e = new RadMultiColumnComboBoxItemEventArgs(multiColumnComboBoxItem);
			if (radMultiColumnComboBoxItemEventHandler != null)
			{
				radMultiColumnComboBoxItemEventHandler(this, e);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x00009FCC File Offset: 0x000081CC
		IList IItemContainer.Children
		{
			get
			{
				return this.Items;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00009FD4 File Offset: 0x000081D4
		public IList<MultiColumnComboBoxItem> GetAllItems()
		{
			return this.Items.ToList();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00009FE4 File Offset: 0x000081E4
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this._oldText = this.Text;
			this._oldValue = this.Value;
			string text = this.Text;
			string value = this.Value;
			string text2 = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text2))
			{
				return false;
			}
			RadMultiColumnComboBoxClientState radMultiColumnComboBoxClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				radMultiColumnComboBoxClientState = javaScriptSerializer.Deserialize<RadMultiColumnComboBoxClientState>(text2);
				radMultiColumnComboBoxClientState.Text = this.DecodeText(radMultiColumnComboBoxClientState.Text);
				radMultiColumnComboBoxClientState.Value = this.DecodeText(radMultiColumnComboBoxClientState.Value);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (radMultiColumnComboBoxClientState == null)
			{
				return false;
			}
			this.LoadClientState(radMultiColumnComboBoxClientState);
			return this.Text != this._oldText;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000A0A8 File Offset: 0x000082A8
		private void LoadClientState(RadMultiColumnComboBoxClientState clientState)
		{
			this.Enabled = clientState.Enabled;
			this.Text = clientState.Text;
			this.Value = clientState.Value;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000A0CE File Offset: 0x000082CE
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000A0D7 File Offset: 0x000082D7
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A0DC File Offset: 0x000082DC
		string ICallbackEventHandler.GetCallbackResult()
		{
			string a = HttpContext.Current.Request["__CALLBACKPARAM"];
			if (a == "loadChartData")
			{
				if (this.dataBindData == null)
				{
					this.DataBind();
				}
				StringBuilder stringBuilder = new StringBuilder();
				HtmlChartHelper.RemoveEndingComma(stringBuilder);
				return stringBuilder.ToString();
			}
			return "";
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A132 File Offset: 0x00008332
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000A134 File Offset: 0x00008334
		// Note: this type is marked as 'beforefieldinit'.
		static RadMultiColumnComboBox()
		{
			RadMultiColumnComboBox.SelectedIndexChangedEvent = new object();
		}

		// Token: 0x04000066 RID: 102
		private static readonly object MultiColumnComboBoxItemDataBoundEvent = new object();

		// Token: 0x04000068 RID: 104
		private string _oldText = string.Empty;

		// Token: 0x04000069 RID: 105
		private string _oldValue = string.Empty;

		// Token: 0x0400006A RID: 106
		protected StringBuilder dataBindData;

		// Token: 0x0400006B RID: 107
		private ClientDataSourceSchema _schema;

		// Token: 0x0400006C RID: 108
		private Animation _animation;

		// Token: 0x0400006D RID: 109
		private MultiColumnComboBoxColumnsCollection _columns;

		// Token: 0x0400006E RID: 110
		private Messages _messages;

		// Token: 0x0400006F RID: 111
		private Popup _popup;

		// Token: 0x04000070 RID: 112
		private Virtual _virtual;

		// Token: 0x04000071 RID: 113
		private MultiColumnComboBoxClientEvents _clientEvents;

		// Token: 0x04000072 RID: 114
		private WebServiceDataSourceSettings _webServiceSettings;

		// Token: 0x04000073 RID: 115
		private MultiColumnComboBoxItemCollection _itemsCollection;

		// Token: 0x04000074 RID: 116
		private readonly AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer();
	}
}
