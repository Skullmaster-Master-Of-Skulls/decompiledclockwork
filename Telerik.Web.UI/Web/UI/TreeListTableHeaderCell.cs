using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001285 RID: 4741
	public class TreeListTableHeaderCell : TableHeaderCell
	{
		// Token: 0x0600C5B6 RID: 50614 RVA: 0x002C2CA2 File Offset: 0x002C0EA2
		public TreeListTableHeaderCell()
		{
		}

		// Token: 0x0600C5B7 RID: 50615 RVA: 0x002C2CAA File Offset: 0x002C0EAA
		public TreeListTableHeaderCell(bool useNbsp)
		{
			this.Text = (useNbsp ? "&nbsp;" : "");
		}

		// Token: 0x0600C5B8 RID: 50616 RVA: 0x002C2CC7 File Offset: 0x002C0EC7
		private void AddScopeAttribute(HtmlTextWriter writer)
		{
			writer.AddAttribute("scope", "col");
		}

		// Token: 0x0600C5B9 RID: 50617 RVA: 0x002C2CDC File Offset: 0x002C0EDC
		private void AddStyleAttributes(HtmlTextWriter writer)
		{
			if (base.ControlStyle is TableItemStyle && (base.ControlStyle as TableItemStyle).HorizontalAlign != HorizontalAlign.NotSet)
			{
				base.Style["text-align"] = (base.ControlStyle as TableItemStyle).HorizontalAlign.ToString().ToLower();
				(base.ControlStyle as TableItemStyle).HorizontalAlign = HorizontalAlign.NotSet;
			}
		}

		// Token: 0x0600C5BA RID: 50618 RVA: 0x002C2D48 File Offset: 0x002C0F48
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.AddScopeAttribute(writer);
			this.AddStyleAttributes(writer);
			base.AddAttributesToRender(writer);
		}
	}
}
