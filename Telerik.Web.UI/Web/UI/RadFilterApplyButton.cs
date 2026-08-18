using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018E1 RID: 6369
	public class RadFilterApplyButton : Button
	{
		// Token: 0x0600F5D7 RID: 62935 RVA: 0x0037CA31 File Offset: 0x0037AC31
		public RadFilterApplyButton(RadFilter owner)
		{
			this._owner = owner;
		}

		// Token: 0x0600F5D8 RID: 62936 RVA: 0x0037CA40 File Offset: 0x0037AC40
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.Text = this._owner.ApplyButtonText;
		}

		// Token: 0x0600F5D9 RID: 62937 RVA: 0x0037CA5C File Offset: 0x0037AC5C
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rfApply");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (this._owner.UseAccessibleApplyButton)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
				writer.RenderBeginTag(HtmlTextWriterTag.A);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rfButton");
			base.Render(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x04004677 RID: 18039
		protected const string ApplyButtonClassName = "rfApply";

		// Token: 0x04004678 RID: 18040
		private RadFilter _owner;
	}
}
