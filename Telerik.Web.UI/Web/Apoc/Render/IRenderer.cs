using System;
using Telerik.Web.Apoc.Image;
using Telerik.Web.Apoc.Layout;
using Telerik.Web.Apoc.Layout.Inline;

namespace Telerik.Web.Apoc.Render
{
	// Token: 0x0200167F RID: 5759
	internal interface IRenderer
	{
		// Token: 0x17004410 RID: 17424
		// (set) Token: 0x0600DE98 RID: 56984
		IRendererOptions Options { set; }

		// Token: 0x0600DE99 RID: 56985
		void StartRenderer();

		// Token: 0x0600DE9A RID: 56986
		void StopRenderer();

		// Token: 0x0600DE9B RID: 56987
		void SetupFontInfo(FontInfo fontInfo);

		// Token: 0x0600DE9C RID: 56988
		void Render(Page page);

		// Token: 0x0600DE9D RID: 56989
		void RenderPage(Page page);

		// Token: 0x0600DE9E RID: 56990
		void RenderAreaContainer(AreaContainer area);

		// Token: 0x0600DE9F RID: 56991
		void RenderBodyAreaContainer(BodyAreaContainer area);

		// Token: 0x0600DEA0 RID: 56992
		void RenderBlockArea(BlockArea area);

		// Token: 0x0600DEA1 RID: 56993
		void RenderSpanArea(SpanArea area);

		// Token: 0x0600DEA2 RID: 56994
		void RenderDisplaySpace(DisplaySpace space);

		// Token: 0x0600DEA3 RID: 56995
		void RenderForeignObjectArea(ForeignObjectArea area);

		// Token: 0x0600DEA4 RID: 56996
		void RenderImageArea(ImageArea area);

		// Token: 0x0600DEA5 RID: 56997
		void RenderWordArea(WordArea area);

		// Token: 0x0600DEA6 RID: 56998
		void RenderInlineSpace(InlineSpace space);

		// Token: 0x0600DEA7 RID: 56999
		void RenderLineArea(LineArea area);

		// Token: 0x0600DEA8 RID: 57000
		void RenderLeaderArea(LeaderArea area);
	}
}
