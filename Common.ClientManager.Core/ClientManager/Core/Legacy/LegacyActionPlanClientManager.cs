using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.ActionPlan;
using TechnoPro.Common.ClientManager.ICore.Legacy;
using TechnoPro.Common.Public;

namespace TechnoPro.Common.ClientManager.Core.Legacy
{
	// Token: 0x02000048 RID: 72
	public class LegacyActionPlanClientManager : ILegacyActionPlanClientManager, IWebService
	{
		// Token: 0x06000297 RID: 663 RVA: 0x0000387F File Offset: 0x00001A7F
		public int CreateActionPlanNote(ActionPlanNoteDTO note)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000387F File Offset: 0x00001A7F
		public void UpdateActionPlanNote(ActionPlanNoteDTO note)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000387F File Offset: 0x00001A7F
		public void DeleteActionPlanNote(int noteId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000387F File Offset: 0x00001A7F
		public IList<ActionPlanNoteDTO> LoadNotes(int pid)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000387F File Offset: 0x00001A7F
		public void UpdateActionPlanTask(ActionPlanTaskDTO task)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000387F File Offset: 0x00001A7F
		public int CreateActionPlanTask(ActionPlanTaskDTO task)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000387F File Offset: 0x00001A7F
		public void DeleteActionPlanTask(int taskId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000387F File Offset: 0x00001A7F
		public IList<ActionPlanTaskDTO> LoadTasks(int pid)
		{
			throw new NotImplementedException();
		}
	}
}
