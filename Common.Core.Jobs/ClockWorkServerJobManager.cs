using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ClockWorkLogger;
using Hangfire;
using Hangfire.Batches;
using Hangfire.Storage;
using TechnoPro.Common.Core.ClockWorkServer;
using TechnoPro.Common.DAO.ClockWorkServerJob;
using TechnoPro.Common.DAO.Impl.ClockWorkServerJob;
using TechnoPro.Common.ICore.ClockWorkServer;
using TechnoPro.Common.ICore.ClockWorkServerJob;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.InstanceInfo;
using TechnoPro.Common.Win32;

namespace TechnoPro.Common.Core.Jobs
{
	// Token: 0x02000002 RID: 2
	public class ClockWorkServerJobManager : IClockWorkServerJobManager, IBaseOperationContext<ClockWorkServerOperationContext>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public ClockWorkServerOperationContext OpContext { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public ClockWorkServerJobManager() : this(new ClockWorkServerOperationContext())
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206E File Offset: 0x0000026E
		public ClockWorkServerJobManager(ClockWorkServerOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000207D File Offset: 0x0000027D
		public IList<ClockWorkServerJobInfo> GetClockWorkServerJobs()
		{
			return ((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).GetClockWorkServerJobs();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000208F File Offset: 0x0000028F
		public IList<ClockWorkServerJobInfo> GetActiveClockWorkServerJobs()
		{
			return ((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).GetActiveClockWorkServerJobs();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020A1 File Offset: 0x000002A1
		public ClockWorkServerJobInfo GetClockWorkServerJobById(int jobId)
		{
			return ((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).GetClockWorkServerJobById(jobId);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020B4 File Offset: 0x000002B4
		public int CreateClockWorkServerJob(ClockWorkServerJobInfo clockWorkServerJob)
		{
			ClockWorkServerJobManager.<>c__DisplayClass9_0 CS$<>8__locals1 = new ClockWorkServerJobManager.<>c__DisplayClass9_0();
			CS$<>8__locals1.<>4__this = this;
			IClockWorkServerJobDAO clockWorkServerJobDAO = new ClockWorkServerJobDAO(this.OpContext);
			CS$<>8__locals1.jobId = clockWorkServerJobDAO.CreateClockWorkServerJob(clockWorkServerJob);
			if (clockWorkServerJob.IsActive)
			{
				RecurringJob.AddOrUpdate(CS$<>8__locals1.jobId.ToString(), Expression.Lambda<Action>(Expression.Call(null, methodof(ClockWorkServerJobManager.RunClockWorkServerJobNow(ClockWorkServerOperationContext, int)), new Expression[]
				{
					Expression.Property(Expression.Constant(this, typeof(ClockWorkServerJobManager)), methodof(ClockWorkServerJobManager.get_OpContext())),
					Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass9_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass9_0.jobId))
				}), Array.Empty<ParameterExpression>()), clockWorkServerJob.JobSchedule.ToCron(clockWorkServerJob.StartTime), TimeZoneInfo.Local, "default");
			}
			else
			{
				RecurringJob.RemoveIfExists(CS$<>8__locals1.jobId.ToString());
			}
			return CS$<>8__locals1.jobId;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A4 File Offset: 0x000003A4
		public void UpdateClockWorkServerJob(ClockWorkServerJobInfo clockWorkServerJob)
		{
			ClockWorkServerJobManager.<>c__DisplayClass10_0 CS$<>8__locals1 = new ClockWorkServerJobManager.<>c__DisplayClass10_0();
			CS$<>8__locals1.<>4__this = this;
			((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).UpdateClockWorkServerJob(clockWorkServerJob);
			CS$<>8__locals1.jobId = clockWorkServerJob.JobId;
			if (clockWorkServerJob.IsActive)
			{
				RecurringJob.AddOrUpdate(CS$<>8__locals1.jobId.ToString(), Expression.Lambda<Action>(Expression.Call(null, methodof(ClockWorkServerJobManager.RunClockWorkServerJobNow(ClockWorkServerOperationContext, int)), new Expression[]
				{
					Expression.Property(Expression.Constant(this, typeof(ClockWorkServerJobManager)), methodof(ClockWorkServerJobManager.get_OpContext())),
					Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass10_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass10_0.jobId))
				}), Array.Empty<ParameterExpression>()), clockWorkServerJob.JobSchedule.ToCron(clockWorkServerJob.StartTime), TimeZoneInfo.Local, "default");
				return;
			}
			RecurringJob.RemoveIfExists(CS$<>8__locals1.jobId.ToString());
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002290 File Offset: 0x00000490
		public void UpdateClockWorkServerJobLastRun(int jobId, DateTime? lastRunStartDatetime, DateTime? lastRunEndDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage)
		{
			((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).UpdateClockWorkServerJobLastRun(jobId, lastRunStartDatetime, lastRunEndDatetime, lastRunStatus, lastRunMessage);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022A9 File Offset: 0x000004A9
		public void RemoveClockWorkServerJob(int jobId)
		{
			((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).RemoveClockWorkServerJob(jobId);
			RecurringJob.RemoveIfExists(jobId.ToString());
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022C8 File Offset: 0x000004C8
		public IList<ClockWorkServerJobStep> GetClockWorkServerJobStepsByJobId(int clockworkServerJobId)
		{
			return ((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).GetClockWorkServerJobStepsByJobId(clockworkServerJobId);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022DB File Offset: 0x000004DB
		public ClockWorkServerJobStep GetClockWorkServerJobStepById(int jobId, int stepId)
		{
			return ((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).GetClockWorkServerJobStepById(jobId, stepId);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022EF File Offset: 0x000004EF
		public IList<ClockWorkServerJobExecutionLog> GetClockWorkServerExecutingLogsByJob(int jobId, DateTime startTime, DateTime endTime)
		{
			return ((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).GetClockWorkServerExecutingLogsByJob(jobId, startTime, endTime);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002304 File Offset: 0x00000504
		public IList<ClockWorkServerJobExecutionLog> GetClockWorkServerExecutingLogs(DateTime startTime, DateTime endTime)
		{
			return ((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).GetClockWorkServerExecutingLogs(startTime, endTime);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002318 File Offset: 0x00000518
		public int AddClockWorkServerExecutingLog(ClockWorkServerJobExecutionLog clockWorkServerJobExecutionLog)
		{
			return ((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).AddClockWorkServerExecutingLog(clockWorkServerJobExecutionLog);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000232C File Offset: 0x0000052C
		public void EnableClockWorkServerJob(int jobId)
		{
			ClockWorkServerJobManager.<>c__DisplayClass18_0 CS$<>8__locals1 = new ClockWorkServerJobManager.<>c__DisplayClass18_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.jobId = jobId;
			((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).EnableClockWorkServerJob(CS$<>8__locals1.jobId);
			ClockWorkServerJobInfo clockWorkServerJobById = this.GetClockWorkServerJobById(CS$<>8__locals1.jobId);
			RecurringJob.AddOrUpdate(CS$<>8__locals1.jobId.ToString(), Expression.Lambda<Action>(Expression.Call(null, methodof(ClockWorkServerJobManager.RunClockWorkServerJobNow(ClockWorkServerOperationContext, int)), new Expression[]
			{
				Expression.Property(Expression.Constant(this, typeof(ClockWorkServerJobManager)), methodof(ClockWorkServerJobManager.get_OpContext())),
				Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass18_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass18_0.jobId))
			}), Array.Empty<ParameterExpression>()), clockWorkServerJobById.JobSchedule.ToCron(clockWorkServerJobById.StartTime), TimeZoneInfo.Local, "default");
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002409 File Offset: 0x00000609
		public void DisableClockWorkServerJob(int jobId)
		{
			((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(this.OpContext)).DisableClockWorkServerJob(jobId);
			RecurringJob.RemoveIfExists(jobId.ToString());
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002428 File Offset: 0x00000628
		public void RunClockWorkServerJobNow(int jobId)
		{
			ClockWorkServerJobManager.RunClockWorkServerJobNow(this.OpContext, jobId);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002438 File Offset: 0x00000638
		public void SynchronizeServerRecurringJobs()
		{
			IList<ClockWorkServerJobInfo> activeClockWorkServerJobs = this.GetActiveClockWorkServerJobs();
			List<RecurringJobDto> recurringJobs = JobStorage.Current.GetConnection().GetRecurringJobs();
			CWLogger.Logger.Trace(string.Format("ClockWorkServerJobManager::SynchronizeServerRecurringJobs: Starting synchronizing {0} active jobs to Hangfire ...", activeClockWorkServerJobs.Count));
			List<string> list = new List<string>();
			using (List<RecurringJobDto>.Enumerator enumerator = recurringJobs.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RecurringJobDto job = enumerator.Current;
					ClockWorkServerJobInfo clockWorkServerJobInfo = activeClockWorkServerJobs.FirstOrDefault((ClockWorkServerJobInfo j) => j.JobId.ToString() == job.Id);
					if (clockWorkServerJobInfo == null || !clockWorkServerJobInfo.IsActive)
					{
						list.Add(job.Id);
					}
				}
			}
			foreach (string text in list)
			{
				RecurringJob.RemoveIfExists(text);
				CWLogger.Logger.Trace("ClockWorkServerJobManager::SynchronizeServerRecurringJobs: Removed job from hangfire server: JobId=" + text);
			}
			using (IEnumerator<ClockWorkServerJobInfo> enumerator3 = activeClockWorkServerJobs.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					ClockWorkServerJobManager.<>c__DisplayClass21_1 CS$<>8__locals2 = new ClockWorkServerJobManager.<>c__DisplayClass21_1();
					CS$<>8__locals2.<>4__this = this;
					CS$<>8__locals2.job = enumerator3.Current;
					if (CS$<>8__locals2.job.IsActive && CS$<>8__locals2.job.JobSchedule != null)
					{
						RecurringJob.AddOrUpdate(CS$<>8__locals2.job.JobId.ToString(), Expression.Lambda<Action>(Expression.Call(null, methodof(ClockWorkServerJobManager.RunClockWorkServerJobNow(ClockWorkServerOperationContext, int)), new Expression[]
						{
							Expression.Property(Expression.Constant(this, typeof(ClockWorkServerJobManager)), methodof(ClockWorkServerJobManager.get_OpContext())),
							Expression.Property(Expression.Field(Expression.Constant(CS$<>8__locals2, typeof(ClockWorkServerJobManager.<>c__DisplayClass21_1)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass21_1.job)), methodof(ClockWorkServerJobInfo.get_JobId()))
						}), Array.Empty<ParameterExpression>()), CS$<>8__locals2.job.JobSchedule.ToCron(CS$<>8__locals2.job.StartTime), TimeZoneInfo.Local, "default");
						CWLogger logger = CWLogger.Logger;
						string format = "ClockWorkServerJobManager::SynchronizeServerRecurringJobs: Added/Modified job to queue: JobId={0}, Schedule={1}";
						ClockWorkServerJobInfo job5 = CS$<>8__locals2.job;
						object arg = (job5 != null) ? new int?(job5.JobId) : null;
						ClockWorkServerJobInfo job2 = CS$<>8__locals2.job;
						object arg2;
						if (job2 == null)
						{
							arg2 = null;
						}
						else
						{
							ClockWorkServerJobSchedule jobSchedule = job2.JobSchedule;
							arg2 = ((jobSchedule != null) ? jobSchedule.ToCron(CS$<>8__locals2.job.StartTime) : null);
						}
						logger.Trace(string.Format(format, arg, arg2));
					}
					else
					{
						CWLogger logger2 = CWLogger.Logger;
						string format2 = "ClockWorkServerJobManager::SynchronizeServerRecurringJobs: Invalid configuration, JobId={0}, Schedule={1}";
						ClockWorkServerJobInfo job3 = CS$<>8__locals2.job;
						object arg3 = (job3 != null) ? new int?(job3.JobId) : null;
						ClockWorkServerJobInfo job4 = CS$<>8__locals2.job;
						object arg4;
						if (job4 == null)
						{
							arg4 = null;
						}
						else
						{
							ClockWorkServerJobSchedule jobSchedule2 = job4.JobSchedule;
							arg4 = ((jobSchedule2 != null) ? jobSchedule2.ToCron(CS$<>8__locals2.job.StartTime) : null);
						}
						logger2.Error(string.Format(format2, arg3, arg4));
					}
				}
			}
			foreach (RecurringJobDto recurringJobDto in JobStorage.Current.GetConnection().GetRecurringJobs())
			{
				CWLogger.Logger.Trace(string.Format("ClockWorkServerJobManager::SynchronizeServerRecurringJobs: Hangfire job: JobId={0}, Cron={1}, CreatedAt={2}, Removed={3}", new object[]
				{
					recurringJobDto.Id,
					recurringJobDto.Cron,
					recurringJobDto.CreatedAt,
					recurringJobDto.Removed
				}));
			}
			CWLogger.Logger.Trace("ClockWorkServerJobManager::SynchronizeServerRecurringJobs: End synchronizing jobs to Hangfire ...");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002820 File Offset: 0x00000A20
		public static void UpdateClockWorkServerJobLastRunForEnd(OperationContext opContext, int jobId, eClockWorkServerJobResult lastRunStatus, string lastRunMessage)
		{
			((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(opContext)).UpdateClockWorkServerJobLastRunForEnd(jobId, DateTime.Now, lastRunStatus, lastRunMessage);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002838 File Offset: 0x00000A38
		public static void RunClockWorkServerJobNow(ClockWorkServerOperationContext opContext, int jobId)
		{
			ClockWorkServerJobManager.<>c__DisplayClass23_0 CS$<>8__locals1 = new ClockWorkServerJobManager.<>c__DisplayClass23_0();
			CS$<>8__locals1.opContext = opContext;
			CS$<>8__locals1.jobId = jobId;
			CS$<>8__locals1.serverVDir = CS$<>8__locals1.opContext.ClockWorkServerVirtualDirectory;
			CS$<>8__locals1.serverInstanceName = CS$<>8__locals1.opContext.ClockWorkServerInstanceName;
			BatchJob.ContinueWith(BatchJob.StartNew(delegate(IBatchAction x)
			{
				ClockWorkServerJobInfo clockWorkServerJobById = ((IClockWorkServerJobManager)new ClockWorkServerJobManager(CS$<>8__locals1.opContext)).GetClockWorkServerJobById(CS$<>8__locals1.jobId);
				int count = clockWorkServerJobById.JobSteps.Count;
				if (count > 0)
				{
					ClockWorkServerJobManager.<>c__DisplayClass23_1 CS$<>8__locals2 = new ClockWorkServerJobManager.<>c__DisplayClass23_1();
					CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
					CS$<>8__locals2.transactionId = Guid.NewGuid();
					int num = 0;
					ClockWorkServerJobStep clockWorkServerJobStep;
					while (num < count && !clockWorkServerJobById.JobSteps[num].IsActive)
					{
						clockWorkServerJobStep = clockWorkServerJobById.JobSteps[num];
						string text = clockWorkServerJobStep.JobType + ":" + clockWorkServerJobStep.Title;
						string message = string.Format("JobExecutingApp:: Skipped ClockWorkServer job step '{0}' execution because of it is disable, ServerInstance={1}, JobId={2}, JobStepId={3}", new object[]
						{
							text,
							CS$<>8__locals1.serverInstanceName,
							CS$<>8__locals1.jobId,
							clockWorkServerJobStep.StepId
						});
						CWLogger.Logger.Warn(message);
						ClockWorkServerJobManager.AddJobExecutingLog(CS$<>8__locals1.opContext, clockWorkServerJobStep, eClockWorkServerJobResult.Warning, DateTime.Now, new DateTime?(DateTime.Now), message, CS$<>8__locals2.transactionId);
						num++;
					}
					if (num >= count)
					{
						return;
					}
					clockWorkServerJobStep = clockWorkServerJobById.JobSteps[num];
					CS$<>8__locals2.step = clockWorkServerJobStep;
					string parentId = x.Enqueue(Expression.Lambda<Action>(Expression.Call(null, methodof(ClockWorkServerJobManager.ExecuteFirstJobStep(ClockWorkServerOperationContext, string, Guid, ClockWorkServerJobStep)), new Expression[]
					{
						Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_0.opContext)),
						Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_0.serverVDir)),
						Expression.Field(Expression.Constant(CS$<>8__locals2, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_1)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_1.transactionId)),
						Expression.Field(Expression.Constant(CS$<>8__locals2, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_1)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_1.step))
					}), Array.Empty<ParameterExpression>()));
					for (int i = num + 1; i < count; i++)
					{
						ClockWorkServerJobManager.<>c__DisplayClass23_2 CS$<>8__locals3 = new ClockWorkServerJobManager.<>c__DisplayClass23_2();
						CS$<>8__locals3.CS$<>8__locals2 = CS$<>8__locals2;
						clockWorkServerJobStep = clockWorkServerJobById.JobSteps[i];
						if (!clockWorkServerJobStep.IsActive)
						{
							string text2 = clockWorkServerJobStep.JobType + ":" + clockWorkServerJobStep.Title;
							string message2 = string.Format("JobExecutingApp:: Skipped ClockWorkServer job step '{0}' execution because of it is disable, ServerInstance={1}, JobId={2}, JobStepId={3}", new object[]
							{
								text2,
								CS$<>8__locals1.serverInstanceName,
								CS$<>8__locals1.jobId,
								clockWorkServerJobStep.StepId
							});
							CWLogger.Logger.Warn(message2);
							ClockWorkServerJobManager.AddJobExecutingLog(CS$<>8__locals1.opContext, clockWorkServerJobStep, eClockWorkServerJobResult.Warning, DateTime.Now, new DateTime?(DateTime.Now), message2, CS$<>8__locals3.CS$<>8__locals2.transactionId);
						}
						else
						{
							CS$<>8__locals3.step2 = clockWorkServerJobStep;
							parentId = x.ContinueWith(parentId, Expression.Lambda<Action>(Expression.Call(null, methodof(ClockWorkServerJobManager.ExecuteJobStep(ClockWorkServerOperationContext, string, Guid, ClockWorkServerJobStep)), new Expression[]
							{
								Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_0.opContext)),
								Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_0.serverVDir)),
								Expression.Field(Expression.Field(Expression.Constant(CS$<>8__locals3, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_2)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_2.CS$<>8__locals2)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_1.transactionId)),
								Expression.Field(Expression.Constant(CS$<>8__locals3, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_2)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_2.step2))
							}), Array.Empty<ParameterExpression>()));
						}
					}
				}
			}, null), delegate(IBatchAction x)
			{
				x.Enqueue(Expression.Lambda<Action>(Expression.Call(null, methodof(ClockWorkServerJobManager.UpdateClockWorkServerJobLastRunForEnd(OperationContext, int, eClockWorkServerJobResult, string)), new Expression[]
				{
					Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_0.opContext)),
					Expression.Field(Expression.Constant(CS$<>8__locals1, typeof(ClockWorkServerJobManager.<>c__DisplayClass23_0)), fieldof(ClockWorkServerJobManager.<>c__DisplayClass23_0.jobId)),
					Expression.Constant(eClockWorkServerJobResult.Success, typeof(eClockWorkServerJobResult)),
					Expression.Field(null, fieldof(string.Empty))
				}), Array.Empty<ParameterExpression>()));
			}, null, BatchContinuationOptions.OnlyOnSucceededState);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000028A4 File Offset: 0x00000AA4
		public static void ExecuteFirstJobStep(ClockWorkServerOperationContext opContext, string serverVDir, Guid transactionId, ClockWorkServerJobStep jobStep)
		{
			string str = jobStep.JobType + ":" + jobStep.Title;
			DateTime now = DateTime.Now;
			CWLogger.Logger.Trace("ClockWorkServerJobManager::ExecuteFirstJobStep: Job step " + str + " start running at " + now.ToString("G"));
			((IClockWorkServerJobDAO)new ClockWorkServerJobDAO(opContext)).UpdateClockWorkServerJobLastRunForBegin(jobStep.JobId, now, eClockWorkServerJobResult.Running, string.Empty);
			ClockWorkServerJobManager.ExecuteJobStep(opContext, serverVDir, transactionId, jobStep);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002918 File Offset: 0x00000B18
		public static void ExecuteJobStep(ClockWorkServerOperationContext opContext, string serverVDir, Guid transactionId, ClockWorkServerJobStep jobStep)
		{
			string jobType = jobStep.JobType;
			string text = jobType + ":" + jobStep.Title;
			try
			{
				Type type = Type.GetType("TechnoPro.Common.Core.Jobs.ExecutingJobs." + jobType);
				IClockWorkServerExecutingJob clockWorkServerExecutingJob = (type != null) ? ((IClockWorkServerExecutingJob)Activator.CreateInstance(type)) : null;
				if (clockWorkServerExecutingJob != null)
				{
					ServerInstanceInfo serverInstanceInfoByName = ((IServerInstanceInfoManager)new ServerInstanceInfoManager()).GetServerInstanceInfoByName(serverVDir);
					clockWorkServerExecutingJob.Init(serverInstanceInfoByName, jobStep.Parameters);
					DateTime startTime = DateTime.Now;
					ClockWorkServerJobRunningResult result = clockWorkServerExecutingJob.Run();
					Task.Run(delegate()
					{
						ClockWorkServerJobManager.AddJobExecutingLog(opContext, jobStep, result.Status, startTime, new DateTime?(DateTime.Now), result.Message ?? string.Empty, transactionId);
					});
				}
				else
				{
					CWLogger.Logger.Error(string.Format("ClockWorkServerJobManager::ExecuteJobStep: Creating step '{0}' failed. ServerVirtualDir={1}, JobId={2}, JobStepId={3}.", new object[]
					{
						text,
						serverVDir,
						jobStep.JobId,
						jobStep.StepId
					}));
					string message = string.Format("ClockWorkServerJobManager::ExecuteJobStep: Creating step '{0}' failed. ServerVirtualDir={1}, JobId={2}, JobStepId={3}.", new object[]
					{
						text,
						serverVDir,
						jobStep.JobId,
						jobStep.StepId
					});
					Task.Run(delegate()
					{
						ClockWorkServerJobManager.AddJobExecutingLog(opContext, jobStep, eClockWorkServerJobResult.Error, DateTime.Now, new DateTime?(DateTime.Now), message, transactionId);
					});
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ClockWorkServerJobManager::ExecuteJobStep: Running job '{0}' failed. ServerVirtualDir={1}, JobId={2}, JobStepId={3}. {4}", new object[]
				{
					text,
					serverVDir,
					jobStep.JobId,
					jobStep.StepId,
					ex
				}), ex);
				string message = string.Format("ClockWorkServerJobManager::ExecuteJobStep: Creating step '{0}' failed. ServerVirtualDir={1}, JobId={2}, JobStepId={3}. Error: {4}", new object[]
				{
					text,
					serverVDir,
					jobStep.JobId,
					jobStep.StepId,
					ex
				});
				Task.Run(delegate()
				{
					ClockWorkServerJobManager.AddJobExecutingLog(opContext, jobStep, eClockWorkServerJobResult.Error, DateTime.Now, new DateTime?(DateTime.Now), message, transactionId);
				});
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public static void AddJobExecutingLog(ClockWorkServerOperationContext opContext, ClockWorkServerJobStep jobStep, eClockWorkServerJobResult result, DateTime startTime, DateTime? endTime, string message, Guid transactionId)
		{
			try
			{
				((IClockWorkServerJobManager)new ClockWorkServerJobManager(opContext)).AddClockWorkServerExecutingLog(new ClockWorkServerJobExecutionLog
				{
					Step = jobStep,
					StartTime = startTime,
					EndTime = endTime,
					Message = message,
					Status = result,
					ServerIpAddress = TechnoPro.Common.Win32.Environment.GetIPAddress(),
					TransactionId = transactionId
				});
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("JobExecutingApp::AddJobExecutingLog: Adding job logs failed: {0}", ex), ex);
			}
		}
	}
}
