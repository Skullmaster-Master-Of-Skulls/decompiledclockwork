using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000329 RID: 809
	public interface IViewSettings
	{
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001B05 RID: 6917
		// (set) Token: 0x06001B06 RID: 6918
		bool UserSelectable { get; set; }

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001B07 RID: 6919
		GanttViewType Type { get; }
	}
}
