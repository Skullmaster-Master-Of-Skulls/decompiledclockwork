using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.DAO.StudentAccommodationRequests;
using TechnoPro.Common.DataStructure;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.Common.DAO.Impl.StudentAccommodationRequests
{
	// Token: 0x02000045 RID: 69
	public class StudentAccommodationRequestDAO : IStudentAccommodationRequestDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00010377 File Offset: 0x0000E577
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0001037F File Offset: 0x0000E57F
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x060001CE RID: 462 RVA: 0x00010388 File Offset: 0x0000E588
		public StudentAccommodationRequestDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001CF RID: 463 RVA: 0x000103B9 File Offset: 0x0000E5B9
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x000103C1 File Offset: 0x0000E5C1
		public OperationContext OpContext { get; set; }

		// Token: 0x060001D1 RID: 465 RVA: 0x000103CC File Offset: 0x0000E5CC
		[DebuggerStepThrough]
		private static Task CreateNewArchiveEntry(StudentCourseAccommodationRequest req, eStudentCourseAccommodationRequestHistoryItemHowModified howModified, int whoModified)
		{
			StudentAccommodationRequestDAO.<CreateNewArchiveEntry>d__9 <CreateNewArchiveEntry>d__ = new StudentAccommodationRequestDAO.<CreateNewArchiveEntry>d__9();
			<CreateNewArchiveEntry>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<CreateNewArchiveEntry>d__.req = req;
			<CreateNewArchiveEntry>d__.howModified = howModified;
			<CreateNewArchiveEntry>d__.whoModified = whoModified;
			<CreateNewArchiveEntry>d__.<>1__state = -1;
			<CreateNewArchiveEntry>d__.<>t__builder.Start<StudentAccommodationRequestDAO.<CreateNewArchiveEntry>d__9>(ref <CreateNewArchiveEntry>d__);
			return <CreateNewArchiveEntry>d__.<>t__builder.Task;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00010420 File Offset: 0x0000E620
		private StudentCourseAccommodationRequestHistoryItem GetStudentCourseAccommodationRequestHistoryItemFromRecord(IDataReader record, IBatchDecryptor batchDecryptor = null)
		{
			bool flag = batchDecryptor == null;
			if (flag)
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				batchDecryptor = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption.GetBatchDecryptor();
			}
			string text = record["HowModified"].ToString().ToLower().Trim();
			int num = (int)((text.Length > 0) ? text[0] : '?');
			int num2 = (record["status"] is DBNull) ? 0 : ((int)record["status"]);
			int studentCourseAccommodationRequestId = (record["StudentCourseAccommodationRequestId"] is DBNull) ? 0 : ((int)record["StudentCourseAccommodationRequestId"]);
			int personId = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
			int luCourseId = (record["lucourseid"] is DBNull) ? 0 : ((int)record["lucourseid"]);
			LookupCourseBase courseBaseFromReader = LookupCourseDAO.GetCourseBaseFromReader("", record);
			eStudentCourseAccommodationRequestHistoryItemHowModified howModified = (eStudentCourseAccommodationRequestHistoryItemHowModified)(Enum.IsDefined(typeof(eStudentCourseAccommodationRequestHistoryItemHowModified), num) ? num : 63);
			DateTime dateModified = (record["Datemodified"] is DBNull) ? DateTime.MinValue : ((DateTime)record["Datemodified"]);
			PersonBase personFromReader = PeopleDAO.GetPersonFromReader("", record, this.OpContext, batchDecryptor);
			eStudentCourseAccommodationRequestStatus status = (eStudentCourseAccommodationRequestStatus)(Enum.IsDefined(typeof(eStudentCourseAccommodationRequestStatus), num2) ? num2 : 0);
			DateTime dateRequested = (record["daterequested"] is DBNull) ? DateTime.MinValue : ((DateTime)record["daterequested"]);
			bool accommodationChangesRequested = record["AccommodationChangesRequested"] != DBNull.Value && (bool)record["AccommodationChangesRequested"];
			bool additionalAccommodationsRequested = record["AdditionalAccommodationsRequested"] != DBNull.Value && (bool)record["AdditionalAccommodationsRequested"];
			string note = (record["Note1"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["Note1"]);
			string note2 = (record["Note2"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["Note2"]);
			return new StudentCourseAccommodationRequestHistoryItem
			{
				StudentCourseAccommodationRequestId = studentCourseAccommodationRequestId,
				PersonId = personId,
				LuCourseId = luCourseId,
				Course = courseBaseFromReader,
				HowModified = howModified,
				DateModified = dateModified,
				WhoModified = personFromReader,
				Status = status,
				DateRequested = dateRequested,
				AccommodationChangesRequested = accommodationChangesRequested,
				AdditionalAccommodationsRequested = additionalAccommodationsRequested,
				Note1 = note,
				Note2 = note2
			};
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00010707 File Offset: 0x0000E907
		private static void LogMissingArchiveDatabase()
		{
			CWLogger.Logger.Warn("StudentAccommodationRequestDAO:ArchiveDatabaseIsMissing");
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0001071C File Offset: 0x0000E91C
		private static bool ReaderContainsColumn(IDataReader reader, string colName)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				bool flag = reader.GetName(i).Equals(colName, StringComparison.OrdinalIgnoreCase);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0001075C File Offset: 0x0000E95C
		private StudentCourseAccommodationModificationRequestItem GetStudentCourseAccommodationRequestItemFromRecord(IDataReader record)
		{
			int num = (record["StudentCourseAccommodationModificationRequestItemId"] == DBNull.Value) ? 0 : ((int)record["StudentCourseAccommodationModificationRequestItemId"]);
			bool flag = num < 1;
			StudentCourseAccommodationModificationRequestItem result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num2 = (record["modificationtype"] == DBNull.Value) ? 0 : ((int)record["modificationtype"]);
				int num3 = (record["saristatus"] == DBNull.Value) ? 0 : ((int)record["saristatus"]);
				eStudentCourseAccommodationModificationType modificationType = (eStudentCourseAccommodationModificationType)(Enum.IsDefined(typeof(eStudentCourseAccommodationModificationType), num2) ? num2 : 0);
				eStudentCourseAccommodationRequestStatus status = (eStudentCourseAccommodationRequestStatus)(Enum.IsDefined(typeof(eStudentCourseAccommodationRequestStatus), num3) ? num3 : 0);
				result = new StudentCourseAccommodationModificationRequestItem
				{
					StudentCourseAccommodationModificationRequestItemId = num,
					DateEntered = (DateTime)record["saridateentered"],
					WhoEntered = PeopleDAO.GetPersonFromReader("sariwhoentered", record, this.OpContext, null),
					Note1 = ((record["note1"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["sarinote1"])),
					Note2 = ((record["note2"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["sarinote2"])),
					RequestedAccommodationData = new DynamicData
					{
						Field = new DynamicField
						{
							ControlId = ((record["controlid"] == DBNull.Value) ? 0 : ((int)record["controlid"]))
						}
					},
					ModificationType = modificationType,
					Status = status
				};
			}
			return result;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00010940 File Offset: 0x0000EB40
		private List<StudentCourseAccommodationRequest> GetRequestsFromReader(IDataReader reader)
		{
			List<StudentCourseAccommodationRequest> list = new List<StudentCourseAccommodationRequest>();
			StudentCourseAccommodationRequest studentCourseAccommodationRequest = null;
			while (reader.Read())
			{
				int num = (int)reader["StudentCourseAccommodationRequestId"];
				bool flag = studentCourseAccommodationRequest == null || studentCourseAccommodationRequest.StudentCourseAccommodationRequestId != num;
				if (flag)
				{
					studentCourseAccommodationRequest = this.GetStudentCourseAccommodationRequestFromRecord(reader);
					bool flag2 = studentCourseAccommodationRequest != null;
					if (flag2)
					{
						list.Add(studentCourseAccommodationRequest);
					}
				}
				bool flag3 = studentCourseAccommodationRequest == null;
				if (!flag3)
				{
					StudentCourseAccommodationModificationRequestItem requestItem = this.GetStudentCourseAccommodationRequestItemFromRecord(reader);
					bool flag4 = requestItem != null && studentCourseAccommodationRequest.AccommodationModificationRequests.FirstOrDefault((StudentCourseAccommodationModificationRequestItem f) => f.StudentCourseAccommodationModificationRequestItemId == requestItem.StudentCourseAccommodationModificationRequestItemId) == null;
					if (flag4)
					{
						studentCourseAccommodationRequest.AccommodationModificationRequests.Add(requestItem);
					}
				}
			}
			return list;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00010A14 File Offset: 0x0000EC14
		private StudentCourseAccommodationRequest GetStudentCourseAccommodationRequestFromRecord(IDataReader record)
		{
			bool flag = record == null;
			StudentCourseAccommodationRequest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (record["status"] == DBNull.Value) ? 0 : ((int)record["status"]);
				eStudentCourseAccommodationRequestStatus status = (eStudentCourseAccommodationRequestStatus)(Enum.IsDefined(typeof(eStudentCourseAccommodationRequestStatus), num) ? num : 0);
				StudentCourseAccommodationRequest studentCourseAccommodationRequest = new StudentCourseAccommodationRequest
				{
					StudentCourseAccommodationRequestId = (int)record["StudentCourseAccommodationRequestId"],
					LuCourseId = (int)record["lucourseid"],
					Student = PeopleDAO.GetPersonFromReader("", record, this.OpContext, null),
					Status = status,
					DateEntered = ((record["dateentered"] != DBNull.Value) ? ((DateTime)record["dateentered"]) : DateTime.MinValue),
					DateRequested = ((record["daterequested"] != DBNull.Value) ? new DateTime?((DateTime)record["daterequested"]) : null),
					AccommodationChangesRequested = Convert.ToBoolean(record["accommodationchangesrequested"]),
					AdditionalAccommodationsRequested = Convert.ToBoolean(record["additionalaccommodationsrequested"]),
					WhoEntered = PeopleDAO.GetPersonFromReader("whoentered", record, this.OpContext, null),
					Note1 = ((record["note1"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["note1"])),
					Note2 = ((record["note2"] == DBNull.Value) ? "" : this.DatabaseManager.Encryption.Decrypt((byte[])record["note2"])),
					AccommodationModificationRequests = new List<StudentCourseAccommodationModificationRequestItem>(),
					AssignedAdvisor = (StudentAccommodationRequestDAO.ReaderContainsColumn(record, "advisorpersonid") ? PeopleDAO.GetPersonFromReader("advisor", record, this.OpContext, null) : null),
					DateApproved = ((record["dateapproved"] is DBNull) ? null : new DateTime?((DateTime)record["dateapproved"]))
				};
				bool flag2 = studentCourseAccommodationRequest.LuCourseId > 0;
				if (flag2)
				{
					studentCourseAccommodationRequest.CourseBase = LookupCourseDAO.GetCourseBaseWithPrimaryInstructorFromReader("", record);
				}
				result = studentCourseAccommodationRequest;
			}
			return result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00010CA4 File Offset: 0x0000EEA4
		public int AddRequest(int StudentPersonId, StudentCourseAccommodationRequest CourseAccommodationRequest, out bool wasInserted)
		{
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@id", DbType.Int32, 0),
				this.DatabaseManager.GetOutputParameter("@wasinserted", DbType.Boolean, 0),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, (CourseAccommodationRequest.Student == null) ? 0 : CourseAccommodationRequest.Student.PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, (CourseAccommodationRequest.LuCourseId > 0 || CourseAccommodationRequest.CourseBase == null) ? CourseAccommodationRequest.LuCourseId : CourseAccommodationRequest.CourseBase.LuCourseId),
				this.DatabaseManager.GetParameter("@status", DbType.Int32, (int)CourseAccommodationRequest.Status),
				this.DatabaseManager.GetParameter("@AccommodationChangesRequested", DbType.Boolean, CourseAccommodationRequest.AccommodationChangesRequested),
				this.DatabaseManager.GetParameter("@AdditionalAccommodationsRequested", DbType.Boolean, CourseAccommodationRequest.AdditionalAccommodationsRequested),
				this.DatabaseManager.GetParameter("@whoenteredpersonid", DbType.Int32, this.OpContext.WhoAmI),
				(!string.IsNullOrEmpty(CourseAccommodationRequest.Note1)) ? this.DatabaseManager.GetParameter("@note1", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(CourseAccommodationRequest.Note1)) : this.DatabaseManager.GetParameter("@note1", DbType.Binary, DBNull.Value),
				(!string.IsNullOrEmpty(CourseAccommodationRequest.Note2)) ? this.DatabaseManager.GetParameter("@note2", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(CourseAccommodationRequest.Note2)) : this.DatabaseManager.GetParameter("@note2", DbType.Binary, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("DECLARE @existingid int\r\nSET @existingid=(SELECT TOP 1 StudentCourseAccommodationRequestId FROM StudentCourseAccommodationRequest WHERE personid=@pid AND lucourseid=@lucid)\r\n\r\nIF NOT @existingid IS NULL AND @existingid > 0\r\nBEGIN\r\n    SET @id=@existingid\r\n\r\n    UPDATE StudentCourseAccommodationRequest \r\n\tSET status=@status,note1=@note1,note2=@note2,accommodationchangesrequested=@AccommodationChangesRequested,additionalaccommodationsrequested=@AdditionalAccommodationsRequested,\r\n\tDateApproved=CASE WHEN NOT @status=8 THEN NULL WHEN DateApproved IS NULL THEN getdate() ELSE DateApproved END\r\n\tWHERE StudentCourseAccommodationRequestId=@id\r\n    SET @wasinserted=0\r\nEND\r\nELSE\r\nBEGIN\r\n    INSERT INTO StudentCourseAccommodationRequest (personid,lucourseid,status,accommodationchangesrequested,additionalaccommodationsrequested,whoenteredpersonid,note1,note2,dateapproved)\r\n        VALUES (@pid,@lucid,@status,@AccommodationChangesRequested,@AdditionalAccommodationsRequested,@whoenteredpersonid,@note1,@note2,CASE WHEN @status=8 THEN getdate() ELSE NULL END);\r\n    SET @id = (SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS StudentCourseAccommodationRequestId)\r\n    SET @wasinserted=1\r\nEND", array);
			int num = (array[0].Value is DBNull) ? 0 : ((int)array[0].Value);
			wasInserted = (array[1].Value != DBNull.Value && (bool)array[1].Value);
			bool flag = num < 1;
			if (flag)
			{
				throw new Exception("Unable to create student accommodation request.");
			}
			CourseAccommodationRequest.StudentCourseAccommodationRequestId = num;
			return num;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		public void UpdateRequestStatus(int StudentAccommodationRequestId, eStudentCourseAccommodationRequestStatus NewStatus)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, StudentAccommodationRequestId),
				this.DatabaseManager.GetParameter("@status", DbType.Int32, (int)NewStatus)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE    StudentCourseAccommodationRequest \r\nSET         status=@status,\r\n            DateApproved=CASE WHEN NOT @status=8 THEN NULL WHEN DateApproved IS NULL THEN getdate() ELSE DateApproved END\r\nWHERE       StudentCourseAccommodationRequestId=@id", parameters);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00010F48 File Offset: 0x0000F148
		public void UpdateRequest(StudentCourseAccommodationRequest CourseAccommodationRequest)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, CourseAccommodationRequest.StudentCourseAccommodationRequestId),
				this.DatabaseManager.GetParameter("@status", DbType.Int32, (int)CourseAccommodationRequest.Status),
				(!string.IsNullOrEmpty(CourseAccommodationRequest.Note1)) ? this.DatabaseManager.GetParameter("@note1", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(CourseAccommodationRequest.Note1)) : this.DatabaseManager.GetParameter("@note1", DbType.Binary, DBNull.Value),
				(!string.IsNullOrEmpty(CourseAccommodationRequest.Note2)) ? this.DatabaseManager.GetParameter("@note2", DbType.Binary, this.DatabaseManager.Encryption.Encrypt(CourseAccommodationRequest.Note2)) : this.DatabaseManager.GetParameter("@note2", DbType.Binary, DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE StudentCourseAccommodationRequest \r\nSET     status=@status,note1=@note1,note2=@note2,\r\n        DateApproved=CASE WHEN NOT @status=8 THEN NULL WHEN DateApproved IS NULL THEN getdate() ELSE DateApproved END\r\nWHERE   StudentCourseAccommodationRequestId=@id", parameters);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00011050 File Offset: 0x0000F250
		public void DeleteRequest(int StudentCourseAccommodationRequestId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@id", DbType.Int32, StudentCourseAccommodationRequestId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE StudentCourseAccommodationRequest SET isactive=0 WHERE StudentCourseAccommodationRequestId=@id", parameters);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000110A4 File Offset: 0x0000F2A4
		public StudentCourseAccommodationRequest LoadRequestById(int StudentCourseAccommodationRequestId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@id", DbType.Int32, StudentCourseAccommodationRequestId)
			};
			StudentCourseAccommodationRequest result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sar.StudentCourseAccommodationRequestId,sar.personid,p.firstname,p.lastname,p.middlename,p.student_no,\r\n            sar.lucourseid,sar.status,sar.daterequested,\r\n            sar.accommodationchangesrequested,sar.additionalaccommodationsrequested,\r\n            sar.whoapprovedpersonid,sar.dateapproved,pa.firstname AS whoapprovedfirstname,pa.lastname AS whoapprovedlastname,pa.student_no AS whoapprovedstudent_no,\r\n            sar.whoenteredpersonid,sar.dateentered,pe.firstname AS whoenteredfirstname,pe.lastname AS whoenteredlastname,pe.student_no AS whoenteredstudent_no,\r\n            sar.note1,sar.note2,sar.isactive,\r\n            sari.StudentCourseAccommodationModificationRequestItemId,sari.controlid,\r\n            sari.modificationtype,sari.note1 AS sarinote1,sari.note2 AS sarinote2,\r\n            sari.status AS saristatus,sari.dateentered AS saridateentered,\r\n            sari.whoenteredpersonid AS sariwhoenteredpersonid,p2.firstname AS sariwhoenteredfirstname,p2.lastname AS sariwhoenteredlastname,p2.student_no AS sariwhoenteredstudent_no,\r\n            luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.section,luc.timeofday,\r\n            luc.campus,luc.department,luc.location,lucd.lookupstring AS subjectcode,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            c.assignedcounsellorlast AS advisorlastname,c.assignedcounsellorfirst AS advisorstudent_no,\r\n            vp.instructorid,vp.instructorname,vp.instructoremail,vp.instructorphone,vp.instructorusername,vp.instructorexternalid,vp.instructoremployeeid\r\nFROM    StudentCourseAccommodationRequest sar LEFT JOIN StudentCourseAccommodationModificationRequestItem sari ON sari.StudentCourseAccommodationRequestId=sar.StudentCourseAccommodationRequestId\r\n        LEFT JOIN people p ON p.personid=sar.personid\r\n        LEFT JOIN people pa ON pa.personid=sar.whoapprovedpersonid\r\n        LEFT JOIN people pe ON pe.personid=sar.whoenteredpersonid\r\n        LEFT JOIN people p2 ON p2.personid=sari.whoenteredpersonid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=sar.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN common c ON c.personid=sar.personid\r\n        LEFT JOIN vinstructorprimarylist vp ON vp.lucourseid=sar.lucourseid\r\nWHERE   sar.StudentCourseAccommodationRequestId=@id", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<StudentCourseAccommodationRequest> requestsFromReader = this.GetRequestsFromReader(dataReader);
					result = ((requestsFromReader != null && requestsFromReader.Count > 0) ? requestsFromReader[0] : null);
				}
			}
			return result;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00011134 File Offset: 0x0000F334
		public IList<StudentCourseAccommodationRequest> LoadRequestsByStudentAndDate(int StudentPersonId, DateTime StartDate, DateTime EndDate)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPersonId),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate)
			};
			IList<StudentCourseAccommodationRequest> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sar.StudentCourseAccommodationRequestId,sar.personid,p.firstname,p.lastname,p.middlename,p.student_no,\r\n            sar.lucourseid,sar.status,sar.daterequested,\r\n            sar.accommodationchangesrequested,sar.additionalaccommodationsrequested,\r\n            sar.whoapprovedpersonid,sar.dateapproved,pa.firstname AS whoapprovedfirstname,pa.lastname AS whoapprovedlastname,pa.student_no AS whoapprovedstudent_no,\r\n            sar.whoenteredpersonid,sar.dateentered,pe.firstname AS whoenteredfirstname,pe.lastname AS whoenteredlastname,pe.student_no AS whoenteredstudent_no,\r\n            sar.note1,sar.note2,sar.isactive,\r\n            sari.StudentCourseAccommodationModificationRequestItemId,sari.controlid,\r\n            sari.modificationtype,sari.note1 AS sarinote1,sari.note2 AS sarinote2,\r\n            sari.status AS saristatus,sari.dateentered AS saridateentered,\r\n            sari.whoenteredpersonid AS sariwhoenteredpersonid,p2.firstname AS sariwhoenteredfirstname,p2.lastname AS sariwhoenteredlastname,p2.student_no AS sariwhoenteredstudent_no,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.section,luc.timeofday,\r\n            luc.campus,luc.department,luc.location,lucd.lookupstring AS subjectcode,\r\n            c.assignedcounsellorlast AS advisorlastname,c.assignedcounsellorfirst AS advisorstudent_no,\r\n            vp.instructorid,vp.instructorname,vp.instructoremail,vp.instructorphone,vp.instructorusername,vp.instructorexternalid,vp.instructoremployeeid\r\nFROM    StudentCourseAccommodationRequest sar LEFT JOIN StudentCourseAccommodationModificationRequestItem sari ON sari.StudentCourseAccommodationRequestId=sar.StudentCourseAccommodationRequestId\r\n        LEFT JOIN people p ON p.personid=sar.personid\r\n        LEFT JOIN people pa ON pa.personid=sar.whoapprovedpersonid\r\n        LEFT JOIN people pe ON pe.personid=sar.whoenteredpersonid\r\n        LEFT JOIN people p2 ON p2.personid=sari.whoenteredpersonid\r\n        LEFT JOIN common c ON c.personid=sar.personid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=sar.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN vinstructorprimarylist vp ON vp.lucourseid=sar.lucourseid\r\nWHERE   sar.personid=@pid AND sar.lucourseid IN\r\n        (SELECT lucourseid FROM lucourses WHERE NOT ( enddate <= @startdate OR startdate > @enddate))\r\n        AND sar.isactive=1\r\nORDER BY sar.StudentCourseAccommodationRequestId,sar.personid,sar.lucourseid", parameters))
			{
				result = ((dataReader == null) ? null : this.GetRequestsFromReader(dataReader));
			}
			return result;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000111D8 File Offset: 0x0000F3D8
		public IList<StudentCourseAccommodationRequest> LoadCourseRegistrationsWithRequestByStatus(Range<DateTime> RestrictToCourseDates, eStudentCourseAccommodationRequestStatus statuses)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@statuses", DbType.Int32, (int)statuses),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, (RestrictToCourseDates == null) ? DBNull.Value : RestrictToCourseDates.Start),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, (RestrictToCourseDates == null) ? DBNull.Value : RestrictToCourseDates.End)
			};
			IList<StudentCourseAccommodationRequest> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(QueryStorageStudentAccommodationRequest.QS_REQUESTS_BY_STATUS, parameters))
			{
				result = ((dataReader == null) ? null : this.GetRequestsFromReader(dataReader));
			}
			return result;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0001129C File Offset: 0x0000F49C
		public IList<StudentCourseAccommodationRequest> LoadCourseRegistrationsWithRequestByStatusWithCourseDatesInFuture(DateTime minCourseDate, eStudentCourseAccommodationRequestStatus statuses)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@statuses", DbType.Int32, (int)statuses),
				this.DatabaseManager.GetParameter("@mindate", DbType.DateTime, minCourseDate)
			};
			IList<StudentCourseAccommodationRequest> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader(QueryStorageStudentAccommodationRequest.QS_REQUESTS_BY_STATUS_FUTURE, parameters))
			{
				result = ((dataReader == null) ? null : this.GetRequestsFromReader(dataReader));
			}
			return result;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00011328 File Offset: 0x0000F528
		[DebuggerStepThrough]
		public Task AddArchiveEntryForUpdateAsync(StudentCourseAccommodationRequest updatedRequest, int whoAmIPid)
		{
			StudentAccommodationRequestDAO.<AddArchiveEntryForUpdateAsync>d__24 <AddArchiveEntryForUpdateAsync>d__ = new StudentAccommodationRequestDAO.<AddArchiveEntryForUpdateAsync>d__24();
			<AddArchiveEntryForUpdateAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddArchiveEntryForUpdateAsync>d__.<>4__this = this;
			<AddArchiveEntryForUpdateAsync>d__.updatedRequest = updatedRequest;
			<AddArchiveEntryForUpdateAsync>d__.whoAmIPid = whoAmIPid;
			<AddArchiveEntryForUpdateAsync>d__.<>1__state = -1;
			<AddArchiveEntryForUpdateAsync>d__.<>t__builder.Start<StudentAccommodationRequestDAO.<AddArchiveEntryForUpdateAsync>d__24>(ref <AddArchiveEntryForUpdateAsync>d__);
			return <AddArchiveEntryForUpdateAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001137C File Offset: 0x0000F57C
		[DebuggerStepThrough]
		public Task AddArchiveEntryForNewEntry(StudentCourseAccommodationRequest newRequest, int whoAmIPid)
		{
			StudentAccommodationRequestDAO.<AddArchiveEntryForNewEntry>d__25 <AddArchiveEntryForNewEntry>d__ = new StudentAccommodationRequestDAO.<AddArchiveEntryForNewEntry>d__25();
			<AddArchiveEntryForNewEntry>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddArchiveEntryForNewEntry>d__.<>4__this = this;
			<AddArchiveEntryForNewEntry>d__.newRequest = newRequest;
			<AddArchiveEntryForNewEntry>d__.whoAmIPid = whoAmIPid;
			<AddArchiveEntryForNewEntry>d__.<>1__state = -1;
			<AddArchiveEntryForNewEntry>d__.<>t__builder.Start<StudentAccommodationRequestDAO.<AddArchiveEntryForNewEntry>d__25>(ref <AddArchiveEntryForNewEntry>d__);
			return <AddArchiveEntryForNewEntry>d__.<>t__builder.Task;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000113D0 File Offset: 0x0000F5D0
		[DebuggerStepThrough]
		public Task AddArchiveEntryForDeletedEntry(StudentCourseAccommodationRequest deletedRequest, int whoAmIPid)
		{
			StudentAccommodationRequestDAO.<AddArchiveEntryForDeletedEntry>d__26 <AddArchiveEntryForDeletedEntry>d__ = new StudentAccommodationRequestDAO.<AddArchiveEntryForDeletedEntry>d__26();
			<AddArchiveEntryForDeletedEntry>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<AddArchiveEntryForDeletedEntry>d__.<>4__this = this;
			<AddArchiveEntryForDeletedEntry>d__.deletedRequest = deletedRequest;
			<AddArchiveEntryForDeletedEntry>d__.whoAmIPid = whoAmIPid;
			<AddArchiveEntryForDeletedEntry>d__.<>1__state = -1;
			<AddArchiveEntryForDeletedEntry>d__.<>t__builder.Start<StudentAccommodationRequestDAO.<AddArchiveEntryForDeletedEntry>d__26>(ref <AddArchiveEntryForDeletedEntry>d__);
			return <AddArchiveEntryForDeletedEntry>d__.<>t__builder.Task;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00011424 File Offset: 0x0000F624
		public StudentCourseAccommodationRequestHistory LoadStudentCourseAccommodationRequestHistory(int PersonId, int LuCourseId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
				databaseLayer.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			StudentCourseAccommodationRequestHistory result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_SelfReg_AccommodationRequestHistory", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					StudentCourseAccommodationRequestHistory studentCourseAccommodationRequestHistory = new StudentCourseAccommodationRequestHistory
					{
						HistoryItems = new List<StudentCourseAccommodationRequestHistoryItem>()
					};
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						StudentCourseAccommodationRequestHistoryItem studentCourseAccommodationRequestHistoryItemFromRecord = this.GetStudentCourseAccommodationRequestHistoryItemFromRecord(dataReader, batchDecryptor);
						bool flag2 = studentCourseAccommodationRequestHistoryItemFromRecord != null;
						if (flag2)
						{
							studentCourseAccommodationRequestHistory.HistoryItems.Add(studentCourseAccommodationRequestHistoryItemFromRecord);
						}
					}
					result = studentCourseAccommodationRequestHistory;
				}
			}
			return result;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00011510 File Offset: 0x0000F710
		public StudentCourseAccommodationRequest LoadRequestByStudentAndCourse(int StudentPersonId, int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, StudentPersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			StudentCourseAccommodationRequest result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    sar.StudentCourseAccommodationRequestId,sar.personid,p.firstname,p.lastname,p.middlename,p.student_no,\r\n            sar.lucourseid,sar.status,sar.daterequested,\r\n            sar.accommodationchangesrequested,sar.additionalaccommodationsrequested,\r\n            sar.whoapprovedpersonid,sar.dateapproved,pa.firstname AS whoapprovedfirstname,pa.lastname AS whoapprovedlastname,pa.student_no AS whoapprovedstudent_no,\r\n            sar.whoenteredpersonid,sar.dateentered,pe.firstname AS whoenteredfirstname,pe.lastname AS whoenteredlastname,pe.student_no AS whoenteredstudent_no,\r\n            sar.note1,sar.note2,sar.isactive,\r\n            sari.StudentCourseAccommodationModificationRequestItemId,sari.controlid,\r\n            sari.modificationtype,sari.note1 AS sarinote1,sari.note2 AS sarinote2,\r\n            sari.status AS saristatus,sari.dateentered AS saridateentered,\r\n            sari.whoenteredpersonid AS sariwhoenteredpersonid,p2.firstname AS sariwhoenteredfirstname,p2.lastname AS sariwhoenteredlastname,p2.student_no AS sariwhoenteredstudent_no,\r\n            luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.subjectid,luc.course,luc.section,luc.timeofday,\r\n            luc.campus,luc.department,luc.location,lucd.lookupstring AS subjectcode,\r\n            c.assignedcounsellorpid AS advisorpersonid,c.assignedcounsellorfirst AS advisorfirstname,\r\n            c.assignedcounsellorlast AS advisorlastname,c.assignedcounsellorfirst AS advisorstudent_no,\r\n            vp.instructorid,vp.instructorname,vp.instructoremail,vp.instructorphone,vp.instructorusername,vp.instructorexternalid,vp.instructoremployeeid\r\nFROM    StudentCourseAccommodationRequest sar LEFT JOIN StudentCourseAccommodationModificationRequestItem sari ON sari.StudentCourseAccommodationRequestId=sar.StudentCourseAccommodationRequestId\r\n        LEFT JOIN people p ON p.personid=sar.personid\r\n        LEFT JOIN people pa ON pa.personid=sar.whoapprovedpersonid\r\n        LEFT JOIN people pe ON pe.personid=sar.whoenteredpersonid\r\n        LEFT JOIN people p2 ON p2.personid=sari.whoenteredpersonid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=sar.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN common c ON c.personid=sar.personid\r\n        LEFT JOIN vinstructorprimarylist vp ON vp.lucourseid=sar.lucourseid\r\nWHERE   sar.personid=@pid AND sar.lucourseid=@lucid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<StudentCourseAccommodationRequest> requestsFromReader = this.GetRequestsFromReader(dataReader);
					result = ((requestsFromReader != null && requestsFromReader.Count > 0) ? requestsFromReader[0] : null);
				}
			}
			return result;
		}
	}
}
