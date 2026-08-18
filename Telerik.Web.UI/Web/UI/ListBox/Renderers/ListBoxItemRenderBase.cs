using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.ListBox.Renderers
{
	// Token: 0x02000579 RID: 1401
	public abstract class ListBoxItemRenderBase : RendererBase
	{
		// Token: 0x060032C0 RID: 12992 RVA: 0x000A759B File Offset: 0x000A579B
		public ListBoxItemRenderBase(RadListBoxItem owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x060032C1 RID: 12993 RVA: 0x000A75AA File Offset: 0x000A57AA
		// (set) Token: 0x060032C2 RID: 12994 RVA: 0x000A75B2 File Offset: 0x000A57B2
		protected RadListBoxItem Owner { get; set; }

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000A75BB File Offset: 0x000A57BB
		protected RadListBox ListBox
		{
			get
			{
				return this.Owner.ListBox;
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000A75C8 File Offset: 0x000A57C8
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000A75CC File Offset: 0x000A57CC
		protected void RenderCheckBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbCheck");
			if (this.Owner.Checked)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
			}
			if (!this.Owner.IsItemEnabled || !this.ListBox.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000A7644 File Offset: 0x000A5844
		protected void RenderImage(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rlbImage");
			writer.AddAttribute(HtmlTextWriterAttribute.Alt, string.Empty);
			writer.AddAttribute(HtmlTextWriterAttribute.Src, this.Owner.ResolveClientUrl(this.Owner.ImageUrl));
			writer.RenderBeginTag(HtmlTextWriterTag.Img);
			writer.RenderEndTag();
		}
	}
}
