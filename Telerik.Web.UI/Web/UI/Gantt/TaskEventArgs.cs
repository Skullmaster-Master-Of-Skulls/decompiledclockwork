using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000337 RID: 823
	public class TaskEventArgs : EventArgs, ITaskEvent
	{
		// Token: 0x06001C3E RID: 7230 RVA: 0x0005A34C File Offset: 0x0005854C
		public TaskEventArgs(IEnumerable<ITask> tasks)
		{
			this._tasks = tasks;
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001C3F RID: 7231 RVA: 0x0005A35B File Offset: 0x0005855B
		// (set) Token: 0x06001C40 RID: 7232 RVA: 0x0005A363 File Offset: 0x00058563
		public bool Cancel { get; set; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x0005A36C File Offset: 0x0005856C
		public IEnumerable<ITask> Tasks
		{
			get
			{
				return this._tasks;
			}
		}

		// Token: 0x04000735 RID: 1845
		private readonly IEnumerable<ITask> _tasks;
	}
}
