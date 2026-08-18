using System;
using System.Web.UI;
using Telerik.Web.UI.Gantt;

namespace Telerik.Web.UI
{
	// Token: 0x02000046 RID: 70
	[ParseChildren(typeof(GanttToolbarItem))]
	public class GanttToolbarItemsCollection : BaseCollection<GanttToolbarItem>
	{
		// Token: 0x06000246 RID: 582 RVA: 0x000064E8 File Offset: 0x000046E8
		public GanttToolbarItemsCollection()
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000064F0 File Offset: 0x000046F0
		public GanttToolbarItemsCollection(IGantt owner) : base(owner)
		{
		}
	}
}
