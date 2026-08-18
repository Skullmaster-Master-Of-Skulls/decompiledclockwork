using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.DAO.Impl.Adapters;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x0200014B RID: 331
	public class TestExamBookingViewDAO : ITestExamBookingViewDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x000642AD File Offset: 0x000624AD
		public TestExamBookingViewDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x000642BF File Offset: 0x000624BF
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x000642C7 File Offset: 0x000624C7
		public OperationContext OpContext { get; set; }

		// Token: 0x060009A8 RID: 2472 RVA: 0x000642D0 File Offset: 0x000624D0
		private ClassTestDefinitionSmall GetClassTestDefinitionSmallFromRecordWithExtendedInfo(IDataReader record, int[] controlIds, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null;
			ClassTestDefinitionSmall result;
			if (flag)
			{
				result = null;
			}
			else
			{
				ClassTestDefinitionSmall classTestDefinitionSmallFromRecord = this.GetClassTestDefinitionSmallFromRecord(record);
				bool flag2 = classTestDefinitionSmallFromRecord == null;
				if (flag2)
				{
					result = null;
				}
				else
				{
					this.AddExtendedInfoToClassTestDefinitionSmall(classTestDefinitionSmallFromRecord, record, controlIds, batchDecryptor);
					while (record.Read())
					{
						int num = (record["examid"] is DBNull) ? 0 : ((int)record["examid"]);
						bool flag3 = num != classTestDefinitionSmallFromRecord.ExamId;
						if (flag3)
						{
							break;
						}
						this.AddExtendedInfoToClassTestDefinitionSmall(classTestDefinitionSmallFromRecord, record, controlIds, batchDecryptor);
					}
					result = classTestDefinitionSmallFromRecord;
				}
			}
			return result;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00064368 File Offset: 0x00062568
		private void AddExtendedInfoToClassTestDefinitionSmall(ClassTestDefinitionSmall exam, IDataReader record, int[] controlIds, IBatchDecryptor batchDecryptor)
		{
			int num = (record == null || record["controlid"] is DBNull) ? 0 : ((int)record["controlid"]);
			bool flag = num < 1;
			if (!flag)
			{
				int num2 = Array.IndexOf<int>(controlIds, num);
				bool flag2 = num2 < 0;
				if (!flag2)
				{
					string text = (record["valtext"] is DBNull) ? "" : ((string)record["valtext"]);
					bool flag3 = text.Length > 0;
					if (flag3)
					{
						exam[num2] = text;
					}
					else
					{
						bool flag4 = !(record["valbytesisencrypted"] is DBNull) && (bool)record["valbytesisencrypted"];
						bool flag5 = !flag4;
						if (!flag5)
						{
							byte[] array = (record["valbytes"] is DBNull) ? ((record["valimage"] is DBNull) ? null : ((byte[])record["valimage"])) : ((byte[])record["valbytes"]);
							bool flag6 = array == null || array.Length < 1;
							if (!flag6)
							{
								exam[num2] = batchDecryptor.Decrypt(array);
							}
						}
					}
				}
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x000644B4 File Offset: 0x000626B4
		private ClassTestDefinitionSmall GetClassTestDefinitionSmallFromRecord(IDataReader record)
		{
			bool flag = record == null || record["examid"] is DBNull;
			ClassTestDefinitionSmall result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = record.ContainsColumn("Test Type") ? record["Test Type"].ToString().Trim().ToLower() : "";
				ClassTestDefinitionSmall classTestDefinitionSmall = new ClassTestDefinitionSmall
				{
					ExamId = this.GetIntFromRecord(record, "examid"),
					LuCourseId = this.GetIntFromRecord(record, "lucourseid"),
					TestDuration = this.GetIntFromRecord(record, "testduration", 0),
					CourseDescription = record["coursedescription"].ToString(),
					DateOfTest = this.GetDateTimeFromRecord(record, "dateoftest"),
					TestStartTime = this.GetDateTimeFromRecord(record, "teststarttime"),
					TestEndTime = this.GetDateTimeFromRecord(record, "testendtime"),
					InstructorContactedDate = this.GetDateTimeNullableFromRecord(record, "instructorcontacteddate"),
					InstructorContactedNote = record["instructorcontactednote"].ToString(),
					TestPickedUpDate = this.GetDateTimeNullableFromRecord(record, "testpickedupdate"),
					TestPickedUpNote = record["testpickedupnote"].ToString(),
					TestType = (text.Contains("midterm") ? eClassTestType.Midterm : (text.Contains("final") ? eClassTestType.FinalExam : eClassTestType.Unknown))
				};
				result = classTestDefinitionSmall;
			}
			return result;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00064628 File Offset: 0x00062828
		private UnbookedStudentsSmall GetUnbookedStudentSmallFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null || record["personid"] is DBNull;
			UnbookedStudentsSmall result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new UnbookedStudentsSmall
				{
					PersonId = this.GetIntFromRecord(record, "personid"),
					ExamId = this.GetIntFromRecord(record, "examid"),
					LuCourseId = this.GetIntFromRecord(record, "lucourseid"),
					TestDuration = this.GetIntFromRecord(record, "testduration"),
					StudentEmail = ((record["valbytes"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["valbytes"])),
					CourseDescription = record["coursedescription"].ToString(),
					DateOfTest = this.GetDateTimeFromRecord(record, "dateoftest"),
					TestStartTime = this.GetDateTimeFromRecord(record, "teststarttime"),
					TestEndTime = this.GetDateTimeFromRecord(record, "testendtime"),
					LastName = ((record["lastname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"])),
					FirstName = ((record["firstname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["firstname"])),
					MiddleName = "",
					Student_no = ((record["student_no"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["student_no"]))
				};
			}
			return result;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x000647E8 File Offset: 0x000629E8
		private TestBookingFull GetTestBookingFullFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			bool flag = record == null || record["appointmentid"] is DBNull;
			TestBookingFull result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (int)record["appointmentid"];
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					TestBookingFull testBookingFull = (TestBookingFull)this.GetTestBookingSmallFromRecord<TestBookingFull>(record, batchDecryptor);
					bool flag3 = testBookingFull == null;
					if (flag3)
					{
						result = null;
					}
					else
					{
						testBookingFull.InvigilatorPid = this.GetIntFromRecord(record, "invigilatorpid", 0);
						testBookingFull.SittingId = this.GetIntFromRecord(record, "sittingid", 0);
						testBookingFull.AlternateContactId = this.GetIntFromRecord(record, "alternatecontactid", 0);
						testBookingFull.InstructorSubmitted = this.GetBoolFromRecord(record, "instructorsubmitted", false);
						testBookingFull.DateLetterIssued = this.GetDateTimeNullableFromRecord(record, "dateletterissued");
						testBookingFull.CourseStartDate = this.GetDateTimeNullableFromRecord(record, "coursestartdate");
						testBookingFull.CourseEndDate = this.GetDateTimeNullableFromRecord(record, "courseenddate");
						testBookingFull.Department = record["department"].ToString();
						testBookingFull.DepartmentEmail = record["departmentemail"].ToString();
						testBookingFull.DepartmentCode = record["departmentcode"].ToString();
						testBookingFull.PrimaryInstructor = record["instructor"].ToString();
						testBookingFull.PrimaryInstructorEmail = record["instructoremail"].ToString();
						testBookingFull.PrimaryInstructorPhone = record["instructorphone"].ToString();
						testBookingFull.ExamAccommodations = ((record["examaccommodations"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["examaccommodations"]));
						testBookingFull.AccommodationGroups = ((record["accommodationgroups"] is DBNull) ? string.Empty : batchDecryptor.Decrypt((byte[])record["accommodationgroups"]));
						testBookingFull.TotalBreakMinutes = this.GetIntFromRecord(record, "totalbreakminutes", 0);
						testBookingFull.AssignedAdvisor = record["counsellorfirst"].ToString() + " " + record["counsellorlast"].ToString();
						testBookingFull.AssignedAdvisorFirstName = record["counsellorfirst"].ToString();
						testBookingFull.AssignedAdvisorLastName = record["counsellorlast"].ToString();
						testBookingFull.Invigilator = record["invigilatorfirstname"].ToString() + " " + record["invigilatorlastname"].ToString();
						testBookingFull.InvigilatorFirstName = record["invigilatorfirstname"].ToString();
						testBookingFull.InvigilatorLastName = record["invigilatorlastname"].ToString();
						testBookingFull.DateAdded = this.GetDateTimeNullableFromRecord(record, "dateadded");
						testBookingFull.WhoBookedFirst = record["whobookedfirst"].ToString();
						testBookingFull.WhoBookedLast = record["whobookedlast"].ToString();
						testBookingFull.WhoBooked = (testBookingFull.WhoBookedFirst + " " + testBookingFull.WhoBookedLast).Trim();
						testBookingFull.StudentReportedClassDate = this.GetDateTimeNullableFromRecord(record, "studentreportedclassdate");
						testBookingFull.StudentReportedClassStartTime = this.GetDateTimeNullableFromRecord(record, "studentreportedclassstarttime");
						testBookingFull.StudentReportedClassEndTime = this.GetDateTimeNullableFromRecord(record, "studentreportedclassendtime");
						testBookingFull.AlternateContact = record["altname"].ToString();
						testBookingFull.AlternateContactEmail = record["altemail"].ToString();
						testBookingFull.AlternateContactPhone = record["altphone"].ToString();
						testBookingFull.AlternateContactUsername = record["altusername"].ToString();
						testBookingFull.AlternateContactPermissionLevel = this.GetIntFromRecord(record, "altpermissionlevel", 0);
						testBookingFull.InstructorAcknowledgedOnline = record["instructoracknowledgedonline"].ToString();
						testBookingFull.InstructorAcknowledgedDate = this.GetDateTimeNullableFromRecord(record, "instructoracknowledgedate");
						testBookingFull.StudentReportedSameAsDefinition = this.GetBoolFromRecord(record, "studentreportedsameasdefinition", false);
						testBookingFull.InstructorContactedNote = record["instructorcontactednote"].ToString();
						testBookingFull.InstructorContactedDate = this.GetDateTimeNullableFromRecord(record, "instructorcontacteddate");
						testBookingFull.TestPickedUpDate = this.GetDateTimeNullableFromRecord(record, "testpickedupdate");
						testBookingFull.TestPickedUpNote = record["testpickedupnote"].ToString();
						testBookingFull.PrivateNote2 = record["privatenote2"].ToString();
						testBookingFull.Sitting = record["sitting"].ToString();
						testBookingFull.SittingRoomFirst = record["sitting_room_first"].ToString();
						testBookingFull.SittingRoomLast = record["sitting_room_last"].ToString();
						testBookingFull.SittingRoom = testBookingFull.SittingRoomFirst + " " + testBookingFull.SittingRoomLast;
						testBookingFull.SittingLocation = record["sitting_location"].ToString();
						testBookingFull.SittingInvigilatorFirst = record["sitting_invigilator_first"].ToString();
						testBookingFull.SittingInvigilatorLast = record["sitting_invigilator_last"].ToString();
						testBookingFull.SittingInvigilator = testBookingFull.SittingInvigilatorFirst + " " + testBookingFull.SittingInvigilatorLast;
						result = testBookingFull;
					}
				}
			}
			return result;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00064D2C File Offset: 0x00062F2C
		private TestBookingSmall GetTestBookingSmallFromRecord<T>(IDataReader record, IBatchDecryptor batchDecryptor) where T : TestBookingSmall
		{
			bool flag = record == null || record["appointmentid"] is DBNull;
			TestBookingSmall result;
			if (flag)
			{
				result = null;
			}
			else
			{
				int num = (int)record["appointmentid"];
				bool flag2 = num < 1;
				if (flag2)
				{
					result = null;
				}
				else
				{
					T t = Activator.CreateInstance<T>();
					t.Student = PeopleDAO.GetPersonFromReader("", record, this.OpContext, batchDecryptor);
					t.Subject = record["subject"].ToString();
					t.Course = record["course"].ToString();
					t.TimeOfDay = record["timeofday"].ToString();
					t.Section = record["section"].ToString();
					t.Classroom = record["classroom"].ToString();
					t.Campus = record["campus"].ToString();
					t.AppointmentId = num;
					t.ExamId = this.GetIntFromRecord(record, "examid");
					t.AppTypeId = this.GetIntFromRecord(record, "apptypeid");
					t.LuCourseId = this.GetIntFromRecord(record, "lucourseid");
					t.RoomPid = this.GetIntFromRecord(record, "roompid");
					t.AppCode = this.GetIntFromRecord(record, "appcode");
					t.ExamStatusLookupId = this.GetIntFromRecord(record, "examstatuslookupid");
					t.Status = this.GetStringFromRecord(record, "status");
					t.ScheduledDate = this.GetDateTimeNullableFromRecord(record, "scheduleddate");
					t.ScheduledStartTime = this.GetDateTimeNullableFromRecord(record, "scheduledstarttime");
					t.ScheduledEndTime = this.GetDateTimeNullableFromRecord(record, "scheduledendtime");
					t.Description = record["description"].ToString();
					t.Room = record["room"].ToString();
					t.Location = record["location"].ToString();
					t.Memo = ((record["memotext"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["memotext"]));
					t.ClassStartTime = this.GetDateTimeFromRecord(record, "classstartdatetime", DateTime.MinValue);
					t.ClassEndTime = this.GetDateTimeFromRecord(record, "classenddatetime", DateTime.MinValue);
					t.ClassDate = t.ClassStartTime.Date;
					t.ClassLocation = record["classlocation"].ToString();
					t.Cancelled = this.GetBoolFromRecord(record, "cancelled", false);
					t.NoShow = this.GetBoolFromRecord(record, "noshow", false);
					t.ActualDate = this.GetDateTimeNullableFromRecord(record, "actualdate");
					t.ActualStartTime = this.GetDateTimeNullableFromRecord(record, "actualstarttime");
					t.ActualEndTime = this.GetDateTimeNullableFromRecord(record, "actualendtime");
					bool flag3 = t.ActualStartTime != null && t.ScheduledStartTime != null && t.ScheduledEndTime != null;
					if (flag3)
					{
						TimeSpan value = t.ScheduledEndTime.Value - t.ScheduledStartTime.Value;
						t.ProjectedActualEndTime = new DateTime?(t.ActualStartTime.Value.Add(value));
					}
					t.TestDelivered = record["TestDelivered"].ToString();
					t.TestWasDelivered = (t.TestDelivered != null && t.TestDelivered.Trim().Length > 0);
					t.ExamStatus = record["examstatus"].ToString();
					t.ColourArgB = this.GetIntFromRecord(record, "colourargb", 0);
					result = t;
				}
			}
			return result;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00065204 File Offset: 0x00063404
		private DateTime GetDateTimeFromRecord(IDataReader record, string name)
		{
			return this.GetDateTimeFromRecord(record, name, DateTime.MinValue);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00065224 File Offset: 0x00063424
		private DateTime GetDateTimeFromRecord(IDataReader record, string name, DateTime defaultValue)
		{
			DateTime? dateTimeNullableFromRecord = this.GetDateTimeNullableFromRecord(record, name);
			return (dateTimeNullableFromRecord != null) ? dateTimeNullableFromRecord.Value : defaultValue;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00065254 File Offset: 0x00063454
		private DateTime? GetDateTimeNullableFromRecord(IDataReader record, string name)
		{
			object obj = record[name];
			bool flag = obj is DBNull;
			DateTime? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new DateTime?((DateTime)record[name]);
			}
			return result;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00065298 File Offset: 0x00063498
		private bool GetBoolFromRecord(IDataReader record, string name)
		{
			return this.GetBoolFromRecord(record, name, false);
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x000652B4 File Offset: 0x000634B4
		private bool GetBoolFromRecord(IDataReader record, string name, bool defaultValue)
		{
			bool? boolNullableFromRecord = this.GetBoolNullableFromRecord(record, name);
			return (boolNullableFromRecord != null) ? boolNullableFromRecord.Value : defaultValue;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x000652E4 File Offset: 0x000634E4
		private bool? GetBoolNullableFromRecord(IDataReader record, string name)
		{
			object obj = record[name];
			bool flag = obj is DBNull;
			bool? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new bool?((bool)obj);
			}
			return result;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00065324 File Offset: 0x00063524
		private int GetIntFromRecord(IDataReader record, string name)
		{
			return this.GetIntFromRecord(record, name, 0);
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00065340 File Offset: 0x00063540
		private int GetIntFromRecord(IDataReader record, string name, int defaultValue)
		{
			int? intNullableFromRecord = this.GetIntNullableFromRecord(record, name);
			return (intNullableFromRecord != null) ? intNullableFromRecord.Value : defaultValue;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00065370 File Offset: 0x00063570
		private int? GetIntNullableFromRecord(IDataReader record, string name)
		{
			object obj = record[name];
			bool flag = obj is DBNull;
			int? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new int?((int)obj);
			}
			return result;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x000653B0 File Offset: 0x000635B0
		private string GetStringFromRecord(IDataReader record, string name)
		{
			object obj = record[name];
			bool flag = obj is DBNull;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = ((string)obj).Trim();
			}
			return result;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x000653EC File Offset: 0x000635EC
		public IList<TestBookingFull> LoadTestsFull(DateTime? StartDate, DateTime? EndDate, bool HideCancelled, int CounsellorCid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, (StartDate != null) ? StartDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, (EndDate != null) ? EndDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@allowcancelled", DbType.Boolean, !HideCancelled),
				databaseLayer.GetParameter("@counsellorcid", DbType.Int32, CounsellorCid)
			};
			IList<TestBookingFull> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_LoadBookingsFull", parameters))
			{
				result = this.LoadTestsFull(dataReader);
			}
			return result;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000654D4 File Offset: 0x000636D4
		public IList<TestBookingSmall> LoadTestsSmall(DateTime? StartDate, DateTime? EndDate, bool HideCancelled, int CounsellorCid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, (StartDate != null) ? StartDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, (EndDate != null) ? EndDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@allowcancelled", DbType.Boolean, !HideCancelled),
				databaseLayer.GetParameter("@counsellorcid", DbType.Int32, CounsellorCid)
			};
			IList<TestBookingSmall> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_LoadBookingsSmall", parameters))
			{
				result = this.LoadTestsSmall(dataReader);
			}
			return result;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000655BC File Offset: 0x000637BC
		public IList<ClassTestDefinitionSmall> LoadClassTestDefinitionsSmall(DateTime? StartDate, DateTime? EndDate)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@sd", DbType.DateTime, (StartDate != null) ? StartDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@ed", DbType.DateTime, (EndDate != null) ? EndDate.Value : DBNull.Value)
			};
			IList<ClassTestDefinitionSmall> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("sp_TestBooking_ClassTestDefinitions", parameters))
			{
				result = this.LoadClassTestDefinitionsSmall(dataReader);
			}
			return result;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00065674 File Offset: 0x00063874
		public IList<ClassTestDefinitionSmall> LoadClassTestDefinitionsSmallWithExtendedInfo(DateTime? StartDate, DateTime? EndDate, params int[] controlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@sd", DbType.DateTime, (StartDate != null) ? StartDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@ed", DbType.DateTime, (EndDate != null) ? EndDate.Value : DBNull.Value),
				databaseLayer.GetParameter("@cids", DbType.String, (controlIds != null) ? controlIds.CommaSeparatedValuesWithoutSpace<int>() : "")
			};
			IList<ClassTestDefinitionSmall> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_ClassTestDefinitionWithExtendedInfo", parameters))
			{
				List<ClassTestDefinitionSmall> list = new List<ClassTestDefinitionSmall>();
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = list;
				}
				else
				{
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					for (;;)
					{
						ClassTestDefinitionSmall classTestDefinitionSmallFromRecordWithExtendedInfo = this.GetClassTestDefinitionSmallFromRecordWithExtendedInfo(dataReader, controlIds, batchDecryptor);
						bool flag2 = classTestDefinitionSmallFromRecordWithExtendedInfo == null;
						if (flag2)
						{
							break;
						}
						list.Add(classTestDefinitionSmallFromRecordWithExtendedInfo);
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x000657A0 File Offset: 0x000639A0
		public IList<UnbookedStudentsSmall> LoadUnbookedStudentsSmall(bool onlyShowLetterIssued)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@loaissuedrequired", DbType.Boolean, onlyShowLetterIssued)
			};
			IList<UnbookedStudentsSmall> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_UnbookedStudents", parameters))
			{
				result = this.LoadUnbookedStudentsSmall(dataReader);
			}
			return result;
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x00065818 File Offset: 0x00063A18
		public IList<TestBookingFull> LoadTestsFull(IDataReader reader)
		{
			bool flag = reader == null;
			IList<TestBookingFull> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				List<TestBookingFull> list = new List<TestBookingFull>();
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				while (reader.Read())
				{
					TestBookingFull testBookingFullFromRecord = this.GetTestBookingFullFromRecord(reader, batchDecryptor);
					bool flag2 = testBookingFullFromRecord != null;
					if (flag2)
					{
						list.Add(testBookingFullFromRecord);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00065894 File Offset: 0x00063A94
		public IList<TestBookingSmall> LoadTestsSmall(IDataReader reader)
		{
			bool flag = reader == null;
			IList<TestBookingSmall> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				List<TestBookingSmall> list = new List<TestBookingSmall>();
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				while (reader.Read())
				{
					TestBookingSmall testBookingSmallFromRecord = this.GetTestBookingSmallFromRecord<TestBookingSmall>(reader, batchDecryptor);
					bool flag2 = testBookingSmallFromRecord != null;
					if (flag2)
					{
						list.Add(testBookingSmallFromRecord);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00065910 File Offset: 0x00063B10
		public IList<ClassTestDefinitionSmall> LoadClassTestDefinitionsSmall(IDataReader reader)
		{
			bool flag = reader == null;
			IList<ClassTestDefinitionSmall> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<ClassTestDefinitionSmall> list = new List<ClassTestDefinitionSmall>();
				while (reader.Read())
				{
					ClassTestDefinitionSmall classTestDefinitionSmallFromRecord = this.GetClassTestDefinitionSmallFromRecord(reader);
					bool flag2 = classTestDefinitionSmallFromRecord != null;
					if (flag2)
					{
						list.Add(classTestDefinitionSmallFromRecord);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00065960 File Offset: 0x00063B60
		public IList<UnbookedStudentsSmall> LoadUnbookedStudentsSmall(IDataReader reader)
		{
			bool flag = reader == null;
			IList<UnbookedStudentsSmall> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
				OperationContext opContext = this.OpContext;
				DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
				List<UnbookedStudentsSmall> list = new List<UnbookedStudentsSmall>();
				IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
				while (reader.Read())
				{
					UnbookedStudentsSmall unbookedStudentSmallFromRecord = this.GetUnbookedStudentSmallFromRecord(reader, batchDecryptor);
					bool flag2 = unbookedStudentSmallFromRecord != null;
					if (flag2)
					{
						list.Add(unbookedStudentSmallFromRecord);
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x000659DC File Offset: 0x00063BDC
		public TestBookingFull LoadTestFullByAppId(int appId, int counsellorCid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, 0),
				databaseLayer.GetParameter("@counsellorcid", DbType.Int32, counsellorCid)
			};
			TestBookingFull testBookingFullFromRecord;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_LoadBookingsFullByAppId", parameters))
			{
				testBookingFullFromRecord = this.GetTestBookingFullFromRecord(dataReader, databaseLayer.Encryption.GetBatchDecryptor());
			}
			return testBookingFullFromRecord;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x00065A78 File Offset: 0x00063C78
		public TestBookingSmall LoadTestSmallByAppId(int appId, int counsellorCid)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@appid", DbType.Int32, 0),
				databaseLayer.GetParameter("@counsellorcid", DbType.Int32, counsellorCid)
			};
			TestBookingSmall testBookingSmallFromRecord;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_LoadBookingsSmallByAppId", parameters))
			{
				testBookingSmallFromRecord = this.GetTestBookingSmallFromRecord<TestBookingSmall>(dataReader, databaseLayer.Encryption.GetBatchDecryptor());
			}
			return testBookingSmallFromRecord;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x00065B14 File Offset: 0x00063D14
		public ClassTestDefinitionSmall LoadClassTestDefinitionSmallByExamId(int examId)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, examId)
			};
			ClassTestDefinitionSmall classTestDefinitionSmallFromRecord;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_ClassTestDefinitionByExamId", parameters))
			{
				classTestDefinitionSmallFromRecord = this.GetClassTestDefinitionSmallFromRecord(dataReader);
			}
			return classTestDefinitionSmallFromRecord;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x00065B8C File Offset: 0x00063D8C
		public ClassTestDefinitionSmall LoadClassTestDefinitionSmallByExamIdWithExtendedInfo(int examId, params int[] controlIds)
		{
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext = this.OpContext;
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext != null) ? opContext.TenantId : null);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@examid", DbType.Int32, examId),
				databaseLayer.GetParameter("@cids", DbType.String, (controlIds != null) ? controlIds.CommaSeparatedValuesWithoutSpace<int>() : "")
			};
			ClassTestDefinitionSmall result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_ClassTestDefinitionWithExtendedInfoByExamId", parameters))
			{
				bool flag = dataReader == null || !dataReader.Read();
				if (flag)
				{
					result = null;
				}
				else
				{
					result = this.GetClassTestDefinitionSmallFromRecordWithExtendedInfo(dataReader, controlIds, databaseLayer.Encryption.GetBatchDecryptor());
				}
			}
			return result;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00065C4C File Offset: 0x00063E4C
		private UnbookedTestExamStudent GetUnbookedTestExamStudentFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			int num = (record["personid"] is DBNull) ? 0 : ((int)record["personid"]);
			int num2 = (record["examid"] is DBNull) ? 0 : ((int)record["examid"]);
			bool flag = num < 1 || num2 < 1;
			UnbookedTestExamStudent result;
			if (flag)
			{
				result = null;
			}
			else
			{
				string text = (record["valtext"] is DBNull) ? "" : ((string)record["valtext"]);
				bool flag2 = text.Length < 1 && record["valbytes"] != DBNull.Value;
				if (flag2)
				{
					text = batchDecryptor.Decrypt((byte[])record["valbytes"]);
				}
				result = new UnbookedTestExamStudent
				{
					Student = PeopleDAO.GetPersonFromReader("", record, this.OpContext, batchDecryptor),
					ClassTest = ClassTestDefinitionDAO.GetClassTestBaseFromRecord<ClassTestBase>(record, "", batchDecryptor),
					DateLetterIssued = ((record["dateletterissued"] is DBNull) ? null : new DateTime?((DateTime)record["dateletterissued"])),
					StudentEmail = text
				};
			}
			return result;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00065DA8 File Offset: 0x00063FA8
		public IList<UnbookedTestExamStudent> LoadUnbookedTestExamStudents(bool onlyShowLetterIssued, bool ignoreTemplate)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@loaissuedrequired", DbType.Boolean, onlyShowLetterIssued),
				databaseLayer.GetParameter("@ignoretemplate", DbType.Boolean, ignoreTemplate)
			};
			IList<UnbookedTestExamStudent> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_UnbookedStudents", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<UnbookedTestExamStudent> list = new List<UnbookedTestExamStudent>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						UnbookedTestExamStudent unbookedTestExamStudentFromRecord = this.GetUnbookedTestExamStudentFromRecord(dataReader, batchDecryptor);
						bool flag2 = unbookedTestExamStudentFromRecord == null;
						if (!flag2)
						{
							list.Add(unbookedTestExamStudentFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}
	}
}
