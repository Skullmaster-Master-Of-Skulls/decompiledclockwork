using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000073 RID: 115
	public class Test
	{
		// Token: 0x060005CF RID: 1487 RVA: 0x000267E0 File Offset: 0x000249E0
		public Test()
		{
			DateTime minValue = DateTime.MinValue;
			this.scheduledStartDateTime = minValue;
			this.scheduledEndDateTime = minValue;
			this.classStartDateTime = minValue;
			this.classEndDateTime = minValue;
			this.actualStartTime = minValue;
			this.actualEndTime = minValue;
			this.appTypeId = -1;
			this.appTypeDescription = "";
			this.appCode = 0;
			this.appCodeDescription = "";
			this.appointmentId = 0;
			this.examId = 0;
			this.roomPid = 0;
			this.roomDescription = "";
			this.location = "";
			this.luCourseId = 0;
			this.courseDescription = "";
			this.students = new List<AttendeeDTO>();
			this.otherAttendees = new List<AttendeeDTO>();
			this.studentNote = "";
			this.testNote = "";
			this.course = null;
			this.cancelled = false;
			this.status = "";
			this.memo = "";
			this.testDelivered = "";
			this.privateNote2 = "";
			this.sittingId = 0;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00026904 File Offset: 0x00024B04
		public Test(DataRow dr)
		{
			DataTable table = dr.Table;
			this.appointmentId = ((dr["appointmentid"] == DBNull.Value) ? 0 : ((int)dr["appointmentid"]));
			this.examId = ((dr["examid"] == DBNull.Value) ? 0 : ((int)dr["examid"]));
			this.scheduledStartDateTime = ((dr["scheduledstarttime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["scheduledstarttime"]));
			this.scheduledEndDateTime = ((dr["scheduledendtime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["scheduledendtime"]));
			this.totalBreakMinutes = ((dr["totalbreakminutes"] == DBNull.Value) ? 0 : ((int)dr["totalbreakminutes"]));
			this.appointmentId = ((dr["appointmentid"] == DBNull.Value) ? 0 : ((int)dr["appointmentid"]));
			this.appTypeId = ((dr["apptypeid"] == DBNull.Value) ? -1 : ((int)dr["apptypeid"]));
			this.appTypeDescription = dr["description"].ToString();
			bool flag = this.appointmentId <= 0 && this.appTypeId <= 0;
			if (flag)
			{
			}
			this.appCode = ((dr["appcode"] == DBNull.Value) ? 0 : ((int)dr["appcode"]));
			this.appCodeDescription = "";
			DateTime minValue = DateTime.MinValue;
			this.classStartDateTime = minValue;
			this.classEndDateTime = minValue;
			this.actualStartTime = minValue;
			this.actualEndTime = minValue;
			this.students = new List<AttendeeDTO>();
			this.otherAttendees = new List<AttendeeDTO>();
			this.classStartDateTime = ((dr["classstartdatetime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["classstartdatetime"]));
			this.classEndDateTime = ((dr["classenddatetime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["classenddatetime"]));
			bool flag2 = this.scheduledStartDateTime == DateTime.MinValue;
			if (flag2)
			{
				this.scheduledStartDateTime = this.classStartDateTime;
				this.scheduledEndDateTime = this.classEndDateTime;
			}
			this.actualStartTime = ((dr["actualstarttime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["actualstarttime"]));
			this.actualEndTime = ((dr["actualendtime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["actualendtime"]));
			bool flag3 = dr["personid"] != DBNull.Value;
			if (flag3)
			{
				bool isNoShow = dr["noshow"] != DBNull.Value && Convert.ToBoolean(dr["noshow"]);
				PersonBaseDTO person = new PersonBaseDTO
				{
					PersonId = (int)dr["personid"],
					FirstName = dr["firstname"].ToString(),
					MiddleName = "",
					LastName = dr["student_no"].ToString(),
					CoreGroup = eCoreGroupDTO.Students
				};
				AttendeeDTO item = new AttendeeDTO
				{
					Person = person,
					IsNoShow = isNoShow,
					MiscCode = 0
				};
				this.students.Add(item);
			}
			bool flag4 = table.Columns.Contains("invigilatorpid") && dr["invigilatorpid"] != DBNull.Value;
			if (flag4)
			{
				string firstName = table.Columns.Contains("invigilatorfirstname") ? dr["invigilatorfirstname"].ToString() : "";
				string lastName = table.Columns.Contains("invigilatorlastname") ? dr["invigilatorlastname"].ToString() : "";
				PersonBaseDTO person2 = new PersonBaseDTO
				{
					PersonId = (int)dr["invigilatorpid"],
					FirstName = firstName,
					LastName = lastName,
					MiddleName = "",
					CoreGroup = eCoreGroupDTO.Unknown
				};
				AttendeeDTO item2 = new AttendeeDTO
				{
					Person = person2,
					IsNoShow = false,
					MiscCode = 0
				};
				this.otherAttendees.Add(item2);
			}
			bool flag5 = table.Columns.Contains("memotext") && dr["memotext"] != DBNull.Value;
			if (flag5)
			{
				this.memo = dr["memotext"].ToString();
			}
			this.roomPid = ((dr["roompid"] == DBNull.Value) ? 0 : ((int)dr["roompid"]));
			this.roomDescription = ((dr["room"] == DBNull.Value) ? "" : dr["room"].ToString());
			bool flag6 = table.Columns.Contains("location");
			if (flag6)
			{
				this.location = ((dr["location"] == DBNull.Value) ? "" : dr["location"].ToString());
			}
			else
			{
				this.location = "";
			}
			bool flag7 = table.Columns.Contains("lucourseid");
			if (flag7)
			{
				this.luCourseId = ((dr["lucourseid"] == DBNull.Value) ? 0 : ((int)dr["lucourseid"]));
			}
			else
			{
				this.luCourseId = 0;
			}
			bool flag8 = this.luCourseId > 0;
			if (flag8)
			{
				this.courseDescription = string.Format("{0} {1} {2} {3}", new object[]
				{
					dr["subject"].ToString(),
					dr["course"].ToString(),
					dr["section"].ToString(),
					table.Columns.Contains("timeofday") ? dr["timeofday"].ToString() : ""
				});
				this.course = new Course(dr);
			}
			else
			{
				this.courseDescription = "";
				this.course = null;
			}
			this.testNote = dr["examaccommodations"].ToString();
			this.studentNote = dr["accommodationgroups"].ToString();
			this.cancelled = (dr["cancelled"] != DBNull.Value && Convert.ToBoolean(dr["cancelled"]));
			bool flag9 = table.Columns.Contains("status");
			if (flag9)
			{
				this.status = dr["status"].ToString();
			}
			else
			{
				this.status = "";
			}
			bool flag10 = table.Columns.Contains("testdelivered");
			if (flag10)
			{
				this.testDelivered = dr["testdelivered"].ToString();
			}
			else
			{
				bool flag11 = table.Columns.Contains("usercomment");
				if (flag11)
				{
					this.testDelivered = dr["usercomment"].ToString();
				}
				else
				{
					this.testDelivered = "";
				}
			}
			bool flag12 = table.Columns.Contains("privatenote2");
			if (flag12)
			{
				this.privateNote2 = dr["privatenote2"].ToString();
			}
			else
			{
				this.privateNote2 = "";
			}
			bool flag13 = table.Columns.Contains("sittingid");
			if (flag13)
			{
				this.sittingId = ((dr["sittingid"] == DBNull.Value) ? 0 : ((int)dr["sittingid"]));
			}
			else
			{
				this.sittingId = 0;
			}
			bool flag14 = dr["ExamStatusLookupId"] != DBNull.Value;
			if (flag14)
			{
				this.ExamStatusLookupId = (int)dr["ExamStatusLookupId"];
				this.ExamStatusTitle = dr["ExamStatus"].ToString();
				bool flag15 = dr["colourargb"] != DBNull.Value;
				if (flag15)
				{
					this.ExamStatusColourId = (int)dr["colourargb"];
				}
			}
			else
			{
				this.ExamStatusTitle = "";
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000271C0 File Offset: 0x000253C0
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x000271C8 File Offset: 0x000253C8
		public int ExamStatusLookupId { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x000271D1 File Offset: 0x000253D1
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x000271D9 File Offset: 0x000253D9
		public string ExamStatusTitle { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000271E2 File Offset: 0x000253E2
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x000271EA File Offset: 0x000253EA
		public int ExamStatusColourId { get; set; }

		// Token: 0x040002FE RID: 766
		private DateTime scheduledStartDateTime;

		// Token: 0x040002FF RID: 767
		private DateTime scheduledEndDateTime;

		// Token: 0x04000300 RID: 768
		private int totalBreakMinutes;

		// Token: 0x04000301 RID: 769
		private DateTime classStartDateTime;

		// Token: 0x04000302 RID: 770
		private DateTime classEndDateTime;

		// Token: 0x04000303 RID: 771
		private DateTime actualStartTime;

		// Token: 0x04000304 RID: 772
		private DateTime actualEndTime;

		// Token: 0x04000305 RID: 773
		private int appTypeId;

		// Token: 0x04000306 RID: 774
		private int appCode;

		// Token: 0x04000307 RID: 775
		private string appTypeDescription;

		// Token: 0x04000308 RID: 776
		private string appCodeDescription;

		// Token: 0x04000309 RID: 777
		private string memo;

		// Token: 0x0400030A RID: 778
		private string testDelivered;

		// Token: 0x0400030B RID: 779
		private int appointmentId;

		// Token: 0x0400030C RID: 780
		private int examId;

		// Token: 0x0400030D RID: 781
		private List<AttendeeDTO> students;

		// Token: 0x0400030E RID: 782
		private List<AttendeeDTO> otherAttendees;

		// Token: 0x0400030F RID: 783
		private int roomPid;

		// Token: 0x04000310 RID: 784
		private string roomDescription;

		// Token: 0x04000311 RID: 785
		private string location;

		// Token: 0x04000312 RID: 786
		private int luCourseId;

		// Token: 0x04000313 RID: 787
		private string courseDescription;

		// Token: 0x04000314 RID: 788
		private Course course;

		// Token: 0x04000315 RID: 789
		private string testNote;

		// Token: 0x04000316 RID: 790
		private string studentNote;

		// Token: 0x04000317 RID: 791
		private string privateNote2;

		// Token: 0x04000318 RID: 792
		private bool cancelled;

		// Token: 0x04000319 RID: 793
		private string status;

		// Token: 0x0400031A RID: 794
		private int sittingId;

		// Token: 0x0400031B RID: 795
		private ExamSitting sitting = null;

		// Token: 0x0400031C RID: 796
		private PersonBaseDTO assignedCounsellor = null;
	}
}
