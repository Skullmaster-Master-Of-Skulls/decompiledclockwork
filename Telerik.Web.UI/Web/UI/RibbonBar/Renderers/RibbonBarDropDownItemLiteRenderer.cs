using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007AF RID: 1967
	internal class RibbonBarDropDownItemLiteRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x060044B8 RID: 17592 RVA: 0x000D8CED File Offset: 0x000D6EED
		public RibbonBarDropDownItemLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x060044B9 RID: 17593 RVA: 0x000D8CF8 File Offset: 0x000D6EF8
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			string text = base.Owner.Enabled ? string.Empty : "rrbDisabled";
			base.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				((RibbonBarDropDownItem)base.Owner).ItemCssClass,
				base.Owner.CssClass,
				text
			});
			base.Owner.BaseAddAttributesToRender(writer);
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Accesskey, base.Owner.AccessKey);
			}
			base.Owner.CssClass = cssClass;
		}

		// Token: 0x060044BA RID: 17594 RVA: 0x000D8DA4 File Offset: 0x000D6FA4
		public override void RenderContents(HtmlTextWriter writer)
		{
			if (!string.IsNullOrEmpty(base.Owner.AccessKey))
			{
				this.RenderKeyboardBox(writer);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, ((RibbonBarDropDownItem)base.Owner).InnerCssClass);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderInput(writer);
			this.RenderButtons(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060044BB RID: 17595 RVA: 0x000D8E00 File Offset: 0x000D7000
		public override void RenderDropDown(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rrbDropDownSlide rrbDropDownSlide_{0}", base.Owner.RibbonBar.RuntimeSkin));
			writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
			if (base.Owner.Width != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.Owner.Width.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("rrbPopup", new object[0]));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderDropDownContents(writer);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060044BC RID: 17596 RVA: 0x000D8EAF File Offset: 0x000D70AF
		protected virtual void RenderDropDownContents(HtmlTextWriter writer)
		{
		}

		// Token: 0x060044BD RID: 17597 RVA: 0x000D8EB1 File Offset: 0x000D70B1
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x000D8EDF File Offset: 0x000D70DF
		protected virtual void RenderInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, ((RibbonBarDropDownItem)base.Owner).InputCssClass);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x000D8F14 File Offset: 0x000D7114
		protected virtual void RenderButtons(HtmlTextWriter writer)
		{
			this.RenderButton(writer, "Select");
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x000D8F24 File Offset: 0x000D7124
		protected virtual void RenderButton(HtmlTextWriter writer, string text)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, RibbonBarStyles.Combine(new string[]
			{
				"radIcon",
				"radIconDown"
			}));
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
