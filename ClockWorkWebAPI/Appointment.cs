using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;
using Databases;
using EncryptionClassLibrary;

namespace ClockWorkWebAPI
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class Appointment
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003894 File Offset: 0x00001A94
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000038AC File Offset: 0x00001AAC
		public int Colour
		{
			get
			{
				return this.colour;
			}
			set
			{
				this.colour = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000038B8 File Offset: 0x00001AB8
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000038D0 File Offset: 0x00001AD0
		public string SubTitle
		{
			get
			{
				return this.subTitle;
			}
			set
			{
				this.subTitle = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000038DC File Offset: 0x00001ADC
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000038F4 File Offset: 0x00001AF4
		public string CourseDescription
		{
			get
			{
				return this.courseDescription;
			}
			set
			{
				this.courseDescription = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003900 File Offset: 0x00001B00
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00003918 File Offset: 0x00001B18
		public string MemoPlainText
		{
			get
			{
				return this.memoPlainText;
			}
			set
			{
				this.memoPlainText = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003924 File Offset: 0x00001B24
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000393C File Offset: 0x00001B3C
		public DateTime ActualStartDateTime
		{
			get
			{
				return this.actualStartDateTime;
			}
			set
			{
				this.actualStartDateTime = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003948 File Offset: 0x00001B48
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003960 File Offset: 0x00001B60
		public DateTime ActualEndDateTime
		{
			get
			{
				return this.actualEndDateTime;
			}
			set
			{
				this.actualEndDateTime = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005C RID: 92 RVA: 0x0000396C File Offset: 0x00001B6C
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003984 File Offset: 0x00001B84
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003990 File Offset: 0x00001B90
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000039A8 File Offset: 0x00001BA8
		public string ServiceProviderName
		{
			get
			{
				return this.serviceProviderName;
			}
			set
			{
				this.serviceProviderName = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000039B4 File Offset: 0x00001BB4
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000039CC File Offset: 0x00001BCC
		public int AppointmentId
		{
			get
			{
				return this.appointmentId;
			}
			set
			{
				this.appointmentId = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000062 RID: 98 RVA: 0x000039D8 File Offset: 0x00001BD8
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000039F0 File Offset: 0x00001BF0
		public DateTime StartDateTime
		{
			get
			{
				return this.startDateTime;
			}
			set
			{
				this.startDateTime = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000039FC File Offset: 0x00001BFC
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003A14 File Offset: 0x00001C14
		public DateTime EndDateTime
		{
			get
			{
				return this.endDateTime;
			}
			set
			{
				this.endDateTime = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003A20 File Offset: 0x00001C20
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003A38 File Offset: 0x00001C38
		public int RoomId
		{
			get
			{
				return this.roomId;
			}
			set
			{
				this.roomId = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003A44 File Offset: 0x00001C44
		public string CourseSection
		{
			get
			{
				return this.courseSection;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003A5C File Offset: 0x00001C5C
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003A74 File Offset: 0x00001C74
		public ArrayList Attendees
		{
			get
			{
				return this.attendees;
			}
			set
			{
				this.attendees = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003A80 File Offset: 0x00001C80
		public string TestNote
		{
			get
			{
				return this.testNote;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003A98 File Offset: 0x00001C98
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public int AppTypeId
		{
			get
			{
				return this.appTypeId;
			}
			set
			{
				this.appTypeId = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003ABC File Offset: 0x00001CBC
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public string Title
		{
			get
			{
				return (this.title == null) ? "" : this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003AF0 File Offset: 0x00001CF0
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003B08 File Offset: 0x00001D08
		public int ServiceProviderId
		{
			get
			{
				return this.serviceProviderPid;
			}
			set
			{
				this.serviceProviderPid = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003B14 File Offset: 0x00001D14
		public int DurationMinutes
		{
			get
			{
				return Convert.ToInt32((this.endDateTime - this.startDateTime).TotalMinutes);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003B44 File Offset: 0x00001D44
		public Appointment()
		{
			this.appointmentId = 0;
			this.startDateTime = DateTime.Now;
			this.endDateTime = this.startDateTime.AddHours(1.0);
			this.attendees = new ArrayList();
			this.testNote = "";
			this.courseSection = "";
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public Appointment(DateTime startDateTime, DateTime endDateTime)
		{
			this.appointmentId = 0;
			this.startDateTime = startDateTime;
			this.endDateTime = endDateTime;
			this.attendees = new ArrayList();
			this.testNote = "";
			this.courseSection = "";
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003C70 File Offset: 0x00001E70
		public Appointment(DataRow dr)
		{
			this.appointmentId = ((dr["appointmentid"] == DBNull.Value) ? -1 : ((int)dr["appointmentid"]));
			this.startDateTime = ((dr["startdate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["startdate"]));
			this.endDateTime = ((dr["enddate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["enddate"]));
			this.testNote = ((!dr.Table.Columns.Contains("testnote") || dr["testnote"] == DBNull.Value) ? "" : "");
			this.attendees = new ArrayList();
			this.courseSection = ((!dr.Table.Columns.Contains("section") || dr["section"] == DBNull.Value) ? "" : ((string)dr["section"]));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public int AddAttendee(int personid, string firstname, string lastname, string student_no)
		{
			return this.AddAttendee(personid, firstname, lastname, student_no, 0);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003DF8 File Offset: 0x00001FF8
		public int AddAttendee(int personid, string firstname, string lastname, string student_no, int primaryGroupId)
		{
			Person person = new Person(personid, firstname + " " + lastname, "");
			person.StudentNumber = student_no;
			bool flag = primaryGroupId > 0;
			if (flag)
			{
				person.PrimaryGroupId = primaryGroupId;
			}
			this.attendees.Add(person);
			return this.attendees.Count - 1;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003E58 File Offset: 0x00002058
		public int GetDurationMinutes()
		{
			return (int)(this.endDateTime - this.startDateTime).TotalMinutes;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003E84 File Offset: 0x00002084
		public string GetDurationMinutesString()
		{
			int durationMinutes = this.GetDurationMinutes();
			return (durationMinutes <= 0) ? "" : durationMinutes.ToString();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003EB0 File Offset: 0x000020B0
		public override string ToString()
		{
			return this.startDateTime.ToString("dddd MMMM d, yyyy ... h:mm tt") + " to " + this.endDateTime.ToString("h:mm tt");
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003EEC File Offset: 0x000020EC
		public static int CreateAppointment(int whoBooked, int[] attendees, DateTime startDate, DateTime endDate, int appTypeId, bool tentative, string memo)
		{
			return Appointment.CreateAppointment(whoBooked, attendees, startDate, endDate, appTypeId, tentative, memo, null);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003F10 File Offset: 0x00002110
		public static int CreateAppointment(int whoBooked, int[] attendees, DateTime startDate, DateTime endDate, int appTypeId, bool tentative, string memo, string subTitle)
		{
			TimeSpan timeSpan = endDate - startDate;
			int durationMinutes = (timeSpan.TotalMinutes <= 0.0) ? 10 : Convert.ToInt32(timeSpan.TotalMinutes);
			return Appointment.CreateAppointment(whoBooked, attendees, startDate, durationMinutes, appTypeId, tentative, memo, subTitle);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003F60 File Offset: 0x00002160
		public static int CreateAppointment(int whoBooked, int[] attendees, DateTime startDate, int durationMinutes, int appTypeId, bool tentative, string memo)
		{
			return Appointment.CreateAppointment(whoBooked, attendees, startDate, durationMinutes, appTypeId, tentative, memo, null);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003F84 File Offset: 0x00002184
		public static int CreateAppointment(int whoBooked, int[] attendees, DateTime startDate, int durationMinutes, int appTypeId, bool tentative, string memo, string subTitle)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			bool flag = string.IsNullOrEmpty(subTitle);
			int num;
			if (flag)
			{
				string query = "INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode) VALUES (@apptypeid,@startdate,@enddate,@cancelled,@dateadded,@personid,@ishidden,@islocked,@extraattendeescount,@appcode,@groupcode); SET @id=SCOPE_IDENTITY()";
				DbParameter[] array = new DbParameter[]
				{
					clockWork.GetParameter("@apptypeid", DbType.Int32, appTypeId),
					clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
					clockWork.GetParameter("@enddate", DbType.DateTime, startDate.AddMinutes((double)durationMinutes)),
					clockWork.GetParameter("@cancelled", DbType.Boolean, false),
					clockWork.GetParameter("@dateadded", DbType.DateTime, DateTime.Now),
					clockWork.GetParameter("@personid", DbType.Int32, whoBooked),
					clockWork.GetParameter("@ishidden", DbType.Boolean, false),
					clockWork.GetParameter("@islocked", DbType.Boolean, false),
					clockWork.GetParameter("@extraattendeescount", DbType.Int32, 0),
					clockWork.GetParameter("@appcode", DbType.Int32, tentative ? -1 : 0),
					clockWork.GetParameter("@groupcode", DbType.Int32, -1),
					clockWork.GetOutputParameter("@id", DbType.Int32, 0)
				};
				clockWork.ExecuteNonQuery(query, array);
				num = ((array[11].Value is DBNull || array[11].Value == null) ? 0 : ((int)array[11].Value));
			}
			else
			{
				string query = "INSERT INTO appointments (apptypeid,startdate,enddate,cancelled,dateadded,personid,ishidden,islocked,extraattendeescount,appcode,groupcode,subject) VALUES (@apptypeid,@startdate,@enddate,@cancelled,@dateadded,@personid,@ishidden,@islocked,@extraattendeescount,@appcode,@groupcode,@subtitle); SET @id=SCOPE_IDENTITY()";
				DbParameter[] array = new DbParameter[]
				{
					clockWork.GetParameter("@apptypeid", DbType.Int32, appTypeId),
					clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
					clockWork.GetParameter("@enddate", DbType.DateTime, startDate.AddMinutes((double)durationMinutes)),
					clockWork.GetParameter("@cancelled", DbType.Boolean, false),
					clockWork.GetParameter("@dateadded", DbType.DateTime, DateTime.Now),
					clockWork.GetParameter("@personid", DbType.Int32, whoBooked),
					clockWork.GetParameter("@ishidden", DbType.Boolean, false),
					clockWork.GetParameter("@islocked", DbType.Boolean, false),
					clockWork.GetParameter("@extraattendeescount", DbType.Int32, 0),
					clockWork.GetParameter("@appcode", DbType.Int32, tentative ? -1 : 0),
					clockWork.GetParameter("@groupcode", DbType.Int32, -1),
					clockWork.GetParameter("@subtitle", DbType.Binary, encryption.Encrypt(subTitle)),
					clockWork.GetOutputParameter("@id", DbType.Int32, 0)
				};
				clockWork.ExecuteNonQuery(query, array);
				num = ((array[12].Value is DBNull) ? 0 : ((int)array[12].Value));
			}
			bool flag2 = num <= 0;
			int result;
			if (flag2)
			{
				result = 0;
			}
			else
			{
				foreach (int num2 in attendees)
				{
					string query = "INSERT INTO attendees (appointmentid,personid,noshow,misccode) VALUES (@appid,@pid,@noshow,@misccode)";
					DbParameter[] array = new DbParameter[]
					{
						clockWork.GetParameter("@appid", DbType.Int32, num),
						clockWork.GetParameter("@pid", DbType.Int32, num2),
						clockWork.GetParameter("@noshow", DbType.Boolean, false),
						clockWork.GetParameter("@misccode", DbType.Int32, -1)
					};
					Exception ex;
					try
					{
						clockWork.ExecuteNonQuery(query, array);
						ex = null;
					}
					catch (Exception ex2)
					{
						ex = ex2;
					}
					bool flag3 = ex != null;
					if (flag3)
					{
						return 0;
					}
				}
				bool flag4 = !string.IsNullOrEmpty(memo);
				if (flag4)
				{
					string query = "INSERT INTO appointmentmemos (appointmentid,memotext,isencrypted) VALUES (@appid,@memotext,@isencrypted)";
					DbParameter[] array = new DbParameter[]
					{
						clockWork.GetParameter("@appid", DbType.Int32, num),
						clockWork.GetParameter("@memotext", DbType.Binary, encryption.Encrypt(Appointment.StringToRtf2(memo))),
						clockWork.GetParameter("@isencrypted", DbType.Boolean, true)
					};
					Exception ex;
					try
					{
						clockWork.ExecuteNonQuery(query, array);
						ex = null;
					}
					catch (Exception ex3)
					{
						ex = ex3;
					}
					bool flag5 = ex != null;
					if (flag5)
					{
						return 0;
					}
				}
				result = num;
			}
			return result;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000442C File Offset: 0x0000262C
		public static string StringToRtf2(string str)
		{
			return "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Arial;}} \\viewkind4\\uc1\\pard\\fs20 " + str + "\\par }";
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004450 File Offset: 0x00002650
		public string ToStringForEmail(bool showRoom, bool showServiceProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.subject);
			stringBuilder.Append("<br />");
			stringBuilder.Append(this.startDateTime.ToString("dddd MMMM d, yyyy"));
			stringBuilder.Append(" . ");
			stringBuilder.Append(this.GetDurationMinutesString());
			if (showRoom)
			{
				stringBuilder.Append("<br />");
			}
			if (showServiceProvider)
			{
				stringBuilder.Append("<br />");
				stringBuilder.Append(this.serviceProviderName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000044EC File Offset: 0x000026EC
		[Obsolete]
		public static bool IsUserAllowedToBook(int appId, db conn, int pid, int maxNumAppsInTheFuture, int maxNumAppsPerWeek, int bannedDateTimeCid, int bannedNumDaysActive, bool noConsecutiveAppointments, out string reasonFailed)
		{
			return Appointment.IsUserAllowedToBook(appId, pid, maxNumAppsInTheFuture, maxNumAppsPerWeek, bannedDateTimeCid, bannedNumDaysActive, noConsecutiveAppointments, out reasonFailed);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004510 File Offset: 0x00002710
		public static bool IsUserAllowedToBook(int appId, int pid, int maxNumAppsInTheFuture, int maxNumAppsPerWeek, int bannedDateTimeCid, int bannedNumDaysActive, bool noConsecutiveAppointments, out string reasonFailed)
		{
			return Appointment.IsUserAllowedToBook(appId, pid, maxNumAppsInTheFuture, maxNumAppsPerWeek, bannedDateTimeCid, bannedNumDaysActive, noConsecutiveAppointments, 0, out reasonFailed);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004534 File Offset: 0x00002734
		public static bool IsUserAllowedToBook(int appId, int pid, int maxNumAppsInTheFuture, int maxNumAppsPerWeek, int bannedDateTimeCid, int bannedNumDaysActive, bool noConsecutiveAppointments, int numNoshowsInARowToBan, out string reasonFailed)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@appid", DbType.Int32, appId)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_AppointmentByAppointmentId, parameters);
			bool flag = dataTable.Rows.Count > 0;
			bool result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				result = new Appointment((DateTime)dataRow["startdate"], (DateTime)dataRow["enddate"])
				{
					appointmentId = appId,
					appTypeId = (int)dataRow["apptypeid"]
				}.IsUserAllowedToBook(pid, maxNumAppsInTheFuture, maxNumAppsPerWeek, bannedDateTimeCid, bannedNumDaysActive, noConsecutiveAppointments, numNoshowsInARowToBan, out reasonFailed);
			}
			else
			{
				reasonFailed = "Can't find app with appid=" + appId.ToString();
				result = false;
			}
			return result;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004610 File Offset: 0x00002810
		[Obsolete]
		public bool IsUserAllowedToBook(db conn, int pid, int maxNumAppsInTheFuture, int maxNumAppsPerWeek, int bannedDateTimeCid, int bannedNumDaysActive, bool noConsecutiveAppointments, out string reasonFailed)
		{
			return this.IsUserAllowedToBook(pid, maxNumAppsInTheFuture, maxNumAppsPerWeek, bannedDateTimeCid, bannedNumDaysActive, noConsecutiveAppointments, out reasonFailed);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004634 File Offset: 0x00002834
		public bool IsUserAllowedToBook(int pid, int maxNumAppsInTheFuture, int maxNumAppsPerWeek, int bannedDateTimeCid, int bannedNumDaysActive, bool noConsecutiveAppointments, out string reasonFailed)
		{
			return this.IsUserAllowedToBook(pid, maxNumAppsInTheFuture, maxNumAppsPerWeek, bannedDateTimeCid, bannedNumDaysActive, noConsecutiveAppointments, 0, out reasonFailed);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004658 File Offset: 0x00002858
		public bool IsUserAllowedToBook(int pid, int maxNumAppsInTheFuture, int maxNumAppsPerWeek, int bannedDateTimeCid, int bannedNumDaysActive, bool noConsecutiveAppointments, int numNoshowsInARowToBan, out string reasonFailed)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DateTime dateTime = this.startDateTime;
			DateTime t = this.startDateTime.AddDays(1.0);
			while (dateTime.DayOfWeek > DayOfWeek.Sunday)
			{
				dateTime = dateTime.AddDays(-1.0);
			}
			while (t.DayOfWeek > DayOfWeek.Sunday)
			{
				t = t.AddDays(1.0);
			}
			dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
			t = new DateTime(t.Year, t.Month, t.Day).AddDays(1.0);
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@sd", DbType.DateTime, dateTime)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_AllAppointments_By_Pid, parameters);
			DataRow[] array = dataTable.Select("cancelled=0 AND startdate>='" + dateTime.ToString("yyyy-MM-dd") + "'");
			DataTable dataTable2 = dataTable.Clone();
			foreach (DataRow row in array)
			{
				dataTable2.ImportRow(row);
			}
			DateTime now = DateTime.Now;
			bool flag = maxNumAppsInTheFuture > 0;
			if (flag)
			{
				int num = 0;
				foreach (object obj in dataTable2.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					DateTime t2 = (DateTime)dataRow["startdate"];
					bool flag2 = t2 > now;
					if (flag2)
					{
						num++;
					}
				}
				bool flag3 = num >= maxNumAppsInTheFuture;
				if (flag3)
				{
					reasonFailed = "Max num in future (" + num.ToString() + ")";
					return false;
				}
			}
			bool flag4 = maxNumAppsPerWeek > 0;
			if (flag4)
			{
				int num = 0;
				foreach (object obj2 in dataTable2.Rows)
				{
					DataRow dataRow2 = (DataRow)obj2;
					DateTime t3 = (DateTime)dataRow2["startdate"];
					bool flag5 = t3 > dateTime && t3 < t;
					if (flag5)
					{
						num++;
					}
				}
				bool flag6 = num >= maxNumAppsPerWeek;
				if (flag6)
				{
					reasonFailed = "Max # per week (" + num.ToString() + ")";
					return false;
				}
			}
			if (noConsecutiveAppointments)
			{
				foreach (object obj3 in dataTable2.Rows)
				{
					DataRow dataRow3 = (DataRow)obj3;
					DateTime t4 = (DateTime)dataRow3["startdate"];
					DateTime t5 = (DateTime)dataRow3["enddate"];
					bool flag7 = (t5 >= this.startDateTime && t5 <= this.endDateTime) || (t4 >= this.startDateTime && t4 <= this.endDateTime) || (t4 <= this.startDateTime && t5 >= this.endDateTime);
					if (flag7)
					{
						reasonFailed = "Consecutive apps (" + dataRow3["appointmentid"].ToString() + ")";
						return false;
					}
				}
			}
			bool flag8 = numNoshowsInARowToBan > 0;
			if (flag8)
			{
				int num2 = 0;
				bool flag9 = false;
				foreach (object obj4 in new DataView(dataTable)
				{
					Sort = "startdate desc"
				})
				{
					DataRowView dataRowView = (DataRowView)obj4;
					DataRow row2 = dataRowView.Row;
					DateTime t6 = (DateTime)row2["startdate"];
					bool flag10 = !flag9 && t6 <= now;
					if (flag10)
					{
						flag9 = true;
					}
					bool flag11 = flag9;
					if (flag11)
					{
						bool flag12 = !(row2["noshow"] is DBNull) && Convert.ToBoolean(row2["noshow"]);
						bool flag13 = !flag12;
						if (flag13)
						{
							break;
						}
						num2++;
					}
				}
				bool flag14 = num2 >= numNoshowsInARowToBan;
				if (flag14)
				{
					reasonFailed = "Too many no-shows in a row (" + num2.ToString() + ")";
					return false;
				}
			}
			bool flag15 = bannedDateTimeCid > 0;
			if (flag15)
			{
				parameters = new DbParameter[]
				{
					clockWork.GetParameter("@pid", DbType.Int32, pid),
					clockWork.GetParameter("@cid", DbType.Int32, bannedDateTimeCid),
					clockWork.GetParameter("@d", DbType.DateTime, DateTime.Now.AddDays((double)(-(double)bannedNumDaysActive)))
				};
				dataTable2 = clockWork.ExecuteQuery(QueryStorage.QS_Select_DateTimePsData, parameters);
				bool flag16 = dataTable2.Rows.Count > 0;
				if (flag16)
				{
					reasonFailed = "Banned.";
					return false;
				}
			}
			reasonFailed = "";
			return true;
		}

		// Token: 0x0400001B RID: 27
		private int appointmentId;

		// Token: 0x0400001C RID: 28
		private DateTime startDateTime;

		// Token: 0x0400001D RID: 29
		private DateTime endDateTime;

		// Token: 0x0400001E RID: 30
		private int roomId;

		// Token: 0x0400001F RID: 31
		private ArrayList attendees;

		// Token: 0x04000020 RID: 32
		private string subject;

		// Token: 0x04000021 RID: 33
		private string courseDescription = "";

		// Token: 0x04000022 RID: 34
		private string subTitle = "";

		// Token: 0x04000023 RID: 35
		private int colour;

		// Token: 0x04000024 RID: 36
		private DateTime actualStartDateTime;

		// Token: 0x04000025 RID: 37
		private DateTime actualEndDateTime;

		// Token: 0x04000026 RID: 38
		private string memoText = "";

		// Token: 0x04000027 RID: 39
		private string memoPlainText = "";

		// Token: 0x04000028 RID: 40
		private string courseSection;

		// Token: 0x04000029 RID: 41
		private string title;

		// Token: 0x0400002A RID: 42
		private int appTypeId;

		// Token: 0x0400002B RID: 43
		private string serviceProviderName = "";

		// Token: 0x0400002C RID: 44
		private string testNote;

		// Token: 0x0400002D RID: 45
		private int serviceProviderPid = 0;

		// Token: 0x02000086 RID: 134
		public enum eCreateAppointmentFailedReason
		{
			// Token: 0x0400035B RID: 859
			None,
			// Token: 0x0400035C RID: 860
			Unknown,
			// Token: 0x0400035D RID: 861
			RoomDoubleBooked,
			// Token: 0x0400035E RID: 862
			MissingInformation,
			// Token: 0x0400035F RID: 863
			StudentDoubleBooked,
			// Token: 0x04000360 RID: 864
			StudentAlreadyBookedSameCourseSameDay
		}
	}
}
