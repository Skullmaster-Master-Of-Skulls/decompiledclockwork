using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.Common.UI.Web.AccommodationsRequest.Adapters
{
	// Token: 0x02000009 RID: 9
	public static class LookupCoursesAdapter
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000050C4 File Offset: 0x000032C4
		public static string GetCheckBoxCourseDescription(this LookupCourseDTO course)
		{
			bool flag = course == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				LookupInstructorDTO primaryInstructor = course.GetPrimaryInstructor();
				result = string.Format("<b>{0} {1}</b> section {2} {3}<br /><span style='font-size: .85em;'>{4} {5}</span>", new object[]
				{
					(course.Subject == null || course.Subject.SubjectDescription == null) ? "" : course.Subject.SubjectDescription,
					course.Course ?? "",
					course.Section ?? "",
					course.TimeOfDay ?? "",
					(primaryInstructor == null) ? "" : primaryInstructor.GetInstructorName(),
					(primaryInstructor == null) ? "" : (primaryInstructor.Email ?? "")
				});
			}
			return result;
		}
	}
}
