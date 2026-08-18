using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x02001326 RID: 4902
	internal sealed class InlineEditTemplate : InlineTemplate
	{
		// Token: 0x0600CCCB RID: 52427 RVA: 0x002DA6CA File Offset: 0x002D88CA
		public InlineEditTemplate(RadScheduler owner) : base(owner)
		{
		}

		// Token: 0x0600CCCC RID: 52428 RVA: 0x002DA6D4 File Offset: 0x002D88D4
		protected override void CreateChildControls(Control container)
		{
			LinkButton linkButton = new LinkButton();
			container.Controls.Add(linkButton);
			linkButton.CommandName = "Update";
			linkButton.Text = base.Owner.Localization.Save;
			linkButton.CssClass = "rsAptEditConfirm";
			if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight)
			{
				LinkButton linkButton2 = linkButton;
				linkButton2.CssClass += " rsButton";
			}
			linkButton.ID = "update";
			if (base.Owner.ResolvedRenderMode == RenderMode.Mobile && this._renderDeleteButton)
			{
				LinkButton linkButton3 = new LinkButton();
				container.Controls.Add(linkButton3);
				linkButton3.CommandName = "Delete";
				linkButton3.Text = base.Owner.Localization.Delete;
				linkButton3.CssClass = "rsAptEditDelete";
				linkButton3.ID = "delete";
			}
		}
	}
}
