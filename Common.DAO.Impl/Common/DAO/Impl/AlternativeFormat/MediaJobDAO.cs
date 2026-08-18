using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AlternativeFormat;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.Common.DAO.Impl.AlternativeFormat
{
	// Token: 0x02000168 RID: 360
	public class MediaJobDAO : IMediaJobDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x0006F626 File Offset: 0x0006D826
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x0006F62E File Offset: 0x0006D82E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000A99 RID: 2713 RVA: 0x0006F637 File Offset: 0x0006D837
		public MediaJobDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0006F64C File Offset: 0x0006D84C
		public IList<CancelledMediaJob> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate)
		{
			List<CancelledMediaJob> list = new List<CancelledMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@assignedstaffid", DbType.Int32, assignedStaffId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy where mj.AssignedTo=@assignedstaffid AND mj.JobStartTime < @enddate AND mj.CancelledOn > @startdate\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CancelledMediaJob cancelledMediaJobFromReader = this.GetCancelledMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = cancelledMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(cancelledMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0006F73C File Offset: 0x0006D93C
		public IList<CancelledMediaJob> GetCancelledJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId)
		{
			List<CancelledMediaJob> list = new List<CancelledMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@assignedstaffid", DbType.Int32, assignedStaffId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy where mj.AssignedTo=@assignedstaffid AND mj.CampusId=@campusid AND (mj.JobStartTime < @enddate AND mj.CancelledOn > @startdate)\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CancelledMediaJob cancelledMediaJobFromReader = this.GetCancelledMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = cancelledMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(cancelledMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0006F844 File Offset: 0x0006DA44
		public IList<CompletedMediaJob> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@assignedstaffid", DbType.Int32, assignedStaffId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.AssignedTo=@assignedstaffid AND (mj.JobStartTime < @enddate AND mj.CompletedOn > @startdate)\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0006F934 File Offset: 0x0006DB34
		public IList<CompletedMediaJob> GetCompletedJobsByStaffAndDateRange(int assignedStaffId, DateTime startDate, DateTime endDate, int campusId)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@assignedstaffid", DbType.Int32, assignedStaffId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.AssignedTo=@assignedstaffid AND mj.CampusId=@campusid AND (mj.JobStartTime < @enddate AND mj.CompletedOn > @startdate)\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0006FA3C File Offset: 0x0006DC3C
		public bool AvailableJobsByContentFormatIdAndStudentId(int mediaContentPerFormatId, int studentId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormatId)
			};
			int num = Convert.ToInt32(databaseLayer.ExecuteScalar("SELECT count(*) FROM AlternativeFormat_MediaJob \r\n            where FKMediaContentPerFormatID = @mediacontentperformatid", parameters));
			return num > 0;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0006FA9C File Offset: 0x0006DC9C
		public int AddMediaJobNote(int mediaJobId, MediaJobRunningNote note)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] array = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@mediajobnoteid", DbType.Int32, 0),
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId),
				databaseLayer.GetParameter("@notetext", DbType.String, note.Notes),
				databaseLayer.GetParameter("@whomodifiedid", DbType.Int32, this.OpContext.WhoAmI)
			};
			databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_MediaJobNote]\r\n                   ([MediaJobId]\r\n                   ,[NoteText]\r\n                   ,[WhoModifiedId])\r\n             VALUES\r\n                   (@mediajobid\r\n                   ,@notetext\r\n                   ,@whomodifiedid)\r\n            set @mediajobnoteid = SCOPE_IDENTITY()", array);
			PeopleDAO peopleDAO = new PeopleDAO(this.OpContext);
			note.WhoModified = peopleDAO.LoadPerson(this.OpContext.WhoAmI);
			return note.NoteId = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0006FB88 File Offset: 0x0006DD88
		public void UpdateMediaJobNote(MediaJobRunningNote note)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediajobnoteid", DbType.Int32, note.NoteId),
				databaseLayer.GetParameter("@notetext", DbType.String, note.Notes),
				databaseLayer.GetParameter("@whomodifiedid", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@lastmodifieddatetime", DbType.DateTime, note.LastModifiedDatetime)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [AlternativeFormat_MediaJobNote]\r\n   SET [NoteText] = @notetext\r\n      ,[LastModifiedDatetime] = @lastmodifieddatetime\r\n      ,WhoModifiedId = @whomodifiedid\r\n WHERE MediaJobNoteId = @mediajobnoteid", parameters);
			PeopleDAO peopleDAO = new PeopleDAO(this.OpContext);
			note.WhoModified = peopleDAO.LoadPerson(this.OpContext.WhoAmI);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0006FC54 File Offset: 0x0006DE54
		public IList<MediaJobRunningNote> GetRunningNotesByMediaJob(int mediaJobId)
		{
			List<MediaJobRunningNote> list = new List<MediaJobRunningNote>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select MediaJobNoteId, MediaJobId, NoteText, LastModifiedDatetime,\r\n               WhoModifiedId as personid, p.firstname as firstname, p.lastname as lastname, p.middlename as middlename, p.student_no as student_no, pg.mingroupid AS groupid\r\n               from [AlternativeFormat_MediaJobNote]\r\n               LEFT JOIN people p ON p.personid=WhoModifiedId\r\n               LEFT JOIN peoplemingroup pg ON pg.personid=WhoModifiedId\r\n               where MediaJobId = @mediajobid\r\n               order by LastModifiedDatetime", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJobRunningNote mediaJobNoteFromReader = this.GetMediaJobNoteFromReader(dataReader, batchDecryptor);
						bool flag2 = mediaJobNoteFromReader != null;
						if (flag2)
						{
							list.Add(mediaJobNoteFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0006FD1C File Offset: 0x0006DF1C
		public MediaJob GetActiveMediaJobById(int mediaJobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where mj.MediaJobID=@mediajobid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetActiveMediaJobFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0006FDB0 File Offset: 0x0006DFB0
		public int GetCountActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentId),
				databaseLayer.GetParameter("@mediacontentformat", DbType.String, mediaContentFormat)
			};
			return (int)databaseLayer.ExecuteScalar("SELECT Count(mj.MediaJobID)\r\nFROM AlternativeFormat_MediaJob mj\r\nINNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nwhere mcxf.FKMediaContentID=@mediacontentid AND mcxf.MediaContentFormat=@mediacontentformat", parameters);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0006FE20 File Offset: 0x0006E020
		public IList<MediaJob> GetActiveMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0)
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormatId),
				databaseLayer.GetParameter("@studentpersonid", DbType.Int32, studentId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where mj.FKMediaContentPerFormatID=@mediacontentperformatid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				while (dataReader.Read())
				{
					MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
					bool flag = activeMediaJobFromReader != null;
					if (flag)
					{
						list.Add(activeMediaJobFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0006FEF0 File Offset: 0x0006E0F0
		public IList<MediaJob> GetActiveMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0)
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentId),
				databaseLayer.GetParameter("@mediacontentformat", DbType.String, mediaContentFormat),
				databaseLayer.GetParameter("@studentpersonid", DbType.Int32, studentPersonId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where mcxf.FKMediaContentID=@mediacontentid AND mcxf.MediaContentFormat=@mediacontentformat\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0006FFE4 File Offset: 0x0006E1E4
		public int GetCountActiveMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentId = 0)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormatId)
			};
			return (int)databaseLayer.ExecuteScalar("SELECT Count(MediaJobID)\r\nFROM AlternativeFormat_MediaJob\r\nwhere FKMediaContentPerFormatID=@mediacontentperformatid", parameters);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00070040 File Offset: 0x0006E240
		public IList<MediaJob> GetActiveMediaJobsByAssignedStaff(int assignedStaffId)
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@assignedstaffid", DbType.Int32, assignedStaffId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where mj.AssignedTo=@assignedstaffid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00070108 File Offset: 0x0006E308
		public IList<MediaJob> GetActiveMediaJobsByAssignedStaff(int assignedStaffId, int campusId)
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@assignedstaffid", DbType.Int32, assignedStaffId),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where mj.AssignedTo=@assignedstaffid AND mj.CampusId=@campusid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000701E4 File Offset: 0x0006E3E4
		public IList<MediaJob> GetActiveMediaJobsByExpiredInLessThan(TimeSpan dueDateIn)
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@jobduedate", DbType.DateTime, DateTime.Now.Add(dueDateIn));
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where mj.JobDueDate between GETDATE() AND @jobduedate\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", new DbParameter[]
			{
				parameter
			}))
			{
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				bool flag = dataReader != null;
				if (flag)
				{
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000702BC File Offset: 0x0006E4BC
		public IList<MediaJob> GetActiveExpiredMediaJobs()
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where mj.JobDueDate < GETDATE()\r\n            order by mj.JobPriority desc, mj.JobDueDate asc"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00070364 File Offset: 0x0006E564
		public IList<MediaJob> GetActiveJobs()
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId order by mj.JobPriority desc, mj.JobDueDate asc"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0007040C File Offset: 0x0006E60C
		public IList<MediaJob> GetActiveJobs(int campusId)
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId WHERE mj.CampusId=@campusid order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000704D4 File Offset: 0x0006E6D4
		public IList<MediaJob> GetActiveJobsByStudent(int studentPersonId)
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@studentid", DbType.Int32, studentPersonId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where smr.RequestMadeFromStudentNo=@studentid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0007059C File Offset: 0x0006E79C
		public IList<MediaJob> GetActiveJobsByStudent(int studentPersonId, int campusId)
		{
			List<MediaJob> list = new List<MediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate,\r\n                   mj.Publisher_JobActiveStatusName, mj.Vendor_JobActiveStatusName, mj.General_JobActiveStatusName, mj.InHouse_JobActiveStatusName,\r\n                   mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n                   mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n                   mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n                   mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n\t\t\t\t   mj.StartPageIndex,mj.EndPageIndex\r\n            FROM AlternativeFormat_MediaJob mj\r\n            INNER JOIN AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\n            LEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\n            LEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\n            LEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\n            LEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId where mj.CampusId=@campusid AND smr.RequestMadeFromStudentNo=@studentid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						MediaJob activeMediaJobFromReader = this.GetActiveMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = activeMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(activeMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00070678 File Offset: 0x0006E878
		public CancelledMediaJob GetCancelledMediaJobById(int mediaJobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy where mj.MediaJobID=@mediajobid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetCancelledMediaJobFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0007070C File Offset: 0x0006E90C
		public CompletedMediaJob GetCompletedMediaJobById(int mediaJobId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.MediaJobID=@mediajobid", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return this.GetCompletedMediaJobFromReader(dataReader, null);
				}
			}
			return null;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000707A0 File Offset: 0x0006E9A0
		public IList<CompletedMediaJob> GetCompletedMediaJobByMediaContentAndFormat(Guid mediaContentId, MediaContentFormat mediaContentFormat, int studentPersonId = 0)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaContentId),
				databaseLayer.GetParameter("@mediacontentformat", DbType.String, mediaContentFormat),
				databaseLayer.GetParameter("@studentpersonid", DbType.Int32, studentPersonId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mcxf.FKMediaContentID=@mediacontentid AND mcxf.MediaContentFormat=@mediacontentformat\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00070894 File Offset: 0x0006EA94
		public IList<CompletedMediaJob> GetCompletedMediaJobByMediaContentPerFormatId(int mediaContentPerFormatId, int studentPersonId = 0)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, mediaContentPerFormatId),
				databaseLayer.GetParameter("@studentpersonid", DbType.Int32, studentPersonId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.FKMediaContentPerFormatID=@mediacontentperformatid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				while (dataReader.Read())
				{
					CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
					bool flag = completedMediaJobFromReader != null;
					if (flag)
					{
						list.Add(completedMediaJobFromReader);
					}
				}
			}
			return list;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x00070964 File Offset: 0x0006EB64
		public IList<CompletedMediaJob> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@assignedstaffid", DbType.Int32, assignedStaffId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.AssignedTo=@assignedstaffid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00070A2C File Offset: 0x0006EC2C
		public IList<CompletedMediaJob> GetCompletedMediaJobsByAssignedStaff(int assignedStaffId, int campusId)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@assignedstaffid", DbType.Int32, assignedStaffId),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.AssignedTo=@assignedstaffid AND mj.CampusId=@campusid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00070B08 File Offset: 0x0006ED08
		public IList<CompletedMediaJob> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.JobStartTime < @enddate AND mj.CompletedOn > @startdate\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00070BE4 File Offset: 0x0006EDE4
		public IList<CompletedMediaJob> GetCompletedJobsByDateRange(DateTime startDate, DateTime endDate, int campusId)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.CampusId=@campusid AND (mj.JobStartTime < @enddate AND mj.CompletedOn > @startdate)\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00070CD4 File Offset: 0x0006EED4
		public IList<CancelledMediaJob> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate)
		{
			List<CancelledMediaJob> list = new List<CancelledMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy where mj.JobStartTime < @enddate AND mj.CancelledOn > @startdate\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CancelledMediaJob cancelledMediaJobFromReader = this.GetCancelledMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = cancelledMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(cancelledMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00070DB0 File Offset: 0x0006EFB0
		public IList<CancelledMediaJob> GetCancelledJobsByDateRange(DateTime startDate, DateTime endDate, int campusId)
		{
			List<CancelledMediaJob> list = new List<CancelledMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy where mj.CampusId=@campusid AND (mj.JobStartTime < @enddate AND mj.CancelledOn > @startdate)\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CancelledMediaJob cancelledMediaJobFromReader = this.GetCancelledMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = cancelledMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(cancelledMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00070EA0 File Offset: 0x0006F0A0
		public IList<CompletedMediaJob> GetCompletedJobs()
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy order by mj.JobPriority desc, mj.JobDueDate asc"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00070F48 File Offset: 0x0006F148
		public IList<CompletedMediaJob> GetCompletedJobs(int campusId)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.CampusId=@campusid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x00071010 File Offset: 0x0006F210
		public IList<CancelledMediaJob> GetCancelledJobs()
		{
			List<CancelledMediaJob> list = new List<CancelledMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy order by mj.JobPriority desc, mj.JobDueDate asc"))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CancelledMediaJob cancelledMediaJobFromReader = this.GetCancelledMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = cancelledMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(cancelledMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000710B8 File Offset: 0x0006F2B8
		public IList<CancelledMediaJob> GetCancelledJobs(int campusId)
		{
			List<CancelledMediaJob> list = new List<CancelledMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy where mj.CampusId=@campusid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CancelledMediaJob cancelledMediaJobFromReader = this.GetCancelledMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = cancelledMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(cancelledMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x00071180 File Offset: 0x0006F380
		public IList<CompletedMediaJob> GetCompletedJobsByStudent(int studentId)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter parameter = databaseLayer.GetParameter("@studentid", DbType.Int32, studentId);
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where smr.RequestMadeFromStudentNo=@studentid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", new DbParameter[]
			{
				parameter
			}))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00071248 File Offset: 0x0006F448
		public IList<CompletedMediaJob> GetCompletedJobsByStudent(int studentPersonId, int campusId)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.CampusId=@campusid AND smr.RequestMadeFromStudentNo=@studentid\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x00071324 File Offset: 0x0006F524
		public IList<CompletedMediaJob> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where smr.RequestMadeFromStudentNo=@studentid AND mj.JobStartTime < @enddate AND mj.CompletedOn > @startdate\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00071414 File Offset: 0x0006F614
		public IList<CompletedMediaJob> GetCompletedJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId)
		{
			List<CompletedMediaJob> list = new List<CompletedMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CompletedOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CompletedNotes, \r\n        mj.CompletedBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Completed_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CompletedBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CompletedBy where mj.CampusId=@campusid AND smr.RequestMadeFromStudentNo=@studentid AND (mj.JobStartTime < @enddate AND mj.CompletedOn > @startdate)\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CompletedMediaJob completedMediaJobFromReader = this.GetCompletedMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = completedMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(completedMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0007151C File Offset: 0x0006F71C
		public IList<CancelledMediaJob> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate)
		{
			List<CancelledMediaJob> list = new List<CancelledMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy where smr.RequestMadeFromStudentNo=@studentid AND mj.JobStartTime < @enddate AND mj.CancelledOn > @startdate\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CancelledMediaJob cancelledMediaJobFromReader = this.GetCancelledMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = cancelledMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(cancelledMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0007160C File Offset: 0x0006F80C
		public IList<CancelledMediaJob> GetCancelledJobsByStudentAndDateRange(int studentPersonId, DateTime startDate, DateTime endDate, int campusId)
		{
			List<CancelledMediaJob> list = new List<CancelledMediaJob>();
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@studentid", DbType.Int32, studentPersonId),
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate),
				databaseLayer.GetParameter("@campusid", DbType.Int32, campusId)
			};
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("select mj.MediaJobID, mj.JobTitle, mj.JobStartTime, mj.JobDueDate, mj.CancelledOn,\r\n        mj.AssignedTo as topersonid, pto.firstname as tofirstname, pto.lastname as tolastname, pto.middlename as tomiddlename, pto.student_no as tostudent_no, pgto.mingroupid AS togroupid,\r\n        mj.FKMediaContentPerFormatID, mcxf.MediaContentFormat, mcxf.FKMediaContentID, mj.JobPriority,\r\n        mj.WhoCreatedJobId as waddpersonid, pwadd.firstname as waddfirstname, pwadd.lastname as waddlastname, pwadd.middlename as waddmiddlename, pwadd.student_no as waddstudent_no, pgwadd.mingroupid AS waddgroupid,\r\n        mj.CampusId,clk.CampusName,clk.CampusDescription,clk.IsActive as CampusIsActive,\r\n        mj.ArchivedOn, mj.CancellationNotes, \r\n        mj.CancelledBy as wcompletedpersonid, pwcompleted.firstname as wcompletedfirstname, pwcompleted.lastname as wcompletedlastname, pwcompleted.middlename as wcompletedmiddlename, pwcompleted.student_no as wcompletedstudent_no, pgwcompleted.mingroupid AS wcompletedgroupid,\r\n\t\tmj.StartPageIndex,mj.EndPageIndex\r\nfrom AlternativeFormat_Cancelled_MediaJob mj\r\ninner join AlternativeFormat_MediaContent_x_MediaContentFormat mcxf on mj.FKMediaContentPerFormatID=mcxf.MediaContentPerFormatID\r\nLEFT JOIN people pto ON pto.personid=mj.AssignedTo\r\nLEFT JOIN peoplemingroup pgto ON pgto.personid=mj.AssignedTo\r\nLEFT JOIN people pwadd ON pwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN peoplemingroup pgwadd ON pgwadd.personid=mj.WhoCreatedJobId\r\nLEFT JOIN CampusLookup clk ON clk.CampusId=mj.CampusId\r\nLEFT JOIN people pwcompleted ON pwcompleted.personid=mj.CancelledBy\r\nLEFT JOIN peoplemingroup pgwcompleted ON pgwcompleted.personid=mj.CancelledBy where mj.CampusId=@campusid AND smr.RequestMadeFromStudentNo=@studentid AND (mj.JobStartTime < @enddate AND mj.CancelledOn > @startdate)\r\n            order by mj.JobPriority desc, mj.JobDueDate asc", parameters))
			{
				bool flag = dataReader != null;
				if (flag)
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						CancelledMediaJob cancelledMediaJobFromReader = this.GetCancelledMediaJobFromReader(dataReader, batchDecryptor);
						bool flag2 = cancelledMediaJobFromReader != null;
						if (flag2)
						{
							list.Add(cancelledMediaJobFromReader);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x00071714 File Offset: 0x0006F914
		public int CreateMediaJob(MediaJob mediaJob)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			int num = mediaJob.MediaContentPerFormatId;
			DbTransaction transaction = databaseLayer.BeginDbTransaction();
			bool flag = num == 0;
			if (flag)
			{
				DbParameter[] array = new DbParameter[]
				{
					databaseLayer.GetOutputParameter("@mediacontentperformatid", DbType.Int32, 0),
					databaseLayer.GetParameter("@mediacontentid", DbType.Guid, mediaJob.MediaContent.MediaContentUniqueId),
					databaseLayer.GetParameter("@mediacontentformat", DbType.String, mediaJob.MediaContentFormat.ToString())
				};
				databaseLayer.ExecuteNonQueryTransaction("if exists (select 1 from [AlternativeFormat_MediaContent_x_MediaContentFormat] where FKMediaContentID=@mediacontentid and MediaContentFormat=@mediacontentformat)\r\n\t            begin\r\n\t\t            set @mediacontentperformatid = (select MediaContentPerFormatID from [AlternativeFormat_MediaContent_x_MediaContentFormat] where FKMediaContentID=@mediacontentid and MediaContentFormat=@mediacontentformat)\r\n\t            end\r\n            else\r\n\t            begin\r\n\t\t            INSERT INTO [AlternativeFormat_MediaContent_x_MediaContentFormat]\r\n\t\t\t\t               ([FKMediaContentID]\r\n\t\t\t\t               ,[MediaContentFormat])\r\n\t\t            VALUES\r\n\t\t\t\t               (@mediacontentid\r\n\t\t\t\t               ,@mediacontentformat)\r\n\t\t            set @mediacontentperformatid = SCOPE_IDENTITY()\r\n\t            end", transaction, array);
				num = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
			}
			bool flag2 = num > 0;
			int result;
			if (flag2)
			{
				DbParameter[] array2 = new DbParameter[]
				{
					databaseLayer.GetOutputParameter("@mediajobid", DbType.Int32, 0),
					databaseLayer.GetParameter("@jobtitle", DbType.String, mediaJob.JobTitle ?? string.Empty),
					databaseLayer.GetParameter("@jobstarttime", DbType.DateTime, mediaJob.JobStartTime),
					databaseLayer.GetParameter("@jobduedate", DbType.DateTime, mediaJob.JobDueDate),
					databaseLayer.GetParameter("@JobCurrentStatusNameAboutPublisher", DbType.String, mediaJob.JobCurrentStatusNameAboutPublisher ?? string.Empty),
					databaseLayer.GetParameter("@JobCurrentStatusNameAboutVendor", DbType.String, mediaJob.JobCurrentStatusNameAboutVendor ?? string.Empty),
					databaseLayer.GetParameter("@JobCurrentStatusNameAboutInHouse", DbType.String, mediaJob.JobCurrentStatusNameAboutInHouse ?? string.Empty),
					databaseLayer.GetParameter("@JobCurrentStatusNameGeneral", DbType.String, mediaJob.JobCurrentStatusNameGeneral ?? string.Empty),
					databaseLayer.GetParameter("@assignedto", DbType.Int32, (mediaJob.AssignedTo != null) ? mediaJob.AssignedTo.PersonId : 0),
					databaseLayer.GetParameter("@mediacontentperformatid", DbType.Int32, num),
					databaseLayer.GetParameter("@whocreatedjobid", DbType.Int32, this.OpContext.WhoAmI),
					databaseLayer.GetParameter("@jobpriority", DbType.Int32, mediaJob.JobPriority),
					databaseLayer.GetParameter("@campusid", DbType.Int32, (mediaJob.Campus == null) ? DBNull.Value : mediaJob.Campus.CampusId),
					databaseLayer.GetParameter("@startpageindex", DbType.Int32, mediaJob.StartPageIndex),
					databaseLayer.GetParameter("@endpageindex", DbType.Int32, mediaJob.EndPageIndex)
				};
				databaseLayer.ExecuteNonQueryTransaction("INSERT INTO [AlternativeFormat_MediaJob]\r\n                        ([JobTitle]\r\n                        ,[JobStartTime]\r\n                        ,[JobDueDate]\r\n                        ,[AssignedTo]\r\n                        ,[FKMediaContentPerFormatID]\r\n                        ,[Publisher_JobActiveStatusName]\r\n                        ,[Vendor_JobActiveStatusName]\r\n                        ,[General_JobActiveStatusName]\r\n                        ,[InHouse_JobActiveStatusName]\r\n                        ,[WhoCreatedJobId]\r\n                        ,[JobPriority]\r\n                        ,[CampusId]\r\n                        ,[StartPageIndex]\r\n                        ,[EndPageIndex])\r\n            VALUES\r\n                        (@jobtitle\r\n                        ,@jobstarttime\r\n                        ,@jobduedate\r\n                        ,@assignedto\r\n                        ,@mediacontentperformatid\r\n                        ,@JobCurrentStatusNameAboutPublisher\r\n                        ,@JobCurrentStatusNameAboutVendor\r\n                        ,@JobCurrentStatusNameGeneral\r\n                        ,@JobCurrentStatusNameAboutInHouse\r\n                        ,@whocreatedjobid\r\n                        ,@jobpriority\r\n                        ,@campusid\r\n                        ,@startpageindex\r\n                        ,@endpageindex)\r\n           \r\n            SET @mediajobid = scope_identity()", transaction, array2);
				mediaJob.MediaJobId = ((array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value));
				bool flag3 = mediaJob.MediaJobId > 0;
				if (flag3)
				{
					bool flag4 = !string.IsNullOrEmpty(mediaJob.JobCurrentStatusNameAboutPublisher);
					if (flag4)
					{
						DbParameter[] parameters = new DbParameter[]
						{
							databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJob.MediaJobId),
							databaseLayer.GetParameter("@mediajobstatusname", DbType.String, mediaJob.JobCurrentStatusNameAboutPublisher),
							databaseLayer.GetParameter("@mediajobstatusgroupname", DbType.String, MediaJobStatusGroup.PublisherActionStatus.ToString()),
							databaseLayer.GetParameter("@whochangedstatusid", DbType.Int32, this.OpContext.WhoAmI),
							databaseLayer.GetParameter("@statuschangednotes", DbType.String, "Initial job status")
						};
						databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_Archive_MediaJob_x_MediaJobStatus]\r\n                   ([MediaJobID]\r\n                   ,[MediaJobStatusName]\r\n                   ,[MediaJobStatusGroupName]\r\n                   ,[WhoChangedStatusId]\r\n                   ,[StatusChangedNotes])\r\n             VALUES\r\n                   (@mediajobid\r\n                   ,@mediajobstatusname\r\n                   ,@mediajobstatusgroupname\r\n                   ,@whochangedstatusid\r\n                   ,@statuschangednotes)", parameters);
					}
					bool flag5 = !string.IsNullOrEmpty(mediaJob.JobCurrentStatusNameAboutVendor);
					if (flag5)
					{
						DbParameter[] parameters2 = new DbParameter[]
						{
							databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJob.MediaJobId),
							databaseLayer.GetParameter("@mediajobstatusname", DbType.String, mediaJob.JobCurrentStatusNameAboutVendor),
							databaseLayer.GetParameter("@mediajobstatusgroupname", DbType.String, MediaJobStatusGroup.VendorActionStatus.ToString()),
							databaseLayer.GetParameter("@whochangedstatusid", DbType.Int32, this.OpContext.WhoAmI),
							databaseLayer.GetParameter("@statuschangednotes", DbType.String, "Initial job status")
						};
						databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_Archive_MediaJob_x_MediaJobStatus]\r\n                   ([MediaJobID]\r\n                   ,[MediaJobStatusName]\r\n                   ,[MediaJobStatusGroupName]\r\n                   ,[WhoChangedStatusId]\r\n                   ,[StatusChangedNotes])\r\n             VALUES\r\n                   (@mediajobid\r\n                   ,@mediajobstatusname\r\n                   ,@mediajobstatusgroupname\r\n                   ,@whochangedstatusid\r\n                   ,@statuschangednotes)", parameters2);
					}
					bool flag6 = !string.IsNullOrEmpty(mediaJob.JobCurrentStatusNameAboutInHouse);
					if (flag6)
					{
						DbParameter[] parameters3 = new DbParameter[]
						{
							databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJob.MediaJobId),
							databaseLayer.GetParameter("@mediajobstatusname", DbType.String, mediaJob.JobCurrentStatusNameAboutInHouse),
							databaseLayer.GetParameter("@mediajobstatusgroupname", DbType.String, MediaJobStatusGroup.InHouseActionStatus.ToString()),
							databaseLayer.GetParameter("@whochangedstatusid", DbType.Int32, this.OpContext.WhoAmI),
							databaseLayer.GetParameter("@statuschangednotes", DbType.String, "Initial job status")
						};
						databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_Archive_MediaJob_x_MediaJobStatus]\r\n                   ([MediaJobID]\r\n                   ,[MediaJobStatusName]\r\n                   ,[MediaJobStatusGroupName]\r\n                   ,[WhoChangedStatusId]\r\n                   ,[StatusChangedNotes])\r\n             VALUES\r\n                   (@mediajobid\r\n                   ,@mediajobstatusname\r\n                   ,@mediajobstatusgroupname\r\n                   ,@whochangedstatusid\r\n                   ,@statuschangednotes)", parameters3);
					}
					bool flag7 = !string.IsNullOrEmpty(mediaJob.JobCurrentStatusNameGeneral);
					if (flag7)
					{
						DbParameter[] parameters4 = new DbParameter[]
						{
							databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJob.MediaJobId),
							databaseLayer.GetParameter("@mediajobstatusname", DbType.String, mediaJob.JobCurrentStatusNameGeneral),
							databaseLayer.GetParameter("@mediajobstatusgroupname", DbType.String, MediaJobStatusGroup.GeneralActionStatus.ToString()),
							databaseLayer.GetParameter("@whochangedstatusid", DbType.Int32, this.OpContext.WhoAmI),
							databaseLayer.GetParameter("@statuschangednotes", DbType.String, "Initial job status")
						};
						databaseLayer.ExecuteNonQuery("INSERT INTO [AlternativeFormat_Archive_MediaJob_x_MediaJobStatus]\r\n                   ([MediaJobID]\r\n                   ,[MediaJobStatusName]\r\n                   ,[MediaJobStatusGroupName]\r\n                   ,[WhoChangedStatusId]\r\n                   ,[StatusChangedNotes])\r\n             VALUES\r\n                   (@mediajobid\r\n                   ,@mediajobstatusname\r\n                   ,@mediajobstatusgroupname\r\n                   ,@whochangedstatusid\r\n                   ,@statuschangednotes)", parameters4);
					}
					databaseLayer.CommitDbTransaction(transaction);
					result = mediaJob.MediaJobId;
				}
				else
				{
					databaseLayer.RollbackDbTransaction(transaction);
					result = 0;
				}
			}
			else
			{
				databaseLayer.RollbackDbTransaction(transaction);
				result = 0;
			}
			return result;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00071D18 File Offset: 0x0006FF18
		public void UpdateMediaJob(MediaJob mediaJob)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJob.MediaJobId),
				databaseLayer.GetParameter("@jobtitle", DbType.String, mediaJob.JobTitle ?? string.Empty),
				databaseLayer.GetParameter("@jobduedate", DbType.DateTime, mediaJob.JobDueDate),
				databaseLayer.GetParameter("@assignedto", DbType.Int32, (mediaJob.AssignedTo != null) ? mediaJob.AssignedTo.PersonId : 0),
				databaseLayer.GetParameter("@jobpriority", DbType.Int32, (int)mediaJob.JobPriority),
				databaseLayer.GetParameter("@campusid", DbType.Int32, (mediaJob.Campus == null) ? DBNull.Value : mediaJob.Campus.CampusId),
				databaseLayer.GetParameter("@startpageindex", DbType.Int32, mediaJob.StartPageIndex),
				databaseLayer.GetParameter("@endpageindex", DbType.Int32, mediaJob.EndPageIndex)
			};
			databaseLayer.ExecuteNonQuery("UPDATE [AlternativeFormat_MediaJob]\r\n            SET JobDueDate=@jobduedate,\r\n\t            AssignedTo=@assignedto,\r\n                JobPriority=@jobpriority,\r\n                CampusId=@campusid,\r\n                JobTitle=@jobtitle,\r\n                StartPageIndex=@startpageindex,\r\n                EndPageIndex=@endpageindex\r\n            WHERE MediaJobID=@mediajobid", parameters);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00071E54 File Offset: 0x00070054
		public void CancelMediaJob(int mediaJobId, string changeNotes)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId),
				databaseLayer.GetParameter("@cancelledby", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@cancellationnotes", DbType.String, changeNotes ?? string.Empty)
			};
			databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_CancelMediaJob2", parameters);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00071EE0 File Offset: 0x000700E0
		public void MarkMediaJobAsCompleted(int mediaJobId, string changeNotes)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId),
				databaseLayer.GetParameter("@completedby", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@completednotes", DbType.String, changeNotes ?? string.Empty)
			};
			databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_MarkMediaJobAsCompleted2", parameters);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00071F6C File Offset: 0x0007016C
		public void ChangeMediaJobStatus(int mediaJobId, string changeNotes, string generalStatusnName, string publisherStatusName, string vendorStatusName, string inHouseStatusName)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@mediajobid", DbType.Int32, mediaJobId),
				databaseLayer.GetParameter("@generalstatusname", DbType.String, generalStatusnName ?? string.Empty),
				databaseLayer.GetParameter("@publisherstatusname", DbType.String, publisherStatusName ?? string.Empty),
				databaseLayer.GetParameter("@vendorstatusname", DbType.String, vendorStatusName ?? string.Empty),
				databaseLayer.GetParameter("@inhousestatusname", DbType.String, inHouseStatusName ?? string.Empty),
				databaseLayer.GetParameter("@whomadechange", DbType.Int32, this.OpContext.WhoAmI),
				databaseLayer.GetParameter("@changenotes", DbType.String, changeNotes ?? string.Empty)
			};
			databaseLayer.ExecuteStoredProcedure("sp_AlternateFormat_ChangeMediaJobStatus", parameters);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00072064 File Offset: 0x00070264
		private MediaJob GetActiveMediaJobFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			Guid mediaContentId = (Guid)record["FKMediaContentID"];
			IMediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
			MediaContent mediaContent = mediaContentDAO.LoadMediaContentById(mediaContentId);
			return new MediaJob
			{
				MediaJobId = Convert.ToInt32(record["MediaJobID"]),
				JobTitle = (string)record["JobTitle"],
				JobStartTime = (DateTime)record["JobStartTime"],
				JobDueDate = (DateTime)record["JobDueDate"],
				JobCurrentStatusNameAboutPublisher = Convert.ToString(record["Publisher_JobActiveStatusName"]),
				JobCurrentStatusNameAboutVendor = Convert.ToString(record["Vendor_JobActiveStatusName"]),
				JobCurrentStatusNameGeneral = Convert.ToString(record["General_JobActiveStatusName"]),
				JobCurrentStatusNameAboutInHouse = Convert.ToString(record["InHouse_JobActiveStatusName"]),
				AssignedTo = PeopleDAO.GetPersonFromReader("to", record, this.OpContext, decryptor),
				MediaContentFormat = (MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), Convert.ToString(record["MediaContentFormat"])),
				MediaContent = mediaContent,
				WhoCreatedJob = PeopleDAO.GetPersonFromReader("wadd", record, this.OpContext, decryptor),
				JobPriority = (eMediaJobPriority)record["JobPriority"],
				MediaContentPerFormatId = Convert.ToInt32(record["FKMediaContentPerFormatID"]),
				Campus = CampusDAO.GetCampusFromReader(record),
				IsCancelled = false,
				IsCompleted = false,
				StartPageIndex = Convert.ToInt32(record["StartPageIndex"]),
				EndPageIndex = Convert.ToInt32(record["EndPageIndex"])
			};
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0007223C File Offset: 0x0007043C
		private CancelledMediaJob GetCancelledMediaJobFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			Guid mediaContentId = (Guid)record["FKMediaContentID"];
			IMediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
			MediaContent mediaContent = mediaContentDAO.LoadMediaContentById(mediaContentId);
			return new CancelledMediaJob
			{
				MediaJobId = Convert.ToInt32(record["MediaJobID"]),
				JobTitle = (string)record["JobTitle"],
				JobStartTime = (DateTime)record["JobStartTime"],
				JobDueDate = (DateTime)record["JobDueDate"],
				JobCurrentStatusNameGeneral = "Cancelled",
				AssignedTo = PeopleDAO.GetPersonFromReader("to", record, this.OpContext, decryptor),
				IsCompleted = false,
				IsCancelled = true,
				MediaContentFormat = (MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), Convert.ToString(record["MediaContentFormat"])),
				MediaContent = mediaContent,
				WhoCreatedJob = PeopleDAO.GetPersonFromReader("wadd", record, this.OpContext, decryptor),
				JobPriority = (eMediaJobPriority)record["JobPriority"],
				MediaContentPerFormatId = Convert.ToInt32(record["FKMediaContentPerFormatID"]),
				Campus = CampusDAO.GetCampusFromReader(record),
				CancelledOn = (DateTime)record["CancelledOn"],
				CancellationReason = (string)record["CancellationNotes"],
				CancelledBy = PeopleDAO.GetPersonFromReader("wcancel", record, this.OpContext, decryptor),
				StartPageIndex = Convert.ToInt32(record["StartPageIndex"]),
				EndPageIndex = Convert.ToInt32(record["EndPageIndex"])
			};
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0007240C File Offset: 0x0007060C
		private CompletedMediaJob GetCompletedMediaJobFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			Guid mediaContentId = (Guid)record["FKMediaContentID"];
			IMediaContentDAO mediaContentDAO = new MediaContentDAO(this.OpContext);
			MediaContent mediaContent = mediaContentDAO.LoadMediaContentById(mediaContentId);
			return new CompletedMediaJob
			{
				MediaJobId = Convert.ToInt32(record["MediaJobID"]),
				JobTitle = (string)record["JobTitle"],
				JobStartTime = (DateTime)record["JobStartTime"],
				JobDueDate = (DateTime)record["JobDueDate"],
				JobCurrentStatusNameGeneral = "Completed",
				AssignedTo = PeopleDAO.GetPersonFromReader("to", record, this.OpContext, decryptor),
				IsCompleted = true,
				IsCancelled = false,
				MediaContentFormat = (MediaContentFormat)Enum.Parse(typeof(MediaContentFormat), Convert.ToString(record["MediaContentFormat"])),
				MediaContent = mediaContent,
				WhoCreatedJob = PeopleDAO.GetPersonFromReader("wadd", record, this.OpContext, decryptor),
				JobPriority = (eMediaJobPriority)record["JobPriority"],
				MediaContentPerFormatId = Convert.ToInt32(record["FKMediaContentPerFormatID"]),
				Campus = CampusDAO.GetCampusFromReader(record),
				CompletedOn = (DateTime)record["CompletedOn"],
				CompletedNotes = (string)record["CompletedNotes"],
				CompletedBy = PeopleDAO.GetPersonFromReader("wcompleted", record, this.OpContext, decryptor),
				StartPageIndex = Convert.ToInt32(record["StartPageIndex"]),
				EndPageIndex = Convert.ToInt32(record["EndPageIndex"])
			};
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000725DC File Offset: 0x000707DC
		private MediaJobRunningNote GetMediaJobNoteFromReader(IDataReader record, IBatchDecryptor decryptor = null)
		{
			return new MediaJobRunningNote
			{
				NoteId = Convert.ToInt32(record["MediaJobNoteId"]),
				Notes = Convert.ToString(record["NoteText"]),
				LastModifiedDatetime = (DateTime)record["LastModifiedDatetime"],
				WhoModified = PeopleDAO.GetPersonFromReader("", record, this.OpContext, decryptor)
			};
		}
	}
}
