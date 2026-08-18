using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000789 RID: 1929
	internal class RibbonBarApplicationMenuItemRenderBase : RendererBase
	{
		// Token: 0x060043E1 RID: 17377 RVA: 0x000D48EE File Offset: 0x000D2AEE
		public RibbonBarApplicationMenuItemRenderBase(RibbonBarApplicationMenuItemBase owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001618 RID: 5656
		// (get) Token: 0x060043E2 RID: 17378 RVA: 0x000D48FD File Offset: 0x000D2AFD
		// (set) Token: 0x060043E3 RID: 17379 RVA: 0x000D4905 File Offset: 0x000D2B05
		protected RibbonBarApplicationMenuItemBase Owner { get; set; }

		// Token: 0x17001619 RID: 5657
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x000D490E File Offset: 0x000D2B0E
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x000D4914 File Offset: 0x000D2B14
		public override void AddAttributesToRender(HtmlTextWriter writer)
		{
			string cssClass = this.Owner.CssClass;
			string text = this.Owner.Enabled ? string.Empty : "rrbDisabled";
			this.Owner.CssClass = RibbonBarStyles.Combine(new string[]
			{
				this.GetItemCssClassToRender(),
				text,
				this.Owner.CssClass
			});
			this.Owner.BaseAddAttributesToRender(writer);
			this.Owner.CssClass = cssClass;
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x000D4992 File Offset: 0x000D2B92
		protected virtual string GetItemCssClassToRender()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x000D4999 File Offset: 0x000D2B99
		protected void RenderKeyboardBox(HtmlTextWriter writer, string text)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "rrbKeyBox");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.Write(text);
			writer.RenderEndTag();
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x000D49BD File Offset: 0x000D2BBD
		protected virtual void RenderInnerContents(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
