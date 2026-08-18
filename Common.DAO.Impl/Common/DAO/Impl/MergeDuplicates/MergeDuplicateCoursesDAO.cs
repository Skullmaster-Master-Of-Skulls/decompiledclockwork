using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.MergeDuplicates;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MergeDuplicates.Courses;

namespace TechnoPro.Common.DAO.Impl.MergeDuplicates
{
	// Token: 0x0200008C RID: 140
	public class MergeDuplicateCoursesDAO : IMergeDuplicateCoursesDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x000202C5 File Offset: 0x0001E4C5
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x000202CD File Offset: 0x0001E4CD
		public OperationContext OpContext { get; set; }

		// Token: 0x060003A3 RID: 931 RVA: 0x000202D6 File Offset: 0x0001E4D6
		public MergeDuplicateCoursesDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00020308 File Offset: 0x0001E508
		public List<DuplicateCourseMergeResult> MergeDuplicateCourseRegistrationsWithSameLuCourseIdForStudents(DateTime StartDate, DateTime EndDate)
		{
			string query = "SELECT personid,lucourseid,COUNT(*) AS ct,MAX(DateLetterIssued) AS maxdli\r\nFROM Courses \r\nWHERE (registrationstatus IS NULL OR NOT registrationstatus=2)\r\nAND lucourseid IN (SELECT lucourseid FROM lucourses WHERE NOT ( enddate <= @startdate OR startdate > @enddate)) \r\nGROUP BY personID,lucourseid\r\nHAVING COUNT(*)>1\r\nORDER BY personID,lucourseid";
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery(query, parameters);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["personid"];
				int num2 = (int)dataRow["lucourseid"];
				query = "SELECT coursesid,personid,lucourseid,dateletterissued FROM courses WHERE personid=@pid AND lucourseid=@lucid AND (registrationstatus IS NULL OR NOT registrationstatus=2) ORDER BY dateletterissued DESC";
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@pid", DbType.Int32, num),
					this.DatabaseManager.GetParameter("@lucid", DbType.Int32, num2)
				};
				DataTable dataTable2 = this.DatabaseManager.ExecuteQuery(query, parameters);
				bool flag = dataTable2.Rows.Count > 1;
				if (flag)
				{
					DateTime? dateTime = null;
					int num3 = (int)dataTable2.Rows[0][0];
					foreach (object obj2 in dataTable2.Rows)
					{
						DataRow dataRow2 = (DataRow)obj2;
						bool flag2 = dataRow2["dateletterissued"] != DBNull.Value;
						if (flag2)
						{
							DateTime dateTime2 = (DateTime)dataRow2["dateletterissued"];
							bool flag3 = dateTime == null || dateTime2 > dateTime.Value;
							if (flag3)
							{
								num3 = (int)dataRow2[0];
								dateTime = new DateTime?(dateTime2);
							}
						}
					}
					bool flag4 = dateTime == null;
					DbParameter parameter;
					if (flag4)
					{
						parameter = this.DatabaseManager.GetParameter("@dli", DbType.DateTime, DBNull.Value);
					}
					else
					{
						parameter = this.DatabaseManager.GetParameter("@dli", DbType.DateTime, dateTime.Value);
					}
					query = "UPDATE courses SET registrationstatus=2 WHERE personid=@pid AND lucourseid=@lucid AND (registrationstatus IS NULL OR NOT registrationstatus=2) AND NOT coursesid=@coursesid; UPDATE courses SET dateletterissued=@dli WHERE coursesid=@coursesid";
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@pid", DbType.Int32, num),
						this.DatabaseManager.GetParameter("@lucid", DbType.Int32, num2),
						this.DatabaseManager.GetParameter("@coursesid", DbType.Int32, num3),
						parameter
					};
					this.DatabaseManager.ExecuteNonQuery(query, parameters);
				}
			}
			return null;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00020608 File Offset: 0x0001E808
		public List<DuplicateCourseMergeResult> MergeDuplicateCourseRegistrationsWithSameLuCourseIdForServiceProviders(DateTime StartDate, DateTime EndDate)
		{
			string query = "SELECT serviceproviderapplicationid,serviceprovidertype,lucourseid,COUNT(*) AS ct\r\nFROM ServiceProviderApplicationCourses\r\nWHERE lucourseid IN (SELECT lucourseid FROM lucourses WHERE NOT ( enddate <= @startdate OR startdate > @enddate)) \r\nAND (registrationstatus IS NULL OR NOT registrationstatus=2)\r\nGROUP BY serviceproviderapplicationid,serviceprovidertype,lucourseid\r\nHAVING COUNT(*)>1\r\nORDER BY serviceproviderapplicationid,lucourseid";
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			DataTable dataTable = this.DatabaseManager.ExecuteQuery(query, parameters);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["serviceproviderapplicationid"];
				int num2 = (int)dataRow["serviceprovidertype"];
				int num3 = (int)dataRow["lucourseid"];
				query = "SELECT serviceproviderapplicationcourseid FROM serviceproviderapplicationcourses WHERE serviceproviderapplicationid=@spaid AND serviceprovidertype=@sptype AND lucourseid=@lucid AND (registrationstatus IS NULL OR NOT registrationstatus=2)";
				parameters = new DbParameter[]
				{
					this.DatabaseManager.GetParameter("@spaid", DbType.Int32, num),
					this.DatabaseManager.GetParameter("@sptype", DbType.Int32, num2),
					this.DatabaseManager.GetParameter("@lucid", DbType.Int32, num3)
				};
				DataTable dataTable2 = this.DatabaseManager.ExecuteQuery(query, parameters);
				bool flag = dataTable2.Rows.Count > 1;
				if (flag)
				{
					int num4 = (int)dataTable2.Rows[0][0];
					query = "UPDATE serviceProviderApplicationCourses SET registrationstatus=2 WHERE serviceproviderapplicationid=@spaid AND serviceprovidertype=@sptype AND lucourseid=@lucid AND (registrationstatus IS NULL OR NOT registrationstatus=2) AND NOT serviceproviderapplicationcourseid=@spacid";
					parameters = new DbParameter[]
					{
						this.DatabaseManager.GetParameter("@spaid", DbType.Int32, num),
						this.DatabaseManager.GetParameter("@sptype", DbType.Int32, num2),
						this.DatabaseManager.GetParameter("@lucid", DbType.Int32, num3),
						this.DatabaseManager.GetParameter("@spacid", DbType.Int32, num4)
					};
					this.DatabaseManager.ExecuteNonQuery(query, parameters);
				}
			}
			return null;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00020834 File Offset: 0x0001EA34
		public List<DuplicateCourseMergeResult> ExecuteCourseMergeActions(List<DuplicateCourseMergeAction> Actions)
		{
			DbTransaction transaction = this.DatabaseManager.BeginDbTransaction();
			List<DuplicateCourseMergeResult> list = new List<DuplicateCourseMergeResult>();
			try
			{
				list.Add(new DuplicateCourseMergeResult
				{
					Action = null,
					Status = eDuplicateCourseMergeStatus.BatchProcessStarted,
					ErrorMessage = null
				});
				foreach (DuplicateCourseMergeAction duplicateCourseMergeAction in Actions)
				{
					eDuplicateCourseMergeActionType actionType = duplicateCourseMergeAction.ActionType;
					eDuplicateCourseMergeActionType eDuplicateCourseMergeActionType = actionType;
					string text;
					if (eDuplicateCourseMergeActionType != eDuplicateCourseMergeActionType.ChangeLucid)
					{
						if (eDuplicateCourseMergeActionType != eDuplicateCourseMergeActionType.RemoveLookupCourse)
						{
							text = "";
						}
						else
						{
							text = string.Format("ALTER TABLE lucourses DISABLE TRIGGER ALL;\r\nUPDATE lucourses SET startdate=dateadd(year,-20,startdate),enddate=dateadd(year,-20,enddate) WHERE lucourseid={0};\r\nALTER TABLE lucourses ENABLE TRIGGER ALL;", duplicateCourseMergeAction.NewLucid.ToString());
						}
					}
					else
					{
						text = string.Format("UPDATE {0} SET {1}={2} WHERE {1}={3}", new object[]
						{
							duplicateCourseMergeAction.TableAndColumnToApplyTo.Table.ToString(),
							duplicateCourseMergeAction.TableAndColumnToApplyTo.Column.ToString(),
							duplicateCourseMergeAction.NewLucid.ToString(),
							duplicateCourseMergeAction.OldLucid.ToString()
						});
					}
					bool flag = !string.IsNullOrEmpty(text);
					if (flag)
					{
						this.DatabaseManager.ExecuteNonQueryTransaction(text, transaction);
						list.Add(new DuplicateCourseMergeResult
						{
							ErrorMessage = null,
							Status = eDuplicateCourseMergeStatus.Success,
							Action = duplicateCourseMergeAction
						});
					}
				}
				this.DatabaseManager.CommitDbTransaction(transaction);
				list.Add(new DuplicateCourseMergeResult
				{
					Action = null,
					Status = eDuplicateCourseMergeStatus.BatchProcessCompletedSuccessfully,
					ErrorMessage = null
				});
			}
			catch (DbException ex)
			{
				this.DatabaseManager.RollbackDbTransaction(transaction);
				CWLogger.Logger.Error("ExecuteCourseMergeActions:{0}", ex.ToString());
				list.Add(new DuplicateCourseMergeResult
				{
					Action = null,
					Status = eDuplicateCourseMergeStatus.BatchProcessFailedInterrupted,
					ErrorMessage = ex.ToString()
				});
			}
			return list;
		}

		// Token: 0x040001A8 RID: 424
		private DatabaseLayer DatabaseManager;
	}
}
