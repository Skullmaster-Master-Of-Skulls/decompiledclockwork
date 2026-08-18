using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Notetaking;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.DataStructure.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Notetaking;

namespace TechnoPro.Common.DAO.Impl.Notetaking
{
	// Token: 0x02000083 RID: 131
	public class NotetakerNotesDAO : INotetakerNotesDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000353 RID: 851 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public NotetakerNotesDAO()
		{
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001D1C4 File Offset: 0x0001B3C4
		public NotetakerNotesDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0001D1D6 File Offset: 0x0001B3D6
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0001D1DE File Offset: 0x0001B3DE
		public OperationContext OpContext { get; set; }

		// Token: 0x06000357 RID: 855 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		public IList<LectureNoteDescription> LoadLectureNoteDescriptions(DateTime courseStartDate, DateTime courseEndDate)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, courseStartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, courseEndDate.Date)
			};
			IList<LectureNoteDescription> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT NotetakerDocumentId INTO #t1 FROM NotetakerDocument WHERE LUCourseId IN (SELECT LUCourseId FROM LUCourses WHERE NOT ( ( enddate<@sd) OR (startdate > @ed) ))\r\n\r\nSELECT\tnd.NotetakerDocumentId,nd.lectureDate,nd.dateCreated,nd.notes,nd.docName,nd.sizeInBytes,nd.DeletionDate,\r\n\t\tnd.LUCourseId,luc.StartDate,luc.EndDate,luc.Duration,luc.Term,luc.SubjectID,lucd.altLookupString AS [subject],\r\n\t\tluc.[Section],luc.Course,luc.TimeOfDay,luc.campus,luc.department,luc.CourseNote,luc.location,\r\n\t\tnd.NotetakerID AS serviceproviderid,sp.email,sp.firstname,sp.middlename,sp.lastname,sp.student_no,sp.altid\r\nFROM\tNotetakerDocument nd LEFT JOIN LUCourses luc ON luc.LUCourseID=nd.LUCourseId\r\n\t\tLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.SubjectID\r\n\t\tLEFT JOIN ServiceProviders sp ON sp.ServiceProviderId=nd.NotetakerID\r\nWHERE\tnd.NotetakerDocumentId IN (SELECT NotetakerDocumentId FROM #t1)\r\n\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LectureNoteDescription> list = new List<LectureNoteDescription>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						LectureNoteDescription lectureNoteDescriptionFromRecord = NotetakingDAO.GetLectureNoteDescriptionFromRecord(dataReader, batchDecryptor);
						bool flag2 = lectureNoteDescriptionFromRecord == null;
						if (!flag2)
						{
							list.Add(lectureNoteDescriptionFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001D2C8 File Offset: 0x0001B4C8
		public IList<LectureNoteDescription> LoadMarkedForDeletionLectureNoteDescriptions(DateTime courseStartDate, DateTime courseEndDate)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, courseStartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, courseEndDate.Date)
			};
			IList<LectureNoteDescription> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT NotetakerDocumentId INTO #t1 FROM NotetakerDocument WHERE LUCourseId IN (SELECT LUCourseId FROM LUCourses WHERE NOT ( ( enddate<@sd) OR (startdate > @ed) ))\r\n\r\nSELECT\tnd.NotetakerDocumentId,nd.lectureDate,nd.dateCreated,nd.notes,nd.docName,nd.sizeInBytes,nd.DeletionDate,\r\n\t\tnd.LUCourseId,luc.StartDate,luc.EndDate,luc.Duration,luc.Term,luc.SubjectID,lucd.altLookupString AS [subject],\r\n\t\tluc.[Section],luc.Course,luc.TimeOfDay,luc.campus,luc.department,luc.CourseNote,luc.location,\r\n\t\tnd.NotetakerID AS serviceproviderid,sp.email,sp.firstname,sp.middlename,sp.lastname,sp.student_no,sp.altid\r\nFROM\tNotetakerDocument nd LEFT JOIN LUCourses luc ON luc.LUCourseID=nd.LUCourseId\r\n\t\tLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.SubjectID\r\n\t\tLEFT JOIN ServiceProviders sp ON sp.ServiceProviderId=nd.NotetakerID\r\nWHERE\tnd.NotetakerDocumentId IN (SELECT NotetakerDocumentId FROM #t1)\r\n\r\nDROP TABLE #t1 AND NOT nd.DeletionDate IS NULL", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<LectureNoteDescription> list = new List<LectureNoteDescription>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						LectureNoteDescription lectureNoteDescriptionFromRecord = NotetakingDAO.GetLectureNoteDescriptionFromRecord(dataReader, batchDecryptor);
						bool flag2 = lectureNoteDescriptionFromRecord == null;
						if (!flag2)
						{
							list.Add(lectureNoteDescriptionFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public int DeleteAllNotesMarkedForDeletionTodayOrEarlier()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@ct", DbType.Int32, 0)
			};
			databaseLayer.ExecuteNonQuery("DECLARE @maxDate datetime = DATEADD(D, 1, DATEDIFF(D, 0, GETDATE()))\r\nSET @ct = (SELECT COUNT(NotetakerDocumentId) FROM NotetakerDocument WHERE NOT DeletionDate IS NULL AND DeletionDate<@maxDate)\r\nDELETE FROM NotetakerDocument WHERE NOT DeletionDate IS NULL AND DeletionDate<@maxDate", array);
			return (int)array[0].Value;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001D400 File Offset: 0x0001B600
		public int DeleteAllNotesMarkedForDeletion()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@ct", DbType.Int32, 0)
			};
			databaseLayer.ExecuteNonQuery("SET @ct = (SELECT COUNT(NotetakerDocumentId) FROM NotetakerDocument WHERE NOT DeletionDate IS NULL)\r\nDELETE FROM NotetakerDocument WHERE NOT DeletionDate IS NULL", array);
			return (int)array[0].Value;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0001D458 File Offset: 0x0001B658
		public void RemoveAllNotesDeletionMarks()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			databaseLayer.ExecuteNonQuery("UPDATE NotetakerDocument SET DeletionDate=NULL");
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001D484 File Offset: 0x0001B684
		public void RemoveNotesDeletionMarks(params int[] notetakerDocumentIds)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			List<int> items = notetakerDocumentIds.ToList<int>();
			IList<Chunk> list = items.BreakdownItemsIntoChunks(1000);
			foreach (Chunk chunk in list)
			{
				DbParameter[] array = new DbParameter[1];
				array[0] = databaseLayer.GetParameter("@ids", DbType.String, string.Join(",", (from g in chunk.GetChunkRange(items)
				select g.ToString()).ToArray<string>()));
				DbParameter[] parameters = array;
				databaseLayer.ExecuteNonQuery("UPDATE NotetakerDocument SET DeletionDate=NULL WHERE NotetakerDocumentId IN (SELECT orderid AS NotetakerDocumentId FROM SplitOrderIds(@ids,','))", parameters);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001D554 File Offset: 0x0001B754
		public void AddNotesDeletionMarks(DateTime newDateOfDeletion, params int[] notetakerDocumentIds)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			List<int> items = notetakerDocumentIds.ToList<int>();
			IList<Chunk> list = items.BreakdownItemsIntoChunks(1000);
			foreach (Chunk chunk in list)
			{
				DbParameter[] array = new DbParameter[2];
				array[0] = databaseLayer.GetParameter("@dt", DbType.DateTime, newDateOfDeletion);
				array[1] = databaseLayer.GetParameter("@ids", DbType.String, string.Join(",", (from g in chunk.GetChunkRange(items)
				select g.ToString()).ToArray<string>()));
				DbParameter[] parameters = array;
				databaseLayer.ExecuteNonQuery("UPDATE NotetakerDocument SET DeletionDate=@dt WHERE NotetakerDocumentId IN (SELECT orderid AS NotetakerDocumentId FROM SplitOrderIds(@ids,','))", parameters);
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001D63C File Offset: 0x0001B83C
		public IDictionary<DateTime, long> GetTotalFileSizeByMonth()
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			Dictionary<DateTime, long> dictionary = new Dictionary<DateTime, long>();
			IDictionary<DateTime, long> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT DATEADD(day,-DAY(nd.lectureDate)+1,nd.lectureDate) AS LectureDateMonthYear,nd.SizeInBytes INTO #t1 FROM NotetakerDocument nd \r\nSELECT\tDISTINCT LectureDateMonthYear,SUM(SizeInBytes) AS TotalSizeInBytes\r\nFROM\t#t1\r\nGROUP BY LectureDateMonthYear\r\nORDER BY LectureDateMonthYear DESC\r\n\r\nDROP TABLE #t1"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					while (dataReader.Read())
					{
						dictionary.Add((DateTime)dataReader["LectureDateMonthYear"], (long)((int)dataReader["TotalSizeInBytes"]));
					}
					result = dictionary;
				}
			}
			return result;
		}
	}
}
