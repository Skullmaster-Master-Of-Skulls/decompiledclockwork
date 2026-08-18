using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkDailyJob;

namespace TechnoPro.Common.ICore.ClockWorkDailyJob
{
	// Token: 0x020000B6 RID: 182
	public interface IDailyJobManager : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600056C RID: 1388
		IList<DailyJobTask> LoadDailyJobTasks();

		// Token: 0x0600056D RID: 1389
		DailyJobTask LoadDailyJobTaskById(int WindowsTaskJobId);

		// Token: 0x0600056E RID: 1390
		void UpdateDailyJobTask(DailyJobTask Task);

		// Token: 0x0600056F RID: 1391
		int CreateDailyJobTask(DailyJobTask Task);

		// Token: 0x06000570 RID: 1392
		void DeleteDailyJobTask(int WindowsTaskJobId);

		// Token: 0x06000571 RID: 1393
		void ChangeTaskActiveStatus(int WindowsTaskJobId, bool NewIsActive);

		// Token: 0x06000572 RID: 1394
		IList<int> GetActiveDailyJobGroups();

		// Token: 0x06000573 RID: 1395
		IList<DailyJobTaskResult> RunDailyJob(int GroupId);

		// Token: 0x06000574 RID: 1396
		IList<DailyJobTask> LoadDailyJobTasksByGroup(int GroupId);
	}
}
