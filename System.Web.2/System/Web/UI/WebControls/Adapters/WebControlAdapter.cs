using System;
using System.Web.UI.Adapters;

namespace System.Web.UI.WebControls.Adapters
{
	// Token: 0x020005C2 RID: 1474
	public class WebControlAdapter : ControlAdapter
	{
		// Token: 0x17001604 RID: 5636
		// (get) Token: 0x06004AB9 RID: 19129 RVA: 0x000F90FB File Offset: 0x000F72FB
		protected new WebControl Control
		{
			get
			{
				return (WebControl)base.Control;
			}
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06004ABA RID: 19130 RVA: 0x000F9108 File Offset: 0x000F7308
		protected bool IsEnabled
		{
			get
			{
				return this.Control.IsEnabled;
			}
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x000F9115 File Offset: 0x000F7315
		protected virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.Control.RenderBeginTag(writer);
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x000F9123 File Offset: 0x000F7323
		protected virtual void RenderEndTag(HtmlTextWriter writer)
		{
			this.Control.RenderEndTag(writer);
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x000F9131 File Offset: 0x000F7331
		protected virtual void RenderContents(HtmlTextWriter writer)
		{
			this.Control.RenderContents(writer);
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x000F913F File Offset: 0x000F733F
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}
	}
}
