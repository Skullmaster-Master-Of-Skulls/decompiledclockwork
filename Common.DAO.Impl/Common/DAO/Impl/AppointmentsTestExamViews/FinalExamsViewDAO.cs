using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.DAO.AppointmentsTestExamViews;
using TechnoPro.Common.DAO.Impl.LookupCourses;
using TechnoPro.Common.DAO.Impl.People;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;

namespace TechnoPro.Common.DAO.Impl.AppointmentsTestExamViews
{
	// Token: 0x02000143 RID: 323
	public class FinalExamsViewDAO : IFinalExamsViewDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x0006072C File Offset: 0x0005E92C
		public FinalExamsViewDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000950 RID: 2384 RVA: 0x0006073E File Offset: 0x0005E93E
		// (set) Token: 0x06000951 RID: 2385 RVA: 0x00060746 File Offset: 0x0005E946
		public OperationContext OpContext { get; set; }

		// Token: 0x06000952 RID: 2386 RVA: 0x00060750 File Offset: 0x0005E950
		public IList<PotentialFinalExamBooking> LoadUnbookedFinalExams(DateTime startDate, DateTime endDate, bool requiresApprovedSelfReg, bool requiresUnexpiredAccommodations, bool requiresLoaGeneratedByStaff, int accommodationExpiryControlId)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, startDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, endDate.Date),
				databaseLayer.GetParameter("@requiresApprovedSelfReg", DbType.Boolean, requiresApprovedSelfReg),
				databaseLayer.GetParameter("@requiresUnexpiredAccommodations", DbType.Boolean, requiresUnexpiredAccommodations),
				databaseLayer.GetParameter("@requiresLOAGeneratedByStaff", DbType.Boolean, requiresLoaGeneratedByStaff),
				databaseLayer.GetParameter("@expirycid", DbType.Int32, accommodationExpiryControlId)
			};
			IList<PotentialFinalExamBooking> result;
			using (IDataReader dataReader = databaseLayer.ExecuteStoredProcedureReader("sp_TestBooking_UnbookedFinalExams", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<PotentialFinalExamBooking> list = new List<PotentialFinalExamBooking>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					while (dataReader.Read())
					{
						PotentialFinalExamBooking potentialFinalExamBookingFromRecord = this.GetPotentialFinalExamBookingFromRecord(dataReader, batchDecryptor);
						bool flag2 = potentialFinalExamBookingFromRecord == null;
						if (!flag2)
						{
							list.Add(potentialFinalExamBookingFromRecord);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00060888 File Offset: 0x0005EA88
		private PotentialFinalExamBooking GetPotentialFinalExamBookingFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			int num = (record["examid"] is DBNull) ? 0 : ((int)record["examid"]);
			bool flag = num < 1;
			PotentialFinalExamBooking result;
			if (flag)
			{
				result = null;
			}
			else
			{
				DateTime examStartDateTime = (DateTime)record["dateoftest"];
				result = new PotentialFinalExamBooking
				{
					ExamId = num,
					Course = LookupCourseDAO.GetCourseBaseFromReader("", record),
					Student = PeopleDAO.GetBasicPersonFromRecord("", record, batchDecryptor),
					ExamStartDateTime = examStartDateTime,
					ExamEndDateTime = examStartDateTime.AddMinutes((double)((int)record["testduration"]))
				};
			}
			return result;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00060938 File Offset: 0x0005EB38
		public IList<FinalExamsViewLight> LoadFinalExamsLight(FinalExamsContext context)
		{
			DatabaseLayer databaseLayer = DatabaseLayerFactory.GetDatabaseLayer(eDatabaseConnectionStringName.ClockWork, this.OpContext.TenantId);
			DbParameter[] parameters = new DbParameter[]
			{
				databaseLayer.GetParameter("@startdate", DbType.DateTime, context.StartDate.Date),
				databaseLayer.GetParameter("@enddate", DbType.DateTime, context.EndDate.Date)
			};
			IList<FinalExamsViewLight> result;
			using (IDataReader dataReader = databaseLayer.ExecuteQueryReader("DECLARE @sdate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nDECLARE @edate datetime = DATEADD(D, 0, DATEDIFF(D, 0, @enddate))\r\n\r\nSELECT  e.examid,e.dateentered,e.[description],e.dateoftest,e.usercomment,e.testduration,\r\n\t\te.testpickedupdate,e.testpickedupnote,e.instructorcontacteddate,e.instructorcontactednote,e.privatenote,\r\n\t\te.instructoracknowledged,e.lastmodified,\r\n\t\te.lucourseid,luc.startdate,luc.enddate,luc.term,lucd.altLookupString AS [subject],luc.course,luc.TimeOfDay,luc.[section],\r\n        coalesce(lucd.altlookupstring,'','') + ' ' + luc.course + luc.timeofday + ' ' + luc.section + ' (' + luc.term + ')' AS CourseDescription,\r\n\t\ta.appointmentid,a.startDate AS appstartdate,a.endDate AS appenddate,a.cancelled,a.appCode,a.AppTypeID,apt.[description] AS apptypedescription,\r\n\t\tatt.PersonID,p.lastName,p.firstName,p.middleName,p.student_no,pg.GroupID,att.noshow  \r\nFROM\texams e LEFT JOIN Appointments a ON a.examid=e.examid \r\n\t\tLEFT JOIN lucourses luc ON luc.LUCourseID=e.lucourseid\r\n\t\tLEFT JOIN lucoursedata lucd ON lucd.luCourseDataID=luc.SubjectID\r\n\t\tLEFT JOIN attendees att ON att.AppointmentID=a.AppointmentID\r\n\t\tLEFT JOIN people p ON p.PersonID=att.PersonID\r\n\t\tLEFT JOIN peoplegroups pg ON pg.PersonID=p.personid\r\n\t\tLEFT JOIN AppointmentTypes apt ON apt.AppTypeID=a.AppTypeID\r\nWHERE\te.visible = 1 \r\n\t\tAND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )\r\nORDER BY e.examid,a.AppointmentID", parameters))
			{
				bool flag = dataReader == null;
				if (flag)
				{
					result = null;
				}
				else
				{
					List<FinalExamsViewLight> list = new List<FinalExamsViewLight>();
					IBatchDecryptor batchDecryptor = databaseLayer.Encryption.GetBatchDecryptor();
					FinalExamsViewLight finalExamsViewLight = null;
					while (dataReader.Read())
					{
						int num = (dataReader["examid"] is DBNull) ? 0 : ((int)dataReader["examid"]);
						bool flag2 = num < 1;
						if (!flag2)
						{
							bool flag3 = finalExamsViewLight == null || num != finalExamsViewLight.ExamId;
							if (flag3)
							{
								FinalExamsViewLight finalExamsExamLightFromRecord = this.GetFinalExamsExamLightFromRecord(dataReader, batchDecryptor);
								bool flag4 = finalExamsExamLightFromRecord == null;
								if (flag4)
								{
									continue;
								}
								list.Add(finalExamsExamLightFromRecord);
								finalExamsViewLight = finalExamsExamLightFromRecord;
							}
							this.SetFinalExamsBookingLightFromRecord(dataReader, batchDecryptor);
						}
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00060A94 File Offset: 0x0005EC94
		private FinalExamsViewLight GetFinalExamsExamLightFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
			int examId = (int)record["examid"];
			DateTime examStartDateTime = (DateTime)record["dateoftest"];
			int num = (int)record["testduration"];
			return new FinalExamsViewLight
			{
				ExamId = examId,
				CourseTitle = record["coursedescription"].ToString().Trim(),
				DateEntered = (DateTime)record["dateentered"],
				ExamStartDateTime = examStartDateTime,
				ExamEndDateTime = examStartDateTime.AddMinutes((double)num),
				DateLastModified = ((record["lastmodified"] is DBNull) ? null : new DateTime?((DateTime)record["lastmodified"])),
				TestPickedUpDate = ((record["testpickedupdate"] is DBNull) ? null : new DateTime?((DateTime)record["testpickedupdate"])),
				InstructorContactedDate = ((record["instructorcontacteddate"] is DBNull) ? null : new DateTime?((DateTime)record["instructorcontacteddate"])),
				InstructorContactedNote = ((record["instructorcontactednote"] is DBNull) ? null : ((string)record["instructorcontactednote"])),
				HasTestCopy = (!(record["usercomment"] is DBNull) && ((string)record["usercomment"]).Trim().Length > 0),
				TestCopyNote = record["usercomment"].ToString().Trim(),
				LuCourseId = ((record["lucourseid"] is DBNull) ? 0 : ((int)record["lucourseid"])),
				TestPickedUpNote = ((record["testpickedupnote"] is DBNull) ? null : ((string)record["testpickedupnote"])),
				Bookings = new List<FinalExamsViewLightBooking>()
			};
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00013135 File Offset: 0x00011335
		private void SetFinalExamsBookingLightFromRecord(IDataReader record, IBatchDecryptor batchDecryptor)
		{
		}
	}
}
