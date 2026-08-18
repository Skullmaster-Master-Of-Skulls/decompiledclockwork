using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A2 RID: 1954
	internal class RibbonBarColorPickerClassicRenderer : RibbonBarDropDownItemClassicRenderer
	{
		// Token: 0x06004475 RID: 17525 RVA: 0x000D76AD File Offset: 0x000D58AD
		public RibbonBarColorPickerClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x06004476 RID: 17526 RVA: 0x000D76B8 File Offset: 0x000D58B8
		protected override void RenderDropDownContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbList rrbColorList");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			((RibbonBarColorPicker)base.Owner).SyncronizeItemsAndPreset();
			foreach (RibbonBarColorPickerItem ribbonBarColorPickerItem in ((RibbonBarColorPicker)base.Owner).Items)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbColorListItem");
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbCPColorBox");
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.AddAttribute(HtmlTextWriterAttribute.Title, ribbonBarColorPickerItem.Title);
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, RibbonBarColorPickerClassicRenderer.ColorToHex(ribbonBarColorPickerItem.Value));
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(RibbonBarColorPickerClassicRenderer.ColorToHex(ribbonBarColorPickerItem.Value));
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x06004477 RID: 17527 RVA: 0x000D77C0 File Offset: 0x000D59C0
		protected override void RenderInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, ((RibbonBarColorPicker)base.Owner).InputCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			if (!string.IsNullOrEmpty(((RibbonBarColorPicker)base.Owner).ImageUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbCPImage");
				writer.AddAttribute(HtmlTextWriterAttribute.Src, ((RibbonBarColorPicker)base.Owner).ResolveUrl(((RibbonBarColorPicker)base.Owner).ImageUrl));
				writer.AddAttribute(HtmlTextWriterAttribute.Alt, "image");
				writer.RenderBeginTag(HtmlTextWriterTag.Img);
				writer.RenderEndTag();
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbCPColorPreview");
			Color color = ((RibbonBarColorPicker)base.Owner).SelectedColor;
			if (color.Equals(Color.Empty))
			{
				color = Color.White;
			}
			string value = RibbonBarColorPickerClassicRenderer.ColorToHex(color);
			writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, value);
			if (!string.IsNullOrEmpty(((RibbonBarColorPicker)base.Owner).ImageUrl))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "3px");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x06004478 RID: 17528 RVA: 0x000D78D8 File Offset: 0x000D5AD8
		protected static string ColorToHex(Color color)
		{
			if (color.Equals(Color.Empty))
			{
				return string.Empty;
			}
			return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
		}
	}
}
