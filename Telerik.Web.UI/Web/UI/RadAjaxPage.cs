using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FE0 RID: 4064
	public class RadAjaxPage : Page, IRadAjaxPage
	{
		// Token: 0x06009E16 RID: 40470 RVA: 0x00233E66 File Offset: 0x00232066
		internal void AttachOnRender(RenderMethod renderMethod)
		{
			this.onRenderDelegate = renderMethod;
		}

		// Token: 0x06009E17 RID: 40471 RVA: 0x00233E6F File Offset: 0x0023206F
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.onRenderDelegate != null)
			{
				this.onRenderDelegate(writer, this);
			}
			base.Render(writer);
		}

		// Token: 0x06009E18 RID: 40472 RVA: 0x00233E8D File Offset: 0x0023208D
		void IRadAjaxPage.AttachOnRender(RenderMethod renderMethod)
		{
			this.AttachOnRender(renderMethod);
		}

		// Token: 0x04002C72 RID: 11378
		private RenderMethod onRenderDelegate;
	}
}
