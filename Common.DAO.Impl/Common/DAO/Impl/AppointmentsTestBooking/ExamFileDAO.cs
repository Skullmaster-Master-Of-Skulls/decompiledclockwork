using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x02000148 RID: 328
	public class ExamFileDAO : IExamFileDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x00062EB0 File Offset: 0x000610B0
		public ExamFileDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00062EC2 File Offset: 0x000610C2
		// (set) Token: 0x06000982 RID: 2434 RVA: 0x00062ECA File Offset: 0x000610CA
		public OperationContext OpContext { get; set; }

		// Token: 0x06000983 RID: 2435 RVA: 0x00062ED4 File Offset: 0x000610D4
		private ExamFile GetExamFileFromRecord(IDataReader record)
		{
			bool flag = record == null;
			ExamFile result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ExamFile
				{
					ExamFileId = ((record["examfileid"] is DBNull) ? 0 : ((int)record["examfileid"])),
					ExamId = ((record["examid"] is DBNull) ? 0 : ((int)record["examid"])),
					DateEntered = ((record["dateentered"] is DBNull) ? DateTime.MinValue : ((DateTime)record["dateentered"])),
					WhoEntered = ((record["whoentered"] is DBNull) ? 0 : ((int)record["whoentered"])),
					Description = record["description"].ToString(),
					IsVisible = (record["visible"] != DBNull.Value && Convert.ToBoolean(record["visible"])),
					File = new BinaryFile
					{
						FileName = record["filename"].ToString(),
						ByteArray = ((record["filedata"] is DBNull) ? null : ((byte[])record["filedata"]))
					}
				};
			}
			return result;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00063044 File Offset: 0x00061244
		public IList<ExamFile> LoadExamFilesByExam(int ExamId, bool IncludeDeletedFiles, bool LoadFileData)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamId),
				databaseLayer.GetParameter("@includedeletedfiles", DbType.Boolean, IncludeDeletedFiles),
				databaseLayer.GetParameter("@loadfiles", DbType.Boolean, LoadFileData)
			};
			IList<ExamFile> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT   ef.examfileid,ef.examid,ef.filename,ef.dateentered,ef.whoentered,ef.description,ef.visible,\r\n            CASE WHEN @loadfiles=1 THEN ef.filedata ELSE CAST(NULL AS image) END AS filedata\r\nFROM examfiles ef \r\nWHERE   ef.examid=@examid AND (@includedeletedfiles=1 OR ef.visible=1)\r\nORDER BY ef.dateentered", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<ExamFile> list = new List<ExamFile>();
					while (dataReader.Read())
					{
						ExamFile examFileFromRecord = this.GetExamFileFromRecord(dataReader);
						bool flag2 = examFileFromRecord != null;
						if (flag2)
						{
							list.Add(examFileFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00063124 File Offset: 0x00061324
		public ExamFile LoadExamFileById(int ExamFileId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examfileid", DbType.Int32, ExamFileId)
			};
			ExamFile result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT   ef.examfileid,ef.examid,ef.filename,ef.dateentered,ef.whoentered,ef.description,ef.visible,ef.filedata\r\nFROM examfiles ef \r\nWHERE   ef.examfileid=@examfileid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetExamFileFromRecord(dataReader);
				}
			}
			return result;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000631B8 File Offset: 0x000613B8
		public void DeleteExamFile(int ExamFileId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examfileid", DbType.Int32, ExamFileId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM examfiles WHERE examfileid=@examfileid", parameters);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0006320C File Offset: 0x0006140C
		public IList<int> LoadExamFileIdsOlderThanDate(DateTime cutoffDate)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@dt", DbType.DateTime, cutoffDate.Date)
			};
			IList<int> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT DISTINCT examid FROM ExamFiles WHERE dateentered<@dt", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						int num = (dataReader["examid"] is DBNull) ? 0 : ((int)dataReader["examid"]);
						bool flag2 = num < 1;
						if (!flag2)
						{
							list.Add(num);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x000632E0 File Offset: 0x000614E0
		public IList<int> LoadExamFileIdsWhereCourseEndDateIsInThePast(int courseEndDateOffsetInDays)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@numDays", DbType.Int32, courseEndDateOffsetInDays)
			};
			IList<int> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @today datetime = getdate()\r\n\r\nSELECT\tDISTINCT ef.examid\r\nFROM\texamfiles ef LEFT JOIN exams e ON e.examid=ef.examid \r\n\t\tLEFT JOIN lucourses luc ON luc.LUCourseID=e.lucourseid\r\nWHERE\tluc.enddate IS NULL OR DATEADD(day,@numDays,luc.enddate)>@today AND NOT ef.examid IS NULL", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<int> list = new List<int>();
					while (dataReader.Read())
					{
						int num = (dataReader["examid"] is DBNull) ? 0 : ((int)dataReader["examid"]);
						bool flag2 = num < 1;
						if (!flag2)
						{
							list.Add(num);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00003998 File Offset: 0x00001B98
		public void MoveExamFilesToArchives(IList<int> examIds)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x000633B0 File Offset: 0x000615B0
		public int CreateExamFile(ExamFile ExamFile)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@examfileid", DbType.Int32, 0),
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamFile.ExamId),
				databaseLayer.GetParameter("@filename", DbType.String, ExamFile.File.FileName),
				databaseLayer.GetParameter("@filedata", DbType.Binary, ExamFile.File.ByteArray),
				databaseLayer.GetParameter("@whoentered", DbType.Int32, ExamFile.WhoEntered),
				databaseLayer.GetParameter("@description", DbType.String, ExamFile.Description ?? ""),
				databaseLayer.GetParameter("@visible", DbType.Boolean, ExamFile.IsVisible)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO examfiles \r\n        (examid,filename,filedata,whoentered,description,visible) \r\nVALUES  (@examid,@filename,@filedata,@whoentered,@description,@visible);\r\n\r\nSET @examfileid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))", array);
			ExamFile.ExamFileId = ((int?)array[0].Value).GetValueOrDefault();
			return ExamFile.ExamFileId;
		}
	}
}
