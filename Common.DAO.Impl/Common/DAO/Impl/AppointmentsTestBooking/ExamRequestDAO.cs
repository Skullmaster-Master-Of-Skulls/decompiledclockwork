using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Exceptions.DatabaseOperations;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x02000149 RID: 329
	public class ExamRequestDAO : IExamRequestDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000634C6 File Offset: 0x000616C6
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x000634CE File Offset: 0x000616CE
		public OperationContext OpContext { get; set; }

		// Token: 0x0600098D RID: 2445 RVA: 0x000634D7 File Offset: 0x000616D7
		public ExamRequestDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x000634EC File Offset: 0x000616EC
		private IList<ExamRequest> GetExamRequestsFromReader(IDataReader reader)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
			AccommodationsDAO accommodationsDAO = new AccommodationsDAO(this.OpContext);
			List<ExamRequest> list = new List<ExamRequest>();
			ExamRequest examRequest = null;
			while (reader.Read())
			{
				int num = (reader["examrequestid"] is DBNull) ? 0 : ((int)reader["examrequestid"]);
				bool flag = examRequest == null || examRequest.ExamRequestId != num;
				if (flag)
				{
					ExamRequest examRequestFromRecord = ExamRequestDAO.GetExamRequestFromRecord(reader, this.OpContext, batchDecryptor);
					bool flag2 = examRequestFromRecord == null;
					if (flag2)
					{
						continue;
					}
					list.Add(examRequestFromRecord);
					examRequest = examRequestFromRecord;
				}
				AccommodationData accommodationDataFromRecord = accommodationsDAO.GetAccommodationDataFromRecord(reader);
				bool flag3 = accommodationDataFromRecord != null;
				if (flag3)
				{
					examRequest.AccommodationsSelected.Add(accommodationDataFromRecord);
				}
			}
			return list;
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x000635E4 File Offset: 0x000617E4
		public static ExamRequest GetExamRequestFromRecord(IDataReader record, OperationContext opContext, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = record == null;
			ExamRequest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LookupCourseBaseWithPrimaryInstructor courseBaseFromReader = LookupCourseDAO.GetCourseBaseFromReader<LookupCourseBaseWithPrimaryInstructor>("", record);
				bool flag2 = courseBaseFromReader != null;
				if (flag2)
				{
					courseBaseFromReader.PrimaryInstructor = LookupInstructorDAO.GetInstructorFromReader(record, "");
				}
				DateTime dateTime = (DateTime)record["dateentered"];
				DateTime dateTime2 = (record["dateoftest"] is DBNull) ? dateTime : ((DateTime)record["dateoftest"]);
				result = new ExamRequest
				{
					Course = courseBaseFromReader,
					AccommodationsSelected = new List<AccommodationData>(),
					ClassTestStartDateTime = dateTime2,
					ClassTestEndDateTime = ((record["testduration"] is DBNull) ? dateTime2 : dateTime2.AddMinutes((double)((int)record["testduration"]))),
					ClassTestDescription = record["description"].ToString(),
					DateEntered = dateTime,
					ExamRequestId = (int)record["examrequestid"],
					Student = PeopleDAO.GetPersonFromReader("", record, opContext, batchDecryptor),
					InstructorName = (record["instructorfirstname"].ToString() + " " + record["instructorlastname"].ToString()).Trim(),
					InstructorEmail = record["instructoremail"].ToString().Trim(),
					InstructorSubmittedDescription = record["instructoracknowledged"].ToString()
				};
			}
			return result;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00063780 File Offset: 0x00061980
		public IList<ExamRequest> LoadRequestsByDateRange(DateTime StartDate, DateTime EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0))
			};
			IList<ExamRequest> examRequestsFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT er.ExamRequestId,er.personid,er.lucourseid,er.startdate AS erstartdate,er.enddate AS erenddate,er.dateentered,er.instructorfirstname,er.instructorlastname,er.instructoremail,er.instructorphone,\r\n    era.ExamRequestAccommodationId,era.controlid,\r\n    e.examid,e.dateentered AS examdateentered,e.whoentered AS examwhoentered,e.description,e.dateoftest,e.visible,e.usercomment,e.testduration,e.lastmodified,e.wholastmodified,e.typecode,e.extendedproperties,e.testpickedupdate,e.testpickedupnote,\r\n    e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,e.filename\r\n    ,luc.startdate,luc.enddate\r\n    ,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.timeofday,luc.section,v.instructorid,\r\n    lucd2.altlookupstring AS instructorname,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,\r\n    lucd2.username AS instructorusername,lucd2.externalid AS instructorexternalid,lucd2.id AS instructoremployeeid,\r\n    p.firstname,p.lastname,p.middlename,p.student_no\r\n    ,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.defaultvalue\r\n    ,ad.valtext,ad.valint,ad.valbytes,ad.valdate,ad.valimage,ad.valbytesisencrypted,ad.dataid,ad.courseid\r\n    ,acc.longdescription\r\nFROM examrequest er LEFT JOIN examrequestaccommodations era ON era.examrequestid=er.examrequestid\r\n    LEFT JOIN exams e ON e.lucourseid=er.lucourseid AND e.dateoftest>=@startdate AND e.dateoftest<=@enddate AND e.typecode='F'\r\n    LEFT JOIN lucourses luc ON luc.lucourseid=er.lucourseid\r\n    LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n    LEFT JOIN people p ON p.personid=er.personid\r\n    LEFT JOIN dynamiccontrols dc ON dc.controlid=era.controlid\r\n    LEFT JOIN accommodationdata ad ON ad.controlid=era.controlid AND ad.personid=er.personid AND ad.courseid=0\r\n    LEFT JOIN accommodations acc ON acc.controlid=era.controlid\r\n    LEFT JOIN vInstructorPrimaryList v ON v.lucourseid=er.lucourseid\r\n    LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=v.instructorid\r\nWHERE er.dateentered>=@startdate AND er.dateentered<@enddate\r\nORDER BY er.examrequestid", parameters))
			{
				examRequestsFromReader = this.GetExamRequestsFromReader(dataReader);
			}
			return examRequestsFromReader;
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0006382C File Offset: 0x00061A2C
		public int CreateExamRequest(int PersonId, int LuCourseId)
		{
			bool flag = PersonId < 1 || LuCourseId < 1;
			int result;
			if (flag)
			{
				result = 0;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				DbParameter[] array = new DbParameter[]
				{
					databaseLayer.GetOutputParameter("@examrequestid", DbType.Int32, 0),
					databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
					databaseLayer.GetParameter("@lucid", DbType.Int32, LuCourseId)
				};
				databaseLayer.ExecuteNonQuery("DECLARE @name varchar(8000), @email varchar(8000)\r\nSET @name=(SELECT TOP 1 instructorname FROM vInstructorPrimaryList WHERE lucourseid=@lucid)\r\nSET @email=(SELECT TOP 1 instructoremail FROM vInstructorPrimaryList WHERE lucourseid=@lucid)\r\nINSERT INTO ExamRequest (personid,lucourseid,instructorfirstname,instructoremail) \r\nVALUES (@pid,@lucid,COALESCE(@name,''),COALESCE(@email,''));\r\n\r\nSET @examrequestid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))", array);
				int num = (array[0].Value != null && array[0].Value != DBNull.Value) ? ((int)array[0].Value) : 0;
				bool flag2 = num < 1;
				if (flag2)
				{
					throw new DatabaseInsertFailedException("Failed to insert exam request");
				}
				array = new DbParameter[]
				{
					databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
					databaseLayer.GetParameter("@lucid", DbType.Int32, LuCourseId),
					databaseLayer.GetParameter("@id", DbType.Int32, num)
				};
				databaseLayer.ExecuteNonQuery("INSERT INTO ExamRequestAccommodations (ExamRequestId,controlid) \r\n\tSELECT DISTINCT @id,controlid FROM accommodationdata WHERE personid=@pid AND courseid=dbo.AccommodationsCourseOrTemplate(@pid,@lucid) AND controlid IN (SELECT controlid FROM dynamicscreencontrols) AND controlid IN (SELECT controlid FROM accommodations WHERE (showonletter & 2)>0)", array);
				result = num;
			}
			return result;
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00063958 File Offset: 0x00061B58
		public void DeleteExamRequest(int ExamRequestId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, ExamRequestId)
			};
			databaseLayer.ExecuteNonQuery("DELETE FROM ExamRequestAccommodations WHERE ExamRequestId=@id;\r\nDELETE FROM ExamRequest WHERE ExamRequestId=@id", parameters);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x000639AC File Offset: 0x00061BAC
		public IList<ExamRequest> LoadRequestsByCourse(int LuCourseId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			IList<ExamRequest> examRequestsFromReader;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT er.ExamRequestId,er.personid,er.lucourseid,er.startdate AS erstartdate,er.enddate AS erenddate,er.dateentered,er.instructorfirstname,er.instructorlastname,er.instructoremail,er.instructorphone,\r\n    era.ExamRequestAccommodationId,era.controlid,\r\n    e.examid,e.dateentered AS examdateentered,e.whoentered AS examwhoentered,e.description,e.dateoftest,e.visible,e.usercomment,e.testduration,e.lastmodified,e.wholastmodified,e.typecode,e.extendedproperties,e.testpickedupdate,e.testpickedupnote,\r\n    e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,e.filename\r\n    ,luc.startdate,luc.enddate\r\n    ,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.timeofday,luc.section,v.instructorid,\r\n    lucd2.altlookupstring AS instructorname,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,\r\n    lucd2.username AS instructorusername,lucd2.externalid AS instructorexternalid,lucd2.id AS instructoremployeeid,\r\n    p.firstname,p.lastname,p.middlename,p.student_no\r\n    ,dc.controlcaption,dc.controlcode,dc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.defaultvalue\r\n    ,ad.valtext,ad.valint,ad.valbytes,ad.valdate,ad.valimage,ad.valbytesisencrypted,ad.dataid,ad.courseid\r\n    ,acc.longdescription\r\nFROM examrequest er LEFT JOIN examrequestaccommodations era ON era.examrequestid=er.examrequestid\r\n    LEFT JOIN exams e ON e.lucourseid=er.lucourseid AND e.typecode='F'\r\n    LEFT JOIN lucourses luc ON luc.lucourseid=er.lucourseid\r\n    LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n    LEFT JOIN people p ON p.personid=er.personid\r\n    LEFT JOIN dynamiccontrols dc ON dc.controlid=era.controlid\r\n    LEFT JOIN accommodationdata ad ON ad.controlid=era.controlid AND ad.personid=er.personid AND ad.courseid=0\r\n    LEFT JOIN accommodations acc ON acc.controlid=era.controlid\r\n    LEFT JOIN vInstructorPrimaryList v ON v.lucourseid=er.lucourseid\r\n    LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=v.instructorid\r\nWHERE er.lucourseid=@lucid\r\nORDER BY er.examrequestid", parameters))
			{
				examRequestsFromReader = this.GetExamRequestsFromReader(dataReader);
			}
			return examRequestsFromReader;
		}
	}
}
