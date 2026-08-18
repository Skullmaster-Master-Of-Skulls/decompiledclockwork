using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ButtonRendering.Lightweight
{
	// Token: 0x020000E5 RID: 229
	public class IconRenderer
	{
		// Token: 0x0600098E RID: 2446 RVA: 0x000229BC File Offset: 0x00020BBC
		public IconRenderer(ButtonRenderingOptions btnOptions, IconRenderingOptions o)
		{
			this.buttonOptions = btnOptions;
			this.iconOptions = o;
			if (this.iconOptions.ShowPrimaryIcon)
			{
				this.isPrimaryIconEmbedded = !string.IsNullOrEmpty(this.iconOptions.PrimaryIconCssClass);
				this.isPrimaryIconCustom = (!this.isPrimaryIconEmbedded && !string.IsNullOrEmpty(this.iconOptions.PrimaryIconUrl));
			}
			this.HasPrimaryIconWithPosition = (this.HasPrimaryIcon && (!this.iconOptions.PrimaryIconTop.IsEmpty || !this.iconOptions.PrimaryIconBottom.IsEmpty || !this.iconOptions.PrimaryIconLeft.IsEmpty || !this.iconOptions.PrimaryIconRight.IsEmpty));
			if (this.iconOptions.ShowSecondaryIcon)
			{
				this.isSecondaryIconEmbedded = !string.IsNullOrEmpty(this.iconOptions.SecondaryIconCssClass);
				this.isSecondaryIconCustom = (!this.isSecondaryIconEmbedded && !string.IsNullOrEmpty(this.iconOptions.SecondaryIconUrl));
			}
			this.HasSecondaryIconWithPosition = (this.HasSecondaryIcon && (!this.iconOptions.SecondaryIconTop.IsEmpty || !this.iconOptions.SecondaryIconBottom.IsEmpty || !this.iconOptions.SecondaryIconLeft.IsEmpty || !this.iconOptions.SecondaryIconRight.IsEmpty));
			this.isCustomToggle = (this.buttonOptions.ToggleType != ButtonToggleType.None && this.buttonOptions.ToggleStatesCount > 1);
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00022B6B File Offset: 0x00020D6B
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x00022B73 File Offset: 0x00020D73
		public bool HasPrimaryIconWithPosition { get; private set; }

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00022B7C File Offset: 0x00020D7C
		// (set) Token: 0x06000992 RID: 2450 RVA: 0x00022B84 File Offset: 0x00020D84
		public bool HasSecondaryIconWithPosition { get; private set; }

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x00022B8D File Offset: 0x00020D8D
		public bool HasPrimaryIcon
		{
			get
			{
				return this.isPrimaryIconEmbedded || this.isPrimaryIconCustom;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00022B9F File Offset: 0x00020D9F
		public bool HasSecondaryIcon
		{
			get
			{
				return this.isSecondaryIconEmbedded || this.isSecondaryIconCustom;
			}
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00022BB4 File Offset: 0x00020DB4
		public void RenderPrimaryIcon(HtmlTextWriter writer)
		{
			if (this.isCustomToggle && this.buttonOptions.HasStateWithPrimaryIcon)
			{
				this.RenderIcon(writer, string.Concat(new string[]
				{
					"rbIcon p-icon rbPrimaryIcon"
				}), null);
				return;
			}
			if (this.HasPrimaryIcon)
			{
				this.RenderIconPosition(writer, this.iconOptions.PrimaryIconTop, this.iconOptions.PrimaryIconBottom, this.iconOptions.PrimaryIconLeft, this.iconOptions.PrimaryIconRight);
				this.RenderIconSize(writer, this.iconOptions.PrimaryIconWidth, this.iconOptions.PrimaryIconHeight);
				string iconUrl = this.isPrimaryIconEmbedded ? null : this.iconOptions.PrimaryIconUrl;
				this.RenderIcon(writer, this.GetPrimaryIconCssClasses(), iconUrl);
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x00022C74 File Offset: 0x00020E74
		public void RenderSecondaryIcon(HtmlTextWriter writer)
		{
			if (this.isCustomToggle && this.buttonOptions.HasStateWithSecondaryIcon)
			{
				this.RenderIcon(writer, string.Concat(new string[]
				{
					"rbIcon p-icon rbSecondaryIcon"
				}), null);
				return;
			}
			if (this.HasSecondaryIcon)
			{
				this.RenderIconPosition(writer, this.iconOptions.SecondaryIconTop, this.iconOptions.SecondaryIconBottom, this.iconOptions.SecondaryIconLeft, this.iconOptions.SecondaryIconRight);
				this.RenderIconSize(writer, this.iconOptions.SecondaryIconWidth, this.iconOptions.SecondaryIconHeight);
				string iconUrl = this.isSecondaryIconEmbedded ? null : this.iconOptions.SecondaryIconUrl;
				this.RenderIcon(writer, this.GetSecondaryIconCssClasses(), iconUrl);
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00022D31 File Offset: 0x00020F31
		private void RenderIcon(HtmlTextWriter writer, string iconCssClasses, string iconUrl)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, iconCssClasses);
			if (!string.IsNullOrEmpty(iconUrl))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundImage, string.Format("'{0}'", iconUrl));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x00022D64 File Offset: 0x00020F64
		private void RenderIconPosition(HtmlTextWriter writer, Unit top, Unit bottom, Unit left, Unit right)
		{
			this.RenderStyleAttribute(writer, HtmlTextWriterStyle.Top, top);
			this.RenderStyleAttribute(writer, "bottom", bottom);
			this.RenderStyleAttribute(writer, HtmlTextWriterStyle.Left, left);
			this.RenderStyleAttribute(writer, "right", right);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x00022D96 File Offset: 0x00020F96
		private void RenderIconSize(HtmlTextWriter writer, Unit width, Unit height)
		{
			this.RenderStyleAttribute(writer, HtmlTextWriterStyle.Width, width);
			this.RenderStyleAttribute(writer, HtmlTextWriterStyle.Height, height);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00022DAC File Offset: 0x00020FAC
		private void RenderStyleAttribute(HtmlTextWriter writer, HtmlTextWriterStyle styleProperty, Unit value)
		{
			if (!value.IsEmpty)
			{
				writer.AddStyleAttribute(styleProperty, value.ToString());
			}
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00022DCB File Offset: 0x00020FCB
		private void RenderStyleAttribute(HtmlTextWriter writer, string styleProperty, Unit value)
		{
			if (!value.IsEmpty)
			{
				writer.AddStyleAttribute(styleProperty, value.ToString());
			}
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00022DEA File Offset: 0x00020FEA
		private string GetPrimaryIconCssClasses()
		{
			return this.GetIconCssClassses(this.iconOptions.PrimaryIconCssClass, "rbPrimaryIcon", this.isPrimaryIconEmbedded, this.isPrimaryIconCustom);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00022E0E File Offset: 0x0002100E
		private string GetSecondaryIconCssClasses()
		{
			return this.GetIconCssClassses(this.iconOptions.SecondaryIconCssClass, "rbSecondaryIcon", this.isSecondaryIconEmbedded, this.isSecondaryIconCustom);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00022E34 File Offset: 0x00021034
		private string GetIconCssClassses(string iconCssClass, string definedIconCssClass, bool isEmbeddedIcon, bool isCustomIcon)
		{
			string result = null;
			if (isEmbeddedIcon)
			{
				result = string.Concat(new string[]
				{
					"rbIcon p-icon",
					" ",
					definedIconCssClass,
					" ",
					iconCssClass
				});
			}
			else if (isCustomIcon)
			{
				result = string.Concat(new string[]
				{
					"rbIcon p-icon",
					" ",
					definedIconCssClass,
					" ",
					"rbCustomIcon"
				});
			}
			return result;
		}

		// Token: 0x0400023F RID: 575
		private readonly IconRenderingOptions iconOptions;

		// Token: 0x04000240 RID: 576
		private readonly ButtonRenderingOptions buttonOptions;

		// Token: 0x04000241 RID: 577
		private readonly bool isPrimaryIconEmbedded;

		// Token: 0x04000242 RID: 578
		private readonly bool isPrimaryIconCustom;

		// Token: 0x04000243 RID: 579
		private readonly bool isSecondaryIconEmbedded;

		// Token: 0x04000244 RID: 580
		private readonly bool isSecondaryIconCustom;

		// Token: 0x04000245 RID: 581
		private readonly bool isCustomToggle;
	}
}
