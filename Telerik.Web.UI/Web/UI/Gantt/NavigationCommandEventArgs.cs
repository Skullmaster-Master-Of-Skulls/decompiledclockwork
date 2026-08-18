using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000339 RID: 825
	public class NavigationCommandEventArgs : EventArgs, INavigationEvent
	{
		// Token: 0x06001C43 RID: 7235 RVA: 0x0005A374 File Offset: 0x00058574
		public NavigationCommandEventArgs(GanttNavigationCommand command)
		{
			this._command = command;
		}

		// Token: 0x1700099A RID: 2458
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x0005A383 File Offset: 0x00058583
		public GanttNavigationCommand Command
		{
			get
			{
				return this._command;
			}
		}

		// Token: 0x04000737 RID: 1847
		private readonly GanttNavigationCommand _command;
	}
}
