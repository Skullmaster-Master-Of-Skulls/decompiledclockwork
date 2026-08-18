using System;
using System.Web.UI;
using Telerik.Web.UI.Renderers;

namespace Telerik.Web.UI.ButtonRendering
{
	// Token: 0x020000DF RID: 223
	public abstract class ButtonRendererBase : RendererBase
	{
		// Token: 0x06000919 RID: 2329 RVA: 0x000213E8 File Offset: 0x0001F5E8
		public ButtonRendererBase(RadButton owner)
		{
			this.Owner = owner;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x000213F7 File Offset: 0x0001F5F7
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x000213FF File Offset: 0x0001F5FF
		private protected RadButton Owner { protected get; private set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600091C RID: 2332 RVA: 0x00021408 File Offset: 0x0001F608
		public override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}
	}
}
