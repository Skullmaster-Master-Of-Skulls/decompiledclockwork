using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using ClockWorkLogger;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace ClockWorkWebAPI
{
	// Token: 0x02000012 RID: 18
	[Serializable]
	public class Course
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000789F File Offset: 0x00005A9F
		// (set) Token: 0x060000FA RID: 250 RVA: 0x000078A7 File Offset: 0x00005AA7
		public string CourseName { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000078B0 File Offset: 0x00005AB0
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000078B8 File Offset: 0x00005AB8
		public string CourseVal { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000078C4 File Offset: 0x00005AC4
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000078DC File Offset: 0x00005ADC
		public string CourseSection
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

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000078E8 File Offset: 0x00005AE8
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00007900 File Offset: 0x00005B00
		public string TimeOfDay
		{
			get
			{
				return this.timeofday;
			}
			set
			{
				this.timeofday = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000790A File Offset: 0x00005B0A
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00007912 File Offset: 0x00005B12
		public string SubjectCode { get; set; }

		// Token: 0x06000103 RID: 259 RVA: 0x0000791C File Offset: 0x00005B1C
		public static List<Course> ParseCourses(List<Course> apiCourses)
		{
			List<Course> list = new List<Course>();
			foreach (Course course in apiCourses)
			{
				Course course2 = new Course();
				string duration = course.Duration;
				string term = course.Term;
				string subject = course.Subject;
				string courseCode = course.CourseCode;
				string section = course.Section;
				string timeOfDay = course.TimeOfDay;
				string campus = course.Campus;
				string department = course.Department;
				string location = course.Location;
				string subjectCode = course.SubjectCode;
				DateTime sd = course.StartDate;
				DateTime ed = course.EndDate;
				Course course3 = list.Find((Course c) => c.Term.Equals(term, StringComparison.OrdinalIgnoreCase) && c.Duration.Equals(duration, StringComparison.OrdinalIgnoreCase) && c.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase) && c.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase) && c.CourseSection.Equals(section, StringComparison.OrdinalIgnoreCase) && c.TimeOfDay.Equals(timeOfDay, StringComparison.OrdinalIgnoreCase) && !(ed <= c.StartDate) && !(sd > c.EndDate));
				bool flag = course3 == null;
				if (flag)
				{
					string courseName = string.Format("{0} {1} {2} . {3}", new object[]
					{
						subject,
						courseCode,
						timeOfDay,
						section
					});
					string courseVal = string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}", new object[]
					{
						duration.Replace("~", ""),
						term.Replace("~", ""),
						subject.Replace("~", ""),
						courseCode.Replace("~", ""),
						section.Replace("~", ""),
						timeOfDay.Replace("~", ""),
						campus.Replace("~", ""),
						department.Replace("~", ""),
						location.Replace("~", ""),
						subjectCode.Replace("~", "")
					});
					list.Add(new Course
					{
						Subject = subject,
						SubjectCode = subjectCode,
						CourseCode = courseCode,
						CourseSection = section,
						TimeOfDay = timeOfDay,
						CourseName = courseName,
						CourseVal = courseVal,
						StartDate = sd,
						EndDate = ed,
						Duration = duration,
						Term = term,
						Campus = campus,
						Department = department,
						Location = location
					});
				}
			}
			return list;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007C20 File Offset: 0x00005E20
		public static List<Course> ParseCourses(DataTable studentCourses)
		{
			List<Course> list = new List<Course>();
			bool flag = studentCourses != null;
			if (flag)
			{
				bool flag2 = !studentCourses.Columns.Contains("duration");
				if (flag2)
				{
					studentCourses.Columns.Add("duration");
				}
				bool flag3 = !studentCourses.Columns.Contains("timeofday");
				if (flag3)
				{
					studentCourses.Columns.Add("timeofday");
				}
				bool flag4 = !studentCourses.Columns.Contains("campus");
				if (flag4)
				{
					studentCourses.Columns.Add("campus");
				}
				bool flag5 = !studentCourses.Columns.Contains("department");
				if (flag5)
				{
					studentCourses.Columns.Add("department");
				}
				bool flag6 = !studentCourses.Columns.Contains("location");
				if (flag6)
				{
					studentCourses.Columns.Add("location");
				}
				bool flag7 = !studentCourses.Columns.Contains("subjectcode");
				if (flag7)
				{
					studentCourses.Columns.Add("subjectcode");
				}
				foreach (object obj in studentCourses.Rows)
				{
					DataRow dataRow = (DataRow)obj;
					string duration = dataRow["duration"].ToString().Trim();
					string term = dataRow["term"].ToString().Trim();
					string subject = dataRow["subject"].ToString().Trim();
					string subjectCode = dataRow["subjectcode"].ToString().Trim();
					string courseCode = dataRow["course"].ToString().Trim();
					string section = dataRow["section"].ToString().Trim();
					string timeOfDay = dataRow["timeofday"].ToString().Trim();
					string text = dataRow["campus"].ToString().Trim();
					string text2 = dataRow["department"].ToString().Trim();
					string text3 = dataRow["location"].ToString().Trim();
					string s = dataRow["startdate"].ToString();
					string s2 = dataRow["enddate"].ToString();
					DateTime ed;
					DateTime sd;
					bool flag8 = DateTime.TryParse(s, out sd) && DateTime.TryParse(s2, out ed);
					if (flag8)
					{
						Course course = list.Find((Course c) => c.Term.Equals(term, StringComparison.OrdinalIgnoreCase) && c.Duration.Equals(duration, StringComparison.OrdinalIgnoreCase) && c.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase) && c.CourseCode.Equals(courseCode, StringComparison.OrdinalIgnoreCase) && c.CourseSection.Equals(section, StringComparison.OrdinalIgnoreCase) && c.TimeOfDay.Equals(timeOfDay, StringComparison.OrdinalIgnoreCase) && !(ed <= c.StartDate) && !(sd > c.EndDate));
						bool flag9 = course == null;
						if (flag9)
						{
							string courseName = string.Format("{0} {1} {2} . {3}", new object[]
							{
								subject,
								courseCode,
								timeOfDay,
								section
							});
							string courseVal = string.Format("{0}~{1}~{2}~{3}~{4}~{5}~{6}~{7}~{8}~{9}", new object[]
							{
								duration.Replace("~", ""),
								term.Replace("~", ""),
								subject.Replace("~", ""),
								courseCode.Replace("~", ""),
								section.Replace("~", ""),
								timeOfDay.Replace("~", ""),
								text.Replace("~", ""),
								text2.Replace("~", ""),
								text3.Replace("~", ""),
								subject.Replace("~", "")
							});
							list.Add(new Course
							{
								Subject = subject,
								SubjectCode = subjectCode,
								CourseCode = courseCode,
								CourseSection = section,
								TimeOfDay = timeOfDay,
								CourseName = courseName,
								CourseVal = courseVal,
								StartDate = sd,
								EndDate = ed,
								Duration = duration,
								Term = term,
								Campus = text,
								Department = text2,
								Location = text3
							});
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000812C File Offset: 0x0000632C
		public static DataTable LoadStudentsCourse(db conn, int pid, int lucid)
		{
			conn.Da.SelectCommand.CommandText = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session FROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid WHERE\tc.personid=@personid AND c.lucourseid=@lucid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) ORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.AddWithValue("@personid", pid);
			conn.Da.SelectCommand.Parameters.AddWithValue("@lucid", lucid);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000081C0 File Offset: 0x000063C0
		public static DataTable LoadStudentsCourseWithStudentName(db conn, int pid, int lucid)
		{
			return Course.LoadStudentsCourseWithStudentName(pid, lucid);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000081DC File Offset: 0x000063DC
		public static DataTable LoadStudentsCourseWithStudentName(int pid, int lucid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, p.firstname,p.lastname,p.student_no FROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid LEFT JOIN people p ON p.personid=c.personid WHERE\tc.personid=@personid AND c.lucourseid=@lucid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) ORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@personid", DbType.Int32, pid),
				clockWork.GetParameter("@lucid", DbType.Int32, lucid)
			};
			DataTable tSource = clockWork.ExecuteQuery(query, parameters);
			return clockWork.Encryption.EncryptOrDecryptNameDataTableBatch(false, tSource, new string[]
			{
				"firstname",
				"lastname",
				"student_no"
			});
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00008268 File Offset: 0x00006468
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00008280 File Offset: 0x00006480
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000828C File Offset: 0x0000648C
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000082A4 File Offset: 0x000064A4
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000082B0 File Offset: 0x000064B0
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000082C8 File Offset: 0x000064C8
		public Person Instructor
		{
			get
			{
				return this.instructor;
			}
			set
			{
				this.instructor = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000082D4 File Offset: 0x000064D4
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000082EC File Offset: 0x000064EC
		public string Name_new
		{
			get
			{
				return this.name_new;
			}
			set
			{
				this.name_new = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000082F8 File Offset: 0x000064F8
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00008310 File Offset: 0x00006510
		public string Email_new
		{
			get
			{
				return this.email_new;
			}
			set
			{
				this.email_new = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000831C File Offset: 0x0000651C
		public int SubjectId
		{
			get
			{
				return this.subjectId;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00008334 File Offset: 0x00006534
		// (set) Token: 0x06000114 RID: 276 RVA: 0x0000834C File Offset: 0x0000654C
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00008358 File Offset: 0x00006558
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00008370 File Offset: 0x00006570
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

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000837C File Offset: 0x0000657C
		public DateTime OriginalStartDateTime
		{
			get
			{
				return this.originalStartDateTime;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00008394 File Offset: 0x00006594
		public DateTime OriginalEndDateTime
		{
			get
			{
				return this.originalEndDateTime;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000119 RID: 281 RVA: 0x000083AC File Offset: 0x000065AC
		// (set) Token: 0x0600011A RID: 282 RVA: 0x000083C4 File Offset: 0x000065C4
		public string OriginalDateTime
		{
			get
			{
				return this.originalDateTime;
			}
			set
			{
				this.originalDateTime = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600011B RID: 283 RVA: 0x000083D0 File Offset: 0x000065D0
		// (set) Token: 0x0600011C RID: 284 RVA: 0x000083E8 File Offset: 0x000065E8
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011D RID: 285 RVA: 0x000083F4 File Offset: 0x000065F4
		// (set) Token: 0x0600011E RID: 286 RVA: 0x0000840C File Offset: 0x0000660C
		public string SubjectEmail
		{
			get
			{
				return this.subjectEmail;
			}
			set
			{
				this.subjectEmail = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00008418 File Offset: 0x00006618
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00008430 File Offset: 0x00006630
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000843C File Offset: 0x0000663C
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00008454 File Offset: 0x00006654
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

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00008460 File Offset: 0x00006660
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00008487 File Offset: 0x00006687
		public string Term
		{
			get
			{
				return (this.term == null) ? "" : this.term;
			}
			set
			{
				this.term = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00008494 File Offset: 0x00006694
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000084BB File Offset: 0x000066BB
		public string Duration
		{
			get
			{
				return (this.duration == null) ? "" : this.duration;
			}
			set
			{
				this.duration = value;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000084C5 File Offset: 0x000066C5
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000084CD File Offset: 0x000066CD
		public string Campus { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000084D6 File Offset: 0x000066D6
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000084DE File Offset: 0x000066DE
		public string Department { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000084E7 File Offset: 0x000066E7
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000084EF File Offset: 0x000066EF
		public string Location { get; set; }

		// Token: 0x0600012D RID: 301 RVA: 0x000084F8 File Offset: 0x000066F8
		public Course()
		{
			this.subject = "";
			this.courseCode = "";
			this.term = "";
			this.section = "";
			this.duration = "";
			this.term = "";
			this.startDate = DateTime.MinValue;
			this.endDate = DateTime.MinValue;
			this.description = "";
			this.instructor = null;
			this.subjectId = 0;
			this.timeofday = "";
			this.originalStartDateTime = DateTime.MinValue;
			this.originalEndDateTime = DateTime.MinValue;
			this.subjectEmail = "";
			this.originalDateTime = "";
			this.luCourseId = 0;
			this.Campus = "";
			this.Location = "";
			this.Department = "";
			this.SubjectCode = "";
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000860C File Offset: 0x0000680C
		public Course(DataRow dr)
		{
			this.luCourseId = (int)dr["lucourseid"];
			try
			{
				this.subject = dr["subject"].ToString().Trim();
				this.courseCode = dr["course"].ToString().Trim();
				this.timeofday = dr["timeofday"].ToString().Trim();
				this.section = dr["section"].ToString().Trim();
				this.term = dr["term"].ToString().Trim();
				this.duration = dr["duration"].ToString().Trim();
				this.startDate = (DateTime)dr["startdate"];
				bool flag = dr.Table.Columns.Contains("enddate");
				if (flag)
				{
					this.endDate = (DateTime)dr["enddate"];
				}
				this.description = string.Format("{0} {1} {2} {3} (Term: {4} {5})", new object[]
				{
					this.subject,
					this.courseCode,
					this.section,
					this.timeofday,
					this.term,
					this.startDate.ToString("MMM yyyy")
				});
				string name = dr.Table.Columns.Contains("instructor") ? dr["instructor"].ToString().Trim() : "";
				string text = dr.Table.Columns.Contains("instructorphone") ? dr["instructorphone"].ToString().Trim() : "";
				string email = dr.Table.Columns.Contains("instructoremail") ? dr["instructoremail"].ToString().Trim() : "";
				int personid = (dr.Table.Columns.Contains("instructorid") && !(dr["instructorid"] is DBNull)) ? ((int)dr["instructorid"]) : 0;
				this.instructor = new Person(personid, name, email);
				this.subjectId = ((!dr.Table.Columns.Contains("subjectid") || dr["subjectid"] == DBNull.Value) ? -1 : ((int)dr["subjectid"]));
				this.timeofday = ((!dr.Table.Columns.Contains("timeofday")) ? "" : dr["timeofday"].ToString());
				this.originalStartDateTime = ((!dr.Table.Columns.Contains("originalstartdatetime") || dr["originalstartdatetime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["originalstartdatetime"]));
				this.originalEndDateTime = ((!dr.Table.Columns.Contains("originalenddatetime") || dr["originalenddatetime"] == DBNull.Value) ? DateTime.MinValue : ((DateTime)dr["originalenddatetime"]));
				this.subjectEmail = ((!dr.Table.Columns.Contains("subjectemail") || dr["subjectemail"] == DBNull.Value) ? "" : dr["subjectemail"].ToString());
				this.originalDateTime = "";
				this.Campus = (dr.Table.Columns.Contains("campus") ? dr["campus"].ToString().Trim() : "");
				this.Department = (dr.Table.Columns.Contains("department") ? dr["department"].ToString().Trim() : "");
				this.Location = (dr.Table.Columns.Contains("location") ? dr["location"].ToString().Trim() : "");
				this.SubjectCode = (dr.Table.Columns.Contains("subjectcode") ? dr["subjectcode"].ToString().Trim() : "");
			}
			catch (Exception ex)
			{
				this.description = "?";
				bool flag2 = this.SubjectCode == null;
				if (flag2)
				{
					this.SubjectCode = "";
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008B04 File Offset: 0x00006D04
		public override string ToString()
		{
			return this.description + " [" + this.luCourseId.ToString() + "]";
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008B38 File Offset: 0x00006D38
		public static string CourseToString(LookupCourseDTO course)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(course.Subject.SubjectDescription);
			stringBuilder.Append(" ");
			stringBuilder.Append(course.Course ?? "");
			string text = course.TimeOfDay ?? "";
			bool flag = text.Length > 0;
			if (flag)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(text);
			}
			stringBuilder.Append(" ");
			stringBuilder.Append(course.Section ?? "");
			return stringBuilder.ToString();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008BE0 File Offset: 0x00006DE0
		public static string CourseToString(DataRow dr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(dr["subject"].ToString());
			stringBuilder.Append(" ");
			stringBuilder.Append(dr["course"].ToString());
			string text = dr["timeofday"].ToString();
			bool flag = text.Length > 0;
			if (flag)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(text);
			}
			stringBuilder.Append(" ");
			stringBuilder.Append(dr["section"].ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008C90 File Offset: 0x00006E90
		public static DataTable LoadStudentsCourses(db conn, int pid, DateTime startDate, DateTime endDate)
		{
			conn.Da.SelectCommand.CommandText = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session FROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid WHERE\tc.personid=@personid AND luc.enddate >= @startdate AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) ORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.AddWithValue("@personid", pid);
			conn.Da.SelectCommand.Parameters.AddWithValue("@startdate", startDate);
			conn.Da.SelectCommand.Parameters.AddWithValue("@enddate", endDate);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008D48 File Offset: 0x00006F48
		public static List<Course> LoadCoursesCurrentTerm(int pid)
		{
			DateTime dateTime;
			DateTime dateTime2;
			Utility.GetTermStartEndDates(out dateTime, out dateTime2);
			DataTable dataTable = Course.LoadStudentsCourses(pid, dateTime, dateTime2);
			List<Course> list = new List<Course>(dataTable.Rows.Count);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				Course item = new Course(dr);
				list.Add(item);
			}
			return list;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008DE4 File Offset: 0x00006FE4
		public static DataTable LoadStudentsCourses(int pid, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\n    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n    luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n    lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n    lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\n    ,c.datestudentlastviewed\r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\n    LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n    LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid AND luc.enddate >= @startdate AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			return clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@personid", DbType.Int32, pid),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			});
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00008E58 File Offset: 0x00007058
		public static DataTable LoadStudentsCourse(db conn, int pid, int lucid, DateTime startDate, DateTime endDate)
		{
			conn.Da.SelectCommand.CommandText = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session FROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid WHERE\tc.personid=@personid AND luc.enddate >= @startdate AND c.lucourseid=@lucid AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) ORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.AddWithValue("@personid", pid);
			conn.Da.SelectCommand.Parameters.AddWithValue("@lucid", lucid);
			conn.Da.SelectCommand.Parameters.AddWithValue("@startdate", startDate);
			conn.Da.SelectCommand.Parameters.AddWithValue("@enddate", endDate);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			return dataTable;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008F30 File Offset: 0x00007130
		public static Course LoadStudentsCourse(int pid, int lucid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\n    luc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\n    luc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\n    lucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\n    lucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid\r\n    LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\n    LEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@personid AND c.lucourseid=@lucid --AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@personid", DbType.Int32, pid),
				clockWork.GetParameter("@lucid", DbType.Int32, lucid)
			});
			bool flag = dataTable.Rows.Count > 0;
			Course result;
			if (flag)
			{
				Course course = new Course(dataTable.Rows[0]);
				result = course;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008FBC File Offset: 0x000071BC
		public static string GetCourseDescription(DataRow dr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append((dr["subject"] == DBNull.Value) ? "" : ((string)dr["subject"] + " "));
			stringBuilder.Append((dr["course"] == DBNull.Value) ? "" : ((string)dr["course"] + " . "));
			stringBuilder.Append((dr["section"] == DBNull.Value) ? "" : ((string)dr["section"]));
			return stringBuilder.ToString();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009080 File Offset: 0x00007280
		public static DataTable LoadInstructorsCourses(int iid, out DateTime startDate, out DateTime endDate)
		{
			Core.GetTermStartEndDates(out startDate, out endDate);
			return Course.LoadInstructorsCourses2(iid, startDate, endDate);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000090AC File Offset: 0x000072AC
		public static DataTable LoadInstructorsCourses2(int iid, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session, \r\nlucd.altlookupstring + ' ' + luc.course + luc.timeofday + ' ' + luc.section AS coursedescription \r\nFROM lucourses luc \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tluc.instructorid=@iid AND luc.enddate >= @startdate \r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			return clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@iid", DbType.Int32, iid),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			});
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00009120 File Offset: 0x00007320
		public static void DeleteUploadedExam(int examid, int examfileid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "DECLARE @examid int\r\nSET @examid=(SELECT TOP 1 examid FROM examfiles WHERE examfileid=@examfileid)\r\nIF @examid>0\r\nBEGIN   \r\n    UPDATE examfiles SET visible=0 WHERE examfileid=@examfileid AND visible=1; \r\n    SET @numleft=(SELECT COUNT(*) FROM examfiles WHERE examid=@examid AND visible=1)\r\nEND\r\nELSE\r\n    SET @numleft=-1";
			DbParameter[] array = new DbParameter[]
			{
				clockWork.GetOutputParameter("@numleft", DbType.Int32, 0),
				clockWork.GetParameter("@examfileid", DbType.Int32, examfileid)
			};
			try
			{
				clockWork.ExecuteNonQuery(query, array);
				bool flag = ((array[0].Value is DBNull) ? -1 : ((int)array[0].Value)) == 0 && examid > 0;
				if (flag)
				{
					Course.UnMarkTestDeliveredOnline(examid);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000091C4 File Offset: 0x000073C4
		public static DataTable LoadUploadedExams(int examId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DataTable result;
			try
			{
				string query = "SELECT ef.examfileid,e.examid,e.dateentered,e.whoentered,e.lucourseid\r\n        ,e.description,ef.filename,ef.filedata,e.dateoftest,e.lastmodified\r\n        ,e.wholastmodified,lucd.altlookupstring as whoenteredname,lucd.email\r\n        ,lucd.phone,ef.dateentered AS datefileentered\r\n        ,lucd0.altlookupstring + ' ' + luc.course + ' . ' + luc.section AS coursedescription\r\n        ,CASE WHEN e.typecode='F' THEN 'Final exam'\r\n             ELSE 'Test' \r\n            END AS TestType\r\nFROM exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid \r\n        LEFT JOIN lucoursedata lucd0 ON lucd0.lucoursedataid=luc.subjectid \r\n        LEFT JOIN examfiles ef ON ef.examid=e.examid \r\n        LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=e.whoentered AND lucd.lookuplisttype=1 \r\nWHERE e.examid=@examid AND ef.visible=@true \r\nORDER BY e.dateoftest,ef.dateentered DESC";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@examid", DbType.Int32, examId),
					clockWork.GetParameter("@true", DbType.Boolean, true)
				});
				result = dataTable;
			}
			catch (Exception ex)
			{
				result = new DataTable();
			}
			return result;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009240 File Offset: 0x00007440
		public static DataTable LoadUploadedExams(int lucid, int instructorId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT\te.examid,e.dateentered,e.whoentered,luc.lucourseid,e.[description]\r\n\t\t,CASE\r\n            WHEN e.whoentered IN (SELECT instructorid FROM vinstructorlist WHERE lucourseid=@lucid) \r\n                    OR e.wholastmodified IN (SELECT instructorid FROM vinstructorlist WHERE lucourseid=@lucid) \r\n                   THEN 'Yes'\r\n            ELSE 'No'\r\n         END AS Submitted\r\n\t\t,e.dateoftest,e.lastmodified,e.wholastmodified\r\n\t\t,lucd.altlookupstring + ' ' + luc.course + ' . ' + luc.section AS coursedescription,luc.startdate,luc.enddate\r\n\t\t,dateadd(n,e.testduration,e.dateentered) AS enddate\r\n\t\t,e.testduration,e.typecode\r\n        ,CASE WHEN e.typecode='F' THEN 'Final exam'\r\n         ELSE 'Test' \r\n        END AS TestType,\r\n        CASE WHEN exists(SELECT ef.examfileid FROM examfiles ef WHERE ef.examid=e.examid AND visible=1) THEN CAST(1 AS bit) \r\n        ELSE CAST(0 AS bit)\r\n        END AS HasFile\r\nFROM\tLUCourses luc INNER JOIN Exams e ON e.lucourseid=luc.LUCourseID \r\n\t\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.SubjectID\r\nWHERE\tluc.LUCourseID=@lucid AND \r\n        (luc.InstructorID=@iid\r\n            OR luc.lucourseid IN (SELECT lucourseid FROM lucourseinstructor WHERE instructorid=@iid)\r\n        )\r\n        AND NOT e.examid IS NULL\r\nORDER BY e.dateoftest DESC";
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, lucid),
				clockWork.GetParameter("@iid", DbType.Int32, instructorId)
			};
			return clockWork.ExecuteQuery(query, parameters);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000092A0 File Offset: 0x000074A0
		public static DataTable LoadUploadedExamsByAltContact(int lucid, int altContactId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "SELECT\te.examid,e.dateentered,e.whoentered,luc.lucourseid,e.[description]\r\n\t\t,CASE\r\n            WHEN e.whoentered IN (SELECT instructorid FROM vinstructorlist WHERE lucourseid=@lucid) \r\n                    OR e.wholastmodified IN (SELECT instructorid FROM vinstructorlist WHERE lucourseid=@lucid) \r\n                   THEN 'Yes'\r\n            ELSE 'No'\r\n         END AS Submitted\r\n\t\t,e.dateoftest,e.lastmodified,e.wholastmodified\r\n\t\t,lucd.altlookupstring + ' ' + luc.course + ' . ' + luc.section AS coursedescription,luc.startdate,luc.enddate\r\n\t\t,dateadd(n,e.testduration,e.dateentered) AS enddate\r\n\t\t,e.testduration,ac.altpermissionlevel,e.typecode\r\n        ,CASE WHEN e.typecode='F' THEN 'Final exam'\r\n         ELSE 'Test' \r\n        END AS TestType,\r\n        CASE WHEN exists(SELECT ef.examfileid FROM examfiles ef WHERE ef.examid=e.examid) THEN CAST(1 AS bit) \r\n        ELSE CAST(0 AS bit)\r\n        END AS HasFile\r\nFROM\tLUCourses luc INNER JOIN Exams e ON e.lucourseid=luc.LUCourseID \r\n\t\tLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.SubjectID\r\n        LEFT JOIN lucoursealternatecontact ac ON ac.alternatecontactid=luc.alternatecontactid\r\nWHERE\tluc.LUCourseID=@lucid\r\n        AND (\r\n            luc.alternatecontactid=@altcontactid\r\n            OR luc.lucourseid IN (SELECT lucourseid FROM lucoursealtcontact WHERE alternatecontactid=@altcontactid)\r\n        )\r\n        AND NOT e.examid IS NULL\r\nORDER BY e.dateoftest DESC";
			return clockWork.ExecuteQuery(query, new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, lucid),
				clockWork.GetParameter("@altcontactid", DbType.Int32, altContactId)
			});
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00009300 File Offset: 0x00007500
		public static DataTable LoadUploadedExams(db conn, DataTable courses)
		{
			string text = "";
			foreach (object obj in courses.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				bool flag = text.Length > 0;
				if (flag)
				{
					text += ",";
				}
				text += dataRow["lucourseid"].ToString();
			}
			DataTable dataTable = new DataTable();
			DataTable result;
			try
			{
				conn.Da.SelectCommand.CommandText = "SELECT \r\n    ef.examfileid,e.examid\r\n    ,CASE\r\n            WHEN e.whoentered IN (SELECT instructorid FROM vinstructorlist WHERE lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))) \r\n                    OR e.wholastmodified IN (SELECT instructorid FROM vinstructorlist WHERE lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,','))) \r\n                   THEN 'Yes'\r\n            ELSE 'No'\r\n         END AS Submitted\r\n    ,e.dateentered,e.whoentered,e.lucourseid,e.description,ef.filename,ef.filedata,e.dateoftest\r\n    ,e.lastmodified,e.wholastmodified,lucd.altlookupstring as whoenteredname,lucd.email,lucd.phone\r\n    ,ef.dateentered AS datefileentered\r\n    ,lucd0.altlookupstring + ' ' + luc.course + ' . ' + luc.section AS coursedescription\r\n    ,dateadd(n,e.testduration,e.dateentered) AS enddate\r\n    ,e.testduration\r\n    FROM exams e LEFT JOIN lucourses luc ON luc.lucourseid=e.lucourseid\r\n    LEFT JOIN lucoursedata lucd0 ON lucd0.lucoursedataid=luc.subjectid \r\n    LEFT JOIN examfiles ef ON ef.examid=e.examid \r\n    LEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=e.whoentered AND lucd.lookuplisttype=1 \r\n    WHERE e.lucourseid IN (SELECT orderid AS lucourseid FROM splitorderids(@lucids,',')) \r\n    ORDER BY e.dateoftest,ef.dateentered DESC";
				conn.Da.SelectCommand.Parameters.Clear();
				conn.Da.SelectCommand.Parameters.AddWithValue("@lucids", text);
				conn.Da.SelectCommand.Parameters.AddWithValue("@true", true);
				conn.Da.Fill(dataTable);
				result = dataTable;
			}
			catch (Exception ex)
			{
				result = dataTable;
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000942C File Offset: 0x0000762C
		public static byte[] DownloadExam(int examfileid, EncryptionCredentials cred, out string filename)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			cred = null;
			byte[] result;
			try
			{
				string query = "SELECT    examid,dateentered,whoentered,description,filename,filedata \r\nFROM        examfiles \r\nWHERE examfileid=@examfileid";
				DataTable dataTable = clockWork.ExecuteQuery(query, new DbParameter[]
				{
					clockWork.GetParameter("@examfileid", DbType.Int32, examfileid)
				});
				bool flag = dataTable.Rows.Count > 0;
				if (flag)
				{
					filename = dataTable.Rows[0]["filename"].ToString();
					byte[] array = (byte[])dataTable.Rows[0]["filedata"];
					bool flag2 = cred != null;
					byte[] array2;
					if (flag2)
					{
						byte[] gzBuffer = Encryption.AESDecrypt2(array, cred.Pass, cred.Salt, cred.Hash, 2, cred.Vector, 256);
						array2 = Compression.Decompress(gzBuffer);
					}
					else
					{
						array2 = array;
					}
					result = array2;
				}
				else
				{
					filename = "";
					result = null;
				}
			}
			catch (Exception ex)
			{
				filename = "";
				result = null;
			}
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009538 File Offset: 0x00007738
		public static void UploadTempExamFileReplace(int examFileId, byte[] fileBytes, string filename)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "UPDATE examfiles SET filename=@filename,filedata=@filedata WHERE examfileid=@examfileid";
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@examfileid", DbType.Int32, examFileId),
				clockWork.GetParameter("@filename", DbType.String, filename),
				clockWork.GetParameter("@filedata", DbType.Binary, fileBytes)
			};
			try
			{
				clockWork.ExecuteNonQuery(query, parameters);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("ClockWorkWebAPI.Course.UploadTempExamFileReplace:examfileid={0}:err={1}", examFileId.ToString(), ex.ToString());
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000095D0 File Offset: 0x000077D0
		public static int UploadTempExamFile(byte[] fileBytes, string filename, int whoAmIPid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "INSERT INTO examfiles (examid,filename,filedata,whoentered,visible) VALUES (@examid,@filename,@filedata,@whoentered,0); SET @examfileid=(SELECT TOP 1 CAST(SCOPE_IDENTITY() AS int) AS examfileid)";
			DbParameter[] array = new DbParameter[]
			{
				clockWork.GetOutputParameter("@examfileid", DbType.Int32, 0),
				clockWork.GetParameter("@examid", DbType.Int32, DBNull.Value),
				clockWork.GetParameter("@filename", DbType.String, filename),
				clockWork.GetParameter("@filedata", DbType.Binary, fileBytes),
				clockWork.GetParameter("@whoentered", DbType.Int32, whoAmIPid)
			};
			try
			{
				clockWork.ExecuteNonQuery(query, array);
				return (array[0].Value is int) ? ((int)array[0].Value) : 0;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("ClockWorkWebAPI.Course.UploadTempExamFile:err={0}", ex.ToString());
			}
			return 0;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000096AC File Offset: 0x000078AC
		public static Exception UploadExamFile(int examId, byte[] fileBytes, string filename, int whoAmIPid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			string query = "INSERT INTO examfiles (examid,filename,filedata,whoentered) VALUES (@examid,@filename,@filedata,@whoentered)";
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@examid", DbType.Int32, examId),
				clockWork.GetParameter("@filename", DbType.String, filename),
				clockWork.GetParameter("@filedata", DbType.Binary, fileBytes),
				clockWork.GetParameter("@whoentered", DbType.Int32, whoAmIPid)
			};
			Exception result;
			try
			{
				clockWork.ExecuteNonQuery(query, parameters);
				Course.MarkTestDeliveredOnline(examId);
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009748 File Offset: 0x00007948
		public static void MarkTestDeliveredOnline(int examId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] array = new DbParameter[2];
			string query = "UPDATE exams SET usercomment=@note WHERE examid=@examid";
			array[0] = clockWork.GetParameter("@note", DbType.String, string.Format("Delivered online {0}", DateTime.Now.ToString("yyyy-MM-dd")));
			array[1] = clockWork.GetParameter("@examid", DbType.Int32, examId);
			clockWork.ExecuteNonQuery(query, array);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000097B4 File Offset: 0x000079B4
		public static void UnMarkTestDeliveredOnline(int examId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] array = new DbParameter[2];
			string query = "UPDATE exams SET usercomment=@note WHERE examid=@examid";
			array[0] = clockWork.GetParameter("@note", DbType.String, "");
			array[1] = clockWork.GetParameter("@examid", DbType.Int32, examId);
			clockWork.ExecuteNonQuery(query, array);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000980C File Offset: 0x00007A0C
		public static Exception UploadExam(int iid, int lucid, string description, DateTime dateOfTest, int testDuration, out int newExamId)
		{
			return Course.UploadExam(iid, lucid, description, dateOfTest, testDuration, "", out newExamId);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009830 File Offset: 0x00007A30
		public static Exception UploadExam(int iid, int lucid, string description, DateTime dateOfTest, int testDuration, string typeCode, out int newExamId)
		{
			return Course.UploadExam(iid, lucid, dateOfTest, testDuration, typeCode, out newExamId);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009850 File Offset: 0x00007A50
		public static Exception UploadExam(int iid, int lucid, DateTime dateOfTest, int testDuration, string typeCode, out int newExamId)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			Exception result;
			try
			{
				string query = "INSERT INTO exams (dateoftest,testduration,dateentered,whoentered,lucourseid,typecode) VALUES (@dateoftest,@testduration,getdate(),@iid,@lucid,@typecode); SELECT CAST(SCOPE_IDENTITY() AS int) AS examid;";
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@dateoftest", DbType.DateTime, dateOfTest),
					clockWork.GetParameter("@testduration", DbType.Int32, testDuration),
					clockWork.GetParameter("@iid", DbType.Int32, iid),
					clockWork.GetParameter("@lucid", DbType.Int32, lucid),
					clockWork.GetParameter("@typecode", DbType.String, typeCode)
				};
				object value = clockWork.ExecuteScalar(query, parameters);
				newExamId = Convert.ToInt32(value);
				result = null;
			}
			catch (Exception ex)
			{
				newExamId = 0;
				result = ex;
			}
			finally
			{
			}
			return result;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000992C File Offset: 0x00007B2C
		public static Course[] LoadCourses(db conn, int pid, DateTime startDate, DateTime endDate)
		{
			DataTable dataTable = Course.LoadStudentsCourses(conn, pid, startDate, endDate);
			bool flag = dataTable.Rows.Count > 0;
			Course[] result;
			if (flag)
			{
				Course[] array = new Course[dataTable.Rows.Count];
				for (int i = 0; i < array.Length; i++)
				{
					DataRow dr = dataTable.Rows[i];
					Course course = new Course(dr);
					array[i] = course;
				}
				result = array;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000099A8 File Offset: 0x00007BA8
		public static Course GetCourse(Course[] courses, int lucourseid)
		{
			bool flag = courses == null;
			Course result;
			if (flag)
			{
				result = null;
			}
			else
			{
				foreach (Course course in courses)
				{
					bool flag2 = course.LuCourseId == lucourseid;
					if (flag2)
					{
						return course;
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000099F4 File Offset: 0x00007BF4
		public static string GetLuCourseIdsAsCommaSeparatedString(Course[] courses)
		{
			string text = "";
			bool flag = courses != null;
			if (flag)
			{
				foreach (Course course in courses)
				{
					bool flag2 = text.Length > 0;
					if (flag2)
					{
						text += ",";
					}
					text += course.LuCourseId.ToString();
				}
			}
			return text;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00009A64 File Offset: 0x00007C64
		public static Person FindInstructor(db conn, IEncryption tripleDES, string iemail, out string passwordHash)
		{
			conn.Da.SelectCommand.CommandText = "SELECT lucoursedataid,email,altlookupstring AS instructor,passwordhash FROM lucoursedata WHERE lookuplisttype=1 AND email=@email";
			conn.Da.SelectCommand.Parameters.Clear();
			conn.Da.SelectCommand.Parameters.Add("@email", iemail);
			DataTable dataTable = new DataTable();
			conn.Da.Fill(dataTable);
			bool flag = dataTable.Rows.Count > 0;
			Person result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				int personid = (int)dataRow[0];
				passwordHash = ((dataRow["passwordhash"] == DBNull.Value) ? "" : ((string)dataRow["passwordhash"]));
				Person person = new Person(personid, dataRow[2].ToString(), dataRow[1].ToString());
				result = person;
			}
			else
			{
				passwordHash = "";
				result = null;
			}
			return result;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00009B5C File Offset: 0x00007D5C
		public static List<TimeTableItem> LoadTimetable(int lucid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, lucid)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_CourseTimetableByCourse, parameters);
			List<TimeTableItem> list = new List<TimeTableItem>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				List<TimeTableItem> timetableItems = TimeTableItem.GetTimetableItems(dr);
				list.AddRange(timetableItems);
			}
			return list;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00009C0C File Offset: 0x00007E0C
		public static List<TimeTableItem> LoadTimetable(int pid, int lucidToExclude, DateTime classTestDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DateTime dateTime;
			DateTime dateTime2;
			Core.GetTermStartEndDates(classTestDate, out dateTime, out dateTime2);
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@lucid", DbType.Int32, lucidToExclude),
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@startdate", DbType.DateTime, dateTime),
				clockWork.GetParameter("@enddate", DbType.DateTime, dateTime2)
			};
			DataTable dataTable = clockWork.ExecuteQuery(QueryStorage.QS_Select_CourseTimetableByStudent2, parameters);
			List<TimeTableItem> list = new List<TimeTableItem>();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				List<TimeTableItem> timetableItems = TimeTableItem.GetTimetableItems(dr);
				bool flag = timetableItems.Count > 0;
				if (flag)
				{
					list.AddRange(timetableItems);
				}
			}
			return list;
		}

		// Token: 0x04000045 RID: 69
		private int luCourseId;

		// Token: 0x04000046 RID: 70
		private string description;

		// Token: 0x04000047 RID: 71
		private Person instructor = null;

		// Token: 0x04000048 RID: 72
		private string name_new = "";

		// Token: 0x04000049 RID: 73
		private string email_new = "";

		// Token: 0x0400004A RID: 74
		private DateTime startDate;

		// Token: 0x0400004B RID: 75
		private DateTime endDate;

		// Token: 0x0400004C RID: 76
		private string duration;

		// Token: 0x0400004D RID: 77
		private string term;

		// Token: 0x0400004E RID: 78
		private int subjectId;

		// Token: 0x0400004F RID: 79
		private string subject;

		// Token: 0x04000050 RID: 80
		private string courseCode;

		// Token: 0x04000051 RID: 81
		private DateTime originalStartDateTime;

		// Token: 0x04000052 RID: 82
		private DateTime originalEndDateTime;

		// Token: 0x04000053 RID: 83
		private string originalDateTime;

		// Token: 0x04000054 RID: 84
		private string subjectEmail;

		// Token: 0x04000055 RID: 85
		private string timeofday;

		// Token: 0x04000056 RID: 86
		private string section;

		// Token: 0x0400005A RID: 90
		private object tag;
	}
}
