using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000787 RID: 1927
	internal class RibbonBarApplicationMenuRenderBase : RendererBase
	{
		// Token: 0x060043CE RID: 17358 RVA: 0x000D4468 File Offset: 0x000D2668
		public RibbonBarApplicationMenuRenderBase(RibbonBarApplicationMenu owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001616 RID: 5654
		// (get) Token: 0x060043CF RID: 17359 RVA: 0x000D4477 File Offset: 0x000D2677
		// (set) Token: 0x060043D0 RID: 17360 RVA: 0x000D447F File Offset: 0x000D267F
		protected RibbonBarApplicationMenu Owner { get; set; }

		// Token: 0x17001617 RID: 5655
		// (get) Token: 0x060043D1 RID: 17361 RVA: 0x000D4488 File Offset: 0x000D2688
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x000D448C File Offset: 0x000D268C
		public virtual string GetItemCssClassToRender()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x000D4493 File Offset: 0x000D2693
		public virtual string GetFooterPaneCssClass()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x000D449A File Offset: 0x000D269A
		public virtual string GetAuxiliaryPaneContentCssClass()
		{
			throw new NotImplementedException();
		}
	}
}
