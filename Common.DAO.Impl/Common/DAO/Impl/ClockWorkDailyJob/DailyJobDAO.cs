using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.ClockWorkDailyJob;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkDailyJob;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.DAO.Impl.ClockWorkDailyJob
{
	// Token: 0x02000113 RID: 275
	public class DailyJobDAO : IDailyJobDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060007D9 RID: 2009 RVA: 0x00051418 File Offset: 0x0004F618
		public DailyJobDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00051448 File Offset: 0x0004F648
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00051450 File Offset: 0x0004F650
		public OperationContext OpContext { get; set; }

		// Token: 0x060007DC RID: 2012 RVA: 0x0005145C File Offset: 0x0004F65C
		private IList<DailyJobTask> LoadDailyJobTasks(int groupId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskgroupid", DbType.Int32, groupId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    w.windowstaskjobid,w.taskgroupid,w.searchinfoid AS reportid,\r\n            si.title AS reporttitle,si.description AS reportdescription,\r\n            w.arguments,w.isactive,w.lastrunstartdate,w.lastrunenddate,w.lastrunresult,\r\n            w.description,w.ordernum\r\nFROM        windowstaskjob w LEFT JOIN searchinfo si ON si.searchinfoid=w.searchinfoid\r\nWHERE       @taskgroupid=-1 OR w.taskgroupid=@taskgroupid\r\nORDER BY    w.taskgroupid,w.ordernum", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<DailyJobTask> list = new List<DailyJobTask>();
					while (dataReader.Read())
					{
						DailyJobTask dailyJobTaskFromRecord = this.GetDailyJobTaskFromRecord(dataReader);
						bool flag2 = dailyJobTaskFromRecord != null;
						if (flag2)
						{
							list.Add(dailyJobTaskFromRecord);
						}
					}
					return list;
				}
			}
			return null;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00051508 File Offset: 0x0004F708
		private DailyJobTask GetDailyJobTaskFromRecord(IDataReader record)
		{
			bool flag = record == null || record["windowstaskjobid"] == DBNull.Value;
			DailyJobTask result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new DailyJobTask
				{
					WindowsTaskJobId = (int)record["windowstaskjobid"],
					Arguments = record["arguments"].ToString(),
					Description = record["description"].ToString(),
					GroupId = (int)record["taskgroupid"],
					IsActive = Convert.ToBoolean(record["isactive"]),
					LastRunEndDate = ((record["lastrunenddate"] == DBNull.Value) ? null : new DateTime?((DateTime)record["lastrunenddate"])),
					LastRunStartDate = ((record["lastrunstartdate"] == DBNull.Value) ? null : new DateTime?((DateTime)record["lastrunstartdate"])),
					LastRunResult = record["lastrunresult"].ToString(),
					OrderNum = (int)record["ordernum"],
					ReportBase = this.GetReportBaseFromRecord(record)
				};
			}
			return result;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00051664 File Offset: 0x0004F864
		private ReportBase GetReportBaseFromRecord(IDataReader record)
		{
			bool flag = record == null || record["reportid"] == DBNull.Value;
			ReportBase result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ReportBase
				{
					ReportId = (int)record["reportid"],
					ReportDescription = record["reportdescription"].ToString(),
					ReportTitle = record["reporttitle"].ToString()
				};
			}
			return result;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x000516E0 File Offset: 0x0004F8E0
		public IList<DailyJobTask> LoadDailyJobTasksByGroup(int GroupId)
		{
			return this.LoadDailyJobTasks(GroupId);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000516FC File Offset: 0x0004F8FC
		public IList<DailyJobTask> LoadDailyJobTasks()
		{
			return this.LoadDailyJobTasks(-1);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00051718 File Offset: 0x0004F918
		public DailyJobTask LoadDailyJobTaskById(int WindowsTaskJobId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, WindowsTaskJobId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    w.windowstaskjobid,w.taskgroupid,w.searchinfoid AS reportid,\r\n            si.title AS reporttitle,si.description AS reportdescription,\r\n            w.arguments,w.isactive,w.lastrunstartdate,w.lastrunenddate,w.lastrunresult,\r\n            w.description,w.ordernum\r\nFROM        windowstaskjob w LEFT JOIN searchinfo si ON si.searchinfoid=w.searchinfoid\r\nWHERE       w.windowstaskjobid=@id\r\nORDER BY    w.taskgroupid,w.ordernum", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetDailyJobTaskFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x0005179C File Offset: 0x0004F99C
		public void UpdateDailyJobTask(DailyJobTask Task)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, Task.WindowsTaskJobId),
				this.DatabaseManager.GetParameter("@arguments", DbType.String, Task.Arguments),
				this.DatabaseManager.GetParameter("@description", DbType.String, Task.Description),
				this.DatabaseManager.GetParameter("@groupid", DbType.Int32, Task.GroupId),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, Task.IsActive),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, Task.OrderNum),
				this.DatabaseManager.GetParameter("@reportid", DbType.Int32, (Task.ReportBase == null) ? 0 : Task.ReportBase.ReportId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE windowstaskjob SET searchinfoid=@reportid,arguments=@arguments,isactive=@isactive,description=@description,ordernum=@ordernum,taskgroupid=@groupid\r\nWHERE windowstaskjobid=@id", parameters);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000518A8 File Offset: 0x0004FAA8
		public int CreateDailyJobTask(DailyJobTask Task)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, Task.WindowsTaskJobId),
				this.DatabaseManager.GetParameter("@arguments", DbType.String, Task.Arguments),
				this.DatabaseManager.GetParameter("@description", DbType.String, Task.Description),
				this.DatabaseManager.GetParameter("@groupid", DbType.Int32, Task.GroupId),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, Task.IsActive),
				this.DatabaseManager.GetParameter("@ordernum", DbType.Int32, Task.OrderNum),
				this.DatabaseManager.GetParameter("@reportid", DbType.Int32, (Task.ReportBase == null) ? 0 : Task.ReportBase.ReportId)
			};
			object obj = this.DatabaseManager.ExecuteScalar("INSERT INTO windowstaskjob \r\n    (taskgroupid,searchinfoid,arguments,isactive,lastrunstartdate,lastrunenddate,lastrunresult,description,ordernum)\r\nVALUES\r\n    (@taskgroupid,@reportid,@arguments,@isactive,@lastrunstartdate,@lastrunenddate,@lastrunresult,@description,@ordernum);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS windowstaskjobid", parameters);
			Task.WindowsTaskJobId = (int)obj;
			return Task.WindowsTaskJobId;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000519CC File Offset: 0x0004FBCC
		public void DeleteDailyJobTask(int WindowsTaskJobId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, WindowsTaskJobId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM windowstaskjob WHERE windowstaskjobid=@id", parameters);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00051A10 File Offset: 0x0004FC10
		public void ChangeTaskActiveStatus(int WindowsTaskJobId, bool NewIsActive)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, WindowsTaskJobId),
				this.DatabaseManager.GetParameter("@isactive", DbType.Boolean, NewIsActive)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE windowstaskjob SET isactive=@isactive WHERE windowstaskjobid=@id", parameters);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00051A6C File Offset: 0x0004FC6C
		public IList<int> GetActiveDailyJobGroups()
		{
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT taskgroupid FROM windowstaskjob ORDER BY taskgroupid"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						bool flag2 = dataReader["taskgroupid"] != DBNull.Value;
						if (flag2)
						{
							int item = (int)dataReader["taskgroupid"];
							bool flag3 = !list.Contains(item);
							if (flag3)
							{
								list.Add(item);
							}
						}
					}
					return list;
				}
			}
			return new List<int>();
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00051B20 File Offset: 0x0004FD20
		public int LogDailyJobRunStart(int TaskGroupId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@taskgroupid", DbType.Int32, TaskGroupId),
				this.DatabaseManager.GetParameter("@runstartdate", DbType.DateTime, DateTime.Now),
				this.DatabaseManager.GetParameter("@runenddate", DbType.DateTime, DBNull.Value),
				this.DatabaseManager.GetParameter("@runresult", DbType.String, ""),
				this.DatabaseManager.GetParameter("@runcomment", DbType.String, "")
			};
			object obj = this.DatabaseManager.ExecuteScalar("INSERT INTO windowstaskjobsetresults \r\n    (taskgroupid,runstartdate,runenddate,runresult,runcomment)\r\nVALUES\r\n    (@taskgroupid,@runstartdate,@runenddate,@runresult,@runcomment);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS windowstaskjobsetresultsid", parameters);
			bool flag = obj != null && obj is int;
			int result;
			if (flag)
			{
				result = (int)obj;
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00051BF0 File Offset: 0x0004FDF0
		public void LogDailyJobRunEnd(int WindowsTaskJobSetResultsId, string runResult, string runComment)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, WindowsTaskJobSetResultsId),
				this.DatabaseManager.GetParameter("@runenddate", DbType.DateTime, DateTime.Now),
				this.DatabaseManager.GetParameter("@runresult", DbType.String, runResult),
				this.DatabaseManager.GetParameter("@runcomment", DbType.String, runComment)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE windowstaskjobsetresults SET runenddate=@runenddate,runresult=@runresult,runcomment=@runcomment WHERE windowstaskjobsetresultsid=@id", parameters);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00051C7C File Offset: 0x0004FE7C
		public void LogDailyJobTaskRunEnd(int WindowsTaskJobResultId, int WindowsTaskJobId, DateTime StartDate, bool Successful, string Result)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@windowstaskjobresultid", DbType.Int32, WindowsTaskJobResultId),
				this.DatabaseManager.GetParameter("@successful", DbType.Boolean, Successful),
				this.DatabaseManager.GetParameter("@runresult", DbType.String, Result ?? "")
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE WindowsTaskJobResult SET successful=@successful,runresult=@runresult,enddate=getdate() WHERE WindowsTaskJobResultId=@windowstaskjobresultid", parameters);
			parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@sd", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@result", DbType.String, string.IsNullOrEmpty(Result) ? "Ok" : Result),
				this.DatabaseManager.GetParameter("@id", DbType.Int32, WindowsTaskJobId)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE windowstaskjob SET lastrunstartdate=@sd,lastrunenddate=getdate(),lastrunresult=@result\r\nWHERE windowstaskjobid=@id", parameters);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00051D70 File Offset: 0x0004FF70
		public int LogDailyJobTaskRunStart(int WindowsTaskJobId, int ReportId, int TaskGroupId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@windowstaskjobid", DbType.Int32, WindowsTaskJobId),
				this.DatabaseManager.GetParameter("@reportid", DbType.Int32, ReportId),
				this.DatabaseManager.GetParameter("@taskgroupid", DbType.Int32, TaskGroupId),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, DBNull.Value),
				this.DatabaseManager.GetParameter("@successful", DbType.Boolean, false),
				this.DatabaseManager.GetParameter("@runresult", DbType.String, "")
			};
			object obj = this.DatabaseManager.ExecuteScalar("INSERT INTO WindowsTaskJobResult (windowstaskjobid,reportid,taskgroupid,enddate,successful,runresult) VALUES (@windowstaskjobid,@reportid,@taskgroupid,@enddate,@successful,@runresult);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS windowstaskjobresultid", parameters);
			bool flag = obj == null || !(obj is int);
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				result = (int)obj;
			}
			return result;
		}

		// Token: 0x04000494 RID: 1172
		private DatabaseLayer DatabaseManager;
	}
}
