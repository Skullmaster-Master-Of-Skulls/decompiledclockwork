using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Navigation
{
	// Token: 0x02000628 RID: 1576
	public class ItemRenderer : NavigationNodeRendererBase
	{
		// Token: 0x06003967 RID: 14695 RVA: 0x000BC907 File Offset: 0x000BAB07
		public ItemRenderer(NavigationNode node)
		{
			base.Node = node;
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x000BC916 File Offset: 0x000BAB16
		public ItemRenderer()
		{
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x000BC91E File Offset: 0x000BAB1E
		public override void RenderContents(HtmlTextWriter writer)
		{
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.RenderLink(writer);
			base.RenderContents(writer);
			writer.RenderEndTag();
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x000BC944 File Offset: 0x000BAB44
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (base.Node.Width != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.Node.Width.ToString());
				base.Node.Width = Unit.Empty;
			}
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x000BC9A0 File Offset: 0x000BABA0
		protected override void RenderLink(HtmlTextWriter writer)
		{
			base.RenderLink(writer);
			if (base.Node.IsTemplateInstantiated)
			{
				base.Node.RenderTemplate(writer);
			}
			else if (string.IsNullOrEmpty(base.Node.NavigateUrl))
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, base.Node.ResolveClientUrl(base.Node.NavigateUrl));
				writer.RenderBeginTag(HtmlTextWriterTag.A);
			}
			if (!base.Node.IsTemplateInstantiated)
			{
				this.RenderLinkContent(writer);
				writer.RenderEndTag();
			}
		}
	}
}
