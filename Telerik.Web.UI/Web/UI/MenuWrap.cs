using System;
using Telerik.Web.UI.RibbonBar.Renderers;

namespace Telerik.Web.UI
{
	// Token: 0x020007CF RID: 1999
	internal class MenuWrap : RibbonBarMenu
	{
		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x060045BE RID: 17854 RVA: 0x000DBA18 File Offset: 0x000D9C18
		internal override string RibbonBarItemTypeCssClass
		{
			get
			{
				return "rrbSubMenu";
			}
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x060045BF RID: 17855 RVA: 0x000DBA1F File Offset: 0x000D9C1F
		public override RibbonBarImageRenderingMode ImageRenderingMode
		{
			get
			{
				return RibbonBarImageRenderingMode.Dual;
			}
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x060045C0 RID: 17856 RVA: 0x000DBA22 File Offset: 0x000D9C22
		public override RibbonBarItemSize Size
		{
			get
			{
				return RibbonBarItemSize.Medium;
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x060045C1 RID: 17857 RVA: 0x000DBA25 File Offset: 0x000D9C25
		internal override bool ShouldRenderTextStructure
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x060045C2 RID: 17858 RVA: 0x000DBA28 File Offset: 0x000D9C28
		internal override bool ShouldRenderTextContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060045C3 RID: 17859 RVA: 0x000DBA2B File Offset: 0x000D9C2B
		protected override IRenderer CreateControlRenderer()
		{
			if (base.RibbonBar.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new RibbonBarMenuWrapLiteRenderer(this);
			}
			return new RibbonBarMenuWrapClassicRenderer(this);
		}
	}
}
