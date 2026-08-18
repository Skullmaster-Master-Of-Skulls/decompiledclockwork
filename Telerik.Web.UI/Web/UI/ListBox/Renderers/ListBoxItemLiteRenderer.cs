using System;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.ListBox.Renderers
{
	// Token: 0x0200057B RID: 1403
	internal class ListBoxItemLiteRenderer : ListBoxItemRenderBase
	{
		// Token: 0x060032CB RID: 13003 RVA: 0x000A7955 File Offset: 0x000A5B55
		public ListBoxItemLiteRenderer(RadListBoxItem owner) : base(owner)
		{
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000A7960 File Offset: 0x000A5B60
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (base.ListBox.CheckBoxes && base.Owner.Checkable)
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Label);
				base.RenderCheckBox(writer);
			}
			if (!string.IsNullOrEmpty(base.Owner.ImageUrl))
			{
				base.RenderImage(writer);
			}
			this.RenderWrap(writer);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000A79B8 File Offset: 0x000A5BB8
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			StringBuilder stringBuilder = new StringBuilder("rlbItem");
			if (!base.Owner.IsItemEnabled)
			{
				stringBuilder.AppendFormat(" {0}", "rlbDisabled");
			}
			if (base.Owner.Selected)
			{
				stringBuilder.AppendFormat(" {0}", "rlbSelected");
			}
			if (!string.IsNullOrEmpty(base.Owner.CssClass))
			{
				stringBuilder.AppendFormat(" {0}", base.Owner.CssClass);
			}
			base.Owner.CssClass = stringBuilder.ToString();
			Color foreColor = base.Owner.ForeColor;
			base.Owner.ForeColor = Color.Empty;
			string id = base.Owner.ID;
			if ((base.Owner.Parent as RadListBox).EnableLoadOnDemand)
			{
				base.Owner.ID = "";
			}
			base.Owner.CallBaseAddAttributesToRender(writer);
			base.Owner.ID = id;
			base.Owner.ForeColor = foreColor;
			Color foreColor2 = base.Owner.ForeColor;
			if (base.Owner.ForeColor != Color.Empty)
			{
				string value = base.Owner.ForeColor.IsKnownColor ? base.Owner.ForeColor.Name : string.Format("rgb({0},{1},{2})", base.Owner.ForeColor.R, base.Owner.ForeColor.G, base.Owner.ForeColor.B);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, value);
			}
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x000A7B6C File Offset: 0x000A5D6C
		protected void RenderWrap(HtmlTextWriter writer)
		{
			bool flag = base.ListBox.CheckBoxes && base.Owner.Checkable;
			if (base.Owner.Templated || base.Owner.Controls.IsReadOnly)
			{
				if (flag)
				{
					writer.RenderEndTag();
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbTemplate");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				base.Owner.CallBaseRenderChildren(writer);
				writer.RenderEndTag();
				return;
			}
			writer.Write(HttpUtility.HtmlEncode(base.Owner.Text));
			if (flag)
			{
				writer.RenderEndTag();
			}
		}
	}
}
