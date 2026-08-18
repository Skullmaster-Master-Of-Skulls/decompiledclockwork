using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200044E RID: 1102
	internal sealed class LabelLiteral : Literal
	{
		// Token: 0x06003537 RID: 13623 RVA: 0x000AC8E5 File Offset: 0x000AAAE5
		internal LabelLiteral(Control forControl)
		{
			this._for = forControl;
		}

		// Token: 0x17000F79 RID: 3961
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000AC8F4 File Offset: 0x000AAAF4
		// (set) Token: 0x06003539 RID: 13625 RVA: 0x000AC8FC File Offset: 0x000AAAFC
		internal bool RenderAsLabel
		{
			get
			{
				return this._renderAsLabel;
			}
			set
			{
				this._renderAsLabel = value;
			}
		}

		// Token: 0x0600353A RID: 13626 RVA: 0x000AC908 File Offset: 0x000AAB08
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.RenderAsLabel)
			{
				writer.Write("<asp:label runat=\"server\" AssociatedControlID=\"");
				writer.Write(this._for.ID);
				writer.Write("\" ID=\"");
				writer.Write(this._for.ID);
				writer.Write("Label\">");
				writer.Write(base.Text);
				writer.Write("</asp:label>");
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.For, this._for.ClientID);
			writer.RenderBeginTag(HtmlTextWriterTag.Label);
			base.Render(writer);
			writer.RenderEndTag();
		}

		// Token: 0x040021BB RID: 8635
		internal Control _for;

		// Token: 0x040021BC RID: 8636
		internal bool _renderAsLabel;
	}
}
