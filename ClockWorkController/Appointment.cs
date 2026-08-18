using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ClockWorkWebAPI;
using ClockWorkWebAPI.TestBooking;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;

namespace ClockWorkController
{
	// Token: 0x02000004 RID: 4
	public class Appointment
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000023E4 File Offset: 0x000005E4
		public static AppointmentDTO LoadAppointment(int appId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@appid", DbType.Int32, appId)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_AppointmentByAppointmentId, parameters);
			bool flag = dataTable.Rows.Count > 0;
			AppointmentDTO result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				AppointmentDTO appointmentDTO = new AppointmentDTO
				{
					AppointmentId = appId,
					StartDateTime = (DateTime)dataRow["startdate"],
					EndDateTime = (DateTime)dataRow["enddate"],
					IsCancelled = (dataRow["cancelled"] != DBNull.Value && Convert.ToBoolean(dataRow["cancelled"])),
					AppType = new AppTypeDTO
					{
						Description = dataRow["description"].ToString(),
						AppTypeId = ((dataRow["apptypeid"] == DBNull.Value) ? 0 : ((int)dataRow["apptypeid"]))
					},
					Attendees = new List<AttendeeDTO>(),
					Icons = new List<AppointmentIconDTO>(),
					Memo = ""
				};
				result = appointmentDTO;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002538 File Offset: 0x00000738
		public static DataTable LoadPreviouslySubmittedTests(int newLucid, int cutoffNumDaysVal)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, newLucid),
				clockWork.GetParameter("@mindate", DbType.DateTime, DateTime.Now.AddDays((double)cutoffNumDaysVal).Date)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_PreviouslySubmittedTests, parameters);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000025A8 File Offset: 0x000007A8
		public static DataTable LoadPreviouslySubmittedClassTestDefinitionsByTypeCode(int newLucid, int cutoffNumDaysVal, string allowedTypeCodesCommaSeparated, string notAllowedTypeCodesCommaSeparated)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, newLucid),
				clockWork.GetParameter("@mindate", DbType.DateTime, DateTime.Now.AddDays((double)cutoffNumDaysVal).Date),
				clockWork.GetParameter("@typecodesallowed", DbType.String, allowedTypeCodesCommaSeparated),
				clockWork.GetParameter("@typecodesnotallowed", DbType.String, notAllowedTypeCodesCommaSeparated)
			};
			string qs_SelectPreviouslySubmittedClassTestDefinitionsTestsByTypeCode = QueryStorage.QS_SelectPreviouslySubmittedClassTestDefinitionsTestsByTypeCode;
			return clockWork.ExecuteQuery(qs_SelectPreviouslySubmittedClassTestDefinitionsTestsByTypeCode, parameters);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002640 File Offset: 0x00000840
		public static DataTable LoadPreviouslySubmittedClassTestDefinitions(int newLucid, int cutoffNumDaysVal, bool onlyIncludeFinalExams)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, newLucid),
				clockWork.GetParameter("@mindate", DbType.DateTime, DateTime.Now.AddDays((double)cutoffNumDaysVal).Date)
			};
			bool flag = !onlyIncludeFinalExams;
			string query;
			if (flag)
			{
				query = QueryStorage.QS_SelectPreviouslySubmittedClassTestDefinitions;
			}
			else
			{
				query = QueryStorage.QS_SelectPreviouslySubmittedRegistrarClassTestDefinitions;
			}
			return clockWork.ExecuteQuery(query, parameters);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000026C8 File Offset: 0x000008C8
		public static void CreateNewTestDefinition(PotentialTest ptest, int pid, int lucid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			TimeSpan timeSpan = ptest.Test.EndDate - ptest.Test.StartDate;
			DateTime dateTime = new DateTime(ptest.Test.StartDate.Year, ptest.Test.StartDate.Month, ptest.Test.StartDate.Day);
			DateTime dateTime2 = dateTime.AddDays(1.0).AddMinutes(-1.0);
			DbParameter[] array = new DbParameter[6];
			array[0] = clockWork.Parameter;
			array[0].ParameterName = "@pid";
			array[0].DbType = DbType.Int32;
			array[0].Value = pid;
			array[1] = clockWork.Parameter;
			array[1].ParameterName = "@lucid";
			array[1].DbType = DbType.Int32;
			array[1].Value = lucid;
			array[2] = clockWork.Parameter;
			array[2].ParameterName = "@dateoftest";
			array[2].DbType = DbType.DateTime;
			array[2].Value = ptest.Test.StartDate;
			array[3] = clockWork.Parameter;
			array[3].ParameterName = "@testduration";
			array[3].DbType = DbType.Int32;
			array[3].Value = Convert.ToInt32(timeSpan.TotalMinutes);
			array[4] = clockWork.Parameter;
			array[4].ParameterName = "@sd";
			array[4].DbType = DbType.DateTime;
			array[4].Value = dateTime;
			array[5] = clockWork.Parameter;
			array[5].ParameterName = "@ed";
			array[5].DbType = DbType.DateTime;
			array[5].Value = dateTime2;
			clockWork.ExecuteQuery(QueryStorage.QS_INSERT_NewTestDefinition, array);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000028C4 File Offset: 0x00000AC4
		public static int CreateTest(int studentPid, int rid, bool makeSureRoomIsntAlreadyBooked, DateTime startDate, DateTime endDate, DateTime classStartDate, DateTime classEndDate, int appTypeId, bool tentative, int lucid, List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsToUse, out Appointment.eCreateAppointmentFailedReason failedReason, out Exception ex, int breakMinutes, List<PrivateNote> privateNotes, FindPotentialBookingsInfo findPotentialBookingsInfo)
		{
			bool flag = privateNotes == null;
			if (flag)
			{
				privateNotes = new List<PrivateNote>();
			}
			string studentNote = string.Join("\r\n", privateNotes.ConvertAll<string>((PrivateNote pn) => pn.Note).ToArray());
			return Appointment.CreateTest(studentPid, rid, makeSureRoomIsntAlreadyBooked, startDate, endDate, classStartDate, classEndDate, appTypeId, tentative, lucid, accommodationsToUse, out failedReason, out ex, breakMinutes, studentNote, findPotentialBookingsInfo);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000293C File Offset: 0x00000B3C
		public static int CreateExam(int studentPid, int rid, bool makeSureRoomIsntAlreadyBooked, DateTime startDate, DateTime endDate, DateTime classStartDate, DateTime classEndDate, int appTypeId, bool tentative, int lucid, List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsToUse, out Appointment.eCreateAppointmentFailedReason failedReason, out Exception ex, int breakMinutes, List<PrivateNote> privateNotes, FindPotentialBookingsInfo findPotentialBookingsInfo)
		{
			bool flag = privateNotes == null;
			if (flag)
			{
				privateNotes = new List<PrivateNote>();
			}
			string studentNote = string.Join("\r\n", privateNotes.ConvertAll<string>((PrivateNote pn) => pn.Note).ToArray());
			return Appointment.CreateTestOrExam(studentPid, rid, makeSureRoomIsntAlreadyBooked, startDate, endDate, classStartDate, classEndDate, appTypeId, tentative, lucid, accommodationsToUse, out failedReason, out ex, breakMinutes, studentNote, findPotentialBookingsInfo, true);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000029B8 File Offset: 0x00000BB8
		public static int CreateTest(int studentPid, int rid, bool makeSureRoomIsntAlreadyBooked, DateTime startDate, DateTime endDate, DateTime classStartDate, DateTime classEndDate, int appTypeId, bool tentative, int lucid, List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsToUse, out Appointment.eCreateAppointmentFailedReason failedReason, out Exception ex, int breakMinutes, string studentNote, FindPotentialBookingsInfo findPotentialBookingsInfo)
		{
			return Appointment.CreateTestOrExam(studentPid, rid, makeSureRoomIsntAlreadyBooked, startDate, endDate, classStartDate, classEndDate, appTypeId, tentative, lucid, accommodationsToUse, out failedReason, out ex, breakMinutes, studentNote, findPotentialBookingsInfo, false);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000029EC File Offset: 0x00000BEC
		private static int CreateTestOrExam(int studentPid, int rid, bool makeSureRoomIsntAlreadyBooked, DateTime startDate, DateTime endDate, DateTime classStartDate, DateTime classEndDate, int appTypeId, bool tentative, int lucid, List<ClockWorkWebAPI.TestBooking.Accommodation> accommodationsToUse, out Appointment.eCreateAppointmentFailedReason failedReason, out Exception ex, int breakMinutes, string studentNote, FindPotentialBookingsInfo findPotentialBookingsInfo, bool isFinalExam)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption tripleDES = clockWork.TripleDES;
			int num = 0;
			try
			{
				int num2 = Convert.ToInt32((classEndDate - classStartDate).TotalMinutes);
				DbParameter[] array = new DbParameter[]
				{
					clockWork.GetOutputParameter("@examid", DbType.Int32, 0),
					clockWork.GetParameter("@sdate", DbType.DateTime, classStartDate.Date),
					clockWork.GetParameter("@edate", DbType.DateTime, classStartDate.Date.AddDays(1.0)),
					clockWork.GetParameter("@lucid", DbType.Int32, lucid),
					clockWork.GetParameter("@dateoftest", DbType.DateTime, classStartDate),
					clockWork.GetParameter("@testduration", DbType.Int32, num2),
					clockWork.GetParameter("@testtype", DbType.String, isFinalExam ? "F" : "N")
				};
				clockWork.ExecuteNonQuery("IF NOT EXISTS(SELECT examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sdate AND dateoftest<@edate)\r\nBEGIN\r\n    INSERT INTO exams (dateentered,whoentered,lucourseid,description,dateoftest,testduration,lastmodified,wholastmodified,visible,usercomment,typecode) VALUES (getdate(),-555,@lucid,'',@dateoftest,@testduration,getdate(),NULL,1,NULL,@testtype)\r\n    set @examid = SCOPE_IDENTITY()\r\nEND\r\nELSE\r\n    SET @examid = (SELECT TOP 1 examid FROM exams WHERE lucourseid=@lucid AND dateoftest>=@sdate AND dateoftest<@edate)", array);
				num = ((array[0].Value is DBNull) ? 0 : ((int)array[0].Value));
				bool flag = num < 1;
				if (flag)
				{
					failedReason = Appointment.eCreateAppointmentFailedReason.Unknown;
					ex = null;
					return 0;
				}
			}
			catch (Exception ex2)
			{
				ex = ex2;
				failedReason = Appointment.eCreateAppointmentFailedReason.Unknown;
				return 0;
			}
			DbTransaction dbTransaction = clockWork.BeginDbTransaction();
			int result;
			try
			{
				DbParameter[] array2 = new DbParameter[]
				{
					clockWork.GetOutputParameter("@appid", DbType.Int32, 0),
					clockWork.GetOutputParameter("@failedreason", DbType.String, 255),
					clockWork.GetParameter("@dontcareifroombooked", DbType.Boolean, !makeSureRoomIsntAlreadyBooked),
					clockWork.GetParameter("@apptypeid", DbType.Int32, appTypeId),
					clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
					clockWork.GetParameter("@enddate", DbType.DateTime, endDate),
					clockWork.GetParameter("@appcode", DbType.Int32, tentative ? -1 : 0),
					clockWork.GetParameter("@rid", DbType.Int32, rid),
					clockWork.GetParameter("@pid", DbType.Int32, studentPid),
					clockWork.GetParameter("@lucid", DbType.Int32, lucid),
					clockWork.GetParameter("@examid", DbType.Int32, num),
					clockWork.GetParameter("@totalbreakminutes", DbType.Int32, breakMinutes),
					clockWork.GetParameter("@ignoreapps", DbType.Boolean, findPotentialBookingsInfo.IgnoreStudentsSchedule),
					clockWork.GetParameter("@ignoresametestsameday", DbType.Boolean, findPotentialBookingsInfo.IgnoreTwoTestsSameCourseSameDay)
				};
				clockWork.ExecuteNonQueryTransaction("DECLARE @sd0 datetime, @sd1 datetime\r\nSET @sd0 = DATEADD(D, 0, DATEDIFF(D, 0, @startdate))\r\nSET @sd1 = DATEADD(day,1,@sd0)\r\n\r\nIF @ignoreapps=0 AND EXISTS(SELECT a.appointmentid FROM apps a WHERE a.PersonID=@pid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\nBEGIN\r\n\tset @appid = 0\r\n    set @failedreason = 'studentbooked'\r\nEND\r\nELSE IF @dontcareifroombooked=0 AND EXISTS(SELECT a.appointmentid FROM apps a WHERE @dontcareifroombooked=0 AND @rid>0 AND a.PersonID=@rid AND a.cancelled=0 AND NOT ( ( a.enddate<=@startdate ) OR (a.startdate >= @enddate ) ))\r\nBEGIN\r\n    set @appid = 0\r\n    set @failedreason = 'roombooked'\r\nEND\r\nELSE IF @ignoresametestsameday=0 AND EXISTS( SELECT ac.appointmentid FROM apps a LEFT JOIN AppointmentCourses ac ON ac.AppointmentID=a.AppointmentID WHERE a.personid=@pid AND a.cancelled=0 AND a.startDate >=@sd0 AND a.startDate<@sd1 AND NOT ac.AppointmentID IS NULL AND ac.LUCourseID=@lucid )\r\nBEGIN\r\n    set @appid = 0\r\n    set @failedreason = 'alreadybookedsamecoursesameday'\r\nEND\r\nELSE\r\nBEGIN\r\n\tINSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,examid,totalbreakminutes) \r\n\t\tSELECT @apptypeid,@startdate,@enddate,0,getdate(),@pid,0,0,0,@appcode,-1,@examid,@totalbreakminutes\r\n\t\r\n    set @appid = SCOPE_IDENTITY()\r\n    set @failedreason = ''\r\nEND", dbTransaction, array2);
				int num3 = (array2[0].Value is DBNull) ? 0 : ((int)array2[0].Value);
				string text = (array2[1].Value is DBNull) ? "" : (((string)array2[1].Value) ?? "");
				bool flag2 = !string.IsNullOrEmpty(text) || num3 < 1;
				if (flag2)
				{
					dbTransaction.Rollback();
					ex = null;
					string text2 = text;
					string a = text2;
					if (!(a == "studentbooked"))
					{
						if (!(a == "roombooked"))
						{
							if (!(a == "alreadybookedsamecoursesameday"))
							{
								failedReason = Appointment.eCreateAppointmentFailedReason.Unknown;
								ex = new Exception(text);
							}
							else
							{
								failedReason = Appointment.eCreateAppointmentFailedReason.StudentAlreadyBookedSameCourseSameDay;
							}
						}
						else
						{
							failedReason = Appointment.eCreateAppointmentFailedReason.RoomDoubleBooked;
						}
					}
					else
					{
						failedReason = Appointment.eCreateAppointmentFailedReason.StudentDoubleBooked;
					}
					result = 0;
				}
				else
				{
					int[] array3 = new int[]
					{
						studentPid,
						rid
					};
					foreach (int num4 in array3)
					{
						array2 = new DbParameter[]
						{
							clockWork.GetParameter("@appid", DbType.Int32, num3),
							clockWork.GetParameter("@pid", DbType.Int32, num4),
							clockWork.GetParameter("@noshow", DbType.Boolean, false),
							clockWork.GetParameter("@misccode", DbType.Int32, -1)
						};
						clockWork.ExecuteNonQueryTransaction("INSERT INTO attendees (appointmentid,personid,noshow,misccode) VALUES (@appid,@pid,@noshow,@misccode)", dbTransaction, array2);
					}
					string accommodationsString = ClockWorkWebAPI.TestBooking.Accommodation.GetAccommodationsString(accommodationsToUse);
					array2 = new DbParameter[]
					{
						clockWork.GetParameter("@appointmentid", DbType.Int32, num3),
						clockWork.GetParameter("@lucid", DbType.Int32, lucid),
						clockWork.GetParameter("@classsd", DbType.DateTime, classStartDate),
						clockWork.GetParameter("@classed", DbType.DateTime, classEndDate),
						clockWork.GetParameter("@testnote", DbType.Binary, tripleDES.Encrypt(accommodationsString)),
						clockWork.GetParameter("@studentnote", DbType.Binary, tripleDES.Encrypt(studentNote))
					};
					clockWork.ExecuteNonQueryTransaction("INSERT INTO appointmentcourses (appointmentid,lucourseid,originalstartdatetime,originalenddatetime,testnote,studentnote) VALUES (@appointmentid,@lucid,@classsd,@classed,@testnote,@studentnote)", dbTransaction, array2);
					foreach (ClockWorkWebAPI.TestBooking.Accommodation accommodation in accommodationsToUse)
					{
						array2 = new DbParameter[]
						{
							clockWork.GetParameter("@appid", DbType.Int32, num3),
							clockWork.GetParameter("@cid", DbType.Int32, accommodation.Controlid),
							clockWork.GetParameter("@pid", DbType.Int32, studentPid)
						};
						clockWork.ExecuteNonQueryTransaction("INSERT INTO accommodationstest (examid,appointmentid,personid,controlid,whoselected,datemodified)\r\nSELECT @appid,@appid,@pid,@cid,@pid,getdate() WHERE NOT EXISTS(SELECT examid FROM accommodationstest WHERE personid=@pid AND controlid=@cid AND appointmentid=@appid)", dbTransaction, array2);
					}
					failedReason = Appointment.eCreateAppointmentFailedReason.None;
					dbTransaction.Commit();
					ex = null;
					result = num3;
				}
			}
			catch (Exception ex3)
			{
				failedReason = Appointment.eCreateAppointmentFailedReason.Unknown;
				ex = ex3;
				result = 0;
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002FF0 File Offset: 0x000011F0
		public static DataTable LoadInstructorTestInfo(int examId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@examid", DbType.Int32, examId)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_InstructorTestInfo, parameters);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003034 File Offset: 0x00001234
		public static int GetNoShowConsecutiveCount(int pid, DateTime beforeDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] array = new DbParameter[2];
			array[0] = clockWork.Parameter;
			array[0].ParameterName = "@pid";
			array[0].DbType = DbType.Int32;
			array[0].Value = pid;
			array[1] = clockWork.Parameter;
			array[1].ParameterName = "@beforedate";
			array[1].DbType = DbType.DateTime;
			array[1].Value = beforeDate;
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_UserAppointmentsReverseOrder, array);
			int num = 0;
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = dataRow[1] != DBNull.Value && Convert.ToBoolean(dataRow[1]);
				if (!flag)
				{
					break;
				}
				num++;
			}
			return num;
		}
	}
}
