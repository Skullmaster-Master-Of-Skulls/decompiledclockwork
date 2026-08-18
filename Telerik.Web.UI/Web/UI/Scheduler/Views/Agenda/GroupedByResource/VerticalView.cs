using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource
{
	// Token: 0x0200083E RID: 2110
	internal class VerticalView : View
	{
		// Token: 0x06004E1E RID: 19998 RVA: 0x000F4D01 File Offset: 0x000F2F01
		public VerticalView(ModelBase model) : base(model)
		{
		}

		// Token: 0x06004E1F RID: 19999 RVA: 0x000F4D0C File Offset: 0x000F2F0C
		protected override IList<ViewHeader> CreateColumnHeaders()
		{
			List<ViewHeader> list = new List<ViewHeader>();
			base.AddDateHeader(list);
			list.AddRange(base.CreateColumnHeaders());
			return list;
		}
	}
}
