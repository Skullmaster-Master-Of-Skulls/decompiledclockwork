using System;
using Telerik.Web.UI.SchedulerAdvancedTemplate.Classic;
using Telerik.Web.UI.SchedulerAdvancedTemplate.Lite;
using Telerik.Web.UI.SchedulerAdvancedTemplate.Native;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate
{
	// Token: 0x02000814 RID: 2068
	internal class RendererFactory
	{
		// Token: 0x170018BE RID: 6334
		// (get) Token: 0x06004BDD RID: 19421 RVA: 0x000EDDE9 File Offset: 0x000EBFE9
		public AdvancedTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x000EDDF1 File Offset: 0x000EBFF1
		public RendererFactory(AdvancedTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x000EDE00 File Offset: 0x000EC000
		public IAdvancedTemplateRenderer CreateRenderer()
		{
			switch (this.Owner.ResolvedRenderMode)
			{
			case RenderMode.Lightweight:
				return new Telerik.Web.UI.SchedulerAdvancedTemplate.Lite.Renderer(this.Owner.View);
			case RenderMode.Mobile:
				return new Telerik.Web.UI.SchedulerAdvancedTemplate.Native.Renderer(this.Owner.View);
			}
			return new Telerik.Web.UI.SchedulerAdvancedTemplate.Classic.Renderer(this.Owner.View);
		}

		// Token: 0x04001322 RID: 4898
		private readonly AdvancedTemplate _owner;
	}
}
