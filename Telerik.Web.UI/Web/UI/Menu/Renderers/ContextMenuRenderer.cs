using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005DD RID: 1501
	public class ContextMenuRenderer : MenuRendererBase
	{
		// Token: 0x06003698 RID: 13976 RVA: 0x000B4B48 File Offset: 0x000B2D48
		public ContextMenuRenderer(RadContextMenu menu) : base(menu)
		{
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x000B4B51 File Offset: 0x000B2D51
		public override string CssClassFormatString
		{
			get
			{
				if (base.Owner.InDesignMode)
				{
					return "RadMenu RadMenu_{0} RadMenu_Context RadMenu_{0}_Context";
				}
				return string.Empty;
			}
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000B4B6C File Offset: 0x000B2D6C
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = base.Owner.CssClass;
			base.Owner.CssClass = string.Empty;
			base.AddAttributesToRender(writer);
			base.Owner.CssClass = cssClass;
			if (base.Owner.InDesignMode)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "block");
			}
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000B4BC4 File Offset: 0x000B2DC4
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (base.Owner.InDesignMode)
			{
				base.RenderDesignTimeHtml(writer);
			}
			else
			{
				string text = string.Empty;
				string text2 = string.Empty;
				if (base.Owner.EnableRoundedCorners)
				{
					text = "rmRoundedCorners";
					text2 = string.Format("{0}_{1}", "rmRoundedCorners", base.Owner.RuntimeSkin);
				}
				string text3 = string.Empty;
				if (base.Owner.EnableShadows)
				{
					text3 = "rmShadows";
				}
				string text4 = "RadMenu RadMenu_{0} RadMenu_Context RadMenu_{0}_Context";
				string text5 = string.Empty;
				if (base.Owner.Attributes["dir"] == "rtl")
				{
					text4 += " RadMenu_rtl RadMenu_{0}_rtl RadMenu_Context_rtl RadMenu_{0}_Context_rtl";
				}
				text5 = string.Format(text4, base.Owner.RuntimeSkin);
				if (base.Owner.TabIndex != 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, base.Owner.TabIndex.ToString());
				}
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1} {2} {3} {4}", new object[]
				{
					text5,
					text,
					text2,
					text3,
					base.Owner.CssClass
				}).Trim());
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
			}
			Unit width = base.Owner.DefaultGroupSettings.Width;
			Unit height = base.Owner.DefaultGroupSettings.Height;
			bool flag = !width.IsEmpty || !height.IsEmpty;
			if (!width.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, width.ToString());
			}
			if (!height.IsEmpty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, height.ToString());
			}
			string text6;
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("{0} {1} {2}1", "rmScrollWrap", "rmGroup", "rmLevel"));
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				text6 = string.Format("rmActive {0}", "rmVertical");
			}
			else
			{
				text6 = string.Format("rmActive {0} {1} {2}1", "rmVertical", "rmGroup", "rmLevel");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, text6);
			if (base.Owner.Items.Count > 0)
			{
				base.RenderRootGroup(writer, new Action<RadMenuItemCollection>(RadMenuItem.UpdatePositionCssClass));
			}
			else
			{
				base.Owner.ChildListElementCssClass = text6;
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
			if (!base.Owner.InDesignMode)
			{
				writer.RenderEndTag();
			}
		}
	}
}
