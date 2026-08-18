using System;
using System.Collections.Generic;
using System.Data;
using ClockWorkAPI;
using ClockWorkAPI.Courses;
using ClockWorkLogger;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.UI.ClientManager.ClientCaching.cs;
using UnivOleDb;

namespace ReportFunctions.DataSync
{
	// Token: 0x02000049 RID: 73
	public class Utility
	{
		// Token: 0x0600043E RID: 1086 RVA: 0x0004AECC File Offset: 0x00049ECC
		public static Exception ImportStudentCourses(DataView dv, bool writeChangesToClockWorkDatabase, PersonBaseDTO student, Dictionary<string, string> args)
		{
			int personId = student.PersonId;
			string text = student.Student_no.Trim().ToUpper();
			DataTable table = dv.Table;
			List<DataSyncDateScope> list = new List<DataSyncDateScope>();
			DataSyncDateScope nowScope = new DataSyncDateScope();
			list.Add(nowScope);
			Exception result;
			try
			{
				UnivDataAdapter da = ClientCache.CurrentInstance.da;
				TripleDESEncryptionClass tripleDES = ClientCache.CurrentInstance.tripleDES;
				string[] array = new string[]
				{
					"student_no",
					"duration",
					"term",
					"subject",
					"course",
					"timeofday",
					"section",
					"startdate",
					"enddate",
					"instructorfirstname",
					"instructorlastname",
					"instructoremail",
					"instructorphone",
					"instructorusername"
				};
				List<string> list2 = new List<string>();
				foreach (string text2 in array)
				{
					if (!table.Columns.Contains(text2))
					{
						table.Columns.Add(text2);
						list2.Add(text2);
					}
				}
				CWLogger.Logger.Info("ImportStudentCourses:Start:snum={0}:missingcolumns={1}", (student == null) ? "NULL STUDENT" : student.Student_no, string.Join(", ", list2.ToArray()));
				DataRow[] array3 = table.Select(string.Format("NOT student_no='' AND student_no='{0}'", text));
				DataTable dataTable = table.Clone();
				foreach (DataRow row in array3)
				{
					dataTable.ImportRow(row);
				}
				DataView dataView = new DataView();
				dataTable.TableName = "extcourses";
				dataView.Table = dataTable;
				dataView.Sort = "student_no,duration,term,subject,course,timeofday,section";
				List<Course> allExternalCourses = new List<Course>();
				List<DataSyncCourse> list3 = new List<DataSyncCourse>(allExternalCourses.Count);
				foreach (object obj in dataView)
				{
					DataRowView dataRowView = (DataRowView)obj;
					Course course5 = new Course(dataRowView.Row);
					allExternalCourses.Add(course5);
					list3.Add(new DataSyncCourse
					{
						ExternalCourse = course5,
						PendingDataSyncAction = DataSyncCourseAction.DoNothing
					});
				}
				List<Course> nowCourses = allExternalCourses.FindAll((Course e) => !(e.EndDate.Date <= nowScope.StartDate.Date) && !(e.StartDate.Date >= nowScope.EndDate.Date));
				List<Course> list4 = allExternalCourses.FindAll((Course e2) => !nowCourses.Contains(e2));
				nowScope.ExternalCourses = nowCourses;
				Course course;
				foreach (Course course2 in list4)
				{
					course = course2;
					DataSyncDateScope dataSyncDateScope = list.Find((DataSyncDateScope sc) => sc.ShouldContain(course));
					if (dataSyncDateScope == null)
					{
						dataSyncDateScope = new DataSyncDateScope(course.StartDate.AddDays(1.0).Date);
						list.Add(dataSyncDateScope);
					}
					Course course3 = dataSyncDateScope.ExternalCourses.Find((Course ef) => ef.IsSameCourse(course));
					if (course3 == null)
					{
						dataSyncDateScope.ExternalCourses.Add(course);
					}
				}
				foreach (DataSyncDateScope dataSyncDateScope2 in list)
				{
					dataSyncDateScope2.ClockWorkCourses = Course.LoadStudentsCoursesForDataSync(personId, dataSyncDateScope2.StartDate, dataSyncDateScope2.EndDate);
				}
				List<Course> createdCourses = new List<Course>();
				foreach (Course course4 in allExternalCourses)
				{
					bool flag;
					int num = course4.LookupLuCourseId(out flag);
					if (num > 0)
					{
						course4.LuCourseId = num;
						if (flag)
						{
							createdCourses.Add(course4);
						}
					}
					else
					{
						CWLogger.Logger.Debug(string.Format("DataSync.ImportStudentCourses.lookupforlucids:subject:{0}:course:{1}:section:{2}", course4.Subject, course4.CourseCode, course4.Section));
					}
				}
				List<Course> list5 = allExternalCourses.FindAll((Course r) => r.LuCourseId > 0 && !createdCourses.Contains(r));
				foreach (Course course4 in list5)
				{
					Utility.UpdateCourseInfo(course4);
				}
				DataSyncDateScope scope;
				foreach (DataSyncDateScope scope2 in list)
				{
					scope = scope2;
					Course c2;
					Course c;
					List<Course> list6 = scope.ClockWorkCourses.FindAll((Course c) => allExternalCourses.Find((Course c2) => c2.LuCourseId == c.LuCourseId) != null);
					List<Course> list7 = allExternalCourses.FindAll((Course c) => scope.ClockWorkCourses.Find((Course c2) => c2.LuCourseId == c.LuCourseId) == null);
					foreach (Course c3 in list6)
					{
						c = c3;
						DataSyncCourse dataSyncCourse = list3.Find((DataSyncCourse r) => r.ExternalCourse.LuCourseId == c.LuCourseId);
						dataSyncCourse.ClockWorkCourse = c;
						dataSyncCourse.PendingDataSyncAction = DataSyncCourseAction.Drop;
					}
					using (List<Course>.Enumerator enumerator2 = list7.GetEnumerator())
					{
						Course c;
						while (enumerator2.MoveNext())
						{
							c2 = enumerator2.Current;
							c = c2;
							DataSyncCourse dataSyncCourse = list3.Find((DataSyncCourse r) => r.ExternalCourse.LuCourseId == c.LuCourseId);
							dataSyncCourse.ClockWorkCourse = c;
							dataSyncCourse.PendingDataSyncAction = DataSyncCourseAction.Add;
						}
					}
				}
				foreach (DataSyncCourse dataSyncCourse in list3)
				{
					DataSyncCourse dataSyncCourse;
					if (dataSyncCourse.ClockWorkCourse != null && dataSyncCourse.ClockWorkCourse.LuCourseId > 0)
					{
						switch (dataSyncCourse.PendingDataSyncAction)
						{
						case DataSyncCourseAction.Add:
						{
							string commandText = "IF NOT EXISTS(SELECT coursesid FROM courses WHERE personid=@pid AND lucourseid=@lucid)\r\n        INSERT INTO courses (personid,lucourseid,dateadded,whoadded,registrationstatus) VALUES (@pid,@lucid,getdate(),-444,1)\r\n    ELSE\r\n        UPDATE courses SET registrationstatus=1 WHERE registrationstatus=2 AND lucourseid=@lucid AND personid=@pid";
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@pid", personId);
							da.SelectCommand.Parameters.Add("@lucid", dataSyncCourse.ClockWorkCourse.LuCourseId);
							da.Fill(new DataTable());
							CWLogger.Logger.Info("ImportStudentCourses:AddCourse:pid={0}:snum={1}:lucid={2}", personId.ToString(), text, "");
							break;
						}
						case DataSyncCourseAction.Drop:
						{
							string commandText = "UPDATE courses SET registrationstatus=2 \r\n    WHERE NOT registrationstatus=8 AND lucourseid=@lucid AND personid=@pid";
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@pid", personId);
							da.SelectCommand.Parameters.Add("@lucid", dataSyncCourse.ClockWorkCourse.LuCourseId);
							da.Fill(new DataTable());
							CWLogger.Logger.Info("ImportStudentCourses:DropCourse:pid={0}:snum={1}:lucid={2}", personId.ToString(), text, "");
							break;
						}
						case DataSyncCourseAction.UnDrop:
						{
							string commandText = "UPDATE courses SET registrationstatus=1 \r\n    WHERE NOT registrationstatus=8 AND lucourseid=@lucid AND personid=@pid";
							da.SelectCommand.CommandText = commandText;
							da.SelectCommand.Parameters.Clear();
							da.SelectCommand.Parameters.Add("@pid", personId);
							da.SelectCommand.Parameters.Add("@lucid", dataSyncCourse.ClockWorkCourse.LuCourseId);
							da.Fill(new DataTable());
							CWLogger.Logger.Info("ImportStudentCourses:UnDrop:pid={0}:snum={1}:lucid={2}", personId.ToString(), text, "");
							break;
						}
						}
					}
					else
					{
						CWLogger.Logger.Warn("ImportStudentCourses:ExecuteSyncActions:MissingClockWorkCourse:ExternalCourse={0}", dataSyncCourse.ExternalCourse.ToStringDebug());
					}
				}
				result = null;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("ReportFunction:ImportStudentCourses:GeneralFail:pid={0}:snum={1}:emesg={2}", personId.ToString(), text, ex.ToString());
				result = ex;
			}
			return result;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0004BB58 File Offset: 0x0004AB58
		public static bool UpdateCourseInfo(Course course)
		{
			DatabaseLayer instance = DatabaseLayer.GetInstance();
			Course clockWorkCourse = new Course(course.LuCourseId);
			if (clockWorkCourse != null && clockWorkCourse.LuCourseId > 0)
			{
				Instructor prof2;
				List<Instructor> list = course.Instructors.FindAll((Instructor prof) => clockWorkCourse.Instructors.Find((Instructor prof2) => prof2.InstructorId == prof.InstructorId) != null);
				List<Instructor> list2 = course.Instructors.FindAll((Instructor prof) => clockWorkCourse.Instructors.Find((Instructor prof2) => prof2.InstructorId == prof.InstructorId) == null);
				List<Instructor> list3 = clockWorkCourse.Instructors.FindAll((Instructor prof) => course.Instructors.Find((Instructor prof2) => prof2.InstructorId == prof.InstructorId) == null);
				using (List<Instructor>.Enumerator enumerator = list.GetEnumerator())
				{
					Instructor prof;
					while (enumerator.MoveNext())
					{
						prof2 = enumerator.Current;
						prof = prof2;
						Utility.UpdateInstructorInfo(prof, clockWorkCourse.Instructors.Find((Instructor p) => p.InstructorId == prof.InstructorId));
					}
				}
				foreach (Instructor instructor in list3)
				{
					Utility.UpdateInstructorInfo(instructor);
					Utility.DropInstructor(course, instructor);
					course.Instructors.Remove(instructor);
				}
				foreach (Instructor instructor in list2)
				{
					Utility.UpdateInstructorInfo(instructor);
					Utility.AddInstructor(course, instructor);
					course.Instructors.Add(instructor);
				}
				if (course.Location.Length > 0 && !course.Location.Equals(clockWorkCourse.Location, StringComparison.OrdinalIgnoreCase))
				{
					string commandText = "UPDATE lucourses SET location=@location WHERE lucourseid=@lucid";
					UnivDataAdapter da = ClientCache.CurrentInstance.da;
					da.SelectCommand.CommandText = commandText;
					da.SelectCommand.Parameters.Clear();
					da.SelectCommand.Parameters.Add("@location", course.Location);
					da.SelectCommand.Parameters.Add("@lucid", course.LuCourseId);
					da.Fill(new DataTable());
				}
			}
			return false;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0004BE80 File Offset: 0x0004AE80
		private static bool UpdateInstructorInfo(Instructor prof)
		{
			return Utility.UpdateInstructorInfo(prof, null);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0004BE9C File Offset: 0x0004AE9C
		private static bool UpdateInstructorInfo(Instructor prof, Instructor compareProf)
		{
			if (compareProf == null)
			{
				compareProf = prof;
			}
			if (prof != null && compareProf != null)
			{
				if ((prof.FirstName.Length > 0 && !prof.FirstName.Equals(compareProf.FirstName, StringComparison.OrdinalIgnoreCase)) || (prof.LastName.Length > 0 && !prof.LastName.Equals(compareProf.LastName, StringComparison.OrdinalIgnoreCase)) || (prof.Email.Length > 0 && !prof.Email.Equals(compareProf.Email, StringComparison.OrdinalIgnoreCase)) || (prof.Phone.Length > 0 && !prof.Phone.Equals(compareProf.Phone, StringComparison.OrdinalIgnoreCase)))
				{
				}
			}
			return false;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0004BF64 File Offset: 0x0004AF64
		private static bool DropInstructor(Course course, Instructor prof)
		{
			return false;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0004BF78 File Offset: 0x0004AF78
		private static bool AddInstructor(Course course, Instructor prof)
		{
			return false;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0004BF8C File Offset: 0x0004AF8C
		public static void CreateLookupCourse(DataSyncCourse dsc)
		{
			Subject subject = new Subject(dsc.ExternalCourse.Subject);
			Subject.LookupSubjectId(ref subject, true);
			if (subject.SubjectId > 0)
			{
			}
		}

		// Token: 0x04000251 RID: 593
		public static string[] daysOfWeek = new string[]
		{
			"sun",
			"mon",
			"tue",
			"wed",
			"thu",
			"fri",
			"sat"
		};
	}
}
