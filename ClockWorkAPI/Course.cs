using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using ClockWorkAPI.Courses;
using EncryptionClassLibrary;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ClockWorkAPI
{
	// Token: 0x0200008E RID: 142
	public class Course : IComparable
	{
		// Token: 0x060006DB RID: 1755 RVA: 0x00026510 File Offset: 0x00025510
		public static string GetSemesterString(bool fullSeasonDescription)
		{
			return Course.GetSemesterString(fullSeasonDescription, "Winter", "Spring", "Summer", "Fall");
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0002653C File Offset: 0x0002553C
		public static string GetSemesterString(bool fullSeasonDescription, string termForWinter, string termForSpring, string termForSummer, string termForFall)
		{
			DateTime now = DateTime.Now;
			string str;
			if (now.Month >= 9 || (now.Month == 8 && now.Day > 12))
			{
				str = (fullSeasonDescription ? termForFall : termForFall.Substring(0, 2).ToUpper());
			}
			else if (now.Month >= 7 || (now.Month == 6 && now.Day == 12))
			{
				str = (fullSeasonDescription ? termForSummer : termForSummer.Substring(0, 2).ToUpper());
			}
			else if (now.Month >= 5 || (now.Month == 4 && now.Day == 12))
			{
				str = (fullSeasonDescription ? termForSpring : termForSummer.Substring(0, 2).ToUpper());
			}
			else
			{
				str = (fullSeasonDescription ? termForWinter : termForWinter.Substring(0, 2).ToUpper());
			}
			return str + " " + now.Year.ToString();
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x00026654 File Offset: 0x00025654
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0002666C File Offset: 0x0002566C
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

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x00026678 File Offset: 0x00025678
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x00026690 File Offset: 0x00025690
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

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0002669C File Offset: 0x0002569C
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x000266B3 File Offset: 0x000256B3
		public string SubjectCode { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x000266BC File Offset: 0x000256BC
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x000266D4 File Offset: 0x000256D4
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x000266E0 File Offset: 0x000256E0
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x000266F8 File Offset: 0x000256F8
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

		// Token: 0x060006E7 RID: 1767 RVA: 0x00026704 File Offset: 0x00025704
		public Course(UnivDataAdapter da, int luCourseId)
		{
			this.LoadCourse(luCourseId);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00026758 File Offset: 0x00025758
		public Course(int luCourseId)
		{
			this.LoadCourse(luCourseId);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000267AC File Offset: 0x000257AC
		private void LoadCourse(int luCourseId)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			this.luCourseId = luCourseId;
			this.instructors = new List<Instructor>();
			this.altContacts = new List<CourseContactInformation>();
			string commandText = "SELECT \tluc.lucourseid,luc.startdate,luc.enddate,luc.term,luc.duration\r\n            ,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section\r\n            ,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone ,lucd2.username AS instructorusername\r\n            ,alt.alternatecontactid,alt.altname,alt.altemail,alt.altphone,alt.altusername,alt.altpermissionlevel\r\nFROM \tlucourses luc \r\n\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n\tLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n    LEFT JOIN lucoursealternatecontact alt ON alt.alternatecontactid=luc.alternatecontactid\r\nWHERE luc.lucourseid=@lucid";
			DataTable dataTable = new DataTable();
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@lucid", luCourseId);
			da.Fill(dataTable);
			if (dataTable.Rows.Count > 0)
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
				if (dataRow["alternatecontactid"] != DBNull.Value)
				{
					this.alternateContact = new CourseContactInformation(dataRow);
				}
				else
				{
					this.alternateContact = null;
				}
				if (dataTable.Columns.Contains("instructorusername"))
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

		// Token: 0x060006EA RID: 1770 RVA: 0x00026B60 File Offset: 0x00025B60
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

		// Token: 0x060006EB RID: 1771 RVA: 0x00026C4C File Offset: 0x00025C4C
		public Course(DataRow dr)
		{
			DataTable table = dr.Table;
			this.instructors = new List<Instructor>();
			this.altContacts = new List<CourseContactInformation>();
			string name;
			string name2;
			if (dr.Table != null)
			{
				if (dr.Table.Columns.Contains("instructorphone"))
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
			if (table.Columns.Contains("lucourseid"))
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
			if (table != null && table.Columns.Contains("startdate") && table.Columns.Contains("enddate"))
			{
				if (table.Columns["startdate"].DataType == typeof(DateTime))
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
			if (table.Columns.Contains("alternatecontactid") && dr["alternatecontactid"] != DBNull.Value)
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
			if (table.Columns.Contains("registrationstatus"))
			{
				this.registrationStatus = ((dr["registrationstatus"] == DBNull.Value) ? 0 : ((int)dr["registrationstatus"]));
			}
			this.registrationStatus = 0;
			if (table.Columns.Contains("instructorusername"))
			{
				this.instructorUsername = dr["instructorusername"].ToString();
			}
			else
			{
				this.instructorUsername = "";
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00027064 File Offset: 0x00026064
		private string GetStringCellvalueIfExists(string name, DataTable t, DataRow dr)
		{
			string result;
			if (t != null && t.Columns.Contains(name))
			{
				result = ((dr[name] == DBNull.Value) ? "" : ((string)dr[name]));
			}
			else
			{
				result = "";
			}
			return result;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000270BC File Offset: 0x000260BC
		public bool Matches(Course course)
		{
			return this.subject.Equals(course.Subject) && this.courseCode.Equals(course.CourseCode) && this.section.Equals(course.Section) && this.timeOfDay.Equals(course.TimeOfDay) && this.term.Equals(course.Term) && this.duration.Equals(course.Duration) && !(course.EndDate <= this.startDate) && !(course.StartDate >= this.endDate);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00027174 File Offset: 0x00026174
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

		// Token: 0x060006EF RID: 1775 RVA: 0x000271C0 File Offset: 0x000261C0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.subject);
			stringBuilder.Append(" ");
			stringBuilder.Append(this.courseCode);
			if (!string.IsNullOrEmpty(this.timeOfDay))
			{
				stringBuilder.Append(this.timeOfDay);
			}
			stringBuilder.Append(" ");
			stringBuilder.Append(this.section);
			stringBuilder.Append(" (");
			if (!string.IsNullOrEmpty(this.duration))
			{
				stringBuilder.Append(this.duration);
				stringBuilder.Append(" ");
			}
			stringBuilder.Append(this.term);
			stringBuilder.Append(")");
			if (!string.IsNullOrEmpty(this.instructorName))
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

		// Token: 0x060006F0 RID: 1776 RVA: 0x000272F4 File Offset: 0x000262F4
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

		// Token: 0x060006F1 RID: 1777 RVA: 0x00027384 File Offset: 0x00026384
		public static DataTable GetSameCourses(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DateTime startDate, DateTime endDate, string term, string duration, string subject, string course)
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

		// Token: 0x060006F2 RID: 1778 RVA: 0x00027464 File Offset: 0x00026464
		public static DataTable GetStudentsSameCourse(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, DateTime startDate, DateTime endDate, string term, string duration, string subject, string course)
		{
			DataTable sameCourses = Course.GetSameCourses(da, tripleDES, startDate, endDate, term, duration, subject, course);
			string text = "";
			foreach (object obj in sameCourses.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = (int)dataRow["lucourseid"];
				if (text.Length > 0)
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

		// Token: 0x060006F3 RID: 1779 RVA: 0x000275A8 File Offset: 0x000265A8
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
				if (num > 0 && num2 > num)
				{
					int num3 = (int)((double)num / 60.0);
					int num4 = (int)((double)num2 / 60.0);
					string text2 = (num - num3 * 60).ToString();
					if (text2.Length < 2)
					{
						text2 = "0" + text2;
					}
					string text3 = (num2 - num4 * 60).ToString();
					if (text3.Length < 2)
					{
						text3 = "0" + text3;
					}
					if (text.Length > 0)
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

		// Token: 0x060006F4 RID: 1780 RVA: 0x00027798 File Offset: 0x00026798
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
				if (num > 0 && num2 > num)
				{
					int num3 = (int)((double)num / 60.0);
					int num4 = (int)((double)num2 / 60.0);
					string text = (num - num3 * 60).ToString();
					if (text.Length < 2)
					{
						text = "0" + text;
					}
					string text2 = (num2 - num4 * 60).ToString();
					if (text2.Length < 2)
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

		// Token: 0x060006F5 RID: 1781 RVA: 0x0002797C File Offset: 0x0002697C
		public static DataTable LoadCourses(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int pid, DateTime sdate, DateTime edate)
		{
			return Course.LoadCourses(da, tripleDES, pid, sdate, edate, true);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0002799C File Offset: 0x0002699C
		public static List<Course> LoadStudentsCourses(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int pid, DateTime sdate, DateTime edate, bool includeDroppedCourses)
		{
			DataTable dataTable = Course.LoadCourses(da, tripleDES, pid, sdate, edate, includeDroppedCourses);
			List<Course> list = new List<Course>();
			if (dataTable != null)
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

		// Token: 0x060006F7 RID: 1783 RVA: 0x00027A3C File Offset: 0x00026A3C
		public static DataTable LoadCourses(UnivDataAdapter da, TripleDESEncryptionClass tripleDES, int pid, DateTime sdate, DateTime edate, bool includeDroppedCourses)
		{
			da.SelectCommand.CommandText = "SELECT c.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,lucd.altlookupstring AS subject,luc.course,luc.timeofday,luc.section,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,lucd2.username AS instructorusername";
			UnivCommand selectCommand = da.SelectCommand;
			selectCommand.CommandText += " FROM courses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid";
			UnivCommand selectCommand2 = da.SelectCommand;
			selectCommand2.CommandText += " WHERE c.personid=@pid AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )";
			if (!includeDroppedCourses)
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
			if (text != null && text.Length > 0)
			{
				MessageBox.Show(text);
			}
			return dataTable;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00027B6C File Offset: 0x00026B6C
		public static string CourseLucidsToStringCommaSeparated(List<Course> courses)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (Course course in courses)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(course.LuCourseId.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00027C04 File Offset: 0x00026C04
		public static Course GetCourseById(List<Course> courses, int lucid)
		{
			foreach (Course course in courses)
			{
				if (course.luCourseId == lucid)
				{
					return course;
				}
			}
			return null;
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00027D54 File Offset: 0x00026D54
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x00027C70 File Offset: 0x00026C70
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

		// Token: 0x060006FC RID: 1788 RVA: 0x00027D6C File Offset: 0x00026D6C
		public static string GetCourseDisplayString(DataRow dr)
		{
			string text = Course.CourseDisplayTemplateString;
			text = text.Replace("{0}", "#<subject>#");
			text = text.Replace("{1}", "#<course>#");
			text = text.Replace("{2}", "#<section>#");
			text = text.Replace("{3}", "#<timeofday>#");
			text = text.Replace("{4}", "#<startdate>#");
			text = text.Replace("{5}", "#<enddate>#");
			text = text.Replace("{6}", "#<duration>#");
			text = text.Replace("{7}", "#<term>#");
			return string.Format(text, new object[]
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

		// Token: 0x060006FD RID: 1789 RVA: 0x00027EF8 File Offset: 0x00026EF8
		public bool IsSameCourse(Course course)
		{
			return string.Equals(this.duration, course.Duration, StringComparison.CurrentCultureIgnoreCase) && string.Equals(this.term, course.Term, StringComparison.CurrentCultureIgnoreCase) && string.Equals(this.subject, course.Subject, StringComparison.CurrentCultureIgnoreCase) && string.Equals(this.timeOfDay, course.TimeOfDay, StringComparison.CurrentCultureIgnoreCase) && string.Equals(this.section, course.Section, StringComparison.CurrentCultureIgnoreCase) && !(course.EndDate <= this.startDate) && !(course.startDate >= this.EndDate);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00027FA0 File Offset: 0x00026FA0
		public static List<Course> LoadStudentsCoursesForDataSync(int pid, DateTime startDate, DateTime endDate)
		{
			UnivDataAdapter da = ClientCache.CurrentInstance.da;
			TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
			string commandText = "SELECT    p.personid,c.coursesid,c.registrationstatus\r\n            ,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,lucd.altlookupstring AS subject\r\n            ,luc.course,luc.timeofday,luc.[section]\r\n            ,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone\r\n            ,lucd2.username AS instructorusername,lucd2.id AS instructorid\r\n            ,luc.alternatecontactid,alt.altname,alt.altemail,alt.altphone,alt.altusername,alt.alternatecontactid\r\n            ,tt.*\r\nFROM        people p LEFT JOIN courses c ON c.personid=p.personid\r\n            LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n            LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\n            LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid\r\n            LEFT JOIN lucoursealternatecontact alt ON alt.alternatecontactid=luc.alternatecontactid\r\n            LEFT JOIN timetable tt ON tt.lucourseid=c.lucourseid\r\nWHERE       p.isactive=1 AND p.personid=@pid\r\n            AND NOT ( ( luc.enddate<@startdate ) OR (luc.startdate > @enddate ) )\r\nORDER BY    c.lucourseid";
			da.SelectCommand.CommandText = commandText;
			da.SelectCommand.Parameters.Clear();
			da.SelectCommand.Parameters.Add("@pid", pid);
			da.SelectCommand.Parameters.Add("@startdate", startDate);
			da.SelectCommand.Parameters.Add("@enddate", endDate);
			DataTable dataTable = new DataTable();
			da.Fill(dataTable);
			return Course.GetCoursesFromCoursesTable(dataTable.DefaultView);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00028060 File Offset: 0x00027060
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
					if (!course2.IsSameCourse(course))
					{
						break;
					}
				}
				Course item = new Course(dv[i].Row);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0002810C File Offset: 0x0002710C
		public bool Drop(int pid)
		{
			bool result;
			if (this.luCourseId > 0 && pid > 0)
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string commandText = "UPDATE courses SET registrationstatus=2 WHERE lucourseid=@lucid AND personid=@pid";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@lucid", this.luCourseId);
				da.SelectCommand.Parameters.Add("@pid", pid);
				da.Fill(new DataTable());
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000281B4 File Offset: 0x000271B4
		public bool UnDrop(int pid)
		{
			bool result;
			if (this.luCourseId > 0 && pid > 0)
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string commandText = "UPDATE courses SET registrationstatus=1 WHERE lucourseid=@lucid AND personid=@pid";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@lucid", this.luCourseId);
				da.SelectCommand.Parameters.Add("@pid", pid);
				da.Fill(new DataTable());
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0002825C File Offset: 0x0002725C
		public bool AddToStudentList(int pid, int whoAdded)
		{
			bool result;
			if (this.luCourseId > 0 && pid > 0)
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string commandText = "IF NOT EXISTS(SELECT coursesid FROM courses WHERE personid=@pid AND lucourseid=@lucid)\r\n    INSERT INTO courses (dateadded,whoadded,registrationstatus,pid,lucourseid) VALUES (getdate(),@whoadded,1,@pid,@lucourseid)";
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@lucid", this.luCourseId);
				da.SelectCommand.Parameters.Add("@pid", pid);
				da.SelectCommand.Parameters.Add("@whoadded", whoAdded);
				da.Fill(new DataTable());
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0002835C File Offset: 0x0002735C
		public int LookupLuCourseId(out bool createdCourse)
		{
			createdCourse = false;
			int result;
			if (this.luCourseId > 0)
			{
				result = this.luCourseId;
			}
			else if (string.IsNullOrEmpty(this.subject))
			{
				result = 0;
			}
			else if (string.IsNullOrEmpty(this.term))
			{
				result = 0;
			}
			else if (string.IsNullOrEmpty(this.courseCode))
			{
				result = 0;
			}
			else if (this.startDate == DateTime.MinValue || this.endDate == DateTime.MinValue)
			{
				result = 0;
			}
			else
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				string commandText = "SELECT luc.lucourseid FROM lucourses luc LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid\r\nWHERE luc.duration=@duration AND luc.term=@term AND lucd.altlookupstring=@subject AND luc.[section]=@section\r\n    AND luc.timeofday=@timeofday AND luc.campus=@campus AND luc.department=@department\r\n    AND NOT ( ( luc.enddate<@sdate ) OR (luc.startdate > @edate ) )";
				DataTable dataTable = new DataTable();
				da.SelectCommand.CommandText = commandText;
				da.SelectCommand.Parameters.Clear();
				da.SelectCommand.Parameters.Add("@duration", this.duration);
				da.SelectCommand.Parameters.Add("@term", this.term);
				da.SelectCommand.Parameters.Add("@subject", this.subject);
				da.SelectCommand.Parameters.Add("@timeofday", this.timeOfDay);
				da.SelectCommand.Parameters.Add("@section", this.section);
				da.SelectCommand.Parameters.Add("@sdate", this.startDate);
				da.SelectCommand.Parameters.Add("@edate", this.endDate);
				da.SelectCommand.Parameters.Add("@campus", this.campus);
				da.SelectCommand.Parameters.Add("@department", this.department);
				da.Fill(dataTable);
				if (dataTable.Rows.Count > 0 && dataTable.Rows[0][0] != DBNull.Value)
				{
					this.luCourseId = (int)dataTable.Rows[0][0];
					result = this.luCourseId;
				}
				else
				{
					createdCourse = true;
					commandText = "DECLARE @subjectid int\r\nIF EXISTS(SELECT lucoursedataid FROM lucoursedata WHERE altlookupstring=@subject)\r\n    SELECT TOP 1 lucoursedataid AS subjectid FROM lucoursedata WHERE altlookupstring=@subject\r\nELSE\r\nBEGIN\r\n    INSERT INTO lucoursedata (lookuplisttype,lookupstring,altlookupstring) VALUES (0,@subject,@subject);\r\n    SELECT CAST(SCOPE_IDENTITY() AS int) AS subjectid\r\nEND";
					DataTable dataTable2 = new DataTable();
					da.SelectCommand.CommandText = commandText;
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@subject", this.subject);
					da.Fill(dataTable2);
					if (dataTable2.Rows.Count < 1)
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
						commandText = "INSERT INTO lucourses (startdate,enddate,term,duration,subjectid,course,timeofday,[section],instructorid,crosslistcode,equivalentcode,coursenote,whoadded,dateadded,location,alternatecontactid,instructorpermissionlevel,campus,department)\r\n    VALUES (@sdate,@edate,@term,@duration,@subjectid,@course,@timeofday,@section,@instructorid,-1,-1,'',-556,getdate(),@location,@alternatecontactid,256,@campus,@department);\r\nSELECT CAST(SCOPE_IDENTITY() AS int) AS lucourseid;";
						DataTable dataTable3 = new DataTable();
						da.SelectCommand.CommandText = commandText;
						da.SelectCommand.Parameters.Clear();
						da.SelectCommand.Parameters.Add("@sdate", this.startDate);
						da.SelectCommand.Parameters.Add("@edate", this.endDate);
						da.SelectCommand.Parameters.Add("@term", this.term);
						da.SelectCommand.Parameters.Add("@duration", this.duration);
						da.SelectCommand.Parameters.Add("@subjectid", num);
						da.SelectCommand.Parameters.Add("@course", this.courseCode);
						da.SelectCommand.Parameters.Add("@timeofday", this.timeOfDay);
						da.SelectCommand.Parameters.Add("@section", this.section);
						Instructor instructor2 = this.instructors.Find((Instructor e) => e.InstructorId > 0);
						da.SelectCommand.Parameters.Add("@instructorid", (instructor2 == null) ? -1 : instructor2.InstructorId);
						da.SelectCommand.Parameters.Add("@location", this.location);
						CourseContactInformation courseContactInformation2 = this.altContacts.Find((CourseContactInformation f) => f.AlternateContactId > 0);
						da.SelectCommand.Parameters.Add("@alternatecontactid", (courseContactInformation2 == null) ? -1 : courseContactInformation2.AlternateContactId);
						da.SelectCommand.Parameters.Add("@campus", this.campus);
						da.SelectCommand.Parameters.Add("@department", this.department);
						da.Fill(dataTable3);
						if (dataTable3.Rows.Count < 1 || dataTable3.Rows[0][0] == DBNull.Value)
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
			return result;
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x00028958 File Offset: 0x00027958
		// (set) Token: 0x06000705 RID: 1797 RVA: 0x00028970 File Offset: 0x00027970
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

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x0002897C File Offset: 0x0002797C
		// (set) Token: 0x06000707 RID: 1799 RVA: 0x00028994 File Offset: 0x00027994
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

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x000289A0 File Offset: 0x000279A0
		// (set) Token: 0x06000709 RID: 1801 RVA: 0x000289B8 File Offset: 0x000279B8
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

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600070A RID: 1802 RVA: 0x000289C4 File Offset: 0x000279C4
		// (set) Token: 0x0600070B RID: 1803 RVA: 0x000289DC File Offset: 0x000279DC
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

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x000289E8 File Offset: 0x000279E8
		// (set) Token: 0x0600070D RID: 1805 RVA: 0x00028A00 File Offset: 0x00027A00
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

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600070E RID: 1806 RVA: 0x00028A0C File Offset: 0x00027A0C
		// (set) Token: 0x0600070F RID: 1807 RVA: 0x00028A24 File Offset: 0x00027A24
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

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x00028A30 File Offset: 0x00027A30
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x00028A48 File Offset: 0x00027A48
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

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x00028A54 File Offset: 0x00027A54
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x00028A6C File Offset: 0x00027A6C
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

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x00028A78 File Offset: 0x00027A78
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x00028A90 File Offset: 0x00027A90
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

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00028A9C File Offset: 0x00027A9C
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x00028AB4 File Offset: 0x00027AB4
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

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00028AC0 File Offset: 0x00027AC0
		// (set) Token: 0x06000719 RID: 1817 RVA: 0x00028AD8 File Offset: 0x00027AD8
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

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00028AE4 File Offset: 0x00027AE4
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x00028AFC File Offset: 0x00027AFC
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

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x00028B08 File Offset: 0x00027B08
		public string SubjectEmail
		{
			get
			{
				return this.subjectEmail;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00028B20 File Offset: 0x00027B20
		// (set) Token: 0x0600071E RID: 1822 RVA: 0x00028B38 File Offset: 0x00027B38
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

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00028B44 File Offset: 0x00027B44
		// (set) Token: 0x06000720 RID: 1824 RVA: 0x00028B5C File Offset: 0x00027B5C
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

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000721 RID: 1825 RVA: 0x00028B68 File Offset: 0x00027B68
		// (set) Token: 0x06000722 RID: 1826 RVA: 0x00028B80 File Offset: 0x00027B80
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

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000723 RID: 1827 RVA: 0x00028B8C File Offset: 0x00027B8C
		public CourseContactInformation AlternateContact
		{
			get
			{
				return this.alternateContact;
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00028BA4 File Offset: 0x00027BA4
		public int CompareTo(object obj)
		{
			int result;
			if (obj == null)
			{
				result = -1;
			}
			else if (obj is Course)
			{
				Course c = (Course)obj;
				if (Course.CompareCourses(c, this, false, true, true, true, true))
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
			return result;
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00028BFC File Offset: 0x00027BFC
		public static bool CompareCourses(Course c1, Course c2, bool compareDates, bool compareTermDuration, bool compareSection, bool compareTimeOfDay, bool compareInstructorEmail)
		{
			bool result;
			if (compareDates && !Course.CompareDatesYearMonthDayOnly(c1.StartDate, c2.StartDate, false))
			{
				result = false;
			}
			else if (compareDates && !Course.CompareDatesYearMonthDayOnly(c1.EndDate, c2.EndDate, false))
			{
				result = false;
			}
			else if (compareTermDuration && !Course.CompareStrings(c1.Term, c2.Term))
			{
				result = false;
			}
			else if (compareTermDuration && !Course.CompareStrings(c1.Duration, c2.Duration))
			{
				result = false;
			}
			else if (!Course.CompareStrings(c1.Subject, c2.Subject))
			{
				result = false;
			}
			else if (!Course.CompareStrings(c1.CourseCode, c2.CourseCode))
			{
				result = false;
			}
			else
			{
				if (compareSection && !Course.CompareStrings(c1.Section, c2.Section))
				{
					int num;
					int num2;
					if (!Course.ParseString(c1.Section, out num) || !Course.ParseString(c2.Section, out num2))
					{
						return false;
					}
					if (num != num2)
					{
						return false;
					}
				}
				result = ((!compareTimeOfDay || Course.CompareStrings(c1.TimeOfDay, c2.TimeOfDay)) && (!compareInstructorEmail || Course.CompareStrings(c1.InstructorEmail, c2.InstructorEmail)));
			}
			return result;
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00028D6C File Offset: 0x00027D6C
		private static bool CompareStrings(string s1, string s2)
		{
			return Course.GetCompareString(s1).CompareTo(Course.GetCompareString(s2)) == 0;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00028D94 File Offset: 0x00027D94
		private static string GetCompareString(string s)
		{
			return s.Trim().ToLower();
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00028DB4 File Offset: 0x00027DB4
		private static bool ParseString(string str, out int number)
		{
			string text = str.Trim();
			bool result;
			if (text.Length > 0)
			{
				string text2 = "";
				foreach (char c in text)
				{
					if (!char.IsDigit(c))
					{
						number = 0;
						return false;
					}
					text2 += c;
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

		// Token: 0x06000729 RID: 1833 RVA: 0x00028E48 File Offset: 0x00027E48
		private static bool CompareDatesYearMonthDayOnly(DateTime d1, DateTime d2, bool compareDay)
		{
			return d1.Year == d2.Year && d1.Month == d2.Month && (!compareDay || d1.Day == d2.Day);
		}

		// Token: 0x04000385 RID: 901
		public const int REGISTRATIONSTATUS_DROPPED = 2;

		// Token: 0x04000386 RID: 902
		public const int REGISTRATIONSTATUS_EXEMPTFROMDATASYNC = 8;

		// Token: 0x04000387 RID: 903
		private int luCourseId = -1;

		// Token: 0x04000388 RID: 904
		private string term;

		// Token: 0x04000389 RID: 905
		private string duration;

		// Token: 0x0400038A RID: 906
		private string subject;

		// Token: 0x0400038B RID: 907
		private string courseCode;

		// Token: 0x0400038C RID: 908
		private string timeOfDay;

		// Token: 0x0400038D RID: 909
		private string section;

		// Token: 0x0400038E RID: 910
		private string instructorName;

		// Token: 0x0400038F RID: 911
		private string instructorEmail;

		// Token: 0x04000390 RID: 912
		private string instructorPhone;

		// Token: 0x04000391 RID: 913
		private string instructorUsername;

		// Token: 0x04000392 RID: 914
		private string subjectEmail;

		// Token: 0x04000393 RID: 915
		private DateTime startDate;

		// Token: 0x04000394 RID: 916
		private DateTime endDate;

		// Token: 0x04000395 RID: 917
		private CourseContactInformation alternateContact = null;

		// Token: 0x04000396 RID: 918
		private int registrationStatus = 0;

		// Token: 0x04000397 RID: 919
		private string campus = "";

		// Token: 0x04000398 RID: 920
		private string location = "";

		// Token: 0x04000399 RID: 921
		private string department = "";

		// Token: 0x0400039A RID: 922
		private List<Instructor> instructors;

		// Token: 0x0400039B RID: 923
		private List<CourseContactInformation> altContacts;

		// Token: 0x0400039C RID: 924
		private static string courseDisplayTemplateString = "";

		// Token: 0x0200008F RID: 143
		public enum CourseContactPermissionLevel
		{
			// Token: 0x040003A1 RID: 929
			None,
			// Token: 0x040003A2 RID: 930
			Receive_emails,
			// Token: 0x040003A3 RID: 931
			Update_test_info_online,
			// Token: 0x040003A4 RID: 932
			Update_test_info_online_and_receive_emails,
			// Token: 0x040003A5 RID: 933
			View_accommodation_letters_online,
			// Token: 0x040003A6 RID: 934
			View_accommodation_letters_online_and_receive_emails,
			// Token: 0x040003A7 RID: 935
			View_accommodation_letters_online_and_update_test_info_online,
			// Token: 0x040003A8 RID: 936
			View_accommodation_letters_online_and_update_test_info_online_and_receive_emails
		}
	}
}
