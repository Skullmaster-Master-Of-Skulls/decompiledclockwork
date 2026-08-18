using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000A24 RID: 2596
	[ToolboxItem(false)]
	public class RadButtonToggleState : StateManager
	{
		// Token: 0x17002016 RID: 8214
		// (get) Token: 0x06006212 RID: 25106 RVA: 0x001724B6 File Offset: 0x001706B6
		// (set) Token: 0x06006213 RID: 25107 RVA: 0x001724D6 File Offset: 0x001706D6
		[DefaultValue("")]
		[Description("Gets or sets the text displayed in the RadButton control.")]
		[Category("Appearance")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				return ((string)base.ViewState["Text"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x17002017 RID: 8215
		// (get) Token: 0x06006214 RID: 25108 RVA: 0x001724E9 File Offset: 0x001706E9
		// (set) Token: 0x06006215 RID: 25109 RVA: 0x00172509 File Offset: 0x00170709
		[Description("Gets or sets optional Value.")]
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		public string Value
		{
			get
			{
				return ((string)base.ViewState["Value"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}

		// Token: 0x17002018 RID: 8216
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x0017251C File Offset: 0x0017071C
		// (set) Token: 0x06006217 RID: 25111 RVA: 0x0017253D File Offset: 0x0017073D
		[Category("Behavior")]
		[DefaultValue(false)]
		[Themeable(false)]
		[Description("Gets or sets a bool value indicating whether the ToggleState is selected or not.")]
		public bool Selected
		{
			get
			{
				return (bool)(base.ViewState["Selected"] ?? false);
			}
			set
			{
				if (value && this.Container != null)
				{
					this.Container.ClearSelection();
				}
				base.ViewState["Selected"] = value;
			}
		}

		// Token: 0x17002019 RID: 8217
		// (get) Token: 0x06006218 RID: 25112 RVA: 0x0017256B File Offset: 0x0017076B
		// (set) Token: 0x06006219 RID: 25113 RVA: 0x0017258B File Offset: 0x0017078B
		[DefaultValue("")]
		[Description("Gets or sets the CSS class applied to the RadButton control.")]
		[Category("Appearance")]
		[CssClassProperty]
		public string CssClass
		{
			get
			{
				return (base.ViewState["CssClass"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x1700201A RID: 8218
		// (get) Token: 0x0600621A RID: 25114 RVA: 0x0017259E File Offset: 0x0017079E
		// (set) Token: 0x0600621B RID: 25115 RVA: 0x001725BE File Offset: 0x001707BE
		[DefaultValue("")]
		[Description("Gets or sets the CSS class applied to the RadButton control when the mouse pointer is over the control.")]
		[CssClassProperty]
		[Category("Appearance")]
		public string HoveredCssClass
		{
			get
			{
				return (base.ViewState["HoveredCssClass"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x1700201B RID: 8219
		// (get) Token: 0x0600621C RID: 25116 RVA: 0x001725D1 File Offset: 0x001707D1
		// (set) Token: 0x0600621D RID: 25117 RVA: 0x001725F1 File Offset: 0x001707F1
		[CssClassProperty]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the RadButton control when the control is pressed.")]
		[DefaultValue("")]
		public string PressedCssClass
		{
			get
			{
				return (base.ViewState["PressedCssClass"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["PressedCssClass"] = value;
			}
		}

		// Token: 0x1700201C RID: 8220
		// (get) Token: 0x0600621E RID: 25118 RVA: 0x00172604 File Offset: 0x00170804
		// (set) Token: 0x0600621F RID: 25119 RVA: 0x00172629 File Offset: 0x00170829
		[Description("Gets or sets the width of the RadButton control.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700201D RID: 8221
		// (get) Token: 0x06006220 RID: 25120 RVA: 0x00172641 File Offset: 0x00170841
		// (set) Token: 0x06006221 RID: 25121 RVA: 0x00172666 File Offset: 0x00170866
		[Description("Gets or sets the height of the RadButton control.")]
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		public Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x1700201E RID: 8222
		// (get) Token: 0x06006222 RID: 25122 RVA: 0x0017267E File Offset: 0x0017087E
		// (set) Token: 0x06006223 RID: 25123 RVA: 0x0017269E File Offset: 0x0017089E
		[CssClassProperty]
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Gets or sets the CSS class applied to the Primary Icon.")]
		public string PrimaryIconCssClass
		{
			get
			{
				return (string)(base.ViewState["PrimaryIconCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconCssClass"] = value;
			}
		}

		// Token: 0x1700201F RID: 8223
		// (get) Token: 0x06006224 RID: 25124 RVA: 0x001726B1 File Offset: 0x001708B1
		// (set) Token: 0x06006225 RID: 25125 RVA: 0x001726D1 File Offset: 0x001708D1
		[DefaultValue("")]
		[Bindable(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL to the image used as Primary Icon.")]
		[UrlProperty]
		[Category("Appearance")]
		public string PrimaryIconUrl
		{
			get
			{
				return (string)(base.ViewState["PrimaryIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconUrl"] = value;
			}
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x06006226 RID: 25126 RVA: 0x001726E4 File Offset: 0x001708E4
		// (set) Token: 0x06006227 RID: 25127 RVA: 0x00172704 File Offset: 0x00170904
		[Category("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[UrlProperty]
		[Bindable(true)]
		[DefaultValue("")]
		[Description("Gets or sets the URL to the image showed when the Primary Icon is hovered.")]
		public string PrimaryHoveredIconUrl
		{
			get
			{
				return (string)(base.ViewState["PrimaryHoveredIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PrimaryHoveredIconUrl"] = value;
			}
		}

		// Token: 0x17002021 RID: 8225
		// (get) Token: 0x06006228 RID: 25128 RVA: 0x00172717 File Offset: 0x00170917
		// (set) Token: 0x06006229 RID: 25129 RVA: 0x00172737 File Offset: 0x00170937
		[Description("Gets or sets the URL to the image showed when the Primary Icon is pressed.")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Category("Appearance")]
		[DefaultValue("")]
		[Bindable(true)]
		public string PrimaryPressedIconUrl
		{
			get
			{
				return (string)(base.ViewState["PrimaryPressedIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PrimaryPressedIconUrl"] = value;
			}
		}

		// Token: 0x17002022 RID: 8226
		// (get) Token: 0x0600622A RID: 25130 RVA: 0x0017274A File Offset: 0x0017094A
		// (set) Token: 0x0600622B RID: 25131 RVA: 0x0017276F File Offset: 0x0017096F
		[Category("Layout")]
		[Description("Gets or sets the Height of the Primary Icon.")]
		[DefaultValue(typeof(Unit), "")]
		public Unit PrimaryIconHeight
		{
			get
			{
				return (Unit)(base.ViewState["PrimaryIconHeight"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconHeight"] = value;
			}
		}

		// Token: 0x17002023 RID: 8227
		// (get) Token: 0x0600622C RID: 25132 RVA: 0x00172787 File Offset: 0x00170987
		// (set) Token: 0x0600622D RID: 25133 RVA: 0x001727AC File Offset: 0x001709AC
		[Description("Gets or sets the Width of the Primary Icon.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit PrimaryIconWidth
		{
			get
			{
				return (Unit)(base.ViewState["PrimaryIconWidth"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconWidth"] = value;
			}
		}

		// Token: 0x17002024 RID: 8228
		// (get) Token: 0x0600622E RID: 25134 RVA: 0x001727C4 File Offset: 0x001709C4
		// (set) Token: 0x0600622F RID: 25135 RVA: 0x001727E9 File Offset: 0x001709E9
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the top edge of the Primary Icon, relative to the RadButton control's wrapper element.")]
		[Category("Layout")]
		public Unit PrimaryIconTop
		{
			get
			{
				return (Unit)(base.ViewState["PrimaryIconTop"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconTop"] = value;
			}
		}

		// Token: 0x17002025 RID: 8229
		// (get) Token: 0x06006230 RID: 25136 RVA: 0x00172801 File Offset: 0x00170A01
		// (set) Token: 0x06006231 RID: 25137 RVA: 0x00172826 File Offset: 0x00170A26
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the bottom edge of the Primary Icon, relative to the RadButton control's wrapper element.")]
		public Unit PrimaryIconBottom
		{
			get
			{
				return (Unit)(base.ViewState["PrimaryIconBottom"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconBottom"] = value;
			}
		}

		// Token: 0x17002026 RID: 8230
		// (get) Token: 0x06006232 RID: 25138 RVA: 0x0017283E File Offset: 0x00170A3E
		// (set) Token: 0x06006233 RID: 25139 RVA: 0x00172863 File Offset: 0x00170A63
		[Description("Gets or sets the left edge of the Primary Icon, relative to the RadButton control's wrapper element.")]
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		public Unit PrimaryIconLeft
		{
			get
			{
				return (Unit)(base.ViewState["PrimaryIconLeft"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconLeft"] = value;
			}
		}

		// Token: 0x17002027 RID: 8231
		// (get) Token: 0x06006234 RID: 25140 RVA: 0x0017287B File Offset: 0x00170A7B
		// (set) Token: 0x06006235 RID: 25141 RVA: 0x001728A0 File Offset: 0x00170AA0
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the right edge of the Primary Icon, relative to the RadButton control's wrapper element.")]
		public Unit PrimaryIconRight
		{
			get
			{
				return (Unit)(base.ViewState["PrimaryIconRight"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconRight"] = value;
			}
		}

		// Token: 0x17002028 RID: 8232
		// (get) Token: 0x06006236 RID: 25142 RVA: 0x001728B8 File Offset: 0x00170AB8
		// (set) Token: 0x06006237 RID: 25143 RVA: 0x001728D8 File Offset: 0x00170AD8
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Gets or sets the CSS class applied to the Secondary Icon.")]
		[CssClassProperty]
		public string SecondaryIconCssClass
		{
			get
			{
				return (string)(base.ViewState["SecondaryIconCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconCssClass"] = value;
			}
		}

		// Token: 0x17002029 RID: 8233
		// (get) Token: 0x06006238 RID: 25144 RVA: 0x001728EB File Offset: 0x00170AEB
		// (set) Token: 0x06006239 RID: 25145 RVA: 0x0017290B File Offset: 0x00170B0B
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL to the image used as Secondary Icon.")]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public string SecondaryIconUrl
		{
			get
			{
				return (string)(base.ViewState["SecondaryIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconUrl"] = value;
			}
		}

		// Token: 0x1700202A RID: 8234
		// (get) Token: 0x0600623A RID: 25146 RVA: 0x0017291E File Offset: 0x00170B1E
		// (set) Token: 0x0600623B RID: 25147 RVA: 0x0017293E File Offset: 0x00170B3E
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Gets or sets the URL to the image showed when the Secondary Icon is hovered.")]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public string SecondaryHoveredIconUrl
		{
			get
			{
				return (string)(base.ViewState["SecondaryHoveredIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SecondaryHoveredIconUrl"] = value;
			}
		}

		// Token: 0x1700202B RID: 8235
		// (get) Token: 0x0600623C RID: 25148 RVA: 0x00172951 File Offset: 0x00170B51
		// (set) Token: 0x0600623D RID: 25149 RVA: 0x00172971 File Offset: 0x00170B71
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL to the image showed when the Secondary Icon is pressed.")]
		[DefaultValue("")]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public string SecondaryPressedIconUrl
		{
			get
			{
				return (string)(base.ViewState["SecondaryPressedIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SecondaryPressedIconUrl"] = value;
			}
		}

		// Token: 0x1700202C RID: 8236
		// (get) Token: 0x0600623E RID: 25150 RVA: 0x00172984 File Offset: 0x00170B84
		// (set) Token: 0x0600623F RID: 25151 RVA: 0x001729A9 File Offset: 0x00170BA9
		[Category("Layout")]
		[Description("Gets or sets the Height of the Secondary Icon.")]
		[DefaultValue(typeof(Unit), "")]
		public Unit SecondaryIconHeight
		{
			get
			{
				return (Unit)(base.ViewState["SecondaryIconHeight"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconHeight"] = value;
			}
		}

		// Token: 0x1700202D RID: 8237
		// (get) Token: 0x06006240 RID: 25152 RVA: 0x001729C1 File Offset: 0x00170BC1
		// (set) Token: 0x06006241 RID: 25153 RVA: 0x001729E6 File Offset: 0x00170BE6
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the Width of the Secondary Icon.")]
		public Unit SecondaryIconWidth
		{
			get
			{
				return (Unit)(base.ViewState["SecondaryIconWidth"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconWidth"] = value;
			}
		}

		// Token: 0x1700202E RID: 8238
		// (get) Token: 0x06006242 RID: 25154 RVA: 0x001729FE File Offset: 0x00170BFE
		// (set) Token: 0x06006243 RID: 25155 RVA: 0x00172A23 File Offset: 0x00170C23
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the top edge of the Secondary Icon, relative to the RadButton control's wrapper element.")]
		public Unit SecondaryIconTop
		{
			get
			{
				return (Unit)(base.ViewState["SecondaryIconTop"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconTop"] = value;
			}
		}

		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x06006244 RID: 25156 RVA: 0x00172A3B File Offset: 0x00170C3B
		// (set) Token: 0x06006245 RID: 25157 RVA: 0x00172A60 File Offset: 0x00170C60
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the bottom edge of the Secondary Icon, relative to the RadButton control's wrapper element.")]
		public Unit SecondaryIconBottom
		{
			get
			{
				return (Unit)(base.ViewState["SecondaryIconBottom"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconBottom"] = value;
			}
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x06006246 RID: 25158 RVA: 0x00172A78 File Offset: 0x00170C78
		// (set) Token: 0x06006247 RID: 25159 RVA: 0x00172A9D File Offset: 0x00170C9D
		[Description("Gets or sets the left edge of the Secondary Icon, relative to the RadButton control's wrapper element.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public Unit SecondaryIconLeft
		{
			get
			{
				return (Unit)(base.ViewState["SecondaryIconLeft"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconLeft"] = value;
			}
		}

		// Token: 0x17002031 RID: 8241
		// (get) Token: 0x06006248 RID: 25160 RVA: 0x00172AB5 File Offset: 0x00170CB5
		// (set) Token: 0x06006249 RID: 25161 RVA: 0x00172ADA File Offset: 0x00170CDA
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the right edge of the Secondary Icon, relative to the RadButton control's wrapper element.")]
		public Unit SecondaryIconRight
		{
			get
			{
				return (Unit)(base.ViewState["SecondaryIconRight"] ?? Unit.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconRight"] = value;
			}
		}

		// Token: 0x17002032 RID: 8242
		// (get) Token: 0x0600624A RID: 25162 RVA: 0x00172AF2 File Offset: 0x00170CF2
		// (set) Token: 0x0600624B RID: 25163 RVA: 0x00172B13 File Offset: 0x00170D13
		[DefaultValue(false)]
		[Description("Gets or sets a bool value indicating how the Image is used - i.e. as a background image or as an Image Button.")]
		[Category("Behavior")]
		public bool IsBackgroundImage
		{
			get
			{
				return (bool)(base.ViewState["IsBackgroundImage"] ?? false);
			}
			set
			{
				base.ViewState["IsBackgroundImage"] = value;
			}
		}

		// Token: 0x17002033 RID: 8243
		// (get) Token: 0x0600624C RID: 25164 RVA: 0x00172B2B File Offset: 0x00170D2B
		// (set) Token: 0x0600624D RID: 25165 RVA: 0x00172B4B File Offset: 0x00170D4B
		[Category("Appearance")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Gets or sets the location of an image to display in the RadButton control.")]
		[Bindable(true)]
		public string ImageUrl
		{
			get
			{
				return (string)(base.ViewState["ImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
			}
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x0600624E RID: 25166 RVA: 0x00172B5E File Offset: 0x00170D5E
		// (set) Token: 0x0600624F RID: 25167 RVA: 0x00172B7E File Offset: 0x00170D7E
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Bindable(true)]
		[Category("Appearance")]
		[Description("Gets or sets the location of an image to display in the RadButton control, when the mouse pointer is over the control.")]
		[UrlProperty]
		public string HoveredImageUrl
		{
			get
			{
				return (string)(base.ViewState["HoveredImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["HoveredImageUrl"] = value;
			}
		}

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x06006250 RID: 25168 RVA: 0x00172B91 File Offset: 0x00170D91
		// (set) Token: 0x06006251 RID: 25169 RVA: 0x00172BB1 File Offset: 0x00170DB1
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the location of an image to display in the RadButton control, when the control is pressed.")]
		public string PressedImageUrl
		{
			get
			{
				return (string)(base.ViewState["PressedImageUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PressedImageUrl"] = value;
			}
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x00172BC4 File Offset: 0x00170DC4
		public RadButtonToggleState()
		{
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x00172BCC File Offset: 0x00170DCC
		public RadButtonToggleState(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x06006254 RID: 25172 RVA: 0x00172BDB File Offset: 0x00170DDB
		public RadButtonToggleState(string text, string cssClass) : this()
		{
			this.Text = text;
			this.CssClass = cssClass;
		}

		// Token: 0x06006255 RID: 25173 RVA: 0x00172BF1 File Offset: 0x00170DF1
		public RadButtonToggleState(string text, string cssClass, string value) : this()
		{
			this.Text = text;
			this.CssClass = cssClass;
			this.Value = value;
		}

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x06006256 RID: 25174 RVA: 0x00172C0E File Offset: 0x00170E0E
		// (set) Token: 0x06006257 RID: 25175 RVA: 0x00172C16 File Offset: 0x00170E16
		internal RadButton Container
		{
			get
			{
				return this._container;
			}
			set
			{
				this._container = value;
			}
		}

		// Token: 0x06006258 RID: 25176 RVA: 0x00172C20 File Offset: 0x00170E20
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState()
			};
		}

		// Token: 0x06006259 RID: 25177 RVA: 0x00172C40 File Offset: 0x00170E40
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
		}

		// Token: 0x0400180B RID: 6155
		private RadButton _container;
	}
}
