using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.Appointments;
using TechnoPro.Common.DAO.Impl.DynamicForms;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Accommodations;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeData;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x02000152 RID: 338
	public class TestBookingDAO : ITestBookingDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00067618 File Offset: 0x00065818
		private DynamicDataDAO dynamicDataDao
		{
			get
			{
				bool flag = this._dynamicDataDao == null;
				if (flag)
				{
					this._dynamicDataDao = new DynamicDataDAO(this.OpContext);
				}
				return this._dynamicDataDao;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0006764E File Offset: 0x0006584E
		// (set) Token: 0x060009E5 RID: 2533 RVA: 0x00067656 File Offset: 0x00065856
		public DatabaseLayer DatabaseManager { get; private set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00067660 File Offset: 0x00065860
		private DynamicFormsDAO dynamicFormsDAO
		{
			get
			{
				bool flag = this.dd == null;
				if (flag)
				{
					this.dd = new DynamicFormsDAO(this.OpContext);
				}
				return this.dd;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00067698 File Offset: 0x00065898
		private LookupCourseDAO lookupCourseDAO
		{
			get
			{
				bool flag = this.ld == null;
				if (flag)
				{
					this.ld = new LookupCourseDAO(this.OpContext);
				}
				return this.ld;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x000676D0 File Offset: 0x000658D0
		private PeopleDAO peopleDAO
		{
			get
			{
				bool flag = this.pd == null;
				if (flag)
				{
					this.pd = new PeopleDAO(this.OpContext);
				}
				return this.pd;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00067708 File Offset: 0x00065908
		private TestBookingDAO testDAO
		{
			get
			{
				bool flag = this.td == null;
				if (flag)
				{
					this.td = new TestBookingDAO(this.OpContext);
				}
				return this.td;
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0006773E File Offset: 0x0006593E
		public TestBookingDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0006776F File Offset: 0x0006596F
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00067777 File Offset: 0x00065977
		public OperationContext OpContext { get; set; }

		// Token: 0x060009ED RID: 2541 RVA: 0x00067780 File Offset: 0x00065980
		public TestForEdit LoadTestForEditById(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			TestForEdit result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,a.subject AS subtitle,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,pg.groupid,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour,\r\n        a.examid,e.dateentered,e.lucourseid AS examcourselucourseid,e.description,e.dateoftest,e.visible,e.usercomment,e.testduration,e.lastmodified,e.wholastmodified,\r\n        e.typecode,e.extendedproperties,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,\r\n        e.instructoracknowledged,e.filename,\r\n        ac.appointmentcourseid,ac.lucourseid AS studentcourselucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS examcoursestartdate,luc.enddate AS examcourseenddate,luc.duration AS examcourseduration,luc.term AS examcourseterm,luc.subjectid AS examcoursesubjectid,\r\n        lucd.altlookupstring AS examcoursesubject,luc.course AS examcoursecourse,luc.[section] AS examcoursesection,luc.timeofday AS examcoursetimeofday,\r\n        lucs.startdate AS studentcoursestartdate,lucs.enddate AS studentcourseenddate,lucs.duration AS studentcourseduration,lucs.term AS studentcourseterm,\r\n        lucs.subjectid AS studentcoursesubjectid,lucds.altlookupstring AS studentcoursesubject,lucs.course AS studentcoursecourse,\r\n        lucs.[section] AS studentcoursesection,lucs.timeofday AS studentcoursetimeofday,\r\n        rm.firstname AS roomfirstname,rm.student_no AS roomstudent_no,rm.lastname AS roomlastname,attrm.personid AS roompersonid,a.totalbreakminutes AS breaktimeminutes\r\nFROM\tapps a LEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\n        LEFT JOIN exams e ON e.examid=a.examid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n        LEFT JOIN lucourses lucs ON lucs.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucds ON lucds.lucoursedataid=lucs.subjectid\r\n        LEFT JOIN attendees attrm ON attrm.appointmentid=a.appointmentid AND attrm.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people rm ON rm.personid=attrm.personid\r\n        LEFT JOIN peoplegroups pg ON pg.groupid<=10 AND pg.personid=a.personid\r\nWHERE a.appointmentid=@appid\r\nORDER BY pg.groupid,a.AttendeeID", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2;
					result = this.GetNextTestForEditFromReader(dataReader, out flag2, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0006781C File Offset: 0x00065A1C
		private TestForEdit GetNextTestForEditFromReader(IDataReader reader, out bool isThereAnotherTestToRead, IBatchDecryptor batchDecryptor)
		{
			bool flag = reader == null;
			TestForEdit result;
			if (flag)
			{
				isThereAnotherTestToRead = false;
				result = null;
			}
			else
			{
				Test mainBaseExtendedAppointment = BaseAppointmentDAO.GetMainBaseExtendedAppointment<Test>(reader, this.OpContext, batchDecryptor);
				bool flag2 = mainBaseExtendedAppointment == null;
				if (flag2)
				{
					isThereAnotherTestToRead = false;
					result = null;
				}
				else
				{
					TestForEdit testForEdit = new TestForEdit
					{
						Test = mainBaseExtendedAppointment
					};
					BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(reader, mainBaseExtendedAppointment, this.OpContext, batchDecryptor);
					this.AddExtendedTestInfoFromRecord(ref mainBaseExtendedAppointment, reader, batchDecryptor);
					this.AddExtendedInfoToTestForEdit(ref testForEdit, reader, batchDecryptor);
					for (;;)
					{
						bool flag3 = !reader.Read();
						if (flag3)
						{
							break;
						}
						int num = (int)reader["appointmentid"];
						bool flag4 = num != mainBaseExtendedAppointment.AppointmentId;
						if (flag4)
						{
							goto Block_4;
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(reader, mainBaseExtendedAppointment, this.OpContext, batchDecryptor);
						this.AddExtendedTestInfoFromRecord(ref mainBaseExtendedAppointment, reader, batchDecryptor);
						this.AddExtendedInfoToTestForEdit(ref testForEdit, reader, batchDecryptor);
					}
					isThereAnotherTestToRead = false;
					return testForEdit;
					Block_4:
					isThereAnotherTestToRead = true;
					result = testForEdit;
				}
			}
			return result;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00067908 File Offset: 0x00065B08
		private void AddExtendedInfoToTestForEdit(ref TestForEdit testForEdit, IDataReader record, IBatchDecryptor batchDecryptor)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			IEncryption encryption = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null).Encryption;
			bool flag = testForEdit.PrivateNote == null && PeopleDAO.ReaderContainsColumn(record, "privatenote2");
			if (flag)
			{
				testForEdit.PrivateNote = ((record["privatenote2"] is DBNull) ? "" : ((string)record["privatenote2"]));
			}
			bool flag2 = testForEdit.TestNote == null && PeopleDAO.ReaderContainsColumn(record, "testnote");
			if (flag2)
			{
				byte[] array = (record["testnote"] is DBNull) ? null : ((byte[])record["testnote"]);
				testForEdit.TestNote = ((array == null) ? "" : ((batchDecryptor == null) ? encryption.Decrypt(array) : batchDecryptor.Decrypt(array)));
			}
			bool flag3 = testForEdit.BookingNote == null && PeopleDAO.ReaderContainsColumn(record, "studentnote");
			if (flag3)
			{
				byte[] array2 = (record["studentnote"] is DBNull) ? null : ((byte[])record["studentnote"]);
				testForEdit.BookingNote = ((array2 == null) ? "" : ((batchDecryptor == null) ? encryption.Decrypt(array2) : batchDecryptor.Decrypt(array2)));
			}
			bool flag4 = testForEdit.StudentReportedClassStartDateTime == null;
			if (flag4)
			{
				testForEdit.StudentReportedClassStartDateTime = (PeopleDAO.ReaderContainsColumn(record, "originalstartdatetime") ? ((record["originalstartdatetime"] is DBNull) ? null : new DateTime?((DateTime)record["originalstartdatetime"])) : null);
				testForEdit.StudentReportedClassEndDateTime = (PeopleDAO.ReaderContainsColumn(record, "originalenddatetime") ? ((record["originalenddatetime"] is DBNull) ? null : new DateTime?((DateTime)record["originalenddatetime"])) : null);
			}
			bool flag5 = testForEdit.InstructorSubmittedTestInfo == null && PeopleDAO.ReaderContainsColumn(record, "wholastmodified");
			if (flag5)
			{
				testForEdit.InstructorSubmittedTestInfo = new bool?(!(record["wholastmodified"] is DBNull));
			}
			bool flag6 = testForEdit.TestDeliveryMethod == null && PeopleDAO.ReaderContainsColumn(record, "usercomment");
			if (flag6)
			{
				testForEdit.TestDeliveryMethod = ((record["usercomment"] is DBNull) ? "" : ((string)record["usercomment"]));
			}
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x00067BB4 File Offset: 0x00065DB4
		private Test GetNextTestFromReader(IDataReader reader, out bool isThereAnotherTestToRead, IBatchDecryptor batchDecryptor)
		{
			bool flag = reader == null;
			Test result;
			if (flag)
			{
				isThereAnotherTestToRead = false;
				result = null;
			}
			else
			{
				Test mainBaseExtendedAppointment = BaseAppointmentDAO.GetMainBaseExtendedAppointment<Test>(reader, this.OpContext, batchDecryptor);
				bool flag2 = mainBaseExtendedAppointment == null;
				if (flag2)
				{
					isThereAnotherTestToRead = false;
					result = null;
				}
				else
				{
					BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(reader, mainBaseExtendedAppointment, this.OpContext, batchDecryptor);
					this.AddExtendedTestInfoFromRecord(ref mainBaseExtendedAppointment, reader, batchDecryptor);
					for (;;)
					{
						bool flag3 = !reader.Read();
						if (flag3)
						{
							break;
						}
						int num = (int)reader["appointmentid"];
						bool flag4 = num != mainBaseExtendedAppointment.AppointmentId;
						if (flag4)
						{
							goto Block_4;
						}
						BaseAppointmentDAO.AddExtendedInfoToBaseExtendedAppointment(reader, mainBaseExtendedAppointment, this.OpContext, batchDecryptor);
						this.AddExtendedTestInfoFromRecord(ref mainBaseExtendedAppointment, reader, batchDecryptor);
					}
					isThereAnotherTestToRead = false;
					return mainBaseExtendedAppointment;
					Block_4:
					isThereAnotherTestToRead = true;
					result = mainBaseExtendedAppointment;
				}
			}
			return result;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x00067C78 File Offset: 0x00065E78
		private void AddExtendedTestInfoFromRecord(ref Test test, IDataReader record, IBatchDecryptor batchDecryptor)
		{
			bool flag = test == null || record == null;
			if (!flag)
			{
				bool flag2 = test.ClassTestInfo == null;
				if (flag2)
				{
					int num = (record["examid"] is DBNull) ? 0 : ((int)record["examid"]);
					bool flag3 = num > 0;
					if (flag3)
					{
						test.ClassTestInfo = ClassTestDefinitionDAO.GetClassTestBaseFromRecord<ClassTestBase>(record, "examcourse", batchDecryptor);
					}
				}
				bool flag4 = test.StudentClassTestInfo == null;
				if (flag4)
				{
					int num2 = (record["appointmentcourseid"] is DBNull) ? 0 : ((int)record["appointmentcourseid"]);
					bool flag5 = num2 > 0;
					if (flag5)
					{
						test.StudentClassTestInfo = StudentClassTestInfoDAO.GetStudentClassTestFromRecord(record, this.OpContext, "studentcourse", batchDecryptor);
					}
				}
				bool flag6 = test.BreakTimeMinutes < 1;
				if (flag6)
				{
					test.BreakTimeMinutes = (PeopleDAO.ReaderContainsColumn(record, "breaktimeminutes") ? ((record["breaktimeminutes"] is DBNull) ? 0 : ((int)record["breaktimeminutes"])) : 0);
				}
				bool flag7 = PeopleDAO.ReaderContainsColumn(record, "roompersonid");
				if (flag7)
				{
					PersonBase personFromReader = PeopleDAO.GetPersonFromReader("room", record, this.OpContext, null);
					bool flag8 = personFromReader != null;
					if (flag8)
					{
						test.Room = new AppointmentRoom
						{
							RoomId = personFromReader.PersonId,
							RoomTitle = personFromReader.FirstName,
							RoomDescription = personFromReader.LastName,
							RoomUniqueId = personFromReader.Student_no
						};
					}
				}
			}
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00067E10 File Offset: 0x00066010
		private List<Test> GetTestsFromRecords(IDataReader reader)
		{
			List<Test> list = new List<Test>();
			IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
			bool flag;
			for (Test nextTestFromReader = this.GetNextTestFromReader(reader, out flag, batchDecryptor); nextTestFromReader != null; nextTestFromReader = this.GetNextTestFromReader(reader, out flag, batchDecryptor))
			{
				list.Add(nextTestFromReader);
				bool flag2 = !flag;
				if (flag2)
				{
					return list;
				}
			}
			return list;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00067E78 File Offset: 0x00066078
		private List<AccommodationForTest> ConsolodateApprovedAccommodationsAndTestAccommodations(List<AccommodationForTest> accomms)
		{
			List<AccommodationForTest> list = new List<AccommodationForTest>();
			using (List<AccommodationForTest>.Enumerator enumerator = accomms.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					AccommodationForTest acc = enumerator.Current;
					AccommodationForTest accommodationForTest = list.Find((AccommodationForTest a2) => a2.DynamicFieldData.Field.ControlId == acc.DynamicFieldData.Field.ControlId);
					bool flag = accommodationForTest == null;
					if (flag)
					{
						List<AccommodationForTest> list2 = accomms.FindAll((AccommodationForTest ac) => ac.DynamicFieldData.Field.ControlId == acc.DynamicFieldData.Field.ControlId);
						bool flag2 = list2.Count > 0;
						if (flag2)
						{
							AccommodationForTest accommodationForTest2 = list2.Find((AccommodationForTest m) => m.UseForTest);
							AccommodationForTest accommodationForTest3 = list2.Find((AccommodationForTest m) => !m.UseForTest);
							bool flag3 = accommodationForTest2 != null && accommodationForTest3 != null;
							bool discrepency;
							string discrepencyMessage;
							if (flag3)
							{
								string a = "";
								string text = "";
								bool flag4 = a != text;
								if (flag4)
								{
									discrepency = true;
									discrepencyMessage = (("Current value has changed to: " + text == null) ? "" : text);
								}
								else
								{
									discrepency = false;
									discrepencyMessage = "";
								}
							}
							else
							{
								discrepency = false;
								discrepencyMessage = "";
							}
							bool flag5 = accommodationForTest2 != null;
							if (flag5)
							{
								accommodationForTest2.Discrepency = discrepency;
								accommodationForTest2.DiscrepencyMessage = discrepencyMessage;
								list.Add(accommodationForTest2);
							}
							else
							{
								bool flag6 = accommodationForTest3 != null;
								if (flag6)
								{
									accommodationForTest3.Discrepency = discrepency;
									accommodationForTest3.DiscrepencyMessage = discrepencyMessage;
									list.Add(accommodationForTest3);
								}
							}
						}
						else
						{
							list.Add(acc);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0006805C File Offset: 0x0006625C
		private AccommodationForTest GetAccommodationForTestFromReader(IDataReader reader)
		{
			bool flag = reader == null;
			AccommodationForTest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DynamicData dataFromRecords = this.dynamicDataDao.GetDataFromRecords(reader);
				bool flag2 = dataFromRecords == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					object obj = reader["usefortest"];
					AccommodationForTest accommodationForTest = new AccommodationForTest
					{
						DynamicFieldData = dataFromRecords,
						UseForTest = (obj != DBNull.Value && Convert.ToBoolean(obj))
					};
					result = accommodationForTest;
				}
			}
			return result;
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000680CC File Offset: 0x000662CC
		public List<Test> LoadTests(DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0)),
				this.DatabaseManager.GetParameter("@hidecancelled", DbType.Boolean, HideCancelled)
			};
			List<Test> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,a.subject AS subtitle,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,pg.groupid,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour,\r\n        a.examid,e.dateentered,e.lucourseid AS examcourselucourseid,e.description,e.dateoftest,e.visible,e.usercomment,e.testduration,e.lastmodified,e.wholastmodified,\r\n        e.typecode,e.extendedproperties,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,\r\n        e.instructoracknowledged,e.filename,\r\n        ac.appointmentcourseid,ac.lucourseid AS studentcourselucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS examcoursestartdate,luc.enddate AS examcourseenddate,luc.duration AS examcourseduration,luc.term AS examcourseterm,luc.subjectid AS examcoursesubjectid,\r\n        lucd.altlookupstring AS examcoursesubject,luc.course AS examcoursecourse,luc.[section] AS examcoursesection,luc.timeofday AS examcoursetimeofday,\r\n        lucs.startdate AS studentcoursestartdate,lucs.enddate AS studentcourseenddate,lucs.duration AS studentcourseduration,lucs.term AS studentcourseterm,\r\n        lucs.subjectid AS studentcoursesubjectid,lucds.altlookupstring AS studentcoursesubject,lucs.course AS studentcoursecourse,\r\n        lucs.[section] AS studentcoursesection,lucs.timeofday AS studentcoursetimeofday,\r\n        rm.firstname AS roomfirstname,rm.student_no AS roomstudent_no,rm.lastname AS roomlastname,attrm.personid AS roompersonid,a.totalbreakminutes AS breaktimeminutes\r\nFROM\tapps a LEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\n        LEFT JOIN exams e ON e.examid=a.examid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n        LEFT JOIN lucourses lucs ON lucs.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucds ON lucds.lucoursedataid=lucs.subjectid\r\n        LEFT JOIN attendees attrm ON attrm.appointmentid=a.appointmentid AND attrm.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people rm ON rm.personid=attrm.personid\r\n        LEFT JOIN peoplegroups pg ON pg.groupid<=10 AND pg.personid=a.personid\r\nWHERE (NOT a.examid IS NULL OR a.apptypeid IN (SELECT apptypeid FROM appointmenttypes WHERE iscourse=1))\r\n        AND a.startdate BETWEEN @startdate AND @enddate\r\n        AND (@hidecancelled=0 OR a.cancelled=0)\r\n        AND a.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\nORDER BY a.appointmentid,pg.groupid,a.AttendeeID", new CommandOverrideSettings(120), parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = !dataReader.Read();
					if (flag2)
					{
						result = new List<Test>();
					}
					else
					{
						result = this.GetTestsFromRecords(dataReader);
					}
				}
			}
			return result;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000681C4 File Offset: 0x000663C4
		public Test LoadTestById(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			Test result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,a.subject AS subtitle,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,pg.groupid,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour,\r\n        a.examid,e.dateentered,e.lucourseid AS examcourselucourseid,e.description,e.dateoftest,e.visible,e.usercomment,e.testduration,e.lastmodified,e.wholastmodified,\r\n        e.typecode,e.extendedproperties,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,\r\n        e.instructoracknowledged,e.filename,\r\n        ac.appointmentcourseid,ac.lucourseid AS studentcourselucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS examcoursestartdate,luc.enddate AS examcourseenddate,luc.duration AS examcourseduration,luc.term AS examcourseterm,luc.subjectid AS examcoursesubjectid,\r\n        lucd.altlookupstring AS examcoursesubject,luc.course AS examcoursecourse,luc.[section] AS examcoursesection,luc.timeofday AS examcoursetimeofday,\r\n        lucs.startdate AS studentcoursestartdate,lucs.enddate AS studentcourseenddate,lucs.duration AS studentcourseduration,lucs.term AS studentcourseterm,\r\n        lucs.subjectid AS studentcoursesubjectid,lucds.altlookupstring AS studentcoursesubject,lucs.course AS studentcoursecourse,\r\n        lucs.[section] AS studentcoursesection,lucs.timeofday AS studentcoursetimeofday,\r\n        rm.firstname AS roomfirstname,rm.student_no AS roomstudent_no,rm.lastname AS roomlastname,attrm.personid AS roompersonid,a.totalbreakminutes AS breaktimeminutes\r\nFROM\tapps a LEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\n        LEFT JOIN exams e ON e.examid=a.examid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n        LEFT JOIN lucourses lucs ON lucs.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucds ON lucds.lucoursedataid=lucs.subjectid\r\n        LEFT JOIN attendees attrm ON attrm.appointmentid=a.appointmentid AND attrm.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people rm ON rm.personid=attrm.personid\r\n        LEFT JOIN peoplegroups pg ON pg.groupid<=10 AND pg.personid=a.personid\r\nWHERE a.appointmentid=@appid\r\nORDER BY pg.groupid,a.AttendeeID", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
					bool flag2;
					result = this.GetNextTestFromReader(dataReader, out flag2, batchDecryptor);
				}
			}
			return result;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00068260 File Offset: 0x00066460
		public IList<AccommodationData> LoadAccommodationsByTest(int AppointmentId, out int PersonId, out int LuCourseId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId)
			};
			PersonId = 0;
			LuCourseId = 0;
			IList<AccommodationData> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @lucid int = COALESCE((SELECT TOP 1 lucourseid FROM appointmentcourses WHERE appointmentid=@appid),0)\r\nSELECT\tDISTINCT @lucid AS lucourseid,at.personid,dbo.AccommodationsCourseOrTemplate(at.personid,@lucid) AS courseortemplate\r\nINTO #t1\r\nFROM accommodationstest at \r\nWHERE at.AppointmentId=@appid\r\n\r\nSELECT  DISTINCT   ac.lucourseid,m.personid, m.appointmentid, m.ControlID, dc.controlcaption, \r\nCASE \tWHEN (dc.controlcode=520 AND (dc.setting3=0 OR dc.setting3=2))\r\n\t\t\t\tTHEN (SELECT lookuptext FROM lookuplists WHERE lookuplistid=(m.valint / power(2,(SELECT COUNT(orderid) FROM splitstrings2(dc.controlcaption,'.'))))-1)\r\n\tWHEN ((dc.controlcode=3 OR dc.controlcode=703 OR dc.controlcode=520) AND dc.setting3=0)\r\n\t\tTHEN (SELECT ll.lookuptext FROM lookuplists ll WHERE lookuplistid=m.valint)\r\n\tWHEN (((dc.controlcode=1 OR dc.controlcode=701) AND dc.setting3=1) OR ((dc.controlcode=3 OR dc.controlcode=703 OR dc.controlcode=520) AND dc.setting3=-1))\r\n\t\tTHEN (NULL)\r\n\tWHEN (((dc.controlcode=1 OR dc.controlcode=701) AND dc.setting3=0) OR ((dc.controlcode=3 OR dc.controlcode=703 OR dc.controlcode=520) AND dc.setting3=1))\r\n\t\tTHEN (SELECT CAST(m.valbytes AS varchar(8000)))\r\n\tWHEN (dc.controlcode=2 OR dc.controlcode=4)\r\n\t\tTHEN ('True')\r\n\tWHEN ((dc.controlcode=6 OR dc.controlcode=702))\r\n\t\tTHEN (SELECT CAST(m.valdate AS varchar(8000)))\r\n\tWHEN (dc.controlcode=14 AND dc.setting4=1) -- radiogroup\r\n\t\tTHEN (SELECT controlcaption FROM dynamiccontrols WHERE controlid=m.valint)\r\n\tWHEN (dc.controlcode = 14) THEN\r\n                          (SELECT     ll.lookuptext\r\n                            FROM          lookuplists ll\r\n                            WHERE      lookuplistid = m.valint)\r\n\tELSE\r\n\t\t(NULL)\r\n\tEND AS valtext,\r\nm.valint, \r\nm.valbytes, \r\nm.valdate, \r\nm.valimage,\r\ndc.setting1,dc.setting2,dc.setting3,dc.setting4,dc.defaultvalue,dc.controlcode,\r\nCASE WHEN (((dc.controlcode=1 OR dc.controlcode=701) AND dc.setting3=1) OR ((dc.controlcode=3 OR dc.controlcode=703 OR dc.controlcode=520) AND dc.setting3=-1)) THEN 1\r\nELSE 0 END AS valbytesisencrypted\r\n,ad.[offline],ad.expirydate,ad.altlongdescription,ad.note,ad.recommendedbutdeclined,ad.rationale\r\n,CAST(NULL AS DateTime) AS sessiondateentered,ad.recommendedbutdeclineddetail\r\n,a.showOnLetter,dc.uniqueid\r\nINTO #accommodationstestdata2\r\nFROM         \r\n(\r\n\tSELECT \tat.personid,at.controlid,at.appointmentid,at.whoselected,at.datemodified,at.valbytes,at.valdate,at.valint,NULL AS valimage\r\n\tFROM \taccommodationstest at\r\n\tWHERE at.appointmentid=@appid\r\n) AS m\r\n\tLEFT JOIN dynamiccontrols dc ON dc.controlid=m.controlid\r\n\tLEFT JOIN appointmentcourses ac ON ac.appointmentid=m.appointmentid\r\n\tLEFT JOIN accommodationdata ad ON ad.personid=m.personid AND ad.courseid=COALESCE((SELECT TOP 1 courseortemplate FROM #t1 WHERE personid=m.personid AND lucourseid=ac.lucourseid),0)\r\n\tLEFT JOIN Accommodations a ON a.ControlID=m.controlid \r\n \r\nSELECT  DISTINCT at.AppointmentId\r\n\t\t,ad2.DataID,at.controlcaption,at.controlcode,at.controlid\r\n\t\t,at.setting1,at.setting2,at.setting3,at.setting4,at.defaultvalue\r\n\t\t,COALESCE(at.valtext,ad2.valtext,'') AS valtext,\r\n\t\tCOALESCE(at.valbytes,ad2.valbytes) AS valbytes,\r\n\t\tCOALESCE(at.valdate,ad2.valdate) AS valdate,\r\n\t\tCOALESCE(at.valint,ad2.valint) AS valint,\r\n\t\tat.valimage,\r\n\t\tCOALESCE(at.valbytesisencrypted,ad2.valbytesisencrypted) AS valbytesisencrypted,\r\n\t\tdc.setting4string,dc.defaultvaluestring,dc.mask,dc.controlgroup,at.lucourseid,at.personid,dsc.ordernum,dc.uniqueid,\r\n\t\tad2.showonletter AS showonletter1,acc.showonemail,acc.showonreport,\r\n        acc.extratime,acc.isalone,acc.needscomputer,acc.needsReaderScribe,acc.availableInAllRooms,acc.isgroup,acc.tapedexams,acc.other,acc.enlarged,\r\n        acc.longdescription,acc.shortcode\r\nFROM\t#accommodationstestdata2 at LEFT JOIN dynamiccontrols dc ON dc.controlid=at.controlid\r\n\t\tLEFT JOIN accommodationdata ad2 ON ad2.PersonID=at.personid \r\n\t\t\tAND ad2.ControlID=at.controlid \r\n\t\t\t--AND ad2.courseid=dbo.AccommodationsCourseOrTemplate(at.personid,at.lucourseid)\r\n\t\t\tAND ad2.courseid=COALESCE((SELECT TOP 1 courseortemplate FROM #t1 WHERE personid=at.personid AND lucourseid=at.lucourseid),0)\r\n        LEFT JOIN dynamicscreencontrols dsc ON dsc.screennum=4 AND dsc.controlid=at.controlid\r\n\t\tLEFT JOIN accommodations acc ON acc.controlID=at.controlid\r\nWHERE\tat.AppointmentId=@appid \r\nORDER BY dsc.ordernum,at.controlcaption\r\n\r\nDROP TABLE #accommodationstestdata2\r\nDROP TABLE #t1", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					AccommodationsDAO accommodationsDAO = new AccommodationsDAO(this.OpContext);
					List<AccommodationData> list = new List<AccommodationData>();
					while (dataReader.Read())
					{
						int num = (dataReader["dataid"] is DBNull) ? 0 : ((int)dataReader["dataid"]);
						int num2 = (dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"]);
						int num3 = (dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]);
						bool flag2 = num2 > 0;
						if (flag2)
						{
							LuCourseId = num2;
						}
						bool flag3 = num3 > 0;
						if (flag3)
						{
							PersonId = num3;
						}
						bool flag4 = num > 0;
						AccommodationData accommodationData;
						if (flag4)
						{
							accommodationData = accommodationsDAO.GetAccommodationDataFromRecord(dataReader);
						}
						else
						{
							int num4 = (dataReader["controlcode"] is DBNull) ? 0 : ((int)dataReader["controlcode"]);
							eControlCode eControlCode = (eControlCode)(Enum.IsDefined(typeof(eControlCode), num4) ? num4 : 0);
							string text = PeopleDAO.ReaderContainsColumn(dataReader, "valtext") ? dataReader["valtext"].ToString().Trim() : "";
							AccommodationData accommodationData2 = new AccommodationData();
							AccommodationData accommodationData3 = accommodationData2;
							DynamicData dynamicData = new DynamicData();
							dynamicData.Field = DynamicFieldDAO.GetFieldFromRecord(dataReader);
							DynamicData dynamicData2 = dynamicData;
							object value;
							if (eControlCode != eControlCode.CheckBox && eControlCode != eControlCode.AccommodationCheckBox)
							{
								value = string.Join(": ", (from g in new string[]
								{
									dataReader["controlcaption"].ToString().Trim(),
									text
								}
								where g.Length > 0
								select g).ToArray<string>());
							}
							else
							{
								value = true;
							}
							dynamicData2.Value = value;
							accommodationData3.Data = dynamicData;
							accommodationData = accommodationData2;
						}
						bool flag5 = accommodationData != null;
						if (flag5)
						{
							list.Add(accommodationData);
						}
					}
					bool flag6 = PersonId < 1;
					if (flag6)
					{
						parameters = new DbParameter[]
						{
							databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId)
						};
						using (IDataReader dataReader2 = databaseLayer.ExecuteQueryReader("SELECT a.appointmentid,att.personid,COALESCE(e.lucourseid,sc.lucourseid) AS lucourseid\r\nFROM appointments a LEFT JOIN attendees att ON att.appointmentid=a.appointmentid AND att.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n    LEFT JOIN exams e ON e.examid=a.examid\r\n    LEFT JOIN appointmentcourses sc ON sc.appointmentid=a.appointmentid\r\nWHERE a.appointmentid=@appid", parameters))
						{
							bool flag7 = dataReader2 != null && dataReader2.Read();
							if (flag7)
							{
								PersonId = ((dataReader2["personid"] is DBNull) ? PersonId : ((int)dataReader2["personid"]));
								LuCourseId = ((dataReader2["lucourseid"] is DBNull) ? LuCourseId : ((int)dataReader2["lucourseid"]));
							}
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000685C4 File Offset: 0x000667C4
		public List<AccommodationForTest> LoadTestAccommodations(int AppointmentId, int PersonId, int LuCourseId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId),
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@lucid", DbType.Int32, LuCourseId)
			};
			List<AccommodationForTest> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("DECLARE @courseortemplate int = COALESCE((SELECT dbo.AccommodationsCourseOrTemplate(@pid,@lucid)),0)\r\n\r\nSELECT @pid AS personid,@lucid AS lucourseid,at.AppointmentId\r\n\t\t,0 AS DataID,at.controlcaption,at.controlcode,at.controlid\r\n\t\t,at.setting1,at.setting2,at.setting3,at.setting4,at.defaultvalue\r\n\t\t,COALESCE(at.valtext,ad2.valtext) AS valtext,\r\n\t\tCOALESCE(at.valbytes,ad2.valbytes) AS valbytes,\r\n\t\tCOALESCE(at.valdate,ad2.valdate) AS valdate,\r\n\t\tCOALESCE(at.valint,ad2.valint) AS valint,\r\n\t\tat.valimage,\r\n\t\tCOALESCE(at.valbytesisencrypted,ad2.valbytesisencrypted),\r\n\t\tCAST(1 AS bit) AS UseForTest,\r\n        dc.setting4string,dc.defaultvaluestring,dc.mask,dc.controlgroup\r\nFROM\taccommodationstestdata at LEFT JOIN dynamiccontrols dc ON dc.controlid=at.controlid\r\n\t\tLEFT JOIN accommodationdata ad2 ON ad2.PersonID=at.personid AND ad2.courseid=at.lucourseid AND ad2.ControlID=at.controlid\r\nWHERE\tat.AppointmentId=@appid \r\nUNION\r\nSELECT\tad.PersonID,ad.courseid,@appid AS appointmentid\r\n\t\t,ad.DataID,ad.controlcaption,ad.controlcode,ad.controlid\r\n\t\t,ad.setting1,ad.setting2,ad.setting3,ad.setting4,ad.defaultvalue\r\n\t\t,ad.valtext,ad.valbytes,ad.valdate,ad.valint,NULL AS valimage,ad.valbytesisencrypted \r\n\t\t,CAST(0 AS bit) AS UseForTest\r\n\t\t,dc.setting4string,dc.defaultvaluestring,dc.mask,dc.controlgroup\r\nFROM\taccommodationdata ad LEFT JOIN dynamiccontrols dc ON dc.controlid=ad.controlid\r\nWHERE\tad.PersonID=@pid AND ad.courseid=@courseortemplate\r\n\t\tAND ad.[offline]=0 \r\n\t\tAND (ad.expirydate IS NULL OR ad.expirydate > GETDATE() )\r\n\t\tAND (ad.showonletter & 2 = 2)", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IList<AccommodationForTest> list = new List<AccommodationForTest>();
					while (dataReader.Read())
					{
						AccommodationForTest item = this.GetAccommodationForTestFromReader(dataReader);
						bool flag2 = item != null && list.FirstOrDefault((AccommodationForTest ii) => ii.DynamicFieldData.Field.ControlId == item.DynamicFieldData.Field.ControlId) == null;
						if (flag2)
						{
							list.Add(item);
						}
					}
					this.dynamicDataDao.MergeDynamicDataIntoUniqueControlIds<AccommodationForTest>(list);
					result = this.ConsolodateApprovedAccommodationsAndTestAccommodations(list.ToList<AccommodationForTest>());
				}
			}
			return result;
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000686EC File Offset: 0x000668EC
		public List<Test> LoadClassTestDefinitionBookings(int ExamId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@examid", DbType.Int32, ExamId)
			};
			List<Test> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,a.subject AS subtitle,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,pg.groupid,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour,\r\n        a.examid,e.dateentered,e.lucourseid AS examcourselucourseid,e.description,e.dateoftest,e.visible,e.usercomment,e.testduration,e.lastmodified,e.wholastmodified,\r\n        e.typecode,e.extendedproperties,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,\r\n        e.instructoracknowledged,e.filename,\r\n        ac.appointmentcourseid,ac.lucourseid AS studentcourselucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS examcoursestartdate,luc.enddate AS examcourseenddate,luc.duration AS examcourseduration,luc.term AS examcourseterm,luc.subjectid AS examcoursesubjectid,\r\n        lucd.altlookupstring AS examcoursesubject,luc.course AS examcoursecourse,luc.[section] AS examcoursesection,luc.timeofday AS examcoursetimeofday,\r\n        lucs.startdate AS studentcoursestartdate,lucs.enddate AS studentcourseenddate,lucs.duration AS studentcourseduration,lucs.term AS studentcourseterm,\r\n        lucs.subjectid AS studentcoursesubjectid,lucds.altlookupstring AS studentcoursesubject,lucs.course AS studentcoursecourse,\r\n        lucs.[section] AS studentcoursesection,lucs.timeofday AS studentcoursetimeofday,\r\n        rm.firstname AS roomfirstname,rm.student_no AS roomstudent_no,rm.lastname AS roomlastname,attrm.personid AS roompersonid,a.totalbreakminutes AS breaktimeminutes\r\nFROM\tapps a LEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\n        LEFT JOIN exams e ON e.examid=a.examid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n        LEFT JOIN lucourses lucs ON lucs.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucds ON lucds.lucoursedataid=lucs.subjectid\r\n        LEFT JOIN attendees attrm ON attrm.appointmentid=a.appointmentid AND attrm.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people rm ON rm.personid=attrm.personid\r\n        LEFT JOIN peoplegroups pg ON pg.groupid<=10 AND pg.personid=a.personid\r\nWHERE a.examid=@examid\r\n        AND a.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n        AND a.cancelled=0\r\nORDER BY a.appointmentid,pg.groupid,a.attendeeid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = !dataReader.Read();
					if (flag2)
					{
						result = new List<Test>();
					}
					else
					{
						result = this.GetTestsFromRecords(dataReader);
					}
				}
			}
			return result;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00068778 File Offset: 0x00066978
		public static void CreateTestExamInfo(int AppointmentId, BasicAppointmentTestExamInfo TestExamInfo, OperationContext opContext, DbTransaction transaction = null)
		{
			bool flag = TestExamInfo == null;
			if (!flag)
			{
				TestBookingDAO.CreateTestExamInfo(AppointmentId, TestExamInfo.ExamId, (TestExamInfo.Course == null) ? 0 : TestExamInfo.Course.LuCourseId, new DateTime?(TestExamInfo.ClassStartDateTime), new DateTime?(TestExamInfo.ClassEndDateTime), TestExamInfo.TestNote, TestExamInfo.StudentNote, opContext);
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000687D8 File Offset: 0x000669D8
		public static void CreateTestExamInfo(int AppointmentId, int ExamId, int LuCourseId, DateTime? StudentClassStartDateTime, DateTime? StudentClassEndDateTime, string TestNote, string StudentNote, OperationContext opContext)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, (opContext != null) ? opContext.TenantId : null);
			bool flag = ExamId > 0;
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
					databaseLayer.GetParameter("@examid", DbType.Int32, ExamId)
				};
				databaseLayer.ExecuteNonQuery("DECLARE @classStart DATETIME, @classEnd DATETIME\r\nDECLARE @lucid int\r\nIF @originalstartdatetime IS NULL OR @originalenddatetime IS NULL\r\nBEGIN\r\n    SET @classStart=(SELECT TOP 1 dateoftest FROM exams WHERE examid=@examid)\r\n    SET @classEnd=(SELECT TOP 1 DATEADD(n,testduration,dateoftest) AS edt FROM exams WHERE examid=@examid)\r\nEND\r\nELSE\r\n    SET @classStart=@originalstartdatetime\r\n    SET @classEnd=@originalenddatetime\r\nBEGIN\r\nEND\r\n\r\nSET @lucid=(SELECT TOP 1 lucourseid FROM exams WHERE examid=@examid)\r\n\r\nIF EXISTS(SELECT appointmentcourseid FROM appointmentcourses WHERE appointmentid=@appid)\r\nBEGIN\r\n    UPDATE appointmentcourses SET originalstartdatetime=@classStart,originalenddatetime=@classEnd,testnote=@testnote,studentnote=@studentnote \r\n    WHERE appointmentid=@appid\r\nEND\r\nELSE \r\n    INSERT INTO appointmentcourses (appointmentid,lucourseid,originalstartdatetime,originalenddatetime,testnote,studentnote)\r\n    VALUES (@appid,@lucid,@classStart,@classEnd,@testnote,@studentnote)\r\n\r\nSET @appointmentcourseid=(SELECT TOP 1 appointmentcourseid FROM appointmentcourses WHERE appointmentid=@appid)", parameters);
				parameters = new DbParameter[]
				{
					databaseLayer.GetOutputParameter("@appointmentcourseid", DbType.Int32, 0),
					databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
					databaseLayer.GetParameter("@examid", DbType.Int32, ExamId),
					databaseLayer.GetParameter("@originalstartdatetime", DbType.DateTime, (StudentClassStartDateTime != null) ? StudentClassStartDateTime.Value : DBNull.Value),
					databaseLayer.GetParameter("@originalenddatetime", DbType.DateTime, (StudentClassEndDateTime != null) ? StudentClassEndDateTime.Value : DBNull.Value),
					databaseLayer.GetParameter("@testnote", DbType.Binary, string.IsNullOrEmpty(TestNote) ? DBNull.Value : databaseLayer.Encryption.Encrypt(TestNote)),
					databaseLayer.GetParameter("@studentnote", DbType.Binary, string.IsNullOrEmpty(StudentNote) ? DBNull.Value : databaseLayer.Encryption.Encrypt(StudentNote))
				};
				databaseLayer.ExecuteNonQuery("IF @lucid IS NULL OR @lucid<1\r\nBEGIN\r\n    DELETE FROM appointmentcourses WHERE appointmentid=@appid\r\n    SET @appointmentcourseid=0\r\nEND\r\nELSE\r\nBEGIN\r\n    IF EXISTS(SELECT appointmentcourseid FROM appointmentcourses WHERE appointmentid=@appid)\r\n        UPDATE appointmentcourses SET lucourseid=@lucid,originalstartdatetime=@originalstartdatetime,originalenddatetime=@originalenddatetime,testnote=@testnote,studentnote=@studentnote \r\n        WHERE appointmentid=@appid\r\n    ELSE \r\n        INSERT INTO appointmentcourses (appointmentid,lucourseid,originalstartdatetime,originalenddatetime,testnote,studentnote)\r\n        VALUES (@appid,@lucid,@originalstartdatetime,@originalenddatetime,@testnote,@studentnote)\r\n\r\n    SET @appointmentcourseid=(SELECT TOP 1 appointmentcourseid FROM appointmentcourses WHERE appointmentid=@appid)\r\nEND", parameters);
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00068948 File Offset: 0x00066B48
		public void UpdateClassTestDefinitionSpecific(int ExamId, TestForEditClassDefinitionSpecific info)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamId),
				databaseLayer.GetParameter("@lastmodified", DbType.DateTime, DateTime.Now),
				databaseLayer.GetParameter("@usercomment", DbType.String, string.IsNullOrEmpty(info.TestDeliveredMessage) ? DBNull.Value : info.TestDeliveredMessage.Trim()),
				databaseLayer.GetParameter("@typecode", DbType.StringFixedLength, (info.ExamType == eClassTestType.Unknown) ? DBNull.Value : ((info.ExamType == eClassTestType.FinalExam) ? "F" : "N")),
				databaseLayer.GetParameter("@extendedproperties", DbType.String, info.ExternalExamId ?? ""),
				databaseLayer.GetParameter("@privatenote", DbType.String, info.ClassPrivateNote ?? ""),
				databaseLayer.GetParameter("@filename", DbType.String, info.Location ?? "")
			};
			databaseLayer.ExecuteNonQuery("UPDATE exams SET  lastmodified=@lastmodified,usercomment=@usercomment,\r\n                    typecode=CASE WHEN @typecode IS NULL THEN typecode ELSE @typecode END,extendedproperties=@extendedproperties,privatenote=@privatenote,filename=@filename\r\nWHERE examid=@examid", parameters);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00068A74 File Offset: 0x00066C74
		public void CreateTestBookingSpecific(int AppointmentId, int LuCourseId, TestForEditBookingSpecific info)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetOutputParameter("@appointmentcourseid", DbType.Int32, 0),
				databaseLayer.GetOutputParameter("@examid", DbType.Int32, 0),
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@lucourseid", DbType.Int32, LuCourseId),
				databaseLayer.GetParameter("@updateoriginaldatetime", DbType.Boolean, true),
				databaseLayer.GetParameter("@originalstartdatetime", DbType.DateTime, (info.StudentReportedClassStartTime != null) ? info.StudentReportedClassStartTime.Value : DBNull.Value),
				databaseLayer.GetParameter("@originalenddatetime", DbType.DateTime, (info.StudentReportedClassEndTime != null) ? info.StudentReportedClassEndTime.Value : DBNull.Value),
				databaseLayer.GetParameter("@testnote", DbType.Binary, string.IsNullOrEmpty(info.AccommodationsForTestCachedList) ? DBNull.Value : databaseLayer.Encryption.Encrypt(info.AccommodationsForTestCachedList.Trim())),
				databaseLayer.GetParameter("@studentnote", DbType.Binary, string.IsNullOrEmpty(info.BookingNote) ? DBNull.Value : databaseLayer.Encryption.Encrypt(info.BookingNote.Trim())),
				databaseLayer.GetParameter("@instructoracknowledgevalue", DbType.Int32, info.InstructorAcknowledgedOnline ? 1 : 0),
				databaseLayer.GetParameter("@instructoracknowledgedate", DbType.DateTime, (info.InstructorAcknowledgeDate != null) ? info.InstructorAcknowledgeDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@privatenote2", DbType.String, (info.PrivateNote ?? "").Trim()),
				databaseLayer.GetParameter("@testpickedupdate", DbType.DateTime, (info.TestPickedUpDate != null) ? info.TestPickedUpDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@testpickedupnote", DbType.String, (info.TestPickedUpNote ?? "").Trim()),
				databaseLayer.GetParameter("@examstatuslookupid", DbType.Int32, (info.ExamStatusLookupId > 0) ? info.ExamStatusLookupId : DBNull.Value)
			};
			databaseLayer.ExecuteNonQuery("IF NOT EXISTS(SELECT appointmentcourseid FROM appointmentcourses WHERE appointmentid=@appid)\r\nBEGIN\r\n    DECLARE @id0 int\r\n    SET @id0=CASE WHEN @examstatuslookupid IS NULL THEN NULL ELSE (SELECT TOP 1 examstatuslookupid FROM examstatuslookup WHERE examstatuslookupid=@examstatuslookupid) END;\r\n\r\n    INSERT INTO appointmentcourses (appointmentid,lucourseid,originalstartdatetime,originalenddatetime,testnote,studentnote,instructoracknowledgevalue,instructoracknowledgedate,privatenote2,\r\n            testpickedupdate,testpickedupnote,examstatuslookupid)\r\n    VALUES (@appid,@lucourseid,@originalstartdatetime,@originalenddatetime,@testnote,@studentnote,@instructoracknowledgevalue,@instructoracknowledgedate,@privatenote2,\r\n            @testpickedupdate,@testpickedupnote,@id0)\r\n    \r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS appointmentcourseid\r\nEND\r\nELSE \r\nBEGIN\r\nDECLARE @id int\r\nSET @id=CASE WHEN NOT @examstatuslookupid IS NULL THEN @examstatuslookupid ELSE (SELECT TOP 1 examstatuslookupid FROM appointmentcourses WHERE appointmentid=@appid) END;\r\n\r\nUPDATE appointmentcourses SET \r\n    originalstartdatetime=CASE WHEN @updateoriginaldatetime=1 THEN @originalstartdatetime ELSE originalstartdatetime END,\r\n    originalenddatetime=CASE WHEN @updateoriginaldatetime=1 THEN @originalenddatetime ELSE originalenddatetime END,\r\n    testnote=@testnote,studentnote=@studentnote,\r\n    instructoracknowledgevalue=@instructoracknowledgevalue,instructoracknowledgedate=@instructoracknowledgedate,privatenote2=@privatenote2,\r\n    testpickedupdate=@testpickedupdate,testpickedupnote=@testpickedupnote,examstatuslookupid=@id\r\nWHERE appointmentid=@appid\r\nSELECT appointmentcourseid FROM appointmentcourses WHERE appointmentid=@appid\r\nEND", parameters);
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00068D04 File Offset: 0x00066F04
		public void UpdateTestBookingSpecific(int AppointmentId, TestForEditBookingSpecific info)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@updateoriginaldatetime", DbType.Boolean, info.UpdateStudentReportedClassTime),
				databaseLayer.GetParameter("@originalstartdatetime", DbType.DateTime, (info.StudentReportedClassStartTime != null) ? info.StudentReportedClassStartTime.Value : DBNull.Value),
				databaseLayer.GetParameter("@originalenddatetime", DbType.DateTime, (info.StudentReportedClassEndTime != null) ? info.StudentReportedClassEndTime.Value : DBNull.Value),
				databaseLayer.GetParameter("@testnote", DbType.Binary, string.IsNullOrEmpty(info.AccommodationsForTestCachedList) ? DBNull.Value : databaseLayer.Encryption.Encrypt(info.AccommodationsForTestCachedList.Trim())),
				databaseLayer.GetParameter("@studentnote", DbType.Binary, string.IsNullOrEmpty(info.BookingNote) ? DBNull.Value : databaseLayer.Encryption.Encrypt(info.BookingNote.Trim())),
				databaseLayer.GetParameter("@instructoracknowledgevalue", DbType.Int32, info.InstructorAcknowledgedOnline ? 1 : 0),
				databaseLayer.GetParameter("@instructoracknowledgedate", DbType.DateTime, (info.InstructorAcknowledgeDate != null) ? info.InstructorAcknowledgeDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@privatenote2", DbType.String, (info.PrivateNote ?? "").Trim()),
				databaseLayer.GetParameter("@testpickedupdate", DbType.DateTime, (info.TestPickedUpDate != null) ? info.TestPickedUpDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@testpickedupnote", DbType.String, (info.TestPickedUpNote ?? "").Trim()),
				databaseLayer.GetParameter("@examstatuslookupid", DbType.Int32, (info.ExamStatusLookupId != 0) ? ((info.ExamStatusLookupId > 0) ? info.ExamStatusLookupId : 0) : DBNull.Value)
			};
			databaseLayer.ExecuteNonQuery("DECLARE @id int\r\nSET @id=CASE WHEN NOT @examstatuslookupid IS NULL THEN @examstatuslookupid ELSE (SELECT TOP 1 examstatuslookupid FROM appointmentcourses WHERE appointmentid=@appid) END;\r\n\r\nUPDATE appointmentcourses SET \r\n    originalstartdatetime=CASE WHEN @updateoriginaldatetime=1 THEN @originalstartdatetime ELSE originalstartdatetime END,\r\n    originalenddatetime=CASE WHEN @updateoriginaldatetime=1 THEN @originalenddatetime ELSE originalenddatetime END,\r\n    testnote=@testnote,studentnote=@studentnote,\r\n    instructoracknowledgevalue=@instructoracknowledgevalue,instructoracknowledgedate=@instructoracknowledgedate,privatenote2=@privatenote2,\r\n    testpickedupdate=@testpickedupdate,testpickedupnote=@testpickedupnote,examstatuslookupid=@id\r\nWHERE appointmentid=@appid", parameters);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00068F68 File Offset: 0x00067168
		public IList<MailMergeTestBooking> LoadTestBookingMailMergeInfoByDate(DateTime Date, bool ExcludeCancelled, IList<int> AppTypeIdsToExclude)
		{
			DbParameter[] array = new DbParameter[4];
			array[0] = this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, Date.Date);
			array[1] = this.DatabaseManager.GetParameter("@enddate", DbType.Date, Date.Date.AddDays(1.0).AddMinutes(-1.0));
			int num = 2;
			DatabaseLayer databaseManager = this.DatabaseManager;
			string pName = "@excludeapptypeids";
			DbType pType = DbType.String;
			object value;
			if (AppTypeIdsToExclude != null)
			{
				value = string.Join(",", AppTypeIdsToExclude.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray());
			}
			else
			{
				value = "";
			}
			array[num] = databaseManager.GetParameter(pName, pType, value);
			array[3] = this.DatabaseManager.GetParameter("@showcancelled", DbType.Boolean, !ExcludeCancelled);
			DbParameter[] parameters = array;
			IList<MailMergeTestBooking> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT DISTINCT a.appointmentid,a.examid,att.personid,ac.lucourseid,a.startdate \r\nFROM    appointments a LEFT JOIN attendees att ON att.appointmentid=a.appointmentid AND att.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\nWHERE   a.startdate>=@startdate AND a.startdate<=@enddate\r\n        AND NOT a.examid IS NULL AND a.examid>0\r\n        AND (@showcancelled=1 OR a.cancelled=0)\r\n        AND (@excludeapptypeids='' OR a.apptypeid IN (SELECT orderid AS apptypeid FROM splitorderids(@excludeapptypeids,',')))\r\nORDER BY a.startdate", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<MailMergeTestBooking> list = new List<MailMergeTestBooking>();
					while (dataReader.Read())
					{
						list.Add(new MailMergeTestBooking
						{
							AppointmentId = ((dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"])),
							ExamId = ((dataReader["examid"] is DBNull) ? 0 : ((int)dataReader["examid"])),
							LuCourseId = ((dataReader["lucourseid"] is DBNull) ? 0 : ((int)dataReader["lucourseid"])),
							PersonId = ((dataReader["personid"] is DBNull) ? 0 : ((int)dataReader["personid"]))
						});
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0006917C File Offset: 0x0006737C
		public IList<Test> LoadTestsByAppointmentIds(IList<int> AppointmentIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@appointmentids", DbType.String, string.Join(",", AppointmentIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IList<Test> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,a.subject AS subtitle,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,pg.groupid,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour,\r\n        a.examid,e.dateentered,e.lucourseid AS examcourselucourseid,e.description,e.dateoftest,e.visible,e.usercomment,e.testduration,e.lastmodified,e.wholastmodified,\r\n        e.typecode,e.extendedproperties,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,\r\n        e.instructoracknowledged,e.filename,\r\n        ac.appointmentcourseid,ac.lucourseid AS studentcourselucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS examcoursestartdate,luc.enddate AS examcourseenddate,luc.duration AS examcourseduration,luc.term AS examcourseterm,luc.subjectid AS examcoursesubjectid,\r\n        lucd.altlookupstring AS examcoursesubject,luc.course AS examcoursecourse,luc.[section] AS examcoursesection,luc.timeofday AS examcoursetimeofday,\r\n        lucs.startdate AS studentcoursestartdate,lucs.enddate AS studentcourseenddate,lucs.duration AS studentcourseduration,lucs.term AS studentcourseterm,\r\n        lucs.subjectid AS studentcoursesubjectid,lucds.altlookupstring AS studentcoursesubject,lucs.course AS studentcoursecourse,\r\n        lucs.[section] AS studentcoursesection,lucs.timeofday AS studentcoursetimeofday,\r\n        rm.firstname AS roomfirstname,rm.student_no AS roomstudent_no,rm.lastname AS roomlastname,attrm.personid AS roompersonid,a.totalbreakminutes AS breaktimeminutes\r\nFROM\tapps a LEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\n        LEFT JOIN exams e ON e.examid=a.examid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n        LEFT JOIN lucourses lucs ON lucs.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucds ON lucds.lucoursedataid=lucs.subjectid\r\n        LEFT JOIN attendees attrm ON attrm.appointmentid=a.appointmentid AND attrm.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people rm ON rm.personid=attrm.personid\r\n        LEFT JOIN peoplegroups pg ON pg.groupid<=10 AND pg.personid=a.personid\r\nWHERE a.appointmentid IN (SELECT orderid AS appointmentid FROM splitorderids(@appointmentids,','))\r\nORDER BY a.appointmentid,pg.groupid,a.attendeeid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = !dataReader.Read();
					if (flag2)
					{
						result = new List<Test>();
					}
					else
					{
						result = this.GetTestsFromRecords(dataReader);
					}
				}
			}
			return result;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0006923C File Offset: 0x0006743C
		public IList<BasicTest> LoadBasicTestsByAppointmentIds(IList<int> AppointmentIds)
		{
			DbParameter[] array = new DbParameter[1];
			array[0] = this.DatabaseManager.GetParameter("@appointmentids", DbType.String, string.Join(",", AppointmentIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			DbParameter[] parameters = array;
			IList<BasicTest> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT    a.appointmentid,a.apptypeid,at.[description] AS apptypedescription,at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n            a.appcode,a.startdate,a.enddate,a.[subject] AS subtitle,a.location,a.groupcode,a.cancelled,a.islocked,a.ishidden,\r\n            att.personid,p.firstname,p.middlename,p.lastname,p.student_no,\r\n            a.examid,e.dateoftest,e.testduration,e.typecode,e.lucourseid AS examcourselucourseid,\r\n            luc.startdate AS examcoursestartdate,luc.enddate AS examcourseenddate,luc.duration AS examcourseduration,luc.term AS examcourseterm,\r\n            luc.subjectid AS examcoursesubjectid,lucd.altlookupstring AS examcoursesubject,luc.course AS examcoursecourse,luc.[section] AS examcoursesection,\r\n            luc.timeofday AS examcoursetimeofday\r\nFROM        appointments a LEFT JOIN appointmenttypes at ON at.apptypeid=a.apptypeid\r\n            LEFT JOIN appointmenttypegroups atg ON atg.appointmenttypegroupid=at.appointmenttypegroupid\r\n            LEFT JOIN attendees att ON att.appointmentid=a.appointmentid AND att.personid IN (SELECT personid FROM peoplegroups WHERE groupid=1)\r\n            LEFT JOIN people p ON p.personid=att.personid\r\n            LEFT JOIN exams e ON e.examid=a.examid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE       a.appointmentid IN (SELECT orderid AS appointmentid FROM splitorderids(@appointmentids,','))\r\nORDER BY    a.appointmentid,att.personid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					result = TestBookingDAO.GetBasicTestsFromReader(dataReader, this.OpContext);
				}
			}
			return result;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x000692EC File Offset: 0x000674EC
		internal static IList<BasicTest> GetBasicTestsFromReader(IDataReader reader, OperationContext opContext)
		{
			BasicTest basicTest = null;
			List<BasicTest> list = new List<BasicTest>();
			while (reader.Read())
			{
				int num = (int)reader["appointmentid"];
				bool flag = basicTest == null || basicTest.AppointmentId != num;
				if (flag)
				{
					basicTest = BaseAppointmentDAO.GetMainBaseBasicAppointment<BasicTest>(reader, opContext);
					basicTest.ExamId = ((reader["examid"] is DBNull) ? 0 : ((int)reader["examid"]));
					bool flag2 = reader["dateoftest"] != DBNull.Value;
					if (flag2)
					{
						basicTest.StartDateTime = (DateTime)reader["dateoftest"];
						int num2 = (reader["testduration"] is DBNull) ? 0 : ((int)reader["testduration"]);
						basicTest.EndDateTime = basicTest.StartDateTime.AddMinutes((double)num2);
					}
					basicTest.CourseBase = LookupCourseDAO.GetCourseBaseFromReader("examcourse", reader);
					string s = reader["typecode"].ToString().Trim().ToUpper();
					basicTest.ExamType = s.GetClassTestTypeFromString();
					list.Add(basicTest);
				}
				bool flag3 = basicTest.Student == null;
				if (flag3)
				{
					int num3 = (reader["personid"] is DBNull) ? 0 : ((int)reader["personid"]);
					bool flag4 = num3 > 0;
					if (flag4)
					{
						basicTest.Student = PeopleDAO.GetPersonFromReader("", reader, opContext, null);
					}
				}
			}
			return list;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00069490 File Offset: 0x00067690
		internal static ExamStatus GetExamStatusFromRecord(IDataReader record)
		{
			bool flag = record["examstatuslookupid"] is DBNull;
			ExamStatus result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new ExamStatus
				{
					ExamStatusLookupId = (int)record["examstatuslookupid"],
					ColourArgB = ((record["colourargb"] is DBNull) ? 0 : ((int)record["colourargb"])),
					Title = record["title"].ToString()
				};
			}
			return result;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00069520 File Offset: 0x00067720
		public IList<ExamStatus> LoadAllExamStatuses()
		{
			IList<ExamStatus> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT examstatuslookupid,title,colourargb FROM examstatuslookup WHERE isactive=1 ORDER BY ordernum,title"))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<ExamStatus> list = new List<ExamStatus>();
					while (dataReader.Read())
					{
						ExamStatus examStatusFromRecord = TestBookingDAO.GetExamStatusFromRecord(dataReader);
						bool flag2 = examStatusFromRecord != null;
						if (flag2)
						{
							list.Add(examStatusFromRecord);
						}
					}
					list.Sort((ExamStatus g1, ExamStatus g2) => g1.Title.CompareTo(g2.Title));
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000695C4 File Offset: 0x000677C4
		public void AddTestAccommodations(int AppointmentId, int PersonId, IList<int> ControlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			foreach (int num in ControlIds)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
					databaseLayer.GetParameter("@pid", DbType.Int32, PersonId),
					databaseLayer.GetParameter("@cid", DbType.Int32, num),
					databaseLayer.GetParameter("@whoami", DbType.Int32, this.OpContext.WhoAmI)
				};
				databaseLayer.ExecuteNonQuery("DECLARE @examid int, @lucid int\r\nSET @examid = (SELECT TOP 1 examid FROM appointments WHERE appointmentid=@appid)\r\n\r\nIF NOT @examid IS NULL AND NOT EXISTS(SELECT examid FROM accommodationstest WHERE examid=@examid AND appointmentid=@appid AND personid=@pid AND controlid=@cid)\r\nBEGIN\r\n    SET @lucid = (SELECT TOP 1 lucourseid FROM exams WHERE examid=@examid )\r\n\r\n    DECLARE @courseortemplate int = (SELECT dbo.AccommodationsCourseOrTemplate(@pid,@lucid))\r\n\r\n    SELECT valbytes,valint,valdate INTO #t1 FROM accommodationdata WHERE personid=@pid AND controlid=@cid AND courseid=COALESCE(@courseortemplate,0) ORDER BY courseid;\r\n    DECLARE @valbytes varbinary(8000);\r\n    DECLARE @valint int;\r\n    DECLARE @valdate datetime;\r\n    SET @valbytes=(SELECT TOP 1 valbytes FROM #t1);\r\n    SET @valint=(SELECT TOP 1 valint FROM #t1);\r\n    SET @valdate=(SELECT TOP 1 valdate FROM #t1);\r\n\r\n    INSERT INTO accommodationstest (examid,personid,controlid,whoselected,datemodified,appointmentid,valbytes,valint,valdate)\r\n        VALUES (@appid,@pid,@cid,@whoami,getdate(),@appid,@valbytes,@valint,@valdate);\r\n\r\n    DROP TABLE #t1\r\nEND", parameters);
			}
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0006969C File Offset: 0x0006789C
		public void RemoveTestAccommodations(int AppointmentId, int PersonId, IList<int> ControlIds)
		{
			DbParameter[] array = new DbParameter[3];
			array[0] = this.DatabaseManager.GetParameter("@appid", DbType.Int32, AppointmentId);
			array[1] = this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId);
			array[2] = this.DatabaseManager.GetParameter("@cids", DbType.String, string.Join(",", ControlIds.ToList<int>().ConvertAll<string>((int g) => g.ToString()).ToArray()));
			DbParameter[] parameters = array;
			this.DatabaseManager.ExecuteNonQuery("DECLARE @examid int\r\nSET @examid = (SELECT TOP 1 COALESCE(examid,@appid) AS examid FROM appointments WHERE appointmentid=@appid) \r\n    DELETE FROM accommodationstest \r\n        WHERE   (examid=@examid OR examid=@appid) AND appointmentid=@appid AND personid=@pid \r\n                AND controlid IN (SELECT orderid AS controlid FROM splitorderids(@cids,','))", parameters);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00069748 File Offset: 0x00067948
		public void UpdateBreakTime(int AppointmentId, int BreakTimeMinutes)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@mins", DbType.Int32, BreakTimeMinutes)
			};
			databaseLayer.ExecuteNonQuery("UPDATE appointments SET totalbreakminutes=@mins WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000697B0 File Offset: 0x000679B0
		public void SetAppointmentExamId(int AppointmentId, int ExamId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, AppointmentId),
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamId)
			};
			databaseLayer.ExecuteNonQuery("UPDATE appointments SET examid=@examid WHERE appointmentid=@appid", parameters);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00069818 File Offset: 0x00067A18
		public IList<Test> LoadTestsByStudent(int PersonId, DateTime StartDate, DateTime EndDate, bool HideCancelled)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@pid", DbType.Int32, PersonId),
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0)),
				this.DatabaseManager.GetParameter("@hidecancelled", DbType.Boolean, HideCancelled)
			};
			IList<Test> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("SELECT\ta.appointmentid,a.AppTypeID,a.[description] AS apptypedescription,a.subject AS subtitle,\r\n        at.appointmenttypegroupid,atg.title AS apptypegrouptitle,\r\n\t\ta.appCode,a.startDate,a.endDate,a.[subject],a.location,\r\n\t\ta.cancelled,a.isLocked,a.isHidden,a.groupCode,a.extraattendeescount,\r\n\t\ta.AttendeeID,a.PersonID,a.firstName,a.lastName,a.student_no,a.miscCode,a.noShow,pg.groupid,\r\n\t\tat.isCourse,at.isWorkshop,at.defaultColour,\r\n\t\tam.memoText,am.isEncrypted,\r\n\t\ta.whoadded AS wbpersonid,pwb.firstName AS wbfirstname,pwb.lastName AS wblastname,pwb.student_no AS wbstudent_no,\r\n\t\ta.dateAdded AS datebooked,a.overrideColour,a.actualstarttime,a.actualendtime,\r\n\t\tacr.cancelreasonid,cr.cancelreasongroupname,cr.cancelreasontitle,\r\n\t\tacr.cancelledbypersonid AS cbpersonid,pcb.firstName AS cbfirstname,pcb.lastName AS cblastname,pcb.student_no AS cbstudent_no,\r\n\t\tacr.cancelleddate,acr.cancelreasontext,\r\n\t\tast.appointmentshowtimeasid,ast.showtimeastitle,ast.extraiconid,ast.showtimeascolour,\r\n        a.examid,e.dateentered,e.lucourseid AS examcourselucourseid,e.description,e.dateoftest,e.visible,e.usercomment,e.testduration,e.lastmodified,e.wholastmodified,\r\n        e.typecode,e.extendedproperties,e.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,\r\n        e.instructoracknowledged,e.filename,\r\n        ac.appointmentcourseid,ac.lucourseid AS studentcourselucourseid,ac.originalstartdatetime,ac.originalenddatetime,ac.testnote,ac.studentnote,ac.instructoracknowledgevalue,ac.instructoracknowledgedate,\r\n        ac.privatenote2,ac.testpickedupdate AS stestpickedupdate,ac.testpickedupnote AS stestpickedupnote,ac.extendedproperties AS sextendedproperties,\r\n        luc.startdate AS examcoursestartdate,luc.enddate AS examcourseenddate,luc.duration AS examcourseduration,luc.term AS examcourseterm,luc.subjectid AS examcoursesubjectid,\r\n        lucd.altlookupstring AS examcoursesubject,luc.course AS examcoursecourse,luc.[section] AS examcoursesection,luc.timeofday AS examcoursetimeofday,\r\n        lucs.startdate AS studentcoursestartdate,lucs.enddate AS studentcourseenddate,lucs.duration AS studentcourseduration,lucs.term AS studentcourseterm,\r\n        lucs.subjectid AS studentcoursesubjectid,lucds.altlookupstring AS studentcoursesubject,lucs.course AS studentcoursecourse,\r\n        lucs.[section] AS studentcoursesection,lucs.timeofday AS studentcoursetimeofday,\r\n        rm.firstname AS roomfirstname,rm.student_no AS roomstudent_no,rm.lastname AS roomlastname,attrm.personid AS roompersonid,a.totalbreakminutes AS breaktimeminutes\r\nFROM\tapps a LEFT JOIN AppointmentTypes at ON at.apptypeid=a.AppTypeID \r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID \r\n\t\tLEFT JOIN AppointmentMemos am ON am.AppointmentID=a.AppointmentID \r\n\t\tLEFT JOIN People pwb ON pwb.PersonID=a.whoadded \r\n\t\tLEFT JOIN AppointmentCancelledReason acr ON acr.appointmentid=a.AppointmentID\r\n\t\tLEFT JOIN CancelReason cr ON cr.cancelreasonid=acr.cancelreasonid \r\n\t\tLEFT JOIN People pcb ON pcb.PersonID=acr.cancelledbypersonid \r\n        LEFT JOIN AppointmentShowTimeAs ast ON ast.extraiconid=a.appcode\r\n        LEFT JOIN exams e ON e.examid=a.examid\r\n        LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n        LEFT JOIN appointmentcourses ac ON ac.appointmentid=a.appointmentid\r\n        LEFT JOIN lucourses lucs ON lucs.lucourseid=ac.lucourseid\r\n        LEFT JOIN lucoursedata lucds ON lucds.lucoursedataid=lucs.subjectid\r\n        LEFT JOIN attendees attrm ON attrm.appointmentid=a.appointmentid AND attrm.personid IN (SELECT personid FROM peoplegroups WHERE groupid=3)\r\n        LEFT JOIN people rm ON rm.personid=attrm.personid\r\n        LEFT JOIN peoplegroups pg ON pg.groupid<=10 AND pg.personid=a.personid\r\nWHERE (NOT a.examid IS NULL OR a.apptypeid IN (SELECT apptypeid FROM appointmenttypes WHERE iscourse=1))\r\n        AND a.startdate BETWEEN @startdate AND @enddate\r\n        AND (@hidecancelled=0 OR a.cancelled=0)\r\n        AND a.personid=@pid\r\nORDER BY a.appointmentid,pg.groupid,a.AttendeeID", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					bool flag2 = !dataReader.Read();
					if (flag2)
					{
						result = new List<Test>();
					}
					else
					{
						result = this.GetTestsFromRecords(dataReader);
					}
				}
			}
			return result;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00069928 File Offset: 0x00067B28
		public IList<int> LoadAppointmentIdsByExamId(int ExamId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, ExamId)
			};
			IList<int> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT appointmentid FROM appointments WHERE NOT examid IS NULL AND examid=@examid", parameters))
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
						int num = (dataReader["appointmentid"] is DBNull) ? 0 : ((int)dataReader["appointmentid"]);
						bool flag2 = num > 0 && !list.Contains(num);
						if (flag2)
						{
							list.Add(num);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00069A0C File Offset: 0x00067C0C
		private StudentWritingTest GetStudentWritingTestFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null || record["personid"] is DBNull || record["appointmentid"] is DBNull;
			StudentWritingTest result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object obj = record["InstructorAcknowledgeDate"];
				int num = (record["instructorAcknowledgevalue"] is DBNull) ? 0 : ((int)record["instructorAcknowledgevalue"]);
				result = new StudentWritingTest
				{
					Student = PeopleDAO.GetPersonFromReader("", record, this.OpContext, batchDecryptor),
					ExamId = ((record["examid"] is DBNull) ? 0 : ((int)record["examid"])),
					AppointmentId = (int)record["appointmentid"],
					AppointmentType = AppointmentTypeDAO.GetAppTypeFromReader("", record),
					StartDateTime = (DateTime)record["startdate"],
					EndDateTime = (DateTime)record["enddate"],
					IsCancelled = (!(record["cancelled"] is DBNull) && (bool)record["cancelled"]),
					Location = ((record["location"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["location"])),
					SubTitle = ((record["subtitle"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["subtitle"])),
					IsTentative = (((record["appcode"] is DBNull) ? 0 : ((int)record["appcode"])) == -1),
					InstructorAcknowledgedValue = ((obj is DBNull) ? null : new bool?(num == 1)),
					InstructorAcknowledgedDate = ((obj is DBNull) ? null : new DateTime?((DateTime)obj))
				};
			}
			return result;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00069C48 File Offset: 0x00067E48
		public InstructorAcknowledgedStudent LoadInstructorAcknowledgedStudent(int appId, IDictionary<int, string> acknowledgeValueTitles)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, appId)
			};
			InstructorAcknowledgedStudent result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT instructoracknowledgevalue,instructoracknowledgedate FROM appointmentcourses WHERE appointmentid=@appid", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					int num = (dataReader["instructoracknowledgevalue"] is DBNull) ? 0 : ((int)dataReader["instructoracknowledgevalue"]);
					result = new InstructorAcknowledgedStudent
					{
						DateAcknowledged = ((dataReader["instructoracknowledgedate"] is DBNull) ? null : new DateTime?((DateTime)dataReader["instructoracknowledgedate"])),
						SelectedIndex = num,
						SelectedText = (acknowledgeValueTitles.ContainsKey(num) ? acknowledgeValueTitles[num] : "?")
					};
				}
			}
			return result;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00069D6C File Offset: 0x00067F6C
		public IList<StudentWritingTest> LoadStudentsWritingExam(int examId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, examId)
			};
			IList<StudentWritingTest> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("SELECT\tapp.AppointmentID,app.examid,app.startDate,app.endDate,app.cancelled,app.appCode,app.Location,app.[Subject] AS SubTitle,\r\n\t\tapp.AppTypeID,at.[description] AS apptypedescription,at.isCourse,at.isWorkshop,at.defaultColour,at.isActive AS apptypeisactive,at.defaultcolour,\r\n\t\tatg.appointmenttypegroupid,atg.title AS apptypegrouptitle,atg.[description] AS gidstr,\r\n\t\tatt.personid,p.firstname,p.middlename,p.lastname,p.student_no,p.isactive,\r\n\t\tac.InstructorAcknowledgeDate,ac.InstructorAcknowledgeValue\r\nFROM\tAppointments app LEFT JOIN AppointmentTypes at ON at.AppTypeID=app.AppTypeID\r\n\t\tLEFT JOIN AppointmentTypeGroups atg ON atg.AppointmentTypeGroupID=at.appointmentTypeGroupID\r\n\t\tLEFT JOIN Attendees att ON att.AppointmentID=app.AppointmentID\r\n\t\tLEFT JOIN PeopleGroups pg ON pg.GroupID=1 AND pg.PersonID=att.PersonID\r\n\t\tLEFT JOIN People p ON p.personid=pg.personid \r\n\t\tLEFT JOIN AppointmentCourses ac ON ac.AppointmentId=app.AppointmentID\r\nWHERE\tapp.examid=@examid \r\n        AND app.cancelled=0\r\n\t\tAND NOT pg.groupid IS NULL", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					List<StudentWritingTest> list = new List<StudentWritingTest>();
					while (dataReader.Read())
					{
						StudentWritingTest studentWritingTestFromRecord = this.GetStudentWritingTestFromRecord(dataReader, batchDecryptor);
						bool flag2 = studentWritingTestFromRecord != null;
						if (flag2)
						{
							list.Add(studentWritingTestFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x040005D5 RID: 1493
		private DynamicDataDAO _dynamicDataDao;

		// Token: 0x040005D7 RID: 1495
		private DynamicFormsDAO dd;

		// Token: 0x040005D8 RID: 1496
		private LookupCourseDAO ld;

		// Token: 0x040005D9 RID: 1497
		private PeopleDAO pd;

		// Token: 0x040005DA RID: 1498
		private TestBookingDAO td;
	}
}
