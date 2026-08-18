using System;
using Telerik.Web.UI.SchedulerReminderDialog.Classic;
using Telerik.Web.UI.SchedulerReminderDialog.Lite;
using Telerik.Web.UI.SchedulerReminderDialog.Native;

namespace Telerik.Web.UI.SchedulerReminderDialog
{
	// Token: 0x0200080B RID: 2059
	internal class RendererFactory
	{
		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06004B50 RID: 19280 RVA: 0x000EB9D4 File Offset: 0x000E9BD4
		public ReminderDialog Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06004B51 RID: 19281 RVA: 0x000EB9DC File Offset: 0x000E9BDC
		public RendererFactory(ReminderDialog owner)
		{
			this._owner = owner;
		}

		// Token: 0x06004B52 RID: 19282 RVA: 0x000EB9EC File Offset: 0x000E9BEC
		public IReminderRenderer CreateRenderer()
		{
			switch (this.Owner.ResolvedRenderMode)
			{
			case RenderMode.Lightweight:
				return new Telerik.Web.UI.SchedulerReminderDialog.Lite.Renderer(this.Owner);
			case RenderMode.Mobile:
				return new Telerik.Web.UI.SchedulerReminderDialog.Native.Renderer(this.Owner);
			}
			return new Telerik.Web.UI.SchedulerReminderDialog.Classic.Renderer(this.Owner);
		}

		// Token: 0x0400130D RID: 4877
		private readonly ReminderDialog _owner;
	}
}
