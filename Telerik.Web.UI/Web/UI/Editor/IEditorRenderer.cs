using System;
using System.Web.UI;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002BE RID: 702
	public interface IEditorRenderer : IRenderer
	{
		// Token: 0x06001878 RID: 6264
		void RenderBeginTag(HtmlTextWriter writer);

		// Token: 0x06001879 RID: 6265
		void RenderEndTag(HtmlTextWriter writer);

		// Token: 0x0600187A RID: 6266
		void RenderChildren(HtmlTextWriter writer);
	}
}
