using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A3 RID: 1955
	internal class RibbonBarDropDownClassicRenderer : RibbonBarColorPickerClassicRenderer
	{
		// Token: 0x06004479 RID: 17529 RVA: 0x000D7932 File Offset: 0x000D5B32
		public RibbonBarDropDownClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x0600447A RID: 17530 RVA: 0x000D793C File Offset: 0x000D5B3C
		protected override void RenderDropDownContents(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbList");
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			foreach (RibbonBarListItem ribbonBarListItem in ((RibbonBarDropDown)base.Owner).Items)
			{
				ribbonBarListItem.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600447B RID: 17531 RVA: 0x000D79B4 File Offset: 0x000D5BB4
		protected override void RenderInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, ((RibbonBarDropDown)base.Owner).InputCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}
	}
}
