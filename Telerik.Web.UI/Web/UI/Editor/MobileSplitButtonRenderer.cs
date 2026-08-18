using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002D0 RID: 720
	internal class MobileSplitButtonRenderer : MobileDropDownRenderer
	{
		// Token: 0x06001919 RID: 6425 RVA: 0x00052C8C File Offset: 0x00050E8C
		public MobileSplitButtonRenderer(EditorSplitButton editor) : base(editor)
		{
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x00052C95 File Offset: 0x00050E95
		public override void RenderToolIcon(HtmlTextWriter writer)
		{
			if (base.Owner.ShowIcon)
			{
				this.AddIconAttributesToRender(writer);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
			}
		}
	}
}
