using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Adapters
{
	// Token: 0x02000C85 RID: 3205
	public static class LookupCourseAdapter
	{
		// Token: 0x060042C8 RID: 17096 RVA: 0x00021E18 File Offset: 0x00020018
		public static LookupInstructorDTO GetPrimaryInstructor(this LookupCourseDTO Course)
		{
			bool flag = Course == null || Course.Instructors == null;
			LookupInstructorDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LookupInstructorDTO lookupInstructorDTO = Course.Instructors.Find((LookupInstructorDTO i) => i.IsPrimary);
				bool flag2 = lookupInstructorDTO == null && Course.Instructors.Count > 0;
				if (flag2)
				{
					lookupInstructorDTO = Course.Instructors[0];
				}
				result = lookupInstructorDTO;
			}
			return result;
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x00021E94 File Offset: 0x00020094
		public static string GetPrimaryInstructorDescription(this LookupCourseDTO Course)
		{
			bool flag = Course == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				LookupInstructorDTO primaryInstructor = Course.GetPrimaryInstructor();
				bool flag2 = primaryInstructor == null;
				if (flag2)
				{
					result = "";
				}
				else
				{
					string text = primaryInstructor.Email ?? "";
					string text2 = primaryInstructor.Phone ?? "";
					string text3 = primaryInstructor.Username ?? "";
					string text4 = primaryInstructor.EmployeeId ?? "";
					string instructorName = primaryInstructor.GetInstructorName();
					text = ((text.Length > 0) ? (" email: " + text) : "");
					text2 = ((text2.Length > 0) ? (" phone: " + text2) : "");
					text3 = ((text3.Length > 0) ? (" username: " + text3) : "");
					text4 = ((text4.Length > 0) ? (" Id: " + text4) : "");
					result = string.Format("{0}{1}{2}{3}{4}", new object[]
					{
						instructorName,
						text,
						text2,
						text3,
						text4
					});
				}
			}
			return result;
		}

		// Token: 0x060042CA RID: 17098 RVA: 0x00021FC0 File Offset: 0x000201C0
		public static string GetCourseDescription(this LookupCourseDTO Course, string overrideTemplate)
		{
			return Course.GetCourseDescription(overrideTemplate, null);
		}

		// Token: 0x060042CB RID: 17099 RVA: 0x00021FDC File Offset: 0x000201DC
		public static string GetCourseDescription(this LookupCourseDTO Course, string overrideTemplate, LookupCourseDisplayOptionsDTO displayOptions)
		{
			bool flag = Course == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LookupInstructorDTO primaryInstructor = Course.GetPrimaryInstructor();
				string text = (primaryInstructor != null && primaryInstructor.InstructorId > 0) ? primaryInstructor.GetInstructorName() : "";
				bool flag2 = displayOptions != null && displayOptions.IncludeSubjectLongIfAvailable;
				bool flag3 = flag2;
				string text4;
				if (flag3)
				{
					LookupSubjectDTO subject = Course.Subject;
					string text2 = (((subject != null) ? subject.SubjectCode : null) ?? "").Trim();
					string text3;
					if (text2.Length <= 0)
					{
						LookupSubjectDTO subject2 = Course.Subject;
						text3 = (((subject2 != null) ? subject2.SubjectDescription : null) ?? string.Empty);
					}
					else
					{
						LookupSubjectDTO subject3 = Course.Subject;
						text3 = (((subject3 != null) ? subject3.SubjectDescription : null) ?? string.Empty) + " (" + text2 + ")";
					}
					text4 = text3;
				}
				else
				{
					LookupSubjectDTO subject4 = Course.Subject;
					text4 = (((subject4 != null) ? subject4.SubjectDescription : null) ?? string.Empty);
				}
				result = string.Format(overrideTemplate, new object[]
				{
					text4,
					Course.Course ?? "",
					Course.Section ?? "",
					Course.TimeOfDay ?? "",
					Course.Term ?? "",
					string.IsNullOrEmpty(Course.Campus) ? "" : (" " + Course.Campus + " "),
					(text.Length > 0) ? (": " + text) : ""
				});
			}
			return result;
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x00022170 File Offset: 0x00020370
		public static string GetCourseDescription(this LookupCourseDTO Course)
		{
			return Course.GetCourseDescription(null);
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x0002218C File Offset: 0x0002038C
		public static string GetCourseDescription(this LookupCourseDTO Course, LookupCourseDisplayOptionsDTO displayOptions)
		{
			return Course.GetCourseDescription("{0} {1} section {2} {3} ({4}){5}{6}", displayOptions);
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x000221AC File Offset: 0x000203AC
		public static string GetCourseDescriptionShort(this LookupCourseDTO Course)
		{
			return Course.GetCourseDescriptionShort("{0} {1} sect. {2} {3} ({4}){5}{6}");
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x000221CC File Offset: 0x000203CC
		public static string GetCourseDescriptionShort(this LookupCourseDTO Course, string overrideTemplate)
		{
			bool flag = Course == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = string.Format(overrideTemplate, new object[]
				{
					(Course.Subject == null || Course.Subject.SubjectDescription == null) ? "" : Course.Subject.SubjectDescription,
					Course.Course ?? "",
					Course.Section ?? "",
					Course.TimeOfDay ?? "",
					Course.Term ?? "",
					string.IsNullOrEmpty(Course.Campus) ? "" : " ",
					Course.Campus ?? ""
				});
			}
			return result;
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x0002229C File Offset: 0x0002049C
		public static string GetCourseDescription(this LookupCourseBaseDTO CourseBase)
		{
			return CourseBase.GetCourseDescription("{0} {1} section {2} {3} ({4}){5}");
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x000222BC File Offset: 0x000204BC
		public static string GetCourseDescription(this LookupCourseBaseDTO CourseBase, string overrideTemplate)
		{
			bool flag = CourseBase == null;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object[] array = new object[6];
				int num = 0;
				LookupSubjectDTO subject = CourseBase.Subject;
				array[num] = (((subject != null) ? subject.SubjectDescription : null) ?? "");
				array[1] = (CourseBase.Course ?? "");
				array[2] = (CourseBase.Section ?? "");
				array[3] = (CourseBase.TimeOfDay ?? "");
				array[4] = (CourseBase.Term ?? "");
				array[5] = (string.IsNullOrEmpty(CourseBase.Campus) ? "" : (" " + CourseBase.Campus));
				result = string.Format(overrideTemplate, array);
			}
			return result;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x00022378 File Offset: 0x00020578
		public static bool IsExemptFromDataSync(this CourseRegistrationDTO CourseRegistration)
		{
			return CourseRegistration.RegistrationStatus == eRegistrationStatusDTO.NormalAndExemptFromDataSync;
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x00022394 File Offset: 0x00020594
		public static string GetDescription(this SessionDTO Session)
		{
			return string.Format("{0} {1}", Session.StartDate.Year.ToString(), Session.AcademicTerm.Title);
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000223D4 File Offset: 0x000205D4
		public static string GetPermissionDescription(this int PermissionLevel)
		{
			List<string> list = new List<string>();
			bool flag = (PermissionLevel & 1) > 0;
			if (flag)
			{
				list.Add("Receive emails");
			}
			bool flag2 = (PermissionLevel & 2) > 0;
			if (flag2)
			{
				list.Add("Access tests online");
			}
			bool flag3 = (PermissionLevel & 4) > 0;
			if (flag3)
			{
				list.Add("Access accommodation letters online");
			}
			return string.Join(", ", list.ToArray());
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x00022440 File Offset: 0x00020640
		public static string GetTimetableDescription(this List<LookupTimetableItemDTO> items)
		{
			bool flag = items == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Join(", ", items.ConvertAll<string>((LookupTimetableItemDTO f) => f.GetTimetableDescription()).ToArray());
			}
			return result;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x00022498 File Offset: 0x00020698
		public static string GetTimetableDescription(this LookupTimetableItemDTO item)
		{
			bool flag = item == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				result = string.Format("{0} [{1} to {2}]", Enum.GetName(typeof(DayOfWeek), item.DayOfWeek), item.StartTime.ConvertToDateTime().ToString("h:mm tt"), item.EndTime.ConvertToDateTime().ToString("h:mm tt"));
			}
			return result;
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x00022510 File Offset: 0x00020710
		public static DateTime ConvertToDateTime(this TimeSpan ts)
		{
			return DateTime.Now.Date.AddMinutes(ts.TotalMinutes);
		}
	}
}
