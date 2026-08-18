using System;
using System.Web.UI;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007AD RID: 1965
	internal class RibbonBarMenuButtonWrapLiteRenderer : RibbonBarButtonLiteRenderer
	{
		// Token: 0x060044B2 RID: 17586 RVA: 0x000D8C0E File Offset: 0x000D6E0E
		public RibbonBarMenuButtonWrapLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001634 RID: 5684
		// (get) Token: 0x060044B3 RID: 17587 RVA: 0x000D8C17 File Offset: 0x000D6E17
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.A;
			}
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x000D8C1C File Offset: 0x000D6E1C
		public override void RenderBeginTagContext(HtmlTextWriter writer)
		{
			string value = "#";
			if (!string.IsNullOrEmpty(((MenuButtonWrap)base.Owner).NavigateUrl) && ((MenuButtonWrap)base.Owner).NavigateUrl != "#")
			{
				value = base.Owner.ResolveUrl(((MenuButtonWrap)base.Owner).NavigateUrl);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Href, value);
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x000D8C87 File Offset: 0x000D6E87
		protected override void RenderImage(HtmlTextWriter writer)
		{
			if (!this.ImageUrlToRender.Contains("WebResource.axd"))
			{
				base.RenderImage(writer);
			}
		}
	}
}
