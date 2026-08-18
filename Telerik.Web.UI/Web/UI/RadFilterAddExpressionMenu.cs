using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018E0 RID: 6368
	public class RadFilterAddExpressionMenu : Control
	{
		// Token: 0x0600F5D4 RID: 62932 RVA: 0x0037C937 File Offset: 0x0037AB37
		public RadFilterAddExpressionMenu(RadFilter owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600F5D5 RID: 62933 RVA: 0x0037C948 File Offset: 0x0037AB48
		protected override void Render(HtmlTextWriter writer)
		{
			string arg = HttpUtility.HtmlEncode(this._owner.RuntimeSkin ?? "");
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("RadFilterAddDrop RadFilterAddDrop_{0}", arg));
			writer.AddAttribute(HtmlTextWriterAttribute.Id, HttpUtility.HtmlEncode(this.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.Render(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600F5D6 RID: 62934 RVA: 0x0037C9B8 File Offset: 0x0037ABB8
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			HyperLink hyperLink = new HyperLink();
			hyperLink.CssClass = "rfAddExp";
			hyperLink.NavigateUrl = "#";
			hyperLink.Text = "Add Expression";
			this.Controls.Add(hyperLink);
			HyperLink hyperLink2 = new HyperLink();
			hyperLink2.CssClass = "rfAddGroup";
			hyperLink2.NavigateUrl = "#";
			hyperLink2.Text = "Add Group";
			this.Controls.Add(hyperLink2);
		}

		// Token: 0x04004676 RID: 18038
		private RadFilter _owner;
	}
}
