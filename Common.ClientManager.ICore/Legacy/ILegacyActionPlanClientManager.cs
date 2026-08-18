using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ActionPlan;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.ICore.Legacy
{
	// Token: 0x02000043 RID: 67
	public interface ILegacyActionPlanClientManager : IWebService
	{
		// Token: 0x060001E2 RID: 482
		int CreateActionPlanNote(ActionPlanNoteDTO note);

		// Token: 0x060001E3 RID: 483
		void UpdateActionPlanNote(ActionPlanNoteDTO note);

		// Token: 0x060001E4 RID: 484
		void DeleteActionPlanNote(int noteId);

		// Token: 0x060001E5 RID: 485
		IList<ActionPlanNoteDTO> LoadNotes(int pid);

		// Token: 0x060001E6 RID: 486
		void UpdateActionPlanTask(ActionPlanTaskDTO task);

		// Token: 0x060001E7 RID: 487
		int CreateActionPlanTask(ActionPlanTaskDTO task);

		// Token: 0x060001E8 RID: 488
		void DeleteActionPlanTask(int taskId);

		// Token: 0x060001E9 RID: 489
		IList<ActionPlanTaskDTO> LoadTasks(int pid);
	}
}
