using System;
using System.Collections.Generic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkDailyJob;

namespace TechnoPro.Common.DAO.ClockWorkDailyJob
{
	// Token: 0x0200009B RID: 155
	public interface IDailyJobDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000401 RID: 1025
		IList<DailyJobTask> LoadDailyJobTasks();

		// Token: 0x06000402 RID: 1026
		DailyJobTask LoadDailyJobTaskById(int WindowsTaskJobId);

		// Token: 0x06000403 RID: 1027
		void UpdateDailyJobTask(DailyJobTask Task);

		// Token: 0x06000404 RID: 1028
		int CreateDailyJobTask(DailyJobTask Task);

		// Token: 0x06000405 RID: 1029
		void DeleteDailyJobTask(int WindowsTaskJobId);

		// Token: 0x06000406 RID: 1030
		void ChangeTaskActiveStatus(int WindowsTaskJobId, bool NewIsActive);

		// Token: 0x06000407 RID: 1031
		IList<int> GetActiveDailyJobGroups();

		// Token: 0x06000408 RID: 1032
		IList<DailyJobTask> LoadDailyJobTasksByGroup(int GroupId);

		// Token: 0x06000409 RID: 1033
		int LogDailyJobRunStart(int TaskGroupId);

		// Token: 0x0600040A RID: 1034
		void LogDailyJobRunEnd(int WindowsTaskJobSetResultsId, string runResult, string runComment);

		// Token: 0x0600040B RID: 1035
		void LogDailyJobTaskRunEnd(int WindowsTaskJobResultId, int WindowsTaskJobId, DateTime StartDate, bool Successful, string Result);

		// Token: 0x0600040C RID: 1036
		int LogDailyJobTaskRunStart(int WindowsTaskJobId, int ReportId, int TaskGroupId);
	}
}
