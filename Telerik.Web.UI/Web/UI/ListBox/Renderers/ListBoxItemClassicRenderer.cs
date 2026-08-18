using System;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.ListBox.Renderers
{
	// Token: 0x0200057A RID: 1402
	internal class ListBoxItemClassicRenderer : ListBoxItemRenderBase
	{
		// Token: 0x060032C7 RID: 12999 RVA: 0x000A7696 File Offset: 0x000A5896
		public ListBoxItemClassicRenderer(RadListBoxItem owner) : base(owner)
		{
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x000A76A0 File Offset: 0x000A58A0
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
			writer.RenderEndTag();
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000A76FC File Offset: 0x000A58FC
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
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000A7800 File Offset: 0x000A5A00
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
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbText");
			Color foreColor = base.Owner.ForeColor;
			if (base.Owner.ForeColor != Color.Empty)
			{
				string value = base.Owner.ForeColor.IsKnownColor ? base.Owner.ForeColor.Name : string.Format("rgb({0},{1},{2})", base.Owner.ForeColor.R, base.Owner.ForeColor.G, base.Owner.ForeColor.B);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Color, value);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(HttpUtility.HtmlEncode(base.Owner.Text));
			if (flag)
			{
				writer.RenderEndTag();
			}
		}
	}
}
