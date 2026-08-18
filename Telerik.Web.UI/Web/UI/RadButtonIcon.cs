using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000A27 RID: 2599
	[ToolboxItem(false)]
	public class RadButtonIcon : StateManager
	{
		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x06006264 RID: 25188 RVA: 0x00173152 File Offset: 0x00171352
		// (set) Token: 0x06006265 RID: 25189 RVA: 0x00173173 File Offset: 0x00171373
		[DefaultValue(false)]
		[Description("Gets or sets a bool value indicating whether the RadButton will show the Primary Icon.")]
		internal bool ShowPrimaryIcon
		{
			get
			{
				return (bool)(base.ViewState["ShowPrimaryIcon"] ?? false);
			}
			set
			{
				base.ViewState["ShowPrimaryIcon"] = value;
			}
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x06006266 RID: 25190 RVA: 0x0017318B File Offset: 0x0017138B
		// (set) Token: 0x06006267 RID: 25191 RVA: 0x001731AB File Offset: 0x001713AB
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Gets or sets the CSS class applied to the Primary Icon.")]
		[CssClassProperty]
		public virtual string PrimaryIconCssClass
		{
			get
			{
				return (string)(base.ViewState["PrimaryIconCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconCssClass"] = value;
				this.ShowPrimaryIcon = (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(this.PrimaryIconUrl));
			}
		}

		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x06006268 RID: 25192 RVA: 0x001731DD File Offset: 0x001713DD
		// (set) Token: 0x06006269 RID: 25193 RVA: 0x001731FD File Offset: 0x001713FD
		[DefaultValue("")]
		[Bindable(true)]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL to the image used as Primary Icon.")]
		[UrlProperty]
		[Category("Appearance")]
		public virtual string PrimaryIconUrl
		{
			get
			{
				return (string)(base.ViewState["PrimaryIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["PrimaryIconUrl"] = value;
				this.ShowPrimaryIcon = (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(this.PrimaryIconCssClass));
			}
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x0600626A RID: 25194 RVA: 0x0017322F File Offset: 0x0017142F
		// (set) Token: 0x0600626B RID: 25195 RVA: 0x0017324F File Offset: 0x0017144F
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL to the image showed when the Primary Icon is hovered.")]
		[DefaultValue("")]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public virtual string PrimaryHoveredIconUrl
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

		// Token: 0x1700203F RID: 8255
		// (get) Token: 0x0600626C RID: 25196 RVA: 0x00173262 File Offset: 0x00171462
		// (set) Token: 0x0600626D RID: 25197 RVA: 0x00173282 File Offset: 0x00171482
		[Category("Appearance")]
		[Description("Gets or sets the URL to the image showed when the Primary Icon is hovered.")]
		[Bindable(true)]
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		public virtual string PrimaryPressedIconUrl
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

		// Token: 0x17002040 RID: 8256
		// (get) Token: 0x0600626E RID: 25198 RVA: 0x00173295 File Offset: 0x00171495
		// (set) Token: 0x0600626F RID: 25199 RVA: 0x001732BA File Offset: 0x001714BA
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the Height of the Primary Icon.")]
		public virtual Unit PrimaryIconHeight
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

		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x06006270 RID: 25200 RVA: 0x001732D2 File Offset: 0x001714D2
		// (set) Token: 0x06006271 RID: 25201 RVA: 0x001732F7 File Offset: 0x001714F7
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the Width of the Primary Icon.")]
		public virtual Unit PrimaryIconWidth
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

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x06006272 RID: 25202 RVA: 0x0017330F File Offset: 0x0017150F
		// (set) Token: 0x06006273 RID: 25203 RVA: 0x00173334 File Offset: 0x00171534
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the top edge of the Primary Icon, relative to the RadButton control's wrapper element.")]
		public virtual Unit PrimaryIconTop
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

		// Token: 0x17002043 RID: 8259
		// (get) Token: 0x06006274 RID: 25204 RVA: 0x0017334C File Offset: 0x0017154C
		// (set) Token: 0x06006275 RID: 25205 RVA: 0x00173371 File Offset: 0x00171571
		[Description("Gets or sets the bottom edge of the Primary Icon, relative to the RadButton control's wrapper element.")]
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		public virtual Unit PrimaryIconBottom
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

		// Token: 0x17002044 RID: 8260
		// (get) Token: 0x06006276 RID: 25206 RVA: 0x00173389 File Offset: 0x00171589
		// (set) Token: 0x06006277 RID: 25207 RVA: 0x001733AE File Offset: 0x001715AE
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the left edge of the Primary Icon, relative to the RadButton control's wrapper element.")]
		public virtual Unit PrimaryIconLeft
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

		// Token: 0x17002045 RID: 8261
		// (get) Token: 0x06006278 RID: 25208 RVA: 0x001733C6 File Offset: 0x001715C6
		// (set) Token: 0x06006279 RID: 25209 RVA: 0x001733EB File Offset: 0x001715EB
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the right edge of the Primary Icon, relative to the RadButton control's wrapper element.")]
		public virtual Unit PrimaryIconRight
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

		// Token: 0x17002046 RID: 8262
		// (get) Token: 0x0600627A RID: 25210 RVA: 0x00173403 File Offset: 0x00171603
		// (set) Token: 0x0600627B RID: 25211 RVA: 0x00173424 File Offset: 0x00171624
		[Description("Gets or sets a bool value indicating whether the RadButton will show the Secondary Icon.")]
		[DefaultValue(false)]
		internal bool ShowSecondaryIcon
		{
			get
			{
				return (bool)(base.ViewState["ShowSecondaryIcon"] ?? false);
			}
			set
			{
				base.ViewState["ShowSecondaryIcon"] = value;
			}
		}

		// Token: 0x17002047 RID: 8263
		// (get) Token: 0x0600627C RID: 25212 RVA: 0x0017343C File Offset: 0x0017163C
		// (set) Token: 0x0600627D RID: 25213 RVA: 0x0017345C File Offset: 0x0017165C
		[CssClassProperty]
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Gets or sets the CSS class applied to the Secondary Icon.")]
		public virtual string SecondaryIconCssClass
		{
			get
			{
				return (string)(base.ViewState["SecondaryIconCssClass"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconCssClass"] = value;
				this.ShowSecondaryIcon = (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(this.SecondaryIconUrl));
			}
		}

		// Token: 0x17002048 RID: 8264
		// (get) Token: 0x0600627E RID: 25214 RVA: 0x0017348E File Offset: 0x0017168E
		// (set) Token: 0x0600627F RID: 25215 RVA: 0x001734AE File Offset: 0x001716AE
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Gets or sets the URL to the image used as Secondary Icon.")]
		[Bindable(true)]
		public virtual string SecondaryIconUrl
		{
			get
			{
				return (string)(base.ViewState["SecondaryIconUrl"] ?? string.Empty);
			}
			set
			{
				base.ViewState["SecondaryIconUrl"] = value;
				this.ShowSecondaryIcon = (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(this.SecondaryIconCssClass));
			}
		}

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x06006280 RID: 25216 RVA: 0x001734E0 File Offset: 0x001716E0
		// (set) Token: 0x06006281 RID: 25217 RVA: 0x00173500 File Offset: 0x00171700
		[DefaultValue("")]
		[Bindable(true)]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[Description("Gets or sets the URL to the image showed when the Secondary Icon is hovered.")]
		[Category("Appearance")]
		public virtual string SecondaryHoveredIconUrl
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

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x06006282 RID: 25218 RVA: 0x00173513 File Offset: 0x00171713
		// (set) Token: 0x06006283 RID: 25219 RVA: 0x00173533 File Offset: 0x00171733
		[Editor("System.Web.UI.Design.ImageUrlEditor", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Gets or sets the URL to the image showed when the Secondary Icon is pressed.")]
		[Bindable(true)]
		[UrlProperty]
		[Category("Appearance")]
		public virtual string SecondaryPressedIconUrl
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

		// Token: 0x1700204B RID: 8267
		// (get) Token: 0x06006284 RID: 25220 RVA: 0x00173546 File Offset: 0x00171746
		// (set) Token: 0x06006285 RID: 25221 RVA: 0x0017356B File Offset: 0x0017176B
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the Height of the Secondary Icon.")]
		public virtual Unit SecondaryIconHeight
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

		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x06006286 RID: 25222 RVA: 0x00173583 File Offset: 0x00171783
		// (set) Token: 0x06006287 RID: 25223 RVA: 0x001735A8 File Offset: 0x001717A8
		[Description("Gets or sets the Width of the Secondary Icon.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public virtual Unit SecondaryIconWidth
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

		// Token: 0x1700204D RID: 8269
		// (get) Token: 0x06006288 RID: 25224 RVA: 0x001735C0 File Offset: 0x001717C0
		// (set) Token: 0x06006289 RID: 25225 RVA: 0x001735E5 File Offset: 0x001717E5
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the top edge of the Secondary Icon, relative to the RadButton control's wrapper element.")]
		[Category("Layout")]
		public virtual Unit SecondaryIconTop
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

		// Token: 0x1700204E RID: 8270
		// (get) Token: 0x0600628A RID: 25226 RVA: 0x001735FD File Offset: 0x001717FD
		// (set) Token: 0x0600628B RID: 25227 RVA: 0x00173622 File Offset: 0x00171822
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the bottom edge of the Secondary Icon, relative to the RadButton control's wrapper element.")]
		public virtual Unit SecondaryIconBottom
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

		// Token: 0x1700204F RID: 8271
		// (get) Token: 0x0600628C RID: 25228 RVA: 0x0017363A File Offset: 0x0017183A
		// (set) Token: 0x0600628D RID: 25229 RVA: 0x0017365F File Offset: 0x0017185F
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the left edge of the Secondary Icon, relative to the RadButton control's wrapper element.")]
		[Category("Layout")]
		public virtual Unit SecondaryIconLeft
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

		// Token: 0x17002050 RID: 8272
		// (get) Token: 0x0600628E RID: 25230 RVA: 0x00173677 File Offset: 0x00171877
		// (set) Token: 0x0600628F RID: 25231 RVA: 0x0017369C File Offset: 0x0017189C
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		[Description("Gets or sets the right edge of the Secondary Icon, relative to the RadButton control's wrapper element.")]
		public virtual Unit SecondaryIconRight
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
	}
}
