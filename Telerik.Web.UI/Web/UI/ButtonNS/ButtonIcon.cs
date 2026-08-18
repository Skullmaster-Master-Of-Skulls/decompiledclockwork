using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ButtonNS
{
	// Token: 0x020000DD RID: 221
	[ToolboxItem(false)]
	public class ButtonIcon : StateManager
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x0002112E File Offset: 0x0001F32E
		// (set) Token: 0x060008F9 RID: 2297 RVA: 0x00021140 File Offset: 0x0001F340
		[DefaultValue("")]
		[CssClassProperty]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the Icon.")]
		public virtual string CssClass
		{
			get
			{
				return base.GetViewStateValue<string>("CssClass", string.Empty);
			}
			set
			{
				base.ViewState["CssClass"] = value;
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00021153 File Offset: 0x0001F353
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x00021165 File Offset: 0x0001F365
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the Height of the Icon.")]
		public virtual Unit Height
		{
			get
			{
				return base.GetViewStateValue<Unit>("Height", Unit.Empty);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0002117D File Offset: 0x0001F37D
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x0002118F File Offset: 0x0001F38F
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the Icon, when button is hovered.")]
		[CssClassProperty]
		public virtual string HoveredCssClass
		{
			get
			{
				return base.GetViewStateValue<string>("HoveredCssClass", string.Empty);
			}
			set
			{
				base.ViewState["HoveredCssClass"] = value;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x000211A2 File Offset: 0x0001F3A2
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x000211B4 File Offset: 0x0001F3B4
		[UrlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the URL to the image showed when the button is hovered.")]
		[Bindable(true)]
		[Category("Appearance")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		public virtual string HoveredUrl
		{
			get
			{
				return base.GetViewStateValue<string>("HoveredUrl", string.Empty);
			}
			set
			{
				base.ViewState["HoveredUrl"] = value;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x000211C7 File Offset: 0x0001F3C7
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x000211D9 File Offset: 0x0001F3D9
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the left edge of the Icon, relative to the Button control's wrapper element.")]
		[Category("Layout")]
		public virtual Unit Left
		{
			get
			{
				return base.GetViewStateValue<Unit>("Left", Unit.Empty);
			}
			set
			{
				base.ViewState["Left"] = value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x000211F1 File Offset: 0x0001F3F1
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x00021203 File Offset: 0x0001F403
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the Icon, when button is pressed.")]
		[CssClassProperty]
		public virtual string PressedCssClass
		{
			get
			{
				return base.GetViewStateValue<string>("PressedCssClass", string.Empty);
			}
			set
			{
				base.ViewState["PressedCssClass"] = value;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00021216 File Offset: 0x0001F416
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x00021228 File Offset: 0x0001F428
		[Bindable(true)]
		[Category("Appearance")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL to the image showed when the button is pressed.")]
		[DefaultValue("")]
		public virtual string PressedUrl
		{
			get
			{
				return base.GetViewStateValue<string>("PressedUrl", string.Empty);
			}
			set
			{
				base.ViewState["PressedUrl"] = value;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x0002123B File Offset: 0x0001F43B
		[Description("Gets or sets a bool value indicating whether the Button will show the Icon.")]
		[DefaultValue(false)]
		internal bool ShowIcon
		{
			get
			{
				return !string.IsNullOrEmpty(this.CssClass) || !string.IsNullOrEmpty(this.Url);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0002125A File Offset: 0x0001F45A
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0002126C File Offset: 0x0001F46C
		[Description("Gets or sets the top edge of the Icon, relative to the Button control's wrapper element.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public virtual Unit Top
		{
			get
			{
				return base.GetViewStateValue<Unit>("Top", Unit.Empty);
			}
			set
			{
				base.ViewState["Top"] = value;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00021284 File Offset: 0x0001F484
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x00021296 File Offset: 0x0001F496
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL to the image used as Icon.")]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public virtual string Url
		{
			get
			{
				return base.GetViewStateValue<string>("Url", string.Empty);
			}
			set
			{
				base.ViewState["Url"] = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x000212A9 File Offset: 0x0001F4A9
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x000212BB File Offset: 0x0001F4BB
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the Width of the Icon.")]
		public virtual Unit Width
		{
			get
			{
				return base.GetViewStateValue<Unit>("Width", Unit.Empty);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}
	}
}
