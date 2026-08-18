using System;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000343 RID: 835
	public interface ITaskData : ITaskBase
	{
		// Token: 0x06001C74 RID: 7284
		void CopyFrom(ITask srcTask);

		// Token: 0x06001C75 RID: 7285
		void CopyTo(ITask destTask);
	}
}
