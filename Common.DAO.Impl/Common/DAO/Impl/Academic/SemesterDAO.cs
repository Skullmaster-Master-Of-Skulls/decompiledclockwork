using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.Academic;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Academic;

namespace TechnoPro.Common.DAO.Impl.Academic
{
	// Token: 0x02000189 RID: 393
	public class SemesterDAO : ISemesterDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000B7B RID: 2939 RVA: 0x00079614 File Offset: 0x00077814
		public SemesterDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x00079626 File Offset: 0x00077826
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0007962E File Offset: 0x0007782E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000B7E RID: 2942 RVA: 0x00079638 File Offset: 0x00077838
		public int CreateSemester(Semester semester)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@semesterid", DbType.Int32, 0),
				databaseLayer.GetParameter("@title", DbType.String, semester.SemesterTitle ?? ""),
				databaseLayer.GetParameter("@sd", DbType.DateTime, semester.StartDate.Date),
				databaseLayer.GetParameter("@ed", DbType.DateTime, semester.EndDate.Date)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO semester (semestertitle,startdate,enddate) VALUES (@title,@sd,@ed) \r\nSELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS semesterid", array);
			DbParameter dbParameter = array[0];
			object obj = (dbParameter != null) ? dbParameter.Value : null;
			bool flag = obj == null || obj is DBNull || !(obj is int);
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

		// Token: 0x06000B7F RID: 2943 RVA: 0x00079728 File Offset: 0x00077928
		public void DeleteSemester(int semesterId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@semesterid", DbType.Int32, semesterId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM semester WHERE semesterid=@semesterid", parameters);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0007977C File Offset: 0x0007797C
		public void UpdateSemester(Semester semester)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@semesterid", DbType.Int32, semester.SemesterId),
				databaseLayer.GetParameter("@title", DbType.String, semester.SemesterTitle ?? ""),
				databaseLayer.GetParameter("@sd", DbType.DateTime, semester.StartDate.Date),
				databaseLayer.GetParameter("@ed", DbType.DateTime, semester.EndDate.Date)
			};
			databaseLayer.ExecuteNonQuery("UPDATE semester SET semestertitle=@title,startdate=@sd,enddate=@ed WHERE semesterid=@semesterid", parameters);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00079838 File Offset: 0x00077A38
		public static Semester GetSemesterFromRecord(IDataRecord record)
		{
			return new Semester
			{
				SemesterId = ((record["semesterid"] is DBNull) ? 0 : ((int)record["semesterid"])),
				SemesterTitle = record["semestertitle"].ToString().Trim(),
				StartDate = (DateTime)record["startdate"],
				EndDate = (DateTime)record["enddate"]
			};
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000798C8 File Offset: 0x00077AC8
		public Semester LoadNextSemester()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@now", DbType.DateTime, DateTime.Now.Date)
			};
			Semester result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT TOP 2 semesterid,semestertitle,startdate,enddate FROM semester WHERE startdate>=@now ORDER BY startdate", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<Semester> list = new List<Semester>();
					while (dataReader.Read())
					{
						Semester semesterFromRecord = SemesterDAO.GetSemesterFromRecord(dataReader);
						list.Add(semesterFromRecord);
					}
					result = ((list.Count >= 2) ? list[1] : null);
				}
			}
			return result;
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x00079994 File Offset: 0x00077B94
		public Semester LoadCurrentSemester()
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@now", DbType.DateTime, DateTime.Now.Date)
			};
			Semester result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT semesterid,semestertitle,startdate,enddate FROM semester WHERE @now >= startdate AND @now <= enddate", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = SemesterDAO.GetSemesterFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x00079A34 File Offset: 0x00077C34
		public Semester LoadSemesterById(int semesterId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@semesterid", DbType.Int32, semesterId)
			};
			Semester result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT semesterid,semestertitle,startdate,enddate FROM semester WHERE semesterid=@semesterid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = SemesterDAO.GetSemesterFromRecord(dataReader);
				}
			}
			return result;
		}
	}
}
