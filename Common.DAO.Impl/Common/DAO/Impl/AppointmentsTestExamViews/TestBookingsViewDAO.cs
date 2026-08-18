using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsTestExamViews;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.TestBookings;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestExamViews
{
	// Token: 0x02000144 RID: 324
	public class TestBookingsViewDAO : ITestBookingsViewDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public TestBookingsViewDAO()
		{
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00060CC8 File Offset: 0x0005EEC8
		public TestBookingsViewDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00060CDA File Offset: 0x0005EEDA
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x00060CE2 File Offset: 0x0005EEE2
		public OperationContext OpContext { get; set; }

		// Token: 0x0600095B RID: 2395 RVA: 0x00060CEC File Offset: 0x0005EEEC
		public IList<TestBookingsViewLight> LoadTestBookingsViewLight(TestBookingsViewContext context)
		{
			return this.LoadTestBookingsView<TestBookingsViewLight>("DECLARE @startdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @sd))\r\nDECLARE @enddate datetime = DATEADD(D, 1, DATEDIFF(D, 0, @ed))\r\nDECLARE @allowcancelled bit = (SELECT CASE WHEN @hidecancelled IS NULL OR @hidecancelled=0 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS allowcancelled)\r\n\r\nSELECT\te.examid,a.appointmentid,att.personid,pg.GroupID\r\n\t\t,CASE WHEN NOT e.wholastmodified IS NULL THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS InstructorSubmitted\r\n\t\t,CASE\t\r\n\t\t\tWHEN p.personid IS NULL THEN 'No students'\r\n\t\t\tWHEN NOT c.registrationstatus IS NULL AND c.registrationstatus=2 THEN 'Dropped'\r\n\t\t\tWHEN a.AppointmentID IS NULL THEN 'Unknown'\r\n            WHEN a.cancelled=1 THEN 'Cancelled'\r\n            WHEN att.noshow=1 THEN 'No-show'\r\n            WHEN a.appcode=-1 THEN 'Tentative'\r\n            WHEN NOT aa.personid IS NULL THEN 'Accommodations modified'\r\n            ELSE 'Booked'\r\n\t\t END AS Status\r\n\t\t,e.dateoftest AS classdate,e.testduration,e.typecode,e.usercomment\r\n\t\t,a.startDate,a.endDate\r\n        ,a.totalbreakminutes\r\n\t\t,a.appcode,a.cancelled AS Cancelled\r\n\t\t,a.apptypeid,at.[description] AS Description\r\n\t\t,luc.LUCourseID,c.DateLetterIssued\r\n\t\t,luc.startdate AS coursestartdate,luc.enddate AS courseenddate\r\n\t\t,lucd.phone AS Department,lucd.email AS DepartmentEmail\r\n\t\t,lucd.altlookupstring AS Subject,luc.course AS Course,luc.timeofday,luc.section AS Section\r\n        ,coalesce(lucd.altlookupstring,'','') + ' ' + luc.course + luc.timeofday + ' ' + luc.section + ' (' + luc.term + ')' AS CourseDescription\r\n        ,luc.[location] AS ClassRoom,luc.campus,luc.department AS DepartmentCode\r\n\t\t,p.firstname,p.lastname,p.student_no\r\n        ,att.noshow\r\n\t\t,a.[location],a.dateadded,a.ActualStartTime,a.ActualEndTime\r\n        ,ac.originalstartdatetime \r\n        ,ac.originalenddatetime \r\n        ,ac.ExamStatusLookupId,el.title AS ExamStatus,el.colourargb\r\n\t\t,e.dateoftest,a.startdate,e.[filename] AS classlocation\r\nFROM\texams e LEFT JOIN Appointments a ON a.examid=e.examid \r\n        LEFT JOIN people pwho ON pwho.personid=a.personid\r\n\t\tLEFT JOIN AppointmentTypes at ON at.AppTypeID=a.apptypeid\r\n\r\n\t\tLEFT JOIN attendees att ON att.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN people p ON p.personid=att.personid \r\n\t\tLEFT JOIN PeopleGroups pg ON pg.PersonID=att.PersonID AND pg.GroupID<5 \r\n\r\n\t\tLEFT JOIN AppointmentCourses ac ON ac.AppointmentID=a.appointmentid\r\n\r\n\t\tLEFT JOIN LUCourses luc ON luc.LUCourseID=ac.lucourseid\r\n\t\tLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.subjectid\r\n\t\tLEFT JOIN Courses c ON c.personID=p.PersonID AND c.luCourseID=luc.lucourseid\r\n\r\n\t\tLEFT JOIN AppointmentsLastDateModified amd ON amd.AppointmentId=a.AppointmentID\r\n\t\tLEFT JOIN (SELECT DISTINCT personid,MAX(dateentered) AS maxdateentered FROM accommodationsapproval GROUP BY personid) aa ON aa.personid=p.PersonID AND aa.maxdateentered>amd.DateLastModified\r\n        LEFT JOIN ExamStatusLookup el ON el.ExamStatusLookupId=ac.ExamStatusLookupId\r\nWHERE\t(a.startdate BETWEEN @startdate AND @enddate ) \r\n        AND NOT a.AppointmentID IS NULL \r\n\t\tAND NOT pg.personid IS NULL \r\n\t\tAND (NOT a.appointmentid IS NULL) \r\n        AND (@allowcancelled=1 OR a.cancelled=0)\r\nORDER BY a.AppointmentID,e.examid,att.personid ", context, new Func<TestBookingsViewLight, IBatchDecryptor, IDataReader, TestBookingsViewLight>(this.SetMainTestBookingViewDataLight), new Func<TestBookingsViewLight, IBatchDecryptor, IDataReader, TestBookingsViewLight>(this.SetExtraTestBookingViewDataLight));
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x00060D24 File Offset: 0x0005EF24
		public IList<TestBookingsViewFull> LoadTestBookingsViewFull(TestBookingsViewContext context)
		{
			return this.LoadTestBookingsView<TestBookingsViewFull>("DECLARE @startdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @sd))\r\nDECLARE @enddate datetime = DATEADD(D, 1, DATEDIFF(D, 0, @ed))\r\nDECLARE @allowcancelled bit = (SELECT CASE WHEN @hidecancelled IS NULL OR @hidecancelled=0 THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS allowcancelled)\r\n\r\nSELECT\te.examid,a.appointmentid,att.personid,pg.GroupID\r\n\t\t,CASE WHEN NOT e.wholastmodified IS NULL THEN CAST(1 AS bit) ELSE CAST(0 AS bit) END AS InstructorSubmitted\r\n\t\t,CASE\t\r\n\t\t\tWHEN p.personid IS NULL THEN 'No students'\r\n\t\t\tWHEN NOT c.registrationstatus IS NULL AND c.registrationstatus=2 THEN 'Dropped'\r\n\t\t\tWHEN a.AppointmentID IS NULL THEN 'Unknown'\r\n            WHEN a.cancelled=1 THEN 'Cancelled'\r\n            WHEN att.noshow=1 THEN 'No-show'\r\n            WHEN a.appcode=-1 THEN 'Tentative'\r\n            WHEN NOT aa.personid IS NULL THEN 'Accommodations modified'\r\n            ELSE 'Booked'\r\n\t\t END AS Status\r\n\t\t,e.dateoftest AS classdate,e.testduration,e.typecode,e.usercomment\r\n\t\t,a.startDate,a.endDate\r\n        ,a.totalbreakminutes\r\n\t\t,a.appcode,a.cancelled AS Cancelled\r\n\t\t,a.apptypeid,at.[description] AS Description\r\n\t\t,luc.LUCourseID,c.DateLetterIssued\r\n\t\t,luc.startdate AS coursestartdate,luc.enddate AS courseenddate\r\n\t\t,lucd.phone AS Department,lucd.email AS DepartmentEmail\r\n\t\t,lucd.altlookupstring AS Subject,luc.course AS Course,luc.timeofday,luc.section AS Section\r\n        ,coalesce(lucd.altlookupstring,'','') + ' ' + luc.course + luc.timeofday + ' ' + luc.section + ' (' + luc.term + ')' AS CourseDescription\r\n        ,luc.[location] AS ClassRoom,luc.campus,luc.department AS DepartmentCode\r\n\t\t,p.firstname,p.lastname,p.student_no\r\n        ,att.noshow\r\n\t\t,a.[location],a.dateadded,a.ActualStartTime,a.ActualEndTime\r\n        ,ac.originalstartdatetime \r\n        ,ac.originalenddatetime \r\n        ,ac.ExamStatusLookupId,el.title AS ExamStatus,el.colourargb\r\n\t\t,e.dateoftest,a.startdate,e.[filename] AS classlocation,\r\n\t\tam.memoText,am.isEncrypted\r\nFROM\texams e LEFT JOIN Appointments a ON a.examid=e.examid \r\n        LEFT JOIN people pwho ON pwho.personid=a.personid\r\n\t\tLEFT JOIN AppointmentTypes at ON at.AppTypeID=a.apptypeid\r\n\r\n\t\tLEFT JOIN attendees att ON att.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN people p ON p.personid=att.personid \r\n\t\tLEFT JOIN PeopleGroups pg ON pg.PersonID=att.PersonID\r\n\r\n\t\tLEFT JOIN AppointmentCourses ac ON ac.AppointmentID=a.appointmentid\r\n\r\n\t\tLEFT JOIN LUCourses luc ON luc.LUCourseID=ac.lucourseid\r\n\t\tLEFT JOIN LUCourseData lucd ON lucd.luCourseDataID=luc.subjectid\r\n\t\tLEFT JOIN Courses c ON c.personID=p.PersonID AND c.luCourseID=luc.lucourseid\r\n\r\n\t\tLEFT JOIN AppointmentsLastDateModified amd ON amd.AppointmentId=a.AppointmentID\r\n\t\tLEFT JOIN (SELECT DISTINCT personid,MAX(dateentered) AS maxdateentered FROM accommodationsapproval GROUP BY personid) aa ON aa.personid=p.PersonID AND aa.maxdateentered>amd.DateLastModified\r\n        LEFT JOIN ExamStatusLookup el ON el.ExamStatusLookupId=ac.ExamStatusLookupId\r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID\r\nWHERE\t(a.startdate BETWEEN @startdate AND @enddate ) \r\n        AND NOT a.AppointmentID IS NULL \r\n\t\tAND NOT pg.personid IS NULL \r\n\t\tAND (NOT a.appointmentid IS NULL) \r\n        AND (@allowcancelled=1 OR a.cancelled=0)\r\nORDER BY a.AppointmentID,e.examid,att.personid", context, new Func<TestBookingsViewFull, IBatchDecryptor, IDataReader, TestBookingsViewFull>(this.SetMainTestBookingViewDataFull), new Func<TestBookingsViewFull, IBatchDecryptor, IDataReader, TestBookingsViewFull>(this.SetExtraTestBookingViewDataFull));
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00060D5C File Offset: 0x0005EF5C
		private IList<T> LoadTestBookingsView<T>(string sql, TestBookingsViewContext context, Func<T, IBatchDecryptor, IDataReader, T> getMainTestBooking, Func<T, IBatchDecryptor, IDataReader, T> getExtraTestBooking) where T : TestBookingsViewBase
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@sd", DbType.DateTime, context.StartDate.Date),
				databaseLayer.GetParameter("@ed", DbType.DateTime, context.EndDate.Date),
				databaseLayer.GetParameter("@hidecancelled", DbType.Boolean, context.HideCancelled)
			};
			IList<T> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader(sql, parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					T t = default(T);
					List<T> list = new List<T>();
					while (dataReader.Read())
					{
						int num = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
						bool flag2 = t == null || num != t.AppointmentId;
						if (flag2)
						{
							t = Activator.CreateInstance<T>();
							t.AppointmentId = num;
							list.Add(t);
							getMainTestBooking(t, batchDecryptor, dataReader);
						}
						getExtraTestBooking(t, batchDecryptor, dataReader);
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00060EDC File Offset: 0x0005F0DC
		private TestBookingsViewBase SetMainTestBookingViewDataBase(TestBookingsViewBase item, IBatchDecryptor batchDecryptor, IDataReader record)
		{
			int num = (record["apptypeid"] is DBNull) ? 0 : ((int)record["apptypeid"]);
			AppTypeBase appointmentTypeBase;
			if (num >= 1)
			{
				AppTypeBase appTypeBase = new AppTypeBase();
				appTypeBase.AppTypeId = num;
				appointmentTypeBase = appTypeBase;
				appTypeBase.Description = record["apptypedescription"].ToString().Trim();
			}
			else
			{
				appointmentTypeBase = null;
			}
			item.AppointmentTypeBase = appointmentTypeBase;
			item.ExamId = ((record["examid"] is DBNull) ? 0 : ((int)record["examid"]));
			item.ClassTestStartDateTime = ((DateTime)record["classdate"]).Date;
			item.ClassTestEndDateTime = item.ClassTestStartDateTime.AddMinutes((double)((int)record["testduration"]));
			string a = record["typecode"].ToString().ToLower().Trim();
			item.ClassTestType = ((a == "f") ? eClassTestType.FinalExam : eClassTestType.Midterm);
			item.CourseTitle = record["CourseDescription"].ToString();
			item.HasTestCopy = (!(record["usercomment"] is DBNull) && ((string)record["usercomment"]).Trim().Length > 0);
			item.IsCancelled = (!(record["cancelled"] is DBNull) && Convert.ToBoolean(record["cancelled"]));
			item.IsTentative = (((record["appcode"] is DBNull) ? 0 : ((int)record["appcode"])) == -1);
			int num2 = (record["ExamStatusLookupId"] is DBNull) ? 0 : ((int)record["ExamStatusLookupId"]);
			item.Label = ((num2 > 0) ? new TestLabel
			{
				ExamStatusLookupId = num2,
				Title = record["ExamStatus"].ToString().Trim(),
				ColourArgb = ((record["colourargb"] is DBNull) ? 0 : ((int)record["colourargb"]))
			} : null);
			item.Location = ((record["location"] is DBNull) ? null : batchDecryptor.Decrypt((byte[])record["location"]));
			item.ScheduledStartDateTime = (DateTime)record["startdate"];
			item.ScheduledStartDateTime = (DateTime)record["enddate"];
			item.TestCopyNote = record["usercomment"].ToString().Trim();
			string statusCode = record["Status"].ToString().Trim();
			item.Status = ((eTestBookingsStatus[])Enum.GetValues(typeof(eTestBookingsStatus))).FirstOrDefault(delegate(eTestBookingsStatus g)
			{
				TestBookingsStatusAttribute attribute = g.GetAttribute<TestBookingsStatusAttribute>();
				return attribute != null && attribute.Title.Equals(statusCode, StringComparison.OrdinalIgnoreCase);
			});
			item.StudentReportedClassTestStartDateTime = (DateTime)record["originalstartdatetime"];
			item.StudentReportedClassTestEndDateTime = (DateTime)record["originalenddatetime"];
			return item;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00061228 File Offset: 0x0005F428
		private TestBookingsViewBase SetExtraTestBookingViewDataBase(TestBookingsViewBase item, IBatchDecryptor batchDecryptor, IDataReader record)
		{
			int num = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
			bool flag = num > 0;
			if (flag)
			{
				int num2 = (record["groupid"] is DBNull) ? 0 : ((int)record["groupid"]);
				bool flag2 = num2 == 1;
				if (flag2)
				{
					bool flag3 = item.Student == null;
					if (flag3)
					{
						item.Student = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor);
						item.IsNoShow = (!(record["noshow"] is DBNull) && Convert.ToBoolean(record["noshow"]));
					}
				}
				else
				{
					bool flag4 = num2 == 3;
					if (flag4)
					{
						bool flag5 = item.Room == null;
						if (flag5)
						{
							BasicPerson basicPersonFromRecord = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor);
							item.Room = new AppointmentRoom
							{
								RoomId = basicPersonFromRecord.PersonId,
								RoomTitle = basicPersonFromRecord.FirstName,
								RoomDescription = basicPersonFromRecord.LastName,
								RoomUniqueId = basicPersonFromRecord.StudentNumber
							};
						}
					}
				}
			}
			return item;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00061364 File Offset: 0x0005F564
		private TestBookingsViewLight SetMainTestBookingViewDataLight(TestBookingsViewLight item, IBatchDecryptor batchDecryptor, IDataReader record)
		{
			this.SetMainTestBookingViewDataBase(item, batchDecryptor, record);
			return item;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00061384 File Offset: 0x0005F584
		private TestBookingsViewLight SetExtraTestBookingViewDataLight(TestBookingsViewLight item, IBatchDecryptor batchDecryptor, IDataReader record)
		{
			this.SetExtraTestBookingViewDataBase(item, batchDecryptor, record);
			return item;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000613A4 File Offset: 0x0005F5A4
		private TestBookingsViewFull SetMainTestBookingViewDataFull(TestBookingsViewFull item, IBatchDecryptor batchDecryptor, IDataReader record)
		{
			this.SetMainTestBookingViewDataBase(item, batchDecryptor, record);
			byte[] array = (record["memotext"] is DBNull) ? null : ((byte[])record["memotext"]);
			bool flag = array != null;
			if (flag)
			{
				item.MemoPlainText = ((!(record["isencrypted"] is DBNull) && Convert.ToBoolean(record["isencrypted"])) ? batchDecryptor.Decrypt(array) : Encoding.UTF8.GetString(array));
			}
			return item;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00061438 File Offset: 0x0005F638
		private TestBookingsViewFull SetExtraTestBookingViewDataFull(TestBookingsViewFull item, IBatchDecryptor batchDecryptor, IDataReader record)
		{
			this.SetExtraTestBookingViewDataBase(item, batchDecryptor, record);
			int pid = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
			int num = (pid > 0) ? ((record["groupid"] is DBNull) ? 0 : ((int)record["groupid"])) : 0;
			bool flag;
			if (num != 1 && num != 3)
			{
				IList<BasicPerson> proctors = item.Proctors;
				flag = (proctors != null && proctors.All((BasicPerson g) => g.PersonId != pid));
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = item.Proctors == null;
				if (flag3)
				{
					item.Proctors = new List<BasicPerson>();
				}
				BasicPerson basicPersonFromRecord = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor);
				bool flag4 = basicPersonFromRecord != null;
				if (flag4)
				{
					item.Proctors.Add(basicPersonFromRecord);
				}
			}
			return item;
		}
	}
}
