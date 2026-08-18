using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200032E RID: 814
	public interface IGantt
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001B0D RID: 6925
		// (set) Token: 0x06001B0E RID: 6926
		bool AutoGenerateColumns { get; set; }

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001B0F RID: 6927
		// (set) Token: 0x06001B10 RID: 6928
		GanttViewType SelectedView { get; set; }

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001B11 RID: 6929
		GanttDataBindings DataBindings { get; }

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001B12 RID: 6930
		DataSourceView TasksView { get; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001B13 RID: 6931
		DataSourceView DependenciesView { get; }

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001B14 RID: 6932
		DataSourceView ResourcesView { get; }

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001B15 RID: 6933
		DataSourceView AssignmentsView { get; }

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001B16 RID: 6934
		TaskCollection Tasks { get; }

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001B17 RID: 6935
		DependencyCollection Dependencies { get; }

		// Token: 0x06001B18 RID: 6936
		IList<ITask> GetAllTasks();
	}
}
