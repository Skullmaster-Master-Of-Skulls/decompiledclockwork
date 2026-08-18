using System;
using System.Collections.Generic;
using Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByDate
{
	// Token: 0x0200082B RID: 2091
	internal class HorizontalView : View
	{
		// Token: 0x06004D55 RID: 19797 RVA: 0x000F309B File Offset: 0x000F129B
		public HorizontalView(ModelBase model) : base(model)
		{
		}

		// Token: 0x06004D56 RID: 19798 RVA: 0x000F30A4 File Offset: 0x000F12A4
		protected override IList<ViewHeader> CreateColumnHeaders()
		{
			List<ViewHeader> list = new List<ViewHeader>();
			base.AddDateHeader(list);
			base.AddResourceHeader(list);
			list.AddRange(base.CreateColumnHeaders());
			return list;
		}
	}
}
