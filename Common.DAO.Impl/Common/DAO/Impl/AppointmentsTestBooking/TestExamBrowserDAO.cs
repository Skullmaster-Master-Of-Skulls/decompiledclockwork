using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsTestBooking;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestExamBrowser;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestBooking
{
	// Token: 0x0200014C RID: 332
	public class TestExamBrowserDAO : ITestExamBrowserDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x00065E7C File Offset: 0x0006407C
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x00065E84 File Offset: 0x00064084
		public OperationContext OpContext { get; set; }

		// Token: 0x060009C9 RID: 2505 RVA: 0x00065E8D File Offset: 0x0006408D
		public TestExamBrowserDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
			eDatabaseConnectionStringName csName = eDatabaseConnectionStringName.ClockWork;
			OperationContext opContext2 = this.OpContext;
			this.DatabaseManager = DatabaseLayerFactory.GetDatabaseLayer(csName, (opContext2 != null) ? opContext2.TenantId : null);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x00065EC0 File Offset: 0x000640C0
		private IBatchDecryptor BatchDecryptor
		{
			get
			{
				bool flag = this._batchDecryptor == null;
				if (flag)
				{
					this._batchDecryptor = this.DatabaseManager.Encryption.GetBatchDecryptor();
				}
				return this._batchDecryptor;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00065EFB File Offset: 0x000640FB
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00065F03 File Offset: 0x00064103
		private DatabaseLayer DatabaseManager { get; set; }

		// Token: 0x060009CD RID: 2509 RVA: 0x00065F0C File Offset: 0x0006410C
		private TestExamRow GetTestExamRowFromRecord(IDataReader record)
		{
			bool flag = record == null;
			TestExamRow result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IBatchDecryptor batchDecryptor = this.BatchDecryptor;
				result = new TestExamRow
				{
					AppointmentId = (int)record["appointmentid"],
					ExamId = ((record["examid"] is DBNull) ? 0 : ((int)record["examid"])),
					PersonId = ((record["personid"] is DBNull) ? 0 : ((int)record["personid"])),
					AppTypeId = ((record["apptypeid"] is DBNull) ? 0 : ((int)record["apptypeid"])),
					LuCourseId = ((record["lucourseid"] is DBNull) ? 0 : ((int)record["lucourseid"])),
					InvigilatorPid = ((record["invigilatorpid"] is DBNull) ? 0 : ((int)record["invigilatorpid"])),
					RoomPid = ((record["roompid"] is DBNull) ? 0 : ((int)record["roompid"])),
					AppCode = ((record["appcode"] is DBNull) ? 0 : ((int)record["appcode"])),
					AlternateContactId = ((record["alternatecontactid"] is DBNull) ? 0 : ((int)record["alternatecontactid"])),
					ExamStatusLookupId = ((record["examstatuslookupid"] is DBNull) ? 0 : ((int)record["examstatuslookupid"])),
					Status = record["status"].ToString(),
					FirstName = ((record["firstname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["firstname"])),
					LastName = ((record["lastname"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["lastname"])),
					Student_no = ((record["student_no"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["student_no"])),
					ScheduledStartTime = DateTime.Now,
					ScheduledEndTime = DateTime.Now,
					Description = record["description"].ToString(),
					Room = ((record["room"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["room"])),
					Location = ((record["location"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["location"])),
					Memo = ((record["memotext"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["memotext"])),
					ClassStartTime = DateTime.Now,
					ClassEndTime = DateTime.Now,
					Cancelled = (!(record["cancelled"] is DBNull) && Convert.ToBoolean(record["cancelled"])),
					NoShow = (!(record["noshow"] is DBNull) && Convert.ToBoolean(record["noshow"])),
					Tentative = false,
					InstructorSubmitted = true,
					DateLetterIssued = new DateTime?(DateTime.Now),
					CourseStartDate = DateTime.Now,
					CourseEndDate = DateTime.Now,
					Department = record["department"].ToString(),
					DepartmentEmail = record["departmentemail"].ToString(),
					DepartmentCode = "",
					Term = "",
					Duration = "",
					Subject = record["subject"].ToString(),
					Course = record["course"].ToString(),
					Section = record["section"].ToString(),
					TimeOfDay = record["timeofday"].ToString(),
					ClassRoom = "",
					Campus = record["campus"].ToString(),
					PrimaryInstructor = "",
					PrimaryInstructorEmail = "",
					PrimaryInstructorPhone = "",
					ExamAccommodations = "",
					AccommodationGroups = "",
					TotalBreakMinutes = 0,
					AssignedAdvisorFirstName = ((record["counsellorfirst"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["counsellorfirst"])),
					AssingedAdvisorLastName = ((record["counsellorlast"] is DBNull) ? "" : batchDecryptor.Decrypt((byte[])record["counsellorlast"])),
					AssignedAdvisorPersonId = 0,
					Invigilator = "",
					DateAdded = DateTime.Now,
					WhoBooked = "",
					WhoBookedPersonId = 0,
					ActualStartTime = new DateTime?(DateTime.Now),
					ActualEndTime = new DateTime?(DateTime.Now),
					TestDelivered = "",
					StudentReportedClassStartTime = new DateTime?(DateTime.Now),
					StudentReportedClassEndTime = new DateTime?(DateTime.Now),
					AlternateContact = "",
					AlternateContactEmail = "",
					AlternateContactPhone = "",
					AlternateContactUsername = "",
					AlternateContactPermissionLevel = 0,
					InstructorAcknowledged = "",
					InstructorAcknowledgedOnline = "",
					InstructorAcknolwedgedDate = new DateTime?(DateTime.Now),
					InstructorContactedDate = new DateTime?(DateTime.Now),
					InstructorContactedNote = "",
					TestPickedUpDate = new DateTime?(DateTime.Now),
					TestPickedUpNote = "",
					PrivateNote2 = "",
					ExamStatus = "",
					ColourArgB = 0,
					SittingId = 0,
					Sitting = "",
					SittingRoom = "",
					SittingLocation = "",
					SittingInvigilator = ""
				};
			}
			return result;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0006660C File Offset: 0x0006480C
		public IList<TestExamRow> LoadTestExamRows(DateTime StartDate, DateTime EndDate, bool HideCancelled, eTestExamColumnGroup ColumnsToLoad)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, StartDate.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, EndDate.Date.AddDays(1.0).AddMinutes(-1.0)),
				this.DatabaseManager.GetParameter("@allowcancelled", DbType.Boolean, !HideCancelled),
				this.DatabaseManager.GetParameter("@onlyappid", DbType.Int32, 0)
			};
			IList<TestExamRow> result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC LoadTestsExams @startdate,@enddate,@allowcancelled,@onlyappid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<TestExamRow> list = new List<TestExamRow>();
					while (dataReader.Read())
					{
						TestExamRow testExamRowFromRecord = this.GetTestExamRowFromRecord(dataReader);
						bool flag2 = testExamRowFromRecord != null;
						if (flag2)
						{
							list.Add(testExamRowFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00066734 File Offset: 0x00064934
		public TestExamRow LoadTestExamRow(int AppointmentId)
		{
			DbParameter[] parameters = new DbParameter[]
			{
				this.DatabaseManager.GetParameter("@startdate", DbType.DateTime, DateTime.Now.Date),
				this.DatabaseManager.GetParameter("@enddate", DbType.DateTime, DateTime.Now.Date),
				this.DatabaseManager.GetParameter("@allowcancelled", DbType.Boolean, true),
				this.DatabaseManager.GetParameter("@onlyappid", DbType.Int32, AppointmentId)
			};
			TestExamRow result;
			using (IDataReader dataReader = this.DatabaseManager.ExecuteQueryReader("EXEC LoadTestsExams @startdate,@enddate,@allowcancelled,@onlyappid", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<TestExamRow> list = new List<TestExamRow>();
					while (dataReader.Read())
					{
						TestExamRow testExamRowFromRecord = this.GetTestExamRowFromRecord(dataReader);
						bool flag2 = testExamRowFromRecord != null;
						if (flag2)
						{
							list.Add(testExamRowFromRecord);
						}
					}
					result = ((list.Count > 0) ? list[0] : null);
				}
			}
			return result;
		}

		// Token: 0x040005B2 RID: 1458
		private IBatchDecryptor _batchDecryptor;
	}
}
