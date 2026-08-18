using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.ActionPlan;

namespace TechnoPro.Common.DAO.Legacy
{
	// Token: 0x0200005F RID: 95
	public interface ILegacyActionPlanDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000225 RID: 549
		int CreateActionPlanNote(ActionPlanNote note);

		// Token: 0x06000226 RID: 550
		void UpdateActionPlanNote(ActionPlanNote note);

		// Token: 0x06000227 RID: 551
		void DeleteActionPlanNote(int noteId);

		// Token: 0x06000228 RID: 552
		IList<ActionPlanNote> LoadNotes(int pid);

		// Token: 0x06000229 RID: 553
		void UpdateActionPlanTask(ActionPlanTask task);

		// Token: 0x0600022A RID: 554
		int CreateActionPlanTask(ActionPlanTask task);

		// Token: 0x0600022B RID: 555
		void DeleteActionPlanTask(int taskId);

		// Token: 0x0600022C RID: 556
		IList<ActionPlanTask> LoadTasks(int pid);
	}
}
