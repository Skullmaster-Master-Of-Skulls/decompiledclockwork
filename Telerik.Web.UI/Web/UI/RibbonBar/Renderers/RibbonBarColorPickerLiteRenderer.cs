using System;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B0 RID: 1968
	internal class RibbonBarColorPickerLiteRenderer : RibbonBarDropDownItemLiteRenderer
	{
		// Token: 0x060044C1 RID: 17601 RVA: 0x000D8F97 File Offset: 0x000D7197
		public RibbonBarColorPickerLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044C2 RID: 17602 RVA: 0x000D8FA0 File Offset: 0x000D71A0
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				base.RenderKeyboardBox(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, ((RibbonBarDropDownItem)base.Owner).InnerCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderInput(writer);
			this.RenderButtons(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060044C3 RID: 17603 RVA: 0x000D8FFC File Offset: 0x000D71FC
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
				writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, RibbonBarColorPickerLiteRenderer.ColorToHex(ribbonBarColorPickerItem.Value));
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.Write(RibbonBarColorPickerLiteRenderer.ColorToHex(ribbonBarColorPickerItem.Value));
				writer.RenderEndTag();
				writer.RenderEndTag();
				writer.RenderEndTag();
			}
			writer.RenderEndTag();
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x000D9104 File Offset: 0x000D7304
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
			string value = RibbonBarColorPickerLiteRenderer.ColorToHex(color);
			writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, value);
			if (!string.IsNullOrEmpty(((RibbonBarColorPicker)base.Owner).ImageUrl))
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, "3px");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x000D921C File Offset: 0x000D741C
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
