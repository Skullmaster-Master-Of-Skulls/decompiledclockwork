using System;
using System.ComponentModel;

namespace System.Web.UI.HtmlControls
{
	// Token: 0x02000352 RID: 850
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	[SupportsEventValidation]
	public class HtmlInputPassword : HtmlInputText, IPostBackDataHandler
	{
		// Token: 0x06002723 RID: 10019 RVA: 0x0007FA17 File Offset: 0x0007DC17
		public HtmlInputPassword() : base("password")
		{
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x0007FA24 File Offset: 0x0007DC24
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			this.ViewState.Remove("value");
			base.RenderAttributes(writer);
		}

		// Token: 0x04001DCE RID: 7630
		private static readonly object EventServerChange = new object();
	}
}
