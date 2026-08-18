using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Legacy;
using TechnoPro.Common.DAO.Legacy;
using TechnoPro.Common.ICore.Legacy;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Legacy.ActionPlan;

namespace TechnoPro.Common.Core.Legacy
{
	// Token: 0x020000DA RID: 218
	public class LegacyActionPlanManager : ILegacyActionPlanManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x000387CD File Offset: 0x000369CD
		public LegacyActionPlanManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x000387DF File Offset: 0x000369DF
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x000387E7 File Offset: 0x000369E7
		public OperationContext OpContext { get; set; }

		// Token: 0x0600085A RID: 2138 RVA: 0x000387F0 File Offset: 0x000369F0
		public int CreateActionPlanNote(ActionPlanNote note)
		{
			ILegacyActionPlanDAO legacyActionPlanDAO = new LegacyActionPlanDAO(this.OpContext);
			return legacyActionPlanDAO.CreateActionPlanNote(note);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x00038818 File Offset: 0x00036A18
		public void UpdateActionPlanNote(ActionPlanNote note)
		{
			ILegacyActionPlanDAO legacyActionPlanDAO = new LegacyActionPlanDAO(this.OpContext);
			legacyActionPlanDAO.UpdateActionPlanNote(note);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x0003883C File Offset: 0x00036A3C
		public void DeleteActionPlanNote(int noteId)
		{
			ILegacyActionPlanDAO legacyActionPlanDAO = new LegacyActionPlanDAO(this.OpContext);
			legacyActionPlanDAO.DeleteActionPlanNote(noteId);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00038860 File Offset: 0x00036A60
		public IList<ActionPlanNote> LoadNotes(int pid)
		{
			ILegacyActionPlanDAO legacyActionPlanDAO = new LegacyActionPlanDAO(this.OpContext);
			return legacyActionPlanDAO.LoadNotes(pid);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00038888 File Offset: 0x00036A88
		public void UpdateActionPlanTask(ActionPlanTask task)
		{
			ILegacyActionPlanDAO legacyActionPlanDAO = new LegacyActionPlanDAO(this.OpContext);
			legacyActionPlanDAO.UpdateActionPlanTask(task);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000388AC File Offset: 0x00036AAC
		public int CreateActionPlanTask(ActionPlanTask task)
		{
			ILegacyActionPlanDAO legacyActionPlanDAO = new LegacyActionPlanDAO(this.OpContext);
			return legacyActionPlanDAO.CreateActionPlanTask(task);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x000388D4 File Offset: 0x00036AD4
		public void DeleteActionPlanTask(int taskId)
		{
			ILegacyActionPlanDAO legacyActionPlanDAO = new LegacyActionPlanDAO(this.OpContext);
			legacyActionPlanDAO.DeleteActionPlanTask(taskId);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x000388F8 File Offset: 0x00036AF8
		public IList<ActionPlanTask> LoadTasks(int pid)
		{
			ILegacyActionPlanDAO legacyActionPlanDAO = new LegacyActionPlanDAO(this.OpContext);
			return legacyActionPlanDAO.LoadTasks(pid);
		}
	}
}
