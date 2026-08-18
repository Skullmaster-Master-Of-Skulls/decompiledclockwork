using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Agenda.GroupedByResource
{
	// Token: 0x02000837 RID: 2103
	internal class HorizontalView : View
	{
		// Token: 0x06004DEA RID: 19946 RVA: 0x000F4A9B File Offset: 0x000F2C9B
		public HorizontalView(ModelBase model) : base(model)
		{
		}

		// Token: 0x06004DEB RID: 19947 RVA: 0x000F4AA4 File Offset: 0x000F2CA4
		protected override IList<ViewHeader> CreateColumnHeaders()
		{
			List<ViewHeader> list = new List<ViewHeader>();
			base.AddResourceHeader(list);
			base.AddDateHeader(list);
			list.AddRange(base.CreateColumnHeaders());
			return list;
		}
	}
}
