using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A1 RID: 1953
	internal class RibbonBarDropDownItemClassicRenderer : RibbonBarItemRenderBase
	{
		// Token: 0x0600446C RID: 17516 RVA: 0x000D73F6 File Offset: 0x000D55F6
		public RibbonBarDropDownItemClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x0600446D RID: 17517 RVA: 0x000D7400 File Offset: 0x000D5600
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

		// Token: 0x0600446E RID: 17518 RVA: 0x000D74AC File Offset: 0x000D56AC
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

		// Token: 0x0600446F RID: 17519 RVA: 0x000D7508 File Offset: 0x000D5708
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

		// Token: 0x06004470 RID: 17520 RVA: 0x000D75B7 File Offset: 0x000D57B7
		protected virtual void RenderDropDownContents(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004471 RID: 17521 RVA: 0x000D75BE File Offset: 0x000D57BE
		protected void RenderKeyboardBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(base.Owner.AccessKey);
			writer.RenderEndTag();
		}

		// Token: 0x06004472 RID: 17522 RVA: 0x000D75EC File Offset: 0x000D57EC
		protected virtual void RenderInput(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, ((RibbonBarDropDownItem)base.Owner).InputCssClass);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x06004473 RID: 17523 RVA: 0x000D7621 File Offset: 0x000D5821
		protected void RenderButtons(HtmlTextWriter writer)
		{
			this.RenderButton(writer, "Select");
		}

		// Token: 0x06004474 RID: 17524 RVA: 0x000D7630 File Offset: 0x000D5830
		protected void RenderButton(HtmlTextWriter writer, string text)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbActionButton");
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.RenderBeginTag(HtmlTextWriterTag.Button);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbIcon rrbIconDown");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write("<!-- &nbsp; -->");
			writer.RenderEndTag();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbButtonText");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(text);
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
	}
}
