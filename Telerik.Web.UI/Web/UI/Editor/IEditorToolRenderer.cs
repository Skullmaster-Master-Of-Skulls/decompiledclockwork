using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C1 RID: 705
	public interface IEditorToolRenderer : IEditorRenderer, IRenderer
	{
		// Token: 0x0600188C RID: 6284
		void AddIconAttributesToRender(HtmlTextWriter writer);

		// Token: 0x0600188D RID: 6285
		void Render(HtmlTextWriter writer);

		// Token: 0x0600188E RID: 6286
		void RenderToolIcon(HtmlTextWriter writer);

		// Token: 0x0600188F RID: 6287
		void AddTextAttributesToRender(HtmlTextWriter writer);

		// Token: 0x06001890 RID: 6288
		void RenderToolText(HtmlTextWriter writer);

		// Token: 0x06001891 RID: 6289
		void RenderSplitButtonArrow(HtmlTextWriter writer);

		// Token: 0x06001892 RID: 6290
		string GetCssClassString();

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001893 RID: 6291
		string CssClassString { get; }
	}
}
