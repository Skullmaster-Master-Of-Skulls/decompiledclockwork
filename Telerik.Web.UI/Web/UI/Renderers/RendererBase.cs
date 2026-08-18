using System;
using System.Web.UI;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI.Renderers
{
	// Token: 0x02000015 RID: 21
	public abstract class RendererBase : IRenderer
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00003B9A File Offset: 0x00001D9A
		protected virtual void RenderTrialMessage(HtmlTextWriter writer)
		{
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003B9C File Offset: 0x00001D9C
		public virtual HtmlTextWriterTag TagKey
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00003BA3 File Offset: 0x00001DA3
		public virtual string CssClassFormatString
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00003BAA File Offset: 0x00001DAA
		public virtual void AddAttributesToRender(HtmlTextWriter writer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00003BB1 File Offset: 0x00001DB1
		public virtual void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			BaseClass.RenderVersionStamp(writer);
		}
	}
}
