using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.ButtonRendering;

namespace Telerik.Web.UI
{
	// Token: 0x020000DC RID: 220
	[EmbeddedSkin("Button")]
	[EmbeddedSkin("Button", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadButton", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[SupportsEventValidation]
	[ToolboxData("<{0}:RadButton runat=\"server\" Text=\"RadButton\"></{0}:RadButton>")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	[LightweightRendering]
	[TelerikToolboxCategory("Navigation")]
	[ToolboxBitmap(typeof(RadButton), "Telerik.Web.UI.Button.png")]
	[Designer("Telerik.Web.Design.RadButtonDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadButton : RadWebControl, IButtonControl, IPostBackEventHandler, ICheckBoxControl, INamingContainer
	{
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0001F210 File Offset: 0x0001D410
		[Description("Gets or sets the name of the JavaScript function that will be called when the RadButton is loaded on the page.")]
		[ClientPropertyName("load")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001F223 File Offset: 0x0001D423
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0001F243 File Offset: 0x0001D443
		[ClientControlEvent]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("clicking")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the RadButton is clicked. The event is cancelable.")]
		public string OnClientClicking
		{
			get
			{
				return ((string)this.ViewState["OnClientClicking"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientClicking"] = value;
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001F256 File Offset: 0x0001D456
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0001F276 File Offset: 0x0001D476
		[ClientPropertyName("clicked")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the RadButton is clicked, after the OnClientClicking event.")]
		public string OnClientClicked
		{
			get
			{
				return ((string)this.ViewState["OnClientClicked"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientClicked"] = value;
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x0001F289 File Offset: 0x0001D489
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x0001F2A9 File Offset: 0x0001D4A9
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("mouseOver")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the mouse pointer hovers over the RadButton.")]
		[DefaultValue("")]
		public string OnClientMouseOver
		{
			get
			{
				return ((string)this.ViewState["OnClientMouseOver"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientMouseOver"] = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x0001F2BC File Offset: 0x0001D4BC
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x0001F2DC File Offset: 0x0001D4DC
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the mouse pointer leaves the RadButton.")]
		[ClientPropertyName("mouseOut")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientMouseOut
		{
			get
			{
				return ((string)this.ViewState["OnClientMouseOut"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientMouseOut"] = value;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001F2EF File Offset: 0x0001D4EF
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x0001F30F File Offset: 0x0001D50F
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the Checked property of the RadButton control is about to be changed.")]
		[ClientPropertyName("checkedChanging")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientCheckedChanging
		{
			get
			{
				return ((string)this.ViewState["OnClientCheckedChanging"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCheckedChanging"] = value;
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001F322 File Offset: 0x0001D522
		// (set) Token: 0x0600086D RID: 2157 RVA: 0x0001F342 File Offset: 0x0001D542
		[ClientPropertyName("checkedChanged")]
		[Description("Gets or sets the name of the JavaScript function that will be called after the Checked property of the RadButton control is changed.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnClientCheckedChanged
		{
			get
			{
				return ((string)this.ViewState["OnClientCheckedChanged"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientCheckedChanged"] = value;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x0600086E RID: 2158 RVA: 0x0001F355 File Offset: 0x0001D555
		// (set) Token: 0x0600086F RID: 2159 RVA: 0x0001F375 File Offset: 0x0001D575
		[ClientPropertyName("toggleStateChanging")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the SelectedToggleStateIndex property of the RadButton control is about to be changed.")]
		public string OnClientToggleStateChanging
		{
			get
			{
				return ((string)this.ViewState["OnClientToggleStateChanging"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientToggleStateChanging"] = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0001F388 File Offset: 0x0001D588
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x0001F3A8 File Offset: 0x0001D5A8
		[ClientPropertyName("toggleStateChanged")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called after the SelectedToggleStateIndex property of the RadButton control is changed.")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientToggleStateChanged
		{
			get
			{
				return ((string)this.ViewState["OnClientToggleStateChanged"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientToggleStateChanged"] = value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000872 RID: 2162 RVA: 0x0001F3BB File Offset: 0x0001D5BB
		// (remove) Token: 0x06000873 RID: 2163 RVA: 0x0001F3CE File Offset: 0x0001D5CE
		[Category("Action")]
		[Description("Fired when the RadButton control is clicked.")]
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(RadButton.eventClick, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadButton.eventClick, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000874 RID: 2164 RVA: 0x0001F3E1 File Offset: 0x0001D5E1
		// (remove) Token: 0x06000875 RID: 2165 RVA: 0x0001F3F4 File Offset: 0x0001D5F4
		[Category("Action")]
		[Description("Fired when the RadButton control is clicked.")]
		public event CommandEventHandler Command
		{
			add
			{
				base.Events.AddHandler(RadButton.eventCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadButton.eventCommand, value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000876 RID: 2166 RVA: 0x0001F407 File Offset: 0x0001D607
		// (remove) Token: 0x06000877 RID: 2167 RVA: 0x0001F41A File Offset: 0x0001D61A
		[Category("Action")]
		[Description("Fired when the value of the Checked property changes between posts to the server.")]
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(RadButton.eventCheckedChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadButton.eventCheckedChanged, value);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000878 RID: 2168 RVA: 0x0001F42D File Offset: 0x0001D62D
		// (remove) Token: 0x06000879 RID: 2169 RVA: 0x0001F440 File Offset: 0x0001D640
		[Category("Action")]
		[Description("Fired when the value of the SelectedToggleStateIndex property changes between posts to the server.")]
		public event ButtonToggleStateChangedEventHandler ToggleStateChanged
		{
			add
			{
				base.Events.AddHandler(RadButton.eventToggleStatechanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadButton.eventToggleStatechanged, value);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0001F453 File Offset: 0x0001D653
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0001F474 File Offset: 0x0001D674
		[ClientControlProperty]
		[ClientPropertyName("_causesValidation")]
		[DefaultValue(true)]
		[Themeable(false)]
		[Description("Gets or sets a value indicating whether validation is performed when the RadButton control is clicked.")]
		[Category("Behavior")]
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

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600087C RID: 2172 RVA: 0x0001F48C File Offset: 0x0001D68C
		// (set) Token: 0x0600087D RID: 2173 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		[Description("Gets or sets an optional parameter passed to the Command event along with the associated CommandName.")]
		[Themeable(false)]
		[ClientPropertyName("commandArgument")]
		[DefaultValue("")]
		[Bindable(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		public string CommandArgument
		{
			get
			{
				return ((string)this.ViewState["CommandArgument"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["CommandArgument"] = value;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x0600087E RID: 2174 RVA: 0x0001F4BF File Offset: 0x0001D6BF
		// (set) Token: 0x0600087F RID: 2175 RVA: 0x0001F4DF File Offset: 0x0001D6DF
		[ClientControlProperty]
		[Themeable(false)]
		[Category("Behavior")]
		[Description("Gets or sets the command name associated with the RadButton control that is passed to the Command event.")]
		[DefaultValue("")]
		[ClientPropertyName("commandName")]
		public string CommandName
		{
			get
			{
				return ((string)this.ViewState["CommandName"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["CommandName"] = value;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000880 RID: 2176 RVA: 0x0001F4F2 File Offset: 0x0001D6F2
		// (set) Token: 0x06000881 RID: 2177 RVA: 0x0001F512 File Offset: 0x0001D712
		[Themeable(false)]
		[UrlProperty("*.aspx")]
		[DefaultValue("")]
		[Description("Gets or sets the URL of the page to post to from the current page when the RadButton control is clicked.")]
		[Category("Behavior")]
		[Editor("System.Web.UI.Design.UrlEditor", typeof(UITypeEditor))]
		public string PostBackUrl
		{
			get
			{
				return ((string)this.ViewState["PostBackUrl"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["PostBackUrl"] = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000882 RID: 2178 RVA: 0x0001F525 File Offset: 0x0001D725
		// (set) Token: 0x06000883 RID: 2179 RVA: 0x0001F545 File Offset: 0x0001D745
		[ClientPropertyName("_validationGroup")]
		[Themeable(false)]
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets the group of controls for which the RadButton control causes validation when it posts back to the server.")]
		[ClientControlProperty]
		public virtual string ValidationGroup
		{
			get
			{
				return ((string)this.ViewState["ValidationGroup"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["ValidationGroup"] = value;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x0001F558 File Offset: 0x0001D758
		[DefaultValue(null)]
		[Description("Gets the object that controls the Primary and Secondary Icon related properties.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		public RadButtonIcon Icon
		{
			get
			{
				if (this._icon == null)
				{
					this._icon = new RadButtonIcon();
				}
				return this._icon;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0001F573 File Offset: 0x0001D773
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Description("Gets the object that controls the built-in confirmation dialog properties.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		public RadButtonConfirmSettings ConfirmSettings
		{
			get
			{
				if (this._confirmSettings == null)
				{
					this._confirmSettings = new RadButtonConfirmSettings();
				}
				return this._confirmSettings;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0001F58E File Offset: 0x0001D78E
		[DefaultValue(null)]
		[Description("Gets the object that control the Image properties. A RadButton control can be rendered as an ImageButton, or it can have a BackgroundImage.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MergableProperty(false)]
		public RadButtonImage Image
		{
			get
			{
				if (this._image == null)
				{
					this._image = new RadButtonImage();
				}
				return this._image;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x0001F5A9 File Offset: 0x0001D7A9
		[Editor("Telerik.Web.Design.ControlItemCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Themeable(false)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		[Description("Gets a collection of RadButtonToggleState objects that belong to the RadButton control.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public RadButtonToggleStateCollection ToggleStates
		{
			get
			{
				if (this._toggleStates == null)
				{
					this._toggleStates = new RadButtonToggleStateCollection(this);
				}
				return this._toggleStates;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0001F5C5 File Offset: 0x0001D7C5
		// (set) Token: 0x06000889 RID: 2185 RVA: 0x0001F5CD File Offset: 0x0001D7CD
		[Description("Gets or sets the template for the RadButton control.")]
		[TemplateContainer(typeof(RadButton))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
				this.ButtonType = RadButtonType.LinkButton;
				this.ClearTemplate();
				this.ApplyTemplate();
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0001F5E9 File Offset: 0x0001D7E9
		internal bool IsTemplateInitialized
		{
			get
			{
				this.EnsureChildControls();
				return this.ContentTemplate != null || this.Controls.Count > 0;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001F609 File Offset: 0x0001D809
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0001F62A File Offset: 0x0001D82A
		[Themeable(false)]
		[ClientPropertyName("autoPostBack")]
		[DefaultValue(true)]
		[Description("Gets or sets a bool value indicating whether the RadButton control automatically posts back to the server when clicked.")]
		[Category("Behavior")]
		[ClientControlProperty]
		public virtual bool AutoPostBack
		{
			get
			{
				return (bool)(this.ViewState["AutoPostBack"] ?? true);
			}
			set
			{
				this.ViewState["AutoPostBack"] = value;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x0001F642 File Offset: 0x0001D842
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x0001F662 File Offset: 0x0001D862
		[Description("Gets or sets the text displayed in the RadButton control.")]
		[Localizable(true)]
		[DefaultValue("")]
		[ClientControlProperty]
		[Category("Appearance")]
		[Bindable(true)]
		[ClientPropertyName("text")]
		public string Text
		{
			get
			{
				return ((string)this.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0001F675 File Offset: 0x0001D875
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0001F695 File Offset: 0x0001D895
		[ClientPropertyName("value")]
		[Description("Gets or sets an optional Value of the RadButton control.")]
		[ClientControlProperty]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Value
		{
			get
			{
				return ((string)this.ViewState["Value"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x0001F6A8 File Offset: 0x0001D8A8
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x0001F6C9 File Offset: 0x0001D8C9
		[Themeable(false)]
		[ClientPropertyName("readOnly")]
		[Description("Gets or sets a bool value indicating whether the RadButton control is in a read-only mode.")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get
			{
				return (bool)(this.ViewState["ReadOnly"] ?? false);
			}
			set
			{
				this.ViewState["ReadOnly"] = value;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001F6E1 File Offset: 0x0001D8E1
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0001F702 File Offset: 0x0001D902
		[Category("StandardButton")]
		[Themeable(false)]
		[Description("Gets or sets a value indicating whether the RadButton control uses the client browser's submit mechanism or the ASP.NET postback mechanism.")]
		[DefaultValue(true)]
		public virtual bool UseSubmitBehavior
		{
			get
			{
				return (bool)(this.ViewState["UseSubmitBehavior"] ?? true);
			}
			set
			{
				this.ViewState["UseSubmitBehavior"] = value;
			}
		}

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0001F71A File Offset: 0x0001D91A
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0001F73B File Offset: 0x0001D93B
		[Description("Gets or sets a bool value indicating whether the client browser's default styling will be applied to the RadButton control.")]
		[DefaultValue(false)]
		[Category("StandardButton")]
		public virtual bool EnableBrowserButtonStyle
		{
			get
			{
				return (bool)(this.ViewState["EnableBrowserButtonStyle"] ?? false);
			}
			set
			{
				this.ViewState["EnableBrowserButtonStyle"] = value;
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001F753 File Offset: 0x0001D953
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0001F773 File Offset: 0x0001D973
		[Category("LinkButton")]
		[TypeConverter(typeof(TargetConverter))]
		[DefaultValue("")]
		[ClientControlProperty]
		[ClientPropertyName("target")]
		[Description("Gets or sets the target window or frame in which to display the Web page content linked to when the RadButton control is clicked.")]
		public string Target
		{
			get
			{
				return ((string)this.ViewState["Target"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0001F786 File Offset: 0x0001D986
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x0001F7A6 File Offset: 0x0001D9A6
		[Bindable(true)]
		[ClientPropertyName("_navigateUrl")]
		[Category("LinkButton")]
		[DefaultValue("")]
		[Description("Gets or sets the URL to link to when the RadButton control is clicked.")]
		[UrlProperty]
		[ClientControlProperty]
		public string NavigateUrl
		{
			get
			{
				return (this.ViewState["NavigateUrl"] as string) ?? "";
			}
			set
			{
				this.ViewState["NavigateUrl"] = value;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0001F7B9 File Offset: 0x0001D9B9
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x0001F7D9 File Offset: 0x0001D9D9
		[CssClassProperty]
		[ClientControlProperty]
		[ClientPropertyName("hoveredCssClass")]
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the RadButton control when the mouse pointer is over the control.")]
		public virtual string HoveredCssClass
		{
			get
			{
				return (this.ViewState["HoveredCssClass"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001F7EC File Offset: 0x0001D9EC
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0001F80C File Offset: 0x0001DA0C
		[ClientPropertyName("pressedCssClass")]
		[DefaultValue("")]
		[CssClassProperty]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the RadButton control when the control is pressed.")]
		[ClientControlProperty]
		public virtual string PressedCssClass
		{
			get
			{
				return (this.ViewState["PressedCssClass"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["PressedCssClass"] = value;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001F81F File Offset: 0x0001DA1F
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x0001F83F File Offset: 0x0001DA3F
		[ClientPropertyName("readOnlyCssClass")]
		[ClientControlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the CSS class applied to the RadButton control when it is in ReadOnly mode.")]
		[CssClassProperty]
		[Category("Appearance")]
		public virtual string ReadOnlyCssClass
		{
			get
			{
				return (this.ViewState["ReadOnlyCssClass"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["ReadOnlyCssClass"] = value;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001F852 File Offset: 0x0001DA52
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0001F85A File Offset: 0x0001DA5A
		[ClientPropertyName("toolTip")]
		[Description("Gets or sets the text that will be displayed in the tooltip of the RadButton control when it is hovered.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		public override string ToolTip
		{
			get
			{
				return base.ToolTip;
			}
			set
			{
				base.ToolTip = value;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x0001F863 File Offset: 0x0001DA63
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x0001F86B File Offset: 0x0001DA6B
		[ClientControlProperty]
		[Description("Gets or sets the CSS class rendered by the RadButton control on the client.")]
		[DefaultValue("")]
		[ClientPropertyName("cssClass")]
		[NotifyParentProperty(true)]
		public override string CssClass
		{
			get
			{
				return base.CssClass;
			}
			set
			{
				base.CssClass = value;
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x0001F874 File Offset: 0x0001DA74
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x0001F894 File Offset: 0x0001DA94
		[DefaultValue("")]
		[CssClassProperty]
		[ClientPropertyName("disabledCssClass")]
		[Description("Gets or sets the CSS class applied to the RadButton control when it is in a disabled state.")]
		[Category("Appearance")]
		[ClientControlProperty]
		public virtual string DisabledButtonCssClass
		{
			get
			{
				return (this.ViewState["DisabledButtonCssClass"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["DisabledButtonCssClass"] = value;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x0001F8A7 File Offset: 0x0001DAA7
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x0001F8AF File Offset: 0x0001DAAF
		[NotifyParentProperty(true)]
		[ClientPropertyName("height")]
		[Description("Gets or sets the height of the RadButton control.")]
		[ClientControlProperty]
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001F8B8 File Offset: 0x0001DAB8
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x0001F8C0 File Offset: 0x0001DAC0
		[ClientControlProperty]
		[ClientPropertyName("width")]
		[Description("Gets or sets the width of the RadButton control.")]
		[NotifyParentProperty(true)]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x0001F8C9 File Offset: 0x0001DAC9
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x0001F8D1 File Offset: 0x0001DAD1
		[Description("Gets or sets the accessKey of the RadButton control.")]
		[NotifyParentProperty(true)]
		public override string AccessKey
		{
			get
			{
				return base.AccessKey;
			}
			set
			{
				base.AccessKey = value;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x0001F8DA File Offset: 0x0001DADA
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x0001F8FB File Offset: 0x0001DAFB
		[Description("Enables/Disables the 'Split Button' functionality.")]
		[ClientControlProperty]
		[Category("SplitButton")]
		[DefaultValue(false)]
		[ClientPropertyName("enableSplitButton")]
		public virtual bool EnableSplitButton
		{
			get
			{
				return (bool)(this.ViewState["EnableSplitButton"] ?? false);
			}
			set
			{
				this.ViewState["EnableSplitButton"] = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0001F913 File Offset: 0x0001DB13
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x0001F934 File Offset: 0x0001DB34
		[Category("SplitButton")]
		[DefaultValue(ButtonPosition.Right)]
		[Description("Gets or sets the position (relative to the RadButton's text) of the split button.")]
		public virtual ButtonPosition SplitButtonPosition
		{
			get
			{
				return (ButtonPosition)(this.ViewState["SplitButtonPosition"] ?? ButtonPosition.Right);
			}
			set
			{
				this.ViewState["SplitButtonPosition"] = value;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0001F94C File Offset: 0x0001DB4C
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0001F96C File Offset: 0x0001DB6C
		[Description("Gets or sets the CSS class applied to the SplitButton of the RadButton control.")]
		[Category("SplitButton")]
		[DefaultValue("")]
		public virtual string SplitButtonCssClass
		{
			get
			{
				return ((string)this.ViewState["SplitButtonCssClass"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["SplitButtonCssClass"] = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x0001F97F File Offset: 0x0001DB7F
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x0001F9AA File Offset: 0x0001DBAA
		[DefaultValue(RadButtonType.StandardButton)]
		[Description("Gets or sets the type of the button. RadButtonType: StandardButton(default), LinkButton and ToggleButton.")]
		[ClientControlProperty]
		[Themeable(false)]
		[Category("Behavior")]
		public virtual RadButtonType ButtonType
		{
			get
			{
				if (this.IsTemplateInitialized)
				{
					return RadButtonType.LinkButton;
				}
				return (RadButtonType)(this.ViewState["ButtonType"] ?? RadButtonType.StandardButton);
			}
			set
			{
				this.ViewState["ButtonType"] = value;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001F9C2 File Offset: 0x0001DBC2
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0001F9E3 File Offset: 0x0001DBE3
		[ClientControlProperty]
		[Description("Gets or sets the toggle type of the RadButton control. The Default is ButtonToggleType='None'.")]
		[Category("ToggleButton")]
		[DefaultValue(ButtonToggleType.None)]
		[Themeable(false)]
		public virtual ButtonToggleType ToggleType
		{
			get
			{
				return (ButtonToggleType)(this.ViewState["ToggleType"] ?? ButtonToggleType.None);
			}
			set
			{
				this.ViewState["ToggleType"] = value;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0001F9FB File Offset: 0x0001DBFB
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x0001FA1C File Offset: 0x0001DC1C
		[Bindable(true, BindingDirection.TwoWay)]
		[SimplePersistenceSetting]
		[ClientControlProperty]
		[ClientPropertyName("checked")]
		[DefaultValue(false)]
		[Description("Gets or sets a bool value indicating whether the RadButton control is checked.")]
		[Themeable(false)]
		[Category("ToggleButton")]
		public virtual bool Checked
		{
			get
			{
				return (bool)(this.ViewState["Checked"] ?? false);
			}
			set
			{
				this.ViewState["Checked"] = value;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x0001FA34 File Offset: 0x0001DC34
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x0001FA54 File Offset: 0x0001DC54
		[ClientPropertyName("groupName")]
		[Category("Behavior")]
		[DefaultValue("")]
		[Themeable(false)]
		[Description("Gets or sets the name of the group the RadButton control, configured as a radio button (ToggleType='Radio'), belongs to.")]
		[ClientControlProperty]
		public virtual string GroupName
		{
			get
			{
				return (this.ViewState["GroupName"] as string) ?? string.Empty;
			}
			set
			{
				this.ViewState["GroupName"] = value;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x0001FA68 File Offset: 0x0001DC68
		[ClientPropertyName("uniqueGroupName")]
		[ClientControlProperty]
		protected virtual string UniqueGroupName
		{
			get
			{
				if (this._uniqueGroupName == null)
				{
					string text = this.GroupName;
					string uniqueID = this.UniqueID;
					if (uniqueID != null)
					{
						int num = uniqueID.LastIndexOf(base.IdSeparator);
						if (num >= 0 && text.Length > 0)
						{
							text = uniqueID.Substring(0, num + 1) + text;
						}
					}
					this._uniqueGroupName = text;
				}
				return this._uniqueGroupName;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		[Browsable(false)]
		[Description("Gets the currently selected ToggleState of the RadButton control when used as a custom toggle button.")]
		[Themeable(false)]
		[Category("ToggleButton")]
		public virtual RadButtonToggleState SelectedToggleState
		{
			get
			{
				int selectedToggleStateIndex = this.SelectedToggleStateIndex;
				int count = this.ToggleStates.Count;
				if (selectedToggleStateIndex >= 0 && count != 0)
				{
					return this.ToggleStates[selectedToggleStateIndex];
				}
				return null;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0001FB00 File Offset: 0x0001DD00
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0001FD14 File Offset: 0x0001DF14
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SimplePersistenceSetting]
		[DefaultValue(0)]
		[Browsable(false)]
		[Description("Gets or sets the index of the currently selected ToggleState of the RadButton control, when used as a custom toggle button.")]
		[Themeable(false)]
		[Category("ToggleButton")]
		[ClientControlProperty]
		[ClientPropertyName("selectedToggleStateIndex")]
		public virtual int SelectedToggleStateIndex
		{
			get
			{
				int count = this.ToggleStates.Count;
				if (count <= 1 || (this.ToggleType != ButtonToggleType.CheckBox && this.ToggleType != ButtonToggleType.Radio))
				{
					bool flag = this.Icon.ShowPrimaryIcon;
					bool flag2 = this.Icon.ShowSecondaryIcon;
					for (int i = 0; i < count; i++)
					{
						RadButtonToggleState radButtonToggleState = this.ToggleStates[i];
						if (!string.IsNullOrEmpty(radButtonToggleState.PrimaryIconUrl) || !string.IsNullOrEmpty(radButtonToggleState.PrimaryIconCssClass))
						{
							flag = true;
						}
						if (!string.IsNullOrEmpty(radButtonToggleState.SecondaryIconUrl) || !string.IsNullOrEmpty(radButtonToggleState.SecondaryIconCssClass))
						{
							flag2 = true;
						}
						if (flag && flag2)
						{
							break;
						}
					}
					if (this.ToggleType != ButtonToggleType.None)
					{
						this.Icon.ShowPrimaryIcon = flag;
						this.Icon.ShowSecondaryIcon = flag2;
					}
					for (int j = 0; j < count; j++)
					{
						if (this.ToggleStates[j].Selected)
						{
							return j;
						}
					}
					if (count != 0)
					{
						this.ToggleStates[0].Selected = true;
					}
					return 0;
				}
				int num;
				int num2;
				if (!this.ToggleStates[1].Selected)
				{
					num = 0;
					num2 = 1;
				}
				else
				{
					num = 1;
					num2 = 0;
				}
				RadButtonToggleState radButtonToggleState2 = this.ToggleStates[num];
				RadButtonToggleState radButtonToggleState3 = this.ToggleStates[num2];
				this.Icon.ShowPrimaryIcon = (this.Icon.ShowPrimaryIcon || !string.IsNullOrEmpty(radButtonToggleState2.PrimaryIconUrl) || !string.IsNullOrEmpty(radButtonToggleState2.PrimaryIconCssClass) || !string.IsNullOrEmpty(radButtonToggleState3.PrimaryIconUrl) || !string.IsNullOrEmpty(radButtonToggleState3.PrimaryIconCssClass));
				this.Icon.ShowSecondaryIcon = (this.Icon.ShowSecondaryIcon || !string.IsNullOrEmpty(radButtonToggleState2.SecondaryIconUrl) || !string.IsNullOrEmpty(radButtonToggleState2.SecondaryIconCssClass) || !string.IsNullOrEmpty(radButtonToggleState3.SecondaryIconUrl) || !string.IsNullOrEmpty(radButtonToggleState3.SecondaryIconCssClass));
				if (num == 0)
				{
					this.ToggleStates[0].Selected = true;
				}
				if (this.Checked)
				{
					return num;
				}
				return num2;
			}
			set
			{
				int count = this.ToggleStates.Count;
				if (count != 0)
				{
					if (value < 0 || value >= count)
					{
						throw new ArgumentOutOfRangeException("value");
					}
					if (this.ToggleType == ButtonToggleType.CustomToggle)
					{
						this.ClearSelection();
						this.ToggleStates[value].Selected = true;
						return;
					}
				}
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x0001FD65 File Offset: 0x0001DF65
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x0001FD86 File Offset: 0x0001DF86
		[ClientPropertyName("singleClick")]
		[Description("Gets or sets a bool value indicating whether the RadButton control will be immediately disabled after the user has clicks it. (i.e. enables/disables 'Single Click' functionality)")]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Themeable(false)]
		[ClientControlProperty]
		public virtual bool SingleClick
		{
			get
			{
				return (bool)(this.ViewState["SingleClick"] ?? false);
			}
			set
			{
				this.ViewState["SingleClick"] = value;
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x0001FD9E File Offset: 0x0001DF9E
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x0001FDBE File Offset: 0x0001DFBE
		[Category("Appearance")]
		[Localizable(true)]
		[ClientControlProperty]
		[ClientPropertyName("singleClickText")]
		[DefaultValue("")]
		[Description("Gets or sets the text displayed in the RadButton control after the button is being clicked and disabled.")]
		[Bindable(true)]
		public string SingleClickText
		{
			get
			{
				return ((string)this.ViewState["SingleClickText"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["SingleClickText"] = value;
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0001FDD1 File Offset: 0x0001DFD1
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x0001FDF2 File Offset: 0x0001DFF2
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientPropertyName("enableAriaSupport")]
		[Description("When set to true enables support for WAI-ARIA")]
		[ClientControlProperty]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0001FE0A File Offset: 0x0001E00A
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0001FE2B File Offset: 0x0001E02B
		[Description("Gets/Sets the primary appearance of the button.")]
		[ClientPropertyName("primary")]
		[ClientControlProperty]
		[Category("Appearance")]
		[DefaultValue(false)]
		public bool Primary
		{
			get
			{
				return (bool)(this.ViewState["Primary"] ?? false);
			}
			set
			{
				this.ViewState["Primary"] = value;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0001FE44 File Offset: 0x0001E044
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			List<JavaScriptConverter> list = new List<JavaScriptConverter>();
			list.Add(new RadButtonIconConverter
			{
				ParentButton = this
			});
			list.Add(new RadButtonImageConverter
			{
				ParentButton = this
			});
			list.Add(new RadButtonToggleStateConverter
			{
				ParentButton = this
			});
			list.Add(new RadButtonConfirmSettingsConverter());
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(list);
			descriptor.AddProperty("uniqueID", this.UniqueID);
			descriptor.AddScriptProperty("_postBackReference", "\"" + this.GetPostbackEventReference() + "\"");
			descriptor.AddScriptProperty("iconData", javaScriptSerializer.Serialize(this.Icon));
			descriptor.AddScriptProperty("imageData", javaScriptSerializer.Serialize(this.Image));
			descriptor.AddScriptProperty("toggleStatesData", javaScriptSerializer.Serialize(this.ToggleStates));
			descriptor.AddScriptProperty("confirmSettings", javaScriptSerializer.Serialize(this.ConfirmSettings));
			descriptor.AddProperty("_accessKey", this.AccessKey);
			descriptor.AddProperty("_isImageButton", this.IsImageButton);
			descriptor.AddProperty("_hasImage", this.HasImage);
			descriptor.AddProperty("_hasIcon", this.HasIcon);
			descriptor.AddProperty("_isClientSubmit", this.IsClientSubmit);
			descriptor.AddProperty("_renderMode", this.ResolvedRenderMode);
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0001FFDC File Offset: 0x0001E1DC
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.Text = (string)clientState["text"];
			this.Value = ((clientState["value"] == null) ? string.Empty : clientState["value"].ToString());
			this.Checked = (bool)clientState["checked"];
			this.Target = (string)clientState["target"];
			this.NavigateUrl = (string)clientState["navigateUrl"];
			this.CommandName = (string)clientState["commandName"];
			this.CommandArgument = (string)clientState["commandArgument"];
			this.AutoPostBack = (bool)clientState["autoPostBack"];
			this.SelectedToggleStateIndex = (int)clientState["selectedToggleStateIndex"];
			this.ValidationGroup = (string)clientState["validationGroup"];
			this.ReadOnly = (bool)clientState["readOnly"];
			this.Primary = (bool)clientState["primary"];
			if (base.IsEnabled && clientState.ContainsKey("enabled"))
			{
				this.Enabled = (bool)clientState["enabled"];
			}
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x00020137 File Offset: 0x0001E337
		protected override void ControlPreRender()
		{
			base.ControlPreRender();
			if (!this.RegisterWithScriptManager)
			{
				this.Page.RegisterRequiresPostBack(this);
			}
			if (!base.ScriptManager.EnablePartialRendering)
			{
				this.Page.ClientScript.GetPostBackEventReference(this, string.Empty);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00020177 File Offset: 0x0001E377
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002017A File Offset: 0x0001E37A
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00020182 File Offset: 0x0001E382
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0002018F File Offset: 0x0001E38F
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002019C File Offset: 0x0001E39C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.OriginalEnabled = this.Enabled;
			this.Enabled = true;
			PostBackOptions postBackOptions = this.GetPostBackOptions();
			if (this.Page != null)
			{
				this.Page.ClientScript.RegisterForEventValidation(postBackOptions);
				this.Page.ClientScript.RegisterForEventValidation(this.UniqueID, "true");
				this.Page.ClientScript.RegisterForEventValidation(this.UniqueID, "");
			}
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			this.Renderer.AddAttributesToRender(writer);
			base.AddAttributesToRender(writer);
			this.AccessKey = accessKey;
			this.Enabled = this.OriginalEnabled;
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x0002024A File Offset: 0x0001E44A
		protected override void Render(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "static");
			}
			base.Render(writer);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x00020268 File Offset: 0x0001E468
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002027D File Offset: 0x0001E47D
		internal void RenderContentsBase(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00020286 File Offset: 0x0001E486
		internal bool InDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0002028E File Offset: 0x0001E48E
		internal bool IsButtonEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00020296 File Offset: 0x0001E496
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0002029E File Offset: 0x0001E49E
		internal bool OriginalEnabled
		{
			get
			{
				return this._originalEnabled;
			}
			set
			{
				this._originalEnabled = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000202A8 File Offset: 0x0001E4A8
		internal bool IsImageButton
		{
			get
			{
				if (this._isImageButton == null)
				{
					this._isImageButton = new bool?(this.Image.EnableImageButton && !string.IsNullOrEmpty(this.Image.ImageUrl) && !this.Image.IsBackgroundImage && !this.HasIconInState);
				}
				return this._isImageButton.Value;
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00020310 File Offset: 0x0001E510
		internal bool HasImage
		{
			get
			{
				if (this._hasImage == null)
				{
					bool flag = !string.IsNullOrEmpty(this.Image.ImageUrl) || !string.IsNullOrEmpty(this.CssClass);
					this._hasImage = new bool?((this.Image.EnableImageButton && flag) || this.HasImageInState);
				}
				return this._hasImage.Value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00020380 File Offset: 0x0001E580
		internal bool HasImageInState
		{
			get
			{
				if (this._hasImageInState == null)
				{
					bool flag = false;
					if (this.ToggleType != ButtonToggleType.None)
					{
						foreach (object obj in this.ToggleStates)
						{
							RadButtonToggleState radButtonToggleState = (RadButtonToggleState)obj;
							flag |= !string.IsNullOrEmpty(radButtonToggleState.ImageUrl);
						}
					}
					this._hasImageInState = new bool?(flag);
				}
				return this._hasImageInState.Value;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00020414 File Offset: 0x0001E614
		internal bool HasBackgroundImage
		{
			get
			{
				if (this._hasBackgroundImage == null)
				{
					bool flag = this.Image.EnableImageButton && !string.IsNullOrEmpty(this.Image.ImageUrl) && this.Image.IsBackgroundImage;
					if (!flag && this.ToggleType != ButtonToggleType.None)
					{
						foreach (object obj in this.ToggleStates)
						{
							RadButtonToggleState radButtonToggleState = (RadButtonToggleState)obj;
							flag |= (radButtonToggleState.IsBackgroundImage && !string.IsNullOrEmpty(radButtonToggleState.ImageUrl));
						}
					}
					this._hasBackgroundImage = new bool?(flag);
				}
				return this._hasBackgroundImage.Value;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x000204E4 File Offset: 0x0001E6E4
		internal bool HasIcon
		{
			get
			{
				if (this._hasIcon == null)
				{
					this._hasIcon = new bool?(!this.IsTemplateInitialized && (this.Icon.ShowPrimaryIcon || this.Icon.ShowSecondaryIcon || this.HasIconInState));
				}
				return this._hasIcon.Value;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00020544 File Offset: 0x0001E744
		internal bool HasIconInState
		{
			get
			{
				if (this._hasIconInState == null)
				{
					bool flag = false;
					if (this.ToggleType != ButtonToggleType.None)
					{
						foreach (object obj in this.ToggleStates)
						{
							RadButtonToggleState radButtonToggleState = (RadButtonToggleState)obj;
							flag |= (!string.IsNullOrEmpty(radButtonToggleState.PrimaryIconCssClass) || !string.IsNullOrEmpty(radButtonToggleState.SecondaryIconCssClass) || !string.IsNullOrEmpty(radButtonToggleState.PrimaryIconUrl) || !string.IsNullOrEmpty(radButtonToggleState.SecondaryIconUrl));
						}
					}
					this._hasIconInState = new bool?(flag);
				}
				return this._hasIconInState.Value;
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00020604 File Offset: 0x0001E804
		internal bool HasStateWithPrimaryIcon
		{
			get
			{
				foreach (object obj in this.ToggleStates)
				{
					RadButtonToggleState radButtonToggleState = (RadButtonToggleState)obj;
					this._hasStateWithPrimaryIcon |= (!string.IsNullOrEmpty(radButtonToggleState.PrimaryIconCssClass) || !string.IsNullOrEmpty(radButtonToggleState.PrimaryIconUrl));
					if (this._hasStateWithPrimaryIcon)
					{
						break;
					}
				}
				return this._hasStateWithPrimaryIcon;
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00020690 File Offset: 0x0001E890
		internal bool HasStateWithSecondaryIcon
		{
			get
			{
				foreach (object obj in this.ToggleStates)
				{
					RadButtonToggleState radButtonToggleState = (RadButtonToggleState)obj;
					this._hasStateWithSecondaryIcon |= (!string.IsNullOrEmpty(radButtonToggleState.SecondaryIconCssClass) || !string.IsNullOrEmpty(radButtonToggleState.SecondaryIconUrl));
					if (this._hasStateWithSecondaryIcon)
					{
						break;
					}
				}
				return this._hasStateWithSecondaryIcon;
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0002071C File Offset: 0x0001E91C
		private void ClearTemplate()
		{
			this.Controls.Clear();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00020729 File Offset: 0x0001E929
		private void ApplyTemplate()
		{
			if (this._contentTemplate != null)
			{
				this._contentTemplate.InstantiateIn(this);
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00020740 File Offset: 0x0001E940
		protected virtual string GetPostbackEventReference()
		{
			string postBackEventReference = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), false);
			if (postBackEventReference == null)
			{
				return string.Empty;
			}
			return postBackEventReference.Replace("\"", "'");
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002077E File Offset: 0x0001E97E
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00020786 File Offset: 0x0001E986
		internal bool IsClientSubmit { get; private set; }

		// Token: 0x060008E3 RID: 2275 RVA: 0x00020790 File Offset: 0x0001E990
		internal virtual PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this, string.Empty);
			postBackOptions.ClientSubmit = this.IsClientSubmit;
			if (this.EnableSplitButton)
			{
				postBackOptions.Argument = "RadButtonEventArguments";
			}
			if (this.Page != null)
			{
				this.IsClientSubmit = (!this.UseSubmitBehavior || this.SingleClick || this.ButtonType != RadButtonType.StandardButton || !string.IsNullOrEmpty(this.Image.ImageUrl) || this.Image.EnableImageButton);
				if (this.CausesValidation && this.Page.GetValidators(this.ValidationGroup).Count > 0)
				{
					postBackOptions.PerformValidation = true;
					if (RadAjaxManager.GetCurrent(this.Page) != null)
					{
						this.IsClientSubmit = true;
					}
					postBackOptions.ValidationGroup = this.ValidationGroup;
				}
				if (!string.IsNullOrEmpty(this.PostBackUrl))
				{
					postBackOptions.ActionUrl = HttpUtility.UrlPathEncode(base.ResolveClientUrl(this.PostBackUrl));
				}
				postBackOptions.ClientSubmit = this.IsClientSubmit;
			}
			return postBackOptions;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002088C File Offset: 0x0001EA8C
		protected virtual void OnClick(ButtonClickEventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadButton.eventClick];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x000208BC File Offset: 0x0001EABC
		protected virtual void OnCommand(ButtonCommandEventArgs e)
		{
			CommandEventHandler commandEventHandler = (CommandEventHandler)base.Events[RadButton.eventCommand];
			if (commandEventHandler != null)
			{
				commandEventHandler(this, e);
			}
			base.RaiseBubbleEvent(this, e);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x000208F4 File Offset: 0x0001EAF4
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[RadButton.eventCheckedChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x00020924 File Offset: 0x0001EB24
		protected virtual void OnToggleStateChanged(ButtonToggleStateChangedEventArgs e)
		{
			ButtonToggleStateChangedEventHandler buttonToggleStateChangedEventHandler = (ButtonToggleStateChangedEventHandler)base.Events[RadButton.eventToggleStatechanged];
			if (buttonToggleStateChangedEventHandler != null)
			{
				buttonToggleStateChangedEventHandler(this, e);
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00020954 File Offset: 0x0001EB54
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			if (!this.UseSubmitBehavior && this.Page != null)
			{
				this.Page.ClientScript.ValidateEvent(this.UniqueID, eventArgument);
			}
			bool isSplitButtonClick = eventArgument == "true";
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnClick(new ButtonClickEventArgs(isSplitButtonClick));
			this.OnCommand(new ButtonCommandEventArgs(this.CommandName, this.CommandArgument, isSplitButtonClick));
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x000209D1 File Offset: 0x0001EBD1
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000209DC File Offset: 0x0001EBDC
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = (postCollection[postDataKey] != null) ? postCollection[postDataKey] : postCollection[postDataKey + "_input"];
			string text2 = postCollection[base.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text2))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(text2) as Dictionary<string, object>;
				if (dictionary != null)
				{
					if (dictionary["checked"] != null)
					{
						this._checkedFlag = (bool)dictionary["checked"];
					}
					int num = 0;
					if (dictionary["selectedToggleStateIndex"] != null)
					{
						num = (int)dictionary["selectedToggleStateIndex"];
					}
					this._checkedFlag = (this._checkedFlag != this.Checked);
					this._toggleStateFlag = (num != this.SelectedToggleStateIndex);
					if (text != null && this.Page != null)
					{
						this.Page.ClientScript.ValidateEvent(postDataKey);
					}
					this.LoadClientState(dictionary);
				}
			}
			if (this.UseSubmitBehavior && text != null && this.Page != null)
			{
				this.Page.ClientScript.ValidateEvent(postDataKey);
				this.Page.RegisterRequiresRaiseEvent(this);
			}
			return this._checkedFlag || this._toggleStateFlag;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00020B10 File Offset: 0x0001ED10
		protected override void RaisePostDataChangedEvent()
		{
			if (this._checkedFlag)
			{
				this.OnCheckedChanged(EventArgs.Empty);
			}
			if (this._toggleStateFlag)
			{
				this.OnToggleStateChanged(new ButtonToggleStateChangedEventArgs(this.CommandName, this.CommandArgument, this.SelectedToggleStateIndex, this.SelectedToggleState));
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00020B50 File Offset: 0x0001ED50
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Icon).LoadViewState(array[1]);
			((IStateManager)this.Image).LoadViewState(array[2]);
			((IStateManager)this.ConfirmSettings).LoadViewState(array[3]);
			if (array[4] == null)
			{
				this.ToggleStates.Clear();
				return;
			}
			((IStateManager)this.ToggleStates).LoadViewState(array[4]);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00020BB8 File Offset: 0x0001EDB8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Icon).SaveViewState(),
				((IStateManager)this.Image).SaveViewState(),
				((IStateManager)this.ConfirmSettings).SaveViewState(),
				((IStateManager)this.ToggleStates).SaveViewState()
			};
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00020C10 File Offset: 0x0001EE10
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Icon).TrackViewState();
			((IStateManager)this.Image).TrackViewState();
			((IStateManager)this.ConfirmSettings).TrackViewState();
			((IStateManager)this.ToggleStates).TrackViewState();
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00020C44 File Offset: 0x0001EE44
		public virtual void ClearSelection()
		{
			int count = this.ToggleStates.Count;
			for (int i = 0; i < count; i++)
			{
				this.ToggleStates[i].Selected = false;
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00020C7C File Offset: 0x0001EE7C
		public virtual void SetSelectedToggleStateByValue(string value)
		{
			RadButtonToggleState radButtonToggleState = this.FindToggleStateByValue(value);
			if (radButtonToggleState != null)
			{
				radButtonToggleState.Selected = true;
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00020C9C File Offset: 0x0001EE9C
		public virtual void SetSelectedToggleStateByText(string text)
		{
			RadButtonToggleState radButtonToggleState = this.FindToggleStateByText(text);
			if (radButtonToggleState != null)
			{
				radButtonToggleState.Selected = true;
			}
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00020CBC File Offset: 0x0001EEBC
		public virtual RadButtonToggleState FindToggleStateByValue(string value)
		{
			for (int i = 0; i < this.ToggleStates.Count; i++)
			{
				if (this.ToggleStates[i].Value == value)
				{
					return this.ToggleStates[i];
				}
			}
			return null;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00020D08 File Offset: 0x0001EF08
		public virtual RadButtonToggleState FindToggleStateByText(string text)
		{
			for (int i = 0; i < this.ToggleStates.Count; i++)
			{
				if (this.ToggleStates[i].Text == text)
				{
					return this.ToggleStates[i];
				}
			}
			return null;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00020D54 File Offset: 0x0001EF54
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "autoPostBack", this.AutoPostBack, true);
			base.DescribeProperty<RadButtonType>(descriptor, "buttonType", this.ButtonType, RadButtonType.StandardButton);
			base.DescribeProperty<bool>(descriptor, "_causesValidation", this.CausesValidation, true);
			base.DescribeProperty<bool>(descriptor, "checked", this.Checked, false);
			base.DescribeProperty<string>(descriptor, "commandArgument", this.CommandArgument, "");
			base.DescribeProperty<string>(descriptor, "commandName", this.CommandName, "");
			base.DescribeProperty<string>(descriptor, "cssClass", this.CssClass, "");
			base.DescribeProperty<string>(descriptor, "disabledCssClass", this.DisabledButtonCssClass, "");
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableSplitButton", this.EnableSplitButton, false);
			base.DescribeProperty<string>(descriptor, "groupName", this.GroupName, "");
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "hoveredCssClass", this.HoveredCssClass, "");
			base.DescribeProperty<string>(descriptor, "_navigateUrl", base.ResolveClientUrl(this.NavigateUrl), "");
			base.DescribeProperty<string>(descriptor, "pressedCssClass", this.PressedCssClass, "");
			base.DescribeProperty<bool>(descriptor, "primary", this.Primary, false);
			base.DescribeProperty<bool>(descriptor, "readOnly", this.ReadOnly, false);
			base.DescribeProperty<string>(descriptor, "readOnlyCssClass", this.ReadOnlyCssClass, "");
			base.DescribeProperty<int>(descriptor, "selectedToggleStateIndex", this.SelectedToggleStateIndex, 0);
			base.DescribeProperty<bool>(descriptor, "singleClick", this.SingleClick, false);
			base.DescribeProperty<string>(descriptor, "singleClickText", this.SingleClickText, "");
			base.DescribeProperty<string>(descriptor, "target", this.Target, "");
			base.DescribeProperty<string>(descriptor, "text", this.Text, "");
			base.DescribeProperty<ButtonToggleType>(descriptor, "toggleType", this.ToggleType, ButtonToggleType.None);
			base.DescribeProperty<string>(descriptor, "toolTip", this.ToolTip, "");
			base.DescribeProperty<string>(descriptor, "uniqueGroupName", this.UniqueGroupName, null);
			base.DescribeProperty<string>(descriptor, "_validationGroup", this.ValidationGroup, "");
			base.DescribeProperty<string>(descriptor, "value", this.Value, "");
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00020FF4 File Offset: 0x0001F1F4
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "checkedChanged", this.OnClientCheckedChanged);
			RadWebControl.DescribeEvent(descriptor, "checkedChanging", this.OnClientCheckedChanging);
			RadWebControl.DescribeEvent(descriptor, "clicked", this.OnClientClicked);
			RadWebControl.DescribeEvent(descriptor, "clicking", this.OnClientClicking);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadWebControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			RadWebControl.DescribeEvent(descriptor, "toggleStateChanged", this.OnClientToggleStateChanged);
			RadWebControl.DescribeEvent(descriptor, "toggleStateChanging", this.OnClientToggleStateChanging);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040001FA RID: 506
		private RadButtonIcon _icon;

		// Token: 0x040001FB RID: 507
		private RadButtonConfirmSettings _confirmSettings;

		// Token: 0x040001FC RID: 508
		private RadButtonImage _image;

		// Token: 0x040001FD RID: 509
		private RadButtonToggleStateCollection _toggleStates;

		// Token: 0x040001FE RID: 510
		private string _uniqueGroupName;

		// Token: 0x040001FF RID: 511
		private bool _originalEnabled = true;

		// Token: 0x04000200 RID: 512
		private bool? _isImageButton = null;

		// Token: 0x04000201 RID: 513
		private bool? _hasImage = null;

		// Token: 0x04000202 RID: 514
		private bool? _hasImageInState = null;

		// Token: 0x04000203 RID: 515
		private bool? _hasBackgroundImage = null;

		// Token: 0x04000204 RID: 516
		private bool? _hasIcon = null;

		// Token: 0x04000205 RID: 517
		private bool? _hasIconInState = null;

		// Token: 0x04000206 RID: 518
		private bool _hasStateWithPrimaryIcon;

		// Token: 0x04000207 RID: 519
		private bool _hasStateWithSecondaryIcon;

		// Token: 0x04000208 RID: 520
		private ITemplate _contentTemplate;

		// Token: 0x04000209 RID: 521
		private static readonly object eventClick = new object();

		// Token: 0x0400020A RID: 522
		private static readonly object eventCommand = new object();

		// Token: 0x0400020B RID: 523
		private static readonly object eventCheckedChanged = new object();

		// Token: 0x0400020C RID: 524
		private static readonly object eventToggleStatechanged = new object();

		// Token: 0x0400020D RID: 525
		private bool _checkedFlag;

		// Token: 0x0400020E RID: 526
		private bool _toggleStateFlag;
	}
}
