using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using ClockWorkWebAPI;
using ClockWorkWebAPI.ClockWorkAPIReplacement;
using Databases;

namespace ClockWorkController
{
	// Token: 0x02000005 RID: 5
	public class Course
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00003148 File Offset: 0x00001348
		public static DataTable LoadInstructorsCoursesNowAndFuture(int iid, out DateTime startDate, out DateTime endDate)
		{
			Core.GetTermStartEndDates(out startDate, out endDate);
			DateTime date = DateTime.Now.Date;
			bool flag = startDate > date;
			if (flag)
			{
				startDate = date;
			}
			return ClockWorkController.Course.LoadInstructorsCourses2(iid, startDate, startDate.AddYears(1));
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000319C File Offset: 0x0000139C
		public static DataTable LoadAltContactCoursesNowAndFuture(int altContactId, out DateTime startDate, out DateTime endDate)
		{
			Core.GetTermStartEndDates(out startDate, out endDate);
			DateTime date = DateTime.Now.Date;
			bool flag = startDate > date;
			if (flag)
			{
				startDate = date;
			}
			return ClockWorkController.Course.LoadAltContactCourses(altContactId, startDate, startDate.AddYears(1));
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000031F0 File Offset: 0x000013F0
		public static DataTable LoadInstructorsCourses(int iid, out DateTime startDate, out DateTime endDate)
		{
			Core.GetTermStartEndDates(out startDate, out endDate);
			DateTime date = DateTime.Now.Date;
			bool flag = startDate > date;
			if (flag)
			{
				startDate = date;
			}
			return ClockWorkController.Course.LoadInstructorsCourses2(iid, startDate, endDate);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003244 File Offset: 0x00001444
		public static DataTable LoadInstructorsCourses2(int iid, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@iid", DbType.Int32, iid),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_CourseByInstructor, parameters);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000032B4 File Offset: 0x000014B4
		public static DataTable LoadAltContactCourses(int altContactId, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@altcontactid", DbType.Int32, altContactId),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_CourseByAltContact, parameters);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003324 File Offset: 0x00001524
		public static DataTable LoadAltContactCourses2_OnlyCoursesWhereClassTestDefinitionExists(int altContactId, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@altcontactid", DbType.Int32, altContactId),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_CourseByAltContact, parameters);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003394 File Offset: 0x00001594
		public static DataTable LoadInstructorsCourses2_OnlyCoursesWhereClassTestDefinitionExists(int iid, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@iid", DbType.Int32, iid),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_CourseByInstructor_ExcludeCoursesWhereNoClassTestDefinitionExists, parameters);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003404 File Offset: 0x00001604
		public static DataTable LoadStudentsCourse(int pid, int lucid, DateTime startDate, DateTime endDate)
		{
			return ClockWorkController.Course.LoadStudentsCourses(pid, new List<int>(1)
			{
				lucid
			}, startDate, endDate);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003430 File Offset: 0x00001630
		public static DataTable LoadStudentsCourses(int pid, List<int> lucids)
		{
			return ClockWorkController.Course.LoadStudentsCourses(pid, lucids, DateTime.MinValue, DateTime.MinValue);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003454 File Offset: 0x00001654
		public static DataTable LoadStudentsCourses(int pid, List<int> lucids, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			bool flag = lucids.Count == 1 && startDate != DateTime.MinValue;
			DataTable result;
			if (flag)
			{
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@personid", DbType.Int32, pid),
					clockWork.GetParameter("@lucid", DbType.Int32, lucids[0]),
					clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
					clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
				};
				result = clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentCourse, parameters);
			}
			else
			{
				DbParameter[] parameters = new DbParameter[]
				{
					clockWork.GetParameter("@personid", DbType.Int32, pid),
					clockWork.GetParameter("@lucids", DbType.String, Utility.ListToString(lucids))
				};
				result = clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentCourseMultiple, parameters);
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003540 File Offset: 0x00001740
		public static DataTable LoadStudentsCoursesWithFinalExamDates(int pid, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@personid", DbType.Int32, pid),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentCoursesAndFinalExamDates, parameters);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000035B0 File Offset: 0x000017B0
		public static DataTable LoadStudentsCoursesCurrentTerm_Table(int pid, string campuses)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@pid", DbType.Int32, pid),
				clockWork.GetParameter("@campuses", DbType.String, campuses)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentCoursesCurrentTerm, parameters);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003604 File Offset: 0x00001804
		public static DataTable LoadStudentsCoursesOverlappingNow_Table(int pid, string campuses)
		{
			return ClockWorkController.Course.LoadStudentsCoursesOverlappingNow_Table(pid, campuses, false, false, false);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003620 File Offset: 0x00001820
		public static DataTable LoadStudentsCoursesOverlappingNow_Table(int pid, string campuses, bool onlyCoursesWithLoaGenerated, bool onlyCoursesWithInstructorConfirmedReceiptOfLoa, bool onlyCoursesWithApprovedAccommodationRequest)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] array = new DbParameter[5];
			array[0] = clockWork.GetParameter("@pid", DbType.Int32, pid);
			array[1] = clockWork.GetParameter("@campuses", DbType.String, string.Join(",", (from g in campuses.Split(new char[]
			{
				','
			})
			select g.Trim() into h
			where h.Length > 0
			select h).ToArray<string>()));
			array[2] = clockWork.GetParameter("@onlyCoursesWithLoaGenerated", DbType.Boolean, onlyCoursesWithLoaGenerated);
			array[3] = clockWork.GetParameter("@onlyCoursesWithInstructorConfirmedReceiptOfLoa", DbType.Boolean, onlyCoursesWithInstructorConfirmedReceiptOfLoa);
			array[4] = clockWork.GetParameter("@onlyCoursesWithApprovedAccommodationRequest", DbType.Boolean, onlyCoursesWithApprovedAccommodationRequest);
			DbParameter[] parameters = array;
			string query = "DECLARE @enddate2 datetime\r\nSET @enddate2=(SELECT TOP 1 startdate FROM LUCourses WHERE StartDate BETWEEN GETDATE() AND DATEADD(day,30,getdate()) ORDER BY StartDate)\r\nDECLARE @offset int\r\nSET @offset=coalesce( datediff(day,getdate(),@enddate2),0)\r\n\r\nSELECT c.coursesid,c.dateletterissued,c.dateletterreturned,c.needsnotes,c.extracode,c.coursenote,\r\nluc.lucourseid,luc.startdate,luc.enddate,luc.duration,luc.term,luc.subjectid,luc.course,\r\nluc.timeofday,luc.section,luc.instructorid,luc.equivalentcode AS crosslistequivalentcode,\r\nlucd.altlookupstring AS subject,lucd2.altlookupstring AS instructor,\r\nlucd2.email AS instructoremail,lucd2.phone AS instructorphone,'' AS session \r\nFROM\tcourses c LEFT JOIN lucourses luc ON luc.lucourseid=c.lucourseid \r\nLEFT JOIN lucoursedata lucd ON lucd.lucoursedataid=luc.subjectid \r\nLEFT JOIN lucoursedata lucd2 ON lucd2.lucoursedataid=luc.instructorid \r\nWHERE\tc.personid=@pid \r\nAND dateadd(day,-@offset,getdate()) BETWEEN luc.startdate AND luc.enddate\r\n        AND (c.registrationstatus IS NULL OR NOT c.registrationstatus = 2) \r\n        AND (@campuses='' OR luc.campus IS NULL OR luc.campus='' OR (NOT luc.campus IS NULL AND luc.campus IN (SELECT orderid AS campus FROM splitstrings\r\n\r\n(@campuses))))\r\n        AND (@onlyCoursesWithLoaGenerated=0 OR NOT c.dateletterissued IS NULL)\r\n        AND (@onlyCoursesWithInstructorConfirmedReceiptOfLoa=0 OR NOT c.dateletterreturned IS NULL)\r\n        AND (@onlyCoursesWithApprovedAccommodationRequest=0 OR EXISTS(SELECT studentcourseaccommodationrequestid FROM studentcourseaccommodationrequest \r\n\r\nWHERE status=8 AND personid=c.personid AND lucourseid=c.lucourseid) )\r\nORDER BY luc.startdate,lucd.altlookupstring,luc.course,luc.section";
			return clockWork.ExecuteQuery(query, parameters);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000371C File Offset: 0x0000191C
		public List<ClockWorkWebAPI.ClockWorkAPIReplacement.Course> LoadStudentsCoursesCurrentTerm(int pid, string campuses)
		{
			DataTable dataTable = ClockWorkController.Course.LoadStudentsCoursesCurrentTerm_Table(pid, campuses);
			List<ClockWorkWebAPI.ClockWorkAPIReplacement.Course> list = new List<ClockWorkWebAPI.ClockWorkAPIReplacement.Course>(dataTable.Rows.Count);
			foreach (object obj in dataTable.Rows)
			{
				DataRow dr = (DataRow)obj;
				list.Add(new ClockWorkWebAPI.ClockWorkAPIReplacement.Course(dr));
			}
			return list;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000037A4 File Offset: 0x000019A4
		public static DataTable LoadStudentsCourses(int pid, DateTime startDate, DateTime endDate)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@personid", DbType.Int32, pid),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentCourses, parameters);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003814 File Offset: 0x00001A14
		public static DataTable LoadStudentsCourses(int pid, DateTime startDate, DateTime endDate, string restrictByCampuses)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@personid", DbType.Int32, pid),
				clockWork.GetParameter("@startdate", DbType.DateTime, startDate),
				clockWork.GetParameter("@enddate", DbType.DateTime, endDate),
				clockWork.GetParameter("@campuses", DbType.String, restrictByCampuses)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_Select_StudentCoursesRestrictByCampus, parameters);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003894 File Offset: 0x00001A94
		public static DataTable LoadStudentsCourse2(int pid, int lucid)
		{
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			DbParameter[] parameters = new DbParameter[]
			{
				clockWork.GetParameter("@personid", DbType.Int32, pid),
				clockWork.GetParameter("@lucid", DbType.Int32, lucid)
			};
			return clockWork.ExecuteQuery(QueryStorage.QS_SelectStudentCourse2, parameters);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000038F0 File Offset: 0x00001AF0
		public static Semester GetSemester(eSemester semester)
		{
			bool flag = ClockWorkController.Course.semesters == null;
			if (flag)
			{
				ClockWorkController.Course.semesters = Semester.LoadSemesters();
			}
			Semester semester2 = ClockWorkController.Course.semesters.Find((Semester e) => e.ESemester == semester);
			bool flag2 = semester2 != null;
			Semester result;
			if (flag2)
			{
				result = semester2;
			}
			else
			{
				Semester semester3 = new Semester(semester, Enum.GetName(typeof(eSemester), semester));
				result = semester3;
			}
			return result;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003978 File Offset: 0x00001B78
		public static string Fall
		{
			get
			{
				return ClockWorkController.Course.GetSemester(eSemester.Fall).Title;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00003998 File Offset: 0x00001B98
		public static string Winter
		{
			get
			{
				return ClockWorkController.Course.GetSemester(eSemester.Winter).Title;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000039B8 File Offset: 0x00001BB8
		public static string Spring
		{
			get
			{
				return ClockWorkController.Course.GetSemester(eSemester.Spring).Title;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000039D8 File Offset: 0x00001BD8
		public static string Summer
		{
			get
			{
				return ClockWorkController.Course.GetSemester(eSemester.Summer).Title;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000039F8 File Offset: 0x00001BF8
		public static string SpringSummer
		{
			get
			{
				return ClockWorkController.Course.GetSemester(eSemester.SpringSummer).Title;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003A18 File Offset: 0x00001C18
		public static DataTable LoadInstructorOrAltContactCourseNowOrFuture(int iid, int altContactId)
		{
			bool flag = iid > 0 && altContactId > 0;
			DataTable dataTable;
			if (flag)
			{
				DateTime dateTime;
				DateTime dateTime2;
				dataTable = ClockWorkController.Course.LoadInstructorsCoursesNowAndFuture(iid, out dateTime, out dateTime2);
				DataTable dataTable2 = ClockWorkController.Course.LoadAltContactCoursesNowAndFuture(altContactId, out dateTime, out dateTime2);
				List<DataRow> list = new List<DataRow>();
				foreach (object obj in dataTable2.Rows)
				{
					DataRow item = (DataRow)obj;
					list.Add(item);
				}
				foreach (DataRow dataRow in list)
				{
					DataRow[] array = dataTable.Select("lucourseid=" + dataRow["lucourseid"].ToString());
					bool flag2 = array.Length < 1;
					if (flag2)
					{
						dataTable.ImportRow(dataRow);
					}
				}
			}
			else
			{
				bool flag3 = iid > 0;
				if (flag3)
				{
					DateTime dateTime;
					DateTime dateTime2;
					dataTable = ClockWorkController.Course.LoadInstructorsCoursesNowAndFuture(iid, out dateTime, out dateTime2);
				}
				else
				{
					DateTime dateTime;
					DateTime dateTime2;
					dataTable = ClockWorkController.Course.LoadAltContactCoursesNowAndFuture(altContactId, out dateTime, out dateTime2);
				}
			}
			return dataTable;
		}

		// Token: 0x04000001 RID: 1
		private static List<Semester> semesters;
	}
}
