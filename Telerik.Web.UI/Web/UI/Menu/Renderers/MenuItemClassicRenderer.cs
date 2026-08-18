using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005DB RID: 1499
	public class MenuItemClassicRenderer : MenuItemRenderer
	{
		// Token: 0x0600368C RID: 13964 RVA: 0x000B47C2 File Offset: 0x000B29C2
		public MenuItemClassicRenderer(RadMenuItem owner) : base(owner)
		{
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x000B47CC File Offset: 0x000B29CC
		public override string TemplateContainerClassName
		{
			get
			{
				string arg = (base.Owner.DisabledCssClass != "rmDisabled") ? base.Owner.DisabledCssClass : string.Empty;
				string arg2 = base.Owner.Enabled ? string.Empty : string.Format("{0} {1}", "rmDisabled", arg);
				return string.Format("{0} {1}", "rmText", arg2).TrimEnd(new char[0]);
			}
		}

		// Token: 0x170011E7 RID: 4583
		// (get) Token: 0x0600368E RID: 13966 RVA: 0x000B4844 File Offset: 0x000B2A44
		public override List<string> LinkClassName
		{
			get
			{
				List<string> list = new List<string>();
				list.AddRange(base.LinkClassName);
				list.AddRange(this.ResolvedStateClasses);
				return list;
			}
		}

		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x000B4870 File Offset: 0x000B2A70
		public override List<string> CssClass
		{
			get
			{
				List<string> list = new List<string>();
				list.AddRange(base.CssClass);
				if (!base.Owner.IsSeparator)
				{
					if (!string.IsNullOrEmpty(base.Owner.CssClass) && (base.Owner.IsSeparator || base.Owner.Templated))
					{
						list.Add(base.Owner.CssClass);
					}
					if (!string.IsNullOrEmpty(base.Owner.OuterCssClass))
					{
						list.Add(base.Owner.OuterCssClass);
					}
				}
				return list;
			}
		}

		// Token: 0x06003690 RID: 13968 RVA: 0x000B490C File Offset: 0x000B2B0C
		protected override void RenderLink(HtmlTextWriter writer)
		{
			base.RenderLink(writer);
			if (string.IsNullOrEmpty(base.Owner.NavigateUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Owner.ResolveClientUrl(base.Owner.NavigateUrl));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			this.RenderLinkContent(writer, delegate(HtmlTextWriter textWriter)
			{
				this.RenderTextElement(textWriter, "");
			});
			writer.RenderEndTag();
		}
	}
}
