using System;
using System.Collections.Generic;
using Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByDate
{
	// Token: 0x02000836 RID: 2102
	internal class VerticalView : View
	{
		// Token: 0x06004DE8 RID: 19944 RVA: 0x000F4A68 File Offset: 0x000F2C68
		public VerticalView(ModelBase model) : base(model)
		{
		}

		// Token: 0x06004DE9 RID: 19945 RVA: 0x000F4A74 File Offset: 0x000F2C74
		protected override IList<ViewHeader> CreateColumnHeaders()
		{
			List<ViewHeader> list = new List<ViewHeader>();
			base.AddResourceHeader(list);
			list.AddRange(base.CreateColumnHeaders());
			return list;
		}
	}
}
