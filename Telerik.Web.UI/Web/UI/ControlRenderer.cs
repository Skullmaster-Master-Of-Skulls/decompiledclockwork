using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FD8 RID: 4056
	internal class ControlRenderer : Control
	{
		// Token: 0x06009DA0 RID: 40352 RVA: 0x002328DB File Offset: 0x00230ADB
		public ControlRenderer(Control target)
		{
			this.EnableViewState = false;
			this._target = target;
		}

		// Token: 0x06009DA1 RID: 40353 RVA: 0x002328F4 File Offset: 0x00230AF4
		protected override void Render(HtmlTextWriter writer)
		{
			RenderOnceChecker renderOnceChecker = new RenderOnceChecker(this.Page.Items);
			if (renderOnceChecker.ShouldRender(this._target))
			{
				renderOnceChecker.ControlRendered(this._target);
				this._target.RenderControl(writer);
				if (!(this._target is RadAjaxPanel))
				{
					this._target.Visible = false;
				}
			}
		}

		// Token: 0x04002C5E RID: 11358
		private Control _target;
	}
}
