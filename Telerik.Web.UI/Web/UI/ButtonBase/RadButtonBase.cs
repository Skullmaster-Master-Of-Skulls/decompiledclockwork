using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.ButtonRendering;

namespace Telerik.Web.UI.ButtonBase
{
	// Token: 0x0200001A RID: 26
	[RequiredScript(typeof(MaterialRipple))]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[LightweightRendering]
	[SupportsEventValidation]
	[ClientScriptResource("Telerik.Web.UI.RadButtonBase", "Telerik.Web.UI.Button.RadButtonScripts.js")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[Browsable(false)]
	[RequiredScript(typeof(jQueryPlugins))]
	[DefaultProperty("Text")]
	[ToolboxBitmap(typeof(RadButtonBase), "Telerik.Web.UI.Button.png")]
	[EmbeddedSkin("Button")]
	[EmbeddedSkin("Button", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadButton))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadButton))]
	public abstract class RadButtonBase : RadWebControl, INamingContainer
	{
		// Token: 0x0600013B RID: 315 RVA: 0x00004228 File Offset: 0x00002428
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			base.DescribeRenderMode(descriptor);
			descriptor.AddProperty("uniqueID", this.UniqueID);
			descriptor.AddProperty("_accessKey", this.AccessKey);
			if (!base.IsEnabled)
			{
				descriptor.AddProperty("enabled", false);
			}
			this.AriaSettings.Describe(descriptor);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000428A File Offset: 0x0000248A
		protected internal override string GetSkinSuffix()
		{
			return RenderModeHelper.GetRenderingModeString(RenderMode.Lightweight);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004292 File Offset: 0x00002492
		protected override IRenderer CreateControlRenderer()
		{
			return RendererFactory.GetRenderer(this);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000429A File Offset: 0x0000249A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000042A7 File Offset: 0x000024A7
		protected override string CssClassFormatString
		{
			get
			{
				return string.Format("{0} {1}", this.ButtonName, this.Renderer.CssClassFormatString);
			}
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000042C4 File Offset: 0x000024C4
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.OriginalEnabled = this.Enabled;
			this.Enabled = true;
			string accessKey = this.AccessKey;
			this.AccessKey = string.Empty;
			this.Renderer.AddAttributesToRender(writer);
			base.AddAttributesToRender(writer);
			this.AccessKey = accessKey;
			this.Enabled = this.OriginalEnabled;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000431C File Offset: 0x0000251C
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000432A File Offset: 0x0000252A
		internal void RenderContentsBase(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004333 File Offset: 0x00002533
		internal bool InDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000433B File Offset: 0x0000253B
		internal bool IsButtonEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004343 File Offset: 0x00002543
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000434B File Offset: 0x0000254B
		internal bool OriginalEnabled { get; set; }

		// Token: 0x06000147 RID: 327 RVA: 0x00004354 File Offset: 0x00002554
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			this.Text = (string)clientState["text"];
			this.Value = (((string)clientState["value"]) ?? string.Empty);
			if (base.IsEnabled && clientState.ContainsKey("enabled"))
			{
				this.Enabled = (bool)clientState["enabled"];
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x000043C8 File Offset: 0x000025C8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.AriaSettings).LoadViewState(array[1]);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000043F8 File Offset: 0x000025F8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.AriaSettings).SaveViewState()
			};
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004426 File Offset: 0x00002626
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.AriaSettings).TrackViewState();
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600014B RID: 331
		public abstract string ButtonName { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004439 File Offset: 0x00002639
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000443C File Offset: 0x0000263C
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00004444 File Offset: 0x00002644
		[NotifyParentProperty(true)]
		[Description("Gets or sets the accessKey of the Button control.")]
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

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000444D File Offset: 0x0000264D
		// (set) Token: 0x06000150 RID: 336 RVA: 0x00004455 File Offset: 0x00002655
		[ClientControlProperty]
		[Description("Gets or sets the CSS class rendered by the Button control on the client.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[ClientPropertyName("cssClass")]
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

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000445E File Offset: 0x0000265E
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00004470 File Offset: 0x00002670
		[DefaultValue("")]
		[Description("Gets or sets the CSS class applied to the Button control when it is in a disabled state.")]
		[Category("Appearance")]
		[CssClassProperty]
		[ClientControlProperty]
		[ClientPropertyName("disabledCssClass")]
		public new string DisabledCssClass
		{
			get
			{
				return base.GetViewStateValue<string>("DisabledCssClass", string.Empty);
			}
			set
			{
				this.ViewState["DisabledCssClass"] = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00004483 File Offset: 0x00002683
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000044A4 File Offset: 0x000026A4
		[ClientPropertyName("enableAriaSupport")]
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("When set to true enables support for WAI-ARIA")]
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

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000044BC File Offset: 0x000026BC
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000044CE File Offset: 0x000026CE
		[DefaultValue("")]
		[Description("Gets or sets an optional Value of the Button control.")]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("value")]
		public string Value
		{
			get
			{
				return base.GetViewStateValue<string>("Value", string.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000044E1 File Offset: 0x000026E1
		// (set) Token: 0x06000158 RID: 344 RVA: 0x000044E9 File Offset: 0x000026E9
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[ClientPropertyName("width")]
		[Description("Gets or sets the width of the Button control.")]
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

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000044F2 File Offset: 0x000026F2
		// (set) Token: 0x0600015A RID: 346 RVA: 0x000044FA File Offset: 0x000026FA
		[ClientPropertyName("height")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Description("Gets or sets the height of the Button control.")]
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

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00004503 File Offset: 0x00002703
		// (set) Token: 0x0600015C RID: 348 RVA: 0x00004515 File Offset: 0x00002715
		[CssClassProperty]
		[ClientPropertyName("hoveredCssClass")]
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the Button control when the mouse pointer is over the control.")]
		[ClientControlProperty]
		public virtual string HoveredCssClass
		{
			get
			{
				return base.GetViewStateValue<string>("HoveredCssClass", string.Empty);
			}
			set
			{
				this.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004528 File Offset: 0x00002728
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000453A File Offset: 0x0000273A
		[DefaultValue("")]
		[ClientPropertyName("pressedCssClass")]
		[CssClassProperty]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the Button control when the control is pressed.")]
		[ClientControlProperty]
		public virtual string PressedCssClass
		{
			get
			{
				return base.GetViewStateValue<string>("PressedCssClass", string.Empty);
			}
			set
			{
				this.ViewState["PressedCssClass"] = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000454D File Offset: 0x0000274D
		// (set) Token: 0x06000160 RID: 352 RVA: 0x00004555 File Offset: 0x00002755
		[Description("Gets or sets the TabIndex of the Button control.")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[ClientPropertyName("tabIndex")]
		public override short TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000455E File Offset: 0x0000275E
		// (set) Token: 0x06000162 RID: 354 RVA: 0x00004570 File Offset: 0x00002770
		[DefaultValue("")]
		[ClientPropertyName("text")]
		[Description("Gets or sets the text displayed in the Button control.")]
		[Category("Appearance")]
		[Bindable(true)]
		[Localizable(true)]
		[ClientControlProperty]
		public string Text
		{
			get
			{
				return base.GetViewStateValue<string>("Text", string.Empty);
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004583 File Offset: 0x00002783
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000458B File Offset: 0x0000278B
		[ClientPropertyName("toolTip")]
		[Description("Gets or sets the text that will be displayed in the tooltip of the Button control when it is hovered.")]
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00004594 File Offset: 0x00002794
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000045B4 File Offset: 0x000027B4
		[DefaultValue("")]
		[ClientPropertyName("load")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the Button is loaded on the page.")]
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

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000045C7 File Offset: 0x000027C7
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000045E7 File Offset: 0x000027E7
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("clicking")]
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the Button is clicked. The event is cancelable.")]
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

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000045FA File Offset: 0x000027FA
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000461A File Offset: 0x0000281A
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the JavaScript function that will be called when the Button is clicked, after the OnClientClicking event.")]
		[ClientPropertyName("clicked")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000462D File Offset: 0x0000282D
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000464D File Offset: 0x0000284D
		[Description("Gets or sets the name of the JavaScript function that will be called when the mouse pointer hovers over the Button.")]
		[ClientPropertyName("mouseOver")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00004660 File Offset: 0x00002860
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00004680 File Offset: 0x00002880
		[ClientControlEvent]
		[Description("Gets or sets the name of the JavaScript function that will be called when the mouse pointer leaves the Button.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("mouseOut")]
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00004694 File Offset: 0x00002894
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[MergableProperty(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public WaiAriaSettings AriaSettings
		{
			get
			{
				WaiAriaSettings result;
				if ((result = this._ariaSettings) == null)
				{
					result = (this._ariaSettings = new WaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000046BC File Offset: 0x000028BC
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<string>(descriptor, "cssClass", this.CssClass, "");
			base.DescribeProperty<string>(descriptor, "disabledCssClass", this.DisabledCssClass, "");
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<string>(descriptor, "height", this.Height.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeProperty<string>(descriptor, "hoveredCssClass", this.HoveredCssClass, "");
			base.DescribeProperty<string>(descriptor, "pressedCssClass", this.PressedCssClass, "");
			base.DescribeProperty<short>(descriptor, "tabIndex", this.TabIndex, 0);
			base.DescribeProperty<string>(descriptor, "text", this.Text, "");
			base.DescribeProperty<string>(descriptor, "toolTip", this.ToolTip, "");
			base.DescribeProperty<string>(descriptor, "value", this.Value, "");
			base.DescribeProperty<string>(descriptor, "width", this.Width.ToString(CultureInfo.InvariantCulture), "");
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000047E0 File Offset: 0x000029E0
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "clicked", this.OnClientClicked);
			RadWebControl.DescribeEvent(descriptor, "clicking", this.OnClientClicking);
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "mouseOut", this.OnClientMouseOut);
			RadWebControl.DescribeEvent(descriptor, "mouseOver", this.OnClientMouseOver);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x04000017 RID: 23
		private WaiAriaSettings _ariaSettings;
	}
}
