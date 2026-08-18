using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x02000797 RID: 1943
	internal class RibbonBarItemRenderBase : RendererBase
	{
		// Token: 0x06004437 RID: 17463 RVA: 0x000D6450 File Offset: 0x000D4650
		public RibbonBarItemRenderBase(RibbonBarItem owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17001621 RID: 5665
		// (get) Token: 0x06004438 RID: 17464 RVA: 0x000D645F File Offset: 0x000D465F
		// (set) Token: 0x06004439 RID: 17465 RVA: 0x000D6467 File Offset: 0x000D4667
		protected RibbonBarItem Owner { get; set; }

		// Token: 0x0600443A RID: 17466 RVA: 0x000D6470 File Offset: 0x000D4670
		public virtual void RenderBeginTagContext(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Href, "#");
		}

		// Token: 0x0600443B RID: 17467 RVA: 0x000D647F File Offset: 0x000D467F
		public virtual void RenderDropDown(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}
	}
}
