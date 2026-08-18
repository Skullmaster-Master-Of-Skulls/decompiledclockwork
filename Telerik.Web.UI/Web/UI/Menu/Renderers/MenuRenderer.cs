using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005E0 RID: 1504
	public class MenuRenderer : MenuRendererBase
	{
		// Token: 0x060036A7 RID: 13991 RVA: 0x000B51EC File Offset: 0x000B33EC
		public MenuRenderer(RadMenu menu) : base(menu)
		{
		}

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x060036A8 RID: 13992 RVA: 0x000B51F8 File Offset: 0x000B33F8
		public override string CssClassFormatString
		{
			get
			{
				string text = "RadMenu RadMenu_{0}";
				if (base.Owner.Attributes["dir"] == "rtl")
				{
					text += " RadMenu_rtl RadMenu_{0}_rtl";
				}
				if (!base.Owner.Width.IsEmpty || !base.Owner.Height.IsEmpty)
				{
					text += " rmSized";
				}
				return text;
			}
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000B5270 File Offset: 0x000B3470
		public override void RenderContents(HtmlTextWriter writer)
		{
			base.RenderContents(writer);
			if (base.Owner.InDesignMode)
			{
				base.RenderDesignTimeHtml(writer);
			}
			bool flag = false;
			if (base.Owner.EnableRootItemScroll)
			{
				if (base.Owner.Flow == ItemFlow.Horizontal && !base.Owner.Width.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.Owner.Width.ToString());
					flag = true;
				}
				else if (base.Owner.Flow == ItemFlow.Vertical && !base.Owner.Height.IsEmpty)
				{
					writer.AddStyleAttribute(HtmlTextWriterStyle.Height, base.Owner.Height.ToString());
					flag = true;
				}
			}
			string rootGroupCssClass = this.GetRootGroupCssClass();
			string text = rootGroupCssClass + " " + RadMenu.GetFlowCssClass(base.Owner.Flow);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmScrollWrap " + text);
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rmRootScrollGroup");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, text);
			}
			if (base.Owner.Items.Count > 0)
			{
				base.RenderRootGroup(writer, new Action<RadMenuItemCollection>(RadMenuItem.UpdatePositionCssClass));
			}
			else
			{
				base.Owner.ChildListElementCssClass = text;
			}
			if (flag)
			{
				writer.RenderEndTag();
			}
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000B53D0 File Offset: 0x000B35D0
		protected string GetRootGroupCssClass()
		{
			List<string> list = new List<string>();
			list.Add("rmRootGroup");
			if (base.Owner.EnableRoundedCorners)
			{
				list.Add("rmRoundedCorners");
			}
			if (base.Owner.EnableShadows)
			{
				list.Add("rmShadows");
			}
			if (base.Owner.ShowToggleHandle)
			{
				list.Add("rmToggleHandles");
			}
			return string.Join(" ", list.ToArray());
		}
	}
}
