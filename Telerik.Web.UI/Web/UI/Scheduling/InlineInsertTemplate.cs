using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001327 RID: 4903
	internal sealed class InlineInsertTemplate : InlineTemplate
	{
		// Token: 0x0600CCCD RID: 52429 RVA: 0x002DA7AD File Offset: 0x002D89AD
		public InlineInsertTemplate(RadScheduler owner) : base(owner)
		{
		}

		// Token: 0x0600CCCE RID: 52430 RVA: 0x002DA7B8 File Offset: 0x002D89B8
		protected override void CreateChildControls(Control container)
		{
			LinkButton linkButton = new LinkButton();
			container.Controls.Add(linkButton);
			linkButton.CommandName = "Insert";
			linkButton.Text = base.Owner.Localization.Save;
			linkButton.ID = "insert";
			linkButton.CssClass = "rsAptEditConfirm";
			if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight)
			{
				LinkButton linkButton2 = linkButton;
				linkButton2.CssClass += " rsButton";
			}
		}
	}
}
