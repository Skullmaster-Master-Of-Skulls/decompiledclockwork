using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x02000147 RID: 327
	public class ClassTestDefinitionDAO : IClassTestDefinitionDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x00061528 File Offset: 0x0005F728
		// (set) Token: 0x06000965 RID: 2405 RVA: 0x00061530 File Offset: 0x0005F730
		public OperationContext OpContext { get; set; }

		// Token: 0x06000966 RID: 2406 RVA: 0x00061539 File Offset: 0x0005F739
		public ClassTestDefinitionDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0006156C File Offset: 0x0005F76C
		internal static T GetClassTestBaseFromRecord<T>(IDataReader record, string coursePrefix = "", IBatchDecryptor batchDecryptor = null) where T : ClassTestBase
		{
			bool flag = record["examid"] == DBNull.Value;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				DateTime startDateTime = (DateTime)record["dateoftest"];
				int num = (int)record["testduration"];
				string s = record["typecode"].ToString().Trim().ToUpper();
				T t = (T)((object)Activator.CreateInstance(typeof(T)));
				t.ExamId = (int)record["examid"];
				t.Course = LookupCourseDAO.GetCourseBaseFromReader(coursePrefix ?? "", record);
				t.StartDateTime = startDateTime;
				t.EndDateTime = startDateTime.AddMinutes((double)num);
				t.ExternalExamId = (PeopleDAO.ReaderContainsColumn(record, "externalexamid") ? ((record["externalexamid"] == DBNull.Value) ? "" : ((string)record["externalexamid"])) : "");
				t.Location = (PeopleDAO.ReaderContainsColumn(record, "location") ? record["location"].ToString() : "");
				t.ExamType = s.GetClassTestTypeFromString();
				result = t;
			}
			return result;
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x000616E4 File Offset: 0x0005F8E4
		internal static ClassTest GetClassTestFromRecord(IDataReader record)
		{
			ClassTest classTestBaseFromRecord = ClassTestDefinitionDAO.GetClassTestBaseFromRecord<ClassTest>(record, "", null);
			bool flag = classTestBaseFromRecord == null;
			ClassTest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ClassTestDefinitionDAO.AddClassTestInfoNonBase<ClassTest>(ref classTestBaseFromRecord, record);
				result = classTestBaseFromRecord;
			}
			return result;
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0006171C File Offset: 0x0005F91C
		internal static void AddClassTestInfoNonBase<T>(ref T classTest, IDataReader record) where T : ClassTest
		{
			classTest.TestPickedUpDate = ((record["testpickedupdate"] is DBNull) ? null : new DateTime?((DateTime)record["testpickedupdate"]));
			classTest.TestPickedUpNote = ((record["testpickedupnote"] is DBNull) ? "" : ((string)record["testpickedupnote"]));
			classTest.TestDeliveredMessage = ((record["usercomment"] is DBNull) ? "" : ((string)record["usercomment"]));
			classTest.PrivateNote = ((record["privatenote"] is DBNull) ? "" : ((string)record["privatenote"]));
			classTest.InstructorContactedDate = ((record["instructorcontacteddate"] is DBNull) ? null : new DateTime?((DateTime)record["instructorcontacteddate"]));
			classTest.InstructorContactedNote = ((record["instructorcontactednote"] is DBNull) ? "" : ((string)record["instructorcontactednote"]));
			bool flag = record["instructoracknowledged"] is DBNull;
			if (flag)
			{
				classTest.InstructorAcknowledged = null;
			}
			else
			{
				string text = record["instructoracknowledged"].ToString().Trim().ToUpper();
				bool flag2 = text.Length == 1;
				if (flag2)
				{
					classTest.InstructorAcknowledged = new char?(text[0]);
				}
				else
				{
					classTest.InstructorAcknowledged = null;
				}
			}
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0006190C File Offset: 0x0005FB0C
		private static ClassTestForDisplay GetClassTestForDisplayWithoutFormDataFromRecord(IDataReader record)
		{
			int num = (record["examid"] is DBNull) ? 0 : ((int)record["examid"]);
			bool flag = num < 1;
			ClassTestForDisplay result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DateTime startDateTime = (DateTime)record["dateoftest"];
				int num2 = (record["testduration"] is DBNull) ? 0 : ((int)record["testduration"]);
				string s = record["typecode"].ToString().Trim().ToUpper();
				result = new ClassTestForDisplay
				{
					ExamId = num,
					CourseWithPrimaryInstructor = LookupCourseDAO.GetCourseBaseWithPrimaryInstructorFromReader("", record),
					StartDateTime = startDateTime,
					EndDateTime = startDateTime.AddMinutes((double)num2),
					ExamType = s.GetClassTestTypeFromString(),
					InstructorContactedDate = ((record["instructorcontacteddate"] is DBNull) ? null : new DateTime?((DateTime)record["instructorcontacteddate"])),
					InstructorContactedNote = ((record["instructorcontactednote"] is DBNull) ? "" : ((string)record["instructorcontactednote"])),
					TestPickedUpDate = ((record["testpickedupdate"] is DBNull) ? null : new DateTime?((DateTime)record["testpickedupdate"])),
					TestPickedUpNote = ((record["testpickedupnote"] is DBNull) ? "" : ((string)record["testpickedupnote"])),
					Location = ((record["location"] is DBNull) ? "" : ((string)record["location"])),
					InstructorFormData = new List<DynamicData>()
				};
			}
			return result;
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00061B10 File Offset: 0x0005FD10
		internal static ClassTestForExamRequest GetClassTestForExamRequestFromRecord(IDataReader record)
		{
			ClassTestForExamRequest classTestBaseFromRecord = ClassTestDefinitionDAO.GetClassTestBaseFromRecord<ClassTestForExamRequest>(record, "", null);
			bool flag = classTestBaseFromRecord == null;
			ClassTestForExamRequest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ClassTestDefinitionDAO.AddClassTestInfoNonBase<ClassTestForExamRequest>(ref classTestBaseFromRecord, record);
				classTestBaseFromRecord.ExamRequestInstructorChoices = record["description"].ToString().Trim();
				result = classTestBaseFromRecord;
			}
			return result;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00061B64 File Offset: 0x0005FD64
		public IList<ClassTestForDisplay> LoadClassTestsForDisplayWithoutInstructorFormData(DateTime StartDate, DateTime EndDate)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.Date, StartDate),
				databaseLayer.GetParameter("@enddate", DbType.Date, EndDate)
			};
			IList<ClassTestForDisplay> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @sd datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @ed datetime = DATEADD(D, 1, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT\tDISTINCT e.examid,e.lucourseid,\r\n        luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altLookupString AS subject,luc.course,luc.[section],\r\n        luc.timeofday,luc.campus,\r\n        luc.instructorid,lucd2.altLookupString AS instructorname,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,\r\n        lucd2.username AS instructorusername,lucd2.externalid AS instructorexternalid,lucd2.id AS instructoremployeeid,\r\n\t\te.dateoftest,e.testduration,e.filename AS [location],\r\n        e.typecode,\r\n        CASE WHEN e.typecode='F' THEN 'Final' ELSE 'Midterm' END AS typecode2,\r\n\t\te.instructorcontacteddate,e.instructorcontactednote,e.testpickedupdate,e.testpickedupnote\r\nFROM\texams e LEFT JOIN LUCourses luc ON luc.LUCourseID=e.lucourseid\r\n\t\tLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.subjectid\r\n        LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE\te.dateoftest>=@sd AND e.dateoftest<@ed \r\n\t\tAND (e.visible IS NULL OR e.visible=1)\r\nORDER BY e.dateoftest", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<ClassTestForDisplay> list = new List<ClassTestForDisplay>();
					while (dataReader.Read())
					{
						list.Add(ClassTestDefinitionDAO.GetClassTestForDisplayWithoutFormDataFromRecord(dataReader));
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00061C18 File Offset: 0x0005FE18
		public void RemoveInstructorHasSubmittedInformationAboutThisTestMarker(int examId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, examId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE exams SET lastmodified=null,wholastmodified=null WHERE examid=@examid", parameters);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00061C6C File Offset: 0x0005FE6C
		public int CreateClassTestDefinition(ClassTest ClassTestDefinition)
		{
			bool flag = ClassTestDefinition.EndDateTime.Date != ClassTestDefinition.StartDateTime.Date;
			if (flag)
			{
				ClassTestDefinition.EndDateTime = new DateTime(ClassTestDefinition.StartDateTime.Year, ClassTestDefinition.StartDateTime.Month, ClassTestDefinition.StartDateTime.Day, ClassTestDefinition.EndDateTime.Hour, ClassTestDefinition.EndDateTime.Minute, 0);
			}
			int num = Convert.ToInt32((ClassTestDefinition.EndDateTime - ClassTestDefinition.StartDateTime).TotalMinutes);
			bool flag2 = num <= 0;
			if (flag2)
			{
				ClassTestDefinition.EndDateTime = ClassTestDefinition.EndDateTime.AddMinutes(10.0);
			}
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@examid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, ClassTestDefinition.Course.LuCourseId),
				this.DatabaseManager.GetParameter("@typecode", DbType.String, ClassTestDefinition.ExamType.GetStringFromClassTestType()),
				this.DatabaseManager.GetParameter("@dateoftest", DbType.DateTime, ClassTestDefinition.StartDateTime),
				this.DatabaseManager.GetParameter("@testduration", DbType.Int32, num),
				this.DatabaseManager.GetParameter("@location", DbType.String, ClassTestDefinition.Location ?? ""),
				this.DatabaseManager.GetParameter("@externalexamid", DbType.String, ClassTestDefinition.ExternalExamId ?? ""),
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI),
				this.DatabaseManager.GetParameter("@testpickedupdate", DbType.DateTime, (ClassTestDefinition.TestPickedUpDate != null) ? ClassTestDefinition.TestPickedUpDate.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@testdelivered", DbType.String, ClassTestDefinition.TestDeliveredMessage ?? ""),
				this.DatabaseManager.GetParameter("@testpickedupnote", DbType.String, ClassTestDefinition.TestPickedUpNote ?? ""),
				this.DatabaseManager.GetParameter("@privatenote", DbType.String, ClassTestDefinition.PrivateNote ?? ""),
				this.DatabaseManager.GetParameter("@instructorcontacteddate", DbType.DateTime, (ClassTestDefinition.InstructorContactedDate != null) ? ClassTestDefinition.InstructorContactedDate.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@instructorcontactednote", DbType.String, ClassTestDefinition.InstructorContactedNote ?? ""),
				this.DatabaseManager.GetParameter("@instructoracknowledged", DbType.String, (ClassTestDefinition.InstructorAcknowledged != null) ? ClassTestDefinition.InstructorAcknowledged.Value.ToString() : " ")
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO exams (whoentered,lucourseid,filename,dateoftest,testduration,typecode,extendedproperties,testpickedupdate,usercomment,testpickedupnote,privatenote,instructorcontacteddate,instructorcontactednote,instructoracknowledged)\r\nVALUES (@whoami,@lucid,@location,@dateoftest,@testduration,@typecode,@externalexamid,@testpickedupdate,@testdelivered,@testpickedupnote,@privatenote,@instructorcontacteddate,@instructorcontactednote,@instructoracknowledged);\r\nSET @examid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))", array);
			ClassTestDefinition.ExamId = ((array[0].Value == null) ? 0 : ((int)array[0].Value));
			return ClassTestDefinition.ExamId;
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00061FE4 File Offset: 0x000601E4
		public int CreateClassTestDefinitionBase(ClassTestBase ClassTestBase)
		{
			bool flag = ClassTestBase.EndDateTime.Date != ClassTestBase.StartDateTime.Date;
			if (flag)
			{
				ClassTestBase.EndDateTime = new DateTime(ClassTestBase.StartDateTime.Year, ClassTestBase.StartDateTime.Month, ClassTestBase.StartDateTime.Day, ClassTestBase.EndDateTime.Hour, ClassTestBase.EndDateTime.Minute, 0);
			}
			int num = Convert.ToInt32((ClassTestBase.EndDateTime - ClassTestBase.StartDateTime).TotalMinutes);
			bool flag2 = num <= 0;
			if (flag2)
			{
				ClassTestBase.EndDateTime = ClassTestBase.EndDateTime.AddMinutes(10.0);
			}
			DbParameter[] array = new DbParameter[]
			{
				this.DatabaseManager.GetOutputParameter("@examid", DbType.Int32, 0),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, ClassTestBase.Course.LuCourseId),
				this.DatabaseManager.GetParameter("@typecode", DbType.String, ClassTestBase.ExamType.GetStringFromClassTestType()),
				this.DatabaseManager.GetParameter("@dateoftest", DbType.DateTime, ClassTestBase.StartDateTime),
				this.DatabaseManager.GetParameter("@testduration", DbType.Int32, num),
				this.DatabaseManager.GetParameter("@location", DbType.String, ClassTestBase.Location ?? ""),
				this.DatabaseManager.GetParameter("@externalexamid", DbType.String, ClassTestBase.ExternalExamId ?? ""),
				this.DatabaseManager.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI)
			};
			this.DatabaseManager.ExecuteNonQuery("INSERT INTO exams (whoentered,lucourseid,filename,dateoftest,testduration,typecode,extendedproperties)\r\nVALUES (@whoami,@lucid,@location,@dateoftest,@testduration,@typecode,@externalexamid);\r\nSET @examid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int))", array);
			ClassTestBase.ExamId = ((array[0].Value == null) ? 0 : ((int)array[0].Value));
			return ClassTestBase.ExamId;
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00062200 File Offset: 0x00060400
		public void UpdateClassTestDefinition(ClassTest ClassTestDefinition)
		{
			bool flag = ClassTestDefinition.EndDateTime.Date != ClassTestDefinition.StartDateTime.Date;
			if (flag)
			{
				ClassTestDefinition.EndDateTime = new DateTime(ClassTestDefinition.StartDateTime.Year, ClassTestDefinition.StartDateTime.Month, ClassTestDefinition.StartDateTime.Day, ClassTestDefinition.EndDateTime.Hour, ClassTestDefinition.EndDateTime.Minute, 0);
			}
			int num = Convert.ToInt32((ClassTestDefinition.EndDateTime - ClassTestDefinition.StartDateTime).TotalMinutes);
			bool flag2 = num <= 0;
			if (flag2)
			{
				ClassTestDefinition.EndDateTime = ClassTestDefinition.EndDateTime.AddMinutes(10.0);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ClassTestDefinition.ExamId),
				this.DatabaseManager.GetParameter("@typecode", DbType.String, ClassTestDefinition.ExamType.GetStringFromClassTestType()),
				this.DatabaseManager.GetParameter("@dateoftest", DbType.DateTime, ClassTestDefinition.StartDateTime),
				this.DatabaseManager.GetParameter("@testduration", DbType.Int32, num),
				this.DatabaseManager.GetParameter("@location", DbType.String, ClassTestDefinition.Location ?? ""),
				this.DatabaseManager.GetParameter("@externalexamid", DbType.String, ClassTestDefinition.ExternalExamId ?? ""),
				this.DatabaseManager.GetParameter("@testpickedupdate", DbType.DateTime, (ClassTestDefinition.TestPickedUpDate != null) ? ClassTestDefinition.TestPickedUpDate.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@testdelivered", DbType.String, ClassTestDefinition.TestDeliveredMessage ?? ""),
				this.DatabaseManager.GetParameter("@testpickedupnote", DbType.String, ClassTestDefinition.TestPickedUpNote ?? ""),
				this.DatabaseManager.GetParameter("@privatenote", DbType.String, ClassTestDefinition.PrivateNote ?? ""),
				this.DatabaseManager.GetParameter("@instructorcontacteddate", DbType.DateTime, (ClassTestDefinition.InstructorContactedDate != null) ? ClassTestDefinition.InstructorContactedDate.Value : DBNull.Value),
				this.DatabaseManager.GetParameter("@instructorcontactednote", DbType.String, ClassTestDefinition.InstructorContactedNote ?? ""),
				this.DatabaseManager.GetParameter("@instructoracknowledged", DbType.String, (ClassTestDefinition.InstructorAcknowledged != null) ? ClassTestDefinition.InstructorAcknowledged.Value.ToString() : " "),
				this.DatabaseManager.GetParameter("@description", DbType.String, ClassTestDefinition.Description)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE exams SET filename=@location,dateoftest=@dateoftest,testduration=@testduration,typecode=@typecode,extendedproperties=@externalexamid,\r\ntestpickedupdate=@testpickedupdate,usercomment=@testdelivered,testpickedupnote=@testpickedupnote,privatenote=@privatenote,instructorcontacteddate=@instructorcontacteddate,instructorcontactednote=@instructorcontactednote,instructoracknowledged=@instructoracknowledged\r\nWHERE examid=@examid", parameters);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00062524 File Offset: 0x00060724
		public void UpdateClassTestDefinitionBase(ClassTestBase ClassTestBase)
		{
			bool flag = ClassTestBase.EndDateTime.Date != ClassTestBase.StartDateTime.Date;
			if (flag)
			{
				ClassTestBase.EndDateTime = new DateTime(ClassTestBase.StartDateTime.Year, ClassTestBase.StartDateTime.Month, ClassTestBase.StartDateTime.Day, ClassTestBase.EndDateTime.Hour, ClassTestBase.EndDateTime.Minute, 0);
			}
			int num = Convert.ToInt32((ClassTestBase.EndDateTime - ClassTestBase.StartDateTime).TotalMinutes);
			bool flag2 = num <= 0;
			if (flag2)
			{
				ClassTestBase.EndDateTime = ClassTestBase.EndDateTime.AddMinutes(10.0);
			}
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ClassTestBase.ExamId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, ClassTestBase.Course.LuCourseId),
				this.DatabaseManager.GetParameter("@typecode", DbType.String, ClassTestBase.ExamType.GetStringFromClassTestType()),
				this.DatabaseManager.GetParameter("@dateoftest", DbType.DateTime, ClassTestBase.StartDateTime),
				this.DatabaseManager.GetParameter("@testduration", DbType.Int32, num),
				this.DatabaseManager.GetParameter("@location", DbType.String, ClassTestBase.Location ?? ""),
				this.DatabaseManager.GetParameter("@externalexamid", DbType.String, ClassTestBase.ExternalExamId ?? "")
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE exams SET filename=@location,dateoftest=@dateoftest,testduration=@testduration,typecode=@typecode,extendedproperties=@externalexamid\r\nWHERE examid=@examid", parameters);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x000626F8 File Offset: 0x000608F8
		public void MarkTestDelivered(int ExamId, string TestDeliveredMessage)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ExamId),
				this.DatabaseManager.GetParameter("@testdelivered", DbType.String, TestDeliveredMessage ?? DBNull.Value)
			};
			this.DatabaseManager.ExecuteNonQuery("UPDATE exams SET usercomment=@testdelivered WHERE examid=@examid", parameters);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0006275C File Offset: 0x0006095C
		public IList<ClassTest> LoadClassTestDefinitionsByCourse(int LuCourseId, eClassTestType testType = eClassTestType.Unknown)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@typecode", DbType.String, (testType == eClassTestType.Unknown) ? "" : ((testType == eClassTestType.FinalExam) ? "F" : "N"))
			};
			IList<ClassTest> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.lucourseid=@lucid AND (@typecode='' OR e.typecode=@typecode)", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<ClassTest> list = new List<ClassTest>();
					while (dataReader.Read())
					{
						ClassTest classTestFromRecord = ClassTestDefinitionDAO.GetClassTestFromRecord(dataReader);
						bool flag2 = classTestFromRecord != null;
						if (flag2)
						{
							list.Add(classTestFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x00062830 File Offset: 0x00060A30
		public ClassTest LoadClassTestDefinitionByIdAndConfirmInstructorOrAltContact(int ExamId, int InstructorId, int AlternateContactId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ExamId),
				this.DatabaseManager.GetParameter("@iid", DbType.Int32, InstructorId),
				this.DatabaseManager.GetParameter("@aid", DbType.Int32, AlternateContactId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.examid=@examid \r\n         AND\r\n         ( \r\n            (@iid<1 OR EXISTS(SELECT lucourseid FROM vInstructorList WHERE lucourseid=e.lucourseid AND instructorid=@iid))\r\n            OR (@aid<1 OR EXISTS(SELECT lucourseid FROM vAlternateContactList WHERE lucourseid=e.lucourseid AND alternatecontactid=@aid))\r\n         )", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return ClassTestDefinitionDAO.GetClassTestFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x000628E8 File Offset: 0x00060AE8
		public ClassTest LoadClassTestDefinitionByAppointmentId(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     appointments a LEFT JOIN exams e ON e.examid=a.examid\r\n         LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    a.appointmentid=@appid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return ClassTestDefinitionDAO.GetClassTestFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x00062968 File Offset: 0x00060B68
		public ClassTest LoadClassTestDefinitionById(int ExamId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ExamId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.examid=@examid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return ClassTestDefinitionDAO.GetClassTestFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000629E8 File Offset: 0x00060BE8
		public ClassTestForExamRequest LoadClassTestForExamRequestById(int ExamId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ExamId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.examid=@examid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return ClassTestDefinitionDAO.GetClassTestForExamRequestFromRecord(dataReader);
				}
			}
			return null;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00062A68 File Offset: 0x00060C68
		public ClassTestBase LoadClassTestBaseById(int ExamId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ExamId)
			};
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.examid=@examid", parameters))
			{
				bool flag = dataReader != null && dataReader.Read();
				if (flag)
				{
					return ClassTestDefinitionDAO.GetClassTestBaseFromRecord<ClassTestBase>(dataReader, "", null);
				}
			}
			return null;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00062AF0 File Offset: 0x00060CF0
		public void DeleteClassTestDefinition(int ExamId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ExamId)
			};
			this.DatabaseManager.ExecuteNonQuery("DELETE FROM exams WHERE examid=@examid AND NOT examid IN (SELECT examid FROM appointments)", parameters);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00062B34 File Offset: 0x00060D34
		public bool LoadClassTestWasUpdatedByInstructor(int ExamId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamId)
			};
			bool result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT wholastmodified FROM exams WHERE examid=@examid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read() || dataReader["wholastmodified"] is DBNull;
				if (flag)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00062BD4 File Offset: 0x00060DD4
		public void SetInstructorLastModified(int ExamId, int InstructorId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamId),
				databaseLayer.GetParameter("@who", DbType.Int32, (InstructorId > 0) ? InstructorId : DBNull.Value)
			};
			databaseLayer.ExecuteNonQuery("UPDATE exams SET wholastmodified=@who WHERE examid=@examid", parameters);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00062C46 File Offset: 0x00060E46
		public void ClearInstructorLastModified(int ExamId)
		{
			this.SetInstructorLastModified(ExamId, 0);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00062C54 File Offset: 0x00060E54
		public void UpdateInstructorContactedInfo(int ExamId, DateTime? InstructorContactedDate, string Note)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamId),
				databaseLayer.GetParameter("@instructorcontacteddate", DbType.DateTime, (InstructorContactedDate != null) ? InstructorContactedDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@instructorcontactednote", DbType.String, Note ?? "")
			};
			databaseLayer.ExecuteNonQuery("UPDATE exams SET instructorcontactednote=@instructorcontactednote,instructorcontacteddate=@instructorcontacteddate WHERE examid=@examid", parameters);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00062CEC File Offset: 0x00060EEC
		public void UpdateTestPickedUp(int ExamId, DateTime? DatePickedUp, string Note)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamId),
				databaseLayer.GetParameter("@testpickedupdate", DbType.DateTime, (DatePickedUp != null) ? DatePickedUp.Value : DBNull.Value),
				databaseLayer.GetParameter("@testpickedupnote", DbType.String, Note ?? "")
			};
			databaseLayer.ExecuteNonQuery("UPDATE exams SET testpickedupnote=@testpickedupnote,testpickedupdate=@testpickedupdate WHERE examid=@examid", parameters);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00062D84 File Offset: 0x00060F84
		public IList<ClassTestForExamRequest> LoadClassTestsForExamRequestByDateRange(int LuCourseId, DateTime StartDate, DateTime EndDate, eClassTestType testType = eClassTestType.Unknown)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId),
				this.DatabaseManager.GetParameter("@sd", DbType.DateTime, StartDate.Date),
				this.DatabaseManager.GetParameter("@ed", DbType.DateTime, EndDate.Date.AddDays(1.0)),
				this.DatabaseManager.GetParameter("@typecode", DbType.String, (testType == eClassTestType.Unknown) ? "" : ((testType == eClassTestType.FinalExam) ? "F" : "N"))
			};
			IList<ClassTestForExamRequest> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT e.examid,e.lucourseid,e.dateoftest,e.testduration,e.filename AS location,e.dateentered,\r\n         e.whoentered,e.typecode,e.extendedproperties AS externalexamid,\r\n         luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject,\r\n         luc.course,luc.[section],luc.timeofday,\r\n         e.usercomment,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,e.instructoracknowledged,\r\n         e.[description]\r\nFROM     exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n         LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE    e.lucourseid=@lucid AND e.dateoftest>=@sd AND e.dateoftest<@ed AND (@typecode='' OR e.typecode=@typecode)", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<ClassTestForExamRequest> list = new List<ClassTestForExamRequest>();
					while (dataReader.Read())
					{
						ClassTestForExamRequest classTestForExamRequestFromRecord = ClassTestDefinitionDAO.GetClassTestForExamRequestFromRecord(dataReader);
						bool flag2 = classTestForExamRequestFromRecord != null;
						if (flag2)
						{
							list.Add(classTestForExamRequestFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x040005AA RID: 1450
		private DatabaseLayer DatabaseManager;
	}
}
