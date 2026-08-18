using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkLogger;
using Databases;
using TechnoPro.Common.DAO.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.DAO.Impl.LookupCourses
{
	// Token: 0x020000A1 RID: 161
	public class AcademicTermDAO : IAcademicTermDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x000283BE File Offset: 0x000265BE
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x000283C6 File Offset: 0x000265C6
		public OperationContext OpContext { get; set; }

		// Token: 0x0600046B RID: 1131 RVA: 0x000283CF File Offset: 0x000265CF
		public AcademicTermDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000283E4 File Offset: 0x000265E4
		public IList<AcademicTerm> LoadAcademicTerms()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT lucoursesessiondateid,description,startmonth,startday,endmonth,endday FROM lucoursesessiondate"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					List<AcademicTerm> list = new List<AcademicTerm>();
					while (dataReader.Read())
					{
						AcademicTerm academicTerm = this.AcademicTermFromRecord(dataReader);
						bool flag2 = academicTerm != null;
						if (flag2)
						{
							list.Add(academicTerm);
						}
					}
					list.Sort((AcademicTerm t1, AcademicTerm t2) => t1.StartMonthDay.CompareTo(t2.StartMonthDay));
					return list;
				}
			}
			return null;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000284A8 File Offset: 0x000266A8
		public void ChangeCurrentAcademicTerms(IList<AcademicTerm> newAcademicTermList)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbTransaction transaction = databaseLayer.BeginDbTransaction();
			try
			{
				databaseLayer.ExecuteNonQueryTransaction("DELETE from LuCourseSessionDate", transaction);
				foreach (AcademicTerm academicTerm in newAcademicTermList)
				{
					DbParameter[] parameters = new DbParameter[]
					{
						databaseLayer.GetParameter("@desc", DbType.String, (academicTerm.Title ?? "").Trim()),
						databaseLayer.GetParameter("@sm", DbType.Int32, academicTerm.StartMonthDay.Month),
						databaseLayer.GetParameter("@sd", DbType.Int32, academicTerm.StartMonthDay.Day),
						databaseLayer.GetParameter("@em", DbType.Int32, academicTerm.EndMonthDay.Month),
						databaseLayer.GetParameter("@ed", DbType.Int32, academicTerm.EndMonthDay.Day)
					};
					databaseLayer.ExecuteNonQueryTransaction("INSERT INTO LuCourseSessionDate ([Description], startmonth, startday, endmonth, endday) VALUES (@desc,@sm,@sd,@em,@ed)", transaction, parameters);
				}
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("SessionDAO:ChangeCurrentAcademicTerms:FailedToInsert:Rolledback:err={0}", ex.ToString());
				databaseLayer.RollbackDbTransaction(transaction);
				return;
			}
			databaseLayer.CommitDbTransaction(transaction);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00028640 File Offset: 0x00026840
		private AcademicTerm AcademicTermFromRecord(IDataRecord record)
		{
			AcademicTerm academicTerm = new AcademicTerm
			{
				Title = record["description"].ToString(),
				TermId = (int)record["lucoursesessiondateid"]
			};
			AcademicTerm result;
			try
			{
				int num = (int)record["startmonth"];
				int num2 = (int)record["startday"];
				int num3 = (int)record["endmonth"];
				int num4 = (int)record["endday"];
				bool flag = num == num3 && num2 == num4;
				if (flag)
				{
					result = null;
				}
				else
				{
					academicTerm.StartMonthDay = new DateTime(DateTime.Now.Year, num, num2);
					academicTerm.EndMonthDay = new DateTime(DateTime.Now.Year, num3, num4, 23, 59, 59);
					bool flag2 = academicTerm.EndMonthDay < academicTerm.StartMonthDay;
					if (flag2)
					{
						academicTerm.EndMonthDay = academicTerm.EndMonthDay.AddYears(1);
					}
					result = academicTerm;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
