using System;
using System.Collections.Generic;
using TechnoPro.Common.DAO.Impl.Tasks;
using TechnoPro.Common.DAO.Tasks;
using TechnoPro.Common.ICore.Tasks;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Tasks;

namespace TechnoPro.Common.Core.Tasks
{
	// Token: 0x02000036 RID: 54
	public class TaskGroupManager : ITaskGroupManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000BD32 File Offset: 0x00009F32
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000BD3A File Offset: 0x00009F3A
		public ITaskGroupDAO dao { get; set; }

		// Token: 0x06000223 RID: 547 RVA: 0x0000BD43 File Offset: 0x00009F43
		public TaskGroupManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new TaskGroupDAO(opContext);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000BD62 File Offset: 0x00009F62
		// (set) Token: 0x06000225 RID: 549 RVA: 0x0000BD6A File Offset: 0x00009F6A
		public OperationContext OpContext { get; set; }

		// Token: 0x06000226 RID: 550 RVA: 0x0000BD74 File Offset: 0x00009F74
		public int CreateNewTaskGroup(TaskGroup Group)
		{
			return this.dao.CreateNewTaskGroup(Group);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000BD92 File Offset: 0x00009F92
		public void DeleteTaskGroup(int TaskGroupId)
		{
			this.dao.DeleteTaskGroup(TaskGroupId);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000BDA2 File Offset: 0x00009FA2
		public void UpdateTaskGroup(TaskGroup Group)
		{
			this.dao.UpdateTaskGroup(Group);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000BDB4 File Offset: 0x00009FB4
		public List<TaskGroup> LoadGroups(bool IncludePrivate, bool IncludeShared)
		{
			return this.dao.LoadGroups(IncludePrivate, IncludeShared);
		}
	}
}
