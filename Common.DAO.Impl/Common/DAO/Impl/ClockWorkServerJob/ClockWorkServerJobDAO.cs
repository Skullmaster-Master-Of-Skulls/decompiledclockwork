using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.ClockWorkServerJob;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.Common.DAO.Impl.ClockWorkServerJob
{
	// Token: 0x0200010F RID: 271
	public class ClockWorkServerJobDAO : IClockWorkServerJobDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x0004FD18 File Offset: 0x0004DF18
		public ClockWorkServerJobDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0004FD2C File Offset: 0x0004DF2C
		public IList<ClockWorkServerJobInfo> GetClockWorkServerJobs()
		{
			List<ClockWorkServerJobInfo> list = new List<ClockWorkServerJobInfo>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select JobId, JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, JobUniqueId, LastRunStartDatetime, LastRunEndDatetime, LastRunStatus, LastRunMessage, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword, IsSystemJob \r\nfrom [ClockWorkServerJob_JobInfo] order by StartTimeInMinutes ASC"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						ClockWorkServerJobInfo clockWorkServerJobInfoFromReader = this.GetClockWorkServerJobInfoFromReader(dataReader, batchDecryptor);
						bool flag2 = clockWorkServerJobInfoFromReader != null;
						if (flag2)
						{
							list.Add(clockWorkServerJobInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0004FDD4 File Offset: 0x0004DFD4
		public IList<ClockWorkServerJobInfo> GetActiveClockWorkServerJobs()
		{
			List<ClockWorkServerJobInfo> list = new List<ClockWorkServerJobInfo>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select JobId, JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, JobUniqueId, LastRunStartDatetime, LastRunEndDatetime, LastRunStatus, LastRunMessage, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword, IsSystemJob \r\nfrom [ClockWorkServerJob_JobInfo] where IsJobActive=1 order by StartTimeInMinutes ASC"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						ClockWorkServerJobInfo clockWorkServerJobInfoFromReader = this.GetClockWorkServerJobInfoFromReader(dataReader, batchDecryptor);
						bool flag2 = clockWorkServerJobInfoFromReader != null;
						if (flag2)
						{
							list.Add(clockWorkServerJobInfoFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0004FE7C File Offset: 0x0004E07C
		public ClockWorkServerJobInfo GetClockWorkServerJobById(int jobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select JobId, JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, JobUniqueId, LastRunStartDatetime, LastRunEndDatetime, LastRunStatus, LastRunMessage, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword, IsSystemJob \r\nfrom [ClockWorkServerJob_JobInfo] where JobID=@jobid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetClockWorkServerJobInfoFromReader(dataReader, databaseLayer.Encryption.GetBatchDecryptor());
				}
			}
			return null;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0004FF1C File Offset: 0x0004E11C
		public int CreateClockWorkServerJob(ClockWorkServerJobInfo clockWorkServerJob)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@jobid", DbType.Int32, 0),
				databaseLayer.GetParameter("@jobtitle", DbType.String, clockWorkServerJob.Title ?? string.Empty),
				databaseLayer.GetParameter("@jobnotes", DbType.String, clockWorkServerJob.Notes ?? string.Empty),
				databaseLayer.GetParameter("@starttimeinminutes", DbType.Int32, (int)clockWorkServerJob.StartTime.TotalMinutes),
				databaseLayer.GetParameter("@jobschedule", DbType.String, (clockWorkServerJob.JobSchedule != null) ? clockWorkServerJob.JobSchedule.SaveToXml() : string.Empty),
				databaseLayer.GetParameter("@timeoutinminutes", DbType.Int32, clockWorkServerJob.Timeout.TotalMinutes),
				databaseLayer.GetParameter("@isjobactive", DbType.Boolean, clockWorkServerJob.IsActive),
				databaseLayer.GetParameter("@impersonationdomain", DbType.String, (clockWorkServerJob.Impersonate != null) ? clockWorkServerJob.Impersonate.Domain : DBNull.Value),
				databaseLayer.GetParameter("@impersonationusername", DbType.String, (clockWorkServerJob.Impersonate != null) ? clockWorkServerJob.Impersonate.Username : DBNull.Value),
				databaseLayer.GetParameter("@impersonationpassword", DbType.Binary, (clockWorkServerJob.Impersonate != null) ? databaseLayer.Encryption.Encrypt(clockWorkServerJob.Impersonate.Password) : DBNull.Value)
			};
			databaseLayer.ExecuteNonQueryTransaction("insert into ClockWorkServerJob_JobInfo\r\n\t            (JobTitle, JobNotes, StartTimeInMinutes, JobSchedule, TimeoutInMinutes, IsJobActive, ImpersonationDomain, ImpersonationUsername, ImpersonationPassword)\r\n            values\r\n\t            (@jobtitle, @jobnotes, @starttimeinminutes, @jobschedule, @timeoutinminutes, @isjobactive, @impersonationdomain, @impersonationusername, @impersonationpassword)\r\n            set @jobid = SCOPE_IDENTITY()", dbTransaction, array);
			clockWorkServerJob.JobId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
			foreach (ClockWorkServerJobStep clockWorkServerJobStep in clockWorkServerJob.JobSteps)
			{
				clockWorkServerJobStep.JobId = clockWorkServerJob.JobId;
				clockWorkServerJobStep.StepId = this.AddClockWorkServerJobStep(clockWorkServerJobStep, dbTransaction);
			}
			databaseLayer.CommitDbTransaction(dbTransaction);
			return clockWorkServerJob.JobId;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00050160 File Offset: 0x0004E360
		public void UpdateClockWorkServerJob(ClockWorkServerJobInfo clockWorkServerJob)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbTransaction dbTransaction = databaseLayer.BeginDbTransaction();
			foreach (ClockWorkServerJobStep clockWorkServerJobStep in from j in clockWorkServerJob.JobSteps
			where j.StepId > 0
			select j)
			{
				this.UpdateClockWorkServerJobStep(clockWorkServerJobStep, dbTransaction);
			}
			foreach (ClockWorkServerJobStep clockWorkServerJobStep2 in from j in clockWorkServerJob.JobSteps
			where j.StepId == 0
			select j)
			{
				clockWorkServerJobStep2.StepId = this.AddClockWorkServerJobStep(clockWorkServerJobStep2, dbTransaction);
			}
			DbParameter[] array = new DbParameter[11];
			array[0] = databaseLayer.GetParameter("@jobid", DbType.Int32, clockWorkServerJob.JobId);
			array[1] = databaseLayer.GetParameter("@jobtitle", DbType.String, clockWorkServerJob.Title ?? string.Empty);
			array[2] = databaseLayer.GetParameter("@jobnotes", DbType.String, clockWorkServerJob.Notes ?? string.Empty);
			array[3] = databaseLayer.GetParameter("@starttimeinminutes", DbType.Int32, (int)clockWorkServerJob.StartTime.TotalMinutes);
			array[4] = databaseLayer.GetParameter("@jobschedule", DbType.String, (clockWorkServerJob.JobSchedule != null) ? clockWorkServerJob.JobSchedule.SaveToXml() : string.Empty);
			array[5] = databaseLayer.GetParameter("@timeoutinminutes", DbType.Int32, clockWorkServerJob.Timeout.TotalMinutes);
			array[6] = databaseLayer.GetParameter("@isjobactive", DbType.Boolean, clockWorkServerJob.IsActive);
			array[7] = databaseLayer.GetParameter("@impersonationdomain", DbType.String, (clockWorkServerJob.Impersonate != null) ? clockWorkServerJob.Impersonate.Domain : DBNull.Value);
			array[8] = databaseLayer.GetParameter("@impersonationusername", DbType.String, (clockWorkServerJob.Impersonate != null) ? clockWorkServerJob.Impersonate.Username : DBNull.Value);
			array[9] = databaseLayer.GetParameter("@impersonationpassword", DbType.Binary, (clockWorkServerJob.Impersonate != null) ? databaseLayer.Encryption.Encrypt(clockWorkServerJob.Impersonate.Password) : DBNull.Value);
			array[10] = databaseLayer.GetParameter("@jobsteplist", DbType.String, (from s in clockWorkServerJob.JobSteps
			select s.StepId).ToList<int>().CommaSeparatedValuesWithoutSpace<int>());
			DbParameter[] parameters = array;
			databaseLayer.ExecuteStoredProcedureTransaction("sp_ClockWorkServerJob_UpdateJob", dbTransaction, parameters);
			databaseLayer.CommitDbTransaction(dbTransaction);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00050440 File Offset: 0x0004E640
		public void UpdateClockWorkServerJobLastRunForBegin(int jobId, DateTime lastRunStartDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage)
		{
			this.UpdateClockWorkServerJobLastRun("update ClockWorkServerJob_JobInfo\r\n                set LastRunStartDatetime = @lastrunstartdatetime\r\n                   ,LastRunStatus = @lastrunstatus\r\n                   ,LastRunMessage = @lastrunmessage\r\n                where JobId = @jobid", jobId, new DateTime?(lastRunStartDatetime), null, lastRunStatus, lastRunMessage);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00050470 File Offset: 0x0004E670
		public void UpdateClockWorkServerJobLastRunForEnd(int jobId, DateTime lastRunEndDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage)
		{
			this.UpdateClockWorkServerJobLastRun("update ClockWorkServerJob_JobInfo\r\n                set LastRunEndDatetime = @lastrunenddatetime\r\n                   ,LastRunStatus = @lastrunstatus\r\n                   ,LastRunMessage = @lastrunmessage\r\n                where JobId = @jobid", jobId, null, new DateTime?(lastRunEndDatetime), lastRunStatus, lastRunMessage);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0005049D File Offset: 0x0004E69D
		public void UpdateClockWorkServerJobLastRun(int jobId, DateTime? lastRunStartDatetime, DateTime? lastRunEndDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage)
		{
			this.UpdateClockWorkServerJobLastRun("update ClockWorkServerJob_JobInfo\r\n                set LastRunStartDatetime = @lastrunstartdatetime\r\n                   ,LastRunEndDatetime = @lastrunenddatetime\r\n                   ,LastRunStatus = @lastrunstatus\r\n                   ,LastRunMessage = @lastrunmessage\r\n                where JobId = @jobid", jobId, lastRunStartDatetime, lastRunEndDatetime, lastRunStatus, lastRunMessage);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x000504B4 File Offset: 0x0004E6B4
		private void UpdateClockWorkServerJobLastRun(string sqlQuery, int jobId, DateTime? lastRunStartDatetime, DateTime? lastRunEndDatetime, eClockWorkServerJobResult lastRunStatus, string lastRunMessage)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId),
				databaseLayer.GetParameter("@lastrunstartdatetime", DbType.DateTime, (lastRunStartDatetime != null) ? lastRunStartDatetime.Value : DBNull.Value),
				databaseLayer.GetParameter("@lastrunenddatetime", DbType.DateTime, (lastRunEndDatetime != null) ? lastRunEndDatetime.Value : DBNull.Value),
				databaseLayer.GetParameter("@lastrunstatus", DbType.String, lastRunStatus.ToString()),
				databaseLayer.GetParameter("@lastrunmessage", DbType.String, lastRunMessage)
			};
			databaseLayer.ExecuteNonQuery(sqlQuery, parameters);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00050588 File Offset: 0x0004E788
		public void RemoveClockWorkServerJob(int jobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId)
			};
			databaseLayer.ExecuteNonQuery("delete from [ClockWorkServerJob_JobInfo] where JobId=@jobid", parameters);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x000505DC File Offset: 0x0004E7DC
		public void RemoveClockWorkServerJobStep(int jobId, int stepId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId),
				databaseLayer.GetParameter("@stepid", DbType.Int32, stepId)
			};
			databaseLayer.ExecuteStoredProcedure("sp_ClockWorkServerJob_RemoveJobStep", parameters);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00050644 File Offset: 0x0004E844
		public IList<ClockWorkServerJobStep> GetClockWorkServerJobStepsByJobId(int jobId)
		{
			List<ClockWorkServerJobStep> list = new List<ClockWorkServerJobStep>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select JobStepId, FK_JobId, JobType, StepTitle, StepNotes, StepParams, StepOrderNum, IsStepActive from [ClockWorkServerJob_JobSteps] where Fk_JobId = @jobid order by StepOrderNum ASC", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						ClockWorkServerJobStep clockWorkServerJobStepFromReader = this.GetClockWorkServerJobStepFromReader(dataReader);
						bool flag2 = clockWorkServerJobStepFromReader != null;
						if (flag2)
						{
							list.Add(clockWorkServerJobStepFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x000506F8 File Offset: 0x0004E8F8
		public ClockWorkServerJobStep GetClockWorkServerJobStepById(int jobId, int stepId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId),
				databaseLayer.GetParameter("@stepid", DbType.Int32, stepId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select JobStepId, FK_JobId, JobType, StepTitle, StepNotes, StepParams, StepOrderNum, IsStepActive from [ClockWorkServerJob_JobSteps] where FK_JobId = @jobid and JobStepId = @stepid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetClockWorkServerJobStepFromReader(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000507A0 File Offset: 0x0004E9A0
		public IList<ClockWorkServerJobExecutionLog> GetClockWorkServerExecutingLogsByJob(int jobId, DateTime startTime, DateTime endTime)
		{
			List<ClockWorkServerJobExecutionLog> list = new List<ClockWorkServerJobExecutionLog>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId),
				databaseLayer.GetParameter("@starttime", DbType.DateTime, startTime),
				databaseLayer.GetParameter("@endtime", DbType.DateTime, endTime)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select JobLogId, FK_JobId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress, TransactionId from [ClockWorkServerJob_ExecutionLogs] where FK_JobId = @jobid and StartTime between @starttime and @endtime group by transactionid, FK_JobId, JobLogId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress order by StartTime DESC", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						ClockWorkServerJobExecutionLog clockWorkServerJobExecutionLogFromReader = this.GetClockWorkServerJobExecutionLogFromReader(dataReader);
						bool flag2 = clockWorkServerJobExecutionLogFromReader != null;
						if (flag2)
						{
							list.Add(clockWorkServerJobExecutionLogFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00050880 File Offset: 0x0004EA80
		public IList<ClockWorkServerJobExecutionLog> GetClockWorkServerExecutingLogs(DateTime startTime, DateTime endTime)
		{
			List<ClockWorkServerJobExecutionLog> list = new List<ClockWorkServerJobExecutionLog>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@starttime", DbType.DateTime, startTime),
				databaseLayer.GetParameter("@endtime", DbType.DateTime, endTime)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select JobLogId, FK_JobId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress, TransactionId from [ClockWorkServerJob_ExecutionLogs] where StartTime between @starttime and @endtime group by transactionid, FK_JobId, JobLogId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress order by StartTime DESC", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						ClockWorkServerJobExecutionLog clockWorkServerJobExecutionLogFromReader = this.GetClockWorkServerJobExecutionLogFromReader(dataReader);
						bool flag2 = clockWorkServerJobExecutionLogFromReader != null;
						if (flag2)
						{
							list.Add(clockWorkServerJobExecutionLogFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00050948 File Offset: 0x0004EB48
		public int AddClockWorkServerExecutingLog(ClockWorkServerJobExecutionLog clockWorkServerJobExecutionLog)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@joblogid", DbType.Int32, 0),
				databaseLayer.GetParameter("@fk_jobid", DbType.Int32, clockWorkServerJobExecutionLog.Step.JobId),
				databaseLayer.GetParameter("@fk_stepid", DbType.Int32, clockWorkServerJobExecutionLog.Step.StepId),
				databaseLayer.GetParameter("@jobexecutionstatus", DbType.String, clockWorkServerJobExecutionLog.Status.ToString()),
				databaseLayer.GetParameter("@starttime", DbType.DateTime, clockWorkServerJobExecutionLog.StartTime),
				databaseLayer.GetParameter("@endtime", DbType.DateTime, (clockWorkServerJobExecutionLog.EndTime != null) ? clockWorkServerJobExecutionLog.EndTime : DBNull.Value),
				databaseLayer.GetParameter("@message", DbType.String, clockWorkServerJobExecutionLog.Message ?? string.Empty),
				databaseLayer.GetParameter("@serveripaddress", DbType.String, clockWorkServerJobExecutionLog.ServerIpAddress ?? string.Empty),
				databaseLayer.GetParameter("@transactionid", DbType.Guid, clockWorkServerJobExecutionLog.TransactionId)
			};
			databaseLayer.ExecuteNonQuery("insert into [ClockworkserverJob_ExecutionLogs]\r\n\t            (FK_JobId, FK_StepId, JobExecutionStatus, StartTime, EndTime, [Message], ServerIPAddress, TransactionId)\r\n            values\r\n\t            (@fk_jobid, @fk_stepid, @jobexecutionstatus, @starttime, @endtime, @message, @serveripaddress, @transactionid)\r\n            set @joblogid = SCOPE_IDENTITY()", array);
			return clockWorkServerJobExecutionLog.ExecutionLogId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00050AD0 File Offset: 0x0004ECD0
		public void EnableClockWorkServerJob(int jobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, true)
			};
			databaseLayer.ExecuteNonQuery("Update [ClockWorkServerJob_JobInfo] set IsJobActive=@isactive where JobID=@jobid", parameters);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00050B38 File Offset: 0x0004ED38
		public void DisableClockWorkServerJob(int jobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobid", DbType.Int32, jobId),
				databaseLayer.GetParameter("@isactive", DbType.Boolean, false)
			};
			databaseLayer.ExecuteNonQuery("Update [ClockWorkServerJob_JobInfo] set IsJobActive=@isactive where JobID=@jobid", parameters);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00050BA0 File Offset: 0x0004EDA0
		private void UpdateClockWorkServerJobStep(ClockWorkServerJobStep clockWorkServerJobStep, DbTransaction dbTrans = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@jobstepid", DbType.Int32, clockWorkServerJobStep.StepId),
				databaseLayer.GetParameter("@jobid", DbType.Int32, clockWorkServerJobStep.JobId),
				databaseLayer.GetParameter("@jobtype", DbType.String, clockWorkServerJobStep.JobType ?? string.Empty),
				databaseLayer.GetParameter("@steptitle", DbType.String, clockWorkServerJobStep.Title ?? string.Empty),
				databaseLayer.GetParameter("@stepnotes", DbType.String, clockWorkServerJobStep.Notes ?? string.Empty),
				databaseLayer.GetParameter("@stepparams", DbType.String, clockWorkServerJobStep.Parameters ?? string.Empty),
				databaseLayer.GetParameter("@stepordernum", DbType.Int32, clockWorkServerJobStep.OrderNum),
				databaseLayer.GetParameter("@isstepactive", DbType.Boolean, clockWorkServerJobStep.IsActive)
			};
			bool flag = dbTrans != null;
			if (flag)
			{
				databaseLayer.ExecuteStoredProcedureTransaction("sp_ClockWorkServerJob_UpdateJobStep", dbTrans, parameters);
			}
			else
			{
				databaseLayer.ExecuteStoredProcedure("sp_ClockWorkServerJob_UpdateJobStep", parameters);
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00050CDC File Offset: 0x0004EEDC
		private ClockWorkServerJobExecutionLog GetClockWorkServerJobExecutionLogFromReader(IDataReader reader)
		{
			int num = (int)reader["FK_JobId"];
			int num2 = (int)reader["FK_StepId"];
			ClockWorkServerJobStep step = (num > 0 && num2 > 0) ? this.GetClockWorkServerJobStepById(num, num2) : null;
			return new ClockWorkServerJobExecutionLog
			{
				ExecutionLogId = (int)reader["JobLogId"],
				Step = step,
				Status = (eClockWorkServerJobResult)Enum.Parse(typeof(eClockWorkServerJobResult), (string)reader["JobExecutionStatus"]),
				StartTime = (DateTime)reader["StartTime"],
				EndTime = ((reader["EndTime"] is DBNull) ? null : new DateTime?((DateTime)reader["EndTime"])),
				Message = (string)reader["Message"],
				ServerIpAddress = (string)reader["ServerIPAddress"],
				TransactionId = (Guid)reader["TransactionId"]
			};
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00050E10 File Offset: 0x0004F010
		private ClockWorkServerJobInfo GetClockWorkServerJobInfoFromReader(IDataReader reader, IBatchDecryptor decryptor)
		{
			int jobId = (int)reader["JobId"];
			IList<ClockWorkServerJobStep> clockWorkServerJobStepsByJobId = this.GetClockWorkServerJobStepsByJobId(jobId);
			ClockWorkServerJobInfo.Credentials credentials;
			if (reader["ImpersonationDomain"] is DBNull || reader["ImpersonationUsername"] is DBNull || reader["ImpersonationPassword"] is DBNull)
			{
				credentials = null;
			}
			else
			{
				ClockWorkServerJobInfo.Credentials credentials2 = new ClockWorkServerJobInfo.Credentials();
				credentials2.Domain = (string)reader["ImpersonationDomain"];
				credentials2.Username = (string)reader["ImpersonationUsername"];
				credentials = credentials2;
				credentials2.Password = decryptor.Decrypt((byte[])reader["ImpersonationPassword"]);
			}
			ClockWorkServerJobInfo.Credentials impersonate = credentials;
			return new ClockWorkServerJobInfo
			{
				JobId = jobId,
				JobSteps = clockWorkServerJobStepsByJobId,
				Title = (string)reader["JobTitle"],
				Notes = (string)reader["JobNotes"],
				StartTime = TimeSpan.FromMinutes((double)((int)reader["StartTimeInMinutes"])),
				JobSchedule = ClockWorkServerJobSchedule.FromXml((string)reader["JobSchedule"]),
				Timeout = TimeSpan.FromMinutes((double)((int)reader["TimeoutInMinutes"])),
				IsActive = (bool)reader["IsJobActive"],
				JobUniqueId = (Guid)reader["JobUniqueId"],
				LastRunStartDatetime = ((reader["LastRunStartDatetime"] is DBNull) ? null : new DateTime?((DateTime)reader["LastRunStartDatetime"])),
				LastRunEndDatetime = ((reader["LastRunEndDatetime"] is DBNull) ? null : new DateTime?((DateTime)reader["LastRunEndDatetime"])),
				LastRunStatus = (string.IsNullOrEmpty((string)reader["LastRunStatus"]) ? eClockWorkServerJobResult.UnKnown : ((eClockWorkServerJobResult)Enum.Parse(typeof(eClockWorkServerJobResult), (string)reader["LastRunStatus"]))),
				LastRunMessage = (string)reader["LastRunMessage"],
				Impersonate = impersonate,
				IsSystemJob = Convert.ToBoolean(reader["IsSystemJob"])
			};
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00051080 File Offset: 0x0004F280
		private ClockWorkServerJobStep GetClockWorkServerJobStepFromReader(IDataReader reader)
		{
			return new ClockWorkServerJobStep
			{
				StepId = (int)reader["JobStepId"],
				JobId = (int)reader["FK_JobId"],
				JobType = (string)reader["JobType"],
				Title = (string)reader["StepTitle"],
				Notes = (string)reader["StepNotes"],
				Parameters = (string)reader["StepParams"],
				OrderNum = (int)reader["StepOrderNum"],
				IsActive = (bool)reader["IsStepActive"]
			};
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00051150 File Offset: 0x0004F350
		private int AddClockWorkServerJobStep(ClockWorkServerJobStep clockWorkServerJobStep, DbTransaction dbTrans = null)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@jobstepid", DbType.Int32, 0),
				databaseLayer.GetParameter("@jobid", DbType.Int32, clockWorkServerJobStep.JobId),
				databaseLayer.GetParameter("@jobtype", DbType.String, clockWorkServerJobStep.JobType ?? string.Empty),
				databaseLayer.GetParameter("@steptitle", DbType.String, clockWorkServerJobStep.Title ?? string.Empty),
				databaseLayer.GetParameter("@stepnotes", DbType.String, clockWorkServerJobStep.Notes ?? string.Empty),
				databaseLayer.GetParameter("@stepparams", DbType.String, clockWorkServerJobStep.Parameters ?? string.Empty),
				databaseLayer.GetParameter("@stepordernum", DbType.Int32, clockWorkServerJobStep.OrderNum),
				databaseLayer.GetParameter("@isstepactive", DbType.Boolean, clockWorkServerJobStep.IsActive)
			};
			bool flag = dbTrans == null;
			if (flag)
			{
				databaseLayer.ExecuteStoredProcedure("sp_ClockWorkServerJob_AddJobStep", array);
			}
			else
			{
				databaseLayer.ExecuteStoredProcedureTransaction("sp_ClockWorkServerJob_AddJobStep", dbTrans, array);
			}
			return clockWorkServerJobStep.StepId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x000512AF File Offset: 0x0004F4AF
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x000512B7 File Offset: 0x0004F4B7
		public OperationContext OpContext { get; set; }
	}
}
