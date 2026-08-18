using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x02000840 RID: 2112
	internal class HorizontalView : View
	{
		// Token: 0x06004E21 RID: 20001 RVA: 0x000F4D3C File Offset: 0x000F2F3C
		public HorizontalView(ModelBase model) : base(model)
		{
		}

		// Token: 0x06004E22 RID: 20002 RVA: 0x000F4D48 File Offset: 0x000F2F48
		protected override IList<ViewHeader> CreateColumnHeaders()
		{
			List<ViewHeader> list = new List<ViewHeader>();
			base.AddDateHeader(list);
			list.AddRange(base.CreateColumnHeaders());
			return list;
		}
	}
}
