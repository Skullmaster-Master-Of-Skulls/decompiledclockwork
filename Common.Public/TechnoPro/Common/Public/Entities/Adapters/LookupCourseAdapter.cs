using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.Adapters
{
	// Token: 0x020005DB RID: 1499
	public static class LookupCourseAdapter
	{
		// Token: 0x06003042 RID: 12354 RVA: 0x0003E238 File Offset: 0x0003C438
		public static LookupInstructor GetPrimaryInstructor(this LookupCourse Course)
		{
			bool flag = Course == null || Course.Instructors == null;
			LookupInstructor result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LookupInstructor lookupInstructor = Course.Instructors.Find((LookupInstructor i) => i.IsPrimary);
				bool flag2 = lookupInstructor == null && Course.Instructors.Count > 0;
				if (flag2)
				{
					lookupInstructor = Course.Instructors[0];
				}
				result = lookupInstructor;
			}
			return result;
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x0003E2B4 File Offset: 0x0003C4B4
		public static string GetCourseDescription(this LookupCourse Course)
		{
			bool flag = Course == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				LookupInstructor primaryInstructor = Course.GetPrimaryInstructor();
				string text = (primaryInstructor != null && primaryInstructor.InstructorId > 0) ? primaryInstructor.Name : "";
				result = string.Format("{0} {1} section {2} {3} ({4}){5}{6}", new object[]
				{
					Course.Subject.SubjectDescription,
					Course.Course,
					Course.Section,
					Course.TimeOfDay,
					Course.Term,
					(text.Length > 0) ? ": " : "",
					text
				});
			}
			return result;
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x0003E358 File Offset: 0x0003C558
		public static string GetCourseDescription(this LookupCourseBase Course)
		{
			bool flag = Course == null;
			string result;
			if (flag)
			{
				result = "NULL";
			}
			else
			{
				string text = "";
				result = string.Format("{0} {1} section {2} {3} ({4}){5}{6}", new object[]
				{
					Course.Subject.SubjectDescription,
					Course.Course,
					Course.Section,
					Course.TimeOfDay,
					Course.Term,
					(text.Length > 0) ? ": " : "",
					text
				});
			}
			return result;
		}
	}
}
