using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI.Menu.Renderers
{
	// Token: 0x020005DE RID: 1502
	public class MenuItemLiteRenderer : MenuItemRenderer
	{
		// Token: 0x0600369C RID: 13980 RVA: 0x000B4E36 File Offset: 0x000B3036
		public MenuItemLiteRenderer(RadMenuItem owner) : base(owner)
		{
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x000B4E3F File Offset: 0x000B303F
		public override string TemplateContainerClassName
		{
			get
			{
				return "rmContent";
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x0600369E RID: 13982 RVA: 0x000B4E48 File Offset: 0x000B3048
		public override List<string> CssClass
		{
			get
			{
				List<string> list = new List<string>();
				list.AddRange(base.CssClass);
				if (!base.Owner.IsSeparator)
				{
					list.AddRange(this.ResolvedStateClasses);
				}
				return list;
			}
		}

		// Token: 0x0600369F RID: 13983 RVA: 0x000B4E84 File Offset: 0x000B3084
		protected override void RenderLink(HtmlTextWriter writer)
		{
			base.RenderLink(writer);
			if (string.IsNullOrEmpty(base.Owner.NavigateUrl))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Tabindex, "0");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Owner.ResolveClientUrl(base.Owner.NavigateUrl));
				writer.RenderBeginTag(HtmlTextWriterTag.A);
			}
			this.RenderLinkContent(writer, new Action<HtmlTextWriter>(this.RenderText));
			writer.RenderEndTag();
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000B4F00 File Offset: 0x000B3100
		protected virtual void RenderText(HtmlTextWriter writer)
		{
			string text = base.Menu.EnableTextHTMLEncoding ? HttpUtility.HtmlEncode(base.Owner.Text) : base.Owner.Text;
			if (string.IsNullOrEmpty(base.Owner.CurrentImageUrl) && !base.Owner.ShouldRenderImagePlaceholder && !base.Owner.ShouldRenderToggleButton)
			{
				writer.Write(text);
				return;
			}
			if (!string.IsNullOrEmpty(base.Owner.Text))
			{
				this.RenderTextElement(writer, text);
			}
		}
	}
}
