using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005DF RID: 1503
	public class MenuMobileRenderer : MenuRendererBase
	{
		// Token: 0x060036A1 RID: 13985 RVA: 0x000B4F86 File Offset: 0x000B3186
		public MenuMobileRenderer(RadMenu menu) : base(menu)
		{
		}

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060036A2 RID: 13986 RVA: 0x000B4F8F File Offset: 0x000B318F
		public override string CssClassFormatString
		{
			get
			{
				return "RadMenu RadMenu_{0}";
			}
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000B4F98 File Offset: 0x000B3198
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Unit width = base.Owner.Width;
			base.Owner.Width = Unit.Empty;
			Unit height = base.Owner.Height;
			base.Owner.Height = Unit.Empty;
			base.AddAttributesToRender(writer);
			base.Owner.Width = width;
			base.Owner.Height = height;
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000B4FFC File Offset: 0x000B31FC
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			this.RenderButton(writer);
			this.RenderPopUp(writer);
		}

		// Token: 0x060036A5 RID: 13989 RVA: 0x000B5014 File Offset: 0x000B3214
		protected void RenderPopUp(HtmlTextWriter writer)
		{
			bool flag = base.Owner.Attributes["dir"] == "rtl";
			string arg = string.Format("{0} {0}_{1} {2}", "RadMenuPopup", base.Owner.RuntimeSkin, flag ? "RadMenuPopup_rtl" : string.Empty).TrimEnd(new char[0]);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Dir, "rtl");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1}", arg, base.Owner.CssClass).TrimEnd(new char[0]));
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			if (!base.Owner.Width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.Owner.Width.ToString());
			}
			if (!base.Owner.Height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, base.Owner.Height.ToString());
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmSlide");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			base.Owner.ChildListElementCssClass = string.Format("{0} {1}", "rmGroup", "rmRootGroup");
			if (base.Owner.Items.Count > 0)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, base.Owner.ChildListElementCssClass);
				base.RenderRootGroup(writer, null);
			}
			writer.RenderEndTag();
			writer.RenderEndTag();
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000B5194 File Offset: 0x000B3394
		protected void RenderButton(HtmlTextWriter writer)
		{
			string value = string.Format("{0} {1}", "rmRootToggle", (!base.Owner.Enabled) ? "rmDisabled" : string.Empty).TrimEnd(new char[0]);
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.RenderEndTag();
		}
	}
}
