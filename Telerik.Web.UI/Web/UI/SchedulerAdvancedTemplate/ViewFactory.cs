using System;
using Telerik.Web.UI.SchedulerAdvancedTemplate.Classic;
using Telerik.Web.UI.SchedulerAdvancedTemplate.Lite;
using Telerik.Web.UI.SchedulerAdvancedTemplate.Native;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate
{
	// Token: 0x0200081B RID: 2075
	internal class ViewFactory : IViewFactory
	{
		// Token: 0x17001905 RID: 6405
		// (get) Token: 0x06004CAE RID: 19630 RVA: 0x000F102C File Offset: 0x000EF22C
		public AdvancedTemplate Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06004CAF RID: 19631 RVA: 0x000F1034 File Offset: 0x000EF234
		public ViewFactory(AdvancedTemplate owner)
		{
			this._owner = owner;
		}

		// Token: 0x06004CB0 RID: 19632 RVA: 0x000F1044 File Offset: 0x000EF244
		public IAdvancedTemplateView CreateView()
		{
			switch (this.Owner.ResolvedRenderMode)
			{
			case RenderMode.Lightweight:
				return new Telerik.Web.UI.SchedulerAdvancedTemplate.Lite.View(this.Owner);
			case RenderMode.Mobile:
				return new Telerik.Web.UI.SchedulerAdvancedTemplate.Native.View(this.Owner);
			}
			return new Telerik.Web.UI.SchedulerAdvancedTemplate.Classic.View(this.Owner);
		}

		// Token: 0x0400133D RID: 4925
		private readonly AdvancedTemplate _owner;
	}
}
