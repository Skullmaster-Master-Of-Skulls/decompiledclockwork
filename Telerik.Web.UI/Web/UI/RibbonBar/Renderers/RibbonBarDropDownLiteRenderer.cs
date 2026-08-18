using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007B1 RID: 1969
	internal class RibbonBarDropDownLiteRenderer : RibbonBarColorPickerLiteRenderer
	{
		// Token: 0x060044C6 RID: 17606 RVA: 0x000D9276 File Offset: 0x000D7476
		public RibbonBarDropDownLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x000D9280 File Offset: 0x000D7480
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

		// Token: 0x060044C8 RID: 17608 RVA: 0x000D92F8 File Offset: 0x000D74F8
		protected override void RenderInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, ((RibbonBarDropDown)base.Owner).InputCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}
	}
}
