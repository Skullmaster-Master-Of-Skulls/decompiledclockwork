using System;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007CC RID: 1996
	internal class MenuButtonWrap : RibbonBarButton
	{
		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x060045A2 RID: 17826 RVA: 0x000DB6D8 File Offset: 0x000D98D8
		public override RibbonBarImageRenderingMode ImageRenderingMode
		{
			get
			{
				return RibbonBarImageRenderingMode.Dual;
			}
		}

		// Token: 0x17001677 RID: 5751
		// (get) Token: 0x060045A3 RID: 17827 RVA: 0x000DB6DB File Offset: 0x000D98DB
		public override RibbonBarItemSize Size
		{
			get
			{
				return RibbonBarItemSize.Medium;
			}
		}

		// Token: 0x17001678 RID: 5752
		// (get) Token: 0x060045A4 RID: 17828 RVA: 0x000DB6DE File Offset: 0x000D98DE
		internal override bool ShouldRenderTextStructure
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001679 RID: 5753
		// (get) Token: 0x060045A5 RID: 17829 RVA: 0x000DB6E1 File Offset: 0x000D98E1
		internal override bool ShouldRenderTextContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x000DB6E4 File Offset: 0x000D98E4
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarMenuButtonWrapLiteRenderer(this);
			}
			return new RibbonBarMenuButtonWrapClassicRenderer(this);
		}

		// Token: 0x1700167A RID: 5754
		// (get) Token: 0x060045A7 RID: 17831 RVA: 0x000DB701 File Offset: 0x000D9901
		// (set) Token: 0x060045A8 RID: 17832 RVA: 0x000DB709 File Offset: 0x000D9909
		public string NavigateUrl { get; set; }
	}
}
