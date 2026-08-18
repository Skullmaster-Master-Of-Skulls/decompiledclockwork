using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FDA RID: 4058
	internal class PostControlToAjaxify : Control
	{
		// Token: 0x06009DA5 RID: 40357 RVA: 0x002329F8 File Offset: 0x00230BF8
		public PostControlToAjaxify(Control controlToAjaxify)
		{
			this.EnableViewState = false;
			this.controlToAjaxify = controlToAjaxify;
		}

		// Token: 0x06009DA6 RID: 40358 RVA: 0x00232A0E File Offset: 0x00230C0E
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.controlToAjaxify is RadAjaxPanel)
			{
				this.controlToAjaxify.Visible = true;
			}
		}

		// Token: 0x04002C60 RID: 11360
		private Control controlToAjaxify;
	}
}
