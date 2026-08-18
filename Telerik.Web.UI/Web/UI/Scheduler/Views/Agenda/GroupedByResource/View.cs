using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource
{
	// Token: 0x0200082A RID: 2090
	internal class View : View
	{
		// Token: 0x06004D53 RID: 19795 RVA: 0x000F3067 File Offset: 0x000F1267
		public View(ModelBase model) : base(model)
		{
		}

		// Token: 0x06004D54 RID: 19796 RVA: 0x000F3070 File Offset: 0x000F1270
		protected void AddResourceHeader(IList<ViewHeader> headers)
		{
			if (this.Owner.AgendaView.ShowResourceHeadersResolved)
			{
				base.AddHeader(headers, this.Owner.Localization.HeaderAgendaResource);
			}
		}
	}
}
