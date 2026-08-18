using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechnoPro.Common.Core.Reports;
using TechnoPro.Common.DAO.ClockWorkDailyJob;
using TechnoPro.Common.DAO.Impl.ClockWorkDailyJob;
using TechnoPro.Common.ICore.ClockWorkDailyJob;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkDailyJob;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.ClockWorkDailyJob
{
	// Token: 0x02000120 RID: 288
	public class DailyJobManager : IDailyJobManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000C24 RID: 3108 RVA: 0x000554CD File Offset: 0x000536CD
		public DailyJobManager(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new DailyJobDAO(opContext);
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x000554EB File Offset: 0x000536EB
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x000554F3 File Offset: 0x000536F3
		public OperationContext OpContext { get; set; }

		// Token: 0x06000C27 RID: 3111 RVA: 0x000554FC File Offset: 0x000536FC
		public IList<DailyJobTask> LoadDailyJobTasks()
		{
			return this.dao.LoadDailyJobTasks();
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0005551C File Offset: 0x0005371C
		public DailyJobTask LoadDailyJobTaskById(int WindowsTaskJobId)
		{
			return this.dao.LoadDailyJobTaskById(WindowsTaskJobId);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0005553A File Offset: 0x0005373A
		public void UpdateDailyJobTask(DailyJobTask Task)
		{
			this.dao.UpdateDailyJobTask(Task);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0005554C File Offset: 0x0005374C
		public int CreateDailyJobTask(DailyJobTask Task)
		{
			return this.dao.CreateDailyJobTask(Task);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0005556A File Offset: 0x0005376A
		public void DeleteDailyJobTask(int WindowsTaskJobId)
		{
			this.dao.DeleteDailyJobTask(WindowsTaskJobId);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0005557A File Offset: 0x0005377A
		public void ChangeTaskActiveStatus(int WindowsTaskJobId, bool NewIsActive)
		{
			this.dao.ChangeTaskActiveStatus(WindowsTaskJobId, NewIsActive);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0005558C File Offset: 0x0005378C
		public IList<int> GetActiveDailyJobGroups()
		{
			return this.dao.GetActiveDailyJobGroups();
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x000555AC File Offset: 0x000537AC
		public IList<DailyJobTask> LoadDailyJobTasksByGroup(int GroupId)
		{
			return this.dao.LoadDailyJobTasksByGroup(GroupId);
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000555CC File Offset: 0x000537CC
		public IList<DailyJobTaskResult> RunDailyJob(int GroupId)
		{
			IList<DailyJobTask> source = this.LoadDailyJobTasksByGroup(GroupId);
			List<DailyJobTask> list = source.ToList<DailyJobTask>().FindAll((DailyJobTask f) => f.IsActive);
			int windowsTaskJobSetResultsId = this.dao.LogDailyJobRunStart(GroupId);
			StringBuilder stringBuilder = new StringBuilder();
			IReportManager reportManager = new ReportManager(this.OpContext);
			List<DailyJobTaskResult> list2 = new List<DailyJobTaskResult>();
			foreach (DailyJobTask dailyJobTask in list)
			{
				int windowsTaskJobResultId = this.dao.LogDailyJobTaskRunStart(dailyJobTask.WindowsTaskJobId, dailyJobTask.ReportBase.ReportId, GroupId);
				DateTime now = DateTime.Now;
				DailyJobTaskResult dailyJobTaskResult;
				try
				{
					RunReportResult runReportResult = reportManager.ExecuteReport2(dailyJobTask.ReportBase.ReportId, Array.Empty<ReportParameter>());
					dailyJobTaskResult = new DailyJobTaskResult
					{
						ReportId = dailyJobTask.ReportBase.ReportId,
						RunStartDate = now,
						RunEndDate = DateTime.Now,
						RunResult = (runReportResult.ReportStatus.ErrorMessage ?? ""),
						Successful = (runReportResult.ReportStatus.LastStatusStep == eRunStatusStep.CompletedSuccessfully),
						TaskGroupId = dailyJobTask.GroupId,
						WindowsTaskJobId = dailyJobTask.WindowsTaskJobId
					};
				}
				catch (Exception ex)
				{
					dailyJobTaskResult = new DailyJobTaskResult
					{
						ReportId = dailyJobTask.ReportBase.ReportId,
						RunStartDate = now,
						RunEndDate = DateTime.Now,
						RunResult = ex.ToString(),
						WindowsTaskJobId = dailyJobTask.WindowsTaskJobId,
						TaskGroupId = dailyJobTask.GroupId,
						Successful = false
					};
				}
				list2.Add(dailyJobTaskResult);
				this.dao.LogDailyJobTaskRunEnd(windowsTaskJobResultId, dailyJobTask.WindowsTaskJobId, now, dailyJobTaskResult.Successful, dailyJobTaskResult.RunResult);
			}
			string text = string.Join("; ", list2.ConvertAll<string>((DailyJobTaskResult f) => string.Format("id={0}, reportid={1}, success={2}", f.WindowsTaskJobId.ToString(), f.ReportId.ToString(), f.Successful.ToString())).ToArray());
			this.dao.LogDailyJobRunEnd(windowsTaskJobSetResultsId, stringBuilder.ToString(), text.ToString());
			return list2;
		}

		// Token: 0x0400024C RID: 588
		private IDailyJobDAO dao;
	}
}
