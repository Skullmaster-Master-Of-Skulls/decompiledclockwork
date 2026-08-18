using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.ActionPlan;

namespace TechnoPro.Common.ICore.Legacy
{
	// Token: 0x02000074 RID: 116
	public interface ILegacyActionPlanManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000344 RID: 836
		int CreateActionPlanNote(ActionPlanNote note);

		// Token: 0x06000345 RID: 837
		void UpdateActionPlanNote(ActionPlanNote note);

		// Token: 0x06000346 RID: 838
		void DeleteActionPlanNote(int noteId);

		// Token: 0x06000347 RID: 839
		IList<ActionPlanNote> LoadNotes(int pid);

		// Token: 0x06000348 RID: 840
		void UpdateActionPlanTask(ActionPlanTask task);

		// Token: 0x06000349 RID: 841
		int CreateActionPlanTask(ActionPlanTask task);

		// Token: 0x0600034A RID: 842
		void DeleteActionPlanTask(int taskId);

		// Token: 0x0600034B RID: 843
		IList<ActionPlanTask> LoadTasks(int pid);
	}
}
