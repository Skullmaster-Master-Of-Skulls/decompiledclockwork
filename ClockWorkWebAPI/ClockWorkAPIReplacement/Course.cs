using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Databases;
using EncryptionClassLibrary;
using UnivOleDb;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x02000050 RID: 80
	public class Course : IComparable
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0001B958 File Offset: 0x00019B58
		public static string GetSemesterString(bool fullSeasonDescription)
		{
			return Course.GetSemesterString(fullSeasonDescription, "Winter", "Spring", "Summer", "Fall");
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001B984 File Offset: 0x00019B84
		public static string GetSemesterString(bool fullSeasonDescription, string termForWinter, string termForSpring, string termForSummer, string termForFall)
		{
			DateTime now = DateTime.Now;
			bool flag = now.Month >= 9 || (now.Month == 8 && now.Day > 12);
			string str;
			if (flag)
			{
				str = (fullSeasonDescription ? termForFall : termForFall.Substring(0, 2).ToUpper());
			}
			else
			{
				bool flag2 = now.Month >= 7 || (now.Month == 6 && now.Day == 12);
				if (flag2)
				{
					str = (fullSeasonDescription ? termForSummer : termForSummer.Substring(0, 2).ToUpper());
				}
				else
				{
					bool flag3 = now.Month >= 5 || (now.Month == 4 && now.Day == 12);
					if (flag3)
					{
						str = (fullSeasonDescription ? termForSpring : termForSummer.Substring(0, 2).ToUpper());
					}
					else
					{
						str = (fullSeasonDescription ? termForWinter : termForWinter.Substring(0, 2).ToUpper());
					}
				}
			}
			return str + " " + now.Year.ToString();
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001BA94 File Offset: 0x00019C94
		// (set) Token: 0x060003CB RID: 971 RVA: 0x0001BAAC File Offset: 0x00019CAC
		public int LuCourseId
		{
			get
			{
				return this.luCourseId;
			}
			set
			{
				this.luCourseId = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001BAB8 File Offset: 0x00019CB8
		// (set) Token: 0x060003CD RID: 973 RVA: 0x0001BAD0 File Offset: 0x00019CD0
		public int RegistrationStatus
		{
			get
			{
				return this.registrationStatus;
			}
			set
			{
				this.registrationStatus = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001BADA File Offset: 0x00019CDA
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0001BAE2 File Offset: 0x00019CE2
		public string SubjectCode { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0001BAEC File Offset: 0x00019CEC
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x0001BB04 File Offset: 0x00019D04
		public List<Instructor> Instructors
		{
			get
			{
				return this.instructors;
			}
			set
			{
				this.instructors = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001BB10 File Offset: 0x00019D10
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0001BB28 File Offset: 0x00019D28
		public List<CourseContactInformation> AltContacts
		{
			get
			{
				return this.altContacts;
			}
			set
			{
				this.altContacts = value;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001BB34 File Offset: 0x00019D34
		public Course(UnivDataAdapter da, int luCourseId)
		{
			this.LoadCourse(luCourseId);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001BB88 File Offset: 0x00019D88
		public Course(int luCourseId)
		{
			this.LoadCourse(luCourseId);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001BBDC File Offset: 0x00019DDC
		private void LoadCourse(int luCourseId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			this.luCourseId = luCourseId;
			this.instructors = new List<Instructor>();
			this.altContacts = new List<CourseContactInformation>();
			string query = "SELECT \tluc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration\r\n            ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section\r\n            ,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone ,lucd2.username AS instructorusername\r\n            ,alt.alternatecontactid,alt.altname,alt.altemail,alt.altphone,alt.altusername,alt.altpermissionlevel\r\nFROM \tlucourses luc \r\n\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n\tLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n    LEFT JOIN lucoursealternatecontact alt ON alt.alternatecontactid=luc.alternatecontactid\r\nWHERE luc.lucourseid=@lucid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, luCourseId)
			});
			bool flag = dataTable.Rows.Count > 0;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				this.term = (string)dataRow["term"];
				this.duration = (string)dataRow["duration"];
				this.subject = (string)dataRow["subject"];
				this.courseCode = (string)dataRow["course"];
				this.timeOfDay = (string)dataRow["timeofday"];
				this.section = (string)dataRow["section"];
				this.instructorName = ((dataRow["instructor"] == DBNull.Value) ? "" : ((string)dataRow["instructor"]));
				this.instructorPhone = ((dataRow["instructorphone"] == DBNull.Value) ? "" : ((string)dataRow["instructorphone"]));
				this.instructorEmail = ((dataRow["instructoremail"] == DBNull.Value) ? "" : ((string)dataRow["instructoremail"]));
				this.SubjectCode = (dataTable.Columns.Contains("subjectcode") ? dataRow["subjectcode"].ToString() : "");
				this.startDate = ((dataRow["startdate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["startdate"]));
				this.endDate = ((dataRow["enddate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dataRow["enddate"]));
				bool flag2 = dataRow["alternatecontactid"] != DBNull.Value;
				if (flag2)
				{
					this.alternateContact = new CourseContactInformation(dataRow);
				}
				else
				{
					this.alternateContact = null;
				}
				bool flag3 = dataTable.Columns.Contains("instructorusername");
				if (flag3)
				{
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow2 = (DataRow)obj;
						this.instructorUsername = ((dataRow2["instructorusername"] == DBNull.Value) ? "" : dataRow2["instructorusername"].ToString());
					}
				}
			}
			else
			{
				luCourseId = -1;
				this.term = "";
				this.duration = "";
				this.subject = "";
				this.courseCode = "";
				this.timeOfDay = "";
				this.section = "";
				this.instructorEmail = "";
				this.instructorName = "";
				this.instructorPhone = "";
				this.instructorUsername = "";
				this.SubjectCode = "";
				this.alternateContact = null;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001BF6C File Offset: 0x0001A16C
		public Course()
		{
			this.luCourseId = -1;
			this.term = "";
			this.duration = "";
			this.subject = "";
			this.courseCode = "";
			this.timeOfDay = "";
			this.section = "";
			this.instructorEmail = "";
			this.instructorName = "";
			this.instructorPhone = "";
			this.instructorUsername = "";
			this.instructors = new List<Instructor>();
			this.altContacts = new List<CourseContactInformation>();
			this.alternateContact = null;
			this.SubjectCode = "";
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001C058 File Offset: 0x0001A258
		public Course(DataRow dr)
		{
			DataTable table = dr.Table;
			this.instructors = new List<Instructor>();
			this.altContacts = new List<CourseContactInformation>();
			bool flag = dr.Table != null;
			string name;
			string name2;
			if (flag)
			{
				bool flag2 = dr.Table.Columns.Contains("instructorphone");
				if (flag2)
				{
					name = "instructoremail";
					name2 = "instructorphone";
				}
				else
				{
					name = "iemail";
					name2 = "iphone";
				}
			}
			else
			{
				name = "instructoremail";
				name2 = "instructorphone";
			}
			bool flag3 = table.Columns.Contains("lucourseid");
			if (flag3)
			{
				this.luCourseId = ((dr["lucourseid"] == DBNull.Value) ? -1 : ((int)dr["lucourseid"]));
			}
			else
			{
				this.luCourseId = -1;
			}
			this.term = this.GetStringCellvalueIfExists("term", table, dr);
			this.duration = this.GetStringCellvalueIfExists("duration", table, dr);
			this.subject = this.GetStringCellvalueIfExists("subject", table, dr);
			this.courseCode = this.GetStringCellvalueIfExists("course", table, dr);
			this.timeOfDay = this.GetStringCellvalueIfExists("timeofday", table, dr);
			this.section = this.GetStringCellvalueIfExists("section", table, dr);
			this.instructorName = this.GetStringCellvalueIfExists("instructor", table, dr);
			this.instructorPhone = this.GetStringCellvalueIfExists(name2, table, dr);
			this.instructorEmail = this.GetStringCellvalueIfExists(name, table, dr);
			this.campus = this.GetStringCellvalueIfExists("campus", table, dr);
			this.department = this.GetStringCellvalueIfExists("departmentcode", table, dr);
			this.SubjectCode = (table.Columns.Contains("subjectcode") ? dr["subjectcode"].ToString().Trim() : "");
			this.subjectEmail = this.GetStringCellvalueIfExists("subjectemail", table, dr);
			bool flag4 = table != null && table.Columns.Contains("startdate");
			if (flag4)
			{
				bool flag5 = table.Columns["startdate"].DataType == typeof(DateTime);
				if (flag5)
				{
					this.startDate = ((dr["startdate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["startdate"]));
					this.endDate = ((dr["enddate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["enddate"]));
				}
				else
				{
					string s = dr["startdate"].ToString();
					string s2 = dr["enddate"].ToString();
					DateTime.TryParse(s, out this.startDate);
					DateTime.TryParse(s2, out this.endDate);
				}
			}
			bool flag6 = table.Columns.Contains("alternatecontactid") && dr["alternatecontactid"] != DBNull.Value;
			if (flag6)
			{
				try
				{
					this.alternateContact = new CourseContactInformation(dr);
				}
				catch
				{
					this.alternateContact = null;
				}
			}
			else
			{
				this.alternateContact = null;
			}
			bool flag7 = table.Columns.Contains("registrationstatus");
			if (flag7)
			{
				this.registrationStatus = ((dr["registrationstatus"] == DBNull.Value) ? 0 : ((int)dr["registrationstatus"]));
			}
			this.registrationStatus = 0;
			bool flag8 = table.Columns.Contains("instructorusername");
			if (flag8)
			{
				this.instructorUsername = dr["instructorusername"].ToString();
			}
			else
			{
				this.instructorUsername = "";
			}
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001C450 File Offset: 0x0001A650
		private string GetStringCellvalueIfExists(string name, DataTable t, DataRow dr)
		{
			bool flag = t != null && t.Columns.Contains(name);
			string result;
			if (flag)
			{
				result = ((dr[name] == DBNull.Value) ? "" : ((string)dr[name]));
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
		public bool Matches(Course course)
		{
			return this.subject.Equals(course.Subject) && this.courseCode.Equals(course.CourseCode) && this.section.Equals(course.Section) && this.timeOfDay.Equals(course.TimeOfDay) && this.term.Equals(course.Term) && this.duration.Equals(course.Duration) && !(course.EndDate <= this.startDate) && !(course.StartDate >= this.endDate);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001C55C File Offset: 0x0001A75C
		public string ToStringSimple()
		{
			return string.Format("{0} {1} {2} {3}", new object[]
			{
				this.subject,
				this.courseCode,
				this.section,
				this.timeOfDay
			});
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.subject);
			stringBuilder.Append(" ");
			stringBuilder.Append(this.courseCode);
			bool flag = !string.IsNullOrEmpty(this.timeOfDay);
			if (flag)
			{
				stringBuilder.Append(this.timeOfDay);
			}
			stringBuilder.Append(" ");
			stringBuilder.Append(this.section);
			stringBuilder.Append(" (");
			bool flag2 = !string.IsNullOrEmpty(this.duration);
			if (flag2)
			{
				stringBuilder.Append(this.duration);
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(this.term);
			stringBuilder.Append(")");
			bool flag3 = !string.IsNullOrEmpty(this.instructorName);
			if (flag3)
			{
				stringBuilder.Append("; [");
				stringBuilder.Append(this.instructorName);
				stringBuilder.Append(" . ");
				stringBuilder.Append(this.instructorEmail);
				stringBuilder.Append(" . ");
				stringBuilder.Append(this.instructorPhone);
				stringBuilder.Append("]");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001C6E4 File Offset: 0x0001A8E4
		public string ToStringDebug()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} {1} {2} {3} (term={4}, duration={5}, dates={6} to {7}", new object[]
			{
				this.subject,
				this.courseCode,
				this.timeOfDay,
				this.section,
				this.term,
				this.duration,
				this.startDate.ToString("yyyy-MM-dd"),
				this.endDate.ToString("yyyy-MM-dd")
			});
			return stringBuilder.ToString();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001C770 File Offset: 0x0001A970
		public static DataTable GetSameCourses(UnivDataAdapter da, IEncryption tripleDES, DateTime startDate, DateTime endDate, string term, string duration, string subject, string course)
		{
			da.SelectCommand.CommandText = "SELECT \tluc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone \r\nINTO #t1\r\nFROM \tlucourses luc \r\n\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n\tLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE\tluc.term=@term AND luc.duration=@duration AND luc.course=@course\r\n\tAND luc.subjectid IN (SELECT lucoursedataid AS subjectid FROM lucoursedata WHERE lookuplisttype=0 AND (lookupstring=@subject OR altlookupstring=@subject))\r\n\tAND NOT ((@enddate<luc.startdate) OR (@startdate>luc.enddate));\r\n\r\nSELECT \tluc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone \r\nFROM \tlucourses luc \r\n\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n\tLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\nWHERE luc.crosslistcode IN (SELECT crosslistcode FROM #t1 WHERE crosslistcode>0) AND NOT luc.lucourseid IN (SELECT lucourseid FROM #t1)\r\n    AND NOT ((@enddate<luc.startdate) OR (@startdate>luc.enddate))\r\nUNION\r\nSELECT lucourseid,startdate,enddate,term,duration,subject,course,timeofday,section,instructor,instructoremail,instructorphone\r\nFROM #t1\r\nORDER BY startdate,term,duration,subject,course,section";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@startdate", startDate);
			da.SelectCommand.Parameters.Add("@enddate", endDate);
			da.SelectCommand.Parameters.Add("@term", term);
			da.SelectCommand.Parameters.Add("@duration", duration);
			da.SelectCommand.Parameters.Add("@subject", subject);
			da.SelectCommand.Parameters.Add("@course", course);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			return dataTable;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001C850 File Offset: 0x0001AA50
		public static DataTable GetStudentsSameCourse(UnivDataAdapter da, IEncryption tripleDES, DateTime startDate, DateTime endDate, string term, string duration, string subject, string course)
		{
			DataTable sameCourses = Course.GetSameCourses(da, tripleDES, startDate, endDate, term, duration, subject, course);
			string text = "";
			foreach (object obj in sameCourses.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["lucourseid"];
				bool flag = text.Length > 0;
				if (flag)
				{
					text += ",";
				}
				text += num.ToString();
			}
			string commandText = "SELECT    c.personid,p.lastname,p.firstname,p.student_no,luc.lucourseid\r\n            ,lucd.altlookupstring AS subject,luc.course,luc.section\r\n            ,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail\r\n            ,lucd2.phone AS instructorphone \r\nFROM        courses c LEFT JOIN people p ON p.personid=c.personid \r\n            LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE       c.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@lucids", text);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return tripleDES.EncryptOrDecryptNameDataTableBatch(false, dataTable, new string[]
			{
				"lastname",
				"firstname",
				"student_no"
			});
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0001C97C File Offset: 0x0001AB7C
		public static string ParseTimetableString(DataRow dr)
		{
			string[] array = new string[]
			{
				"sun",
				"mon",
				"tue",
				"wed",
				"thu",
				"fri",
				"sat"
			};
			string text = "";
			for (int i = 0; i < 7; i++)
			{
				string columnName = array[i] + "startminutes";
				string columnName2 = array[i] + "endminutes";
				int num = (dr[columnName] != DBNull.Value) ? ((int)dr[columnName]) : 0;
				int num2 = (dr[columnName2] != DBNull.Value) ? ((int)dr[columnName2]) : 0;
				bool flag = num > 0 && num2 > num;
				if (flag)
				{
					int num3 = (int)((double)num / 60.0);
					int num4 = (int)((double)num2 / 60.0);
					string text2 = (num - num3 * 60).ToString();
					bool flag2 = text2.Length < 2;
					if (flag2)
					{
						text2 = "0" + text2;
					}
					string text3 = (num2 - num4 * 60).ToString();
					bool flag3 = text3.Length < 2;
					if (flag3)
					{
						text3 = "0" + text3;
					}
					bool flag4 = text.Length > 0;
					if (flag4)
					{
						text += ", ";
					}
					text = text + array[i] + " (";
					text = text + num3.ToString() + ":" + text2;
					text += " - ";
					text = text + num4.ToString() + ":" + text3;
					text += ")";
				}
			}
			return text;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001CB54 File Offset: 0x0001AD54
		public static List<DateTime[]> ParseTimetable(DateTime sundayDateMidnight, DataRow dr)
		{
			string[] array = new string[]
			{
				"sun",
				"mon",
				"tue",
				"wed",
				"thu",
				"fri",
				"sat"
			};
			List<DateTime[]> list = new List<DateTime[]>();
			for (int i = 0; i < 7; i++)
			{
				string columnName = array[i] + "startminutes";
				string columnName2 = array[i] + "endminutes";
				int num = (dr[columnName] != DBNull.Value) ? ((int)dr[columnName]) : 0;
				int num2 = (dr[columnName2] != DBNull.Value) ? ((int)dr[columnName2]) : 0;
				bool flag = num > 0 && num2 > num;
				if (flag)
				{
					int num3 = (int)((double)num / 60.0);
					int num4 = (int)((double)num2 / 60.0);
					string text = (num - num3 * 60).ToString();
					bool flag2 = text.Length < 2;
					if (flag2)
					{
						text = "0" + text;
					}
					string text2 = (num2 - num4 * 60).ToString();
					bool flag3 = text2.Length < 2;
					if (flag3)
					{
						text2 = "0" + text2;
					}
					DateTime dateTime = sundayDateMidnight.AddDays((double)i).AddMinutes((double)num);
					DateTime dateTime2 = sundayDateMidnight.AddDays((double)i).AddMinutes((double)num2);
					DateTime[] item = new DateTime[]
					{
						dateTime,
						dateTime2
					};
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001CD14 File Offset: 0x0001AF14
		public static DataTable LoadCourses(UnivDataAdapter da, IEncryption tripleDES, int pid, DateTime sdate, DateTime edate)
		{
			return Course.LoadCourses(da, tripleDES, pid, sdate, edate, true);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001CD34 File Offset: 0x0001AF34
		public static List<Course> LoadStudentsCourses(UnivDataAdapter da, IEncryption tripleDES, int pid, DateTime sdate, DateTime edate, bool includeDroppedCourses)
		{
			DataTable dataTable = Course.LoadCourses(da, tripleDES, pid, sdate, edate, includeDroppedCourses);
			List<Course> list = new List<Course>();
			bool flag = dataTable != null;
			if (flag)
			{
				foreach (object obj in dataTable.Rows)
				{
					DataRow dr = (DataRow)obj;
					Course item = new Course(dr);
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001CDC8 File Offset: 0x0001AFC8
		public static DataTable LoadCourses(UnivDataAdapter da, IEncryption tripleDES, int pid, DateTime sdate, DateTime edate, bool includeDroppedCourses)
		{
			da.SelectCommand.CommandText = "SELECT c.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,lucd2.username AS instructorusername";
			UnivCommand selectCommand = da.SelectCommand;
			selectCommand.CommandText += " FROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid";
			UnivCommand selectCommand2 = da.SelectCommand;
			selectCommand2.CommandText += " WHERE c.personid=@pid AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )";
			bool flag = !includeDroppedCourses;
			if (flag)
			{
				UnivCommand selectCommand3 = da.SelectCommand;
				selectCommand3.CommandText += " AND (c.registrationstatus IS NULL OR NOT c.registrationstatus=2)";
			}
			UnivCommand selectCommand4 = da.SelectCommand;
			selectCommand4.CommandText += " ORDER BY luc.duration,luc.term,lucd.altlookupstring,luc.course,luc.timeofday,luc.section";
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@sdate", sdate);
			da.SelectCommand.Parameters.Add("@edate", edate);
			da.SelectCommand.Parameters.Add("@pid", pid);
			DataTable dataTable = new DataTable();
			string text;
			da.Fill(dataTable, out text);
			return dataTable;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		public static string CourseLucidsToStringCommaSeparated(List<Course> courses)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Course course in courses)
			{
				bool flag = stringBuilder.Length > 0;
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(course.LuCourseId.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001CF6C File Offset: 0x0001B16C
		public static Course GetCourseById(List<Course> courses, int lucid)
		{
			foreach (Course course in courses)
			{
				bool flag = course.luCourseId == lucid;
				if (flag)
				{
					return course;
				}
			}
			return null;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0001CFD0 File Offset: 0x0001B1D0
		public static string CourseDisplayTemplateString
		{
			get
			{
				return Course.courseDisplayTemplateString;
			}
			set
			{
				string text = value.Replace("#<subject>#", "{0}");
				text = text.Replace("#<course>#", "{1}");
				text = text.Replace("#<timeofday>#", "{2}");
				text = text.Replace("#<section>#", "{3}");
				text = text.Replace("#<instructor>#", "{4}");
				text = text.Replace("#<instructorphone>#", "{5}");
				text = text.Replace("#<instructoremail>#", "{6}");
				text = text.Replace("#<term>#", "{7}");
				text = text.Replace("#<duration>#", "{8}");
				text = text.Replace("#<startdate>#", "{9}");
				text = text.Replace("#<enddate>#", "{10}");
				text = text.Replace("#<campus>#", "{11}");
				Course.courseDisplayTemplateString = text;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001D0CC File Offset: 0x0001B2CC
		public static string GetCourseDisplayString(DataRow dr)
		{
			string format = Course.CourseDisplayTemplateString;
			return string.Format(format, new object[]
			{
				dr["subject"].ToString(),
				dr["course"].ToString(),
				dr["timeofday"].ToString(),
				dr["section"].ToString(),
				dr["instructor"].ToString(),
				dr["instructorphone"].ToString(),
				dr["instructoremail"].ToString(),
				dr["term"].ToString(),
				dr["duration"].ToString(),
				dr["startdate"].ToString(),
				dr["enddate"].ToString(),
				""
			});
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0001D1D0 File Offset: 0x0001B3D0
		public bool IsSameCourse(Course course)
		{
			return string.Equals(this.duration, course.Duration, StringComparison.CurrentCultureIgnoreCase) && string.Equals(this.term, course.Term, StringComparison.CurrentCultureIgnoreCase) && string.Equals(this.subject, course.Subject, StringComparison.CurrentCultureIgnoreCase) && string.Equals(this.timeOfDay, course.TimeOfDay, StringComparison.CurrentCultureIgnoreCase) && string.Equals(this.section, course.Section, StringComparison.CurrentCultureIgnoreCase) && !(course.EndDate <= this.startDate) && !(course.startDate >= this.EndDate);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0001D278 File Offset: 0x0001B478
		public static List<Course> LoadStudentsCoursesForDataSync(int pid, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			IEncryption encryption = clockWork.Encryption;
			string query = "SELECT    p.personid,c.coursesid,c.registrationstatus\r\n            ,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject\r\n            ,luc.course,luc.timeofday,luc.[section]\r\n            ,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone\r\n            ,lucd2.username AS instructorusername,lucd2.id AS instructorid\r\n            ,luc.alternatecontactid,alt.altname,alt.altemail,alt.altphone,alt.altusername,alt.alternatecontactid\r\n            ,tt.*\r\nFROM        people p LEFT JOIN courses c ON c.personid=p.personid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n            LEFT JOIN lucoursealternatecontact alt ON alt.alternatecontactid=luc.alternatecontactid\r\n            LEFT JOIN timetable tt ON tt.lucourseid=c.lucourseid\r\nWHERE       p.isactive=1 AND p.personid=@pid\r\n            AND NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) )\r\nORDER BY    c.lucourseid";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			});
			return Course.GetCoursesFromCoursesTable(dataTable.DefaultView);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001D300 File Offset: 0x0001B500
		public static List<Course> GetCoursesFromCoursesTable(DataView dv)
		{
			List<Course> list = new List<Course>();
			int j;
			for (int i = 0; i < dv.Count; i = j)
			{
				DataRow row = dv[i].Row;
				Course course = new Course(row);
				for (j = i; j < dv.Count; j++)
				{
					DataRow row2 = dv[j].Row;
					Course course2 = new Course(row2);
					bool flag = !course2.IsSameCourse(course);
					if (flag)
					{
						break;
					}
				}
				Course item = new Course(dv[i].Row);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		public bool Drop(int pid)
		{
			bool flag = this.luCourseId > 0 && pid > 0;
			bool result;
			if (flag)
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				string query = "UPDATE courses SET registrationstatus=2 WHERE lucourseid=@lucid AND personid=@pid";
				int num = clockWork.ExecuteNonQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@lucid", DbType.Int32, this.luCourseId),
					clockWork.GetParameter("@pid", DbType.Int32, pid)
				});
				result = (num > 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001D434 File Offset: 0x0001B634
		public bool UnDrop(int pid)
		{
			bool flag = this.luCourseId > 0 && pid > 0;
			bool result;
			if (flag)
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				string query = "UPDATE courses SET registrationstatus=1 WHERE lucourseid=@lucid AND personid=@pid";
				int num = clockWork.ExecuteNonQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@lucid", DbType.Int32, this.luCourseId),
					clockWork.GetParameter("@pid", DbType.Int32, pid)
				});
				result = (num > 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001D4B8 File Offset: 0x0001B6B8
		public bool AddToStudentList(int pid, int whoAdded)
		{
			bool flag = this.luCourseId > 0 && pid > 0;
			bool result;
			if (flag)
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				string query = "IF NOT EXISTS(SELECT coursesid FROM courses WHERE personid=@pid AND lucourseid=@lucid)\r\n    INSERT INTO courses (dateadded,whoadded,registrationstatus,pid,lucourseid) VALUES (getdate(),@whoadded,1,@pid,@lucourseid)";
				int num = clockWork.ExecuteNonQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@lucid", DbType.Int32, this.luCourseId),
					clockWork.GetParameter("@pid", DbType.Int32, pid),
					clockWork.GetParameter("@whoadded", DbType.Int32, whoAdded)
				});
				result = (num > 0);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001D550 File Offset: 0x0001B750
		public int LookupLuCourseId(out bool createdCourse)
		{
			createdCourse = false;
			bool flag = this.luCourseId > 0;
			int result;
			if (flag)
			{
				result = this.luCourseId;
			}
			else
			{
				bool flag2 = string.IsNullOrEmpty(this.subject);
				if (flag2)
				{
					result = 0;
				}
				else
				{
					bool flag3 = string.IsNullOrEmpty(this.term);
					if (flag3)
					{
						result = 0;
					}
					else
					{
						bool flag4 = string.IsNullOrEmpty(this.courseCode);
						if (flag4)
						{
							result = 0;
						}
						else
						{
							bool flag5 = this.startDate == DateTime.MinValue || this.endDate == DateTime.MinValue;
							if (flag5)
							{
								result = 0;
							}
							else
							{
								DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
								string query = "SELECT luc.lucourseid FROM lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE luc.duration=@duration AND luc.term=@term AND lucd.altlookupstring=@subject AND luc.[section]=@section\r\n    AND luc.timeofday=@timeofday AND luc.campus=@campus AND luc.department=@department\r\n    AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )";
								DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
								{
									clockWork.GetParameter("@duration", DbType.String, this.duration),
									clockWork.GetParameter("@term", DbType.String, this.term),
									clockWork.GetParameter("@subject", DbType.String, this.subject),
									clockWork.GetParameter("@timeofday", DbType.String, this.timeOfDay),
									clockWork.GetParameter("@section", DbType.String, this.section),
									clockWork.GetParameter("@sdate", DbType.DateTime, this.startDate),
									clockWork.GetParameter("@edate", DbType.DateTime, this.endDate),
									clockWork.GetParameter("@campus", DbType.String, this.campus),
									clockWork.GetParameter("@department", DbType.String, this.department)
								});
								bool flag6 = dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value;
								if (flag6)
								{
									this.luCourseId = (int)dataTable.Rows[0][0];
									result = this.luCourseId;
								}
								else
								{
									createdCourse = true;
									query = "DECLARE @subjectid int\r\nIF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE altlookupstring=@subject)\r\n    SELECT TOP 1 lucoursedataid AS subjectid FROM lucoursedata WHERE altlookupstring=@subject\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring) VALUES (0,@subject,@subject);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS subjectid\r\nEND";
									DataTable dataTable2 = clockWork.ExecuteQuery(query, new DbParameter[]
									{
										clockWork.GetParameter("@subject", DbType.String, this.subject)
									});
									bool flag7 = dataTable2.Rows.Count < 1;
									if (flag7)
									{
										result = 0;
									}
									else
									{
										int num = (int)dataTable2.Rows[0][0];
										foreach (Instructor instructor in this.instructors)
										{
											instructor.LookupInstructorId();
										}
										foreach (CourseContactInformation courseContactInformation in this.altContacts)
										{
											courseContactInformation.LookupAlternateContactId();
										}
										query = "INSERT INTO lucourses (startdate,enddate,term,duration,subjectid,course,timeofday,[section],instructorid,crosslistcode,equivalentcode,coursenote,whoadded,dateadded,location,alternatecontactid,instructorpermissionlevel,campus,department)\r\n    VALUES (@sdate,@edate,@term,@duration,@subjectid,@course,@timeofday,@section,@instructorid,-1,-1,'',-556,getdate(),@location,@alternatecontactid,256,@campus,@department);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS lucourseid;";
										DbParameter[] array = new DbParameter[13];
										array[0] = clockWork.GetParameter("@sdate", DbType.DateTime, this.startDate);
										array[1] = clockWork.GetParameter("@edate", DbType.DateTime, this.endDate);
										array[2] = clockWork.GetParameter("@term", DbType.String, this.term);
										array[3] = clockWork.GetParameter("@duration", DbType.String, this.duration);
										array[4] = clockWork.GetParameter("@subjectid", DbType.Int32, num);
										array[5] = clockWork.GetParameter("@course", DbType.String, this.courseCode);
										array[6] = clockWork.GetParameter("@timeofday", DbType.String, this.timeOfDay);
										array[7] = clockWork.GetParameter("@section", DbType.String, this.section);
										Instructor instructor2 = this.instructors.Find((Instructor e) => e.InstructorId > 0);
										array[8] = clockWork.GetParameter("@instructorid", DbType.Int32, (instructor2 == null) ? -1 : instructor2.InstructorId);
										array[9] = clockWork.GetParameter("@location", DbType.String, this.location);
										CourseContactInformation courseContactInformation2 = this.altContacts.Find((CourseContactInformation f) => f.AlternateContactId > 0);
										array[10] = clockWork.GetParameter("@alternatecontactid", DbType.Int32, (courseContactInformation2 == null) ? -1 : courseContactInformation2.AlternateContactId);
										array[11] = clockWork.GetParameter("@campus", DbType.String, this.campus);
										array[12] = clockWork.GetParameter("@department", DbType.String, this.department);
										DataTable dataTable3 = clockWork.ExecuteQuery(query, array);
										bool flag8 = dataTable3.Rows.Count < 1 || dataTable3.Rows[0][0] == DBNull.Value;
										if (flag8)
										{
											result = 0;
										}
										else
										{
											int num2 = (int)dataTable3.Rows[0][0];
											result = num2;
										}
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0001DA50 File Offset: 0x0001BC50
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0001DA68 File Offset: 0x0001BC68
		public string Term
		{
			get
			{
				return this.term;
			}
			set
			{
				this.term = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0001DA74 File Offset: 0x0001BC74
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0001DA8C File Offset: 0x0001BC8C
		public string Duration
		{
			get
			{
				return this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0001DA98 File Offset: 0x0001BC98
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		public string CourseCode
		{
			get
			{
				return this.courseCode;
			}
			set
			{
				this.courseCode = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0001DABC File Offset: 0x0001BCBC
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0001DAD4 File Offset: 0x0001BCD4
		public string TimeOfDay
		{
			get
			{
				return this.timeOfDay;
			}
			set
			{
				this.timeOfDay = value;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0001DAF8 File Offset: 0x0001BCF8
		public string Section
		{
			get
			{
				return this.section;
			}
			set
			{
				this.section = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0001DB04 File Offset: 0x0001BD04
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0001DB1C File Offset: 0x0001BD1C
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

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001DB28 File Offset: 0x0001BD28
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0001DB40 File Offset: 0x0001BD40
		public string InstructorName
		{
			get
			{
				return this.instructorName;
			}
			set
			{
				this.instructorName = value;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001DB4C File Offset: 0x0001BD4C
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0001DB64 File Offset: 0x0001BD64
		public string InstructorEmail
		{
			get
			{
				return this.instructorEmail;
			}
			set
			{
				this.instructorEmail = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0001DB70 File Offset: 0x0001BD70
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0001DB88 File Offset: 0x0001BD88
		public string InstructorPhone
		{
			get
			{
				return this.instructorPhone;
			}
			set
			{
				this.instructorPhone = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0001DB94 File Offset: 0x0001BD94
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0001DBAC File Offset: 0x0001BDAC
		public string InstructorUsername
		{
			get
			{
				return this.instructorUsername;
			}
			set
			{
				this.instructorUsername = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0001DBB8 File Offset: 0x0001BDB8
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0001DBD0 File Offset: 0x0001BDD0
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0001DBDC File Offset: 0x0001BDDC
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0001DBF4 File Offset: 0x0001BDF4
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0001DC00 File Offset: 0x0001BE00
		public string SubjectEmail
		{
			get
			{
				return this.subjectEmail;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001DC18 File Offset: 0x0001BE18
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0001DC30 File Offset: 0x0001BE30
		public string Location
		{
			get
			{
				return this.location;
			}
			set
			{
				this.location = value;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0001DC54 File Offset: 0x0001BE54
		public string Campus
		{
			get
			{
				return this.campus;
			}
			set
			{
				this.campus = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0001DC60 File Offset: 0x0001BE60
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0001DC78 File Offset: 0x0001BE78
		public string Department
		{
			get
			{
				return this.department;
			}
			set
			{
				this.department = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0001DC84 File Offset: 0x0001BE84
		public CourseContactInformation AlternateContact
		{
			get
			{
				return this.alternateContact;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001DC9C File Offset: 0x0001BE9C
		public int CompareTo(object obj)
		{
			bool flag = obj == null;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = obj is Course;
				if (flag2)
				{
					Course c = (Course)obj;
					bool flag3 = Course.CompareCourses(c, this, false, true, true, true, true);
					if (flag3)
					{
						result = 0;
					}
					else
					{
						result = -1;
					}
				}
				else
				{
					result = -1;
				}
			}
			return result;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		public static bool CompareCourses(Course c1, Course c2, bool compareDates, bool compareTermDuration, bool compareSection, bool compareTimeOfDay, bool compareInstructorEmail)
		{
			bool flag = compareDates && !Course.CompareDatesYearMonthDayOnly(c1.StartDate, c2.StartDate, false);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = compareDates && !Course.CompareDatesYearMonthDayOnly(c1.EndDate, c2.EndDate, false);
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = compareTermDuration && !Course.CompareStrings(c1.Term, c2.Term);
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = compareTermDuration && !Course.CompareStrings(c1.Duration, c2.Duration);
						if (flag4)
						{
							result = false;
						}
						else
						{
							bool flag5 = !Course.CompareStrings(c1.Subject, c2.Subject);
							if (flag5)
							{
								result = false;
							}
							else
							{
								bool flag6 = !Course.CompareStrings(c1.CourseCode, c2.CourseCode);
								if (flag6)
								{
									result = false;
								}
								else
								{
									bool flag7 = compareSection && !Course.CompareStrings(c1.Section, c2.Section);
									if (flag7)
									{
										int num;
										int num2;
										bool flag8 = Course.ParseString(c1.Section, out num) && Course.ParseString(c2.Section, out num2);
										if (!flag8)
										{
											return false;
										}
										bool flag9 = num != num2;
										if (flag9)
										{
											return false;
										}
									}
									bool flag10 = compareTimeOfDay && !Course.CompareStrings(c1.TimeOfDay, c2.TimeOfDay);
									if (flag10)
									{
										result = false;
									}
									else
									{
										bool flag11 = compareInstructorEmail && !Course.CompareStrings(c1.InstructorEmail, c2.InstructorEmail);
										result = !flag11;
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001DE88 File Offset: 0x0001C088
		private static bool CompareStrings(string s1, string s2)
		{
			return Course.GetCompareString(s1).CompareTo(Course.GetCompareString(s2)) == 0;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		private static string GetCompareString(string s)
		{
			return s.Trim().ToLower();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		private static bool ParseString(string str, out int number)
		{
			string text = str.Trim();
			bool flag = text.Length > 0;
			bool result;
			if (flag)
			{
				string text2 = "";
				foreach (char c in text)
				{
					bool flag2 = !char.IsDigit(c);
					if (flag2)
					{
						number = 0;
						return false;
					}
					text2 += c.ToString();
				}
				number = int.Parse(text2);
				result = true;
			}
			else
			{
				number = 0;
				result = false;
			}
			return result;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001DF5C File Offset: 0x0001C15C
		private static bool CompareDatesYearMonthDayOnly(DateTime d1, DateTime d2, bool compareDay)
		{
			return d1.Year == d2.Year && d1.Month == d2.Month && (!compareDay || d1.Day == d2.Day);
		}

		// Token: 0x040001F3 RID: 499
		public const int REGISTRATIONSTATUS_DROPPED = 2;

		// Token: 0x040001F4 RID: 500
		public const int REGISTRATIONSTATUS_EXEMPTFROMDATASYNC = 8;

		// Token: 0x040001F5 RID: 501
		private int luCourseId = -1;

		// Token: 0x040001F6 RID: 502
		private string term;

		// Token: 0x040001F7 RID: 503
		private string duration;

		// Token: 0x040001F8 RID: 504
		private string subject;

		// Token: 0x040001F9 RID: 505
		private string courseCode;

		// Token: 0x040001FA RID: 506
		private string timeOfDay;

		// Token: 0x040001FB RID: 507
		private string section;

		// Token: 0x040001FC RID: 508
		private string instructorName;

		// Token: 0x040001FD RID: 509
		private string instructorEmail;

		// Token: 0x040001FE RID: 510
		private string instructorPhone;

		// Token: 0x040001FF RID: 511
		private string instructorUsername;

		// Token: 0x04000200 RID: 512
		private string subjectEmail;

		// Token: 0x04000201 RID: 513
		private DateTime startDate;

		// Token: 0x04000202 RID: 514
		private DateTime endDate;

		// Token: 0x04000203 RID: 515
		private CourseContactInformation alternateContact = null;

		// Token: 0x04000204 RID: 516
		private int registrationStatus = 0;

		// Token: 0x04000205 RID: 517
		private string campus = "";

		// Token: 0x04000206 RID: 518
		private string location = "";

		// Token: 0x04000207 RID: 519
		private string department = "";

		// Token: 0x04000208 RID: 520
		private List<Instructor> instructors;

		// Token: 0x04000209 RID: 521
		private List<CourseContactInformation> altContacts;

		// Token: 0x0400020B RID: 523
		private static string courseDisplayTemplateString = "";
	}
}
